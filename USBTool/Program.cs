using USBTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace USBTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //new System.Windows.Controls.MediaElement {Source = new Uri("D:\\Chevy\\Golden Wind.mp3") ,LoadedBehavior= System.Windows.Controls.MediaState.Manual,Volume=1}.Play();
            Application.Run(new Form1());
        }
    }
}
