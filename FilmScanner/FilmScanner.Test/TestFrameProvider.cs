
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class TestFrameProvider : IFrameProvider
    {

        Image IFrameProvider.CaptureFrame()
        {
            return GetTestFrame("CaptureFrame()");
        }

        public static Image GetTestFrame(string message)
        {
            return GetTestFrame(message, false);
        }

        /// <summary>
        /// Simulated delay..
        /// </summary>
        //public TimeSpan Delay { get; set; }

        public static Image GetTestFrame(string message, bool flip)
        {

            // Create a dummy bitmap just to get a graphics object
            //Image img1 = new Bitmap(1, 1);
            //Graphics drawing1 = Graphics.FromImage(img1);
            //img1.Dispose();
            //drawing1.Dispose();

            Image result = null;

            //create a new image of the right size
            using (var img = new Bitmap(640, 480))
            {
                var drawing = Graphics.FromImage(img);

                //paint the background
                drawing.Clear(Color.Black);

                //create a brush for the text
                Brush textBrush = new SolidBrush(Color.Yellow);

                drawing.DrawString(message, new Font("Tahoma", 32), textBrush, 30, 30);

                // flip it if required
                if (flip)
                {
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }

                textBrush.Dispose();
                drawing.Dispose();

                // return a clone of the image to avoid contention???
                result = (Image)img.Clone();
            }

            System.Threading.Thread.Sleep(2+000);

            return result;

        }

    }
}