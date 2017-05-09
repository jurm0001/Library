using System;
using System.Collections.Generic;
using System.Text;

using WindowsApis;
using WindowsApis.Data;
using WindowsApis;
using System.Threading;
using System.Diagnostics;


namespace AutomationLib
{
    public class DialogChecker
    {       

        public IntPtr dialogHandle;

        private Thread workerThread = null;
        private int timeout = 0;
        private string caption = "";
        private string exename = "";
        private string buttonText = "";
        

        private Object _sender;
        public Object Sender
        {
            get { return this._sender; }
        }

        public event HandleDialog handleDialog;
        public delegate void HandleDialog(Object Sender, IntPtr Handle);

        public DialogChecker()
        {
            this._sender = null;
            this.dialogHandle = new IntPtr();
            this.dialogHandle = IntPtr.Zero;            
        }

        public void CheckForDialog(Object sender, string exename, int Timeout)
        {
            this._sender = sender;
            this.dialogHandle = IntPtr.Zero;
            this.exename = exename;
            this.timeout = Timeout;
            workerThread = new Thread(CheckForPopupThread);
            workerThread.Start(this);
            workerThread.Join();          
        }

        public void CheckForDialog(Object sender, string exename, int Timeout, string buttonText)
        {
            this._sender = sender;
            this.dialogHandle = IntPtr.Zero;
            this.exename = exename;
            this.timeout = Timeout;
            this.buttonText = buttonText;
            workerThread = new Thread(CheckForPopupThread);
            workerThread.Start(this);
            //workerThread.Join();
        }

        public void CheckForDialog(Object sender, string exename, string caption, int Timeout)
        {
            this._sender = sender;
            this.dialogHandle = IntPtr.Zero;
            this.exename = exename;
            this.caption = caption;
            this.timeout = Timeout;
            workerThread = new Thread(CheckForPopupThread);
            workerThread.Start(this);
            //workerThread.Join();                     
        }

        public void CheckForDialog(Object sender, string exename, string caption, int Timeout, string buttonText)
        {
            this._sender = sender;
            this.dialogHandle = IntPtr.Zero;
            this.exename = exename;
            this.caption = caption;
            this.timeout = Timeout;
            this.buttonText = buttonText;
            workerThread = new Thread(CheckForPopupThread);
            workerThread.Start(this);
            //workerThread.Join();
        }

        public void CheckForDialogJoin()
        {
            workerThread.Join();
        }

        public static void CheckForPopupThread(object data)
        {
            Thread.Sleep(2000);
            DialogChecker parms = (DialogChecker)data;
            DateTime dtStart = DateTime.Now;
            TimeSpan span = new TimeSpan((long)parms.timeout);
            
            while (true)
            {
                if (DateTime.Now - dtStart > span)
                    return;

                Thread.Sleep(0000);
                //DIAMOND® Client/Server System
                IntPtr parentHandle = User32.FindWindow("#32770", null);
                while ((long)parentHandle != 0)
                {

                    StringBuilder value = new StringBuilder(256);
                    User32.GetClassName(parentHandle, value, 256);

                    IntPtr handle = User32.GetWindow(parentHandle, (uint)WindowsApiEnums.GETWINDOW.GW_OWNER);
                    uint processID = 0;
                    long threadId = User32.GetWindowThreadProcessId(handle, out processID);
                    if (GetProcessName(processID).ToUpper().Equals(parms.exename.ToUpper()) && value.ToString().Equals("#32770"))
                    {
                        parms.dialogHandle = parentHandle;
                        if (parms.handleDialog != null)
                        {
                            parms.handleDialog(parms.Sender, parentHandle);
                        }
                        return;
                    }
                    parentHandle = User32.GetWindow(parentHandle, (uint)WindowsApiEnums.GETWINDOW.GW_HWNDNEXT);
                }
            }
            
        }

        #region public string GetProcessName(long processID)
        public static string GetProcessName(long processID)
        {
            return Process.GetProcessById((int)processID).ProcessName;
        }
        #endregion public string GetProcessName(long processID)

    }
}
