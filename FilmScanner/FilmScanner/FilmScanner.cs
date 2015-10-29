
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FilmScanner
{

    public enum FrameResultType
    {
        Error,
        FrameOK,
        EndOfFilm
    }

    /// <summary>
    /// High-level scanning executive.
    /// Responsible for:
    ///      scanning all frames and saving frame images to disk
    /// Not responsible for:
    ///      creating video file from frames
    /// </summary>
    public class FilmScanner
    {

        #region Properties

        private IList<string> m_messages = new List<string>();

        public IList<string> Messages
        {
            get
            {
                return this.m_messages;
            }
        }

        public IDigitalIO SprocketHoleSensor { get; private set; }

        public IDigitalIO FilmSensor { get; private set; }

        #endregion

        public FilmScanner(IDigitalIO sprocketHoleSensor, IDigitalIO filmSensor)
        {
            this.SprocketHoleSensor = sprocketHoleSensor;
            this.FilmSensor = filmSensor;
        }

        //public void Scan(string outputFilename)
        //{
        //    var result = new List<Frame>();

        //    Frame frame = null;
        //    var frameScanner = new FrameScanner();

        //    do
        //    {
        //        frame = frameScanner.GetNextFrame(this.FilmSensor, this.SprocketHoleSensor);
        //        result.Add(frame);
        //    }
        //    while (frame.Result == FrameResultType.FrameOK);

        //    Video.CreateVideo(result, outputFilename);
        //}


        public void ScanMovie(DirectoryInfo workFolder, string outputFilename)
        {
            var frameScanner = new FrameScanner();

            var frameProvider = new TestFrameProvider();

            Frame frame = null;
            int index = 0;
            string filename;
            do
            {
                frame = frameScanner.GetNextFrame(this.FilmSensor, this.SprocketHoleSensor, frameProvider);

                filename = Path.Combine(workFolder.FullName, string.Format("Frame_{0}.bmp", index));

                //frame.Save(filename);
            }
            while (frame.Result == FrameResultType.FrameOK);

            var imageFormat = ImageFormat.Bmp;

            Video.CreateVideoFromFrameFiles(workFolder, outputFilename, imageFormat);

        }

    }

}
