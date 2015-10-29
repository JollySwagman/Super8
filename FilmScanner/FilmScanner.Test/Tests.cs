
using System;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace FilmScanner.Test
{

    [TestFixture]
    public class Tests : TestBase
    {

        [Test]
        public void Can_Scan_And_Save_Frame()
        {
            var filmSensorStub = MockRepository.GenerateStub<IDigitalIO>();
            var sprocketHoleSensorStub = MockRepository.GenerateStub<IDigitalIO>();

            var filmScanner = new FilmScanner(sprocketHoleSensorStub, filmSensorStub);

            var filename = string.Format("test_{0}.avi", DateTime.Now.Ticks);

            var fp = new TestFrameProvider();
            var fs = new FrameScanner();

            var frame = fs.GetNextFrame(filmSensorStub, sprocketHoleSensorStub, fp);

            Assert.That(frame, Is.Not.Null);

            Assert.That(frame.Image, Is.Not.Null);
            Assert.That(frame.Image.Width, Is.EqualTo(640));

            Trace.WriteLine("Width: " + frame.Image.Size.Width);
            Trace.WriteLine("Height: " + frame.Image.Size.Height);

            //Assert.That(new FileInfo(filename).Exists);
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_On_Null_FrameProvider()
        {
            var fs = new FrameScanner();
            var frame = fs.GetNextFrame(null, null, null);
        }


        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "No film detected.")]
        public void FrameScanner_Aborts_If_No_Film_Detected()
        {
            var filmSensorStub = MockRepository.GenerateStub<IDigitalIO>();
            var sprocketHoleSensorStub = MockRepository.GenerateStub<IDigitalIO>();

            filmSensorStub.Stub(x => x.IsLow()).Return(true);

            sprocketHoleSensorStub.Stub(x => x.IsHigh()).Return(false);

            var fp = new TestFrameProvider();
            var fs = new FrameScanner();

            var frame = fs.GetNextFrame(filmSensorStub, sprocketHoleSensorStub, fp);
        }


        [Test]
        [ExpectedException(typeof(TimeoutException), ExpectedMessage = "No sprocket hole detected within timeout period.")]
        public void FrameScanner_SprocketHole_Detect_Timeout()
        {
            var filmSensorStub = MockRepository.GenerateStub<IDigitalIO>();
            var sprocketHoleSensorStub = MockRepository.GenerateStub<IDigitalIO>();

            filmSensorStub.Stub(x => x.IsHigh()).Return(true);
            sprocketHoleSensorStub.Stub(x => x.IsLow()).Return(true);

            var fp = new TestFrameProvider();
            var fs = new FrameScanner();

            var frame = fs.GetNextFrame(filmSensorStub, sprocketHoleSensorStub, fp);
        }



        [Test]
        public void Can_Create_Video_From_Frame_Files()
        {
            const int DURATION_SECONDS = 10;
            const int FRAME_RATE = 18;

            var totalFrames = DURATION_SECONDS * FRAME_RATE;

            Trace.WriteLine("totalFrames: " + totalFrames);

            var workFolder = new DirectoryInfo(@".\WorkFolder");
            if (workFolder.Exists == false)
            {
                workFolder.Create();
            }

            var imageFormat = ImageFormat.Png;

            // CREATE FRAME FILES TO ADD TO THE VIDEO
            Image frame;
            for (int i = 0; i < totalFrames; i++)
            {
                frame = TestFrameProvider.GetTestFrame("Frame " + i.ToString(), true);

                var filename = string.Format("{0}_{1}.{2}", GetTestName(), i.ToString("000000000"), imageFormat.ToString());

                var frameFile = new FileInfo(Path.Combine(workFolder.FullName, filename));

                Trace.WriteLine(string.Format("writing to {0} ({1})", frameFile.FullName, frameFile.Exists));

                frame.Save(frameFile.FullName, imageFormat);

                frame.Dispose();
            }

            // CREATE THE VIDEO
            var videoFilename = string.Format("test_{0}.avi", DateTime.Now.Ticks);
            Video.CreateVideoFromFrameFiles(workFolder, videoFilename, imageFormat);

        }


        /// <summary>
        /// Try and save 2 test frames
        /// </summary>
        [Test]
        public void TestImageSave()
        {
            var text = GetTestName() + "\n\r" + DateTime.Now.Ticks.ToString();

            var flipped_N = TestFrameProvider.GetTestFrame(text, false);
            var flipped_Y = TestFrameProvider.GetTestFrame(text, true);

            flipped_N.Save("flipped_N.bmp");
            flipped_Y.Save("flipped_Y.bmp");
        }

    }

}
