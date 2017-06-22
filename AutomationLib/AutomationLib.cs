
// /////////////////////////////////////////////////////////////
// File: AutomationLib.cs	Class: AutomationLib
// Date: 9/20/2008			Author: Michael Jury
// Language: C#				Framework: .NET
//
// Copyright: Copyright (c) Michael Jury, 2008-?
/////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using System.Threading;

// user created libraries
using WindowsApis;
using WindowsApis.Data;
using WindowsApis;
using System.Data.Linq;
using System.Linq;

using System.Reflection;
using System.Reflection.Emit;

using Auto = AutomationLib.AutomationControls.Controls;

using AC = AutomationLib.AutomationControls;

namespace AutomationLib
{
    public class WindowsAdapter
    {   
        #region Variables       
        protected Process ApplicationProcess = null;
        private IntPtr _MainHandle = IntPtr.Zero;

        public IntPtr MainHandle
        {
            get { return _MainHandle; }
            set { _MainHandle = value; }
        }
        public AC.ControlList<AC.Control> _ControlList;
        protected AC.ControlList<AC.Controls.MenuItem> _MenuControlList;
        public Boolean ExitThead = false;
        protected string AppName = "";
        #endregion

        

        #region Constructors
        public WindowsAdapter(string appname)
        {
            this.AppName = appname;
            _ControlList = new AC.ControlList<AC.Control>();
        }
        public WindowsAdapter(string appname, string eventLogSourceName)
        {
            this.AppName = appname;
            _ControlList = new AC.ControlList<AC.Control>();
            //this.eventLogWriter = new EventLogWriter(eventLogSourceName, EventLogWriter.EventViewerLogType.Application);
        }        
        #endregion

        #region FindWindow
        #region public IntPtr FindWindowHandleFromPartialTitle(string partialTitle)
        /// <summary>
        /// returns the handle to a top level application with partialTitle as part of the 
        /// windows entire title
        /// </summary>
        /// <param name="partialTitle">partial title</param>
        /// <returns>handle of window</returns>
        public IntPtr FindWindowHandleFromPartialTitle(string partialTitle)
        {
            Process[] list = Process.GetProcesses();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.IndexOf(partialTitle, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    //this.MainHandle = p.MainWindowHandle;                         
                    return p.MainWindowHandle;
                }
            }
            return (IntPtr)0;
        }
        #endregion public IntPtr FindWindowHandleFromPartialTitle(string partialTitle)



        #region public IntPtr FindWindowByCaption(string classname, string Caption)
        /// <summary>
        /// returns a window handle with specific caption or window title  and classname
        /// </summary>
        /// <param name="classname">classname / classtype of window</param>
        /// <param name="Caption">caption of window</param>
        /// <returns>handle to window</returns>
        public IntPtr FindWindowByCaption(string Caption)
        {
            //this.eventLogWriter.Write("Enter getWindowByCaption");
            DateTime dtStart = DateTime.Now;
            TimeSpan span = new TimeSpan(0, 0, 0, 0, 2000);

            IntPtr parentHandle = User32.FindWindowByCaption(0, Caption);
            while ((long)parentHandle == 0)
            {

                Thread.Sleep(100);
                parentHandle = User32.FindWindowByCaption(0, Caption);
            }
            parentHandle = User32.FindWindow(null, Caption);
            //this.eventLogWriter.Write("Exit getWindowByCaption - Success");
            return parentHandle;
        }
        #endregion public IntPtr FindWindowByCaption(string classname, string Caption)
        #endregion FindWindow


