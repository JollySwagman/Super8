using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class FrameScanner 
    {

        //private int counter = 0;

        public Frame GetNextFrame(IDigitalIO FilmSensor, IDigitalIO SprocketHoleSensor, IFrameProvider frameProvider)
        {
            return GetNextFrame(FilmSensor, SprocketHoleSensor, new TimeSpan(0, 0, 6), frameProvider);
        }

        /// <summary>
        /// Manages the scanning process to advance to and take one frame
        /// </summary>
        /// <returns></returns>
        public Frame GetNextFrame(IDigitalIO FilmSensor, IDigitalIO SprocketHoleSensor, TimeSpan frameAdvanceTimeout, IFrameProvider frameProvider)
        {

            if (frameProvider == null)
            {
                throw new ArgumentNullException("frameProvider");
            }

            var delayMilliseconds = 50;

            if (FilmSensor.IsLow())
            {
                throw new InvalidOperationException("No film detected.");
            }

            // Find the first sprocket hole...
            var motor = new Motor();

            var sw = new Stopwatch();           // To manage timeout
            sw.Start();

            motor.Start();

            while (SprocketHoleSensor.IsLow() && sw.Elapsed < frameAdvanceTimeout)
            {
                // Wait for sprocket hole
                System.Threading.Thread.Sleep(delayMilliseconds);
            }

            motor.Stop();

            // Still low - we've timed out
            if (SprocketHoleSensor.IsLow())
            {
                throw new TimeoutException("No sprocket hole detected within timeout period.");
            }

            Trace.WriteLine("Frame seek time: "+sw.Elapsed);
            sw.Stop();


            //
            // WE SHOULD HAVE A FRAME LINED UP IN THE SCANNER NOW ...
            //

            // GET THE IMAGE!
            var image = frameProvider.CaptureFrame();

            // Package result
            var result = new Frame()
            {
                Image = image,
                Result = FrameResultType.FrameOK
            };

            return result;
        }

    }

}