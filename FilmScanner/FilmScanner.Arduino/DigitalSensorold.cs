
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpduino;
using Sharpduino.Constants;
using System.Diagnostics;
using System.IO.Ports;

namespace FilmScanner.Arduino
{

    public class DigitalSensorold
    {

        ArduinoUno arduino = null;

        public DigitalSensorold()
        {
            string[] portlist = SerialPort.GetPortNames();

            foreach (String s in portlist)
            {
                Trace.WriteLine(s);
                arduino = new ArduinoUno(s);
                Trace.WriteLine(arduino.IsInitialized);
            }

            arduino.SetPinMode(ArduinoUnoPins.D7, PinModes.Input);
            arduino.SetDO(ArduinoUnoPins.D7, true);  // Turn on internal Pull-Up Resistor
        }


        public void GetState()
        {
            if (arduino.IsInitialized)
            {

                // Read the state of a pin
                var result = arduino.GetCurrentPinState(ArduinoUnoPins.D8);
                Trace.WriteLine(result.CurrentValue);

                //                return result.CurrentValue > 10;
            }
        }

    }

}
