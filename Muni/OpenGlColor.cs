using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;



namespace Muni
{
    public class OpenGlColor
    {
        public byte R;
        public byte G;
        public byte B;

        public OpenGlColor()
        {
            this.R = 0; this.G = 0; this.B = 0;
        }

        public OpenGlColor(byte R, byte G, byte B)
        {
            this.R = R; this.G = G; this.B = B;
        }
    }
}
