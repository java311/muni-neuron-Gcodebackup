using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace Muni
{
    /// <summary>
    /// This class is a little structure to save dots coordenates
    /// </summary>
    public class Punto
    {
        public float x;    //The X coordenate of the dot
        public float y;    //The Y coordenate of the dot
        public float z;    //The Z coordenate of the dot
        public List<Punto> vecinos;   //A List of dot objects to store the dot's neighbors
        
        /// <summary>
        /// This initilizes all the dot coordinates to -1 and the neighbor list to empty.
        /// </summary>
        public Punto()
        {
            x = -1; y = -1; z = -1;
            vecinos = new List<Punto>();
        }
        
        /// <summary>
        /// This constructor intilizes the x,y,z coordenates of the dot. And the neighbor list to empty.
        /// </summary>
        /// <param name="x">X coordenate</param>
        /// <param name="y">Y coordenate</param>
        /// <param name="z">Z coordenate</param>
        public Punto(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            vecinos = new List<Punto>();
        }

        #region Deprecated Punto Functions
        //These are deprecated functions, these functions overload the == and != operators. 
        //These determine if two Punto objects are in the same position or not.
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
        
        #endregion
    }
}
