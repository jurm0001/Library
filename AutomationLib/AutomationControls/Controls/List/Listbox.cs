using System;
using System.Collections.Generic;
using System.Text;

using WindowsApis;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class Listbox : Control
    {
        public static string CLASSNAME = "LISTBOX";
        public Listbox(IntPtr handle) : base(handle) 
        {            
        }
        #region Listbox Messages
        public string SetIndexByText(string text, Boolean exact)
        {            
            int index = 0;
            if (exact)
            {
                index = User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_FINDSTRINGEXACT, (IntPtr)index, text);
            }
            else
            {
                index = User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_FINDSTRING, (IntPtr)(-1), text);
            }
            if (index >= 0)
                SetIndex(index);        
            return "";
        }
        public string GetSelectedText()
        {
            int index = GetSelectedIndex();
            StringBuilder name2 = new StringBuilder(256);
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_GETTEXT, (IntPtr)index, name2);
            return name2.ToString();
        }
        public int GetCount()
        {
            int x = -1;
            x = User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);            
            return x;
        }
        public int GetSelectedTextLength()
        {            
            int len = 0;
            len = User32.SendMessage(this.Handle,
                          (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_GETTEXTLEN,
                          IntPtr.Zero, IntPtr.Zero);
            return len;
        }
        public int GetSelectedIndex()
        {            
            int index = 0;
            index = User32.SendMessage(this.Handle,
                          (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_GETCURSEL,
                          IntPtr.Zero, IntPtr.Zero);
            return index;
        }
        public void SetIndex(int index)
        {
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_LISTBOX.LB_SETCURSEL, (IntPtr)index, IntPtr.Zero);            
        }
        #endregion
    }
}
