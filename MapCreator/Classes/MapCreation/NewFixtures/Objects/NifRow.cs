using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NifUtil;

namespace MapCreator
{
    class NifRow
    {
        private int m_nifId;
        private string m_textualName;
        private string m_filename;
        private int m_color;

        private Polygon[] m_polygons;

        #region Getter/setter

        public int Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        public string Filename
        {
            get { return m_filename; }
            set { m_filename = value; }
        }

        public string TextualName
        {
            get { return m_textualName; }
            set { m_textualName = value; }
        }

        public int NifId
        {
            get { return m_nifId; }
            set { m_nifId = value; }
        }

        public Polygon[] Polygons
        {
            get { return m_polygons; }
            set { m_polygons = value; }
        }

        #endregion

        public NifRow()
        {
        }
    }
}
