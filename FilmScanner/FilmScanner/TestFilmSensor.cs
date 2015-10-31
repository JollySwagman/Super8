
using System;
using System.Diagnostics;

namespace FilmScanner
{
    public class TestFilmSensor : IDigitalIO
    {

        public StateType State { get; set; }

        public TimeSpan Latency = new TimeSpan(0, 0, 3);

        private Stopwatch sw;

        public bool IsHigh()
        {
            return this.State == StateType.HIGH;
        }

        public bool IsLow()
        {
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
