using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using DirectShow;

namespace USBTool
{
	public partial class Mediashow
	{
		public IntPtr MCIWnd;
		public void Play(string file, int rate, int volume)
		{		
			mel.Source = new Uri(file);
			if (rate >= 0)
			{
				mel.SpeedRatio = rate + 1;
			}
			else
			{				
				mel.SpeedRatio = (double)rate / 10 + 1;
			}
			mel.Volume = (double)volume / 100;
			mel.IsEnabled = true;			
			mel.Play();
			while (!mel.NaturalDuration.HasTimeSpan) { }
			if (mel.HasVideo)
				ShowDialog();
			else
				while (mel.Position != mel.NaturalDuration.TimeSpan) { }
			Hide();
		}
		
		public void mel_MediaEnded(object sender, RoutedEventArgs e)
		{
			Hide();
		}

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
    }
}
