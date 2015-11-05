
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmScanner.Common
{
    public class SerialComms
    {

        static SerialPort m_serialPort;

        
        public static List<string> GetSerialPorts()
        {
            var comPortNames = SerialPort.GetPortNames();
            return comPortNames.ToList();
        }

        public void Open()
        {

        }



    }

}
