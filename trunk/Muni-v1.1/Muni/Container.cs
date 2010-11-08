using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;

namespace Muni
{
    /// <summary>
    /// This class is a little data structure to store the container box coordenates
    /// </summary>
    public class Container
    {
        float dx, dy, dz;  //The distances from the center of the box to the edge
        float px, py, pz;  //This are the postion coordenates of the container box.
        
        /// <summary>
        /// This is the default Constructor for the Container class
        /// </summary>
        /// <param name="dx">The distance from the center of the box to the X edge</param>
        /// <param name="dy">The distance from the center of the box to the Y edge</param>
        /// <param name="dz">The distance from the center of the box to the Z edge</param>
        /// <param name="px">The X position coordenate</param>
        /// <param name="py">The Y position coordenate</param>
        /// <param name="pz">The Z position coordenate</param>
        public Container (float dx, float dy, float dz, float px, float py, float pz)
        {
            this.dx = dx/2;
            this.dy = dy/2;
            this.dz = dz/2;
            this.px = px;
            this.py = py;
            this.pz = pz;
        }

        /// <summary>
        /// This function builds, translate and render the container box into the main scene
        /// </summary>
        /// <param name="color">This is just a OpenGlColor to setup the color of the box</param>
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
