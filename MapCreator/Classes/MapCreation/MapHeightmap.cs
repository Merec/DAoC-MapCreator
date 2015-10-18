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
using ImageMagick;
using System.Drawing;
using System.IO;

namespace MapCreator
{
    public class MapHeightmap
    {
        private ZoneConfiguration zoneConfiguration;
        private int m_terrainfactor = 1;
        private int m_offsetfactor = 1;
        private FileInfo m_heightmapFile;
        bool heightmapGenerated = false;

        private MagickImage m_heightmap = null;
        public MagickImage Heightmap
        {
            get { return m_heightmap; }
        }

        private MagickImage m_heightmapScaled = null;
        public MagickImage HeightmapScaled
        {
            get { return m_heightmapScaled; }
        }

        public MapHeightmap(ZoneConfiguration zoneConfiguration)
        {
            MainForm.Log("Preloading zone heightmap ...", MainForm.LogLevel.notice);

            this.zoneConfiguration = zoneConfiguration;
            this.m_terrainfactor = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "scalefactor"));
            this.m_offsetfactor = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "offsetfactor"));
            this.m_heightmapFile = new FileInfo(string.Format("{0}\\data\\heightmaps\\zone{1}_heightmap.png", System.Windows.Forms.Application.StartupPath, zoneConfiguration.ZoneId));

            if (!System.IO.Directory.Exists(m_heightmapFile.DirectoryName))
            {
                Directory.CreateDirectory(m_heightmapFile.DirectoryName);
            }

            // Generate it, needed for lightmap, river and fixtures
            GenerateHeightmap();
        }

        private void GenerateHeightmap()
        {
            if(this.heightmapGenerated) return;
            MainForm.ProgressStart("Processing heightmap ...");

            using (MagickImage offsetmap = zoneConfiguration.GetOffsetMap())
            {
                using (MagickImage terrainmap = zoneConfiguration.GetTerrainMap())
                {
                    m_heightmap = new MagickImage(Color.Black, offsetmap.Width, offsetmap.Height);

                    using (WritablePixelCollection heightmapPixels = m_heightmap.GetWritablePixels())
                    {
                        PixelCollection terrainPixels = terrainmap.GetReadOnlyPixels();
                        PixelCollection offsetPixels = offsetmap.GetReadOnlyPixels();

                        for (int x = 0; x < offsetmap.Width; x++)
                        {
                            for (int y = 0; y < offsetmap.Height; y++)
                            {
                                ushort terrainPixelValue = (ushort)(terrainPixels[x, y].GetChannel(0) / 256);
                                ushort offsetPixelValue = (ushort)(offsetPixels[x, y].GetChannel(0) / 256);
                                ushort heightmapPixelValue = (ushort)(terrainPixelValue * m_terrainfactor + offsetPixelValue * m_offsetfactor);

                                heightmapPixels.Set(x, y, new ushort[] { heightmapPixelValue, heightmapPixelValue, heightmapPixelValue });
                            }

                            int percent = 100 * x / offsetmap.Width;
                            MainForm.ProgressUpdate(percent);
                        }

                        heightmapPixels.Write();    
                    }

                    MainForm.ProgressStartMarquee("Merging ...");

                    m_heightmap.Quality = 100;
                    m_heightmap.Write(m_heightmapFile.FullName);

                    // Scale to target size
                    m_heightmapScaled = new MagickImage(m_heightmap);
                    m_heightmapScaled.Resize(zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize);
                }
            }

            this.heightmapGenerated = true;
            MainForm.ProgressReset();
        }

        /// <summary>
        /// Get the Height of a specified location
        /// </summary>
        /// <param name="loc_x"></param>
        /// <param name="loc_y"></param>
        /// <returns></returns>
        public double GetHeight(double loc_x, double loc_y)
        {
            int x = Convert.ToInt32(zoneConfiguration.ZoneCoordinateToMapCoordinate(loc_x));
            int y = Convert.ToInt32(zoneConfiguration.ZoneCoordinateToMapCoordinate(loc_y));
            
            if (x == zoneConfiguration.TargetMapSize) x -= 1;
            else if (x < 0) x = 0;
            
            if (y == zoneConfiguration.TargetMapSize) y -= 1;
            else if (y < 0) y = 0;

            return m_heightmapScaled.GetReadOnlyPixels().GetPixel(x, y).GetChannel(0);
        }

        public void Dispose()
        {
            if (m_heightmap != null) m_heightmap.Dispose();
            if (m_heightmapScaled != null) m_heightmapScaled.Dispose();
        }

    }
}
