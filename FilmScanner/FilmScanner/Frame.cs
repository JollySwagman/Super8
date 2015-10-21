
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

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




        public static Image GetTestFrame(string message, bool flip)
        {

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap(640, 480);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(Color.Black);

            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Brown);

            drawing.DrawString(message, new Font("Tahoma", 18), textBrush, 10, 10);

            drawing.Save();

            // Once the drawing has been saved into img.... flip it if required
            if (flip)
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            textBrush.Dispose();
            drawing.Dispose();

            //img.Save("Frame_" + DateTime.Now.Ticks + ".bmp");
            //img.Save("Frame_" + DateTime.Now.Ticks + ".jpg", ImageFormat.Jpeg);

            var memStream = new MemoryStream();
            img.Save(memStream, ImageFormat.Jpeg);
            
            //return memStream.ToArray();

            var result = (Image)img.Clone();

            img.Dispose();

            return result;

        }

    }

}