        public Boolean SetWindowHandleFromPartialTitle(string partialTitle)
        {
            Process[] list = Process.GetProcesses();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.IndexOf(partialTitle, 0, StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    this._MainHandle = p.MainWindowHandle;
                    return true;
                }
            }
            return false;
        }

        #region Start / shutdown
        #region protected void StartApplication(string exe)
        /// <summary>
        /// Starts an application
        /// </summary>
        /// <param name="exe">executable to execute</param>
        public void StartApplication(string exe)
        {
            ApplicationProcess = new Process();
            ApplicationProcess.StartInfo.FileName = exe;
            ApplicationProcess.Start();             
            Thread.Sleep(2000);
            while (ApplicationProcess.MainWindowHandle == IntPtr.Zero) ;

            this._MainHandle = ApplicationProcess.MainWindowHandle;
        }
        #endregion protected void StartApplication(string exe)

        #region protected void StartApplication(string exe, string args)
        /// <summary>
        /// Starts an application
        /// </summary>
        /// <param name="exe">executable to execute</param>
        /// <param name="args">arguments to send to executable</param>
        public void StartApplication(string exe, string args)
        {
            ApplicationProcess = new Process();
            ApplicationProcess.StartInfo.FileName = exe;
            ApplicationProcess.StartInfo.Arguments = args;

            ApplicationProcess.Start();
            Thread.Sleep(2000);
        }
        #endregion protected void StartApplication(string exe, string args)

        #region protected void StartApplication(string exe, string args, string workingDirectory)
        /// <summary>
        /// Starts an application
        /// </summary>
        /// <param name="exe">executable to execute</param>
        /// <param name="args">arguments to send to executable</param>
        public void StartApplication(string exe, string args, string workingDirectory)
        {
            ApplicationProcess = new Process();
            ApplicationProcess.StartInfo.FileName = exe;
            ApplicationProcess.StartInfo.Arguments = args;
            ApplicationProcess.StartInfo.WorkingDirectory = workingDirectory;
            ApplicationProcess.Start();
            Thread.Sleep(2000);
        }
        #endregion protected void StartApplication(string exe, string args, string workingDirectory)

        #region public void ShutDown()
        /// <summary>
        /// shut down function to kill all running threads
        /// </summary>
        public void ShutDown()
        {
            //ApplicationProcess.Close();
            ApplicationProcess.Kill();
        }
        #endregion public void ShutDown()
        #endregion Start / shutdown

        #region Methods       


        #region public int Wait()
        /// <summary>            
        /// functions to wait for single object to complete - wait time infinite - uses kernel WaitForSingleObject
        /// </summary>
        /// <returns>return value from WaitForSingleObject</returns>
        public int Wait()
        {
            uint ret = Kernel32.WaitForSingleObject(this.ApplicationProcess.Handle, Kernel32.INFINITE);
            return (int)ret;
        }
        #endregion public int Wait()

        #region public int Wait(uint timeout)
        /// <summary>
        /// functions to wait for single object to complete - wait time = timeout in miliseonds- uses kernel WaitForSingleObject
        /// </summary>
        /// <param name="timeout">timeout period in milliseconds</param>
        /// <returns>return value from WaitForSingleObject</returns>
        public int Wait(uint timeout)
        {
            uint ret = Kernel32.WaitForSingleObject(this.ApplicationProcess.Handle, timeout);
            Thread.Sleep(100);
            return (int)ret;
        }
        #endregion public int Wait(uint timeout)

        #region protected string GetProcessName(long processID)
        /// <summary>
        /// returns process name from PID
        /// </summary>
        /// <param name="processID">PID of process</param>
        /// <returns>name of process</returns>
        protected string GetProcessName(long processID)
        {
            return Process.GetProcessById((int)processID).ProcessName;
        }
        #endregion protected string GetProcessName(long processID)      


        #region private AC.ControlList<AC.Control> GetChildControls(IntPtr parent)
        private AC.ControlList<AC.Control> GetChildControls(IntPtr parent)
        {

            _ControlList = new AC.ControlList<AC.Control>();
            StringBuilder name = new StringBuilder(256);
            StringBuilder value = new StringBuilder(256);
            StringBuilder classname = new StringBuilder(256);

            User32.GetWindowText(parent, name, 256);
            User32.SendMessage(parent, (uint)WindowsApiEnums.SENDMESSAGE_WINDOW.WM_GETTEXT, (IntPtr)80, value);
            User32.GetClassName(parent, classname, 256);
            IntPtr parentP = User32.GetParent(parent);

            AC.Control control = new AC.Control(parent, value.ToString(), classname.ToString(), parentP);
            _ControlList.Add(control);
            GCHandle listHandle = GCHandle.Alloc(_ControlList);
            try
            {
                User32.EnumWindowProc childProc = new User32.EnumWindowProc(EnumControls);
                User32.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return _ControlList;
        }
        #endregion private AC.ControlList<AC.Control> GetChildControls(IntPtr parent)

        #region private static bool EnumControls(IntPtr handle, IntPtr pointer)

        private static bool EnumControls(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            StringBuilder name = new StringBuilder(256);
            StringBuilder value = new StringBuilder(256);
            StringBuilder classname = new StringBuilder(256);

            AC.ControlList<AC.Control> list = gch.Target as AC.ControlList<AC.Control>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }

            User32.GetWindowText(handle, name, 256);
            User32.SendMessage(handle, (uint) WindowsApiEnums.SENDMESSAGE_WINDOW.WM_GETTEXT, (IntPtr)80, value);
            User32.GetClassName(handle, classname, 256);
            IntPtr parent = User32.GetParent(handle);
            
            AC.Control control = new AC.Control(handle, value.ToString(), classname.ToString(), parent);
            WindowsApiStructs.WINDOWINFO f = GetWindowInfo(handle);
            if (classname.ToString().ToUpper().Contains(Auto.Tabpage.CLASSNAME))
            {
                ((AutomationControls.Control)control).ControlType = typeof(AC.Controls.Tabpage);
            }
            else if (classname.ToString().ToUpper().Contains(Auto.Listbox.CLASSNAME))
            {
                ((AutomationControls.Control)control).ControlType = typeof(AC.Controls.Listbox);
            }
            else if (classname.ToString().ToUpper().Contains(Auto.Textbox.CLASSNAME))
            {
                ((AutomationControls.Control)control).ControlType = typeof(AC.Controls.Textbox);
            }
            else if (classname.ToString().ToUpper().Contains(Auto.Button.CLASSNAME))
            {
                ((AutomationControls.Control)control).ControlType = typeof(AC.Controls.Button);
            }
            else if (classname.ToString().ToUpper().Contains(Auto.ComboBox.CLASSNAME))
            {
                ((AutomationControls.Control)control).ControlType = typeof(AC.Controls.ComboBox);
            }
            else
            {
                ((AutomationControls.Control)control).ControlType = null;
            }

            //ews.mappedName = "name";

            list.Add(control);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }
        #endregion private static bool EnumControls(IntPtr handle, IntPtr pointer)

        public void BuildControlList()
        {
            
            AC.ControlList<AC.Control> handleList = GetChildControls(this._MainHandle);
            GetMenu(this._MainHandle);
            ClickMenu("About");

        }

        public void BuildControlList(IntPtr Handle)
        {
            this._MainHandle = Handle;
            AC.ControlList<AC.Control> handleList = GetChildControls(Handle);
            GetMenu(Handle);
            ClickMenu("About");
           
        }

        AutomationControls.Control FindC;
        public AutomationLib.AutomationControls.Control GetControl(string text)
        {
            FindC = null;
            FindC = FindControl(text, this._ControlList[0]);
            return FindC;
        }

        private AutomationControls.Control FindControl(string name, AutomationControls.Control control)
        {
            if (((AutomationControls.Control)control).Text == name)
            {
                FindC = control;
                return control;
            }
            if (((AutomationControls.Control)control).ControlList == null || ((AutomationControls.Control)control).ControlList.Count == 0)
                return null;
            foreach (AutomationControls.Control c in ((AutomationControls.Control)control).ControlList)
            {
                
                this.FindControl(name, c);
            }
            return FindC;
        }

        #region protected void CloseWindowByHandle(IntPtr Handle)
        /// <summary>
        /// close a particular window by the handle
        /// </summary>
        /// <param name="Handle">handle of window to close</param>
        protected void CloseWindowByHandle(IntPtr Handle)
        {
            User32.SendMessage(Handle, (uint) WindowsApiEnums.SENDMESSAGE_WINDOW.WM_CLOSE, (IntPtr)0, "");
        }
        #endregion protected void CloseWindowByHandle(IntPtr Handle)

        #region public void printListing(string filename)
        /// <summary>
        /// used to pring the enumwindowsstruct list of objects return from getchildwindows
        /// a shortcut ... if filename = "desktop" the filename is sent directly to the
        /// desktop
        /// </summary>
        /// <param name="filename">filename to print output to</param> 
        public void printListing(string filename)
        {
            string userHomePath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "");

            if (filename.Equals("desktop"))
            {
                userHomePath += "\\EnumChildWindow.out";
                filename = userHomePath;
            }

            FileStream fsout = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fsout);
            //for (int i = 0; i < EnumResults.Count; ++i)
            //    sw.WriteLine("Index: " + i.ToString() + " " + EnumResults[i].ToString());
            sw.Close();
            fsout.Close();
        }
        #endregion public void printListing(string filename)

        

        

        #endregion Methods



        #region public WindowsApi.WINDOWINFO GetWindowInfo(IntPtr handle)
        /// <summary>
        /// gets the corresponding windowinfo from the handle
        /// </summary>
        /// <param name="handle">handle of window</param>
        /// <returns>windowinfo struct</returns>
        public  static WindowsApiStructs.WINDOWINFO GetWindowInfo(IntPtr handle)
        {
             WindowsApiStructs.WINDOWINFO inf = new  WindowsApiStructs.WINDOWINFO();
            inf.cbSize = Marshal.SizeOf(new  WindowsApiStructs.WINDOWINFO());
            User32.GetWindowInfo(handle, ref inf);
            return inf;
        }
        #endregion public WindowsApi.WINDOWINFO GetWindowInfo(IntPtr handle)

       

        #region Menu Manipulation
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicationHandle">application handle</param>
        /// <param name="menuPath">semi colon delimited path to menu item IE "&Special;Authorization &Waive/Match;&Manual Waive;"</param>
        /// <returns></returns>
        /// 
        public AC.ControlList<AC.Controls.MenuItem> GetMenu(IntPtr ApplicationHandle)
        {
            _MenuControlList = new AC.ControlList<AC.Controls.MenuItem>();            
            IntPtr menuhandle = WindowsApis.User32.GetMenu(ApplicationHandle);
            uint id = 0;
            EnumMenuItems(menuhandle, 0, IntPtr.Zero);            
            return _MenuControlList;
        }

        private void EnumMenuItems(IntPtr menuHandle, int arrIndex, IntPtr parent)
        {
            StringBuilder temp = new StringBuilder();

            int count = WindowsApis.User32.GetMenuItemCount(menuHandle.ToInt32());
            for (int i = 0; i < count; ++i)
            {                
                WindowsApis.User32.GetMenuString(menuHandle, (uint)i, temp, 0x20, 0x400);
                StringBuilder classname = new StringBuilder(256);

                //WindowsApis.Data.WindowsApiStructs.MENUITEMINFO mif = new WindowsApis.Data.WindowsApiStructs.MENUITEMINFO();
                //mif.cbSize = (uint)Marshal.SizeOf(typeof(WindowsApis.Data.WindowsApiStructs.MENUITEMINFO));
                //mif.fMask = 0x10;
                //mif.fType = 0;
                //mif.dwTypeData = null;
                //bool a = User32.GetMenuItemInfo(menuHandle, 0, true, ref mif);
                uint t = User32.GetMenuItemID(menuHandle, i);
                _MenuControlList.Add(new AC.Controls.MenuItem(menuHandle, temp.ToString(), "MenuItem", this._MainHandle, (int)t));

                
                Console.WriteLine(t.ToString());
                IntPtr newH = WindowsApis.User32.GetSubMenu(menuHandle, i);
                if (newH.ToInt32() > 0)
                {
                    EnumMenuItems(newH, arrIndex + 1, newH);
                }                  
            
            }
            return;
        }

        public void ClickMenu(string text)
        {
            int i = 0;
            AC.Controls.MenuItem item = this._MenuControlList[text] as AutomationControls.Controls.MenuItem;
            if(item!=null)
                i = item.SendMenuMessage();            
        }
       
        #endregion Menu Manipulation


         #region Mouse Functionality

        #region SetCursorPosition(int x, int y)
        /// <summary>
        /// sets the cursor position to a specific x,y cooridanate
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        public void SetCursorPosition(int x, int y)
        {
            User32.SetCursorPos(x, y);
        }
        #endregion SetCursorPosition(int x, int y)

        #region public void SetRelativeCursorPosition(IntPtr windowHandle, IntPtr controlHandle)
        /// <summary>
        /// not used
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="controlHandle"></param>
        public void SetRelativeCursorPosition(IntPtr windowHandle, IntPtr controlHandle)
        {
             WindowsApiStructs.WINDOWINFO winInfo = GetWindowInfo(windowHandle);
             WindowsApiStructs.WINDOWINFO controlInfo = GetWindowInfo(controlHandle);
        }
        #endregion public void SetRelativeCursorPosition(IntPtr windowHandle, IntPtr controlHandle)

        #region SendLeftMouseClick()
        /// <summary>
        /// send a left mouse click to the current mouse position
        /// </summary>
        public void SendLeftMouseClick()
        {
            WindowsApiStructs.INPUT structInput = new  WindowsApiStructs.INPUT();
            structInput.type =  WindowsApiConstants.INPUT_MOUSE;
            structInput.mkhi.mi.dx = 0;
            structInput.mkhi.mi.dy = 0;
            structInput.mkhi.mi.time = 0;
            structInput.mkhi.mi.dwFlags =  WindowsApiConstants.MOUSEEVENTF_LEFTDOWN;

            User32.SendInput(2, ref structInput, Marshal.SizeOf(new  WindowsApiStructs.INPUT()));
            structInput.mkhi.mi.dwFlags =  WindowsApiConstants.MOUSEEVENTF_LEFTUP;
            User32.SendInput(2, ref structInput, Marshal.SizeOf(new  WindowsApiStructs.INPUT()));
        }
        #endregion SendLeftMouseClick()

        #region SendRightMouseClick()
        /// <summary>
        /// send a right mouse click to the current mouse position
        /// </summary>
        public void SendRightMouseClick()
        {
             WindowsApiStructs.INPUT structInput = new  WindowsApiStructs.INPUT();
            structInput.type =  WindowsApiConstants.INPUT_MOUSE;
            structInput.mkhi.mi.dx = 0;
            structInput.mkhi.mi.dy = 0;
            structInput.mkhi.mi.time = 0;
            structInput.mkhi.mi.dwFlags =  WindowsApiConstants.MOUSEEVENTF_RIGHTDOWN;

            User32.SendInput(2, ref structInput, Marshal.SizeOf(new  WindowsApiStructs.INPUT()));
            structInput.mkhi.mi.dwFlags =  WindowsApiConstants.MOUSEEVENTF_RIGHTUP;
            User32.SendInput(2, ref structInput, Marshal.SizeOf(new  WindowsApiStructs.INPUT()));
        }
        #endregion SendRightMouseClick()
        #endregion Mouse Functionality

        #region Keyboard Functionality
        #region public void SendStringWait(IntPtr handle, string keys)
        /// <summary>
        /// sends a string to a window/control and waits for response
        /// </summary>
        /// <param name="handle">handle to window/control</param>
        /// <param name="keys">string value to send</param>
        public void  SendStringWait(IntPtr handle, string keys)
        {
            //this.eventLogWriter.Write("Enter SendStringWait");
            User32.SetForegroundWindow(handle);
            Thread.Sleep(100);
            System.Windows.Forms.SendKeys.SendWait(keys);
            Wait();
            //this.eventLogWriter.Write("Exit SendStringWait");
        }
        #endregion public void SendStringWait(IntPtr handle, string keys)

        #region public void SendString(IntPtr handle, string keys)
        /// <summary>
        /// send string to window/control and does not wait for response
        /// </summary>
        /// <param name="handle">handle to window</param>
        /// <param name="keys">string to send</param>
        public void SendString(IntPtr handle, string keys)
        {
            User32.SetForegroundWindow(handle);
            if (keys.Trim().Length == 0)
                return;

            Boolean functionKey = false;

            if (keys.Substring(0, 1).Equals("{") && keys.Substring(keys.Length - 1, 1).Equals("}"))
            {
                functionKey = true;
            }

            if (functionKey)
            {
                // send tab
                string function = keys.Substring(1, keys.Length - 2);
                User32.keybd_event((byte)ConvertStringToVK(function), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertStringToVK(function), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
            }
            else if (keys.Substring(0, 1).Equals("^"))
            {
                // send control n
                User32.keybd_event((byte) WindowsApiEnums.VK_KEYS.Control, 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertCharToVK(keys.Substring(1, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertCharToVK(keys.Substring(1, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
                User32.keybd_event((byte) WindowsApiEnums.VK_KEYS.Control, 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
            }
            else if (keys.Substring(0, 1).Equals("%"))
            {
                // send control n
                User32.keybd_event((byte) WindowsApiEnums.VK_KEYS.Menu, 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertCharToVK(keys.Substring(1, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertCharToVK(keys.Substring(1, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
                User32.keybd_event((byte) WindowsApiEnums.VK_KEYS.Menu, 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
            }
            else if (keys.Substring(0, 1).Equals("+"))
            {
                // send control n
                User32.keybd_event((byte) WindowsApiEnums.VK_KEYS.Shift, 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertCharToVK(keys.Substring(1, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                User32.keybd_event((byte)ConvertCharToVK(keys.Substring(1, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
                User32.keybd_event((byte) WindowsApiEnums.VK_KEYS.Shift, 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
            }
            else
            {
                for (int i = 0; i < keys.Length; ++i)
                {
                    User32.keybd_event((byte)ConvertCharToVK(keys.Substring(i, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY, 0);
                    User32.keybd_event((byte)ConvertCharToVK(keys.Substring(i, 1)), 0, (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_EXTENDEDKEY | (uint) WindowsApiEnums.KEYBOARDEVENTCONSTANTS.KEYEVENTF_KEYUP, 0);
                }
            }
            Thread.Sleep(50);

        }
        #endregion public void SendString(IntPtr handle, string keys)

        #region protected WindowsApi.VK_KEYS ConvertStringToVK(string s)
        protected  WindowsApiEnums.VK_KEYS ConvertStringToVK(string s)
        {
            s = s.ToUpper();
            switch (s)
            {
                case "TAB":
                    return  WindowsApiEnums.VK_KEYS.Tab;
                    
                case "F1":
                    return  WindowsApiEnums.VK_KEYS.F1;
                    
                case "F2":
                    return  WindowsApiEnums.VK_KEYS.F2;
                    
                case "F3":
                    return  WindowsApiEnums.VK_KEYS.F3;
                    
                case "F4":
                    return  WindowsApiEnums.VK_KEYS.F4;
                    
                case "F5":
                    return  WindowsApiEnums.VK_KEYS.F5;
                    
                case "F6":
                    return  WindowsApiEnums.VK_KEYS.F6;
                    
                case "F7":
                    return  WindowsApiEnums.VK_KEYS.F7;
                    
                case "F8":
                    return  WindowsApiEnums.VK_KEYS.F8;
                    
                case "F9":
                    return  WindowsApiEnums.VK_KEYS.F9;
                    
                case "F10":
                    return  WindowsApiEnums.VK_KEYS.F10;
                    
                case "F11":
                    return  WindowsApiEnums.VK_KEYS.F11;
                    
                case "F12":
                    return  WindowsApiEnums.VK_KEYS.F12;
                    
                case "F13":
                    return  WindowsApiEnums.VK_KEYS.F13;
                    
                case "F14":
                    return  WindowsApiEnums.VK_KEYS.F14;
                    
                case "F15":
                    return  WindowsApiEnums.VK_KEYS.F15;
                    
                case "F16":
                    return  WindowsApiEnums.VK_KEYS.F16;
                    
                case "F17":
                    return  WindowsApiEnums.VK_KEYS.F17;
                    
                case "ENTER":
                    return  WindowsApiEnums.VK_KEYS.Return;
                    
                case "ARROWDOWN":
                    return  WindowsApiEnums.VK_KEYS.Down;
                    
                case "ARROWUP":
                    return  WindowsApiEnums.VK_KEYS.Up;
                    

            }
            return  WindowsApiEnums.VK_KEYS.Space;
        }
        #endregion protected WindowsApi.VK_KEYS ConvertStringToVK(string s)

        #region protected WindowsApi.VK_KEYS ConvertCharToVK(char c)
        protected  WindowsApiEnums.VK_KEYS ConvertCharToVK(string s)
        {

            char[] c = s.ToCharArray();
            switch (c[0])
            {
                case 'A':
                case 'a':
                    return  WindowsApiEnums.VK_KEYS.A;
                    
                case 'B':
                case 'b':
                    return  WindowsApiEnums.VK_KEYS.B;
                    
                case 'C':
                case 'c':
                    return  WindowsApiEnums.VK_KEYS.C;
                    
                case 'D':
                case 'd':
                    return  WindowsApiEnums.VK_KEYS.D;
                    
                case 'E':
                case 'e':
                    return  WindowsApiEnums.VK_KEYS.E;
                    
                case 'F':
                case 'f':
                    return  WindowsApiEnums.VK_KEYS.F;
                    
                case 'G':
                case 'g':
                    return  WindowsApiEnums.VK_KEYS.G;
                    
                case 'H':
                case 'h':
                    return  WindowsApiEnums.VK_KEYS.H;
                    
                case 'I':
                case 'i':
                    return  WindowsApiEnums.VK_KEYS.I;
                    
                case 'J':
                case 'j':
                    return  WindowsApiEnums.VK_KEYS.J;
                    
                case 'K':
                case 'k':
                    return  WindowsApiEnums.VK_KEYS.K;
                    
                case 'L':
                case 'l':
                    return  WindowsApiEnums.VK_KEYS.L;
                    
                case 'M':
                case 'm':
                    return  WindowsApiEnums.VK_KEYS.M;
                    
                case 'N':
                case 'n':
                    return  WindowsApiEnums.VK_KEYS.N;
                    
                case 'O':
                case 'o':
                    return  WindowsApiEnums.VK_KEYS.O;
                    
                case 'P':
                case 'p':
                    return  WindowsApiEnums.VK_KEYS.P;
                    
                case 'Q':
                case 'q':
                    return  WindowsApiEnums.VK_KEYS.Q;
                    
                case 'R':
                case 'r':
                    return  WindowsApiEnums.VK_KEYS.R;
                    
                case 'S':
                case 's':
                    return  WindowsApiEnums.VK_KEYS.S;
                    
                case 'T':
                case 't':
                    return  WindowsApiEnums.VK_KEYS.T;
                    
                case 'U':
                case 'u':
                    return  WindowsApiEnums.VK_KEYS.U;
                    
                case 'V':
                case 'v':
                    return  WindowsApiEnums.VK_KEYS.V;
                    
                case 'W':
                case 'w':
                    return  WindowsApiEnums.VK_KEYS.W;
                    
                case 'X':
                case 'x':
                    return  WindowsApiEnums.VK_KEYS.X;
                    
                case 'Y':
                case 'y':
                    return  WindowsApiEnums.VK_KEYS.Y;
                    
                case 'Z':
                case 'z':
                    return  WindowsApiEnums.VK_KEYS.Z;
                    
                case '1':
                    return  WindowsApiEnums.VK_KEYS.Numpad1;
                    
                case '2':
                    return  WindowsApiEnums.VK_KEYS.Numpad2;
                    
                case '3':
                    return  WindowsApiEnums.VK_KEYS.Numpad3;
                    
                case '4':
                    return  WindowsApiEnums.VK_KEYS.Numpad4;
                    
                case '5':
                    return  WindowsApiEnums.VK_KEYS.Numpad5;
                    
                case '6':
                    return  WindowsApiEnums.VK_KEYS.Numpad6;
                    
                case '7':
                    return  WindowsApiEnums.VK_KEYS.Numpad7;
                    
                case '8':
                    return  WindowsApiEnums.VK_KEYS.Numpad8;
                    
                case '9':
                    return  WindowsApiEnums.VK_KEYS.Numpad9;
                    
                case '0':
                    return  WindowsApiEnums.VK_KEYS.Numpad0;
                    
                case '/':
                    return  WindowsApiEnums.VK_KEYS.Divide;
                    
                case '.':
                    return  WindowsApiEnums.VK_KEYS.Decimal;
                    
                case '-':
                    return  WindowsApiEnums.VK_KEYS.Subtract;
                    
            }
            return  WindowsApiEnums.VK_KEYS.Space;
        }
        #endregion protected WindowsApi.VK_KEYS ConvertCharToVK(char c)
        #endregion Keyboard Functionality       

        
    }

}