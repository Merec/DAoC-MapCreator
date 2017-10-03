//
// MapCreator NifUtil Library
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

using System.Collections.Generic;
using Niflib;
using System.IO;
using SharpDX;
using System;

namespace NifUtil
{
    class ConvertPoly : Convert
    {
        private List<Polygon> m_polys = new List<Polygon>();

        internal List<Polygon> Polys
        {
            get { return m_polys; }
            set { m_polys = value; }
        }

        public ConvertPoly(NiFile niFile)
            :base(niFile)
        {
          
        }

        public void Start()
        {
            WalkNodes(File.FindRoot());
        }

        private void WalkNodes(NiAVObject node)
        {
            if (!IsValidNode(node)) return;

            // Render Children
            if (node is NiTriShape)
            {
                ParseShape((NiTriShape)node);
            }
            else if (node is NiTriStrips)
            {
                ParseStrips((NiTriStrips)node);
            }

            NiNode currentNode = node as NiNode;
            if (currentNode != null)
            {
                if (currentNode.Children.Length > 0)
                {

                    foreach (var child in currentNode.Children)
                    {
                        if (child.IsValid())
                        {
                            WalkNodes(child.Object);
                        }
                    }
                }
            }
        }

        private void ParseShape(NiTriShape shape)
        {
            List<string> export = new List<string>();

            NiTriShapeData geometry = (NiTriShapeData)shape.Data.Object;

            // Verticles (v)
            if (geometry.HasVertices && geometry.NumVertices >= 3)
            {
                Matrix transformationMatrix = ComputeWorldMatrix(shape);
                computePolys(geometry.Triangles, geometry.Vertices, transformationMatrix);
            }
        }

        private void ParseStrips(NiTriStrips strips)
        {
            List<string> export = new List<string>();

            NiTriStripsData geometry = (NiTriStripsData)strips.Data.Object;

            List<Triangle> triangles = new List<Triangle>();
            foreach (ushort[] points in geometry.Points)
            {
                bool t = false;
                int j = 1;

                ushort p1 = points[0];
                ushort p2 = points[1];

                while (j < points.Length - 1)
                {
                    ushort p3 = points[j + 1];

                    if (p1 != p2 && p1 != p3 && p2 != p3)
                    {
                        if (t)
                        {
                            triangles.Add(new Triangle(p1, p3, p2));
                        }
                        else
                        {
                            triangles.Add(new Triangle(p1, p2, p3));
                        }
                    }

                    j = j + 1;
                    p1 = p2;
                    p2 = p3;
                    t = !t;
                }
            }

            // Verticles (v)
            if (geometry.HasVertices && geometry.NumVertices >= 3)
            {
                Matrix transformationMatrix = ComputeWorldMatrix(strips);
                computePolys(triangles.ToArray(), geometry.Vertices, transformationMatrix);
            }
        }

        private void computePolys(Triangle[] trianlges, Vector3[] vertices, Matrix transformation)
        {
            // Transaform all vertices
            List<Vector3> verticesTransformed = new List<Vector3>();
            foreach (Vector3 vector in vertices) verticesTransformed.Add(Vector3.TransformCoordinate(vector, transformation));

            foreach (Triangle triangle in trianlges)
            {
                Polygon poly = new Polygon(
                    new Vector3(verticesTransformed[triangle.X].X, verticesTransformed[triangle.X].Y, verticesTransformed[triangle.X].Z),
                    new Vector3(verticesTransformed[triangle.Y].X, verticesTransformed[triangle.Y].Y, verticesTransformed[triangle.Y].Z),
                    new Vector3(verticesTransformed[triangle.Z].X, verticesTransformed[triangle.Z].Y, verticesTransformed[triangle.Z].Z)
                );
                m_polys.Add(poly);
            }
        }

        /// <summary>
        /// Writes a plain version of the poly
        /// </summary>
        /// <param name="targetFile"></param>
        public void WritePlain(string targetFile)
        {
            using (StreamWriter writer = new StreamWriter(targetFile))
            {
                foreach (Polygon poly in m_polys)
                {
                    writer.WriteLine(string.Format("{0} {1} {2}", poly.P1.X, poly.P1.Y, poly.P1.Z));
                    writer.WriteLine(string.Format("{0} {1} {2}", poly.P2.X, poly.P2.Y, poly.P2.Z));
                    writer.WriteLine(string.Format("{0} {1} {2}", poly.P3.X, poly.P3.Y, poly.P3.Z));
                    writer.WriteLine();
                }
            }
        }

        public override void Write(string targetFile)
        {
            using (FileStream fs = new FileStream(targetFile, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    foreach (Polygon poly in m_polys)
                    {
                        writer.Write(poly.P1.X);
                        writer.Write(poly.P1.Y);
                        writer.Write(poly.P1.Z);

                        writer.Write(poly.P2.X);
                        writer.Write(poly.P2.Y);
                        writer.Write(poly.P2.Z);

                        writer.Write(poly.P3.X);
                        writer.Write(poly.P3.Y);
                        writer.Write(poly.P3.Z);
                    }
                }
            }
        }

    }
}
