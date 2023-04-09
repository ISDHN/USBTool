using System.Windows.Forms;
using USBTool.MediaFoundation;

namespace USBTool
{
    public partial class WndVideo : Form
    {
        public WndVideo()
        {
            InitializeComponent();
            Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth / 2;
            Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2;
        }
        public IMFVideoDisplayControl videocontrol;

        private void WndVideo_Paint(object sender, PaintEventArgs e)
        {
			videocontrol.RepaintVideo();
        }
        private void WndVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
