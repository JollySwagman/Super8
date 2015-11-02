
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Ports;
using Arduino4Net.Components.Leds;
using System.Threading;

namespace FilmScanner.Arduino
{

    public class DigitalSensor
    {


        public DigitalSensor()
        {
            string[] portlist = SerialPort.GetPortNames();

            foreach (String s in portlist)
            {
                Trace.WriteLine(s);
            }

        }


        public bool GetState()
        {
            bool result = false;
            using (var board = new Arduino4Net.Models.Arduino("COM8", 57600))
            {
                var led = new Led(board, 13);
                var pin = new Arduino4Net.Components.Buttons.PushButton(board, 7);
               
                for (int i = 0; i < 300; i++)
                {
                    board.DigitalWrite(7, Arduino4Net.Models.DigitalPin.High);

                    result = pin.IsDown;

                    Trace.WriteLine("> " + result);

                    if (pin.IsDown)
                    {
                        Trace.WriteLine(result);
                        led.StrobeOn(20);
                        Thread.Sleep(100);
                        led.StrobeOff();
                    }
                    else
                    {
                        Thread.Sleep(80);
                        //led.StrobeOff();
                    }
                    board.DigitalWrite(7, Arduino4Net.Models.DigitalPin.High);
                }
            }
            return result;
        }

    }

}
