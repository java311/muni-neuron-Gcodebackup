using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Tao.OpenGl;

namespace Muni
{
    /// <summary>
    /// This is the main form of Muni
    /// </summary>
    public partial class Form1 : Form
    {

        private System.Globalization.CultureInfo es = new System.Globalization.CultureInfo("es-MX");

        private Mesh my_mesh;      ///This object saves all the model data and also some data about the scene
        private Container cajita;   ///This object draws a box containing the neuron model

        private int[] selectBuf;         //Selection Buffer used by OpenGL for the pitch functino
        private float escx, escy, escz;   //This are the scale factors for the 3D model
        private float centerx, centery, centerz;   
        private float micronx, microny, micronz, micron;  //The micron resolution for the image package
        private string initial_file;   
        private int objetive;       //The microscope's objetive magnitude
        private int selection;      
        private bool modelview;     //This flag indicates if theres a model ready on the memory to be showed
        private OpenGlColor [] colores;   //This OpenGlColor array saves the colors of the scene's objects
        private OpenGlColor current_color;  //This saves the current color of the neuron model
        private Ruler ruler;   //This is the ruler object to meause the lenght in microns
        private Camera cam1, cam2;  //This are the camera objects for the scene, one is for the principal view and the other is for the color preview sphere
        private int cx1, cy1, cx2, cy2;  
        private bool first_click;  
        private float micron_distance;  //This saves the distance in microns of the ruler.

        
        /// <summary>
        /// Returns false if s is not a valid floating point value.
        /// </summary>
        /// <param name="s">A string that represents an real value</param>
        /// <param name="c">Set the current CultureInfo to transform the string</param>
        /// <returns></returns>
        private bool check_real(string s, System.Globalization.CultureInfo c)
        {
            try
            {
                if (s == null) return false;
                if (s == string.Empty) return false;

                double x = double.Parse(s, c);
                if (x < 0)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This is the constructor of the Main Muni's form
        /// </summary>
        /// <param name="file"></param>
        public Form1(string file)
        {
            initial_file = file;
            colores = new OpenGlColor[4];
            colores[0] = new OpenGlColor(0,128,192);  //neuron color
            colores[1] = new OpenGlColor();           //background color
            colores[2] = new OpenGlColor(175,16,44);  //lenght markers color
            colores[3] = new OpenGlColor(255, 0, 0);  //container color

            modelview = false;

            InitializeComponent();

            //Variables for selection
            selectBuf = new int[256];
            ruler = new Ruler();
            selection = -1;

            this.simpleOpenGlControl2.InitializeContexts();
            this.simpleOpenGlControl1.InitializeContexts();
            cam2 = new Camera(10.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            cam1 = new Camera(187.0638f, 101.0f, 270.8559f, 186.7978f, 101.0f, 269.8919f);
            
            color_combo.SelectedIndex = 0;
            simpleOpenGlControl2.MakeCurrent();
            this.SetupRC();
            cam2.SetView(simpleOpenGlControl2.Width, simpleOpenGlControl2.Height);
            RenderColorScene();
            
            simpleOpenGlControl1.MakeCurrent();
            this.SetupRC();
            cam1.SetView(simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            RenderScene();
        }

        /// <summary>
        /// This function implements the actions for open a created model from a m3d file
        /// </summary>
        /// <param name="file">The fille's path were all the model data is saved on</param>
        /// <returns>This returns false if an error ocurrs and true otherwise</returns>
        private bool OpenModel(string file)
        {
            status.Text = "Opening Model...";
            progress.Visible = true;
            this.Refresh();

            if (my_mesh != null)
                my_mesh = null;

            my_mesh = new Mesh();

            #region Deprecated Read_Debug_File Function
            //This commented code had been deprecated, this saves the 3D model using the old shema 
            //using txt file formats. This technique was slow and space innefficient. 
            //FileStream fo = new FileStream(file, FileMode.Open, FileAccess.Read);
            //Cursor.Current = Cursors.WaitCursor;
            //if (my_mesh.Read_Debug_File(fo, progress) == true)
            //{
            //    escx = my_mesh.escx; escy = my_mesh.escy; escz = my_mesh.escz;
            //    view_escx.Text = escx.ToString(es);
            //    view_escy.Text = escy.ToString(es);
            //    view_escz.Text = escz.ToString(es);

            //    this.micron = my_mesh.micron;
            //    this.micronx = my_mesh.micronx;
            //    this.microny = my_mesh.microny;
            //    this.micronz = my_mesh.micronz;
            //    this.objetive = my_mesh.objetive;

            //    measure_objetive.Text = objetive.ToString();
            //    measure_microns.Text = micron.ToString();
            //    measure_pixels.Text = micronx.ToString();

            //    colores = my_mesh.colores;
            //    cajita = new Container(my_mesh.dimx, my_mesh.dimy, my_mesh.dimz, my_mesh.dimx / 2, my_mesh.dimy / 2, my_mesh.dimz / 2);
            //    Unblock_Form_Parts();
            //    modelview = true;
            //}
            //Cursor.Current = Cursors.Arrow; 
            #endregion

            Cursor.Current = Cursors.WaitCursor;
            my_mesh = MeshSerializer.LoadMesh(file);
            if (my_mesh != null)
            {
                escx = my_mesh.escx; escy = my_mesh.escy; escz = my_mesh.escz;
                view_escx.Text = escx.ToString(es);
                view_escy.Text = escy.ToString(es);
                view_escz.Text = escz.ToString(es);

                this.micron = my_mesh.micron;
                this.micronx = my_mesh.micronx;
                this.microny = my_mesh.microny;
                this.micronz = my_mesh.micronz;
                this.objetive = my_mesh.objetive;

                measure_objetive.Text = objetive.ToString();
                measure_microns.Text = micron.ToString();
                measure_pixels.Text = micronx.ToString();

                colores = my_mesh.colores;
                cajita = new Container(my_mesh.dimx, my_mesh.dimy, my_mesh.dimz, my_mesh.dimx / 2, my_mesh.dimy / 2, my_mesh.dimz / 2);
                Unblock_Form_Parts();
                modelview = true;
            }
            Cursor.Current = Cursors.Arrow;


            progress.Visible = false;
            status.Text = "Idle";
            this.Refresh();

            return true;
        }

        /// <summary>
        /// This function render's the main scene. This function only shows the 3D model if modelview flag
        /// is true and the view neuron checkbox is checked.
        /// </summary>
        private void RenderScene()
        {
            // Clear Screen And Depth Buffer
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            Gl.glInitNames();
            
            #region Draw Axes
            if (view_axes.Checked)
            {

                const float axisSize = 450.0f;
                // draw a line along the z-axis
                Gl.glPushMatrix();
                Gl.glColor3f(1.0f, 0.0f, 0.0f);
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex3f(0.0f, 0.0f, -axisSize);
                Gl.glVertex3f(0.0f, 0.0f, axisSize);
                Gl.glEnd();
                // draw a line along the y-axis
                Gl.glColor3f(0.0f, 1.0f, 0.0f);
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex3f(0.0f, -axisSize, 0.0f);
                Gl.glVertex3f(0.0f, axisSize, 0.0f);
                Gl.glEnd();
                // draw a line along the x-axis
                Gl.glColor3f(0.0f, 0.0f, 1.0f);
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex3f(-axisSize, 0.0f, 0.0f);
                Gl.glVertex3f(axisSize, 0.0f, 0.0f);
                Gl.glEnd();
                Gl.glPopMatrix();
            }
            #endregion Draw Axes

            cam1.Refresh_Look();

            Gl.glTranslatef(-centerx, -centery, -centerz);
            Gl.glPushMatrix();

            Gl.glClearColor(colores[1].R, colores[1].G, colores[1].B, 0.0f);
            if (modelview && my_mesh != null && view_neuron.Checked)
            {
                Gl.glPushMatrix();

                Gl.glTranslatef(centerx , centery , centerz );

                Gl.glRotatef(cam1.rotx, 1.0f, 0.0f, 0.0f);
                Gl.glRotatef(cam1.roty, 0.0f, 1.0f, 0.0f);
                Gl.glRotatef(cam1.rotz, 0.0f, 0.0f, 1.0f);

                if (view_length.Checked)
                    ruler.Draw(colores[2]);

                Gl.glTranslatef(-centerx , -centery , -centerz );

                Gl.glTranslatef(centerx , centery , centerz );
                Gl.glScalef(escx, escy, escz);
                Gl.glTranslatef(-centerx , -centery , -centerz );
                
                my_mesh.Type(view_triangles.Checked, view_lines.Checked, view_points.Checked);
                Paint_Mesh();
            }

            if (cajita != null && view_container.Checked)
            {
               cajita.Draw(colores[3]);
            }
            
            Gl.glPopMatrix();
            Gl.glPopMatrix();

            Gl.glFlush();
        }

        /// <summary>
        /// This little function renders the color sphere scene to get a preview of the
        /// picked color. This scene uses it's own lighting and camera object. 
        /// </summary>
        private void RenderColorScene()
        {
            // Clear Screen And Depth Buffer
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            cam2.Refresh_Look();
            
            Gl.glColor3ub(current_color.R, current_color.G, current_color.B);
                        
            Glu.GLUquadric q;
            q = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(q, Tao.OpenGl.Glu.GLU_SMOOTH);

            Glu.gluSphere(q, 3.0f, 25, 30);
            Gl.glFlush();
        }

        /// <summary>
        /// This function paints the 3D model's vertex mesh. This uses the vertexarray tecnique
        /// no graph all the 3D model because the 3D model are very big. 
        /// </summary>
        unsafe private void Paint_Mesh()
        {
            Gl.glEnable(Gl.GL_RESCALE_NORMAL);
            
            Gl.glColor3ub(colores[0].R, colores[0].G, colores[0].B);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_NORMAL_ARRAY);
                        
            my_mesh.Draw();

            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
            
        }

        /// <summary>
        /// This function intializes all the lighting parameters.
        /// </summary>
        private void SetupRC()
        {
            float[] LightPos = new float[]{ 20.0f, 100.0f, 20.0f, 0.0f };
            float[] LightSpc = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] LightAmb = { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] LightDif = { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] spec = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] emmi = { 0.0f, 0.0f, 0.0f, 1.0f };

            //Pone el fondo Negro
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, LightPos);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, LightSpc);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, LightAmb);
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, LightDif);

            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER, 0);
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Gl.glEnable(Gl.GL_CULL_FACE);
            Gl.glCullFace(Gl.GL_FRONT_AND_BACK);

            Gl.glEnable(Gl.GL_NORMALIZE);
            Gl.glColorMaterial(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT_AND_DIFFUSE);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_SPECULAR, spec);
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_EMISSION, emmi);
        }

        /// <summary>
        /// This function overrides de OnPaint event of the main form. This is
        /// to refresh the principal OpenGL scene object
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Clear Screen And Depth Buffer
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            RenderScene();
        }

        /// <summary>
        /// This function overrides de OnResize event of the main form. This is 
        /// for refresh the main OpenGL scene dimentions
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (cam1 != null)
            {
                cam1.SetView(simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
                RenderScene();
            }
        }

        /// <summary>
        /// Opens image pack process form and unblocks the 3d stuff from the form
        /// if the user did it well opening and processing the images.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image_Form imf = new Image_Form();
            if ( imf.ShowDialog() == DialogResult.OK)
            {
                // Gets the my_mesh object from the Image_Form dialog. 
                my_mesh = imf.my_mesh;
                if (my_mesh != null)
                {
                    cajita = imf.cajita;
                    this.escx = imf.escx;
                    this.escy = imf.escy;
                    this.escz = imf.escz;
                    view_escx.Text = escx.ToString(es);
                    view_escy.Text = escy.ToString(es);
                    view_escz.Text = escz.ToString(es);
                    this.micron = imf.micron;
                    this.micronx = imf.micronx;
                    this.microny = imf.microny;
                    this.micronz = imf.micronz;
                    this.objetive = imf.objetive;

                    measure_objetive.Text = objetive.ToString();
                    measure_microns.Text = micron.ToString();
                    measure_pixels.Text = micronx.ToString();
                    
                    // Unblock the other stuff from the form and paint the generated model.
                    Unblock_Form_Parts();

                    
                }
            }
        }

        /// <summary>
        /// This function enables several button an tabs on the main form, this function is
        /// called when a 3d model had been created or readed. 
        /// </summary>
        private void Unblock_Form_Parts()
        {
            tabControl1.Enabled = true;
            cam1.Adjust_to_Model(escx, escy, escz, my_mesh.dimx, my_mesh.dimy, my_mesh.dimz);

            centerx = my_mesh.dimx / 2;
            centery = my_mesh.dimy / 2;
            centerz = my_mesh.dimz / 2;

            ruler.set_calibration(micronx, microny, micronz, micron);

            splitContainer1.Panel2.Enabled = true;
            Save_Model.Enabled = true;
            menu_savemodel.Enabled = true;
            modelview = true;
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// This funciton implements the OnPaint event for the simpleOpenGlControl object, this
        /// only sets the view of the camera and calls the functions that renders the scene. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            cam1.SetView(simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            RenderScene();
        }

        /// <summary>
        /// This function is called every time the splitContainer dimensions change. 
        /// And then the setview and the dimensions of the scene change. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            cam1.SetView(simpleOpenGlControl1.Width, simpleOpenGlControl1.Height);
            RenderScene();
        }

        /// <summary>
        /// This function applies the scale factors in the 3D model. 
        /// And then the OpenGL scene is refresh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_apply_Click(object sender, EventArgs e)
        {
            if (check_real(view_escx.Text, es) == false)
            {
                MessageBox.Show("The scale factor on X must be a postive real number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                view_escx.BackColor = Color.Firebrick;
                return;
            }
            else
                view_escx.BackColor = SystemColors.Window;
            if (check_real(view_escy.Text, es) == false)
            {
                MessageBox.Show("The scale factor on Y must be a postive real number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                view_escy.BackColor = Color.Firebrick;
                return;
            }
            else
                view_escy.BackColor = SystemColors.Window;
            if (check_real(view_escz.Text, es) == false)
            {
                MessageBox.Show("The scale factor on Z must be a postive real number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                view_escz.BackColor = Color.Firebrick;
                return;
            }
            else
                view_escz.BackColor = SystemColors.Window;

            escx = float.Parse(view_escx.Text, es);
            escy = float.Parse(view_escy.Text, es);
            escz = float.Parse(view_escz.Text, es);

            //cam1.Adjust_to_Model(escx, escy, escz, my_mesh.dimx, my_mesh.dimy, my_mesh.dimz);
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// This function applies the colors selected on the OpenGL scene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_change_Click(object sender, EventArgs e)
        {
            switch (color_combo.SelectedIndex)
            {
                //Change the neuron color.
                case 0:
                    if (my_mesh != null)
                    {
                        colores[0] = current_color;
                        simpleOpenGlControl1.MakeCurrent();
                        simpleOpenGlControl1.Refresh();                                                
                    }
                    break;
                //Change the background color.
                case 1:
                    colores[1] = current_color;
                    simpleOpenGlControl1.MakeCurrent();
                    simpleOpenGlControl1.Refresh();                                                
                    break;
                //Change the Length Markers color.
                case 2:
                    colores[2] = current_color;
                    simpleOpenGlControl1.MakeCurrent();
                    simpleOpenGlControl1.Refresh();                                                
                    break;
                //Change the Container Box color
                case 3:
                    colores[3] = current_color;
                    simpleOpenGlControl1.MakeCurrent();
                    simpleOpenGlControl1.Refresh();
                    break;
            }
        }

        /// <summary>
        /// This function refresh the selected color on the sphere color preview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void color_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (color_combo.SelectedIndex >= 0)
            {
                current_color = colores[color_combo.SelectedIndex];
                simpleOpenGlControl2.MakeCurrent();
                RenderColorScene();
                simpleOpenGlControl2.Refresh();
                simpleOpenGlControl1.MakeCurrent();
                color_trackR.Value = current_color.R;
                color_trackG.Value = current_color.G;
                color_trackB.Value = current_color.B;
            }
        }

        /// <summary>
        /// When the color tab is selected the OpenGL control showing the preview color sphere
        /// is selected as current.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;

            switch (index)
            {
                case 1:
                    simpleOpenGlControl2.MakeCurrent();
                    break;
            }
        }

        /// <summary>
        /// Controls the mouse clicks over the scene control.
        /// Middle = resets the camera 
        /// Left = calls the pitch functions (this needs some debuging work)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleOpenGlControl1_MouseClick(object sender, MouseEventArgs e)
        {
            simpleOpenGlControl1.MakeCurrent();

            if (e.Button == MouseButtons.Middle)
            {
                if (modelview == false)
                {
                    cam1.Reset();
                }
                else
                {
                    cam1.Adjust_to_Model(escx, escy, escz, my_mesh.dimx, my_mesh.dimy, my_mesh.dimz);
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                //This part of the code need a check out. This has several hidden bugs
                selection = seleccion(e.X, simpleOpenGlControl1.Height - e.Y);
                ruler.selected(selection); 
            }
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// This functions recives the windows coordenates where the user clicked. 
        /// And then it implements the pitch on the scene objects. using the glSelectBuffer
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int seleccion(int x, int y)
        {
            int hits;
            int r;
            int[] view = new int[4];

            selectBuf = null;
            selectBuf = new int[256];

            Gl.glSelectBuffer(selectBuf.Length, selectBuf);
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, view);
            Gl.glRenderMode(Gl.GL_SELECT);
            Gl.glInitNames();

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPushMatrix();
            Gl.glLoadIdentity();

            Glu.gluPickMatrix(x, y, 1.0, 1.0, view);
            Glu.gluPerspective(60.0, 1.0, 0.0001, 1000.0);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glFlush();
            this.RenderScene();

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPopMatrix();

            hits = Gl.glRenderMode(Gl.GL_RENDER);

            if (hits != 0)
            {
                memo1.Text = string.Empty;
                foreach (int i in selectBuf)
                { memo1.Text = i.ToString() + Environment.NewLine; }
                r = selectBuf[3];
            }
            else
            {
                memo1.Text = string.Empty;
                foreach (int i in selectBuf)
                { memo1.Text = i.ToString() + Environment.NewLine; }
                r = selectBuf[3];
                r = -1;
            }

            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            return r;

        }

        /// <summary>
        /// This is called when you use the scrolls to select a different color, and to get the
        /// preview of that color the OpenGL control need to be the current one. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void color_trackR_Scroll(object sender, EventArgs e)
        {
            if (color_combo.SelectedIndex >= 0)
            {
                current_color.R = (byte)color_trackR.Value;
                simpleOpenGlControl2.MakeCurrent();
                RenderColorScene();
                simpleOpenGlControl2.Refresh();
                simpleOpenGlControl1.MakeCurrent();
            }
        }

        /// <summary>
        /// This is called when you use the scrolls to select a different color, and to get the
        /// preview of that color the OpenGL control need to be the current one. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void color_trackG_Scroll(object sender, EventArgs e)
        {
            if (color_combo.SelectedIndex >= 0)
            {
                current_color.G = (byte)color_trackG.Value;
                simpleOpenGlControl2.MakeCurrent();
                RenderColorScene();
                simpleOpenGlControl2.Refresh();
                simpleOpenGlControl1.MakeCurrent();
            }

        }

        /// <summary>
        /// This is called when you use the scrolls to select a different color, and to get the
        /// preview of that color the OpenGL control need to be the current one. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void color_trackB_Scroll(object sender, EventArgs e)
        {
            if (color_combo.SelectedIndex >= 0)
            {
                current_color.B = (byte)color_trackB.Value;
                simpleOpenGlControl2.MakeCurrent();
                RenderColorScene();
                simpleOpenGlControl2.Refresh();
                simpleOpenGlControl1.MakeCurrent();
            }
        }

        /// <summary>
        /// This implements the savemodel click event. This also calls all the functions
        /// to serialize and save the current model on a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_savemodel_Click(object sender, EventArgs e)
        {
            status.Text = "Saving Model...";
            progress.Visible = true;
                        
            if (my_mesh != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.Refresh();
                    Cursor.Current = Cursors.WaitCursor;
                    my_mesh.Save_File(saveFileDialog1.FileName, this.colores, this.escx, this.escy, this.escz, this.micronx, this.microny, this.micronz, this.micron, this.objetive);
                    Cursor.Current = Cursors.Arrow;
                    MessageBox.Show("The model have been successfully saved.", "INFO - Muni", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #region Deprecaded Saving Model on a txt file
                    //This piece of code implements the old way to save the model's data on a txt file. 
                    //This code is keep for debug purposes. 
                    //this.Refresh();
                    //FileStream fss = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
                    //Cursor.Current = Cursors.WaitCursor;
                    //my_mesh.Save_Debug_File(fss, colores, escx.ToString(es), escy.ToString(es), escz.ToString(es), micronx.ToString(es), microny.ToString(es), micronz.ToString(es), micron.ToString(es), this.objetive.ToString(es), progress);
                    //Cursor.Current = Cursors.Arrow;
                    //MessageBox.Show("The model have been successfully saved.", "INFO - Muni", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //fss.Close(); 
                    #endregion

                }
            }

            progress.Visible = false;
            status.Text = "Idle";
            toolStrip1.Refresh();
        }

        /// <summary>
        /// This one catches a Enter on the scale factor textbox, and then it calls view_apply function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_openmodel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenModel(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// This one catches a Enter on the scale factor textbox, and then it calls view_apply function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_escx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                view_apply_Click(null, null);
        }

        /// <summary>
        /// This one catches all the key events on the simpleOpenGLControl most of these keys are  
        /// for controling the camera position.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) && simpleOpenGlControl1.Focused)
            {
                if (selection == -1)
                {
                    if (keyData == Keys.Add) // Positive Y-Axis Right Turn
                    {
                        cam1.Up();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.Subtract) // Positive Y-Axis Right Turn
                    {
                        cam1.Down();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.D || keyData == Keys.Right) // Positive Y-Axis Right Turn
                    {
                        cam1.Turn_Right();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.A || keyData == Keys.Left) // Negative Y-Axis Left Turn
                    {
                        cam1.Turn_Left();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.W || keyData == Keys.Up)  // Negative X-Axis go straigth
                    {
                        cam1.Go_Ahead();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.S || keyData == Keys.Down)  // Positive X-Axis go backwards
                    {
                        cam1.Go_Back();
                        simpleOpenGlControl1.Refresh();
                    }
                }
                else
                {
                    if (keyData == Keys.Add) // Positive Y-Axis Right Turn
                    {
                        micron_distance = ruler.Up(selection);
                        measure_result.Text = micron_distance.ToString();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.Subtract) // Positive Y-Axis Right Turn
                    {
                        micron_distance = ruler.Down(selection);
                        measure_result.Text = micron_distance.ToString();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.D || keyData == Keys.Right) // Positive Y-Axis Right Turn
                    {
                        micron_distance = ruler.Right(selection);
                        measure_result.Text = micron_distance.ToString();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.A || keyData == Keys.Left) // Negative Y-Axis Left Turn
                    {
                        micron_distance = ruler.Left(selection);
                        measure_result.Text = micron_distance.ToString();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.W || keyData == Keys.Up)  // Negative X-Axis go straigth
                    {
                        micron_distance = ruler.Ahead(selection);
                        measure_result.Text = micron_distance.ToString();
                        simpleOpenGlControl1.Refresh();
                    }
                    else if (keyData == Keys.S || keyData == Keys.Down)  // Positive X-Axis go backwards
                    {
                        micron_distance = ruler.Back(selection);
                        measure_result.Text = micron_distance.ToString();
                        simpleOpenGlControl1.Refresh();
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Catches the mouse cursor postion on the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleOpenGlControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (modelview == true)
            {
                cx1 = e.X;
                cy1 = e.Y;
                cx2 = e.X;
                cy2 = e.Y;
                first_click = true;
            }
        }

        /// <summary>
        /// This also controls the position of the camera emulation the scroll and zooming functions
        /// in vector drawing programs. Shift + Wheel = Up/Down, Ctrl + Whell = Foward/Backwards
        /// Alt + Whell = Left/Right rotation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleOpenGlControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control) //If Ctrl key is pressed (ZoomIn-Out)
            {
                if (e.Delta > 0)
                    cam1.Go_Ahead();
                else if (e.Delta < 0)
                    cam1.Go_Back();
            }
            else if (Control.ModifierKeys == Keys.Alt) //If Alt is pressed (Left-Rigth)
            {
                if (e.Delta > 0)
                    cam1.Turn_Right();
                else if (e.Delta < 0)
                    cam1.Turn_Left();
            }
            else
            {
                if (e.Delta > 0)
                    cam1.Up();
                else if (e.Delta < 0)
                    cam1.Down();
            }
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// This catches de event when the right or left mouse button is pressed and the mouse cursor moves. 
        /// This event activates the camera movement accordingly to the cursor position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleOpenGlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((modelview == true) && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right )) 
            {
                cam1.Mouse_Move(cx1, cy1, cx2, cy2, e.X, e.Y, e.Button, simpleOpenGlControl1.Width, simpleOpenGlControl1.Height, first_click);

                cx1 = e.X;
                cy1 = e.Y;
                first_click = false;
                simpleOpenGlControl1.Refresh();
            }
        }

        #region Camera Buttons

        private void camera_panel1_up_Click(object sender, EventArgs e)
        {
            cam1.Up();
            simpleOpenGlControl1.Refresh();
        }

        private void camera_panel1_down_Click(object sender, EventArgs e)
        {
            cam1.Down();
            simpleOpenGlControl1.Refresh();
        }

        private void camera_panel1_foward_Click(object sender, EventArgs e)
        {
            cam1.Go_Ahead();
            simpleOpenGlControl1.Refresh();
        }

        private void camera_panel1_backward_Click(object sender, EventArgs e)
        {
            cam1.Go_Back();
            simpleOpenGlControl1.Refresh();
        }

        private void camera_panel1_left_Click(object sender, EventArgs e)
        {
            cam1.Turn_Left();
            simpleOpenGlControl1.Refresh();
        }

        private void camera_panel1_right_Click(object sender, EventArgs e)
        {
            cam1.Turn_Right();
            simpleOpenGlControl1.Refresh();
        }

        private void camera_panel1_reset_Click(object sender, EventArgs e)
        {
            cam1.Adjust_to_Model(escx, escy, escz, my_mesh.dimx, my_mesh.dimy, my_mesh.dimz);
            simpleOpenGlControl1.Refresh();
        }

        private void view_resetrotation_Click(object sender, EventArgs e)
        {
            cam1.rotx = 0.0f;
            cam1.roty = 0.0f;
            cam1.rotz = 0.0f;
            simpleOpenGlControl1.Refresh();
        }



        #endregion Camera Buttons

        /// <summary>
        /// When a radio button changes its value the main scene is refreshed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewradiocheck(object sender, EventArgs e)
        {
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// This moves the selected ruler's pointer to the front.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measuer_ahead_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Ahead(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This moves the selected ruler's pointer to the back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_back_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Back(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This moves the selected ruler's pointer to the left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_left_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Left(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This moves the selected ruler's pointer to the right.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_right_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Right(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This moves the selected ruler pointer up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_up_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Up(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This moves the selected ruler pointer down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_down_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Down(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This function resets the Ruler position to the center of the scene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_reset_Click(object sender, EventArgs e)
        {
            if (selection != -1)
            {
                micron_distance = ruler.Reset(selection);
                measure_result.Text = micron_distance.ToString();
                simpleOpenGlControl1.Refresh();
            }
        }

        /// <summary>
        /// This function resets both ruller pointers on the scene to the center of the model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mesure_resetboth_Click(object sender, EventArgs e)
        {
            ruler.Reset(1);
            micron_distance = ruler.Reset(2);
            measure_result.Text = micron_distance.ToString();
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// When Muni's Main Form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Splash sp = new Splash();
            this.Visible = false;
            sp.ShowDialog();
            this.Visible = true;

            if (initial_file != null)
            {
                if (File.Exists(initial_file))
                {
                    OpenModel(initial_file);
                }
                else
                {
                    MessageBox.Show("The file : " + initial_file + " doesn't exist.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// This one finishes the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (my_mesh != null)
                my_mesh = null;

            this.Close();
        }

        /// <summary>
        /// Opens Muni's webpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_view_website_Click(object sender, EventArgs e)
        {
            //Go to the Website for help
            System.Diagnostics.Process.Start("http://code.google.com/p/muni-neuron/");
        }

        /// <summary>
        /// This diplays activates the ruler on the scene
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void measure_view_length_CheckedChanged(object sender, EventArgs e)
        {   
            view_length.Checked = measure_view_length.Checked;
            simpleOpenGlControl1.Refresh();
        }

        /// <summary>
        /// Displays the about form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_about_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.Show();
        }



        private void simpleOpenGlControl1_Enter(object sender, EventArgs e)
        {
            simpleOpenGlControl1.BorderStyle = BorderStyle.Fixed3D;
        }


    }
}