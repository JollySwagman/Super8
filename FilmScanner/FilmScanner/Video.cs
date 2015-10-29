
using FilmScanner.Common;
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
        /// Creates an AVI video file from a folder of uncompressed BMP files
        /// </summary>
        /// <param name="workFolder">The folder containing the image files</param>
        /// <param name="outputFile">The filename for the resulting file</param>
        /// <remarks>The files are read in order of last write time</remarks>
        public static void CreateVideoFromFrameFiles(DirectoryInfo workFolder, string outputFile, ImageFormat frameFormat)
        {

            if (outputFile.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("outputFile");
            }
            if (workFolder == null)
            {
                throw new ArgumentNullException("workFolder");
            }

            if (workFolder.Exists == false)
            {
                workFolder.Create();
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

    }

}
