using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MapCreator
{
    static class Tools
    {

        /// <summary>
        /// Normalize a 3x1 vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double[] NormalizeVector(double[] vector)
        {
            double modulus = Math.Sqrt(vector[0] * vector[0] + vector[1] * vector[1] + vector[2] * vector[2]);
            if (modulus == 0) modulus = 1e-50;

            return new double[] { vector[0] / modulus, vector[1] / modulus, vector[2] / modulus };
        }

        /// <summary>
        /// Parses a color. Ban be [r,g,b], [r,g,b,a] or int32 (engine color)
        /// </summary>
        /// <returns></returns>
        public static Color ParseColor(string color)
        {
            int[] colorParts;
            if (color.Contains(','))
            {
                colorParts = (int[])color.Split(',').Select(int.Parse);
            }
            else
            {
                colorParts = new int[] { Convert.ToInt32(color) };
            }

            if (colorParts.Length == 1)
            {
                return Color.FromArgb(255, (int)(colorParts[0] % 256), (int)(colorParts[0] / 256) % 256, (int)(colorParts[0] / 256 / 65536));
            }
            else if (colorParts.Length == 3)
            {
                return Color.FromArgb(255, colorParts[0], colorParts[1], colorParts[2]);
            }
            else if (colorParts.Length == 4)
            {
                return Color.FromArgb(colorParts[3], colorParts[0], colorParts[1], colorParts[2]);
            }
            else
            {
                throw new Exception("Bad color given: " + color);
            }

        }

    }
}
