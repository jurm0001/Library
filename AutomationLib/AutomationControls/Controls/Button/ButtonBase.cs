using System;
using System.Collections.Generic;
using System.Text;



using WindowsApis;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class ButtonBase : Control
    {

        public static string CLASSNAME = "BUTTON";

        public static long BS_PUSHBUTTON = 0x00000000;
        public static long BS_DEFPUSHBUTTON = 0x00000001;

        public static long BS_CHECKBOX = 0x00000002;
        public static long BS_AUTOCHECKBOX = 0x00000003;

        public static long BS_RADIOBUTTON = 0x00000004;

        public static long BS_3STATE = 0x00000005;
        public static long BS_AUTO3STATE = 0x00000006;

        public static long BS_GROUPBOX = 0x00000007;
        public static long BS_USERBUTTON = 0x00000008;
        public static long BS_AUTORADIOBUTTON = 0x00000009;
        public static long BS_PUSHBOX = 0x0000000A;
        public static long BS_OWNERDRAW = 0x0000000B;
        public static long BS_TYPEMASK = 0x0000000F;
        public static long BS_LEFTTEXT = 0x00000020;

        public static long BS_TEXT = 0x00000000;
        public static long BS_ICON = 0x00000040;
        public static long BS_BITMAP = 0x00000080;
        public static long BS_LEFT = 0x00000100;
        public static long BS_RIGHT = 0x00000200;
        public static long BS_CENTER = 0x00000300;
        public static long BS_TOP = 0x00000400;
        public static long BS_BOTTOM = 0x00000800;
        public static long BS_VCENTER = 0x00000C00;
        public static long BS_PUSHLIKE = 0x00001000;
        public static long BS_MULTILINE = 0x00002000;
        public static long BS_NOTIFY = 0x00004000;
        public static long BS_FLAT = 0x00008000;
        public static long BS_RIGHTBUTTON = BS_LEFTTEXT;

        public ButtonBase(IntPtr handle)
            : base(handle)
        {
        }
        public ButtonBase(IntPtr handle, string text) : base(handle, text)
        {            
        }
        #region Button Messages

        public void GetText()
        {
            StringBuilder value = new StringBuilder(1024);
            User32.SendMessage(this.Handle, (uint)WindowsApiEnums.SENDMESSAGE_WINDOW.WM_GETTEXT, (IntPtr)1024, value);
            this.Text = value.ToString();
        }

        
        #endregion
    }
}
