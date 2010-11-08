using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace Muni
{
    /// <summary>
    /// This class implements the Ruler to measure the 3D model length
    /// </summary>
    public class Ruler
    {
        private float[] pos1;
        private float[] pos2;
        private OpenGlColor cs1, cs2;
        private Glu.GLUquadric sphere1, sphere2;
        private float micronx, microny, micronz, micron;
        private bool calculate;
        private int sel;
        
        /// <summary>
        /// This is the Ruler constructor
        /// </summary>
        public Ruler()
        {
            pos1 = new float[] { 0.0f, 0.0f, 0.0f };
            pos2 = new float[] { 25.0f, 0.0f, 0.0f };
            cs1 = new OpenGlColor();
            cs2 = new OpenGlColor();
            sphere1 = Glu.gluNewQuadric();
            sphere2 = Glu.gluNewQuadric();
            micron = 0.0f; 
            micronx = 0.0f;
            microny = 0.0f;
            micronz = 0.0f;
            calculate = false;
        }

        /// <summary>
        /// This function moves to the left the selected ruler pointer. (X Axis)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Left(int s)
        {
            if (s == 1)
                pos1[0] = pos1[0] - 1;
            else if (s ==2)
                pos2[0] = pos2[0] - 1;

            return Distance();
        }

        /// <summary>
        /// This function moves to the Right the selected ruler edge (X Axis)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Right(int s)
        {
            if (s == 1)
                pos1[0] = pos1[0] + 1;
            else if (s == 2)
                pos2[0] = pos2[0] + 1;

            return Distance();
        }

        /// <summary>
        /// This function moves ahead the selected ruler edge. (Z Axis)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Ahead(int s)
        {
            if (s == 1)
                pos1[2] = pos1[2] + 1;
            else if (s == 2)
                pos2[2] = pos2[2] + 1;

            return Distance();
        }

        /// <summary>
        /// This function moves back the selected ruler edge (Z Axis)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Back(int s)
        {
            if (s == 1)
                pos1[2] = pos1[2] - 1;
            else if (s == 2)
                pos2[2] = pos2[2] - 1;

            return Distance();
        }

        /// <summary>
        /// This object moves up the selected ruler edge (Y Axis)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Up(int s)
        {
            if (s == 1)
                pos1[1] = pos1[1] + 1;
            else if (s == 2)
                pos2[1] = pos2[1] + 1;

            return Distance();
        }

        /// <summary>
        /// This function moves down the selected ruler edge (Y Axis)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Down(int s)
        {
            if (s == 1)
                pos1[1] = pos1[1] - 1;
            else if (s == 2)
                pos2[1] = pos2[1] - 1;

            return Distance();
        }

        /// <summary>
        /// This function resets the selected Ruler's edge to the center of the model
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public float Reset(int s)
        {
            if (s == 1)
            {
                pos1[0] = 0.0f; pos1[1] = 0.0f; pos1[2] = 0.0f;
            }
            else if (s == 2)
            {
                pos2[0] = 25.0f; pos2[1] = 0.0f; pos2[2] = 0.0f;
            }
            return Distance();
        }

        /// <summary>
        /// This function updates the sel variable. This variable stores current selected edge of the Ruler.
        /// </summary>
        /// <param name="sel"></param>
        public void selected(int sel)
        {
            this.sel = sel;
        }

        /// <summary>
        /// This function sets the micron resolution parameters.
        /// </summary>
        /// <param name="micronx">The x micron image package resolution</param>
        /// <param name="microny">The y micron image packege resolution</param>
        /// <param name="micronz">The z micron image package resolution</param>
        /// <param name="micron">The final micron image package resolution</param>
        public void set_calibration(float micronx, float microny, float micronz, float micron)
        {
            this.micronx = micronx;
            this.microny = microny;
            this.micronz = micronz;
            this.micron = micron;
            calculate = true;
        }

        /// <summary>
        /// This function calculates the distance between the ruler's edges. This function return the meause
        /// in microns. WARNING: Its necessary to check the results for this function
        /// </summary>
        /// <returns></returns>
        public float Distance()
        {
            if (calculate == false)
                return -1.0f;

            float dpx, dpy, dpz;
            float dmx, dmy, dmz;
            float distancia = 0;
            
            dpx = pos2[0] - pos1[0];
            dpy = pos2[1] - pos1[1];
            dpz = pos2[2] - pos1[2];

            dmx = ((dpx * micron) / micronx);
            dmy = ((dpy * micron) / microny);
            dmz = ((dpz * micron) / micronz);

            distancia = (float)(Math.Sqrt((Math.Pow(dmx, 2.0f)) + (Math.Pow(dmy, 2.0f)) + (Math.Pow(dmz, 2.0f))));

            return distancia;
        }

        /// <summary>
        /// This is the render function for the Ruler object. This function also refreshes the color of the
        /// selected ruler's edges. This function also draws a line between the two ruler's edges.
        /// </summary>
        /// <param name="color"></param>
        public void Draw(OpenGlColor color)
        {
            if (sel == 1)
            { 
                cs1.R = (byte)(255 - color.R); cs1.G = (byte)(255 - color.G); cs1.B = (byte)(255 - color.B);
                cs2.R = color.R; cs2.G = color.G; cs2.B = color.B;
            }
            else if (sel == 2)
            {  
                cs2.R = (byte)(255 - color.R); cs2.G = (byte)(255 - color.G); cs2.B = (byte)(255 - color.B);
                cs1.R = color.R; cs1.G = color.G; cs1.B = color.B;
            }

            Gl.glColor3ub(cs1.R, cs1.G, cs1.B);
            Gl.glTranslatef(pos1[0], pos1[1], pos1[2]);
            Gl.glPushName(1);
                Glu.gluQuadricDrawStyle(sphere1, Glu.GLU_FILL);
                Glu.gluSphere(sphere1, 2.0, 23, 23);
            Gl.glPopName();
            Gl.glTranslatef(-pos1[0], -pos1[1], -pos1[2]);

            Gl.glColor3ub(cs2.R, cs2.G, cs2.B);
            Gl.glTranslatef(pos2[0], pos2[1], pos2[2]);
            Gl.glPushName(2);
                Glu.gluQuadricDrawStyle(sphere2, Glu.GLU_FILL);
                Glu.gluSphere(sphere2, 2.0, 23, 23);
            Gl.glPopName();
            Gl.glTranslatef(-pos2[0], -pos2[1], -pos2[2]);

            Gl.glColor3ub((byte)(255-color.R), (byte)(255-color.G), (byte)(255-color.B));
            Gl.glLineWidth(5.0f);
            Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex3fv(pos1);
                Gl.glVertex3fv(pos2);
            Gl.glEnd();
            Gl.glLineWidth(1.0f);
        }

    }
}
