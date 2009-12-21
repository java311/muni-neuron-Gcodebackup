using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.IO;
using Tao.OpenGl;
using System.ComponentModel;


namespace Muni
{
    public class Mesh
    {
        private System.Globalization.CultureInfo es = new System.Globalization.CultureInfo("es-MX");

        public float[, ,] puntos;
        private float[, ,] normales;
        public float escx, escy, escz;
        public int objetive;
        public int dimx, dimy, dimz;
        public float micronx, microny, micronz, micron;
        public OpenGlColor [] colores;
        private int max;

        public Mesh() { }
        
        public Mesh(StreamReader sr, int trian_count, int dimx, int dimy, int dimz)
        {
            puntos = new float[trian_count, 3, 3];
            normales = new float[trian_count, 3, 3];
            max = 0;
            this.dimx = dimx;
            this.dimy = dimy;
            this.dimz = dimz;
            int p = 0;
            int t = 0;
            string tmp;
            string[] line;

            while ((tmp = sr.ReadLine()) != null)
            {
                line = tmp.Split(',');
                float.TryParse(line[0], NumberStyles.Float, es, out puntos[t, p, 0]);
                float.TryParse(line[1], NumberStyles.Float, es, out puntos[t, p, 1]);
                float.TryParse(line[2], NumberStyles.Float, es, out puntos[t, p, 2]);
                float.TryParse(line[3], NumberStyles.Float, es, out normales[t, p, 0]);
                float.TryParse(line[4], NumberStyles.Float, es, out normales[t, p, 1]);
                float.TryParse(line[5], NumberStyles.Float, es, out normales[t, p, 2]);

                if (p == 2) 
                { 
                    p = 0;
                    t = t + 1;
                    max = max + 1;
                } 
                else { p++; }
            }
        }

       

