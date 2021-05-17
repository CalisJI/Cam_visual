using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testcam
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            
                      
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             
            Main main = new Main();
            try
            {
                Application.Run(new Main());
            }
            catch (Exception ex)
            {
                //main.conti();
                MessageBox.Show( ex.Message);
            }
           
            //Application.Run(new Form1());
        }
    }
}
