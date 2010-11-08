using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Muni
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Application.Run(new Form1(args[0]));
            }
            else
                Application.Run(new Form1(null));
        }
    }
}