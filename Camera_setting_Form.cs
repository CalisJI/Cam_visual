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
    public partial class Camera_setting_Form : Form
    {
        private System_config system_config;
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice1;
        public Camera_setting_Form()
        {
            InitializeComponent();
           
        }

        private void Camera_setting_Form_Load(object sender, EventArgs e)
        {
            try
            {
                system_config = Program_Configuration.GetSystem_Config();
                label1.Text = "Index" + system_config.Camera1;
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (filterInfoCollection.Count > 0)
                {
                    foreach (FilterInfo filterInfo in filterInfoCollection)
                    {
                        Cambox1.Items.Add(filterInfo.Name);


                    }
                    Cambox1.SelectedIndex = 0;
                    videoCaptureDevice1 = new VideoCaptureDevice();
                    system_config = Program_Configuration.GetSystem_Config();
                    if (system_config.Camera1 < filterInfoCollection.Count) Cambox1.SelectedIndex = system_config.Camera1;
                }
                else
                {
                    Cambox1.Text = "NO CAMERA";
                    Cam1.Enabled = false;
                }
                if (videoCaptureDevice1 != null)
                {
                    system_config = Program_Configuration.GetSystem_Config();
                    if (system_config.pixel_cam1 < videoCaptureDevice1.VideoCapabilities.Length) comboBox1.SelectedIndex = system_config.pixel_cam1;

                }


                if (videoCaptureDevice1 != null && videoCaptureDevice1.VideoCapabilities.Length > 0)
                {
                    foreach (VideoCapabilities videoCapabilities in videoCaptureDevice1.VideoCapabilities)
                    {
                        comboBox1.Items.Add(videoCapabilities.FrameSize.Width.ToString() + "X" + videoCapabilities.FrameSize.Height.ToString());

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Cam1_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void Cam1_Click(object sender, EventArgs e)
        {
            Form Cam1_Form = new Form1(Cambox1.SelectedIndex);
            Cam1_Form.FormClosed += Cam1_Form_FormClosed;
            this.Hide();
            Cam1_Form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Save_success = true;
            if (Cambox1.Items.Count > 0)
            {
                Program_Configuration.UpdateSystem_Config("Camera1", Cambox1.SelectedIndex.ToString());
                Program_Configuration.UpdateSystem_Config("pixel_cam1", comboBox1.SelectedIndex.ToString());
            }
            else
            {
                MessageBox.Show("Camera1 is not available");
                Save_success = false;

            }
            system_config = Program_Configuration.GetSystem_Config();
            if (Save_success) MessageBox.Show("Camera setting are updated successfully");
            this.Close();
        }

        private void Cambox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load)
            {
                while (load)
                {
                    comboBox1.Items.Clear();
                    system_config.Camera1 = Cambox1.SelectedIndex;
                    Program_Configuration.UpdateSystem_Config("Camera1", Cambox1.SelectedIndex.ToString());
                    system_config = Program_Configuration.GetSystem_Config();
                    videoCaptureDevice1 = new VideoCaptureDevice(filterInfoCollection[system_config.Camera1].MonikerString);
                    if (videoCaptureDevice1.VideoCapabilities.Length > 0)
                    {
                        foreach (VideoCapabilities videoCapabilities in videoCaptureDevice1.VideoCapabilities)
                        {
                            comboBox1.Items.Add(videoCapabilities.FrameSize.Width.ToString() + "x" + videoCapabilities.FrameSize.Height.ToString());

                        }
                    }
                    Program_Configuration.UpdateSystem_Config("pixel_cam1", comboBox1.SelectedIndex.ToString());
                    label1.Text = "Index" + Cambox1.SelectedIndex.ToString();
                    load = false;
                    break;
                }

            }
        }
        bool load = false;

        private void Cambox1_MouseDown(object sender, MouseEventArgs e)
        {
            load = true;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            Cambox1.Items.Clear();

            foreach (FilterInfo filterInfo in filterInfoCollection)
            {

                Cambox1.Items.Add(filterInfo.Name);

            }
        }
    }
}
