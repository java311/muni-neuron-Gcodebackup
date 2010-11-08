using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;
using System.IO;

namespace Muni
{
    /// <summary>
    /// This class calculates the micronz resolution on the package image 
    /// using the estimated diameter of one dendrite. This class uses one selected region 
    /// </summary>
    public class Calibration
    {
        private int x1, y1, x2, y2;
        private int plane;
        private byte[,] mymat;
        private string[] filenames;
        public int micronx, microny, micronz;
        private int max;
        private int isolevel;
        public BackgroundWorker thread;
        private ProgressBar progressbar;
        private Label label_progress;
        private Button cancel;
        private Button finish;
        
        /// <summary>
        /// This is the default constructor of the class, this constructor only initilizes the 
        /// tread initial parameters.
        /// </summary>
        public Calibration()
        {
            Initalize_BgWorker();
        }

        /// <summary>
        /// This function initilizes all the thread parameters; like the progressbar value. An it also defines the EventHandlers
        /// for the DoWork, ProgressChanged and the RunWorkerCompleted events.
        /// </summary>
        private void Initalize_BgWorker()
        {
            thread = new BackgroundWorker();

            this.thread.WorkerReportsProgress = true;
            this.thread.WorkerSupportsCancellation = true;

            this.thread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.thread_DoWork);
            this.thread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thread_ProgressChanged);
            this.thread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thread_RunWorkerCompleted);
        }

        /// <summary>
        /// This events is called every time the progress percentage is incremented. This function also updates
        /// the progressbar status on the user screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != -1)
                progressbar.PerformStep();
            else
                progressbar.Value = progressbar.Maximum;
        }

        /// <summary>
        /// This event ocurrs when the thread task is completed, or when the thread task is cancelled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                label_progress.Text = string.Empty;
                progressbar.Value = 0;
                cancel.Enabled = false;
                finish.Enabled = true;
                MessageBox.Show("The operation have been cancelled.", "INFO - Muni", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// This function implements the main algorithm. This algorithm estimates the micronz value using the estimated
        /// diameter of one dendrite in microns. This algorithm moves on the Z axis and finds the end and the begining of 
        /// the selected dendrite. Using the number of planes and the estimated dendrite diameter in microns, the micronz 
        /// parameter is estimated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            int up, down;
            int w, h;
            int swap;
            Rectangle rc = new Rectangle();

            up = plane;
            do
            {
                thread.ReportProgress(up);

                if (thread.CancellationPending)//checks for cancel request
                {
                    e.Cancel = true;
                    return;
                }

                w = x2 - x1;   //X2 - X1 Coords
                h = y2 - y1;   //Y2 - Y1 Coords

                if (w < 0)
                {
                    swap = x2;
                    x2 = x1;
                    x1 = swap;
                }
                if (h < 0)
                {
                    swap = y2;
                    y2 = y1;
                    y1 = swap;
                }

                rc.X = x1;
                rc.Y = y1;
                rc.Width = Math.Abs(w);
                rc.Height = Math.Abs(h);

                Fill_Matrix(rc, up);
                up--;
            } while ((Calculate_Diameter(isolevel, 0, 0, rc.Width - 1, rc.Height - 1) == true) && (up >= 0));

            down = plane + 1;
            if (down < filenames.Length)
            {
                do
                {
                    thread.ReportProgress(up);

                    if (thread.CancellationPending)//checks for cancel request
                    {
                        e.Cancel = true;
                        return;
                    }

                    w = x2 - x1;   //X2 - X1 Coords
                    h = y2 - y1;   //Y2 - Y1 Coords

                    if (w < 0)
                    {
                        swap = x2;
                        x2 = x1;
                        x1 = swap;
                    }
                    if (h < 0)
                    {
                        swap = y2;
                        y2 = y1;
                        y1 = swap;
                    }

                    rc.X = x1;
                    rc.Y = y1;
                    rc.Width = Math.Abs(w);
                    rc.Height = Math.Abs(h);

                    Fill_Matrix(rc, down);

                    down++;
                } while ((Calculate_Diameter(isolevel, 0, 0, rc.Width - 1, rc.Height - 1) == true) && (down < filenames.Length));
            }
            
            micronx = max;
            microny = max;
            micronz = down - up;
            thread.ReportProgress(-1);
        }

        /// <summary>
        /// This function add a line and a plane number into the class properties.
        /// </summary>
        /// <param name="x1">The begining x coordenate</param>
        /// <param name="y1">The begining y coordenate</param>
        /// <param name="x2">The end x coordenate</param>
        /// <param name="y2">The end y coordenate</param>
        /// <param name="plane">The selection plane number</param>
        public void Add_Line(int x1, int y1, int x2, int y2, int plane)
        {
            this.x1 = x1; this.x2 = x2;
            this.y1 = y1; this.y2 = y2;
            this.plane = plane;
        }

        /// <summary>
        /// This function wraps the entire implementation of the class to the outside world. This function needs several
        /// initial parameters like the filenames array, the isolevel value, and several object to show the task progress.
        /// </summary>
        /// <param name="filenames">A string array with the filenames of the image package</param>
        /// <param name="progressbar">A progressbar object to show the task progress</param>
        /// <param name="label_progress">A label object to show the current working plane number</param>
        /// <param name="cancel">A cancel button object from the Image_Form</param>
        /// <param name="finish">A finish button object fromt the Image Form</param>
        /// <param name="isolevel">The calculate isolevel value</param>
        /// <param name="zoom">This boolean image package have alredy been zoomed or not</param>
        public void Calculate_Resolutions(ref string[] filenames, ref ProgressBar progressbar, ref Label label_progress, ref Button cancel, ref Button finish, int isolevel, bool zoom)
        {
            this.filenames = filenames;
            this.filenames = filenames;
            this.progressbar = progressbar;
            this.label_progress = label_progress;
            this.cancel = cancel;
            this.finish = finish;

            progressbar.Minimum = 0;
            progressbar.Maximum = filenames.Length + 1;
            progressbar.Value = 0;
            this.isolevel = isolevel;

            if (zoom)
            {
                x1 = x1 * 3; x2 = x2 * 3;
                y1 = y1 * 3; y2 = y2 * 3;
            }

            thread.RunWorkerAsync();
        }

        /// <summary>
        /// This function calculates the dendrite diameter, using isolevel value to determine the dendrite diameter in 
        /// pixels, and then translate it into microns.
        /// </summary>
        /// <param name="isolevel">The isolevel value, wich is the margin value between the neuron and the background</param>
        /// <param name="x1">The x coordenate of the line beginig</param>
        /// <param name="y1">The y coordenate of the line beginig</param>
        /// <param name="x2">The x coordenate of the line ending</param>
        /// <param name="y2">The y coordenate of the line ending</param>
        /// <returns>On error this function returns false and true otherwise</returns>
        private bool Calculate_Diameter(int isolevel, int x1, int y1, int x2, int y2)
        {
            int px1, py1;
            int px2, py2;
            int ix, iy;
            int fx, fy;
            double tx, ty;
            double r;
            int n;
            bool first = false;
            bool last = false;
            ix = iy = fx = fy = 0;
            
            r = Math.Sqrt(Math.Pow((x2 - x1), 2.0f) + Math.Pow((y2 - y1), 2.0f));
            tx = (x2 - x1) / r;
            ty = (y2 - y1) / r;
            n = (int)r;
            
            for (int i = 1; i < n-1; i++)
            {
                px1 = (int)(Math.Round(x1 + tx * i));
                py1 = (int)(Math.Round(y1 + ty * i));

                px2 = (int)(Math.Round(x1 + tx * (i + 1)));
                py2 = (int)(Math.Round(y1 + ty * (i + 1)));

                if ((mymat[px1, py1] <= isolevel) && (first == false))
                {
                    ix = px2; iy = py2;
                    first = true;
                }
                if (mymat[px1, py1] <= isolevel)
                {
                    fx = px1; fy = py1;
                    last = true;
                }
            }

            if (first && last)
            {
                r = Math.Sqrt(Math.Pow((fx - ix), 2.0f) + Math.Pow((fy - iy), 2.0f));
                n = (int)r;

                if (max < n)
                { max = n; }

                if (n > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// This function uses the selected user region and the image data to load on the memory a byte matrix
        /// with the image data on the selected region. 
        /// </summary>
        /// <param name="rc">This rectagule defines the user's selected region</param>
        /// <param name="plane">This is the number of the plane where the selection ocurred</param>
        private void Fill_Matrix(Rectangle rc, int plane)
        {
            FileStream fs = new FileStream(filenames[plane], FileMode.Open, FileAccess.Read);
            Bitmap b = (Bitmap)Bitmap.FromStream(fs, true, false);

            mymat = null;
            mymat = new byte[rc.Width, rc.Height];

            BitmapData pixi = b.LockBits(rc, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            System.IntPtr scano = pixi.Scan0;   //Saca el numero apuntador a la primera casilla de la matriz
            int stride = pixi.Stride;           //Esto saca el ancho de la matriz para recorrer filas

            unsafe
            {
                byte prom;
                byte* p = (byte*)(void*)scano;  //Saca el apuntador a la primera casilla de la matriz
                int nOffset = stride - rc.Width * 3; //Saca el corrimiento que se tiene que hacer por cada fila
                int nWidth = rc.Width;   //Se multipicla por que son 3 componentes

                for (int y = 0; y < rc.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        prom = (byte)(((p[0] + p[1] + p[2])) / 3);
                        //p[0] = 0; p[1] = 0; p[2] = 0;
                        mymat[x,y] = prom;
                        p = p + 3;
                    }
                    p = p + nOffset; //Se recorre una fila hacia abajo
                }
            }
            
            b.UnlockBits(pixi);
        }
    }
}
