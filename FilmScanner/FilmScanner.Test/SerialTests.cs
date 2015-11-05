
using System;
using NUnit.Framework;
using Rhino.Mocks;
using System.Net;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Ports;

namespace FilmScanner.Test
{

    [TestFixture]
    public class SerialTests : TestBase
    {

        [Test]
        public void GetCommPortNames()
        {
            foreach (var item in Common.SerialComms.GetSerialPorts())
            {
                Trace.WriteLine(item);
            }

        }


        [Test]
        public void Mock_Serial_Port()
        {

            var serialPortStub = MockRepository.GenerateStub<SerialPort>();

            //serialPortStub.Stub(x => x.DataReceived).Return("");
            //serialPortStub.Stub(x => x..IsHigh()).Return(true);


        }

    }

    public class SerialPortSim : SerialPort
    {



    }

}
