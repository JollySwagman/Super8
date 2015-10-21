
using NUnit.Framework;
using System;
using System.IO;

namespace FilmScanner.Test
{
    public class TestBase
    {


        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {
            CleanFiles("*.avi");
            CleanFiles("*.bmp");

        }


        private void CleanFiles(string pattern)
        {
            foreach (var item in new DirectoryInfo(".").GetFiles(pattern))
            {
                item.Delete();
            }
        }

    }

}
