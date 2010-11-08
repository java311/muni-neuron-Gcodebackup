using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Muni
{
    /// <summary>
    /// This class handles all the camera stuff, position, looking and type
    /// </summary>
    class Camera
    {
        const float SPHERICROT = 0.20f;
        const float ROOTSPEED = 0.23f;
        const float MOUSEROOTSPEED = 0.020f;
        const float SPEED = 5.0f;

        public float posx, posy, posz;      // Where is the camera in space
        public float lookx, looky, lookz;   // Where the camera views
        public float rotx, roty, rotz;      // The angle of rotation of the scene in Spheric Camera. (The camera doesn't move only the scene)
        private float dz;     // The change rate of the x,z position.
        private float yaw;   // This is the angle where the camera is looking in the X,Z axis. (FPC)
        private float yawh;   // This is the angle where the camera is looking in the X,Z axis. (FPC)

        /// <summary>
        /// Constructor of the camera, set the initial values of it as a First Person Camera
        /// </summary>
        public Camera()
        {
            this.posx = 0.0f; this.posy = 0.0f; this.posz = 0.0f;
            this.lookx = 0.0f; this.looky = 0.0f; this.lookz = 0.0f;
            this.rotx = 180.0f; this.roty = 0.0f; this.rotz = 0.0f;
            
            yaw = (float)Math.Asin(lookz - posz);
            yawh = (float)Math.Acos(looky - posy);
        }

        /// <summary>
        /// The Constructor of the Camera who sets the initial parameters given by the user.
        /// </summary>
        /// <param name="posx">Where the camera is in X</param>
        /// <param name="posy">Where the camera is in Y</param>
        /// <param name="posz">Where the camera is in Z</param>
        /// <param name="lookx">Where the camera is looking on X</param>
        /// <param name="looky">Where the camera is looking on Y</param>
        /// <param name="lookz">Where the camera is looking on Z</param>
        /// <param name="type">False for First Person View and True for Scene Rotation</param>
        public Camera(float posx, float posy, float posz, float lookx, float looky, float lookz)
        {
            this.posx = posx; this.posy = posy; this.posz = posz;
            this.lookx = lookx; this.looky = looky; this.lookz = lookz;
            this.rotx = 180.0f; this.roty = 0.0f; this.rotz = 0.0f;

            yaw = (float)Math.Asin(lookz - posz);
            yawh = (float)Math.Acos(looky - posy);
        }

        /// <summary>
        ///  This function sets all initial OpenGl parameters of the camera, depending on positon and where is looking
        /// </summary>
        /// <param name="width">The width of the openglsimplecontrol</param>
        /// <param name="height">The height of the openglsimplecontrol</param>
        public void SetView(int width, int height)
        {
            // Set viewport to window dimensions.
            Gl.glViewport(0, 0, width, height);
            // Reset projection matrix stack
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(60.0, width/height, 0.0001, 1000.0);

            // Reset modelview matrix stack
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(posx, posy, posz, lookx, looky, lookz, 0.0d, 1.0d, 0.0d);
        }

        /// <summary>
        /// Adjust the camera look plane and position to the escale and model dimensions.
        /// </summary>
        /// <param name="escx">The scale factor of the scene in X</param>
        /// <param name="escy">The scale factor of the scene in Y</param>
        /// <param name="escz">The scale factor of the scene in Z</param>
        /// <param name="dimx">The dimension of the image package in X</param>
        /// <param name="dimy">The dimension of the image package in Y</param>
        /// <param name="dimz">The dimension of the image package in Z</param>
        public void Adjust_to_Model(float escx, float escy, float escz, int dimx, int dimy, int dimz)
        {
            posx = 0.0f;
            posy = 0.0f;
            lookx = 0.0f;
            looky = 0.0f;

            if (posx <= posy)
            {
                posz = ((float)(((dimx * escx / 2) * Math.Sin(1.04719)) / (Math.Cos(1.04719)))) + (dimz * escz);
                lookz = ((float)(((dimx * escx / 2) * Math.Sin(1.04719)) / (Math.Cos(1.04719)) - 1)) + (dimz * escz);
            }
            else
            {
                posz = ((float)(((dimy * escy / 2) * Math.Sin(1.04719)) / (Math.Cos(1.04719)))) + (dimz * escz);
                lookz = ((float)(((dimy * escy / 2) * Math.Sin(1.04719)) / (Math.Cos(1.04719)) - 1)) + (dimz * escz);
            }

            yaw = (float)Math.Asin(lookz - posz);
            yawh = (float)Math.Acos(looky - posy);
        }

        /// <summary>
        /// This function refresh the ModelView Matrix, and refresh the position and look plane of the camera
        /// </summary>
        public void Refresh_Look()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(posx, posy, posz, lookx, looky, lookz, 0.0d, 1.0d, 0.0d);
        }

        /// <summary>
        /// Turns the camera to the Left just one step.
        /// </summary>
        public void Turn_Left()
        {
            yaw -= ROOTSPEED * 1.0f;            // This is the a rotation angle in Y axis

            lookx = (float)(posx + (Math.Cos(yaw)));
            lookz = (float)(posz + (Math.Sin(yaw)));
            Refresh_Look();
        }

        /// <summary>
        /// Turns the camera to the Right just one step.
        /// </summary>
        public void Turn_Right()
        {
            yaw += ROOTSPEED * 1.0f;

            lookx = (float)(posx + (Math.Cos(yaw)));
            lookz = (float)(posz + (Math.Sin(yaw)));

            Refresh_Look();
        }

        /// <summary>
        /// Moves the position of the camera one step.
        /// </summary>
        public void Go_Ahead()
        {
            dz = SPEED * 1.0f;             // dz counts down and up input of the keyboard

            posx += (float)(dz * (Math.Cos(yaw)));
            posz += (float)(dz * (Math.Sin(yaw)));

            lookx = (float)(posx + (Math.Cos(yaw)));
            lookz = (float)(posz + (Math.Sin(yaw)));

            Refresh_Look();
        }

        /// <summary>
        /// Moves the position of the camera one step.
        /// </summary>
        public void Go_Back()
        {
            dz = SPEED * -1.0f;             // dz counts down and up input of the keyboard

            posx += (float)(dz * (Math.Cos(yaw)));
            posz += (float)(dz * (Math.Sin(yaw)));

            lookx = (float)(posx + (Math.Cos(yaw)));
            lookz = (float)(posz + (Math.Sin(yaw)));

            Refresh_Look();
        }

        /// <summary>
        /// Moves the camera one step Up on the Y axis.
        /// </summary>
        public void Up()
        {
            posy += 2.0f;
            looky += 2.0f;
            yawh = (float)Math.Acos(looky - posy);

            Refresh_Look();
        }

        /// <summary>
        /// Moves the camera one step down on the Y axis.
        /// </summary>
        public void Down()
        {
            posy -= 2.0f;
            looky -= 2.0f;
            yawh = (float)Math.Acos(looky - posy);

            Refresh_Look();
        }

        /// <summary>
        /// Controls all the mouse movements over the simpleopenglcontrol, and moves the just the look plane.
        /// </summary>
        /// <param name="ix">Where the mouse was on X</param>
        /// <param name="iy">Where the mouse was on Y</param>
        /// <param name="fx">Where the mouse is right now on X</param>
        /// <param name="fy">Where the mouse is right now on Y</param>
        public void Mouse_Move(int ix, int iy, int ix2, int iy2, int fx, int fy, MouseButtons mouse, int w, int h, bool first_click)
        {
            //This block rotates the view angles of the camera
            if (MouseButtons.Right == mouse)
            {
                // Looking up
                if (iy < fy)
                {
                    yawh += MOUSEROOTSPEED * 1.0f;
                    looky = (float)(posy + (Math.Cos(yawh)));
                }
                // Looking down
                else if (iy > fy)
                {
                    yawh -= MOUSEROOTSPEED * 1.0f;
                    looky = (float)(posy + (Math.Cos(yawh)));
                }

                // Lookiyng Right
                if (ix < fx)
                {
                    yaw += MOUSEROOTSPEED * 1.0f;
                    lookx = (float)(posx + (Math.Cos(yaw)));
                    lookz = (float)(posz + (Math.Sin(yaw)));
                }
                // Looking Left
                else if (ix > fx)
                {
                    yaw -= MOUSEROOTSPEED * 1.0f;
                    lookx = (float)(posx + (Math.Cos(yaw)));
                    lookz = (float)(posz + (Math.Sin(yaw)));
                }
                
                Refresh_Look();
            }
            //This block changes the rotation angles of the object
            else if (MouseButtons.Left == mouse)
            {
                if (fx < 0)
                    roty = 0.0f;
                else if ((fx - ix2) > w)
                    roty = 360.0f;
                else
                {
                    roty = roty + ((360.0f * ((float)(fx - ix2)) / (w))) * (0.05f);
                }

                if (fy < 0)
                    rotz = 0.0f;
                else if ((fy - iy2) > h)
                    rotz = 360.0f;
                else
                {
                   rotz = rotz + ((360.0f * ((float)(fy - iy2)) / (h)) ) * (-0.05f);
                }
            }
        }

        /// <summary>
        /// This resets the position and looking of the camera 
        /// </summary>
        public void Reset()
        {
            posx = 187.0638f; posy = 101.0f; posz = 270.8559f;
            lookx = 186.7978f; looky = 101.0f; lookz = 269.8919f;
            yaw = (float)Math.Asin(lookz - posz);
            yawh = 1.5708f;
            looky = (float)(posy + (Math.Cos(yawh)));
            Refresh_Look();
        }
    }
}
