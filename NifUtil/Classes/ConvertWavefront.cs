//
// MapCreator NifUtil Library
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
using Niflib;
using System.IO;
using SharpDX;
using ImageMagick;

namespace NifUtil
{
    class ConvertWavefront : Convert
    {
        private int m_triangleCounter = 1;
        private List<string> mtlExport = new List<string>();
        private List<string> textures = new List<string>();

        public ConvertWavefront(NiFile file)
            : base(file)
        {
            Export.Add("# Build with NifParser by Merec");
            Export.Add("# special thanks to Schaf");
            Export.Add("");

            WalkNodes(file.FindRoot());
        }

        private void WalkNodes(NiAVObject node)
        {
            // Ignore some node names
            if (!IsNodeDrawable(node)) return;

            // Render Children
            if (node is NiTriShape)
            {
                Export.Add("");
                Export.Add(ParseShape((NiTriShape)node));
                Export.Add("");
            }
            else if (node is NiTriStrips)
            {
                Export.Add("");
                Export.Add(ParseStrips((NiTriStrips)node));
                Export.Add("");
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

        private string ParseStrips(NiTriStrips strips)
        {
            if (!strips.Data.IsValid()) return "";

            NiTriStripsData geometry = (NiTriStripsData)strips.Data.Object;

            Matrix transformationMatrix = ComputeWorldMatrix(strips);

            // The final text
            List<string> export = new List<string>();

            // Set Object name
            export.Add("g Strip " + strips.Name + Environment.NewLine);

            // Verticles (v)
            if (geometry.HasVertices && geometry.NumVertices >= 3)
            {
                export.Add(printVertices(geometry.Vertices, transformationMatrix));
            }

            // Texture coordinates (vt)
            if (geometry.UVSets.Length > 0)
            {
                export.Add(printUvSets(geometry.UVSets));
            }

            // Normals (vn)
            if (geometry.HasNormals)
            {
                export.Add(printNormals(geometry.Normals, transformationMatrix));
            }

            if (geometry.Points.Length > 0)
            {
                List<Triangle> triangles = new List<Triangle>();

                foreach (ushort[] points in geometry.Points)
                {
                    bool t = false;
                    int j = 1;

                    ushort p1 = points[0];
                    ushort p2 = points[1];

                    while (j < points.Length - 1)
                    {
                        ushort p3 = points[j+1];

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

                export.Add(printTriangles(triangles.ToArray(), (geometry.UVSets.Length > 0)));
            }

            return string.Join(Environment.NewLine, export);
        }

        private string ParseShape(NiTriShape shape)
        {
            if (!shape.Data.IsValid()) return "";

            NiTriShapeData geometry = (NiTriShapeData)shape.Data.Object;

            // The final text
            List<string> export = new List<string>();

            Matrix transformationMatrix = ComputeWorldMatrix(shape);

            // Set Object name
            export.Add("g Shape " + shape.Name + Environment.NewLine);

            NiMaterialProperty material = null;
            NiTexturingProperty texture = null;
            foreach (NiRef<NiProperty> property in shape.Properties)
            {
                if (property.Object is NiMaterialProperty)
                {
                    material = property.Object as NiMaterialProperty;
                }
                if (property.Object is NiTexturingProperty)
                {
                    texture = property.Object as NiTexturingProperty;
                }
            }

            if (material != null && texture != null)
            {
                export.Add(printMaterial(material, texture));
            }

            // Verticles (v)
            if (geometry.HasVertices && geometry.NumVertices >= 3)
            {
                export.Add(printVertices(geometry.Vertices, transformationMatrix));
            }

            // Texture coordinates (vt)
            if (geometry.UVSets.Length > 0)
            {
                export.Add(printUvSets(geometry.UVSets));
            }

            // Normals (vn)
            if (geometry.HasNormals)
            {
                export.Add(printNormals(geometry.Normals, transformationMatrix));
            }

            // Parameter space vertices (vp)

            // Face Definitions (f)
            export.Add(printTriangles(geometry.Triangles, (geometry.UVSets.Length > 0)));

            return string.Join(Environment.NewLine, export);
        }

        private string printMaterial(NiMaterialProperty material, NiTexturingProperty texture)
        {
            if (mtlExport.Count > 0) mtlExport.Add(Environment.NewLine);

            string name = material.Name.ToString();

            mtlExport.Add("newmtl " + name);

            // Ambient Color
            mtlExport.Add(string.Format("Ka {0} {1} {2}", material.AmbientColor.Red, material.AmbientColor.Green, material.AmbientColor.Blue));
            // Diffuse Color
            mtlExport.Add(string.Format("Kd {0} {1} {2}", material.DiffuseColor.Red, material.DiffuseColor.Green, material.DiffuseColor.Blue));
            // Specular Color
            mtlExport.Add(string.Format("Ks {0} {1} {2}", material.SpecularColor.Red, material.SpecularColor.Green, material.SpecularColor.Blue));
            // Transparency
            mtlExport.Add(string.Format("d {0}", material.Alpha));
            //mtlExport.Add(string.Format("Tr {0}", material.Alpha));

            printTexture(texture);

            string export = "# Material" + Environment.NewLine;
            export += "usemtl " + name + Environment.NewLine;
            return export;
        }

        private void printTexture(NiTexturingProperty texture)
        {
            NiSourceTexture source = texture.File.ObjectsByRef.Where(o => o.Key == texture.BaseTexture.Source.RefId).First().Value as NiSourceTexture;

            string fileName = source.FileName.ToString().ToLower();

            string offset = string.Format("-o {0} {1}", texture.BaseTexture.CenterOffset.X, texture.BaseTexture.CenterOffset.Y);

            mtlExport.Add("# original file " + fileName);
            mtlExport.Add(string.Format("map_Ka {0}.tga", Path.GetFileNameWithoutExtension(fileName), offset));
            mtlExport.Add(string.Format("map_Kd {0}.tga", Path.GetFileNameWithoutExtension(fileName), offset));
            //mtlExport.Add(string.Format("map_Ks {0}.tga", Path.GetFileNameWithoutExtension(fileName), offset));
            textures.Add(fileName);

            if (texture.BumpMapTexture != null)
            {
                NiSourceTexture bumbTexture = texture.File.ObjectsByRef.Where(o => o.Key == texture.BumpMapTexture.Source.RefId).First().Value as NiSourceTexture;
                mtlExport.Add(string.Format("map_bump {0}.tga", Path.GetFileNameWithoutExtension(bumbTexture.FileName.ToString().ToLower())));
                textures.Add(bumbTexture.FileName.ToString());
            }


        }

        private string printVertices(Vector3[] vertices, Matrix transformation)
        {
            string export = "";
            foreach (Vector3 verticle in vertices)
            {
                Vector3 vectorTransformed = Vector3.TransformCoordinate(verticle, transformation);
                export += string.Format("v {0} {1} {2}", vectorTransformed.X, vectorTransformed.Y, vectorTransformed.Z) + Environment.NewLine;
            }
            return export;
        }

        private string printUvSets(Vector2[][] uvsets)
        {
            string export = "";

            /*
            foreach (Vector2[] uvset in uvsets.Reverse())
            {
                foreach (Vector2 uv in uvset)
                {
                    export += string.Format("vt {0} {1}", uv.X, 1f - uv.Y) + Environment.NewLine;
                }
            }
             * */

            // Test: Draw only the first set without reverse
            foreach (Vector2[] uvset in uvsets)
            {
                foreach (Vector2 uv in uvset)
                {
                    export += string.Format("vt {0} {1}", uv.X, 1f - uv.Y) + Environment.NewLine;
                }

                break;
            }

            return export;
        }

        private string printNormals(Vector3[]normals, Matrix transformation)
        {
            string export = "";
            foreach (Vector3 normal in normals)
            {
                Vector3 vectorTransformed = Vector3.TransformNormal(normal, transformation);
                export += string.Format("vn {0} {1} {2}", vectorTransformed.X, vectorTransformed.Y, vectorTransformed.Z) + Environment.NewLine;
            }
            return export;
        }

        private string printTriangles(Triangle[] triangles, bool hasUvSets)
        {
            string export = "";

            string format = "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
            if (!hasUvSets)
            {
                format = "f {0}//{0} {1}//{1} {2}//{2}";
            }

            int max = m_triangleCounter;
            foreach (Triangle face in triangles)
            {
                export += string.Format(format, face.X + m_triangleCounter, face.Y + m_triangleCounter, face.Z + m_triangleCounter) + Environment.NewLine;
                if (face.X + m_triangleCounter > max) max = face.X + m_triangleCounter;
                if (face.Y + m_triangleCounter > max) max = face.Y + m_triangleCounter;
                if (face.Z + m_triangleCounter > max) max = face.Z + m_triangleCounter;
            }
            m_triangleCounter = max + 1;

            /*
            foreach (Triangle face in triangles)
            {
                export += string.Format(format, face.X, face.Y, face.Z) + Environment.NewLine;
            }
             * */

            return export;
        }

        public override void Write(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);

            if (mtlExport.Count > 0)
            {
                string mtlFileName = Path.GetFileNameWithoutExtension(filename) + ".mtl";
                Export.Insert(3, "# Textures " + Environment.NewLine + "mtllib " + mtlFileName);

                using (StreamWriter writer = new StreamWriter(fileInfo.Directory + "\\" + mtlFileName))
                {
                    writer.WriteLine(string.Join(Environment.NewLine, mtlExport));
                }

                using (StreamWriter writer = new StreamWriter(fileInfo.Directory + "\\" + mtlFileName))
                {
                    writer.WriteLine(string.Join(Environment.NewLine, mtlExport));
                }
            }

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(string.Join(Environment.NewLine, Export));
            }

            // Load textues
            if (textures.Count > 0)
            {
                List<string> textureLocations = new List<string>()
                {
                    "D:\\DAoC Extracted\\figures\\Mskins",
                    "D:\\DAoC Extracted\\figures\\skins",
                    "D:\\DAoC Extracted\\items\\pskins",
                    "D:\\Games\\Dark Age of Camelot\\zones\\Nifs",
                    "D:\\Games\\Dark Age of Camelot\\zones\\Dnifs",
                    "D:\\Games\\Dark Age of Camelot\\zones\\sky",
                    "D:\\Games\\Dark Age of Camelot\\zones\\TerrainTex",
                    "D:\\Games\\Dark Age of Camelot\\zones\\textures",
                    "D:\\Games\\Dark Age of Camelot\\zones\\trees",
                    "D:\\Games\\Dark Age of Camelot\\frontiers\\dnifs",
                    "D:\\Games\\Dark Age of Camelot\\frontiers\\items",
                    "D:\\Games\\Dark Age of Camelot\\frontiers\\NIFS",
                    "D:\\Games\\Dark Age of Camelot\\frontiers\\zones\\TerrainTex",
                    "D:\\Games\\Dark Age of Camelot\\frontiers\\zones\\textures",
                    "D:\\Games\\Dark Age of Camelot\\phousing\\nifs",
                    "D:\\Games\\Dark Age of Camelot\\phousing\\textures",
                    "D:\\Games\\Dark Age of Camelot\\pregame",
                    "D:\\Games\\Dark Age of Camelot\\Tutorial\\zones\\nifs",
                    "D:\\Games\\Dark Age of Camelot\\Tutorial\\zones\\terraintex",
                    "D:\\Games\\Dark Age of Camelot\\insignia",
                    "D:\\Games\\Dark Age of Camelot\\items",
                    "D:\\Games\\Dark Age of Camelot\\zones\\zone026\\nifs",
                    "D:\\Games\\Dark Age of Camelot\\zones\\zone050\\nifs",
                    "D:\\Games\\Dark Age of Camelot\\zones\\zone120\\nifs",
                    "D:\\Games\\Dark Age of Camelot\\zones\\zone209\\nifs",

                };


                foreach (string texture in textures)
                {
                    foreach (string loc in textureLocations)
                    {
                        DirectoryInfo dir = new DirectoryInfo(loc);
                        var files = dir.GetFiles(Path.GetFileNameWithoutExtension(texture) + ".*");

                        if (files.Length > 0)
                        {
                            string sourceFileName = files.First().FullName;
                            string targetFileName = fileInfo.Directory + "\\" + Path.GetFileNameWithoutExtension(sourceFileName) + ".tga";
                            string targetFileNamePng = fileInfo.Directory + "\\" + Path.GetFileNameWithoutExtension(sourceFileName) + ".png";

                            using (MagickImage image = new MagickImage(files.First().FullName))
                            {
                                image.Write(targetFileName.ToLower());
                                image.Write(targetFileNamePng.ToLower());
                            }
                            break;
                        }
                    }
                }

            }

        }

    }
}
