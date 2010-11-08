using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace Muni
{
    /// <summary>
    /// This class is a little data structure to represent the tree color components.
    /// It is used to represent the color of the background, the neuron, the container box
    /// and it is also serialized to be able to save it on binary format.
    /// </summary>
    [Serializable()]
    public class OpenGlColor: ISerializable
    {
        public byte R;
        public byte G;
        public byte B;

        /// <summary>
        /// Constructor
        /// </summary>
        public OpenGlColor()
        {
            this.R = 0; this.G = 0; this.B = 0;
        }

        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="R">Red component in byte format</param>
        /// <param name="G">Green component in byte format</param>
        /// <param name="B">Blue component in byte format</param>
        public OpenGlColor(byte R, byte G, byte B)
        {
            this.R = R; this.G = G; this.B = B;
        }

        /// <summary>
        /// This Constructor is only for the ISerializable interface
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="ctxt">StremingContext</param>
        public OpenGlColor(SerializationInfo info, StreamingContext ctxt)
        {
            this.R = (byte)info.GetValue("Red",typeof(byte));
            this.G = (byte)info.GetValue("Green", typeof(byte));
            this.B = (byte)info.GetValue("Blue", typeof(byte));
        }

        /// <summary>
        /// The GetObjectData implementation for the ISerializable interface
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Red", this.R);
            info.AddValue("Green", this.G);
            info.AddValue("Blue", this.B);
        }
    }
}
