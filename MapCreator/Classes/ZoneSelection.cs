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
    public struct ZoneSelection
    {
        private string m_id;
        private string m_name;
        private string m_expansion;
        private string m_type;

        public string Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Expansion
        {
            get { return m_expansion; }
            set { m_expansion = value; }
        }

        public string Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public ZoneSelection(string id, string name, string expansion, string type)
        {
            m_id = id;
            m_name = name;
            m_expansion = expansion;
            m_type = type;
        }

        public override string ToString()
        {
            return Name + " (" + Id + ")";
        }

    }
}
