using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutomationLib.AutomationControls;
using WindowsApis;

namespace AutomationLib.AutomationControls.Controls
{
    public class MenuItem : Control
    {        

        public MenuItem(IntPtr handle, string text, string classname, IntPtr parent, int message) : base(handle, text, classname, parent )
        {
            this.Message = message;
        }

        private int _Message;

        public int Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public int SendMenuMessage()
        {

            
            return User32.SendMessage(this.ParentHandle, (uint)WindowsApis.Data.WindowsApiEnums.SENDMESSAGE_WINDOW.WM_COMMAND, (uint)this.Message, (uint)0);

        }              
        
    }
}
