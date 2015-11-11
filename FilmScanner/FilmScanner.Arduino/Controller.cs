
using System.IO.Ports;
using System;
using System.Diagnostics;
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

        const string EOD = "\r"; // End Of Data

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
                    result = SeekResult.Timeout;
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

            var sw = new Stopwatch();
            sw.Start();

            // Wait for response or timeout
            while (this.m_ReceiveLine.IsNotNullOrEmpty())
            {
                if (sw.Elapsed >= this.Timeout)
                {
                    this.m_ReceiveLine = "TIMEOUT";
                    break;  // GOTO!
                }
                System.Threading.Thread.Sleep(100);
            }
            return this.m_ReceiveLine;
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
            this.m_ReceiveLine = arduinoBoard.ReadTo(EOD);  //Read until the EOT code

            this.m_ReceiveLog.Add(this.m_ReceiveLine);
        }

        public override string ToString()
        {
            return this.ToStringGeneric();
        }

    }

}
