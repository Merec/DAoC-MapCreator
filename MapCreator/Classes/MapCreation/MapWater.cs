//
// MapCreator
// Copyright(C) 2015 Stefan Schäfer <merec@merec.org>
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using ImageMagick;

namespace MapCreator
{
    class MapWater
    {
        private ZoneConfiguration zoneConfiguration;

        private List<WaterConfiguration> m_waterAreas = new List<WaterConfiguration>();
        internal List<WaterConfiguration> WaterAreas
        {
            get { return m_waterAreas; }
        }

        private Color m_waterColor;
        public Color WaterColor
        {
            get { return m_waterColor; }
            set { m_waterColor = value; }
        }

        private int m_waterTransparency;
        public int WaterTransparency
        {
            get { return m_waterTransparency; }
            set { m_waterTransparency = value; }
        }

        private bool m_useClientColors = true;
        public bool UseClientColors
        {
            get { return m_useClientColors; }
            set { m_useClientColors = value; }
        }

        private bool debug = false;

        public MapWater(ZoneConfiguration zoneConfiguration)
        {
            MainForm.ProgressStartMarquee("Loading water configurations ...");
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

                WaterConfiguration waterConf = new WaterConfiguration(riverCheck);
                waterConf.Texture = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "texture");
                waterConf.Multitexture = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "multitexture");
                waterConf.Flow = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "flow");
                waterConf.Height = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "height"));
                waterConf.Bankpoints = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "bankpoints"));
                waterConf.Extend_PosX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_PosX");
                waterConf.Extend_PosY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_PosY");
                waterConf.Extend_NegX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_NegX");
                waterConf.Extend_NegY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Extend_NegY");
                waterConf.Tesselation = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "Tesselation");
                waterConf.Type = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "type");


                // Adjust some river heights heights
                if (zoneConfiguration.ZoneId == "168" || zoneConfiguration.ZoneId == "171" || zoneConfiguration.ZoneId == "178")
                {
                    waterConf.Height += 30;
                }


                string color = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "color");
                string baseColor = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, riverIndexString, "base_color");
                if (color.Length >= 6)
                {
                    waterConf.Color = ColorTranslator.FromWin32(Convert.ToInt32((string.IsNullOrEmpty(baseColor)) ? color : baseColor));
                }

                for (int i = 0; i < waterConf.Bankpoints; i++)
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

                    waterConf.LeftCoordinates.Add(leftPoint);

                    Coordinate rightPoint = new Coordinate(Convert.ToInt32(rightArr[0]), Convert.ToInt32(rightArr[1]));
                    if (rightPoint.X < 0) rightPoint.X = 0;
                    if (rightPoint.Y < 0) rightPoint.Y = 0;

                    waterConf.RightCoordinates.Add(rightPoint);
                }

                this.m_waterAreas.Add(waterConf);

                riverIndex++;
            }

            MainForm.ProgressReset();
        }

        private MagickImage m_waterTexture = null;
        private MagickImage m_lavaTexture = null;

        public MagickImage GetWateryTexture()
        {
            if (m_waterTexture != null) return m_waterTexture;

            string textureFile = string.Format("{0}\\data\\textures\\watery.dds", System.Windows.Forms.Application.StartupPath);
            
            MagickImage tex = new MagickImage(textureFile);
            tex.ColorSpace = ColorSpace.Gray;
            double texResize = (1 - 256.0 / zoneConfiguration.TargetMapSize) * (zoneConfiguration.TargetMapSize * 0.0001);
            if (texResize < 0) texResize = 0.1;
            tex.Resize(new Percentage(texResize));

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
            tex.Resize(new Percentage(texResize));

            m_lavaTexture = tex;
            return m_lavaTexture;
        }

        public void Draw(MagickImage map)
        {
            MainForm.ProgressStart("Rendering water ...");

            using (PixelCollection heightmapPixels = zoneConfiguration.Heightmap.HeightmapScaled.GetReadOnlyPixels())
            {
                using (MagickImage water = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                {
                    int progressCounter = 0;

                    foreach (WaterConfiguration river in m_waterAreas)
                    {
                        MainForm.Log(river.Name + "...", MainForm.LogLevel.notice);

                        MagickColor fillColor;
                        if (m_useClientColors) fillColor = river.Color;
                        else fillColor = m_waterColor;
                        //water.FillColor = fillColor;

                        // Get the river coordinates and scale them to the targets size
                        List<Coordinate> riverCoordinates = river.GetCoordinates().Select(c => new Coordinate(c.X * zoneConfiguration.MapScale, c.Y * zoneConfiguration.MapScale)).ToList();

                        // Texture
                        using (MagickImage texture = new MagickImage((river.Type.ToLower() == "lava") ? GetLavaTexture() : GetWateryTexture()))
                        {
                            using (MagickImage pattern = new MagickImage(fillColor, texture.Width, texture.Height))
                            {
                                texture.Composite(pattern, 0, 0, CompositeOperator.DstIn);
                                texture.Composite(pattern, 0, 0, CompositeOperator.ColorDodge);

                                water.FillPattern = texture;
                                DrawablePolygon poly = new DrawablePolygon(riverCoordinates);
                                water.Draw(poly);
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

                                    ushort pixelHeight = heightmapPixels.GetPixel(x, y).GetChannel(0);
                                    if (pixelHeight > river.Height)
                                    {
                                        Pixel newPixel = new Pixel(x, y, new ushort[] { 0, 0, 0, ushort.MinValue });
                                        riverPixelCollection.Set(newPixel);
                                    }
                                }
                            }
                        }

                        if (debug)
                        {
                            DebugRiver(progressCounter, river, riverCoordinates);
                        }

                        int percent = 100 * progressCounter / m_waterAreas.Count();
                        MainForm.ProgressUpdate(percent);
                        progressCounter++;
                    }

                    MainForm.ProgressStartMarquee("Merging...");

                    if (WaterTransparency != 0)
                    {
                        water.Alpha(AlphaOption.Set);
                        double divideValue = 100.0 / (100.0 - WaterTransparency);
                        water.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    water.Blur();
                    map.Composite(water, 0, 0, CompositeOperator.SrcOver);
                }
            }

            MainForm.ProgressReset();
        }

        private void DebugRiver(int index, WaterConfiguration river, List<Coordinate> riverCoordinates)
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
                debugRiver.FillColor = new MagickColor(0, 0, ushort.MaxValue, 256 * 128);

                double resizeFactor = zoneConfiguration.TargetMapSize / zoneConfiguration.Heightmap.Heightmap.Width;

                DrawablePolygon poly = new DrawablePolygon(riverCoordinates);
                debugRiver.Draw(poly);
                
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
                    DrawableText text = new DrawableText(x, y, string.Format("{0} ({1}/{2})", i, orginalCoords[i].X, orginalCoords[i].Y));
                    debugRiver.Draw(text);
                }

                debugRiver.Quality = 100;
                debugRiver.Write(debugFilename);
            }
        }
        
    }

    class WaterConfiguration
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

        public WaterConfiguration(string name)
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
