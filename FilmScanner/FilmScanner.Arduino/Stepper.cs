
using Sharpduino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmScanner.Arduino
{
    public class Stepper
    {
        public enum Direction
        {
            INPUT,
            OUTPUT
        }
        public enum Level
        {
            HIGH,
            LOW
        }

        public Stepper()
        {
            // initialize the digital pin as an output.
            pinMode(8, Direction.OUTPUT);
            pinMode(9, Direction.OUTPUT);
            pinMode(10, Direction.OUTPUT);
            pinMode(11, Direction.OUTPUT);
        }
        

        public void digitalWrite(int pin, Level level)
        {

        }
        
        public void pinMode(int pin, Direction direction)
        {
        }
        
        private void delay(int milliseconds)
        {
        }

        void loop()
        {
            int i = 0;
            //revolve one revolution clockwise
            for (i = 0; i < 512; i++)
            {
                clockwiserotate();
            }
            delay(500); // wait for a second
            for (i = 0; i < 512; i++)
            {
                counterclockwiserotate();
            }
            delay(500); // wait for a second
        }
        void clockwiserotate()
        { //revolve clockwise
            step1();
            step2();
            step3();
            step4();
            step5();
            step6();
            step7();
            step8();
        }
        void counterclockwiserotate()
        {
            step1();
            step7();
            step6();
            step5();
            step4();
            step3();
            step2();
            step1();
        }
        void step1()
        {
            digitalWrite(8, Level.HIGH);
            digitalWrite(9, Level.LOW);
            digitalWrite(10, Level.LOW);
            digitalWrite(11, Level.LOW);
            delay(1);
        }
        void step2()
        {
            digitalWrite(8, Level.HIGH);
            digitalWrite(9, Level.HIGH);
            digitalWrite(10, Level.LOW);
            digitalWrite(11, Level.LOW);
            delay(1);
        }
        void step3()
        {
            digitalWrite(8, Level.LOW);
            digitalWrite(9, Level.HIGH);
            digitalWrite(10, Level.LOW);
            digitalWrite(11, Level.LOW);
            delay(1);
        }
        void step4()
        {
            digitalWrite(8, Level.LOW);
            digitalWrite(9, Level.HIGH);
            digitalWrite(10, Level.HIGH);
            digitalWrite(11, Level.LOW);
            delay(1);
        }
        void step5()
        {
            digitalWrite(8, Level.LOW);
            digitalWrite(9, Level.LOW);
            digitalWrite(10, Level.HIGH);
            digitalWrite(11, Level.LOW);
            delay(1);
        }
        void step6()
        {
            digitalWrite(8, Level.LOW);
            digitalWrite(9, Level.LOW);
            digitalWrite(10, Level.HIGH);
            digitalWrite(11, Level.HIGH);
            delay(1);
        }
        void step7()
        {
            digitalWrite(8, Level.LOW);
            digitalWrite(9, Level.LOW);
            digitalWrite(10, Level.LOW);
            digitalWrite(11, Level.HIGH);
            delay(1);
        }
        void step8()
        {
            digitalWrite(8, Level.HIGH);
            digitalWrite(9, Level.LOW);
            digitalWrite(10, Level.LOW);
            digitalWrite(11, Level.HIGH);
            delay(1);
        }
    }
}
