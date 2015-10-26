
using System;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using FilmScanner.Common;

namespace FilmScanner.Test
{

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
            FileCleanup();

            Trace.WriteLine("\n\n");
            Trace.WriteLine("".PadRight(BANNER_WIDTH, '-'));

            m_Stopwatch.Stop();
            Trace.WriteLine("Test: " + TestContext.CurrentContext.Test.Name);
            Trace.WriteLine("Elapsed: " + m_Stopwatch.Elapsed.ToString());
            Trace.WriteLine("".PadRight(BANNER_WIDTH, ch));
        }


        private void FileCleanup()
        {
            // File settling
            System.Threading.Thread.Sleep(5000);

            //DeleteFiles("", "*.bmp");
            //DeleteFiles("", "*.avi");

        }


        private void DeleteFiles(string folder, string pattern)
        {
            if (folder.IsNullOrWhiteSpace())
            {
                folder = ".";
            }
            var files = new DirectoryInfo(folder).GetFiles(pattern).ToList();
            Trace.WriteLine(string.Format(@"{0} files found in {1}\{2}", files.Count, folder, pattern));

            var deletedOK = true;
            foreach (var item in files)
            {
                try
                {
                    item.Delete();
                    Trace.WriteLine("Deleted: " + item.Name);
                }
                catch (Exception ex)
                {
                    deletedOK = false;
                    Trace.WriteLine("Error: " + ex.Message);
                }
            }

            if (deletedOK == false)
            {
                throw new ApplicationException("Unable to delete all files.");
            }
        }


        protected string GetTestName()
        {
            return TestContext.CurrentContext.Test.Name;
        }

    }

}