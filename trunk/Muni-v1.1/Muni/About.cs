using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Muni
{
    /// <summary>
    /// This class justs implements the About screen
    /// </summary>
    public partial class About : Form
    {
        private List<string> credits;
        int ix, iy;

        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            credits.Add(credits[0]);
            credits.RemoveAt(0);
            about_textbox.Lines = (string[]) credits.ToArray();
        }

        private void About_Load(object sender, EventArgs e)
        {
            credits = new List<string>(16);

            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add("    ***************************");
            credits.Add("    *\t\t    *");
            credits.Add("    *         MUNI              *");
            credits.Add("    *      version 1.1        *");
            credits.Add("    *\t\t    *");
            credits.Add("    ***************************");
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);

            credits.Add("    Developed for:");
            credits.Add(string.Empty);
            credits.Add("    Autonomous Univesity");
            credits.Add("          of Puebla");
            credits.Add(string.Empty);
            credits.Add("    Neuropsychiatry Laboratory");
            credits.Add("    in the Physiology Institute");
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            
            credits.Add("    By the wise Guidance of:");
            credits.Add(string.Empty);
            credits.Add("    Phd. Manuel Martín Ortiz");
            credits.Add(string.Empty);
            credits.Add("    Phd. Gonzalo Flores Alvarez");
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);

            credits.Add("    Extra Credits for:");
            credits.Add(string.Empty);
            credits.Add("    M.Sc. Gloria Torrealba");
            credits.Add("    Meléndez");
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);

            credits.Add("    Special Thanks to:");
            credits.Add(string.Empty);
            credits.Add("    My family, for all their");
            credits.Add("    support and love");
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add("    And for all the human beings ");
            credits.Add("    who gave me a live lesson,");
            credits.Add("    to be a better person.");
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);

            credits.Add("             THANKS            ");

            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);
            credits.Add(string.Empty);


            timer1.Enabled = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left))
            {
                this.Left = this.Left + (e.X - ix);
                this.Top =  this.Top +  (e.Y - iy);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ix = e.X;
            iy = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}