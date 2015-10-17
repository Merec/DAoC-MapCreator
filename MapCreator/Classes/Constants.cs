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

namespace MapCreator
{

    public enum GameExpansion
    {
        Unknown = 0,

        Tutorial,
        Classic,
        ShroudedIsles,
        TrialsOfAtlantis,
        Catacombs,
        DarknessRising,
        LabyrithOfTheMinotaur,

        Foundations,
        NewFrontiers,
        OldFrontiers
    }

    public enum RiverTypes
    {

        River = 0,
        Lake = 1,
        Swamp = 2,

        Lava = 10
    }

    public class Point3D
    {
        private int m_x;

        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        private int m_y;

        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        private int m_z;

        public int Z
        {
            get { return m_z; }
            set { m_z = value; }
        }
    }

}
