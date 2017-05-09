using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

using WindowsApis.Data;

namespace WindowsApis
{
    public class User32
    {        
        #region GetWindow
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WindowsApiStructs.WINDOWINFO wi);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref WindowsApiStructs.RECT rec);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowsApiStructs.WINDOWPLACEMENT wp);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr GetNextWindow(long hWnd, WindowsApiEnums.WINDOWINFOCMD wCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("User32.dll")]
        public static extern int GetWindow(int hwndSibling, int wFlag);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        //[DllImport("user32.dll")]
        //public static extern int GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);
        #endregion GetWindow

        #region Keyboard
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
           int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern byte VkKeyScan(char ch);
        #endregion Keyboard

        #region Get Functions

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDlgItem(HandleRef hWnd, int nIDDlgItem);

        [DllImport("user32.dll")]
        public static extern bool GetGUIThreadInfo(uint idThread, out WindowsApiStructs.GUITHREADINFO lpgui);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //public static extern int GetForegroundWindow();

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(int h, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder bld, int size);
        [DllImport("User32.Dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);

        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(ref Point point);
        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();
        #endregion Get Functions

        #region SendMessage
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, StringBuilder lString);

        #endregion SendMessage

        #region Set Functions

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32")]
        public extern static int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.Dll")]
        public static extern int SetForegroundWindow(int hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, ref WindowsApiStructs.WINDOWPLACEMENT wp);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(long hWnd,
                            long hWndInsertAfter,
                            int X,
                            int Y,
                            int cx,
                            int cy,
                            uint uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);


        [DllImport("user32.dll")]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);
        #endregion Set Functions

        #region GetWindowPos
        public static void GetWindowPos(int hwnd,
                                        ref int ptrPhwnd,
                                        ref int ptrNhwnd,
                                        ref Point ptPoint,
                                        Size szSize,
                                        WindowsApiEnums.WINDOWINFOCMD intShowCmd)
        {
            WindowsApiStructs.WINDOWPLACEMENT wp = new WindowsApiStructs.WINDOWPLACEMENT();
            wp.length = (uint)System.Runtime.InteropServices.Marshal.SizeOf(wp);
            GetWindowPlacement((IntPtr)hwnd, ref wp);
            szSize = new Size((int)(wp.rcNormalPosition.right - wp.rcNormalPosition.left),
                              (int)(wp.rcNormalPosition.bottom - wp.rcNormalPosition.top));
            ptPoint = new Point((int)Math.Abs(wp.rcNormalPosition.left), (int)Math.Abs(wp.rcNormalPosition.top));
            ptrPhwnd = (int)GetNextWindow((long)hwnd, WindowsApiEnums.WINDOWINFOCMD.GW_HWNDPREV);
            ptrNhwnd = (int)GetNextWindow((long)hwnd, WindowsApiEnums.WINDOWINFOCMD.GW_HWNDNEXT);
            intShowCmd = wp.showCmd;
        }
        #endregion

        #region Find Window
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("User32.dll")]
        //public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(int ZeroOnly, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);
        #endregion Find Window

        #region Enum Windows
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        public delegate bool WindowEnumDelegate(IntPtr hwnd, int lParam);
        // declare the API function to enumerate child windows
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern int EnumChildWindows(IntPtr hwnd, WindowEnumDelegate del, int lParam);
        [DllImport("user32")]
        public extern static int EnumWindows(WindowEnumDelegate lpEnumFunc, int lParam);
        #endregion Enum Windows

        [DllImport("user32.dll")]
        public static extern IntPtr GetMenu(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetSubMenu(IntPtr hMenu, int nPos);
        [DllImport("user32.dll")]
        public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);
        [DllImport("user32.dll")]
        public static extern int GetMenuString(IntPtr hMenu, uint uIDItem, [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder lpString, int nMaxCount, uint uFlag);
        [DllImport("User32.dll")]
        public static extern int GetMenuItemCount(int hMenu); 

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
        [DllImport("User32.dll")]
        public static extern Boolean MessageBeep(UInt32 beepType);

        [DllImport("User32.dll")]
        public static extern Int32 WindowFromPoint(UInt32 x, UInt32 y);
        [DllImport("user32.dll")]
        public static extern int WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32")]
        public extern static int BringWindowToTop(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendInput(uint nInputs, IntPtr pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, ref WindowsApiStructs.INPUT pInputs, int cbSize);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint
        wParam, uint lParam);
        [DllImport("user32.dll")]
        public static extern int PostMessage(int hWnd, int msg, int wParam, int lParam);

        #region Hooking Function

        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(WindowsApiEnums.HookType hookType, HookProc lpfn,
        IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam,
           IntPtr lParam);

        // overload for use with LowLevelKeyboardProc
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, WindowsApiEnums.WM wParam, [In]WindowsApiStructs.KBDLLHOOKSTRUCT lParam);

        // overload for use with LowLevelMouseProc
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, WindowsApiEnums.WM wParam, [In]WindowsApiStructs.MSLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref WindowsApis.Data.WindowsApiStructs.MENUITEMINFO lpmii);
        #endregion Hooking Function

    }
}
