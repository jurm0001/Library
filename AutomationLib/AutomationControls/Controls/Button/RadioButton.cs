using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsApis;
using WindowsApis.Data;
using WindowsApis;



namespace AutomationLib.AutomationControls.Controls
{
    public class RadioButton : ButtonBase
    {
        public static string CLASSNAME = "BUTTONRADIO";
        public RadioButton(IntPtr handle) : base(handle) { }
        public RadioButton(IntPtr handle, string text)
            : base(handle, text)
        {
        }        
    }
}
