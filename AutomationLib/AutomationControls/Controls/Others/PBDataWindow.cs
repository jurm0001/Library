using System;
using System.Collections.Generic;
using System.Text;

using WindowsApis;
using WindowsApis.Data;
using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class PBDataWindow : Control
    {
        public static string CLASSNAME = "PBDW";
        public PBDataWindow(IntPtr handle) : base(handle)
        {            
        }

        public string GetText()
        {
            StringBuilder value = new StringBuilder(1024);            
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_WINDOW.WM_GETTEXT, (IntPtr)1024, value);
            return value.ToString();
        }
    }
}
