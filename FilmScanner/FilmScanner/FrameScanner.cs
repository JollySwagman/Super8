using FilmScanner.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class FrameScanner
    {

        #region Properties

        private TimeSpan m_DefaultTimeout = new TimeSpan(0, 0, 6);

        /// <summary>
        /// The time taken to line up the frame
        /// </summary>
        public TimeSpan SeekTime { get; private set; }

        /// <summary>
        /// The time taken to get the frame from the capture device
        /// </summary>
        public TimeSpan CaptureTime { get; private set; }

        /// <summary>
        /// SeekTime plus CaptureTime
        /// </summary>
        public TimeSpan TotalElapsed { get { return this.SeekTime + this.CaptureTime; } }

        public TimeSpan DefaultTimeout
        {
            get { return m_DefaultTimeout; }
            set { m_DefaultTimeout = value; }
        }

        #endregion

        //        public event EventHandler ThresholdReached;

        public void SeekNextFrame(IDigitalIO FilmSensor, IDigitalIO SprocketHoleSensor, TimeSpan frameAdvanceTimeout)
        {

            if (FilmSensor.IsLow())
            {
                throw new InvalidOperationException("No film detected.");
            }

            // Find the first sprocket hole...
            var motor = new StepperMotor();

            var sw = new Stopwatch();           // To manage timeout
            sw.Start();

            var delayMilliseconds = 50;

            // Wait for sprocket hole to be detected
            while (SprocketHoleSensor.IsLow() && sw.Elapsed < frameAdvanceTimeout)
            {
                // Advance the film
                motor.Move(1);
                //Trace.WriteLine("WAITING " + sw.Elapsed);
                System.Threading.Thread.Sleep(delayMilliseconds);
            }

            sw.Stop();
            this.SeekTime = sw.Elapsed;

            // Still low - we've timed out
            if (SprocketHoleSensor.IsLow())
            {
                throw new TimeoutException("No sprocket hole detected within timeout period.");
            }


            //
            // WE SHOULD HAVE A FRAME LINED UP IN THE SCANNER NOW ...
            //

            sw.Stop();
            this.CaptureTime = sw.Elapsed;
        }


        //protected virtual void OnThresholdReached(EventArgs e)
        //{
        //    EventHandler handler = ThresholdReached;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}

        public override string ToString()
        {
            return this.ToStringGeneric();
        }






















        public Frame MoveToNextFrame(IDigitalIO FilmSensor, IDigitalIO SprocketHoleSensor, IFrameProvider frameProvider)
        {
            return MoveToNextFrame(FilmSensor, SprocketHoleSensor, this.DefaultTimeout, frameProvider);
        }

        /// <summary>
        /// Manages the scanning process to advance to and take one frame
        /// </summary>
        /// <returns></returns>
        public Frame MoveToNextFrame(IDigitalIO FilmSensor, IDigitalIO SprocketHoleSensor, TimeSpan frameAdvanceTimeout, IFrameProvider frameProvider)
        {

            //if (frameProvider == null)
            //{
            //    throw new ArgumentNullException("frameProvider");
            //}


            if (FilmSensor.IsLow())
            {
                throw new InvalidOperationException("No film detected.");
            }

            // Find the first sprocket hole...
            var motor = new StepperMotor();

            var sw = new Stopwatch();           // To manage timeout
            sw.Start();

            //var delayMilliseconds = 50;

            // Wait for sprocket hole to be detected
            while (SprocketHoleSensor.IsLow() && sw.Elapsed < frameAdvanceTimeout)
            {
                // Advance the film
                motor.Move(1);
                //Trace.WriteLine("WAITING " + sw.Elapsed);
                //System.Threading.Thread.Sleep(delayMilliseconds);
            }

            sw.Stop();
            this.SeekTime = sw.Elapsed;

            // Still low - we've timed out
            if (SprocketHoleSensor.IsLow())
            {
                throw new TimeoutException("No sprocket hole detected within timeout period.");
            }


            //
            // WE SHOULD HAVE A FRAME LINED UP IN THE SCANNER NOW ...
            //

            // GET THE IMAGE!
            sw.Restart();
            //var image = frameProvider.CaptureFrame();
            // TELL CLIENT TO CAPTURE IMAGE
            //OnThresholdReached(EventArgs.Empty);
            sw.Stop();
            this.CaptureTime = sw.Elapsed;


            // Package result
            var result = new Frame()
            {
                //                Image = image,
                //              Result = FrameResultType.FrameOK
            };

            return result;
        }

    }

}