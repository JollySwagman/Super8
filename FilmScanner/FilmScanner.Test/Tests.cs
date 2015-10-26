
using System;
using NUnit.Framework;
using FilmScanner;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace FilmScanner.Test
{

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
        public void Can_Create_Video_From_Frame_Files()
        {
            var workFolder = new DirectoryInfo(@".\WorkFolder");
            if (workFolder.Exists == false)
            {
                workFolder.Create();
            }

            var imageFormat = ImageFormat.Png;

            Image frame;
            for (int i = 0; i < 20; i++)
            {
                frame = Frame.GetTestFrame("Frame " + i.ToString(), true);

                var filename = string.Format("{0}_{1}.{2}", GetTestName(), i.ToString("000000000"), imageFormat.ToString());

                var frameFile = new FileInfo(Path.Combine(workFolder.FullName, filename));

                Trace.WriteLine(string.Format("writing to {0} ({1})", frameFile.FullName, frameFile.Exists));

                frame.Save(frameFile.FullName, imageFormat);

                frame.Dispose();
            }

            var videoFilename = string.Format("test_{0}.avi", DateTime.Now.Ticks);
            Video.CreateVideoFromFrameFiles(workFolder, videoFilename, imageFormat);

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
