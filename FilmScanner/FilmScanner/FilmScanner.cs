
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FilmScanner
{

    public enum FrameResultType
    {
        Error,
        FrameOK,
        EndOfFilm
    }

    /// <summary>
    /// High-level scanning executive.
    /// Responsible for:
    ///      scanning all frames and saving frame images to disk
    /// Not responsible for:
    ///      creating video file from frames
    /// </summary>
    public class Scanner
    {

        private IList<string> m_messages = new List<string>();

        private int counter = 0;
        public IList<string> Messages
        {
            get
            {
                return this.m_messages;
            }
        }

        public void Scan(string outputFilename)
        {
            var result = new List<Frame>();

            Frame frame = null;

            do
            {
                frame = GetNextFrame();
                result.Add(frame);
            }
            while (frame.Result == FrameResultType.FrameOK);

            Video.CreateVideo(result, outputFilename);

        }


        public void ScanToDisk(string workFolder, string outputFilename)
        {
            Frame frame = null;
            int index = 0;
            string filename;
            do
            {
                frame = GetNextFrame();

                filename = Path.Combine(workFolder, string.Format("Frame_{0}.bmp", index));

                frame.Save(filename);
            }
            while (frame.Result == FrameResultType.FrameOK);

            Video.CreateVideoFromFrameFiles(workFolder, outputFilename);

        }

        /// <summary>
        /// Manages the scanning process to advance to and take one frame
        /// </summary>
        /// <returns></returns>
        private Frame GetNextFrame()
        {
            var pinLedLighting = 1;
            var pinFilmSensor = 2;
            var pinHoleSensor = 3;

            var frameAdvanceTimeout = new TimeSpan(0, 0, 6);
            var delayMilliseconds = 50;

            // Initialise and check hardware
            var ledLighting = new DigitalIO(pinLedLighting);            // Film light
            var filmSensor = new DigitalIO(pinFilmSensor);              // Should be LOW when film is loaded
            var sprocketHoleSensor = new DigitalIO(pinHoleSensor);      // LOW when over a sprocket hole


            if (filmSensor.IsHigh())
            {
                m_messages.Add("No film detected.");
            }

            // Find the first sprocket hole...
            var motor = new Motor();

            var sw = new Stopwatch();           // To manage timeout
            sw.Start();

            motor.Start();

            while (filmSensor.IsLow() && sprocketHoleSensor.IsLow() && sw.Elapsed < frameAdvanceTimeout)
            {
                // Wait for sprocket hole
                System.Threading.Thread.Sleep(delayMilliseconds);
                sprocketHoleSensor.State = DigitalIO.StateType.HIGH;    // sprocket hole "found"
            }

            motor.Stop();

            Trace.WriteLine(sw.Elapsed);
            sw.Stop();

            if (sw.Elapsed > frameAdvanceTimeout)
            {
                // Check for timeout
                throw new InvalidOperationException(string.Format("Sprocket hole not found within {0} seconds.", frameAdvanceTimeout.TotalSeconds));
            }

            // WE SHOULD HAVE A FRAME LINED UP IN THE SCANNER NOW ...

            // Take image

            var image = Frame.GetTestFrame("Frame no: " + counter, true);


            counter++;


            // Package result
            var result = new Frame()
            {
                Image = image,
                Result = FrameResultType.FrameOK
            };

            // if (rnd.Next(0, 10) == 2)
            if (counter > 30)
            {
                result.Result = FrameResultType.EndOfFilm;
            }

            return result;
        }

    }

}
