using System;
using System.Collections.Generic;
using System.Text;
using WindowsApis;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class ComboBox : Control
    {
        public static string CLASSNAME = "COMBOBOX";
        public ComboBox(IntPtr handle, string text) : base(handle, text)
        {            
        }
        #region ComboBox Messages        
        public void SendText(string text)
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_WINDOW.WM_SETTEXT, IntPtr.Zero, text);            
        }

        public void AddItem(string item)
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_COMBOBOX.CB_ADDSTRING, IntPtr.Zero, item);
        }

        public void SetValue(string item)
        {
            int value = User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_COMBOBOX.CB_FINDSTRING, IntPtr.Zero, item);
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_COMBOBOX.CB_SETCURSEL, (uint)value, (uint)0);
        }

        public void SetValue(int item)
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_COMBOBOX.CB_SETCURSEL, (uint)item, (uint)0);
        }

        public void ClearItems()
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_COMBOBOX.CB_RESETCONTENT, (uint)0, (uint)0);
        }
        #endregion
    }
}
