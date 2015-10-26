
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

        public void Save(string filename)
        {
            this.Image.Save(filename);
        }

        public static Image GetTestFrame2(string message, bool flip)
        {
            Bitmap flag = new Bitmap(640, 480);
            Graphics flagGraphics = Graphics.FromImage(flag);
            int red = 0;
            int white = 11;
            while (white <= 100)
            {
                flagGraphics.FillRectangle(Brushes.Red, 0, red, 200, 10);
                flagGraphics.FillRectangle(Brushes.White, 0, white, 200, 10);
                red += 20;
                white += 20;
            }

            return flag;
        }

        public static Image GetTestFrame(string message)
        {
            return GetTestFrame(message, false);
        }

        public static Image GetTestFrame(string message, bool flip)
        {

            //first, create a dummy bitmap just to get a graphics object
            Image img1 = new Bitmap(1, 1);
            Graphics drawing1 = Graphics.FromImage(img1);

            //free up the dummy image and old graphics object
            img1.Dispose();
            drawing1.Dispose();

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

            return result;

        }

    }

}
