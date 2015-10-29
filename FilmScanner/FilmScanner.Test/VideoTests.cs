
using System;
using NUnit.Framework;
using System.Drawing.Imaging;

namespace FilmScanner.Test
{

    [TestFixture]
    public class VideoTests : TestBase
    {

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throw_On_Missing_Work_Folder()
        {
            Video.CreateVideoFromFrameFiles(null, "outfile.avi", ImageFormat.Bmp);
        }

    }

}
