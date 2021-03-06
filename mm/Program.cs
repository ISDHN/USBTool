﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace mm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            StringBuilder i = new StringBuilder(4096, int.MaxValue);
            while (true)
            {
                try
                {
                    if (GetMemoryRate() < 97)
                    
                        i.Append(' ', 512);

                    if (GetMemoryRate() > 97)
                        i.Remove(0,512);
                    var usbdevice = from ud in DriveInfo.GetDrives()
                                    where ud.DriveType == DriveType.Removable
                                    select ud;
                    if (usbdevice.Count() == 0)
                    {
                        break;
                    }
                }
                catch
                {

                }
            }
        }
        public static double GetMemoryRate()
        {
            return 100 - (double)(new Microsoft.VisualBasic.Devices.Computer()).Info.AvailablePhysicalMemory / (new Microsoft.VisualBasic.Devices.Computer()).Info.TotalPhysicalMemory * 100;
        }
    }
}
