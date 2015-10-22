
using System;
using NUnit.Framework;
using System.Diagnostics;

namespace FilmScanner.Test
{
    [TestFixture]
    public class TestBase 
    {

        private Stopwatch m_Stopwatch;
        private const int BANNER_WIDTH = 160;
        private char ch = '■';


        [SetUp]
        public void Setup()
        {
            Trace.WriteLine("\n\n\n");
            Trace.WriteLine("".PadRight(BANNER_WIDTH, ch));
            Trace.WriteLine("Test: " + TestContext.CurrentContext.Test.Name);
            Trace.WriteLine("Started: " + DateTime.Now);
            Trace.WriteLine("".PadRight(BANNER_WIDTH, '-'));

            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();
        }

        [TearDown]
        public void TearDown()
        {
            Trace.WriteLine("\n\n");
            Trace.WriteLine("".PadRight(BANNER_WIDTH, '-'));

            m_Stopwatch.Stop();
            Trace.WriteLine("Test: " + TestContext.CurrentContext.Test.Name);
            Trace.WriteLine("Elapsed: " + m_Stopwatch.Elapsed.ToString());
            Trace.WriteLine("".PadRight(BANNER_WIDTH, ch));
        }

    }

}