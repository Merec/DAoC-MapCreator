using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ImageMagick;
using System.Drawing.Drawing2D;

namespace MapCreator
{
    class MapRiver
    {
        private ZoneConfiguration zoneConfiguration;

        private List<RiverConfiguration> m_rivers = new List<RiverConfiguration>();
        internal List<RiverConfiguration> Rivers
        {
            get { return m_rivers; }
        }

        private Color m_riverColor;
        public Color RiverColor
        {
            get { return m_riverColor; }
            set { m_riverColor = value; }
        }

        private int m_riverOpacity;
        public int RiverOpacity
        {
            get { return m_riverOpacity; }
            set { m_riverOpacity = value; }
        }

        private bool m_useDefaultColors = true;
        public bool UseDefaultColors
        {
            get { return m_useDefaultColors; }
            set { m_useDefaultColors = value; }
        }

        private bool debug = false;

        public MapRiver(ZoneConfiguration zoneConfiguration)
        {
            MainForm.Log("Parse rivers ...", MainForm.LogLevel.notice);
            this.zoneConfiguration = zoneConfiguration;

            bool riversFound = true;
            int riverIndex = 0;

            while (riversFound)
            {
                string riverIndexString = "river" + ((riverIndex < 10) ? "0" + riverIndex : riverIndex.ToString());

                // Check if there is a section
                string riverCheck = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "name");
                if (string.IsNullOrEmpty(riverCheck))
                {
                    riversFound = false;
                    continue;
                }

                RiverConfiguration rConf = new RiverConfiguration(riverCheck);
                rConf.Texture = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "texture");
                rConf.Multitexture = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "multitexture");
                rConf.Flow = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "flow");
                rConf.Height = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "height"));
                rConf.Bankpoints = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "bankpoints"));
                rConf.Extend_PosX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_PosX");
                rConf.Extend_PosY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_PosY");
                rConf.Extend_NegX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_NegX");
                rConf.Extend_NegY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_NegY");
                rConf.Tesselation = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Tesselation");
                rConf.Type = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "type");


                // Adjust some river heights heights
                if (zoneConfiguration.ZoneId == "168" || zoneConfiguration.ZoneId == "171" || zoneConfiguration.ZoneId == "178")
                {
                    rConf.Height += 30;
                }


                string color = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "color");
                string baseColor = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "base_color");
                if (color.Length >= 6)
                {
                    rConf.Color = ColorTranslator.FromWin32(Convert.ToInt32((string.IsNullOrEmpty(baseColor)) ? color : baseColor));
                }

                for (int i = 0; i < rConf.Bankpoints; i++)
                {
                    string coordinatesIndexString = (i < 10) ? "0" + i : i.ToString();
                    string left = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "left" + coordinatesIndexString);
                    string right = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "right" + coordinatesIndexString);

                    if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
                    {
                        continue;
                    }

                    string[] leftArr = left.Split(',');
                    string[] rightArr = right.Split(',');

                    Coordinate leftPoint = new Coordinate(Convert.ToInt32(leftArr[0]), Convert.ToInt32(leftArr[1]));
                    if (leftPoint.X < 0) leftPoint.X = 0;
                    if (leftPoint.Y < 0) leftPoint.Y = 0;

                    rConf.LeftCoordinates.Add(leftPoint);

                    Coordinate rightPoint = new Coordinate(Convert.ToInt32(rightArr[0]), Convert.ToInt32(rightArr[1]));
                    if (rightPoint.X < 0) rightPoint.X = 0;
                    if (rightPoint.Y < 0) rightPoint.Y = 0;

                    rConf.RightCoordinates.Add(rightPoint);
                }

                this.m_rivers.Add(rConf);

                riverIndex++;
            }

        }

        private MagickImage m_waterTexture = null;
        private MagickImage m_lavaTexture = null;

        public MagickImage GetWateryTexture()
        {
            if (m_waterTexture != null) return m_waterTexture;

            string textureFile = string.Format("{0}\\data\\textures\\watery.dds", System.Windows.Forms.Application.StartupPath);
            
            MagickImage tex = new MagickImage(textureFile);
            tex.ColorSpace = ColorSpace.GRAY;
            double texResize = (1 - 256.0 / zoneConfiguration.TargetMapSize) * (zoneConfiguration.TargetMapSize * 0.0001);
            if (texResize < 0) texResize = 0.1;
            tex.Resize(texResize);

            m_waterTexture = tex;
            return m_waterTexture;
        }

        public MagickImage GetLavaTexture()
        {
            if (m_lavaTexture != null) return m_lavaTexture;

            string textureFile = string.Format("{0}\\data\\textures\\lava.dds", System.Windows.Forms.Application.StartupPath);

            MagickImage tex = new MagickImage(textureFile);
            //tex.ColorSpace = ColorSpace.GRAY;
            double texResize = (1 - 256.0 / zoneConfiguration.TargetMapSize) * (zoneConfiguration.TargetMapSize * 0.0001);
            tex.Resize(texResize);

            m_lavaTexture = tex;
            return m_lavaTexture;
        }

        public void Draw(MagickImage map)
        {
            MainForm.ProgressReset();
            MainForm.Log("Drawing water areas ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Drawing water areas ...");

            // Get the heightmap
            //MagickImage heightmap = zoneConfiguration.Heightmap.HeightmapScaled;
            double resizeFactor = zoneConfiguration.TargetMapSize / zoneConfiguration.Heightmap.Heightmap.Width;

            using (PixelCollection heightmapPixels = zoneConfiguration.Heightmap.HeightmapScaled.GetReadOnlyPixels())
            {
                using (MagickImage water = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                {
                    MainForm.ProgressStart("Drawing water ...");
                    int progressCounter = 0;

                    foreach (RiverConfiguration river in m_rivers)
                    {
                        MainForm.Log(river.Name + "...", MainForm.LogLevel.notice);

                        MagickColor fillColor;
                        if (m_useDefaultColors) fillColor = river.Color;
                        else fillColor = m_riverColor;
                        //water.FillColor = fillColor;

                        // Get the river coordinates and scale them to the targets size
                        List<Coordinate> riverCoordinates = river.GetCoordinates().Select(c => new Coordinate(c.X * resizeFactor, c.Y * resizeFactor)).ToList();

                        // Texture
                        using (MagickImage texture = new MagickImage((river.Type.ToLower() == "lava") ? GetLavaTexture() : GetWateryTexture()))
                        {
                            using (MagickImage pattern = new MagickImage(fillColor, texture.Width, texture.Height))
                            {
                                texture.Composite(pattern, 0, 0, CompositeOperator.DstIn);
                                texture.Composite(pattern, 0, 0, CompositeOperator.ColorDodge);

                                water.FillPattern = texture;
                                using (DrawablePolygon poly = new DrawablePolygon(riverCoordinates))
                                {
                                    water.Draw(poly);
                                }
                            }
                        }

                        // get the min/max and just process them
                        int minX = Convert.ToInt32(riverCoordinates.Min(m => m.X)) - 10;
                        int maxX = Convert.ToInt32(riverCoordinates.Max(m => m.X)) + 10;
                        int minY = Convert.ToInt32(riverCoordinates.Min(m => m.Y)) - 10;
                        int maxY = Convert.ToInt32(riverCoordinates.Max(m => m.Y)) + 10;

                        using (WritablePixelCollection riverPixelCollection = water.GetWritablePixels())
                        {
                            for (int x = minX; x < maxX; x++)
                            {
                                if (x < 0) continue;
                                if (x >= zoneConfiguration.TargetMapSize) continue;

                                for (int y = minY; y < maxY; y++)
                                {
                                    if (y < 0) continue;
                                    if (y >= zoneConfiguration.TargetMapSize) continue;

                                    double pixelHeight = heightmapPixels.GetPixel(x, y).GetChannel(0);
                                    if (pixelHeight > river.Height)
                                    {
                                        Pixel newPixel = new Pixel(x, y, new float[] { 0, 0, 0, 256 * 256 });
                                        riverPixelCollection.Set(newPixel);
                                    }
                                }
                            }
                        }

                        if (debug)
                        {
                            DebugRiver(progressCounter, river, riverCoordinates);
                        }

                        int percent = 100 * progressCounter / m_rivers.Count();
                        MainForm.ProgressUpdate(percent);
                        progressCounter++;
                    }

                    MainForm.ProgressStartMarquee("Merging...");
                    water.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 2);
                    water.Blur();
                    map.Composite(water, 0, 0, CompositeOperator.SrcOver);
                }
            }

            MainForm.ProgressReset();
            MainForm.Log("Finished water areas!", MainForm.LogLevel.success);
        }

        private void DebugRiver(int index, RiverConfiguration river, List<Coordinate> riverCoordinates)
        {
            string debugFilename = string.Format("{0}\\debug\\rivers\\{1}_{2}_{3}.jpg", System.Windows.Forms.Application.StartupPath, zoneConfiguration.ZoneId, index, river.Name);

            if (index == 0)
            {
                DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(debugFilename));
                if (di.Exists) di.EnumerateFiles().ToList().ForEach(f => f.Delete());
                else di.Create();
            }

            using (MagickImage debugRiver = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
            {
                debugRiver.BackgroundColor = Color.White;
                debugRiver.FillColor = new MagickColor(0, 0, 256 * 256, 256 * 128);

                double resizeFactor = zoneConfiguration.TargetMapSize / zoneConfiguration.Heightmap.Heightmap.Width;

                using (DrawablePolygon poly = new DrawablePolygon(riverCoordinates))
                {
                    debugRiver.Draw(poly);
                }

                List<Coordinate> orginalCoords = river.GetCoordinates();
                for (int i = 0; i < riverCoordinates.Count(); i++)
                {
                    double x, y;

                    if (riverCoordinates[i].X > zoneConfiguration.TargetMapSize / 2) x = riverCoordinates[i].X - 15;
                    else x = riverCoordinates[i].X + 1;

                    if (riverCoordinates[i].Y < zoneConfiguration.TargetMapSize / 2) y = riverCoordinates[i].Y + 15;
                    else y = riverCoordinates[i].Y - 1;

                    debugRiver.FontPointsize = 14.0;
                    debugRiver.FillColor = Color.Black;
                    using (DrawableText text = new DrawableText(x, y, string.Format("{0} ({1}/{2})", i, orginalCoords[i].X, orginalCoords[i].Y)))
                    {
                        debugRiver.Draw(text);
                    }
                }

                debugRiver.Quality = 100;
                debugRiver.Write(debugFilename);
            }
        }
        
    }

    class RiverConfiguration
    {

        public string Name;
        public string Texture;
        public string Multitexture;
        public string Flow;
        public int Height;
        public int Bankpoints;
        public Color Color;
        public Color baseColor;
        public string Extend_PosX;
        public string Extend_PosY;
        public string Extend_NegX;
        public string Extend_NegY;
        public string Tesselation;
        public string Type;

        public List<Coordinate> LeftCoordinates = new List<Coordinate>();
        public List<Coordinate> RightCoordinates = new List<Coordinate>();

        public RiverConfiguration(string name)
        {
            Name = name;
        }

        public List<Coordinate> GetCoordinates()
        {
            List<Coordinate> newCoordinates = new List<Coordinate>();
            newCoordinates.AddRange(LeftCoordinates);
            newCoordinates.AddRange(RightCoordinates.Reverse<Coordinate>());
            return newCoordinates;
        }
    }
}
