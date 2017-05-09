using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

using WindowsApis;

namespace WindowsApis
{
    public class Psapi
    {
        [DllImport("psapi.dll")]
        public static extern uint GetModuleBaseName(IntPtr hProcess, uint hModule, out string lpBaseName, uint nSize);

        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool EnumProcessModules(IntPtr hProcess,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In][Out] uint[] lphModule, uint cb, [MarshalAs(UnmanagedType.U4)] out uint lpcbNeeded);


    }
}
