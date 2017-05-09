using System;
using System.Collections.Generic;
using System.Text;
using WindowsApis.Data;
using WindowsApis;
using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class Static : Control
    {
        public static string CLASSNAME = "STATIC";
        public Static(IntPtr handle, string text) : base(handle, text)            
        {            
        }
    }
}
