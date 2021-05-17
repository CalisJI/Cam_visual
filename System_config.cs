using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcam
{
    public class System_config
    {
        public string Map_Path_File { get; set; }
        public string Output_File { get; set; }
        public string DefaultComport { get; set; }
        public string DefaultCOMBaudrate { get; set; }
        public System_config()
        {
        }
        public int Camera1 { get; set; }
        public int pixel_cam1 { get; set; }
        public int Bmin { get; set; }
        public int Bmax { get; set; }
        public int Gmin { get; set; }
        public int Gmax { get; set; }
        public int Rmin { get; set; }
        public int Rmax { get; set; }
        public int down_thresh { get; set; }
        public int up_thresh { get; set; }
        public Int32 ConveyorX { get; set; }
        public Int32 ConveyorY { get; set; }
        public Int32 ConveyorZ_H { get; set; }
        public Int32 ConveyorZ_L { get; set; }

        public Int32 NG_location { get; set; }
        public Int32[][] ouput_location { get; set; }
        public Int32[][] input_location { get; set; }
        public Int32 FrameH { get; set; }
        public Int32 FrameW { get; set; }
        //public Int32[,] Location { get; set; }
    }
}
