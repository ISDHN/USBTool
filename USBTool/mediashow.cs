using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
#if MEDIA_FOUNDATION
using USBTool.MediaFoundation;
#endif

namespace USBTool
{
	public partial class Mediashow
	{
		public IntPtr MCIWnd;
#if MEDIA_FOUNDATION
		public IMFVideoDisplayControl displayControl;
#endif	
        protected override void WndProc(ref Message m)
        {
			base.WndProc(ref m);
#if MEDIA_MCI
			if (m.Msg == (uint)FormMain.MCIConst.MCIWNDM_NOTIFYMODE) 
				if(m.LParam.ToInt32() == (uint)FormMain.MCIConst.MCI_MODE_STOP)
                    Hide();
			if (m.Msg == (uint)FormMain.MCIConst.MCIWNDM_NOTIFYERROR)
				Hide();
#endif
#if MEDIA_FOUNDATION
			if (m.Msg == FormMain.WM_PAINT)
				if (displayControl != null)
					displayControl.RepaintVideo();
#endif
		}

        private void Mediashow_Shown(object sender, EventArgs e)
        {

			if(MCIWnd!= null)
				FormMain.ShowWindow(MCIWnd, FormMain.SW_NORMAL);

		}

        private void Mediashow_Paint(object sender, PaintEventArgs e)
        {
#if MEDIA_FOUNDATION
				if (displayControl != null)
					displayControl.RepaintVideo();
#endif
		}
	}
}
