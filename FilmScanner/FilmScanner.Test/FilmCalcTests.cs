
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FilmScanner.Common;

namespace FilmScanner.Test
{
    [TestFixture]
    public class FilmCalcTests : TestBase
    {

        [Test]
        public void Basic()
        {
            var calc = new FilmCalc(18)
            {
                DurationSeconds = 240,
                ProcessingSecondsPerFrame = 5
            };

            Assert.That(calc.ProcessingSecondsPerFrame, Is.EqualTo(5));
            Assert.That(calc.DurationSeconds, Is.EqualTo(240));
            Assert.That(calc.FrameCount, Is.EqualTo(4320));

            Trace.WriteLine(calc.ToString());

            calc.FrameRate = 24;
            Assert.That(calc.FrameRate, Is.EqualTo(24));

            Trace.WriteLine("ProcessingTime.Description: " + calc.ProcessingTime.Description());

            Trace.WriteLine(calc.ToString());

            Assert.That(calc.FrameCount, Is.EqualTo(5760));

        }

        [Test]
        public void TimeSpanTest()
        {
            TimeSpan ts;

            ts = new TimeSpan(12, 15, 45);
            Trace.WriteLine(ts.Description());
            Assert.That(ts.Description(), Is.EqualTo("12 hours, 15 minutes"));

            ts = new TimeSpan(36, 15, 45);
            Trace.WriteLine(ts.Description());
            Assert.That(ts.Description(), Is.EqualTo("1 day, 12 hours, 15 minutes"));

            ts = new TimeSpan(108, 15, 45);
            Trace.WriteLine(ts.Description());
            Assert.That(ts.Description(), Is.EqualTo("4 days, 12 hours, 15 minutes"));

        }

    }

}
