﻿//
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

namespace MapCreator
{
    class TreeRow
    {
        private string m_name;
        private int m_zOffset;
        private string m_leafTexture;
        private System.Drawing.Color m_averageColor;

        #region Getter/Setter

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int ZOffset
        {
            get { return m_zOffset; }
            set { m_zOffset = value; }
        }

        public string LeafTexture
        {
            get { return m_leafTexture; }
            set { m_leafTexture = value; }
        }

        public System.Drawing.Color AverageColor
        {
            get { return m_averageColor; }
            set { m_averageColor = value; }
        }

        #endregion

        public TreeRow()
        {
        }
    }
}
