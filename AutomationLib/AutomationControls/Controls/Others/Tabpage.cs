using System;
using System.Collections.Generic;
using System.Text;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class Tabpage : Control
    {
        public static string CLASSNAME = "SYSTABCONTROL32";
        public Tabpage(IntPtr handle) : base(handle)
        {            
        }
    }
}
