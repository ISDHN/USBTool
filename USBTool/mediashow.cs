using System.Collections.Generic;
using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Speech.Synthesis;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using USBTool;

namespace USBTool
{

	public partial class mediashow
	{
		public MediaElement mel;
		private bool IsLoaded = false ;
		public void Play(string file, int rate, int volume)
		{
			this.Show();
			while (!IsLoaded){}                
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
			mel.Stop();
			mel.Play();
			if (!mel.HasVideo)
				Hide();
			while (mel.Position!=mel.NaturalDuration) { }
		}
		public void mel_MediaEnded(object sender, RoutedEventArgs e)
		{
			Hide();
		}

		private void mediashow_Load(object sender, EventArgs e)
		{
			IsLoaded = true;
		}
	}
}
