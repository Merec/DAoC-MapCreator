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
using System.Linq;
using System.IO;
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
            MainForm.ProgressStart("Rendering background ...");

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
                        //mapTile.Interpolate = PixelInterpolateMethod.Bilinear;
                        mapTile.AdaptiveResize(newSize, newSize);
                        //mapTile.Write("tile" + col + "_" + row + ".jpg");
                        map.Composite(mapTile, x, y, CompositeOperator.SrcOver);
                         
                        // Calculate new y
                        y += mapTile.Height;
                        lastWidth = mapTile.Height;
                    }
                }

                x += lastWidth;

                int percent = 100 * col / 8;
                MainForm.ProgressUpdate(percent);
            }

            MainForm.ProgressStartMarquee("Merging ...");

            // Remove rounding fails
            map.Trim();

            // Flip if set
            if (this.flipX) map.Flop();
            if (this.flipY) map.Flip();

            // Sharpen (tested a lot, seems to be the best values)
            //map.Sharpen(4, 3);

            MainForm.ProgressReset();

            return map;
        }
    }
}
