﻿//
// MapCreator
// Copyright(C) 2017 Stefan Schäfer <merec@merec.org>
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
using NifUtil;
using System.Collections;
using SharpDX;

namespace MapCreator
{
    class DrawableFixture
    {
        public string Name;
        public string NifName;

        public FixtureRow FixtureRow;

        public double CanvasX;
        public double CanvasY;
        public double CanvasZ;
        public int CanvasWidth;
        public int CanvasHeight;
        public ImageMagick.MagickColor ModelColor;
        public int ModelTransparency;

        public double Scale;

        public IEnumerable<Polygon> RawPolygons;
        public List<Polygon> ProcessedPolygons = new List<Polygon>();
        public IEnumerable<DrawableElement> DrawableElements = new List<DrawableElement>();

        private ZoneConfiguration m_zoneConf;
        public FixtureRendererConfiguration2 RendererConf;

        public bool IsTree = false; // Trees need some extra love
        public TreeRow Tree;
        public bool IsTreeCluster = false; // TreeCluster at all
        public TreeClusterRow TreeCluster;

        #region Getter/Setter

        public ZoneConfiguration ZoneConf
        {
            get { return m_zoneConf; }
            set { m_zoneConf = value; }
        }

        #endregion

        public bool Calc()
        {
            // Do nothig if we don't want to draw the nif
            if (RendererConf.Renderer == FixtureRenderererType.None)
            {
                return false;
            }

            // Do nothing is there are no polgons
            if (RawPolygons.Count() == 0)
            {
                return false;
            }

            // Calculate X, Y, Z on the map canvas
            //CanvasX = ZoneConf.LocToPixel(FixtureRow.X);
            //CanvasY = ZoneConf.LocToPixel(FixtureRow.Y);

            // Calculate correct Z
            if (FixtureRow.OnGround)
            {
                FixtureRow.Z = ZoneConf.Heightmap.GetHeight(FixtureRow.X, FixtureRow.Y);
            }
            FixtureRow.Z = RawPolygons.SelectMany(p => p.Vectors).Max(p => p.Z) + FixtureRow.Z;
            CanvasZ = ZoneConf.ZoneCoordinateToMapCoordinate(FixtureRow.Z);

            // Transform Polygons
            TransformPolygons();
            return GenerateCanvas();
        }

        private bool GenerateCanvas()
        {
            if (ProcessedPolygons.Count() == 0) return false;

            var vectors = ProcessedPolygons.SelectMany(p => p.Vectors);
            double minX = vectors.Min(p => p.X);
            double maxX = vectors.Max(p => p.X);
            double minY = vectors.Min(p => p.Y);
            double maxY = vectors.Max(p => p.Y);

            // Get the canvas size
            double minXProduct = (minX < 0) ? minX * -1 : minX;
            double maxXProduct = (maxX < 0) ? maxX * -1 : maxX;
            double minYProduct = (minY < 0) ? minY * -1 : minY;
            double maxYProduct = (maxY < 0) ? maxY * -1 : maxY;
            CanvasWidth = Convert.ToInt32((minXProduct < maxXProduct) ? maxXProduct * 2f : minXProduct * 2f);
            CanvasHeight = Convert.ToInt32((minYProduct < maxYProduct) ? maxYProduct * 2f : minYProduct * 2f);

            if(CanvasWidth <= 0 || CanvasHeight <= 0)
            {
                return false;
            }


            // Contains all polygons
            List<DrawableElement> drawlist = new List<DrawableElement>();

            foreach (Polygon poly in ProcessedPolygons)
            {
                Vector3 n = Vector3.Normalize(GetNormal(poly.P1, poly.P2, poly.P3));

                // backface cull
                if (n[2] < 0) continue;

                // shade
                double ndotl = RendererConf.LightVector[0] * n[0] + RendererConf.LightVector[1] * n[1] + RendererConf.LightVector[2] * n[2];
                if (ndotl > 0) ndotl = 0;

                // Lightning must be between 0 and 1, its multiplied with RGB and that must return a ushort
                double lighting = RendererConf.LightMin - (RendererConf.LightMax - RendererConf.LightMin) * ndotl;
                if (lighting < 0) lighting = 0;
                else if (lighting > 1) lighting = 1;

                List<ImageMagick.PointD> coordinates = new List<ImageMagick.PointD>();
                foreach (Vector3 vector in poly.Vectors)
                {
                    coordinates.Add(new ImageMagick.PointD(CanvasWidth / 2 + vector.X, CanvasHeight / 2 - vector.Y));
                }

                // We want to draw the vectors in z-order
                double maxZ = poly.Vectors.Max(p => p.Z);
                drawlist.Add(new DrawableElement(maxZ, lighting, coordinates));
            }

            DrawableElements = drawlist.OrderBy(o => o.order);
            CanvasX = Convert.ToInt32(ZoneConf.ZoneCoordinateToMapCoordinate(FixtureRow.X) - CanvasWidth / 2d);
            CanvasY = Convert.ToInt32(ZoneConf.ZoneCoordinateToMapCoordinate(FixtureRow.Y) - CanvasHeight / 2d);
            ModelColor = RendererConf.Color;
            ModelTransparency = RendererConf.Transparency;
            return true;
        }

        private void TransformPolygons()
        {
            Scale = ((FixtureRow.Scale / 100f) * ZoneConf.LocScale);

            double angle = 360d * FixtureRow.AxisZ3D - FixtureRow.A;

            if (ZoneConf.ZoneId == "330" || ZoneConf.ZoneId == "334" || ZoneConf.ZoneId == "335")
            {
                angle = (360 - FixtureRow.A) * FixtureRow.AxisZ3D;
            }

            Matrix rotation = Matrix.Identity;
            if (angle != 0)
            {
                rotation *= Matrix.RotationZ(Convert.ToSingle(angle * Math.PI / 180.0));
            }

            foreach (Polygon poly in RawPolygons)
            {
                Vector3 p1 = Vector3.TransformCoordinate(poly.P1, rotation);
                Vector3 p2 = Vector3.TransformCoordinate(poly.P2, rotation);
                Vector3 p3 = Vector3.TransformCoordinate(poly.P3, rotation);

                if (Scale != 1)
                {
                    p1.X *= (float)Scale;
                    p1.Y *= (float)Scale;
                    p1.Z *= (float)Scale;

                    p2.X *= (float)Scale;
                    p2.Y *= (float)Scale;
                    p2.Z *= (float)Scale;

                    p3.X *= (float)Scale;
                    p3.Y *= (float)Scale;
                    p3.Z *= (float)Scale;
                }

                // Check visibility of polygons
                Polygon newPolygon = new Polygon(p1, p2, p3);
                if (PolygonArea(newPolygon.Vectors) > 0.01)
                {
                    ProcessedPolygons.Add(newPolygon);
                }
            }
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
            return string.Format("{0} ({1})", Name, NifName);
        }
    }

    struct DrawableElement : IEnumerable
    {
        public double order;
        public double lightning;
        public IEnumerable<ImageMagick.PointD> coordinates;

        public DrawableElement(double order, double lightning, IEnumerable<ImageMagick.PointD> coordinates)
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
}
