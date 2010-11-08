using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Muni
{
    /// <summary>
    /// This class implements the 4 step model creation form.
    /// </summary>
    public partial class Image_Form : Form
    {
        private System.Globalization.CultureInfo es = new System.Globalization.CultureInfo("es-MX");
        public int dimx, dimy, dimz;    //This variables store the image package dimentions
        public int objetive;            //The objetive magnification used to take the photos.
        public byte[,] bmp_matrix;      //This matrix stores the image data on the main memory, just for performance reasons.
        public float escx, escy, escz;  //These store the scale factor applicated to the model.  
        public int cube_x, cube_y;      //These store the dimensions of the marching cube, who sets the resolution of the 3d model.
        string[] filenames;             //A string array with all the filenames for the image package
        string[] zoom_filenames;        //This string array stores all the resulting filenames from the Batch Zoom

        Image currentphoto;  // This is use for generate the current thumbnail
        int tx, ty, tbx, tby;    // These are used for calculate the square indicator on the thumbnail
        int firstx, firsty;  //These are used for draw lines over the image.
        bool clicked = false;  //Checks if the user is clicking for draw a Line
        
        int infocus, outfoucus; //These counts how many infocus and out of focus dendrites had been calculated.
        private Isolevel iso_obj;  //This class calculates the isolevel from the dendritic structures choseen by the user. 

        private MarchingCubes mc; //This class implements all the marching cubes algorithm
        private bool updown;     //This flag avoids the unnecesary image loading when an item list switch down or up.
        private ArrayList borderpixels;   //This array stores all the border pixels and show them over the image's thumbnail
        private Calibration calib;   //This class gets the relation between pixels and microns, calculating the diameter in pixels of one dentrite. 

        public int isolevel;   //Has the isolevel value of the dendritic structure. 
        public Mesh my_mesh;    //This object has the info of the generated 3d model, the normal and the point array.
        public Container cajita;   //This object has all the info to draw a box who contains the neuron}
        public float microns;     //These store the pixel/micron resolution of the image package
        public int   pixels;     //These store the pixel/micron resolution of the image package
        public float micronx, microny, micronz;  //These store dimensions of a dendrite in pixels.
        public float micron;       //This one stores the stamated diameter of a dendrite in microns (user given).
        
        /// <summary>
        /// Returns false if s is not a valid integer
        /// </summary>
        /// <param name="s">A string that represents an interger value</param>
        /// <param name="c">Set the current CultureInfo to transform the string</param>
        /// <returns></returns>
        private bool check_integer(string s, System.Globalization.CultureInfo c)
        {
            try
            {
                if (s == null) return false;
                if (s == string.Empty) return false;

                int x = int.Parse(s, c);
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
        /// This is default class constructor
        /// </summary>
        public Image_Form()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            isolevel = -1;
            borderpixels = new ArrayList();
            #region PanelDoubleBuffer
            MethodInfo m = typeof(Panel).GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            Object[] parameters = new Object[2];
            parameters[0] = new ControlStyles();
            parameters[0] = ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint;
            parameters[1] = new bool();
            parameters[1] = true;
            m.Invoke(panel3_thumbover, parameters);
            m.Invoke(panel3_pictureBox1over, parameters);
            m = typeof(Panel).GetMethod("UpdateStyles", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            m.Invoke(panel3_thumbover, null);
            m.Invoke(panel3_pictureBox1over, null);
            #endregion
            UpdateStyles();
            Change_Step(1);
        }

        /// <summary>
        /// This function changes dockstyle of the different panel for each step.
        /// And also performs a step on the progressbar and changes the color of
        /// the corresponding button on the vertical toolbar.
        /// </summary>
        /// <param name="choice">The number of the step to move</param>
        public void Change_Step(byte choice)
        {
            switch (choice)
            {
                case 1:
                    panel1.BringToFront();
                    panel1.Dock = DockStyle.Fill;
                    tool_label.Text = "Step 1 of 4";
                    ToolStep1.BackColor = SystemColors.Highlight;
                    break;
                case 2:
                    panel2.BringToFront();
                    panel2.Dock = DockStyle.Fill;
                    tool_label.Text = "Step 2 of 4";
                    tool_progressBar.PerformStep();
                    ToolStep1.BackColor = SystemColors.Control;
                    toolStep2.BackColor = SystemColors.Highlight;
                    break;
                case 3:
                    panel3.BringToFront();
                    panel3.Dock = DockStyle.Fill;
                    tool_label.Text = "Step 3 of 4";
                    tool_progressBar.PerformStep();
                    toolStep2.BackColor = SystemColors.Control;
                    ToolStep3.BackColor = SystemColors.Highlight;
                    break;
                case 4:
                    panel4.BringToFront();
                    panel4.Dock = DockStyle.Fill;
                    tool_label.Text = "Step 4 of 4";
                    tool_progressBar.PerformStep();
                    ToolStep3.BackColor = SystemColors.Control;
                    ToolStep4.BackColor = SystemColors.Highlight;
                    break;
            }
        }

        /// <summary>
        /// This class implements the openfiles button on the first step. This function opens all the selected
        /// images on a normal CommonDialog Box and fill the filenames array with their paths.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_openfiles_Click(object sender, EventArgs e)
        {
            Image photo;
            FileStream fs;
            openFileDialog1.InitialDirectory = "C:\\tmp\\fotos3\\Zoom3";
                       
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.Length < 4)
                {
                    MessageBox.Show("You must select at least 4 compatible files.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    filenames = null;
                    return;
                }
                else 
                {
                    filenames = openFileDialog1.FileNames;
                    
                    fs = new FileStream(filenames[0], FileMode.Open, FileAccess.Read);
                    photo = Bitmap.FromStream(fs, true, false);

                    dimx = photo.Width;
                    dimy = photo.Height;
                    dimz = filenames.Length;

                    panel1_next.Enabled = true;
                    photo.Dispose(); fs.Close(); fs.Dispose();
                }
            }
        }

        /// <summary>
        /// This function implements the open folder button on the first panel. This function fills the filenames array with
        /// all the image filenames founded on the folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_openfolder_Click(object sender, EventArgs e)
        {
            Image photo;
            FileStream fs;
            List<string> images = new List<string>();
            string extensions = "*.bmp,*.jpg,*.jpeg,*.tif";
            string[] args = extensions.Split(',');
            folderBrowserDialog1.SelectedPath = "C:\\tmp\\fotos3\\Zoom3";
            //folderBrowserDialog1.SelectedPath = "C:\\Documents and Settings\\Invitado\\Mis documentos";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string a in args)
                {
                    images.AddRange(Directory.GetFiles(folderBrowserDialog1.SelectedPath,a));
                }
                
                images.Sort();

                filenames = (string[]) images.ToArray();

                if (filenames.Length < 4)
                {
                    MessageBox.Show("You must select at least 4 compatible files.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    filenames = null;
                    return;
                }
                else
                {
                    fs = new FileStream(filenames[0], FileMode.Open, FileAccess.Read);
                    photo = Bitmap.FromStream(fs, true, false);

                    dimx = photo.Width;
                    dimy = photo.Height;
                    dimz = filenames.Length;

                    panel1_next.Enabled = true;

                    photo.Dispose(); fs.Close(); fs.Dispose();
                }
            }
        }

        /// <summary>
        /// This only calls the Change_Step function with 2 as parameter. This means that the user is passing from 
        /// the first panel to the second one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_next_Click(object sender, EventArgs e)
        {
            Change_Step(2);
        }

        /// <summary>
        /// This panel has some objects to obtain several parameters like the objetive maginitifation, and the micron
        /// image resolution on X and Y.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_next_Click(object sender, EventArgs e)
        {
            if (check_integer(panel2_objetive.Text,es) != true)
            {
                MessageBox.Show("The objetive magnification must be a natural number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel2_objetive.BackColor = Color.Firebrick;
                return;
            }
            if (check_integer(panel2_pixel.Text, es) != true)
            {
                MessageBox.Show("The pixel value must be a natural number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel2_pixel.BackColor = Color.Firebrick;
                return;
            }
            if (check_real(panel2_microns.Text, es) != true)
            {
                MessageBox.Show("The microns must be a floating point number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel2_microns.BackColor = Color.Firebrick;
                return;
            }

            pixels = int.Parse(panel2_pixel.Text, es);
            microns = float.Parse(panel2_microns.Text, es);

            if ((pixels != 0) || (microns != 0)) 
            {
                MessageBox.Show("These must be zero (this option haven't been implemented yet).", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel2_microns.BackColor = Color.Firebrick;
                panel2_pixel.BackColor = Color.Firebrick;
                return;
            }
            
            objetive = int.Parse(panel2_objetive.Text, es);
            
            for (int i=0; i < filenames.Length ;i++)
            {
                panel3_listBox.Items.Add(filenames[i].Substring(filenames[i].LastIndexOf(System.IO.Path.DirectorySeparatorChar) +1 ));
            }
            
            panel3.SetRowSpan(panel3_panel, 2);
            panel3.SetRowSpan(panel3_panel2, 2);
            isolevel = -1;
            
            panel3_thumbover.Parent = panel3_thumb;
            panel3_thumbover.Dock = DockStyle.Fill;
            panel3_thumbover.BringToFront();

            panel3_pictureBox1over.Parent = panel3_pictureBox1;
            panel3_pictureBox1over.Dock = DockStyle.Fill;
            panel3_pictureBox1over.BringToFront();
            panel3_listBox.SelectedIndex = dimz / 2;
            iso_obj = new Isolevel();
            Change_Step(3);
        }

        /// <summary>
        /// This panel calibration interface. This interface has a big picturebox object a list and several buttons. 
        /// This interface helps the user to obtain the isolevel value, just by selecting some focused and diffuse 
        /// regions on the image package.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (panel3_listBox.SelectedIndex >= 0 && updown == false)
            {
                Cursor.Current = Cursors.WaitCursor;
                FileStream fs;
                fs = new FileStream(filenames[panel3_listBox.SelectedIndex], FileMode.Open, FileAccess.Read);
                currentphoto = Bitmap.FromStream(fs, true, false);
                panel3_pictureBox1.Image = currentphoto;
                panel3_thumb.Image = currentphoto.GetThumbnailImage(panel3_thumb.Width, panel3_thumb.Height, null, IntPtr.Zero);
                tx = (panel3_thumb.Image.Width * panel3_panel.HorizontalScroll.Value) / dimx;
                tbx = (panel3_thumb.Image.Width * panel3_panel.HorizontalScroll.LargeChange) / dimx;
                ty = (panel3_thumb.Image.Height * panel3_panel.VerticalScroll.Value) / dimy;
                tby = (panel3_thumb.Image.Height * panel3_panel.VerticalScroll.LargeChange) / dimy;
                Paint_Thumb();
                if (isolevel != -1)
                {
                    GetBorderPixels();
                    Paint_Thumb();
                }
                Cursor.Current = Cursors.Arrow;
            }
            else
                updown = false;
        }

        /// <summary>
        /// This little function shows the selected isolevel value on the picture thumbnail. This
        /// help the user to know if the calculated isolevel is correct or not.
        /// </summary>
        private void GetBorderPixels()
        {
            if (panel3_listBox.SelectedIndex >= 0)
            {
                borderpixels.Clear();
                int index = panel3_listBox.SelectedIndex;
                int xt, yt;
                Fill_Matrix(index);
                
                for (int x = 0; x < dimx; x++)
                {
                    for (int y = 0; y < dimy; y++)
                    {
                        if (bmp_matrix[x, y] == isolevel)
                        {
                            xt = ((x * panel3_thumb.Image.Width) / (dimx));
                            yt = ((y * panel3_thumb.Image.Height) / (dimy));

                            borderpixels.Add(xt); borderpixels.Add(yt);                            
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// This function paint a thumbnail of the image plane in another picturebox. This
        /// helps the user to move around the image stack to quickly select the diffuse and 
        /// focus regions. This also paints a little red rectangle on the thumbnail to show
        /// were is the image scroll
        /// </summary>
        private void Paint_Thumb()
        {
            panel3_thumbover.BringToFront();
            Pen mypen = new Pen(Color.Firebrick);
            Graphics canvas = panel3_thumbover.CreateGraphics();
            panel3_thumbover.Refresh();

            if (isolevel != -1)
            {
                int x, y;
                mypen.Color = Color.AliceBlue;
                for (int i = 0; i < borderpixels.Count; i = i + 2)
                {
                    x = (int)borderpixels[i];
                    y = (int)borderpixels[i + 1];

                    canvas.DrawLine(mypen, x, y, x + 1, y + 1);
                }
            }
            else
            {
                mypen.Width = 2;
                canvas.DrawRectangle(mypen, tx, ty, tbx, tby);
                mypen.Width = 1;
            }
            mypen.Dispose(); canvas.Dispose();
        }

        /// <summary>
        /// This function scrolls the main picturebox object and also refresh the thumbnail picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_panel_Scroll(object sender, ScrollEventArgs e)
        {
            if (isolevel == -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    tx = (panel3_thumb.Width * e.NewValue) / dimx;
                    tbx = (panel3_thumb.Width * panel3_panel.HorizontalScroll.LargeChange) / dimx;
                    Paint_Thumb();
                }
                else if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    ty = (panel3_thumb.Height * e.NewValue) / dimy;
                    tby = (panel3_thumb.Height * panel3_panel.VerticalScroll.LargeChange) / dimy;
                    Paint_Thumb();
                }
                Cursor.Current = Cursors.Arrow;
            }
        }
        
        /// <summary>
        /// The user can scroll the main picturebox using the thumbnail box. The user just needs to 
        /// move the little red rectangule around with a left mouse click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_pictureBox1over_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (panel3_listBox.SelectedIndex < 0)
                    panel3_listBox.SelectedIndex = dimz / 2;

                using (Pen mypen = new Pen(Color.ForestGreen))
                {
                    using (Graphics canvas = panel3_pictureBox1over.CreateGraphics())
                    {
                        clicked = true;
                        firstx = e.X; firsty = e.Y;
                    }
                }
            }
        }
        
        /// <summary>
        /// The user can select the focus and diffuse regions of the image, just by pressing the left mouse button
        /// move the mouse over the selected region and unpressing the left mouse button. This event captures the 
        /// mouseup event and then a line is painted over the selected region, to indicate the user the selected 
        /// region.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_pictureBox1over_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                if (!((firstx == e.X) && (firsty == e.Y)))
                {
                    using (Pen mypen = new Pen(Color.DodgerBlue))
                    {
                        using (Graphics canvas = panel3_pictureBox1over.CreateGraphics())
                        {
                            canvas.DrawLine(mypen, firstx, firsty, e.X, e.Y);
                            mypen.Color = Color.Firebrick;
                            canvas.FillEllipse(mypen.Brush, e.X - 4, e.Y - 4, 8, 8);
                            canvas.FillEllipse(mypen.Brush, firstx - 4, firsty - 4, 8, 8);

                            if (panel3_radio1.Checked)
                            {
                                infocus++;
                                iso_obj.Add_Line(firstx, firsty, e.X, e.Y, filenames[panel3_listBox.SelectedIndex], true);
                                Focus_Num_Check();
                            }
                            else if (panel3_radio2.Checked)
                            {
                                outfoucus++;
                                iso_obj.Add_Line(firstx, firsty, e.X, e.Y, filenames[panel3_listBox.SelectedIndex], false);
                                Focus_Num_Check();
                            }
                            else if (panel3_radio3.Checked)
                            {
                                calib = new Calibration();
                                calib.Add_Line(firstx, firsty, e.X, e.Y, panel3_listBox.SelectedIndex);
                                Focus_Num_Check();
                            }
                        }
                    }
                }
                clicked = false;
            }
        }

        /// <summary>
        /// This function validates the number focus and diffuse regions selected. And it also checks
        /// if the user has selected a dendrite and estimated it's diameter.
        /// </summary>
        private void Focus_Num_Check()
        {
            Cursor.Current = Cursors.WaitCursor;
            
            if (infocus >= 3)
            {
                //Put a Good image besides radio button
                panel3_labelinfocus.ForeColor = Color.ForestGreen;
                panel3_focusok.Visible = true;
            }

            if (outfoucus >= 3)
            {
                //Put a Good image besides radio button
                panel3_labeloutfocus.ForeColor = Color.ForestGreen;
                panel3_outfocusok.Visible = true;
            }
            if (calib != null)
            {
                panel3_single.Visible = true;
            }

            panel3_labelinfocus.Text = infocus.ToString();
            panel3_labeloutfocus.Text = outfoucus.ToString();

            if ((infocus >= 3) && (outfoucus >= 3) && (calib != null))
            {
                isolevel = iso_obj.Calculate_Isolevel();
                panel3_isolist.Items.Clear();
                for (int i = isolevel - 5; i <= isolevel +5 ; i++)
                {
                    panel3_isolist.Items.Add(i.ToString());
                }
                panel3_isolist.SelectedIndex = 5;

                GetBorderPixels();
                Paint_Thumb();

                panel3_next.Enabled = true;
            }
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// This function loads the image data into byte matrix. This feature improves the algorithms speed.
        /// </summary>
        /// <param name="plano">The number of the image plane to be loaded</param>
        private void Fill_Matrix(int plano)
        {
            FileStream fs;
            Bitmap b;
            BitmapData pixi;
            System.IntPtr scano;
            int stride;

            Rectangle rc = new Rectangle(0, 0, dimx, dimy);
            fs = new FileStream(filenames[plano], FileMode.Open, FileAccess.Read);
            b = (Bitmap)Bitmap.FromStream(fs, true, false);
            pixi = b.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            scano = pixi.Scan0;
            stride = pixi.Stride;

            bmp_matrix = null;
            bmp_matrix = new byte[dimx, dimy];

            unsafe
            {
                byte prom;
                byte* p = (byte*)(void*)scano;
                int nOffset = stride - rc.Width * 3;
                int nWidth = rc.Width;

                for (int y = 0; y < rc.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        prom = (byte)(((p[0] + p[1] + p[2])) / 3);
                        bmp_matrix[x, y] = prom;
                        p = p + 3;
                    }
                    p = p + nOffset;
                }
            }
            b.UnlockBits(pixi);
            fs.Close(); fs.Dispose(); b.Dispose();
        }

        /// <summary>
        /// This function paints a little line from the initial mouse pressed coordenate to the 
        /// actual position of the cursor on the picturebox. The result is a line that follows the 
        /// cursor showing the selected region
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_pictureBox1over_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (clicked == true))
            {
                using (Pen mypen = new Pen(Color.DodgerBlue))
                {
                    using (Graphics canvas = panel3_pictureBox1over.CreateGraphics())
                    {
                        panel3_pictureBox1over.Refresh();
                        canvas.DrawLine(mypen, firstx, firsty, e.X, e.Y);
                        mypen.Color = Color.Firebrick;
                        canvas.FillEllipse(mypen.Brush, e.X - 4, e.Y - 4, 8, 8);
                        canvas.FillEllipse(mypen.Brush, firstx - 4, firsty - 4, 8, 8);
                    }
                }
            }
        }

        /// <summary>
        /// This little function refresh the red rectangule dimension on the thumbnail everytime
        /// the main form dimentions change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Form_Resize(object sender, EventArgs e)
        {
            if (dimx > 0 && dimy > 0)
            {
                tx = (panel3_thumb.Image.Width * panel3_panel.HorizontalScroll.Value) / dimx;
                tbx = (panel3_thumb.Image.Width * panel3_panel.HorizontalScroll.LargeChange) / dimx;
                ty = (panel3_thumb.Image.Height * panel3_panel.VerticalScroll.Value) / dimy;
                tby = (panel3_thumb.Image.Height * panel3_panel.VerticalScroll.LargeChange) / dimy;
                Paint_Thumb();
            }
        }

        /// <summary>
        /// This function automatically checks the "Apply a 300% Bilineal Zoom" option if the image dimetions are
        /// below 1000x900. The bilineal zoom improves the quality of the 3D model.
        /// </summary>
        private void Check_Need_Zoom()
        {
            if ((dimx <= 1000) || (dimy <= 900))
                panel4_zoomcheck.Checked = true;
            else
                panel4_zoomcheck.Checked = false;
        }

        /// <summary>
        /// This function implements the panel3 next button event. This button is enabled only if the user
        /// a dendrite, 3 focus regions and 3 diffuse regions. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_next_Click(object sender, EventArgs e)
        {
            if (check_real(panel3_diameter.Text, es) != true)
            {
                MessageBox.Show("The diameter must be a real number higher than zero.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel3_diameter.BackColor = Color.Firebrick;
                return;
            }

            micron = float.Parse(panel3_diameter.Text,es);
            Check_Need_Zoom();
            Change_Step(4);
        }

        /// <summary>
        /// This function validates the given parameters on the Image Processing panel
        /// </summary>
        /// <returns>This function return true if all the parameters are valid, and false otherwise</returns>
        private bool Check_And_Parse_Panel4()
        {
            if (check_integer(panel4_width.Text, es) != true)
            {
                MessageBox.Show("The width value must be a natural number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel4_width.BackColor = Color.Firebrick;
                return false;
            }
            if (check_integer(panel4_height.Text, es) != true)
            {
                MessageBox.Show("The height value must be a natural number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel4_height.BackColor = Color.Firebrick;
                return false;
            }

            if (check_real(panel4_escx.Text, es) != true)
            {
                MessageBox.Show("The scale factor on X must be a real number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel4_escx.BackColor = Color.Firebrick;
                return false;
            }
            if (check_real(panel4_escy.Text, es) != true)
            {
                MessageBox.Show("The scale factor on Y must be a real number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel4_escy.BackColor = Color.Firebrick;
                return false;
            }
            if (check_real(panel4_escz.Text, es) != true)
            {
                MessageBox.Show("The scale factor on Z must be a real number.", "ERROR - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel4_escz.BackColor = Color.Firebrick;
                return false;
            }

            escx = float.Parse(panel4_escx.Text, es);
            escy = float.Parse(panel4_escy.Text, es);
            escz = float.Parse(panel4_escz.Text, es);
            cube_x = int.Parse(panel4_width.Text, es);
            cube_y = int.Parse(panel4_height.Text, es);

            return true;
        }

        /// <summary>
        /// This function implements the next button event on the Image Processing panel. This function is very important
        /// because here are called all the necessary function to generate the 3D model, according to the user given parameters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel4_next_Click(object sender, EventArgs e)
        {
            string path_zoom = null;

            if (Check_And_Parse_Panel4())
            {
                if (panel4_zoomcheck.Checked)
                {
                    folderBrowserDialog1.ShowNewFolderButton = true;
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        path_zoom = folderBrowserDialog1.SelectedPath;
                        panel4_progress_label2.Text = "Running Bilineal Zoom Algorithm...";
                        Zoom z = new Zoom(dimz, ref filenames, path_zoom, ref panel4_progressBar, ref panel4_progress_label, ref panel4_cancel, ref panel4_next);
                        z.Batch_Zoom(dimx, dimy, dimx * 3, dimy * 3);

                        while (z.thread.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        if (z.thread.IsBusy == false)
                        {
                            this.zoom_filenames = z.zoom_filenames;
                            this.dimx = z.dimx;
                            this.dimy = z.dimy;

                            if (zoom_filenames != null & zoom_filenames.Length > 0)
                            {
                                panel4_progress_label2.Text = "Calibrating Parameters...";
                                panel4_progress_label.Text = string.Empty;
                                calib.Calculate_Resolutions(ref zoom_filenames, ref panel4_progressBar, ref panel4_progress_label, ref panel4_cancel, ref panel4_next, this.isolevel, true);

                                while (calib.thread.IsBusy)
                                {
                                    Application.DoEvents();
                                }

                                micronx = calib.micronx;
                                microny = calib.microny;
                                micronz = calib.micronz;
                            
                                panel4_progress_label2.Text = "Running Marching Cubes Algorithm...";
                                mc = new MarchingCubes(dimx, dimy, dimz, isolevel, cube_x, cube_y, ref zoom_filenames, ref panel4_progressBar, ref panel4_progress_label, ref panel4_cancel, ref panel4_next);
                                mc.Generate_Model();

                                while (mc.thread.IsBusy)
                                {
                                    Application.DoEvents();
                                }
                                panel4_progress_label2.Text = "Done!";

                                my_mesh = mc.my_mesh;
                                cajita = mc.cajita;
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                        
                    }
                    folderBrowserDialog1.ShowNewFolderButton = false;
                }
                else
                {
                    panel4_progress_label2.Text = "Calibrating Parameters...";
                    panel4_progress_label.Text = string.Empty;
                    calib.Calculate_Resolutions(ref filenames, ref panel4_progressBar, ref panel4_progress_label, ref panel4_cancel, ref panel4_next, this.isolevel, false);

                    while (calib.thread.IsBusy)
                    {
                        Application.DoEvents();
                    }

                    micronx = calib.micronx;
                    microny = calib.microny;
                    micronz = calib.micronz;
                                      
                    panel4_progress_label2.Text = "Running Marching Cubes Algorithm...";
                    mc = new MarchingCubes(dimx, dimy, dimz, isolevel, cube_x, cube_y, ref filenames, ref panel4_progressBar, ref panel4_progress_label, ref panel4_cancel, ref panel4_next);
                    mc.Generate_Model();

                    while(mc.thread.IsBusy)
                    {
                        Application.DoEvents();
                    }
                    panel4_progress_label2.Text = "Done!";

                    my_mesh = mc.my_mesh;
                    cajita = mc.cajita;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// This function cancels the entire operation. If the model haven't created yet and if there is a thread
        /// running the thread is stopped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel4_cancel_Click(object sender, EventArgs e)
        {
            if (mc != null)
            {
                if(mc.thread.IsBusy)
                   mc.thread.CancelAsync();
                if (calib.thread.IsBusy)
                   calib.thread.CancelAsync();
                panel4_progress_label.Text = string.Empty;
                panel4_progressBar.Value = 0;
                panel4_cancel.Enabled = false;
                panel4_next.Enabled = true;
            }
        }

        #region ImageList Movement
        /// <summary>
        /// This function moves an image plane into the package. The move parameter indicates the number
        /// of times the plane will be move.
        /// </summary>
        /// <param name="move">The number of times the plane will be move</param>
        private void Move_ListItem(int move)
        {
            int index = panel3_listBox.SelectedIndex;
            int destiny = index + move;
            int length = Math.Abs(index-destiny);
            string swap = string.Empty;

            if (length > 1)
            {
                string temp = filenames[index];

                if (index - destiny > 0)
                {
                    for (int i = index; i > destiny; i--)
                    {
                        swap = filenames[i - 1];
                        filenames[i - 1] = filenames[i];
                        filenames[i] = swap;
                    }
                    filenames[destiny] = temp;
                }
                else if (index - destiny < 0)
                {
                    for (int i = index; i < destiny; i++)
                    {
                        swap = filenames[i + 1];
                        filenames[i + 1] = filenames[i];
                        filenames[i] = swap;
                    }
                    filenames[destiny] = temp;
                }
            }
            else
            {
                swap = filenames[index];
                filenames[index] = filenames[destiny];
                filenames[destiny] = swap;
            }
                           
            panel3_listBox.Items.Clear();
            for (int i = 0; i < filenames.Length; i++)
            {
                panel3_listBox.Items.Add(filenames[i].Substring(filenames[i].LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1));
            }

            updown = true;
            panel3_listBox.SelectedIndex = destiny;
        }

        /// <summary>
        /// This function moves the current selected image one step up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_up_Click(object sender, EventArgs e)
        {
            if (panel3_listBox.SelectedIndex - 1 >= 0)
                Move_ListItem(-1);
        }

        /// <summary>
        /// This function moves the current selected image one step down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_down_Click(object sender, EventArgs e)
        {
            if (panel3_listBox.SelectedIndex + 1 < panel3_listBox.Items.Count )
                Move_ListItem(1);
        }

        /// <summary>
        /// This function moves the current selected plane 5 steps up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_upten_Click(object sender, EventArgs e)
        {
            if (panel3_listBox.SelectedIndex - 5 >= 0) 
                Move_ListItem(-5);
        }

        /// <summary>
        /// This function moves the current selected plane 5 steps down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_downten_Click(object sender, EventArgs e)
        {
            if (panel3_listBox.SelectedIndex + 5 < panel3_listBox.Items.Count)
                Move_ListItem(5);
        }
        #endregion ImageList Movement

        /// <summary>
        /// Every time the user clicks on the thumnail's picturebox the main picture box scrolls to that position.
        /// and also the little red rectagle on the thumbnail moves.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_thumbover_MouseClick(object sender, MouseEventArgs e)
        {
            int vx, vy;

            vx = (e.X * dimx) / (panel3_thumb.Image.Width);
            vy = (e.Y * dimy) / (panel3_thumb.Image.Height);

            if (vx > panel3_panel.HorizontalScroll.Maximum)
                vx = panel3_panel.HorizontalScroll.Maximum;
            else if (vx < panel3_panel.HorizontalScroll.Minimum)
                vx = panel3_panel.HorizontalScroll.Minimum;

            if (vy > panel3_panel.VerticalScroll.Maximum)
                vy = panel3_panel.VerticalScroll.Maximum;
            else if (vy < panel3_panel.VerticalScroll.Minimum)
                vy = panel3_panel.VerticalScroll.Minimum;

            panel3_panel.HorizontalScroll.Value = vx;
            panel3_panel.VerticalScroll.Value = vy;

            if (isolevel == -1)
            {
                tx = (panel3_thumb.Width * vx) / dimx;
                tbx = (panel3_thumb.Width * panel3_panel.HorizontalScroll.LargeChange) / dimx;
                ty = (panel3_thumb.Height * vy) / dimy;
                tby = (panel3_thumb.Height * panel3_panel.VerticalScroll.LargeChange) / dimy;
                Paint_Thumb();
            }
        }

        /// <summary>
        /// This function repaints thumbnail and if there is a calulated isolevel value this also repaints 
        /// the margin, everytime the selected image changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_isolist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (panel3_isolist.SelectedIndex >= 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                int.TryParse(panel3_isolist.Items[panel3_isolist.SelectedIndex].ToString(), out isolevel);
                if (isolevel != -1)
                {
                    GetBorderPixels();
                    Paint_Thumb();
                }
                Cursor.Current = Cursors.Arrow;
            }
        }

        /// <summary>
        /// This function implements the reset button event on the Calibration step. This button resets all the 
        /// selected regions and values to empty and also intilizes all the flags and object used on this step.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel3_reset_Click(object sender, EventArgs e)
        {
            iso_obj.Clear();
            calib = null;
            infocus = 0;
            outfoucus = 0;
            isolevel = -1;
            panel3_isolist.Items.Clear();

            panel3_next.Enabled = false;
            panel3_labelinfocus.Text = "0";
            panel3_labelinfocus.ForeColor = Color.Red;
            panel3_focusok.Visible = false;
            panel3_labeloutfocus.Text = "0";
            panel3_labeloutfocus.ForeColor = Color.Red;
            panel3_outfocusok.Visible = false;
            panel3_diameter.Text = string.Empty;
            panel3_single.Visible = false;
        }

    }

}