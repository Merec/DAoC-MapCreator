using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NifUtil;
using MapCreator.data;
using SharpDX;
using System.Collections;

namespace MapCreator
{
    class FixtureModel : Model
    {
        private double m_x;
        public double X
        {
            get { return m_x; }
        }

        private double m_y;
        public double Y
        {
            get { return m_y; }
        }

        private double m_z;
        public double Z
        {
            get { return m_z; }
        }

        private IEnumerable<Polygon> m_polygons;
        public IEnumerable<Polygon> Polygons
        {
            get { return m_polygons; }
        }

        private FixtureRendererConfiguration m_rendererConfiguration;
        internal FixtureRendererConfiguration RendererConfiguration
        {
            get { return m_rendererConfiguration; }
        }

        private FixtureModelCanvas m_canvas;
        internal FixtureModelCanvas Canvas
        {
            get { return m_canvas; }
        }

        private MapNifs.NifRow m_nifRow;
        public MapNifs.NifRow NifRow
        {
            get { return m_nifRow; }
        }

        private MapNifs.FixturesRow m_fixtureRow;
        public MapNifs.FixturesRow FixtureRow
        {
            get { return m_fixtureRow; }
        }

        public string Name
        {
            get { 
                return string.Format("{0} ({1})", m_fixtureRow.Textual_Name, m_nifRow.Filename);
            }
        }

        public FixtureModel(ZoneConfiguration zoneConfiguration, IEnumerable<Polygon> polys, MapNifs.NifRow nifRow, MapNifs.FixturesRow fixtureRow)
        {
            m_nifRow = nifRow;
            m_fixtureRow = fixtureRow;
            m_polygons = SetPolygons(polys, zoneConfiguration.LocScale);
            if (m_polygons.Count() == 0) return;

            m_x = zoneConfiguration.LocToPixel(fixtureRow.X);
            m_y = zoneConfiguration.LocToPixel(fixtureRow.Y);

            // Calculate Z
            m_z = fixtureRow.Z;
            if (fixtureRow.On_Ground == 1)
            {
                m_z = zoneConfiguration.Heightmap.GetHeight(fixtureRow.X, fixtureRow.Y);
            }
            m_z = polys.SelectMany(p => p.Vectors).Max(p => p.Z) + m_z;

            // Get the renderer configuration
            m_rendererConfiguration = FixtureRenderer.GetRendererConfiguration(nifRow.Filename.ToLower());

            if (m_rendererConfiguration.Renderer != FixtureRendererType.None)
            {
                // Generate a Canvas that can be used for drawing
                GenerateCanvas();
            }
        }

        private void GenerateCanvas()
        {
            m_canvas = new FixtureModelCanvas();

            var vectors = m_polygons.SelectMany(p => p.Vectors);
            double minX = vectors.Min(p => p.X);
            double maxX = vectors.Max(p => p.X);
            double minY = vectors.Min(p => p.Y);
            double maxY = vectors.Max(p => p.Y);
           
            // Get the canvas size
            double minXProduct = (minX < 0) ? minX * -1 : minX;
            double maxXProduct = (maxX < 0) ? maxX * -1 : maxX;
            double minYProduct = (minY < 0) ? minY * -1 : minY;
            double maxYProduct = (maxY < 0) ? maxY * -1 : maxY;
            m_canvas.Width = (int)((minXProduct < maxXProduct) ? maxXProduct * 2f : minXProduct * 2f);
            m_canvas.Height = (int)((minYProduct < maxYProduct) ? maxYProduct * 2f : minYProduct * 2f);

            // Contains all polygons
            List<DrawListEntry> drawlist = new List<DrawListEntry>();

            // Draw flatten image by irgnoring Z
            foreach (Polygon poly in m_polygons)
            {
                Vector3 n = Vector3.Normalize(GetNormal(poly.P1, poly.P2, poly.P3));

                // backface cull
                if (n[2] < 0) continue;

                // shade
                double ndotl = m_rendererConfiguration.LightVector[0] * n[0] + m_rendererConfiguration.LightVector[1] * n[1] + m_rendererConfiguration.LightVector[2] * n[2];
                if (ndotl > 0) ndotl = 0;
                double lighting = m_rendererConfiguration.LightMin - (m_rendererConfiguration.LightMax - m_rendererConfiguration.LightMin) * ndotl;

                List<ImageMagick.Coordinate> coordinates = new List<ImageMagick.Coordinate>();
                foreach (Vector3 vector in poly.Vectors)
                {
                    coordinates.Add(new ImageMagick.Coordinate(m_canvas.Width / 2 + vector.X, m_canvas.Height / 2 - vector.Y));
                }

                // We want to draw the vectors in z-oder
                double maxZ = poly.Vectors.Max(p => p.Z);
                drawlist.Add(new DrawListEntry(maxZ, lighting, coordinates));
            }

            m_canvas.DrawListEntries = drawlist.OrderBy(o => o.order);
            m_canvas.X = Convert.ToInt32(m_x - m_canvas.Width / 2);
            m_canvas.Y = Convert.ToInt32(m_y - m_canvas.Height / 2);
            m_canvas.Color = RendererConfiguration.Color;
            m_canvas.Alpha = 255 * 255 * (100 - RendererConfiguration.Transparency) / 100;
        }

