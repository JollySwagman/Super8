
using System;
using System.Drawing;
//using AForge.Imaging;

namespace FilmScanner
{
    public class Frame 
    {

        /// <summary>
        /// Scanner result for this frame
        /// </summary>
        public FrameResultType Result { get; set; }

        public Image Image { get; set; }

        public int Index { get; set; }

        public TimeSpan CaptureTime { get; set; }

        public void Save(string filename)
        {
            this.Image.Save(filename);
        }

    }

}
