
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace RSTest.Integration.ComprehensiveCreditReporting
{
    public class TestBase2
    {

        private Stopwatch m_TestStopwatch;
        private Stopwatch m_FixtureStopwatch;

        private const int BANNER_WIDTH = 120;

        private string outsideBanner = "".PadRight(BANNER_WIDTH, '■');
        private string insideBanner = "".PadRight(BANNER_WIDTH, '▬');

        public string GetTestName()
        {
            return TestContext.CurrentContext.Test.Name;
        }


        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            m_FixtureStopwatch = new Stopwatch();
            m_FixtureStopwatch.Start();
        }


        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            Trace.WriteLine("\n\n\n");
            Trace.WriteLine("".PadRight(BANNER_WIDTH, '~'));
            m_TestStopwatch.Stop();
            Trace.WriteLine("Fixture elapsed: " + m_FixtureStopwatch.Elapsed.ToString());
            Trace.WriteLine("".PadRight(BANNER_WIDTH, '~'));
        }


        [SetUp]
        public void Setup()
        {
            Trace.WriteLine(outsideBanner);
            Trace.WriteLine("    Test start: " + TestContext.CurrentContext.Test.Name);
            Trace.WriteLine("       Started: " + DateTime.Now);
            Trace.WriteLine(insideBanner);

            m_TestStopwatch = new Stopwatch();
            m_TestStopwatch.Start();
        }

        [TearDown]
        public void TearDown()
        {
            Trace.WriteLine("\n\n");
            Trace.WriteLine(insideBanner);
            m_TestStopwatch.Stop();
            Trace.WriteLine("    Test stop: " + TestContext.CurrentContext.Test.Name);
            Trace.WriteLine("      Elapsed: " + m_TestStopwatch.Elapsed.ToString());
            Trace.WriteLine(outsideBanner);
            Trace.WriteLine("\n\n");
        }

    }

}
