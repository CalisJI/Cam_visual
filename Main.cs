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
using System.IO;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Drawing.Imaging;

namespace Testcam
{
    public partial class Main : Form
    {
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice Cam1VIDEO_Device;
        private FilterInfo Cam1_Device;
        System_config system_config;
        Bitmap Live_Cam_1;
        private BackgroundWorker backgroundWorker_1 = new BackgroundWorker();
        Int32[,] save = new Int32[,] { };
        public Main()
        {
            InitializeComponent();
        }
        private void set_up()
        {
            if (!Directory.Exists(system_config.Map_Path_File + @"/" + Parameter_app.IMAGE_FOLDER_NAME))
            {
                try
                {
                    Directory.CreateDirectory(system_config.Map_Path_File + @"/" + Parameter_app.IMAGE_FOLDER_NAME);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }
        int process_max = 0;
        private void Main_Load(object sender, EventArgs e)
        {
            system_config = Program_Configuration.GetSystem_Config();
            set_up();
           
            backgroundWorker_1.DoWork += BackgroundWorker_1_DoWork;
            backgroundWorker_1.RunWorkerCompleted += BackgroundWorker_1_RunWorkerCompleted;
            backgroundWorker_1.WorkerSupportsCancellation = true;
            using (StreamReader sr = new StreamReader("Travel_machine.txt")) 
            {
                process_max = 0;
                while (sr.ReadLine() != null) 
                {
                    process_max++;
                }
            }
            save = read_loca(process_max);
            system_config = Program_Configuration.GetSystem_Config();
            trackB2.Value = system_config.Bmin;
            Tb_Bmin.Text = system_config.Bmin.ToString();
            trackBmax.Value = system_config.Bmax;
            TB_Bmax.Text = system_config.Bmax.ToString();
            trackG2.Value = system_config.Gmin;
            Tb_Gmin.Text = system_config.Gmin.ToString();
            trackGmax.Value = system_config.Gmax;
            TB_Gmax.Text = system_config.Gmax.ToString();
            trackR2.Value = system_config.Rmin;
            Tb_Rmin.Text = system_config.Rmin.ToString();
            trackRmax.Value = system_config.Rmax;
            TB_Rmax.Text = system_config.Rmax.ToString();
            Tb_upper.Text = system_config.up_thresh.ToString();
            Tb_lower.Text = system_config.down_thresh.ToString();
            
        }
        Boolean shot = false;
        private void BackgroundWorker_1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                taked1 = false;
                if (shot)
                {
                    Arduino_commute.Write("OK;");
                    status(" [SYSTEM]" + " Image Processing Completed [OK]");
                }
                if (!shot)
                {
                    Arduino_commute.Write("E;");
                    status(" [SYSTEM]" + " Image Processing Completed [ERROR]");
                }

                Live_Cam_1.Dispose();
                order_1 = false;
            }
            catch ( Exception ex)
            {

                status(ex.Message);
                MessageBox.Show(this, ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void BackgroundWorker_1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker_1.CancellationPending)
            {
                e.Cancel = true;
            }
            DateTime date = DateTime.Now;
            date.ToString("HH:MM:ss");

            string str = date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString() + "-" + date.Hour.ToString() + "-" + date.Minute.ToString() + "-" + date.Second.ToString() + ".jpeg";
            string outputFileName = system_config.Map_Path_File + @"\" + Parameter_app.IMAGE_FOLDER_NAME + "/" + str + "";


            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {

                    try
                    {
                        Live_Cam_1.Save(memory, ImageFormat.Jpeg);
                        Image<Bgr, byte> toimg = new Image<Bgr, byte>(Live_Cam_1);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Dispose();
                        if (File.Exists(outputFileName))
                        {
                            shot = check(toimg);
                           
                            status("[IMAGE PROCCESSING] " + count_red.ToString() + " ");
                            count_red = 0;
                        }
                        toimg.Dispose();
                    }
                    catch (Exception ex)
                    {
                        status("[Exception] " + ex.Message);
                        MessageBox.Show(ex.Message);
                       
                    }

                }
            }
        }
        private void status(string text)
        {
            MethodInvoker inv = delegate
            {
                textBox_stt.AppendText("[" + DateTime.Now.ToString() + "]" + text + Environment.NewLine);
            };
            this.Invoke(inv);
        }

