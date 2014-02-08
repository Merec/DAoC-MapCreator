using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapCreator
{
    class FixtureRow
    {
        private int m_id;
        private int m_nifId; 
        private string m_textualName;
        private double m_x;
        private double m_y;
        private double m_z;
        private double m_a;
        private double m_scale; 
        private bool m_onGround;
        private bool m_flip;

        #region Getter/Setter

        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public int NifId
        {
            get { return m_nifId; }
            set { m_nifId = value; }
        }
       

        public string TextualName
        {
            get { return m_textualName; }
            set { m_textualName = value; }
        }
        

        public double X
        {
            get { return m_x; }
            set { m_x = value; }
        }
        

        public double Y
        {
            get { return m_y; }
            set { m_y = value; }
        }
        

        public double Z
        {
            get { return m_z; }
            set { m_z = value; }
        }
        

        public double A
        {
            get { return m_a; }
            set { m_a = value; }
        }
        

        public double Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }
       

        public bool OnGround
        {
            get { return m_onGround; }
            set { m_onGround = value; }
        }
        

        public bool Flip
        {
            get { return m_flip; }
            set { m_flip = value; }
        }

        #endregion

        public FixtureRow()
        {
        }
    }
}
