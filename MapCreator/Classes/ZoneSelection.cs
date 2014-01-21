using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapCreator
{
    public class ZoneSelection
    {
        private string m_id;

        public string Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public ZoneSelection(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
