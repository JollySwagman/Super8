
using System;
using NUnit.Framework;
using FilmScanner;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FilmScanner.Common;
using FilmScanner.Arduino;
using System.Threading;
namespace FilmScanner.Test
{

    [TestFixture]
    public class MotorTests : TestBase
    {

        [Test]
        public void Firmata_Basic()
        {
            var arduino = new DigitalSensor();
            for (int i = 0; i < 100; i++)
            {
                Trace.WriteLine(arduino.GetState());
                Thread.Sleep(1000);
            }

        }


        [Test]
        public void Motor_Basic()
        {
            var m1 = new StepperMotor();

            Assert.That(m1.Position, Is.EqualTo(0));

            m1.Move(10);
            Assert.That(m1.Position, Is.EqualTo(10));

            m1.Move(-10);
            Assert.That(m1.Position, Is.EqualTo(0));

        }


    }

}
