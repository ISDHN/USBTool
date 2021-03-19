using System;
using System.Windows.Forms;

namespace USBTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnClose);
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.Run(new FormMain());
#if MEDIA_FOUNDATION
            FormMain.MFShutdown();
#endif
        }

        static void OnClose(object sender, EventArgs e)
        {
#if MEDIA_FOUNDATION
            FormMain.MFShutdown();
#endif

        }
    }
}
