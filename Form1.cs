using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Testcam
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        private VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice();
        private int CameraIndex;       
        private int pixel;
        public Form1(int CameraIndex)
        {
            InitializeComponent();
            this.CameraIndex = CameraIndex;
            this.pixel = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.MinimizeBox = false;
            this.Text = filterInfoCollection[this.CameraIndex].Name;
            if (filterInfoCollection.Count > 0 && this.CameraIndex < filterInfoCollection.Count)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[this.CameraIndex].MonikerString);
                if (pixel < 0)
                {
                    MessageBox.Show("Please select your resolution first");
                    return;
                }
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[this.CameraIndex].MonikerString);
                //videoCaptureDevice.VideoResolution = videoCaptureDevice.VideoCapabilities[this.pixel];
                videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                videoCaptureDevice.Start();
            }
            else
            {
                MessageBox.Show("Not found Camera");
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            Bitmap bitmap = eventArgs.Frame.Clone() as Bitmap;
            pictureBox1.Image = bitmap;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }
    }
}
