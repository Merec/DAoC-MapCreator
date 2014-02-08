using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapCreator
{
    class TreeRow
    {
        private string m_name;
        private int m_zOffset;

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

        #endregion

        public TreeRow()
        {
        }
    }
}
