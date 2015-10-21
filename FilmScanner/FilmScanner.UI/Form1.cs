using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharpduino;
using Sharpduino.Constants;
using System.Diagnostics;


namespace Super8Scanner.UI
{
    public partial class Form1 : Form
    {

        private VideoCaptureDevice videoSource;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {

                // Create a new connection
                var arduino = new ArduinoUno("COM11");

                //// Read an analog value 
                //float valueInVolts = arduino.ReadAnalog(ArduinoUnoAnalogPins.A0);

                // Read the state of a pin
                Pin p = arduino.GetCurrentPinState(ArduinoUnoPins.D2);

                lbResults.Items.Add(p.CurrentValue);

                // Write a digital value to an output pin
                arduino.SetPinMode(ArduinoUnoPins.D9_PWM, PinModes.Output);
                arduino.SetDO(ArduinoUnoPins.D9_PWM, true);

                System.Threading.Thread.Sleep(1000);
                arduino.SetDO(ArduinoUnoPins.D9_PWM, false);

                //// Write an analog value (PWM) to a PWM pin
                //arduino.SetPinMode(ArduinoUnoPins.D3_PWM, PinModes.PWM);
                //arduino.SetPWM(ArduinoUnoPWMPins.D3_PWM, 90);

                //// Use a servo
                //arduino.SetPinMode(ArduinoUnoPins.D9_PWM, PinModes.Servo);
                //arduino.SetServo(ArduinoUnoPins.D9_PWM, 90);

                // dispose of the object
                arduino.Dispose();

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetFrame();

        }

        public void GetFrame()
        {
            // enumerate video devices
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //foreach (FilterInfo item in videoDevices)
            //{

            //videoSource = new VideoCaptureDevice(item.MonikerString);
            videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
            //videoSource.DesiredFrameSize = new Size(160, 120);
                    
            // create video source
            //VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            // set NewFrame event handler
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            // start the video source
            videoSource.Start();
            // ...
            System.Threading.Thread.Sleep(500);
            Trace.WriteLine("FramesReceived: " + videoSource.FramesReceived);
            // signal to stop
            videoSource.SignalToStop();
            // ...
            //}
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();

            //// get new frame
            //Bitmap bitmap = eventArgs.Frame;
            //// process the frame
            //bitmap.Save(DateTime.Now.Ticks + ".png", ImageFormat.Png);
        }

        //prevent sudden close while device is running
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
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
    }
}
