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
			Application.ThreadExit += new EventHandler(OnClose);
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());      
			FormMain.MFShutdown();
		}
		static void OnClose(object sender,EventArgs e)
		{
			FormMain.MFShutdown();
		}
	}
}
