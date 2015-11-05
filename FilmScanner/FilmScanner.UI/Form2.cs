
using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.IO;
using FilmScanner;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using FilmScanner.Arduino;

namespace Super8Scanner.UI
{

    /// <summary>
    /// See http://www.emgu.com/wiki/index.php/Camera_Capture_in_7_lines_of_code ??
    /// </summary>
    public partial class Form2 : Form
    {

        private Bitmap m_image;
        private bool takeFrame = false;
        //private FrameScanner m_FrameScanner;
        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;

        public Form2()
        {
            InitializeComponent();
        }

        //toggle start and stop button
        private void start_Click(object sender, EventArgs e)
        {
            if (start.Text == "&Start")
            {
                if (DeviceExist)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    CloseVideoSource();
                    //videoSource.DesiredFrameSize = new Size(160, 120);
                    //videoSource.DesiredFrameRate = 10;
                    videoSource.Start();
                    label2.Text = "Device running...";
                    start.Text = "&Stop";
                    //timer1.Enabled = true;
                    btnSnapshot.Enabled = true;
                }
                else
                {
                    label2.Text = "Error: No Device selected.";
                }
            }
            else
            {
                if (videoSource.IsRunning)
                {
                    //timer1.Enabled = false;
                    CloseVideoSource();
                    label2.Text = "Device stopped.";
                    start.Text = "&Start";
                    btnSnapshot.Enabled = false;
                }
            }
        }


        //eventhandler if new frame is ready
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (m_image != null)
            {
                m_image.Dispose();
            }
            m_image = (Bitmap)eventArgs.Frame.Clone();

            pictureBox1.Image = m_image;

            if (takeFrame)
            {
                takeFrame = false;
                // Save the frame to disk
                var filename = new FileInfo("frame_" + DateTime.Now.Ticks + ".png");
                m_image.Save(filename.FullName, ImageFormat.Png);
                label2.Invoke((Action)(() => label2.Text = filename.Name + " taken"));
                //label2.Text = filename.Name + " taken";
            }

        }

        //close the device safely
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }

        ////get total received frame at 1 second tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            //    label2.Text = "Device running... " + videoSource.FramesReceived.ToString() + " FPS";
            //    // takeFrame = true;
        }

        //prevent sudden close while device is running
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GetCamList();
            //m_FrameScanner.ThresholdReached
        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            takeFrame = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {

            btnCapture.Enabled = false;

            var controller = new Controller("COM1");


            var frameFound = Controller.SeekResult.Found;

            // Loop through frames
            while (frameFound == Controller.SeekResult.Found)
            {


                frameFound = controller.SeekToNextFrame();
            }



            btnCapture.Enabled = true;

        }


        // get the devices name
        private void GetCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                comboBox1.Items.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);
                }
                comboBox1.SelectedIndex = 0; //make dafault to first cam
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                comboBox1.Items.Add("No capture device on your system");
            }
        }




    }

}