
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using SharpAvi.Output;
using SharpAvi;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FilmScanner
{

    /// <summary>
    /// Converts a collection of frames to a video
    /// </summary>
    /// <see cref="https://sharpavi.codeplex.com/"/>
    public class Video
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workFolder"></param>
        /// <param name="outputFile"></param>
        public static void CreateVideoFromFrameFiles(DirectoryInfo workFolder, string outputFile, ImageFormat frameFormat)
        {

            if (workFolder.Exists == false)
            {
                throw new DirectoryNotFoundException(workFolder.FullName);
            }

            var writer = new AviWriter(outputFile)
            {
                FramesPerSecond = 30,
                // Emitting AVI v1 index in addition to OpenDML index (AVI v2)
                // improves compatibility with some software, including 
                // standard Windows programs like Media Player and File Explorer
                EmitIndex1 = true
            };

            // returns IAviVideoStream
            var stream = writer.AddVideoStream();

            // set standard VGA resolution
            stream.Width = 640;
            stream.Height = 480;

            // class SharpAvi.KnownFourCCs.Codecs contains FOURCCs for several well-known codecs
            // Uncompressed is the default value, just set it for clarity
            stream.Codec = KnownFourCCs.Codecs.Uncompressed;

            // Uncompressed format requires to also specify bits per pixel
            stream.BitsPerPixel = BitsPerPixel.Bpp32;

            // Get frame files' info in order
            var frames = workFolder.GetFiles("*." + frameFormat.ToString()).ToList().OrderBy(f => f.LastWriteTime);

            foreach (var item in frames)
            {
                var bitmap = AForge.Imaging.Image.FromFile(item.FullName);

                // and buffer of appropriate size for storing its bits
                var buffer = new byte[stream.Width * stream.Height * 4];

                var pixelFormat = PixelFormat.Format32bppRgb;

                // Now copy bits from bitmap to buffer
                var bits = bitmap.LockBits(new Rectangle(0, 0, stream.Width, stream.Height), ImageLockMode.ReadOnly, pixelFormat);

                Marshal.Copy(bits.Scan0, buffer, 0, buffer.Length);

                bitmap.UnlockBits(bits);

                // and flush buffer to encoding stream
                stream.WriteFrame(true, buffer, 0, buffer.Length);

                bitmap = null;
            }

            stream = null;

            writer.Close();
            writer = null;

        }


        public static void CreateVideo(List<Frame> frames, string outputFile)
        {

            var writer = new AviWriter(outputFile)
            {
                FramesPerSecond = 30,
                // Emitting AVI v1 index in addition to OpenDML index (AVI v2)
                // improves compatibility with some software, including 
                // standard Windows programs like Media Player and File Explorer
                EmitIndex1 = true
            };

            // returns IAviVideoStream
            var stream = writer.AddVideoStream();

            // set standard VGA resolution
            stream.Width = 640;
            stream.Height = 480;

            // class SharpAvi.KnownFourCCs.Codecs contains FOURCCs for several well-known codecs
            // Uncompressed is the default value, just set it for clarity
            stream.Codec = KnownFourCCs.Codecs.Uncompressed;

            // Uncompressed format requires to also specify bits per pixel
            stream.BitsPerPixel = BitsPerPixel.Bpp32;


            var frameData = new byte[stream.Width * stream.Height * 4];


            foreach (var item in frames)
            {

                // Say, you have a System.Drawing.Bitmap
                Bitmap bitmap = (Bitmap)item.Image;

                // and buffer of appropriate size for storing its bits
                var buffer = new byte[stream.Width * stream.Height * 4];

                var pixelFormat = PixelFormat.Format32bppRgb;

                // Now copy bits from bitmap to buffer
                var bits = bitmap.LockBits(new Rectangle(0, 0, stream.Width, stream.Height), ImageLockMode.ReadOnly, pixelFormat);

                //Marshal.Copy(bits.Scan0, buffer, 0, buffer.Length);

                Marshal.Copy(bits.Scan0, buffer, 0, buffer.Length);

                bitmap.UnlockBits(bits);

                // and flush buffer to encoding stream
                stream.WriteFrame(true, buffer, 0, buffer.Length);

            }

            stream = null;
            writer.Close();

        }

        public static string xxx()
        {
            return "hello";
        }

        //private static byte[] BitmapToByteArray(Image img)
        //{
        //    byte[] byteArray = new byte[0];
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.MemoryBmp);
        //        stream.Close();

        //        byteArray = stream.ToArray();
        //    }
        //    return byteArray;
        //}


        //public static byte[] ImageToByte(Image img)
        //{
        //    ImageConverter converter = new ImageConverter();
        //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
        //}

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
