using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;

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
#endif
    }
}
