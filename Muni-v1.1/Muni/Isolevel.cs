using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Text;
using System.IO;

namespace Muni
{
    /// <summary>
    /// This class calculates the isolevel parameter for the Marching Cubes Algorithm
    /// The isolevel value is calculated on the user's selected regions. Remember that there are two kinds of regions
    /// the ones that are in focus and the ones that not.
    /// </summary>
    public class Isolevel
    {
        private List<Punto> beg_line;  //A list of Punto objects. This one stores where the begining of the line starts
        private List<Punto> end_line; //A list of Punto objects. This one stores where the begining of the line ends
        private List<string> plane;   //This string list saves the path of the selected images 
        private byte[,] mymat;        //A byte matrix to store in memory the pixel data of the users selected regions
        private List<bool> focus;     //This list indicates wich selected regions are on focus regions and who are not.
        private int dx,dy;            //This ones are de dimesions of the image package
                  
        /// <summary>
        /// The basic constructor of the class. This initializes several parameters.
        /// </summary>
        public Isolevel()
        {
            beg_line = new List<Punto>();
            end_line = new List<Punto>();
            plane = new List<string>();
            focus = new List<bool>();
        }

        /// <summary>
        /// This one adds a new Line to the class. This fuction recives the start and ending points of the 
        /// line and saves them on the beg_line and end_line lists. This also recives and stores the path
        /// of the selected plane, and also if the line selects a focused region or not.
        /// </summary>
        /// <param name="x1">The x coordenate of the starting point</param>
        /// <param name="y1">The y coordenate of the starting point</param>
        /// <param name="x2">The x coordenate of the ending point</param>
        /// <param name="y2">The y coordenate of the ending point</param>
        /// <param name="plane">This recives the path of the current image plane</param>
        /// <param name="focus">This indicates if the line selects a focused region or not</param>
        public void Add_Line(int x1, int y1, int x2, int y2, string plane, bool focus)
        {
            this.beg_line.Add(new Punto(x1, y1,0.0f));
            this.end_line.Add(new Punto(x2, y2, 0.0f));

            this.plane.Add(plane);
            this.focus.Add(focus);
        }

        /// <summary>
        /// This clears all the data inside the lists of the class
        /// </summary>
        public void Clear()
        {
            beg_line.Clear();
            end_line.Clear();
            plane.Clear();
            focus.Clear();
        }

        /// <summary>
        /// This function calculates the isovalue. Using the 3 lines on focus and 3 lines on the background
        /// this function travels along them and calculates the darkest value on every them. Then the isovalue
        /// its calculated in between the average darkest value of the on focus lines and the smooth lines. 
        /// </summary>
        /// <returns>Rerturns the isolevel value</returns>
        public int Calculate_Isolevel()
        {
            int min_focus, min_outfocus;
            int tmp;
            int w, h;
            float swap;
            Rectangle rc = new Rectangle();
            min_focus = 255; min_outfocus = 255; tmp = 255;

            for (int z = 0; z < this.plane.Count; z++)
            {
                w = (int)(end_line[z].x -  beg_line[z].x);       //X2 - X1 Coords
                h = (int)(end_line[z].y -  beg_line[z].y);       //Y2 - Y1 Coords
                
                
                if (w < 0)
                {
                    swap = end_line[z].x;
                    end_line[z].x = beg_line[z].x;
                    beg_line[z].x = swap;
                }
                if (h < 0)
                {
                    swap = beg_line[z].y;
                    beg_line[z].y = end_line[z].y;
                    end_line[z].y = swap;
                }

                rc.X = (int)beg_line[z].x;
                rc.Y = (int)beg_line[z].y;
                rc.Width = (int)Math.Abs(w);
                rc.Height = (int)Math.Abs(h);

                Fill_Matrix(rc, plane[z]);

                if (this.focus[z] == true)
                {
                    tmp = Calculate_Minimum(beg_line[z].x, beg_line[z].y, end_line[z].x, end_line[z].y );
                    if (tmp < min_focus)
                        min_focus = tmp; 
                }
                else
                {
                    tmp = Calculate_Minimum(beg_line[z].x, beg_line[z].y, end_line[z].x, end_line[z].y);
                    if (tmp < min_outfocus)
                        min_outfocus = tmp;
                }
                
            }

            return (int)((min_focus + min_outfocus) / 2);
        }

        /// <summary>
        /// This function travels along the line, and gets the minimum or darkest value
        /// on the line. The line coordenates are indicated on the function parameters
        /// </summary>
        /// <param name="x1">x start coordenate</param>
        /// <param name="y1">y start coordenate</param>
        /// <param name="x2">x end coordenate</param>
        /// <param name="y2">y en corrdenate</param>
        /// <returns></returns>
        private int Calculate_Minimum(float x1, float y1, float x2, float y2)
        {
            int px, py;
            double tx, ty;
            int min = 255;
            int n;
            double r;

            r = Math.Sqrt(Math.Pow((x2 - x1), 2.0f) + Math.Pow((y2 - y1), 2.0f));
            tx = (x2 - x1) / r;
            ty = (y2 - y1) / r; 
            n = (int)r;

            for (int i = 1; i < n; i++)
            {
                px = (int)(Math.Round(x1 + tx * i));
                py = (int)(Math.Round(y1 + ty * i));

                if (mymat[px, py] < min)
                    min = mymat[px, py];
            }

            return min;
        }

        /// <summary>
        /// This fucntion fill the byte matrix with the pixel data on the provided image.
        /// </summary>
        /// <param name="rc">The region where </param>
        /// <param name="name">The image path were the function gets the data</param>
        private void Fill_Matrix(Rectangle rc, string name)
        {
            FileStream fs = new FileStream(name, FileMode.Open, FileAccess.Read);
            Bitmap b = (Bitmap)Bitmap.FromStream(fs, true, false);
            Rectangle rcx = new Rectangle(0, 0, b.Width, b.Height);

            mymat = null;
            mymat = new byte[b.Width, b.Height];
            dx = b.Width; dy = b.Height;

            BitmapData pixi = b.LockBits(rcx, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            System.IntPtr scano = pixi.Scan0;   //Saca el numero apuntador a la primera casilla de la matriz
            int stride = pixi.Stride;           //Esto saca el ancho de la matriz para recorrer filas

            unsafe
            {
                byte prom;
                byte* p = (byte*)(void*)scano;  //Saca el apuntador a la primera casilla de la matriz
                int nOffset = stride - rcx.Width * 3; //Saca el corrimiento que se tiene que hacer por cada fila
                int nWidth = rcx.Width;   //Se multipicla por que son 3 componentes

                for (int y = 0; y < rcx.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        prom = (byte)(((p[0] + p[1] + p[2])) / 3);
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
