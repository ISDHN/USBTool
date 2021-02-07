using System;
using System.Windows.Forms;


namespace USBTool
{
    public partial class SetAttrib
    {
        public SetAttrib()
        {
            InitializeComponent();
        }
        public static object[] Show(string mode)
        {
            var newform = new SetAttrib();
            newform.Choose.Items.Clear();
            switch (mode)
            {
                case "media":
                    newform.Open.Filter =
#if MEDIA_FOUNDATION
                    "媒体文件|*.3g2;*.3gp;*.3gp2;*.3gpp;*.asf;*.wma;*.wmv;*.aac;*.adts;*.avi;*.mp3;*.m4a;*.m4v;*.mov;*.mp4;*.sami;*.smi;*.wav"
#else
                        "媒体文件|*.mp3;*.wma;*.wmv;*.m3u;*.wav;*.avi;*.dvr-ms;*.gif;*.wtv"
#endif
                        ;
                    newform.SpeechVoice.Hide();
                    newform.SpeechText.Hide();
                    break;
                case "speech":
                    newform.SpeechText.Text = "奥利给";
                    newform.Choose.Hide();
                    newform.Browse.Hide();
                    foreach (var availableVoice in new System.Speech.Synthesis.SpeechSynthesizer().GetInstalledVoices())
                    {
                        newform.SpeechVoice.Items.Add(availableVoice.VoiceInfo.Name);
                    }
                    newform.SpeechVoice.SelectedIndex = 0;
                    break;
                case "picture":
                    newform.Open.Filter = "图像文件|*.png;*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.wmf;*.tif;*.tiff";
                    newform.SpeechVoice.Hide();
                    newform.SpeechText.Hide();
                    newform.Rate.Hide();
                    newform.Volume.Hide();
                    break;
                default:
                    throw (new ArgumentException("错误的初始化变量", mode));
            }
            if (newform.ShowDialog() == DialogResult.Yes)
            {
                if (mode == "speech")
                {
                    return new object[] { newform.Volume.Value, newform.Rate.Value, newform.SpeechVoice.SelectedItem.ToString(), newform.SpeechText.Text };
                }
                else if (mode == "media")
                {
                    return new object[] { newform.Volume.Value, newform.Rate.Value, newform.Choose.SelectedItem.ToString() };
                }
                else
                {
                    return new object[] { newform.Choose.SelectedItem.ToString() };
                }
            }
            else
            {
                return new object[] { };
            }
        }

        public void Yes_Click(object sender, EventArgs e)
        {
            if (Choose.SelectedItem != null || SpeechText.Text.Length != 0)
            {
                DialogResult = DialogResult.Yes;
            }
        }


        public void Browse_Click(object sender, EventArgs e)
        {
            if (Open.ShowDialog() != DialogResult.Cancel)
            {
                Choose.Items.Add(Open.FileName);
            }
        }
        public void Volume_ValueChanged(object sender, EventArgs e)
        {
            ToolTip1.SetToolTip(Volume, System.Convert.ToString(Volume.Value));
        }

        public void Volume_DragLeave(object sender, EventArgs e)
        {
            ToolTip1.SetToolTip(Volume, "音量");
        }

        public void Rate_ValueChanged(object sender, EventArgs e)
        {
            ToolTip1.SetToolTip(Rate, System.Convert.ToString(Rate.Value));
        }

        public void Rate_MouseLeave(object sender, EventArgs e)
        {
            ToolTip1.SetToolTip(Rate, "速度");
        }
    }
}
