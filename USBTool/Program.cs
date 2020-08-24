using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
#if MEDIA_FOUNDATION
			Application.ThreadExit += new EventHandler(OnClose);
#endif
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
#if MEDIA_FOUNDATION
			FormMain.MFShutdown();
#endif
		}
#if MEDIA_FOUNDATION
		static void OnClose(object sender,EventArgs e)
		{
			FormMain.MFShutdown();
		}
#endif
	}
}
