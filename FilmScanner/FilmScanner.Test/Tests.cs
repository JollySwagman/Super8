
using System;
using NUnit.Framework;
using FilmScanner;
using System.IO;
using System.Drawing;
using System.Diagnostics;

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

        [Test]
        public void Basic_Disk()
        {
            var workFolder = ".";
            var filmScanner = new Scanner();

            var filename = string.Format("test_{0}.avi", DateTime.Now.Ticks);

            filmScanner.ScanToDisk(workFolder, filename);
        }


        [Test]
        public void Create_Video_From_Frames()
        {
            var workFolder = ".";

            for (int j = 0; j < 4; j++)
            {
                Image frame;
                for (int i = 0; i < 20; i++)
                {
                    frame = Frame.GetTestFrame(i.ToString(), true);

                    var frameFile = new FileInfo(Path.Combine(workFolder, i + ".bmp"));

                    Trace.WriteLine(string.Format("writing to {0} ({1})", frameFile.Name, frameFile.Exists));

                    frame.Save(frameFile.FullName);
                }
            }

            var videoFilename = string.Format("test_{0}.avi", DateTime.Now.Ticks);
            Video.CreateVideoFromFrameFiles(workFolder, videoFilename);
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
