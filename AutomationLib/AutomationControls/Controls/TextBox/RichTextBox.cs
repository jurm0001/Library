using System;
using System.Collections.Generic;
using System.Text;

using WindowsApis;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class RichTextbox : Control
    {
        public static string CLASSNAME = "RICHEDIT";
        public RichTextbox(IntPtr handle, string text)
            : base(handle, text)
        {
        }
        #region Textbox Messages
        public void SendText(string text)
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_WINDOW.WM_SETTEXT, IntPtr.Zero, text);
        }
        public string GetText()
        {
            StringBuilder value = new StringBuilder(1024);
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_WINDOW.WM_GETTEXT, (IntPtr)1024, value);
            return value.ToString();
        }

        #endregion
    }
}