        private void cOMPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectCOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Com_setting_form = new Com_setting();
            Com_setting_form.FormClosed += (object sender4, FormClosedEventArgs e4) =>
            {
                this.Show();
            };
            this.Hide();
            Com_setting_form.Show();
        }
        bool connect = false;
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            system_config = Program_Configuration.GetSystem_Config();
            if (!connect)
            {
                try
                {
                    if (Arduino_commute.IsOpen) Arduino_commute.Close();
                    Arduino_commute.PortName = system_config.DefaultComport;
                    Arduino_commute.BaudRate = Convert.ToInt32(system_config.DefaultCOMBaudrate);

                    Arduino_commute.Open();
                    status("[COMPORT] Comport " + Arduino_commute.PortName + " Connected");
                    connect = true;
                    connectToolStripMenuItem.Text = "Disconnect";
                }
                catch (Exception)
                {
                    MessageBox.Show(system_config.DefaultComport + " Not Existing, please try another one");
                    status(" [COMPORT] Comport " + Arduino_commute.PortName + " Not found");

                    return;
                }
            }
            else
            {
                if (Arduino_commute.IsOpen) Arduino_commute.Close();
                status(" [COMPORT] Comport " + Arduino_commute.PortName + " Disconnected");
                connect = false;
                connectToolStripMenuItem.Text = "Connect";
            }

        }

        private void HOMING_btn_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("H;");
        }
        Int64 count_red = 0;
        Boolean OK = false;
        private Boolean check(Image<Bgr, byte> imgInput)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();

            }
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();

            }

            _ = imgInput.Convert<Gray, Byte>();

            Image<Gray, byte> grayImg = imgInput.InRange(new Bgr(system_config.Bmin, system_config.Gmin, system_config.Rmin), new Bgr(system_config.Bmax, system_config.Gmax, system_config.Rmax));
            pictureBox1.Image = grayImg.Bitmap;
            pictureBox2.Image = imgInput.Bitmap;
            for (int j = 0; j < grayImg.Height; j++)
            {
                for (int i = 0; i < grayImg.Width; i++)
                {
                    if (grayImg.Data[j, i, 0] == 255)
                        count_red++;
                }
            }
            if (count_red > system_config.down_thresh && count_red < system_config.up_thresh) OK = true;
            else OK = false;
            grayImg.Dispose();
            imgInput.Dispose();
            return OK;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Camera_setting_form = new Camera_setting_Form();
            Camera_setting_form.FormClosed += Camera_setting_form_FormClosed;
            this.Hide();
            Camera_setting_form.Show();
        }
        private void Camera_setting_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
        private void Start_program()
        {
            system_config = Program_Configuration.GetSystem_Config();
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (system_config.Camera1 < filterInfoCollection.Count) Cam1_Device = filterInfoCollection[system_config.Camera1];
            if (Cam1_Device == null)
            {
                MessageBox.Show("Camera 1 is not available, please check connection setting of device and preview");
                status("[START Camera 1 is not availble]");
                return;
            }
            if (!Directory.Exists(system_config.Map_Path_File))
            {
                MessageBox.Show("Could not find Map Path File, please check setting again");
                status("[START] Could not find Map Path File");
                Start_btn.Enabled = true;
                Stop_btn.Enabled = false;
                return;
            }
            try
            {
                if (Arduino_commute.IsOpen) Arduino_commute.Close();
                Arduino_commute.PortName = system_config.DefaultComport;
                Arduino_commute.BaudRate = Convert.ToInt32(system_config.DefaultCOMBaudrate);

                Arduino_commute.Open();
                status("[COMPORT] Comport " + Arduino_commute.PortName + " Connected");

            }
            catch (Exception)
            {
                MessageBox.Show(system_config.DefaultComport + " Not Existing, please try another one");
                status(" [COMPORT] Comport " + Arduino_commute.PortName + " Not found");

                return;
            }
            try
            {
                if (Cam1VIDEO_Device == null || !Cam1VIDEO_Device.IsRunning)
                {
                    Cam1VIDEO_Device = new VideoCaptureDevice(Cam1_Device.MonikerString);
                    //Cam1VIDEO_Device.VideoResolution = Cam1VIDEO_Device.VideoCapabilities[system_config.pixel_cam1];
                    Cam1VIDEO_Device.NewFrame += Cam1VIDEO_Device_NewFrame;
                    Cam1VIDEO_Device.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            Start_btn.Enabled = false;
            Stop_btn.Enabled = true;

            started = true;

            status("[START]" + "Program has been started");
        }
        bool started = false;
        bool taked1 = false;
        bool order_1 = false;

        private void Start_btn_Click(object sender, EventArgs e)
        {
            Start_program();
        }
        private void Cam1VIDEO_Device_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (order_1)
            {
                if (!taked1)
                {
                    Live_Cam_1 = (Bitmap)eventArgs.Frame.Clone();
                    taked1 = true;
                    if (!backgroundWorker_1.IsBusy) backgroundWorker_1.RunWorkerAsync();
                }
            }
            else if (Live_Cam_1 != null)
            {

                taked1 = false;
                Live_Cam_1.Dispose();
            }
            Cam1VIDEO_Device.SignalToStop();

        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            if (backgroundWorker_1.IsBusy) backgroundWorker_1.CancelAsync();
            if (Cam1VIDEO_Device != null && Cam1VIDEO_Device.IsRunning)
            {
                Cam1VIDEO_Device.Stop();
            }
            if (Arduino_commute.IsOpen) Arduino_commute.Close();
            started = false;
            Start_btn.Enabled = true;
            Stop_btn.Enabled = false;
            status("[START]" + "Program has been stopped");
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            update_configuration();
            MessageBox.Show("Update successfully");
        }
        private void update_configuration()
        {
            system_config = Program_Configuration.GetSystem_Config();
            Program_Configuration.UpdateSystem_Config("Bmin", trackB2.Value.ToString());
            Program_Configuration.UpdateSystem_Config("Bmax", trackBmax.Value.ToString());
            Program_Configuration.UpdateSystem_Config("Gmin", trackG2.Value.ToString());
            Program_Configuration.UpdateSystem_Config("Gmax", trackGmax.Value.ToString());
            Program_Configuration.UpdateSystem_Config("Rmin", trackR2.Value.ToString());
            Program_Configuration.UpdateSystem_Config("Rmax", trackRmax.Value.ToString());

            Program_Configuration.UpdateSystem_Config("down_thresh", Tb_lower.Text);
            Program_Configuration.UpdateSystem_Config("up_thresh", Tb_upper.Text);
            using (StreamReader sr = new StreamReader("Travel_machine.txt"))
            {
                process_max = 0;
                while (sr.ReadLine() != null)
                {
                    process_max++;
                }
            }

        }

        private void trackB2_Scroll(object sender, EventArgs e)
        {
            Tb_Bmin.Text = trackB2.Value.ToString();
        }

        private void Tb_Bmin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackB2.Value = Convert.ToInt32(Tb_Bmin.Text);
            }
            catch (Exception)
            {
                trackB2.Value = 0;
                Tb_Bmin.Text = "0";
                MessageBox.Show("Value not invalid (value between 0~255)");
            }
        }

        private void trackG2_Scroll(object sender, EventArgs e)
        {
            Tb_Gmin.Text = trackG2.Value.ToString();
        }

        private void Tb_Gmin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackG2.Value = Convert.ToInt32(Tb_Gmin.Text);
            }
            catch (Exception)
            {
                trackG2.Value = 0;
                Tb_Gmin.Text = "0";
                MessageBox.Show("Value not invalid (value between 0~255)");
            }
        }

        private void trackR2_Scroll(object sender, EventArgs e)
        {
            Tb_Rmin.Text = trackR2.Value.ToString();
        }

        private void Tb_Rmin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackR2.Value = Convert.ToInt32(Tb_Rmin.Text);
            }
            catch (Exception)
            {
                trackR2.Value = 0;
                Tb_Rmin.Text = "0";
                MessageBox.Show("Value not invalid (value between 0~255)");
            }
        }

        private void trackBmax_Scroll(object sender, EventArgs e)
        {
            TB_Bmax.Text = trackBmax.Value.ToString();
        }

        private void TB_Bmax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackBmax.Value = Convert.ToInt32(TB_Bmax.Text);
            }
            catch (Exception)
            {
                trackBmax.Value = 0;
                TB_Bmax.Text = "0";
                MessageBox.Show("Value not invalid (value between 0~255)");
            }
        }

        private void trackGmax_Scroll(object sender, EventArgs e)
        {
            TB_Gmax.Text = trackGmax.Value.ToString();
        }

        private void TB_Gmax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackGmax.Value = Convert.ToInt32(TB_Gmax.Text);
            }
            catch (Exception)
            {
                trackGmax.Value = 0;
                TB_Gmax.Text = "0";
                MessageBox.Show("Value not invalid (value between 0~255)");
            }
        }

        private void trackRmax_Scroll(object sender, EventArgs e)
        {
            TB_Rmax.Text = trackRmax.Value.ToString();
        }

        private void TB_Rmax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                trackRmax.Value = Convert.ToInt32(TB_Rmax.Text);
            }
            catch (Exception)
            {
                trackRmax.Value = 0;
                TB_Rmax.Text = "0";
                MessageBox.Show("Value not invalid (value between 0~255)");
            }
        }

        private void pATHFILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (started)
            {
                MessageBox.Show("Please stop program first!");
                return;
            }
            Form Path_form = new Path_File();
            Path_form.FormClosed += (object sender3, FormClosedEventArgs e3) =>
            {
                this.Show();
            };
            this.Hide();
            Path_form.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker_1.IsBusy) backgroundWorker_1.CancelAsync();
            if (Cam1VIDEO_Device != null && Cam1VIDEO_Device.IsRunning)
            {
                Cam1VIDEO_Device.Stop();
            }
            started = false;
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            system_config = Program_Configuration.GetSystem_Config();
            trackB2.Value = system_config.Bmin;
            Tb_Bmin.Text = system_config.Bmin.ToString();
            trackBmax.Value = system_config.Bmax;
            TB_Bmax.Text = system_config.Bmax.ToString();
            trackG2.Value = system_config.Gmin;
            Tb_Gmin.Text = system_config.Gmin.ToString();
            trackGmax.Value = system_config.Gmax;
            TB_Gmax.Text = system_config.Gmax.ToString();
            trackR2.Value = system_config.Rmin;
            Tb_Rmin.Text = system_config.Rmin.ToString();
            trackRmax.Value = system_config.Rmax;
            TB_Rmax.Text = system_config.Rmax.ToString();
            Tb_upper.Text = system_config.up_thresh.ToString();
            Tb_lower.Text = system_config.down_thresh.ToString();
        }

        private void Para_conveyor_Click(object sender, EventArgs e)
        {
            try
            {
                string[] parameter = new string[4];
                parameter = Conveyor_TB.Text.Split(',');
                Program_Configuration.UpdateSystem_Config("ConveyorX", parameter[0]);
                Program_Configuration.UpdateSystem_Config("ConveyorY", parameter[1]);
                Program_Configuration.UpdateSystem_Config("ConveyorZ_H", parameter[2]);
                Program_Configuration.UpdateSystem_Config("ConveyorZ_L", parameter[3]);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Value is not valid");
            }
        }

        private void Read_convey_btn_Click(object sender, EventArgs e)
        {
            system_config = Program_Configuration.GetSystem_Config();
            Conveyor_TB.Text = system_config.ConveyorX.ToString() + "," + system_config.ConveyorY.ToString() + "," + system_config.ConveyorZ_H.ToString() + "," + system_config.ConveyorZ_L.ToString();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("C;X;" + X_Position.Text + ";" + "Y;" + Y_Position.Text + ";" + "Z;" + Z_Position.Text + ";");

        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("AD;" + convey_tb_set.Text + ";");
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("ND;" + convey_tb_set.Text + ";");
        }
        int process = 0;
       
        int y_d = 0;
       
        int y_s = 0;
        
        int y_m = 0;
        private void Arduino_commute_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string cap_order = Arduino_commute.ReadExisting();
            
            if (cap_order == "1")
            {
                order_1 = true;
                Take_photo();
            }
            status("[RS232] " + cap_order + "");
            if (y_m < process_max - 1 && y_d < process_max - 1 && y_s < process_max - 1)  
            {
                switch (cap_order)
                {
                    case "H":
                        y_m = 0;
                        y_d = 0;
                        y_s = 0;
                        picking = true;
                        caming = false;
                        checking1 = false;
                        checking2 = false;
                        move(y_m, 0, "MOVE");
                        break;
                    case "M":
                        picking = false;
                        caming = true;
                        checking1 = false;
                        checking2 = false;
                        move(1, 1, "C");
                        break;
                    case "B":
                        y_m++;
                        picking = true;
                        caming = false;
                        checking1 = false;
                        checking2 = false;
                        move(y_m, 0, "MOVE");
                        break;
                    case "O":
                        y_d++;
                        picking = false;
                        caming = false;
                        checking1 = true;
                        checking2 = false;
                        move(y_d, 4, "MOVE");
                        break;
                    case "N":
                        y_s++;
                        picking = false;
                        caming = false;
                        checking1 = false;
                        checking2 = true;
                        move(y_s, 8, "MOVE");
                        break;
                }
            }
            
           
            MethodInvoker inv = delegate
            {
                label24.Text = process.ToString();
            };this.Invoke(inv);
            Arduino_commute.DiscardInBuffer();
        }

        bool picking = false;
        bool caming = false;
        bool checking1 = false;
        bool checking2 = false;
        private void move(int T,int X,string mode) 
        {
            MethodInvoker inv = delegate
            {
                
                //if (save[T, 0] == 6) 
                //{
                //    if (!Arduino_commute.IsOpen) return;
                //    Arduino_commute.Write("C;X;" + save[T, 1].ToString() + ";" + "Y;" + save[T, 2].ToString() + ";" + "Z;" + save[T, 3].ToString() + ";");

                //}
                //if (save[T, 0] == 1) 
                //{
                //    if (!Arduino_commute.IsOpen) return;
                //    Arduino_commute.Write("M;X;" + save[T, 1].ToString() + ";" + "Y;" + save[T, 2].ToString() + ";" + "Z;" + save[T, 3].ToString() + ";");

                //}
                if(mode == "MOVE") 
                {
                    switch (save[T, X])
                    {
                        case 1:
                            if (!Arduino_commute.IsOpen) return;
                            Arduino_commute.Write("M;X;" + save[T, 1].ToString() + ";" + "Y;" + save[T, 2].ToString() + ";" + "Z;" + save[T, 3].ToString() + ";");
                            break;
                        case 2:
                            if (!Arduino_commute.IsOpen) return;
                            Arduino_commute.Write("O;X;" + save[T, 5].ToString() + ";" + "Y;" + save[T, 6].ToString() + ";" + "Z;" + save[T, 7].ToString() + ";");
                            break;
                        case 3:
                            if (!Arduino_commute.IsOpen) return;
                            Arduino_commute.Write("N;X;" + save[T, 9].ToString() + ";" + "Y;" + save[T, 10].ToString() + ";" + "Z;" + save[T, 11].ToString() + ";");
                            break;

                    }
                }
               
                if (mode == "C") 
                {                  
                        if (!Arduino_commute.IsOpen) return;
                        Arduino_commute.Write("C;X;" + 2660.ToString() + ";" + "Y;" + 1750.ToString() + ";" + "Z;" + 2000.ToString() + ";");                     
                }
                
            }; this.Invoke(inv);
        }
        private void Take_photo() 
        {
            MethodInvoker inv = delegate 
            {
                if (Cam1VIDEO_Device == null || !Cam1VIDEO_Device.IsRunning)
                {
                    Cam1VIDEO_Device = new VideoCaptureDevice(Cam1_Device.MonikerString);
                    //Cam1VIDEO_Device.VideoResolution = Cam1VIDEO_Device.VideoCapabilities[system_config.pixel_cam1];
                    Cam1VIDEO_Device.NewFrame += Cam1VIDEO_Device_NewFrame;
                    Cam1VIDEO_Device.Start();
                }
            };this.Invoke(inv);
        }
        private void Test_btn_Click(object sender, EventArgs e)
        {
            conti();
        }
        public void conti() 
        {

            if (caming) 
            {
                move(1, 1, "C");
            }
            if (picking)
            {
                move(y_m, 0, "MOVE");
            }
            if (checking1)
            {
                move(y_d, 4, "MOVE");
            }
            if (checking2)
            {
                move(y_s, 8, "MOVE");
            }
        }
        
        private void save_location(Int32[,] loca) 
        {
          
            using (StreamWriter sw = new StreamWriter("Travel_machine.txt"))
            {
                for (int i = 0; i < loca.GetLength(0); i++)
                {
                    for (int j = 0; j < loca.GetLength(1); j++)
                    {
                        sw.Write(loca[i, j] + " ");
                    }
                    sw.Write(new char[] {'\r'});
                }
            }
        }
        private Int32[,] read_loca(int leng)
        {
            
            Int32[,] loca = new Int32[leng,12];
            int count_line = 0;
            using (StreamReader sr = new StreamReader("Travel_machine.txt"))
            {
                while (sr.ReadLine() != null) 
                {
                    count_line++;
                }
            }
            using (StreamReader sr = new StreamReader("Travel_machine.txt"))
            {
                
                
                string[] r = new string[12];
                for(int j = 0; j < count_line; j++) 
                {
                    r = sr.ReadLine().Split(' ');
                    for (int i = 0; i < 12; i++)
                    {                                            
                        loca[j, i] = int.Parse(r[i]);
                    }
                   
                }
             
            }

                return loca;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            update_configuration();
            using (StreamReader sr = new StreamReader("Travel_machine.txt"))
            {
                process_max = 0;
                while (sr.ReadLine() != null)
                {
                    process_max++;
                }
            }
            save = read_loca(process_max);
            y_m = 0;
            y_s = 0;
            y_d = 0;
            move(y_m, 0, "MOVE");

        }

        private void X_move_btn_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("T;X;" + X_Position.Text + ";" + "Y;" + Y_Position.Text + ";" + "Z;" + Z_Position.Text + ";");

        }

        private void Y_move_btn_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("T;X;" + X_Position.Text + ";" + "Y;" + Y_Position.Text + ";" + "Z;" + Z_Position.Text + ";");

        }

        private void Z_move_btn_Click(object sender, EventArgs e)
        {
            if (!Arduino_commute.IsOpen) return;
            Arduino_commute.Write("T;X;" + X_Position.Text + ";" + "Y;" + Y_Position.Text + ";" + "Z;" + Z_Position.Text + ";");

        }
    }
}
