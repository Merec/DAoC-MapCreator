using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ImageMagick;
using System.Drawing;
using MPKLib;

namespace MapCreator
{
    class MapBackground
    {
        private ZoneConfiguration zoneConfiguration;

        private string textureZoneDataDirectory;

        private string textureZoneId;

        private bool flipX = false;

        private bool flipY = false;

        public MapBackground(ZoneConfiguration zoneConfiguration)
        {
            this.zoneConfiguration = zoneConfiguration;
            this.textureZoneId = zoneConfiguration.ZoneId;
            this.textureZoneDataDirectory = zoneConfiguration.ZoneDirectory;

            string flipX = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "flip_x");
            string flipY = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "flip_y");
            string useTexture = DataWrapper.GetDatFileProperty(zoneConfiguration.SectorDatStreamReader, "terrain", "use_texture");

            if (!string.IsNullOrEmpty(flipX)) this.flipX = (Convert.ToInt32(flipX) != 0) ? true : false;
            if (!string.IsNullOrEmpty(flipY)) this.flipY = (Convert.ToInt32(flipY) != 0) ? true : false;

            if (!string.IsNullOrEmpty(useTexture))
            {
                int useTextureInt = Convert.ToInt32(useTexture);
                useTexture = (useTextureInt < 10) ? "00" + useTextureInt : (useTextureInt < 100) ? "0" + useTextureInt : useTextureInt.ToString();
                this.textureZoneId = useTexture;
                this.textureZoneDataDirectory = zoneConfiguration.GetZoneDirectory(useTexture);
            }
        }

        public MagickImage Draw()
        {
            // Check which terrain file is used
            string texMpk = string.Format("{0}\\tex{1}.mpk", this.textureZoneDataDirectory, this.textureZoneId);
            string lodMpk = string.Format("{0}\\lod{1}.mpk", this.textureZoneDataDirectory, this.textureZoneId);

            // Get the tile dimension
            double tileWidth = 512.0;
            string tileTemplate = "";

            MPAK mpak = new MPAK();
            if (File.Exists(texMpk))
            {
                mpak.Load(texMpk);

                if (mpak.Files.Where(f => f.Name.ToLower() == "tex00-00.dds").Count() > 0)
                {
                    tileTemplate += "tex0{0}-0{1}.dds";
                    tileWidth = 512.0;
                }
            }

            if (string.IsNullOrEmpty(tileTemplate))
            {
                mpak.Load(lodMpk);

                if (mpak.Files.Where(f => f.Name.ToLower() == "lod00-00.dds").Count() > 0)
                {
                    tileTemplate += "lod0{0}-0{1}.dds";
                    tileWidth = 256.0;
                }
            }

            if (string.IsNullOrEmpty(tileTemplate))
            {
                MainForm.Log(string.Format("Zone {0}: No background textures found!", zoneConfiguration.ZoneId), MainForm.LogLevel.error);
                return null;
            }

            // original size
            double orginalWidth = tileWidth * 8;
            double resizeFactor = (double)zoneConfiguration.TargetMapSize / (double)orginalWidth; // 0 - 1

            MainForm.Log(string.Format("Rendering background for zone {0}...", zoneConfiguration.ZoneId), MainForm.LogLevel.notice);
            MainForm.ProgressReset();
            MainForm.ProgressStart("Rendering background ...");

            MagickImage map = new MagickImage(Color.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize);

            int lastWidth = 0;
            int x = 0;
            for (int col = 0; col <= 7; col++)
            {
                int y = 0;
                for (int row = 0; row <= 7; row++)
                {
                    string filename = string.Format(tileTemplate, col, row);

                    using (MagickImage mapTile = new MagickImage(mpak.GetFile(filename).Data))
                    {
                        int newSize = Convert.ToInt32(mapTile.Width * resizeFactor);
                        mapTile.Resize(newSize, newSize);
                        map.Composite(mapTile, x, y, CompositeOperator.SrcOver);

                        // Calculate new y
                        y += mapTile.Height;
                        lastWidth = mapTile.Height;
                    }
                }

                x += lastWidth;

                MainForm.ProgressUpdate(80 / 8 * col);
            }

            // Remove rounding fails
            map.Trim();
            MainForm.ProgressUpdate(85);

            // Flip if set
            if (this.flipX) map.Flop();
            if (this.flipY) map.Flip();
            MainForm.ProgressUpdate(90);

            // Sharpen (tested a lot, seems to be the best values)
            map.Sharpen(4, 3);

            MainForm.Log("Finished background!", MainForm.LogLevel.success);
            MainForm.ProgressUpdate(100);

            return map;
        }
    }
}
