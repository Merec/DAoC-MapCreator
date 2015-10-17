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

        private double m_angle3D = 0;
        private double m_axisX3D = 0;
        private double m_axisY3D = 0;
        private double m_axisZ3D = 0;

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

        public double Angle3D
        {
            get { return m_angle3D; }
            set { m_angle3D = value; }
        }

        public double AxisX3D
        {
            get { return m_axisX3D; }
            set { m_axisX3D = value; }
        }


        public double AxisY3D
        {
            get { return m_axisY3D; }
            set { m_axisY3D = value; }
        }

        public double AxisZ3D
        {
            get { return m_axisZ3D; }
            set { m_axisZ3D = value; }
        }

        #endregion

        public FixtureRow()
        {
        }

        public override string ToString()
        {
            return "id: " + m_id.ToString() + "nifId: " + m_nifId.ToString() + "; " + m_textualName;
        }
    }
}
