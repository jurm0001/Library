using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsApis;
using WindowsApis.Data;
using WindowsApis;



namespace AutomationLib.AutomationControls.Controls
{
    public class CheckBox : ButtonBase
    {
        public static string CLASSNAME = "CHECKBOX";
        public CheckBox(IntPtr handle) : base(handle) { }
        public CheckBox(IntPtr handle, string text)
            : base(handle, text)
        {
        }

        public bool IsChecked()
        {
            int state = User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_BUTTON.BM_GETSTATE, IntPtr.Zero, IntPtr.Zero.ToString());
            return state == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setChecked">false with make uncheck, true will check </param>
        public void Check(Boolean setChecked)
        {
            // get state
            int state = User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_BUTTON.BM_GETSTATE, IntPtr.Zero, IntPtr.Zero.ToString());
            if (setChecked)
            {
                if (state == 0)
                    User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_BUTTON.BM_SETCHECKED, IntPtr.Zero, IntPtr.Zero.ToString());
            }
            else
            {
                if (state == 1)
                    User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_BUTTON.BM_SETCHECKED, IntPtr.Zero, IntPtr.Zero.ToString());
            }
        }
    }
}
