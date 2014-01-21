using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ImageMagick;

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

        public MapRiver(ZoneConfiguration zoneConfiguration)
        {
            MainForm.Log("Parse rivers ...", MainForm.LogLevel.notice);
            this.zoneConfiguration = zoneConfiguration;

            bool riversFound = true;
            bool riverCoordinatesFound = true;

            int riverIndex = 0;
            int coordinatesIndex = 0;

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
                rConf.Bankpoints = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "bankpoints");
                rConf.Extend_PosX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_PosX");
                rConf.Extend_PosY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_PosY");
                rConf.Extend_NegX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_NegX");
                rConf.Extend_NegY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_NegY");
                rConf.Tesselation = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Tesselation");
                rConf.Type = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "type");

                string color = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "color");
                string baseColor = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "base_color");
                if (color.Length >= 6)
                {
                    rConf.Color = ColorTranslator.FromWin32(Convert.ToInt32((string.IsNullOrEmpty(baseColor)) ? color : baseColor));
                }

                coordinatesIndex = 0;
                riverCoordinatesFound = true;
                while (riverCoordinatesFound)
                {
                    string coordinatesIndexString = (coordinatesIndex < 10) ? "0" + coordinatesIndex : coordinatesIndex.ToString();

                    string left = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "left" + coordinatesIndexString);
                    string right = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "right" + coordinatesIndexString);

                    if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
                    {
                        riverCoordinatesFound = false;
                        continue;
                    }

                    //////////////////////////////////
                    // Wrong coordinates in some zones
                    //////////////////////////////////

                    // Campacorrentin Forest: Coordinates after 15 are not correct
                    if (zoneConfiguration.ZoneId == "008" && coordinatesIndex == 16)
                    {
                        // We also need to a new coordinate at 40, 5 left
                        rConf.LeftCoordinates.Add(new Coordinate(40, 5));

                        // Correct right point 15 to 43, 21
                        rConf.RightCoordinates[15] = new Coordinate(43, 21);
                        rConf.RightCoordinates.Add(new Coordinate(48, 16));

                        riverCoordinatesFound = false;
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

                    coordinatesIndex++;
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
            MainForm.Log("Drawing rivers ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Drawing rivers ...");

            // Get the heightmap
            MagickImage heightmap = zoneConfiguration.Heightmap.HeightmapScaled;
            double resizeFactor = (double)zoneConfiguration.TargetMapSize / (double)zoneConfiguration.Heightmap.Heightmap.Width;

            using (PixelCollection heightmapPixels = heightmap.GetReadOnlyPixels())
            {
                int riverCounter = 1;
                foreach (RiverConfiguration river in m_rivers)
                {
                    MainForm.Log("Draw " + river.Name, MainForm.LogLevel.notice);

                    using (MagickImage currentRiverMap = new MagickImage(Color.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                    {
                        List<Coordinate> riverCoordinates = new List<Coordinate>();

                        foreach (Coordinate lCoords in river.LeftCoordinates)
                        {
                            riverCoordinates.Add(new Coordinate(lCoords.X * resizeFactor, lCoords.Y * resizeFactor));
                        }
                        foreach (Coordinate rCoords in river.RightCoordinates.Reverse<Coordinate>())
                        {
                            riverCoordinates.Add(new Coordinate(rCoords.X * resizeFactor, rCoords.Y * resizeFactor));
                        }

                        // Get the min/max bounds
                        int min_x = (int)riverCoordinates.Min(r => r.X);
                        int max_x = (int)riverCoordinates.Max(r => r.X);
                        if (max_x > heightmap.Width) max_x = heightmap.Width - 1;
                        int min_y = (int)riverCoordinates.Min(r => r.Y);
                        int max_y = (int)riverCoordinates.Max(r => r.Y);
                        if (max_y > heightmap.Height) max_y = heightmap.Height - 1;

                        int alpha = (255 * m_riverOpacity / 100);
                        if (river.Type.ToLower() == "lava") alpha = 200;

                        if (m_useDefaultColors)
                        {
                            currentRiverMap.FillColor = Color.FromArgb(alpha, river.Color);
                        }
                        else
                        {
                            currentRiverMap.FillColor = Color.FromArgb(alpha, m_riverColor);
                        }

                        using (DrawablePolygon poly = new DrawablePolygon(riverCoordinates))
                        {
                            currentRiverMap.Draw(poly);
                        }

                        using (WritablePixelCollection riverPixelCollection = currentRiverMap.GetWritablePixels())
                        {
                            for (int x = min_x; x < max_x; x++)
                            {
                                for (int y = min_y; y < max_y; y++)
                                {
                                    double pixelHeight = heightmapPixels.GetPixel(x, y).GetChannel(0);
                                    if (pixelHeight > river.Height)
                                    {
                                        Pixel newPixel = new Pixel(x, y, new float[] { 0, 0, 0, 65536 });
                                        riverPixelCollection.Set(newPixel);
                                    }
                                }
                            }
                        }

                        // Add texture
                        using (MagickImage texture = new MagickImage(Color.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                        {
                            if (river.Type.ToLower() == "lava")
                            {
                                texture.Texture(GetLavaTexture());
                            }
                            else
                            {
                                texture.Texture(GetWateryTexture());
                            }

                            texture.Composite(currentRiverMap, 0, 0, CompositeOperator.DstIn);
                            texture.Composite(currentRiverMap, 0, 0, CompositeOperator.ColorDodge);
                            texture.GaussianBlur(1, 2);
                            map.Composite(texture, 0, 0, CompositeOperator.SrcOver);
                        }

                        MainForm.ProgressUpdate(riverCounter * 100 / m_rivers.Count);
                    }

                    MainForm.ProgressUpdate(100 * riverCounter / m_rivers.Count);
                    riverCounter++;
                }
            }

            if (m_waterTexture != null) m_waterTexture.Dispose();
            if (m_lavaTexture != null) m_lavaTexture.Dispose();

            MainForm.Log("Finished rivers!", MainForm.LogLevel.success);
            MainForm.ProgressUpdate(100);
        }
        
    }

    class RiverConfiguration
    {

        public string Name;
        public string Texture;
        public string Multitexture;
        public string Flow;
        public int Height;
        public string Bankpoints;
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
