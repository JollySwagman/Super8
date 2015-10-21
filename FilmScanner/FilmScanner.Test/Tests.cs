
using System;
using NUnit.Framework;
using FilmScanner;
using System.IO;


namespace FilmScanner.UI
{

    [TestFixture]
    public class Tests
    {

        [Test]
        public void Basic()
        {
            var filmScanner = new Scanner();

            var filename = string.Format("test_{0}.avi", DateTime.Now.Ticks);

            filmScanner.Scan(filename);
        }

        [Test]
        public void Basic_Disk()
        {
            var workFolder = ".";
            var filmScanner = new Scanner();

            var filename = string.Format("test_{0}.avi", DateTime.Now.Ticks);

            filmScanner.ScanToDisk(workFolder, filename );
        }


        [Test]
        public void Create_Video_From_Frames()
        {
            var workFolder = ".";

            for (int i = 0; i < 20; i++)
            {
                var frame = Frame.GetTestFrame(i.ToString(), true);
                frame.Save(Path.Combine(workFolder, i + ".bmp"));
            }

            var filename = string.Format("test_{0}.avi", DateTime.Now.Ticks);

            Video.CreateVideoFromFrameFiles(workFolder, "testout.avi");
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
