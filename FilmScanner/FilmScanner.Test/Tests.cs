
using System;
using NUnit.Framework;
using FilmScanner;
using System.IO;

namespace FilmScanner.Test
{

    [TestFixture]
    public class Tests : TestBase
    {

        [Test]
        public void Basic()
        {
            var filmScanner = new Scanner();

            var filename = string.Format("test_{0}.avi", DateTime.Now.Ticks);

            filmScanner.Scan(filename);

            Assert.That(new FileInfo(filename).Exists);


        }


        /// <summary>
        /// Try and save 2 test frames
        /// </summary>
        [Test]
        public void TestImageSave()
        {
            var text = DateTime.Now.Ticks.ToString();

            var result0 = Frame.GetTestFrame(text, false);
            var result1 = Frame.GetTestFrame(text, true);

            result0.Save(text + ".bmp");
            result1.Save(text + "_F.bmp");
        }

    }

}