        private IEnumerable<Polygon> SetPolygons(IEnumerable<Polygon> polys, double mapScaleFactor)
        {
            double scale = ((m_fixtureRow.Scale / 100f) * mapScaleFactor);
            double angle = 360f - m_fixtureRow.A;

            Matrix rotation = Matrix.Identity;
            if (angle != 0)
            {
                rotation = Matrix.RotationZ(Convert.ToSingle(angle * Math.PI / 180.0));
            }

            List<Polygon> polygons = new List<Polygon>();
            foreach (Polygon poly in polys)
            {
                Vector3 p1 = Vector3.TransformCoordinate(poly.P1, rotation);
                Vector3 p2 = Vector3.TransformCoordinate(poly.P2, rotation);
                Vector3 p3 = Vector3.TransformCoordinate(poly.P3, rotation);

                if (scale != 1)
                {
                    p1.X *= (float)scale;
                    p1.Y *= (float)scale;
                    p1.Z *= (float)scale;

                    p2.X *= (float)scale;
                    p2.Y *= (float)scale;
                    p2.Z *= (float)scale;

                    p3.X *= (float)scale;
                    p3.Y *= (float)scale;
                    p3.Z *= (float)scale;
                }

                // Check visibility of polygons
                Polygon newPolygon = new Polygon(p1, p2, p3);
                if (PolygonArea(newPolygon.Vectors) > 0.01)
                {
                    polygons.Add(newPolygon);
                }
            }

            return polygons;
        }

        /// <summary>
        /// Gets the area of a polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private double PolygonArea(Vector3[] polygon)
        {
            int i, j;
            double area = 0;

            for (i = 0; i < polygon.Length; i++)
            {
                j = (i + 1) % polygon.Length;
                area += polygon[i].X * polygon[j].Y;
                area -= polygon[i].Y * polygon[j].X;
            }

            area /= 2.0;
            return (area < 0 ? -area : area);
        }

        /// <summary>
        /// Get the normals of a set of Vectors3 for lighning
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 v1 = new Vector3(p2[0] - p1[0], p2[1] - p1[1], p2[2] - p1[2]);
            Vector3 v2 = new Vector3(p3[0] - p2[0], p3[1] - p2[1], p3[2] - p2[2]);
            return Vector3.Cross(v1, v2);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}; Nif: {1}; Renderer: {2}", m_fixtureRow.Textual_Name, m_nifRow.Filename, m_rendererConfiguration.Renderer);
        }

    }

    struct DrawListEntry : IEnumerable
    {
        public double order;
        public double lightning;
        public IEnumerable<ImageMagick.Coordinate> coordinates;

        public DrawListEntry(double order, double lightning, IEnumerable<ImageMagick.Coordinate> coordinates)
        {
            this.order = order;
            this.lightning = lightning;
            this.coordinates = coordinates;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    struct FixtureModelCanvas
    {
        public int Width, Height, X, Y;
        public System.Drawing.Color Color;
        public double Alpha;
        public IEnumerable<DrawListEntry> DrawListEntries;
    }
}
