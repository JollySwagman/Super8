using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class ConversionHelpers
    {

        private static byte[] BitmapToByteArray(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.MemoryBmp);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }


        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        //private byte[] ConvertBitmap()
        //{

        //    // and buffer of appropriate size for storing its bits
        //    var buffer = new byte[stream.Width * stream.Height * 4];

        //    // Now copy bits from bitmap to buffer
        //    var bits = bitmap.LockBits(new Rectangle(0, 0, stream.Width, stream.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
        //    Marshal.Copy(bits.Scan0, buffer, 0, buffer.Length);
        //    bitmap.UnlockBits(bits);

        //    // and flush buffer to encoding stream
        //    encodingStream.WriteFrame(true, buffer, 0, buffer.Length);

        //}

    }
}