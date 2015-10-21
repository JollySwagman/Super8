
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class DigitalIO
    {

        public enum StateType
        {
            LOW, HIGH
        }

        public StateType State { get; set; }
        private int m_PinIndex;

        public bool IsLow()
        {
            return this.State == StateType.LOW;
        }

        public int PinIndex
        {
            get
            {
                return m_PinIndex;
            }
        }

        public bool IsHigh()
        {
            return this.State == StateType.HIGH;
        }

        public DigitalIO(int pinIndex)
        {
            this.m_PinIndex = pinIndex;
        }

    }

}