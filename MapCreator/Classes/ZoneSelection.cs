using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
