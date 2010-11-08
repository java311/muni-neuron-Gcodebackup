using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace Muni
{
    /// <remarks>
    /// This class only implements a Bilineal Zooming filter on batch. This takes several file paths on a 
    /// string array and saves the result on another path. If the same path is selected then all the original
    /// images are overwriten.
    /// </remarks>
    class Zoom
    {
     
        private byte[,] pixels1;  //A byte matrix to store the original bitmap on memory
        private byte[,] pixels2;  //A byte matrix to store the resulting bitmap 

        private ProgressBar progressbar;   //A progressbar objecto to indicate the task progress
        private Label label_progress;   //A label object to indicate the current plane number
        private Button cancel;   //The cancel button from the Image_Form, to control the cancel event
        private Button finish;   //The finish button from the Image_Form, to disable the button during the task
        public BackgroundWorker thread;   //The BackgroundWorker to implement the thread
        public int dimx, dimy, dimz;   //The image package dimentions
        private string[] filenames;    //A filename array with all the images' paths
        private int anchoi, altoi, ancho, alto;   //The original and the desired picture dimentions
        private string zoom_path;    //Where all the resulting images will be saved

        public string[] zoom_filenames; //This array stores the resulting images paths
        
        /// <summary>
        /// This is the constructor of the Zoom class
        /// </summary>
        /// <param name="dimz">The number of planes on the image package</param>
        /// <param name="filenames">A string array with all the image paths</param>
        /// <param name="zoom_path">A string path were all the resulting images will be save</param>
        /// <param name="progressbar">A progressbar objecto to indicate the task progress</param>
        /// <param name="label_progress">A label object to indicate the current working plane</param>
        /// <param name="cancel">This is the cancel button on the Image_Form</param>
        /// <param name="finish">This is the finish button on the Image_Form</param>
        public Zoom(int dimz, ref string[] filenames, string zoom_path, ref ProgressBar progressbar, ref Label label_progress, ref Button cancel, ref Button finish)
        {
            this.dimz = dimz;
            this.filenames = filenames;
            this.zoom_path = zoom_path;
            zoom_filenames = new string[filenames.Length];
            
            this.progressbar = progressbar;
            this.label_progress = label_progress;
            this.cancel = cancel;
            this.finish = finish;
            Initalize_BgWorker();
        }

        /// <summary>
        /// This function initilizes the thread parameters before appliying the bath zoom filter
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
        /// This function loads a Bitmap object into the memory, on a two dimentional byte matrix.
        /// First this recives the path of the image on the filename parameter, and then the image is 
        /// converted into Bitmap object, converted into a byte matrix. And then the resulting matrix
        /// is loaded into the main memory.
        /// </summary>
        /// <param name="pixels">This is the resulting byte matrix, passed by reference</param>
        /// <param name="filename">The path of the image</param>
        public void ImgToMat(ref byte [,] pixels, string filename)
        {
            BitmapData pixi;
            System.IntPtr scano;
            int stride;
            byte prom;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (Bitmap b = (Bitmap)Bitmap.FromStream(fs, true, false))
                {
                    pixi = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    scano = pixi.Scan0;   //Saca el numero apuntador a la primera casilla de la matriz
                    stride = pixi.Stride;           //Esto saca el ancho de la matriz para recorrer filas

                    unsafe
                    {
                        //int nVal;
                        byte* p = (byte*)(void*)scano;  //Saca el apuntador a la primera casilla de la matriz
                        int nOffset = stride - b.Width * 3; //Saca el corrimiento que se tiene que hacer por cada fila
                        int nWidth = b.Width;   //Se multipicla por que son 3 componentes

                        for (int y = 0; y < b.Height; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                prom = (byte)(((p[0] + p[1] + p[2])) / 3);
                                pixels[x, y] = prom;
                                p = p + 3;
                            }
                            p = p + nOffset; //Se recorre una fila hacia abajo
                        }
                    }
                    b.UnlockBits(pixi);
                }
                fs.Close();
            }
        }

        /// <summary>
        /// This function recives a two dimentional matrix and a bitmap object, both by reference.
        /// With this data this function converts the data on the byte matrix into a bitmap object.
        /// </summary>
        /// <param name="pixels">The byte matrix data stored on the main memory</param>
        /// <param name="b">The resulting bitmap object</param>
        public void MatToImg(ref byte [,] pixels, ref Bitmap b)
        {
            BitmapData pixi = b.LockBits(new Rectangle(0, 0, b.Width , b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            System.IntPtr  scano = pixi.Scan0;   //Saca el numero apuntador a la primera casilla de la matriz
            int stride = pixi.Stride; //Esto saca el ancho de la matriz para recorrer filas

            unsafe
            {
                //int nVal;
                byte* p = (byte*)(void*)scano;  //Saca el apuntador a la primera casilla de la matriz
                int nOffset = stride - b.Width * 3; //Saca el corrimiento que se tiene que hacer por cada fila
                int nWidth = b.Width;   //Se multipicla por que son 3 componentes

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        p[0] = pixels[x, y];
                        p[1] = pixels[x, y];
                        p[2] = pixels[x, y];
                        p = p + 3;
                    }
                    p = p + nOffset; //Se recorre una fila hacia abajo
                }
            }
            b.UnlockBits(pixi);
        }

        
        /// <summary>
        /// This function implements a Bilinear Zoom, to do it this recibes a bitmap by reference and the new
        /// desired bitmap dimentions. Then the function returns a Bitmap object as result.
        /// </summary>
        /// <param name="anchoi">The original iamge width</param>
        /// <param name="altoi">The original image heigth</param>
        /// <param name="ancho">The desired width for the resulting image</param>
        /// <param name="alto">The desired heigth for the resulting image</param>
        private void Bilineal_Zoom(int anchoi, int altoi, int ancho, int alto)
        {
            float yy, xx;
            int l, k;
            int l1, k1;
            float by, bx, ax, ay;
            float f1, f2, f3, f4;

            float cx = (float)(anchoi-1) / (float)(ancho-1);
            float cy = (float)(altoi-1) / (float)(alto-1);

            for (int yp = 0; yp < alto-1; yp++)
            {
                yy = cy * (float)yp;
                l = (int)yy;
                by = yy - l;
                ay = 1 - by;
                l1 = l + 1;
                
                for (int xp = 0; xp < ancho-1; xp++)
                {
                    xx = cx * (float)xp;
                    k = (int)xx;
                    bx = xx - k;
                    ax = 1 - bx;
                    k1 = k + 1;
                    f1 = (ax * ay); f2 = (bx * ay); f3 = (ax * by); f4 = (bx * by);

                    pixels2[xp, yp] = (byte)((f1 * (pixels1[k, l])) + (f2 * (pixels1[k1, l])) + (f3 * (pixels1[k, l1])) + (f4 * (pixels1[k1, l1])));
                }
            }
        }

        /// <summary>
        /// This function implements the entire process to zoom an imgage. First the original image data is loaded
        /// into the main memory and another byte matrix is created to store the result. Then the Bilineal Zoom is applied
        /// to the original image and then the result stored on the memory is converted into an image. The only way 
        /// to call this function is using the Batch_Zoom wrapper function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            int z;
            string new_file_name;
            //pixels1 will have the data of the original image
            pixels1 = new byte[anchoi, altoi];
            //and pixels2 will have the data of the resulting zoom image. 
            pixels2 = new byte[ancho, alto];
            Bitmap resultado;

            for (z = 0; z < dimz; z = z + 1)
            {
                thread.ReportProgress(z + 1);

                if (thread.CancellationPending)//checks for cancel request
                {
                    e.Cancel = true;
                    return;
                }

                //Creates a new blank bitmap
                resultado = new Bitmap(ancho, alto);

                //Converts an image bitmap into a bidimensional matrix. (Converts the bitmap into grayscale)
                ImgToMat(ref pixels1, this.filenames[z]);

                //Applies a bilineal zoom using the info on pixels1 and puting the result on pixels2.
                Bilineal_Zoom(anchoi, altoi, ancho, alto);

                //Converts a bidimensional byte array into a bitmap. (On grayscale)
                MatToImg(ref pixels2, ref resultado);

                //Saves the resulting bitmap on the user indicated path. (Overwrittes if another image exist)
                new_file_name = zoom_path + Path.DirectorySeparatorChar + (filenames[z].Substring(filenames[z].LastIndexOf(System.IO.Path.DirectorySeparatorChar) +1 ));

                if (File.Exists(new_file_name))
                {
                    File.Delete(new_file_name);
                }

                resultado.Save(new_file_name);
                this.zoom_filenames[z] = new_file_name;
            }
            resultado = null;
        }

        /// <summary>
        /// This funcition is called everytime the progressbar value changes. This function refresh the 
        /// status label with current plane number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label_progress.Text = progressbar.Value.ToString() + " of " + progressbar.Maximum.ToString();
            progressbar.PerformStep();
        }

        /// <summary>
        /// This function is called when the operation is cancel or the thread finish the work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                zoom_filenames = null;

                label_progress.Text = string.Empty;
                progressbar.Value = 0;
                cancel.Enabled = false;
                finish.Enabled = true;

                MessageBox.Show("The bilineal zoom operation have been cancelled.", "INFO - Muni", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dimx = ancho;
                dimy = alto;
            }                
        }

        /// <summary>
        /// This function graps the entire functionality of the Zoom class. This applies the zoom filter to
        /// all the images on the filenames[] array.
        /// </summary>
        /// <param name="anchoi">The original width of the pictures</param>
        /// <param name="altoi">The original heigth of the pictures</param>
        /// <param name="ancho">The desired width for the resulting image</param>
        /// <param name="alto">The desired heigth for the resulting image</param>
        /// <returns>If an error ocurrs the funtion returns false and true otherwise</returns>
        public bool Batch_Zoom(int anchoi, int altoi, int ancho, int alto)
        {
            label_progress.Text = string.Empty;
            progressbar.Minimum = 1;
            progressbar.Value = 1;
            progressbar.Maximum = dimz;
            cancel.Enabled = true;
            finish.Enabled = false;
            this.anchoi = anchoi;
            this.altoi = altoi;
            this.ancho = ancho;
            this.alto = alto;

            thread.RunWorkerAsync();
            return true;
        }

    }
}
