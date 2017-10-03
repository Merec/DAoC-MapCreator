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

using System;
using System.Collections.Generic;

using System.IO;
using Niflib;
using SharpDX;

namespace NifUtil
{
    public class NifParser : IDisposable
    {
        string m_fileName;

        private StreamReader m_fileReader;

        private NiFile m_nifFile;

        private Polygon[] m_polygons;

        #region Events
        public event IsNodeDrawableEventHandler IsNodeDrawable;
        #endregion

        public NifParser()
        {
            // Language settings
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        }

        /// <summary>
        /// Loads a NIF file
        /// </summary>
        /// <param name="nifFile"></param>
        public void Load(string nifFile)
        {
            if (!System.IO.File.Exists(nifFile))
            {
                throw new FileNotFoundException("NIF File not found!");
            }

            m_fileName = nifFile;
            m_fileReader = new StreamReader(nifFile);
            ReadNifFile();
        }

        /// <summary>
        /// Loads a NIF file
        /// </summary>
        /// <param name="nifFileStream"></param>
        public void Load(StreamReader nifFileStream)
        {
            m_fileReader = nifFileStream;
            ReadNifFile();
        }

        /// <summary>
        /// Reads the added nif File
        /// </summary>
        private void ReadNifFile()
        {
            using (BinaryReader br = new BinaryReader(m_fileReader.BaseStream))
            {
                m_nifFile = new NiFile(br);
            }
        }

        /// <summary>
        /// Converts a nif
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetFilename"></param>
        public void Convert(ConvertType type, string targetFilename)
        {
            switch (type)
            {
                case ConvertType.WaveFrontObject:
                    ConvertWavefront wf = new ConvertWavefront(m_nifFile);
                    if(IsNodeDrawable != null)
                    {
                        wf.IsNodeDrawable += delegate(NiAVObject node)
                        {
                            return IsNodeDrawable(node);
                        };
                    }

                    wf.Start();
                    wf.Write(targetFilename);
                    break;
                case ConvertType.PolyText:
                case ConvertType.Poly:
                    ConvertPoly conv = new ConvertPoly(m_nifFile);
                    if (IsNodeDrawable != null)
                    {
                        conv.IsNodeDrawable += delegate (NiAVObject node)
                        {
                            return IsNodeDrawable(node);
                        };
                    }

                    conv.Start();
                    m_polygons = conv.Polys.ToArray();

                    if (type == ConvertType.PolyText) conv.WritePlain(targetFilename);
                    else conv.Write(targetFilename);

                    break;
            }
        }

        public Polygon[] GetPolys()
        {
            if (m_polygons != null) return m_polygons;

            ConvertPoly conv = new ConvertPoly(m_nifFile);
            return conv.Polys.ToArray();
        }

        public void Dispose()
        {
            if (m_fileReader != null)
            {
                m_fileReader.Dispose();
            }
        }

        public static Polygon[] ReadPoly(string polyFile)
        {
            using (StreamReader reader = new StreamReader(polyFile))
            {
                return ReadPoly(reader);
            }
        }

        public static Polygon[] ReadPoly(StreamReader polyFileReader)
        {
            List<Polygon> polys = new List<Polygon>();

            using (BinaryReader reader = new BinaryReader(polyFileReader.BaseStream))
            {
                List<float> points = new List<float>();

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    points.Add(reader.ReadSingle());

                    if (points.Count == 9)
                    {
                        polys.Add(new Polygon(
                            new Vector3(points[0], points[1], points[2]),
                            new Vector3(points[3], points[4], points[5]),
                            new Vector3(points[6], points[7], points[8])
                        ));

                        points.Clear();
                    }
                }
            }

            return polys.ToArray();
        }

    }

    public enum ConvertType
    {
        Poly,
        PolyText,
        WaveFrontObject,
        Collada
    }

}
