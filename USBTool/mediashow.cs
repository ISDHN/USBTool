using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
#if MEDIA_FOUNDATION
using USBTool.MediaFoundation;
#endif
namespace USBTool
{
	public partial class Mediashow
	{

#if MEDIA_MCI
		public IntPtr MCIWnd;
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == (uint)FormMain.MCIConst.MCIWNDM_NOTIFYMODE) 
				if(m.LParam.ToInt32() == (uint)FormMain.MCIConst.MCI_MODE_STOP)
					Hide();
			if (m.Msg == (uint)FormMain.MCIConst.MCIWNDM_NOTIFYERROR)
				Hide();
		}
		private void Mediashow_Shown(object sender, EventArgs e)
		{
			if(MCIWnd!= null)
				FormMain.ShowWindow(MCIWnd, FormMain.SW_NORMAL);
		}
#elif MEDIA_FOUNDATION
		public IMFVideoDisplayControl videocontrol;
		private void Mediashow_Paint(object sender, PaintEventArgs e)
		{
			videocontrol.RepaintVideo();
		}

#endif
		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

	}   
}
