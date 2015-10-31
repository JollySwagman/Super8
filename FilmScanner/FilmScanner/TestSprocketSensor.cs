
using System;
using System.Diagnostics;

namespace FilmScanner
{
    public class TestSprocketSensor : IDigitalIO
    {

        public StateType State { get; set; }

        public TimeSpan Latency = new TimeSpan(0, 0, 2);

        private Stopwatch sw;

        public TestSprocketSensor()
        {
            sw = new Stopwatch();
            sw.Start();
        }

        public bool IsHigh()
        {
            return this.State == StateType.HIGH;
        }

        public bool IsLow()
        {

            //Trace.WriteLine("IsLow() " + sw.Elapsed + " " + this.State.ToString());
            //Trace.WriteLine(string.Format("latency: {0}  Elapsed: {1}", this.Latency, sw.Elapsed));
            //Trace.WriteLine(string.Format("wait: {0}", this.Latency - sw.Elapsed));

            if (sw.Elapsed > this.Latency)
            {
                ToggleStateType();
                Trace.WriteLine("Time Out!");
                sw.Reset();
            }

            return this.State == StateType.LOW;
        }

        public StateType ToggleStateType()
        {
            if (this.State == StateType.LOW)
            {
                this.State = StateType.HIGH;
            }
            else
            {
                this.State = StateType.LOW;
            }
            return this.State;
        }

    }

}
