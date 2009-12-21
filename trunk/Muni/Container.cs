using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace Muni
{
    [Serializable]
    public class Container
    {
        /// <summary>
        /// Variables Miembro
        /// </summary>
        #region Variables Miembro
        float dx, dy, dz;
        float px, py, pz;
        #endregion
        
        public Container (float dx, float dy, float dz, float px, float py, float pz)
        {
            this.dx = dx/2;
            this.dy = dy/2;
            this.dz = dz/2;
            this.px = px;
            this.py = py;
            this.pz = pz;
        }

        public void Draw(OpenGlColor color)
        {
            Gl.glTranslatef(px, py, pz);

                Gl.glColor3f(color.R,color.G,color.B);
                            
                Gl.glBegin(Gl.GL_LINE_LOOP);                                     
                    Gl.glVertex3f(dx, dy, -dz);	    // Top Right Of The Quad (Top)
                    Gl.glVertex3f(-dx, dy, -dz);	// Top Left Of The Quad (Top)
                    Gl.glVertex3f(-dx, dy, dz);	    // Bottom Left Of The Quad (Top)
                    Gl.glVertex3f(dx, dy, dz);	    // Bottom Right Of The Quad (Top)
                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINE_LOOP);
                    Gl.glVertex3f(dx, dy, -dz);	   // Top Right Of The Quad (Right)
                    Gl.glVertex3f(dx, dy, dz);	   // Top Left Of The Quad (Right)
                    Gl.glVertex3f(dx, -dy, dz);	   // Bottom Left Of The Quad (Right)
                    Gl.glVertex3f(dx, -dy, -dz);   // Bottom Right Of The Quad (Right)
                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINE_LOOP);
                    Gl.glVertex3f(dx, -dy, -dz);	// Top Right Of The Quad (Back)
                    Gl.glVertex3f(-dx, -dy, -dz);	// Top Left Of The Quad (Back)
                    Gl.glVertex3f(-dx, dy, -dz);	// Bottom Left Of The Quad (Back)
                    Gl.glVertex3f(dx, dy, -dz);	    // Bottom Right Of The Quad (Back)
                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINE_LOOP);                                     
                    Gl.glVertex3f(dx, dy, dz);	    // Top Right Of The Quad (Front)
                    Gl.glVertex3f(-dx, dy, dz);	    // Top Left Of The Quad (Front)
                    Gl.glVertex3f(-dx, -dy, dz);	// Bottom Left Of The Quad (Front)
                    Gl.glVertex3f(dx, -dy, dz);	    // Bottom Right Of The Quad (Front)
                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINE_LOOP);
                    Gl.glVertex3f(dx, -dy, dz);	    // Top Right Of The Quad (Bottom)
                    Gl.glVertex3f(-dx, -dy, dz);	// Top Left Of The Quad (Bottom)
                    Gl.glVertex3f(-dx, -dy, -dz);	// Bottom Left Of The Quad (Bottom)
                    Gl.glVertex3f(dx, -dy, -dz);	// Bottom Right Of The Quad (Bottom)
                Gl.glEnd();

                Gl.glBegin(Gl.GL_LINE_LOOP);                                     
                    Gl.glVertex3f(-dx, dy, dz);	    // Top Right Of The Quad (Left)
                    Gl.glVertex3f(-dx, dy, -dz);	// Top Left Of The Quad (Left)
                    Gl.glVertex3f(-dx, -dy, -dz);	// Bottom Left Of The Quad (Left)
                    Gl.glVertex3f(-dx, -dy, dz);	// Bottom Right Of The Quad (Left)
                Gl.glEnd();

            Gl.glTranslatef(-px, -py, -pz);
        }
    }
}
