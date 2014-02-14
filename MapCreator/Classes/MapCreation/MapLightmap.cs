using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ImageMagick;
using System.Drawing;

namespace MapCreator
{

    /// <summary>
    /// Direct conversion of MapperGuis BumpmapRender.py
    /// </summary>
    class MapLightmap
    {
        private ZoneConfiguration zoneConfiguration;

        private double zScale = 20.0;
        public double ZScale
        {
            get { return zScale; }
            set { zScale = value; }
        }

        private double m_lightMin = 0.5;
        public double LightMin
        {
            get { return m_lightMin; }
            set { m_lightMin = value; }
        }

        private double m_lightMax = 1.5;
        public double LightMax
        {
            get { return m_lightMax; }
            set { m_lightMax = value; }
        }

        private double[] lightVector = new double[] { -1.0, 1, -1.0 };
        public double[] ZVector
        {
            get { return lightVector; }
            set { lightVector = value; }
        }

        private double lightBase;
        private double lightScale;

        public MapLightmap(ZoneConfiguration zoneConfiguration)
        {
            this.zoneConfiguration = zoneConfiguration;
            this.RecalculateLights();
        }

        public void RecalculateLights()
        {
            // Set vector and lights
            this.lightVector = Tools.NormalizeVector(this.lightVector);

            double lminScaled = m_lightMin / m_lightMax;
            double baseLight = 255 * lminScaled;
            lightBase = baseLight + (255 - baseLight) / 2;
            lightScale = 255 - lightBase;
        }

        public void Draw(MagickImage map)
        {
            MainForm.ProgressReset();
            MainForm.Log("Generating lightmap ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Generating lightmap ...");

            // Get the heightmap
            MagickImage heightmap = zoneConfiguration.Heightmap.Heightmap;

            using (MagickImage lightmap = new MagickImage(Color.Transparent, 256, 256))
            {
                using (PixelCollection heightmapPixels = heightmap.GetReadOnlyPixels())
                {
                    using (WritablePixelCollection lightmapPixels = lightmap.GetWritablePixels())
                    {
                        // z-component of surface normals
                        double nz = 512d / zScale;
                        double nz_2 = nz * nz;
                        double nzlz = nz * lightVector[2];

                        int y1 = 0, y2 = 0;
                        for (int y = 0; y < lightmap.Height; y++)
                        {
                            if (y == 0) y1 = 0;
                            else y1 = y - 1;
                            if (y == 255) y2 = 255;
                            else y2 = y + 1;

                            int x1 = 0, x2 = 0;
                            for (int x = 0; x < lightmap.Width; x++)
                            {
                                if (x == 0) x1 = 0;
                                else x1 = x - 1;
                                if (x == 255) x2 = 255;
                                else x2 = x + 1;

                                double l = heightmapPixels.GetPixel(x1, y).GetChannel(0);
                                double r = heightmapPixels.GetPixel(x2, y).GetChannel(0);
                                double u = heightmapPixels.GetPixel(x, y1).GetChannel(0);
                                double d = heightmapPixels.GetPixel(x, y2).GetChannel(0);

                                double nx = l - r;
                                double ny = u - d;

                                double m_normal = Math.Sqrt(nx * nx + ny * ny + nz_2);
                                double ndotl = (nx * lightVector[0] + ny * lightVector[1] + nzlz) / m_normal;

                                double pixelValue = lightBase - ndotl * lightScale * 256d;
                                lightmapPixels.Set(x, y, new float[] { (float)pixelValue, (float)pixelValue, (float)pixelValue, 0 });
                            }

                            MainForm.ProgressUpdate(y * 80 / lightmap.Height);
                        }
                    }
                }

                lightmap.Resize(zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize);
                MainForm.ProgressUpdate(90);

                // Apply the bumpmap using ColorDodge
                map.Composite(lightmap, 0, 0, CompositeOperator.ColorDodge);

                MainForm.Log("Finished lightmap!", MainForm.LogLevel.success);
                MainForm.ProgressUpdate(100);
            }
        }

    }
}