        public bool Read_File(FileStream file, ToolStripProgressBar progress)
        {
            string tmp;
            string [] line;
            int current = 0;
            int linecount = 0;
            bool resp = false;

            using (StreamReader sr = new StreamReader(file.Name))
            {
                while ((tmp = sr.ReadLine()) != null)
                {
                    linecount++;
                }
            }
            progress.Value = 0;
            progress.Maximum = linecount;

            using (StreamReader sr = new StreamReader(file))
            {
                 tmp = sr.ReadLine(); progress.PerformStep();
                 resp = int.TryParse(tmp, NumberStyles.Integer, es, out max);

                 tmp = sr.ReadLine(); progress.PerformStep();
                 line = tmp.Split(',');
                 if (line.Length != 3)
                 {
                     MessageBox.Show("Invalid number of parameters in scale factors. ", "Error - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return false;
                 }
                 resp = float.TryParse(line[0], NumberStyles.Float, es, out escx);
                 resp = float.TryParse(line[1], NumberStyles.Float, es, out escy);
                 resp = float.TryParse(line[2], NumberStyles.Float, es, out escz);

                 tmp = sr.ReadLine(); progress.PerformStep();
                 line = tmp.Split(',');
                 if (line.Length != 3)
                 {
                     MessageBox.Show("Invalid number of parameters in dimensions. " , "Error - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return false;
                 }
                 resp = int.TryParse(line[0], NumberStyles.Integer, es, out dimx);
                 resp = int.TryParse(line[1], NumberStyles.Integer, es, out dimy);
                 resp = int.TryParse(line[2], NumberStyles.Integer, es, out dimz);

                 tmp = sr.ReadLine(); progress.PerformStep();
                 line = tmp.Split(',');
                 if (line.Length != 5)
                 {
                     MessageBox.Show("Invalid number of parameters in micron scale. ", "Error - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return false;
                 }
                 resp = float.TryParse(line[0], NumberStyles.Integer, es, out micronx);
                 resp = float.TryParse(line[1], NumberStyles.Integer, es, out microny);
                 resp = float.TryParse(line[2], NumberStyles.Integer, es, out micronz);
                 resp = float.TryParse(line[3], NumberStyles.Integer, es, out micron);
                 resp = int.TryParse(line[4], NumberStyles.Integer, es, out objetive);
                 
                 if (resp == false)
                 {
                     MessageBox.Show("The header file has a wrong format." , "Error - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return false;
                 }

                 puntos = new float[max, 3, 3];
                 normales = new float[max, 3, 3];
                 colores = new OpenGlColor[4];
                 colores[0] = new OpenGlColor();
                 colores[1] = new OpenGlColor();
                 colores[2] = new OpenGlColor();
                 colores[3] = new OpenGlColor();
                 
                 for (int i = 0; i < 4; i++)
                 {
                     tmp = sr.ReadLine(); progress.PerformStep();
                     line = tmp.Split(',');

                     resp = byte.TryParse(line[0], NumberStyles.Integer, es, out colores[i].R);
                     resp = byte.TryParse(line[1], NumberStyles.Integer, es, out colores[i].G);
                     resp = byte.TryParse(line[2], NumberStyles.Integer, es, out colores[i].B);
                 }

                 if (resp == false)
                 {
                     MessageBox.Show("The colors have a wrong format.", "Error - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return false;
                 }

                 while ((tmp = sr.ReadLine()) != null) 
                 {
                     progress.PerformStep();
                     line = tmp.Split(','); 

                     if (line.Length != 18)
                     {
                         MessageBox.Show("The number of coordenates or normals is wrong in the triangle #" + current.ToString(), "Error - Muni", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         return false;
                     }
                     
                     float.TryParse(line[0], NumberStyles.Float, es, out puntos[current, 0, 0]);
                     float.TryParse(line[1], NumberStyles.Float, es, out puntos[current, 0, 1]);
                     float.TryParse(line[2], NumberStyles.Float, es, out puntos[current, 0, 2]);
                     float.TryParse(line[3], NumberStyles.Float, es, out puntos[current, 1, 0]);
                     float.TryParse(line[4], NumberStyles.Float, es, out puntos[current, 1, 1]);
                     float.TryParse(line[5], NumberStyles.Float, es, out puntos[current, 1, 2]);
                     float.TryParse(line[6], NumberStyles.Float, es, out puntos[current, 2, 0]);
                     float.TryParse(line[7], NumberStyles.Float, es, out puntos[current, 2, 1]);
                     float.TryParse(line[8], NumberStyles.Float, es, out puntos[current, 2, 2]);

                     float.TryParse(line[9], NumberStyles.Float, es, out normales[current, 0, 0]);
                     float.TryParse(line[10], NumberStyles.Float, es, out normales[current, 0, 1]);
                     float.TryParse(line[11], NumberStyles.Float, es, out normales[current, 0, 2]);
                     float.TryParse(line[12], NumberStyles.Float, es, out normales[current, 1, 0]);
                     float.TryParse(line[13], NumberStyles.Float, es, out normales[current, 1, 1]);
                     float.TryParse(line[14], NumberStyles.Float, es, out normales[current, 1, 2]);
                     float.TryParse(line[15], NumberStyles.Float, es, out normales[current, 2, 0]);
                     float.TryParse(line[16], NumberStyles.Float, es, out normales[current, 2, 1]);
                     float.TryParse(line[17], NumberStyles.Float, es, out normales[current, 2, 2]);


                 current++;
                 }

                 return true;        
            }
        }

        public bool Save_File(FileStream file, OpenGlColor [] colors, string escx, string escy, string escz, string micronx, string microny, string micronz, string micron, string objetive, ToolStripProgressBar progress)
        {
            StringBuilder sbt = new StringBuilder();
            StringBuilder sbn = new StringBuilder();
            string tmp;

            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(max.ToString(es));
                tmp = escx + ',' + escy + ',' + escz;
                sw.WriteLine(tmp);

                tmp = string.Empty;
                tmp = dimx.ToString(es) + ',' + dimy.ToString(es) + ',' + dimz.ToString(es);
                sw.WriteLine(tmp);

                tmp = string.Empty;
                tmp = micronx.ToString(es) + ',' + microny.ToString(es) + ',' + micronz.ToString(es) + "," + micron.ToString(es) + "," + objetive.ToString(es);
                sw.WriteLine(tmp);
                
                foreach (OpenGlColor cl in colors)
                {
                    sbt.Append(cl.R); sbt.Append(','); sbt.Append(cl.G); sbt.Append(','); sbt.Append(cl.B);
                    sw.WriteLine(sbt);
                    sbt.Length = 0;  
                }
                
                int t,p,c;

                progress.Value = 0;
                progress.Maximum = max;
                for (t=0; t<max ;t++)
                {
                    progress.PerformStep();
                    for (p=0; p<3 ;p++)
                    {
                        for (c=0; c<3 ;c++)
                        {
                            sbt.Append(puntos[t, p, c].ToString("0.00", es));
                            sbt.Append(',');
                        
                            sbn.Append(normales[t, p, c].ToString("0.00", es));
                            sbn.Append(',');
                        }
                    }
                    
                    sbn.Remove(sbn.Length - 1, 1);
                    sbt.Append(sbn);

                    sw.WriteLine(sbt.ToString());
                    sbt.Length = 0;
                    sbn.Length = 0;
                }
            }

            return true;
        }

        public void Type(bool triangulos, bool lineas, bool pts)
        {
            if (triangulos)
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            else if (lineas)
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
            else if (pts)
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_POINT);
        }

        public void Draw()
        {
            Gl.glNormalPointer(Gl.GL_FLOAT, 0, normales);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, puntos);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, max * 3);
        }

    }
}
