using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsApis;
using WindowsApis.Data;
using WindowsApis;



namespace AutomationLib.AutomationControls.Controls
{
    public class Button : ButtonBase
    {
        

        public static string CLASSNAME = "BUTTON";
        public Button(IntPtr handle) : base(handle) { }
        public Button(IntPtr handle, string text) : base(handle, text)
        {            
        }
        public void ClickButton()
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_BUTTON.BM_CLICK, IntPtr.Zero, IntPtr.Zero.ToString());
        }
    }
}
