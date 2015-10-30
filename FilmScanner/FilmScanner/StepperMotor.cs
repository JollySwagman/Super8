
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmScanner
{
    public class StepperMotor
    {

        public int Position { get; set; }

        public StepperMotor() // Init params?
        {
            // Init?
        }

        /// <summary>
        /// Move stepper, clockwise if positive, anticlockwise if negative
        /// </summary>
        /// <param name="steps"></param>
        public void Move(int steps)
        {
            this.Position += steps;
        }

    }

}