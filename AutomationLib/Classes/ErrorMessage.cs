using System;
using System.Collections.Generic;
using System.Text;

namespace AutomationLib
{
    #region public class ErrorMessage
    public class ErrorMessage
    {
        public string Appname;
        public int ProcessID;
        public string ErrorMsg;
        public IntPtr handle;
        public string buttonText;

        public ErrorMessage()
        {
        }
        public ErrorMessage(string appname, int pid, string msg, IntPtr handle, string buttonText)
        {
            this.Appname = appname;
            this.ProcessID = pid;
            this.ErrorMsg = msg;
            this.handle = handle;
            this.buttonText = buttonText;
        }
    }
    #endregion public class ErrorMessage
}
