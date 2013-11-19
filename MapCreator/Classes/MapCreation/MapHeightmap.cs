using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public MapHeightmap(ZoneConfiguration zoneConfiguration)
        {
            MainForm.Log("Loading heightmap files ...", MainForm.LogLevel.notice);

            this.zoneConfiguration = zoneConfiguration;
            this.m_terrainfactor = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "scalefactor"));
            this.m_offsetfactor = Convert.ToInt32(DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "offsetfactor"));
            this.m_heightmapFile = new FileInfo(string.Format("{0}\\data\\heightmaps\\zone{1}_heightmap.png", System.Windows.Forms.Application.StartupPath, zoneConfiguration.ZoneId));

            if (!System.IO.Directory.Exists(m_heightmapFile.DirectoryName))
            {
                Directory.CreateDirectory(m_heightmapFile.DirectoryName);
            }
        }

        private void GenerateHeightmap()
        {
            MainForm.ProgressReset();
            MainForm.Log("Generating heightmap ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Generating heightmap ...");

            using (MagickImage offsetmap = zoneConfiguration.GetOffsetMap())
            {
                using (MagickImage terrainmap = zoneConfiguration.GetTerrainMap())
                {
                    using (MagickImage heightmap = new MagickImage(Color.Transparent, offsetmap.Width, offsetmap.Height))
                    {
                        using (WritablePixelCollection heightmapPixels = heightmap.GetWritablePixels())
                        {
                            PixelCollection terrainPixels = terrainmap.GetReadOnlyPixels();
                            PixelCollection offsetPixels = offsetmap.GetReadOnlyPixels();

                            for (int x = 0; x < offsetmap.Width; x++)
                            {
                                for (int y = 0; y < offsetmap.Height; y++)
                                {
                                    float terrainPixelValue = terrainPixels[x, y].GetChannel(0) / 256;
                                    float offsetPixelValue = offsetPixels[x, y].GetChannel(0) / 256;
                                    float heightmapPixelValue = (terrainPixelValue * m_terrainfactor + offsetPixelValue * m_offsetfactor);

                                    heightmapPixels.Set(x, y, new float[] { heightmapPixelValue, heightmapPixelValue, heightmapPixelValue, 0 });
                                }
                                MainForm.ProgressUpdate(100 * x / offsetmap.Width);
                            }

                            heightmapPixels.Write();    
                        }
                        heightmap.Quality = 100;
                        heightmap.Write(m_heightmapFile.FullName);
                    }
                }
            }

            MainForm.Log("Finished heightmap!", MainForm.LogLevel.success);
        }

        public MagickImage GetHeightmap()
        {
            if (!System.IO.File.Exists(m_heightmapFile.FullName))
            {
                GenerateHeightmap();
            }

            return new MagickImage(m_heightmapFile.FullName);
        }

    }
}
