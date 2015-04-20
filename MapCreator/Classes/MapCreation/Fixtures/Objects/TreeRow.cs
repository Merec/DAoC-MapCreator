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
