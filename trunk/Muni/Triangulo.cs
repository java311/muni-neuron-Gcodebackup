using System;
using Tao.OpenGl;
using System.Collections.Generic;

namespace Muni
{
    public struct Triangulo
	{
        public Punto[] p;
        public List<Triangulo> neighbors;
                                    
        public Triangulo(Punto p1, Punto p2, Punto p3)
        {
            p = new Punto[3];
            p[0] = p1;
            p[1] = p2;
            p[2] = p3;
            neighbors = new List<Triangulo>();
        }

        public void Draw_Points()
        {
            Gl.glPushMatrix();
                Gl.glColor3f(1.0f, 0.0f, 0.0f);

                Gl.glPointSize(1.0f);
                Gl.glBegin(Gl.GL_POINTS);
                    Gl.glVertex3f(p[0].x, p[0].y, p[0].z);
                    Gl.glVertex3f(p[1].x, p[1].y, p[1].z);
                    Gl.glVertex3f(p[2].x, p[2].y, p[2].z);
                Gl.glEnd();
            Gl.glPopMatrix();
        }
	}
}