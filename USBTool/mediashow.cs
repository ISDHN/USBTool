using System;
using System.Windows;
using System.Windows.Controls;

namespace USBTool
{

    public partial class mediashow
    {
        public MediaElement mel;
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
            mel.Stop();
            while (!mel.NaturalDuration .HasTimeSpan) { }
            mel.Play();

            if (mel.HasVideo)
                ShowDialog();
            else
                while (mel.Position != mel.NaturalDuration.TimeSpan) { }
        }
        public void mel_MediaEnded(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        public void mel_MediaOpened(object sender, RoutedEventArgs e)
        {

        }
        private void mediashow_Load(object sender, EventArgs e)
        {
        }
    }
}
