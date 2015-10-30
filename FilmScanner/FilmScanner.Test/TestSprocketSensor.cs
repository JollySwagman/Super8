
using System;
using System.Diagnostics;

namespace FilmScanner.Test
{
    public class TestSprocketSensor : IDigitalIO
    {

        public StateType State { get; set; }

        private TimeSpan latency = new TimeSpan(0, 0, 3);

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

            Trace.WriteLine("IsLow() " + sw.Elapsed + " " + this.State.ToString());

            if (sw.Elapsed > latency)
            {
                ToggleStateType();
                Trace.WriteLine("FOUND!");
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
