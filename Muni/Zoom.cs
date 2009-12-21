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
    /// Clase que maneja  las funciones de todos los filtros ya sean de combolucion o puntuales
    /// Por lo general las funciones solo reciben un objeto de tipo Bitmap por referencia
    /// Y le aplican el filtro correspondiente. 
    /// </remarks>
    class Zoom
    {
        // Matrices de flotantes que guardadan el bitmap original (pixels1) y el resultado del filtro (pixels2)
        private byte[,] pixels1;
        private byte[,] pixels2;

        private ProgressBar progressbar;
        private Label label_progress;
        private Button cancel;
        private Button finish;
        public BackgroundWorker thread;
        public int dimx, dimy, dimz;
        private string[] filenames;
        private int anchoi, altoi, ancho, alto;
        private string zoom_path;

        public string[] zoom_filenames;
        
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

        private void Initalize_BgWorker()
        {
            thread = new BackgroundWorker();

            this.thread.WorkerReportsProgress = true;
            this.thread.WorkerSupportsCancellation = true;

            this.thread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.thread_DoWork);
            this.thread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thread_ProgressChanged);
            this.thread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thread_RunWorkerCompleted);
        }

        // Esta funion recibe una matriz real de 3 dimensiones y un bitmap ambos por referencia
        // Y lo unico que hace es copiar la imformaciòn contenida en el bitmap en la matriz de pixeles
        // Esto con el fin de que la implementacion de algunos filtros se vuelva mas facil
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

        // Recibe una matriz real de 3 dimensiones y un objeto bitmap ambos por referencia. 
        // Lo unico que hace es copiar el contenido de la matriz de reales en el Bitmap pasado por refencia. 
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

        // Realiza el zoom Bilinear, para hacerlo recibe el bitmap por refencia y las nuevas dimensiones
        // deseadas para el ancho y el alto. El resultado del zoom bilinear se devuelve en el objeto Bitmap
        // que se devuelve en el return 
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

        private void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label_progress.Text = progressbar.Value.ToString() + " of " + progressbar.Maximum.ToString();
            progressbar.PerformStep();
        }

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
