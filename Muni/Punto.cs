using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace Muni
{
    public class Punto
    {
        public float x;
        public float y;
        public float z;
        public List<Punto> vecinos;
        
        public Punto()
        {
            x = -1; y = -1; z = -1;
            vecinos = new List<Punto>();
        }
        
        public Punto(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            vecinos = new List<Punto>();
        }

        /*public static bool operator==(Punto a, Punto b)
        {
            /*double ax = Math.Round(a.x, 1);
            double ay = Math.Round(a.y, 1);
            double az = Math.Round(a.z, 1);
            double bx = Math.Round(b.x, 1);
            double by = Math.Round(b.y, 1);
            double bz = Math.Round(b.z, 1);

            if ((a.x == b.x) && (a.y == b.y) && (a.z == b.z))
                return true;
            else
                return false;
        }

        public static bool operator!=(Punto a, Punto b)
        {
            /*double ax = Math.Round(a.x, 1);
            double ay = Math.Round(a.y, 1);
            double az = Math.Round(a.z, 1);
            double bx = Math.Round(b.x, 1);
            double by = Math.Round(b.y, 1);
            double bz = Math.Round(b.z, 1);

            if ((a.x != b.x) || (a.y != b.y) || (a.z != b.z))
                return true;
            else
                return false;
        }*/

        private static bool AlmostEqual(float nVal1, float nVal2, float nEpsilon)
        {
            bool bRet = (((nVal2 - nEpsilon) < nVal1) && (nVal1 < (nVal2 + nEpsilon)));
            return bRet;
        }


        
    }
}
