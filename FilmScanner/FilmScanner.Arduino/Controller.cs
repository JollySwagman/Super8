
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpduino;
using FilmScanner.Common;

namespace FilmScanner.Arduino
{

    public class Controller
    {

        public enum SeekResult
        {
            Unknown,
            Found,
            Timeout,
            EndOfFilm
        }

        public enum Command
        {
            Seek
        }


        private IList<string> m_ReceiveLog = new List<string>();
        private string m_ReceiveLine;
        private SerialPort arduinoBoard;

        public TimeSpan Timeout { get; set; }

        public Controller(string portName) : this(portName, new TimeSpan(0, 0, 3))
        { }


        public Controller(string portName, TimeSpan timeout)
        {
            this.Timeout = timeout;
            this.arduinoBoard = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            OpenPort(portName);
        }


        public SeekResult SeekToNextFrame()
        {
            // Send the command and wait for response
            var response = ExecCommand(Command.Seek);

            var result = SeekResult.Unknown;

            switch (response.ToUpperSafe())
            {
                case "OK":
                    result = SeekResult.Found;
                    break;
                case "TIMEOUT":
                    result = SeekResult.Timeout ;
                    break;
                case "ENDOFFILM":
                    result = SeekResult.EndOfFilm;
                    break;
                default:
                    result = SeekResult.Unknown;
                    break;
            }


            return result;

        }


        private string ExecCommand(Command command)
        {
            this.m_ReceiveLine = null;
            arduinoBoard.Write(command + "\n");


            // Wait for response
            while (this.m_ReceiveLine.IsNullOrWhiteSpace())
            {
                System.Threading.Thread.Sleep(100);
            }

            return null;
        }


        public void OpenPort(string portName)
        {
            if (!arduinoBoard.IsOpen)
            {
                arduinoBoard.DataReceived += arduinoBoard_DataReceived;
                arduinoBoard.PortName = portName;
                arduinoBoard.Open();
            }
        }


        void arduinoBoard_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            const string EOD = "\r"; // End Of Data

            this.m_ReceiveLine = arduinoBoard.ReadTo(EOD);  //Read until the EOT code

            this.m_ReceiveLog.Add(this.m_ReceiveLine);

            //string[] dataArray = data.Split(new string[] { "\x02", "$" }, StringSplitOptions.RemoveEmptyEntries);
            ////Iterate through the split data and parse it into weather data items
            ////and add them to the list of received weather data.
            //foreach (string dataItem in dataArray.ToList())
            //{
            //}
            //if (NewWeatherDataReceived != null)//If there is someone waiting for this event to be fired
            //{
            //    NewWeatherDataReceived(this, new EventArgs()); //Fire the event, 
            //                                                   // indicating that new WeatherData was added to the list.
            //}
        }

        public override string ToString()
        {
            return this.ToStringGeneric();
        }

    }

}
