
using FilmScanner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class FilmCalc
    {

        private int m_FrameRate;
        private int m_DurationSeconds;
        private int m_ProcessingSecondsPerFrame;

        public string DurationDescription
        {
            get
            {
                return this.ProcessingTime.Description();
            }
        }

        public int FrameRate
        {
            get { return m_FrameRate; }
            set
            {
                m_FrameRate = value;
                Recalc();
            }
        }

        public int DurationSeconds
        {
            get { return m_DurationSeconds; }
            set
            {
                m_DurationSeconds = value;
                Recalc();
            }
        }

        public int ProcessingSecondsPerFrame
        {
            get { return m_ProcessingSecondsPerFrame; }
            set
            {
                m_ProcessingSecondsPerFrame = value;
                Recalc();
            }
        }

        public TimeSpan ProcessingTime { get; private set; }

        public int FrameCount { get; private set; }

        public FilmCalc(int frameRate)
        {
            this.FrameRate = frameRate;
        }

        private void Recalc()
        {
            this.FrameCount = this.FrameRate * this.DurationSeconds;

            this.ProcessingTime = new TimeSpan(0, 0, this.FrameCount * this.ProcessingSecondsPerFrame);

        }

        public override string ToString()
        {
            return this.ToStringGeneric();
        }

    }

}