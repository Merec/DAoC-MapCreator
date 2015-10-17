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

using SharpDX;

namespace NifUtil
{
    public struct Polygon
    {
        public Vector3 P1
        {
            get { return m_vectors[0]; }
        }

        public Vector3 P2
        {
            get { return m_vectors[1]; }
        }

        public Vector3 P3
        {
            get { return m_vectors[2]; }
        }

        private Vector3[] m_vectors;

        public Vector3[] Vectors
        {
            get { return m_vectors; }
            set { m_vectors = value; }
        }

        public Polygon(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            m_vectors = new Vector3[3];
            m_vectors.SetValue(p1, 0);
            m_vectors.SetValue(p2, 1);
            m_vectors.SetValue(p3, 2);
        }
    }
}
