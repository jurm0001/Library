using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

/*
INPUT structInput;
structInput = new INPUT();
structInput.type = Win32Consts.INPUT_KEYBOARD;

// Key down shift, ctrl, and/or alt
structInput.ki.wScan = 0;
structInput.ki.time = 0;
structInput.ki.dwFlags = 0;
if (Shift)
{
structInput.ki.wVk = (ushort)VK.SHIFT;
intReturn = SendInput(1, ref structInput, Marshal.SizeOf(new INPUT()));
}  
*/


namespace WindowsApis.Data
{  
        #region Constants
        //public class Constants
        public class WindowsApiConstants
        {
            public const int INPUT_MOUSE = 0;
            public const int INPUT_KEYBOARD = 1;
            public const int INPUT_HARDWARE = 2;
            public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
            public const uint KEYEVENTF_KEYUP = 0x0002;
            public const uint KEYEVENTF_UNICODE = 0x0004;
            public const uint KEYEVENTF_SCANCODE = 0x0008;
            public const uint XBUTTON1 = 0x0001;
            public const uint XBUTTON2 = 0x0002;
            public const uint MOUSEEVENTF_MOVE = 0x0001;
            public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
            public const uint MOUSEEVENTF_LEFTUP = 0x0004;
            public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
            public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
            public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
            public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
            public const uint MOUSEEVENTF_XDOWN = 0x0080;
            public const uint MOUSEEVENTF_XUP = 0x0100;
            public const uint MOUSEEVENTF_WHEEL = 0x0800;
            public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
            public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

            const UInt32 MIM_MAXHEIGHT = 0x00000001;
            const UInt32 MIM_BACKGROUND = 0x00000002;
            const UInt32 MIM_HELPID = 0x00000004;
            const UInt32 MIM_MENUDATA = 0x00000008;
            const UInt32 MIM_STYLE = 0x00000010;
            const UInt32 MIM_APPLYTOSUBMENUS = 0x80000000;
        }        
        #endregion Constants

        #region enums

        public class WindowsApiEnums
        {
            #region SENDMESSAGE ENUMERATIONS
            public enum SENDMESSAGE_COMBOBOX : uint
            {
                CB_SHOWDROPDOWN = 0x14F,
                CB_ADDSTRING = 0x143,
                CB_RESETCONTENT = 0x14B,
                CB_SETCURSEL = 0x14E,
                CB_GETCURSEL = 0x147,
                // CB_GETTEXT,
                CB_FINDSTRING = 0x14C,
                CB_FINDSTRINGEXACT = 0x158,
                CB_DIR = 0x145,
                CB_INSERTSTRING = 0x14A,
                CB_GETITEMDATA = 0x150,
                CB_SETITEMDATA = 0x151,
                CB_SELECTSTRING = 0x14D
            };
            public enum SENDMESSAGE_LISTBOX : uint
            {
                LB_GETSELITEMS = 0x191,
                LB_GETCOUNT = 0x018B,
                LB_SETCOUNT = 0x01A7,
                LB_SETCURSEL = 0x186,
                LB_GETSEL = 0x187,
                LB_GETCURSEL = 0x188,
                LB_GETTEXT = 0x189,
                LB_FINDSTRING = 0x18F,
                LB_FINDSTRINGEXACT = 0x1A2,
                LB_ADDSTRING = 0x180,

                LB_GETTEXTLEN = 0x18A
            };
            public enum SENDMESSAGE_WINDOW : uint
            {
                WM_NCLBUTTONDOWN = 0xA1,
                WM_NCLBUTTONUP = 0xA2,
                WM_NCLBUTTONDBLCLK = 0xA3,
                WM_NCRBUTTONDOWN = 0xA4,
                WM_NCRBUTTONUP = 0xA5,
                WM_COMMAND = 0x111,
                WM_DESTROY = 0x2,
                WM_NCDESTROY = 0x82,
                WM_ENABLE = 0xA,
                WM_HSCROLL = 0x114,
                WM_LBUTTONDBLCLK = 0x203,
                WM_LBUTTONDOWN = 0x201,
                WM_LBUTTONUP = 0x202,
                WM_MBUTTONDBLCLK = 0x209,
                WM_MBUTTONDOWN = 0x207,
                WM_MBUTTONUP = 0x208,
                WM_PASTE = 0x302,
                WM_QUIT = 0x12,
                WM_RBUTTONDBLCLK = 0x206,
                WM_RBUTTONDOWN = 0x204,
                WM_RBUTTONUP = 0x205,
                WM_SETFOCUS = 0x7,
                WM_VSCROLL = 0x115,
                WM_CLOSE = 0x10,
                WM_COPY = 0x301,
                WM_GETTEXT = 0xD,
                WM_GETTEXTLENGTH = 0xE,
                WM_SETTEXT = 0xC,
                WM_CLEAR = 0x303,
                WM_CUT = 0x300,
                WM_FONTCHANGE = 0x1D,
                WM_GETFONT = 0x31,
                WM_GETMINMAXINFO = 0x24,
                WM_KEYDOWN = 0x100,
                WM_KEYUP = 0x101,
                WM_SETFONT = 0x30,
                WM_UNDO = 0x304,

            };
            public enum SENDMESSAGE_DIRECTORY : uint
            {
                DDL_ARCHIVE = 0x20, //Includes archived files. 
                DDL_DIRECTORY = 0x10,//  Includes subdirectories. Subdirectory names are enclosed in square brackets ([ ]).      
                DDL_DRIVES = 0x4000,//  Includes drives. Drives are listed in the form [-x-], where x is the drive letter. 
                DDL_EXCLUSIVE = 0x800, //  Includes only files with the specified attributes. By default, read-write files are listed even if DDL_READWRITE is not specified. 
                DDL_HIDDEN = 0x2,//  Includes hidden files. 
                DDL_READONLY = 0x1,//  Includes read-only files. 
                DDL_READWRITE = 0x0,//  Includes read-write files with no additional attributes. 
                DDL_SYSTEM = 0x4,//  Includes system files.   
            };
            public enum SENDMESSAGE_BUTTON : uint
            {
                BM_GETCHECK = 0xF0,
                BM_SETCHECKED = 0xF1,
                BM_GETSTATE = 0xF2,
                BM_CLICK = 0xF5
            }
            public enum SENDMESSSAGE_TABPAGE : uint
            {
                TCM_FIRST = 0x1300,
                TCM_GETIMAGELIST = TCM_FIRST + 2,
                TCM_SETIMAGELIST = TCM_FIRST + 3,
                TCM_GETITEMCOUNT = TCM_FIRST + 4,
                TCM_GETITEM = TCM_FIRST + 5,
                TCM_SETITEM = TCM_FIRST + 6,
                TCM_INSERTITEM = TCM_FIRST + 7,
                TCM_DELETEITEM = TCM_FIRST + 8,
                TCM_DELETEALLITEMS = TCM_FIRST + 9,
                TCM_GETITEMRECT = TCM_FIRST + 10,
                TCM_GETCURSEL = TCM_FIRST + 11,
                TCM_SETCURSEL = TCM_FIRST + 12,
                TCM_HITTEST = TCM_FIRST + 13,
                TCM_SETITEMEXTRA = TCM_FIRST + 14,
                TCM_ADJUSTRECT = TCM_FIRST + 40,
                TCM_SETITEMSIZE = TCM_FIRST + 41,
                TCM_REMOVEIMAGE = TCM_FIRST + 42,
                TCM_SETPADDING = TCM_FIRST + 43,
                TCM_GETROWCOUNT = TCM_FIRST + 44,
                TCM_GETTOOLTIPS = TCM_FIRST + 45,
                TCM_SETTOOLTIPS = TCM_FIRST + 46,
                TCM_GETCURFOCUS = TCM_FIRST + 47,
                TCM_SETCURFOCUS = TCM_FIRST + 48,
                TCM_SETMINTABWIDTH = TCM_FIRST + 49,
                TCM_DESELECTALL = TCM_FIRST + 50,
                TCM_HIGHLIGHTITEM = TCM_FIRST + 51,
                TCM_SETEXTENDEDSTYLE = TCM_FIRST + 52,
                TCM_GETEXTENDEDSTYLE = TCM_FIRST + 53,
                TCM_SETUNICODEFORMAT = 0x2005,
                TCM_GETUNICODEFORMAT = 0x2006

            }
            public enum WM : uint
            {
                /// <summary>
                /// The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
                /// </summary>
                NULL = 0x0000,
                /// <summary>
                /// The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
                /// </summary>
                CREATE = 0x0001,
                /// <summary>
                /// The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen. 
                /// This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
                /// /// </summary>
                DESTROY = 0x0002,
                /// <summary>
                /// The WM_MOVE message is sent after a window has been moved. 
                /// </summary>
                MOVE = 0x0003,
                /// <summary>
                /// The WM_SIZE message is sent to a window after its size has changed.
                /// </summary>
                SIZE = 0x0005,
                /// <summary>
                /// The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated. If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately. 
                /// </summary>
                ACTIVATE = 0x0006,
                /// <summary>
                /// The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus. 
                /// </summary>
                SETFOCUS = 0x0007,
                /// <summary>
                /// The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus. 
                /// </summary>
                KILLFOCUS = 0x0008,
                /// <summary>
                /// The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed. 
                /// </summary>
                ENABLE = 0x000A,
                /// <summary>
                /// An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn. 
                /// </summary>
                SETREDRAW = 0x000B,
                /// <summary>
                /// An application sends a WM_SETTEXT message to set the text of a window. 
                /// </summary>
                SETTEXT = 0x000C,
                /// <summary>
                /// An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller. 
                /// </summary>
                GETTEXT = 0x000D,
                /// <summary>
                /// An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window. 
                /// </summary>
                GETTEXTLENGTH = 0x000E,
                /// <summary>
                /// The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function. 
                /// </summary>
                PAINT = 0x000F,
                /// <summary>
                /// The WM_CLOSE message is sent as a signal that a window or an application should terminate.
                /// </summary>
                CLOSE = 0x0010,
                /// <summary>
                /// The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
                /// After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
                /// </summary>
                QUERYENDSESSION = 0x0011,
                /// <summary>
                /// The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
                /// </summary>
                QUERYOPEN = 0x0013,
                /// <summary>
                /// The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
                /// </summary>
                ENDSESSION = 0x0016,
                /// <summary>
                /// The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.
                /// </summary>
                QUIT = 0x0012,
                /// <summary>
                /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting. 
                /// </summary>
                ERASEBKGND = 0x0014,
                /// <summary>
                /// This message is sent to all top-level windows when a change is made to a system color setting. 
                /// </summary>
                SYSCOLORCHANGE = 0x0015,
                /// <summary>
                /// The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
                /// </summary>
                SHOWWINDOW = 0x0018,
                /// <summary>
                /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
                /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
                /// </summary>
                WININICHANGE = 0x001A,
                /// <summary>
                /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
                /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
                /// </summary>
                SETTINGCHANGE = WM.WININICHANGE,
                /// <summary>
                /// The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings. 
                /// </summary>
                DEVMODECHANGE = 0x001B,
                /// <summary>
                /// The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
                /// </summary>
                ACTIVATEAPP = 0x001C,
                /// <summary>
                /// An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources. 
                /// </summary>
                FONTCHANGE = 0x001D,
                /// <summary>
                /// A message that is sent whenever there is a change in the system time.
                /// </summary>
                TIMECHANGE = 0x001E,
                /// <summary>
                /// The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
                /// </summary>
                CANCELMODE = 0x001F,
                /// <summary>
                /// The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured. 
                /// </summary>
                SETCURSOR = 0x0020,
                /// <summary>
                /// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
                /// </summary>
                MOUSEACTIVATE = 0x0021,
                /// <summary>
                /// The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
                /// </summary>
                CHILDACTIVATE = 0x0022,
                /// <summary>
                /// The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure. 
                /// </summary>
                QUEUESYNC = 0x0023,
                /// <summary>
                /// The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size. 
                /// </summary>
                GETMINMAXINFO = 0x0024,
                /// <summary>
                /// Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.
                /// </summary>
                PAINTICON = 0x0026,
                /// <summary>
                /// Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
                /// </summary>
                ICONERASEBKGND = 0x0027,
                /// <summary>
                /// The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box. 
                /// </summary>
                NEXTDLGCTL = 0x0028,
                /// <summary>
                /// The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue. 
                /// </summary>
                SPOOLERSTATUS = 0x002A,
                /// <summary>
                /// The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
                /// </summary>
                DRAWITEM = 0x002B,
                /// <summary>
                /// The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
                /// </summary>
                MEASUREITEM = 0x002C,
                /// <summary>
                /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
                /// </summary>
                DELETEITEM = 0x002D,
                /// <summary>
                /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message. 
                /// </summary>
                VKEYTOITEM = 0x002E,
                /// <summary>
                /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message. 
                /// </summary>
                CHARTOITEM = 0x002F,
                /// <summary>
                /// An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text. 
                /// </summary>
                SETFONT = 0x0030,
                /// <summary>
                /// An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text. 
                /// </summary>
                GETFONT = 0x0031,
                /// <summary>
                /// An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window. 
                /// </summary>
                SETHOTKEY = 0x0032,
                /// <summary>
                /// An application sends a WM_GETHOTKEY message to determine the hot key associated with a window. 
                /// </summary>
                GETHOTKEY = 0x0033,
                /// <summary>
                /// The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
                /// </summary>
                QUERYDRAGICON = 0x0037,
                /// <summary>
                /// The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style. 
                /// </summary>
                COMPAREITEM = 0x0039,
                /// <summary>
                /// Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application. 
                /// Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message. 
                /// </summary>
                GETOBJECT = 0x003D,
                /// <summary>
                /// The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
                /// </summary>
                COMPACTING = 0x0041,
                /// <summary>
                /// WM_COMMNOTIFY is Obsolete for Win32-Based Applications
                /// </summary>
                [Obsolete]
                COMMNOTIFY = 0x0044,
                /// <summary>
                /// The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
                /// </summary>
                WINDOWPOSCHANGING = 0x0046,
                /// <summary>
                /// The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
                /// </summary>
                WINDOWPOSCHANGED = 0x0047,
                /// <summary>
                /// Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
                /// Use: POWERBROADCAST
                /// </summary>
                [Obsolete]
                POWER = 0x0048,
                /// <summary>
                /// An application sends the WM_COPYDATA message to pass data to another application. 
                /// </summary>
                COPYDATA = 0x004A,
                /// <summary>
                /// The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle. 
                /// </summary>
                CANCELJOURNAL = 0x004B,
                /// <summary>
                /// Sent by a common control to its parent window when an event has occurred or the control requires some information. 
                /// </summary>
                NOTIFY = 0x004E,
                /// <summary>
                /// The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately. 
                /// </summary>
                INPUTLANGCHANGEREQUEST = 0x0050,
                /// <summary>
                /// The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on. 
                /// </summary>
                INPUTLANGCHANGE = 0x0051,
                /// <summary>
                /// Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
                /// </summary>
                TCARD = 0x0052,
                /// <summary>
                /// Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window. 
                /// </summary>
                HELP = 0x0053,
                /// <summary>
                /// The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
                /// </summary>
                USERCHANGED = 0x0054,
                /// <summary>
                /// Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
                /// </summary>
                NOTIFYFORMAT = 0x0055,
                /// <summary>
                /// The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
                /// </summary>
                CONTEXTMENU = 0x007B,
                /// <summary>
                /// The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
                /// </summary>
                STYLECHANGING = 0x007C,
                /// <summary>
                /// The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles
                /// </summary>
                STYLECHANGED = 0x007D,
                /// <summary>
                /// The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
                /// </summary>
                DISPLAYCHANGE = 0x007E,
                /// <summary>
                /// The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption. 
                /// </summary>
                GETICON = 0x007F,
                /// <summary>
                /// An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption. 
                /// </summary>
                SETICON = 0x0080,
                /// <summary>
                /// The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
                /// </summary>
                NCCREATE = 0x0081,
                /// <summary>
                /// The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window. 
                /// The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
                /// </summary>
                NCDESTROY = 0x0082,
                /// <summary>
                /// The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
                /// </summary>
                NCCALCSIZE = 0x0083,
                /// <summary>
                /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
                /// </summary>
                NCHITTEST = 0x0084,
                /// <summary>
                /// The WM_NCPAINT message is sent to a window when its frame must be painted. 
                /// </summary>
                NCPAINT = 0x0085,
                /// <summary>
                /// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
                /// </summary>
                NCACTIVATE = 0x0086,
                /// <summary>
                /// The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
                /// </summary>
                GETDLGCODE = 0x0087,
                /// <summary>
                /// The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
                /// </summary>
                SYNCPAINT = 0x0088,
                /// <summary>
                /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCMOUSEMOVE = 0x00A0,
                /// <summary>
                /// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCLBUTTONDOWN = 0x00A1,
                /// <summary>
                /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCLBUTTONUP = 0x00A2,
                /// <summary>
                /// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCLBUTTONDBLCLK = 0x00A3,
                /// <summary>
                /// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCRBUTTONDOWN = 0x00A4,
                /// <summary>
                /// The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCRBUTTONUP = 0x00A5,
                /// <summary>
                /// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCRBUTTONDBLCLK = 0x00A6,
                /// <summary>
                /// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCMBUTTONDOWN = 0x00A7,
                /// <summary>
                /// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCMBUTTONUP = 0x00A8,
                /// <summary>
                /// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCMBUTTONDBLCLK = 0x00A9,
                /// <summary>
                /// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCXBUTTONDOWN = 0x00AB,
                /// <summary>
                /// The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCXBUTTONUP = 0x00AC,
                /// <summary>
                /// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
                /// </summary>
                NCXBUTTONDBLCLK = 0x00AD,
                /// <summary>
                /// The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.
                /// </summary>
                INPUT_DEVICE_CHANGE = 0x00FE,
                /// <summary>
                /// The WM_INPUT message is sent to the window that is getting raw input. 
                /// </summary>
                INPUT = 0x00FF,
                /// <summary>
                /// This message filters for keyboard messages.
                /// </summary>
                KEYFIRST = 0x0100,
                /// <summary>
                /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed. 
                /// </summary>
                KEYDOWN = 0x0100,
                /// <summary>
                /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus. 
                /// </summary>
                KEYUP = 0x0101,
                /// <summary>
                /// The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed. 
                /// </summary>
                CHAR = 0x0102,
                /// <summary>
                /// The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key. 
                /// </summary>
                DEADCHAR = 0x0103,
                /// <summary>
                /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter. 
                /// </summary>
                SYSKEYDOWN = 0x0104,
                /// <summary>
                /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter. 
                /// </summary>
                SYSKEYUP = 0x0105,
                /// <summary>
                /// The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down. 
                /// </summary>
                SYSCHAR = 0x0106,
                /// <summary>
                /// The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key. 
                /// </summary>
                SYSDEADCHAR = 0x0107,
                /// <summary>
                /// The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed. 
                /// The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.
                /// </summary>
                UNICHAR = 0x0109,
                /// <summary>
                /// This message filters for keyboard messages.
                /// </summary>
                KEYLAST = 0x0109,
                /// <summary>
                /// Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_STARTCOMPOSITION = 0x010D,
                /// <summary>
                /// Sent to an application when the IME ends composition. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_ENDCOMPOSITION = 0x010E,
                /// <summary>
                /// Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_COMPOSITION = 0x010F,
                IME_KEYLAST = 0x010F,
                /// <summary>
                /// The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box. 
                /// </summary>
                INITDIALOG = 0x0110,
                /// <summary>
                /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated. 
                /// </summary>
                COMMAND = 0x0111,
                /// <summary>
                /// A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button, close button, or moves the form. You can stop the form from moving by filtering this out.
                /// </summary>
                SYSCOMMAND = 0x0112,
                /// <summary>
                /// The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function. 
                /// </summary>
                TIMER = 0x0113,
                /// <summary>
                /// The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control. 
                /// </summary>
                HSCROLL = 0x0114,
                /// <summary>
                /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control. 
                /// </summary>
                VSCROLL = 0x0115,
                /// <summary>
                /// The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed. 
                /// </summary>
                INITMENU = 0x0116,
                /// <summary>
                /// The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu. 
                /// </summary>
                INITMENUPOPUP = 0x0117,
                /// <summary>
                /// The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item. 
                /// </summary>
                MENUSELECT = 0x011F,
                /// <summary>
                /// The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu. 
                /// </summary>
                MENUCHAR = 0x0120,
                /// <summary>
                /// The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages. 
                /// </summary>
                ENTERIDLE = 0x0121,
                /// <summary>
                /// The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item. 
                /// </summary>
                MENURBUTTONUP = 0x0122,
                /// <summary>
                /// The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item. 
                /// </summary>
                MENUDRAG = 0x0123,
                /// <summary>
                /// The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item. 
                /// </summary>
                MENUGETOBJECT = 0x0124,
                /// <summary>
                /// The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed. 
                /// </summary>
                UNINITMENUPOPUP = 0x0125,
                /// <summary>
                /// The WM_MENUCOMMAND message is sent when the user makes a selection from a menu. 
                /// </summary>
                MENUCOMMAND = 0x0126,
                /// <summary>
                /// An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
                /// </summary>
                CHANGEUISTATE = 0x0127,
                /// <summary>
                /// An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.
                /// </summary>
                UPDATEUISTATE = 0x0128,
                /// <summary>
                /// An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.
                /// </summary>
                QUERYUISTATE = 0x0129,
                /// <summary>
                /// The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle. 
                /// </summary>
                CTLCOLORMSGBOX = 0x0132,
                /// <summary>
                /// An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control. 
                /// </summary>
                CTLCOLOREDIT = 0x0133,
                /// <summary>
                /// Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle. 
                /// </summary>
                CTLCOLORLISTBOX = 0x0134,
                /// <summary>
                /// The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message. 
                /// </summary>
                CTLCOLORBTN = 0x0135,
                /// <summary>
                /// The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle. 
                /// </summary>
                CTLCOLORDLG = 0x0136,
                /// <summary>
                /// The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control. 
                /// </summary>
                CTLCOLORSCROLLBAR = 0x0137,
                /// <summary>
                /// A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control. 
                /// </summary>
                CTLCOLORSTATIC = 0x0138,
                /// <summary>
                /// Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
                /// </summary>
                MOUSEFIRST = 0x0200,
                /// <summary>
                /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                MOUSEMOVE = 0x0200,
                /// <summary>
                /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                LBUTTONDOWN = 0x0201,
                /// <summary>
                /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                LBUTTONUP = 0x0202,
                /// <summary>
                /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                LBUTTONDBLCLK = 0x0203,
                /// <summary>
                /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                RBUTTONDOWN = 0x0204,
                /// <summary>
                /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                RBUTTONUP = 0x0205,
                /// <summary>
                /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                RBUTTONDBLCLK = 0x0206,
                /// <summary>
                /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                MBUTTONDOWN = 0x0207,
                /// <summary>
                /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                MBUTTONUP = 0x0208,
                /// <summary>
                /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                MBUTTONDBLCLK = 0x0209,
                /// <summary>
                /// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
                /// </summary>
                MOUSEWHEEL = 0x020A,
                /// <summary>
                /// The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse. 
                /// </summary>
                XBUTTONDOWN = 0x020B,
                /// <summary>
                /// The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                XBUTTONUP = 0x020C,
                /// <summary>
                /// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
                /// </summary>
                XBUTTONDBLCLK = 0x020D,
                /// <summary>
                /// The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
                /// </summary>
                MOUSEHWHEEL = 0x020E,
                /// <summary>
                /// Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.
                /// </summary>
                MOUSELAST = 0x020E,
                /// <summary>
                /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
                /// </summary>
                PARENTNOTIFY = 0x0210,
                /// <summary>
                /// The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered. 
                /// </summary>
                ENTERMENULOOP = 0x0211,
                /// <summary>
                /// The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited. 
                /// </summary>
                EXITMENULOOP = 0x0212,
                /// <summary>
                /// The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu. 
                /// </summary>
                NEXTMENU = 0x0213,
                /// <summary>
                /// The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position. 
                /// </summary>
                SIZING = 0x0214,
                /// <summary>
                /// The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
                /// </summary>
                CAPTURECHANGED = 0x0215,
                /// <summary>
                /// The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
                /// </summary>
                MOVING = 0x0216,
                /// <summary>
                /// Notifies applications that a power-management event has occurred.
                /// </summary>
                POWERBROADCAST = 0x0218,
                /// <summary>
                /// Notifies an application of a change to the hardware configuration of a device or the computer.
                /// </summary>
                DEVICECHANGE = 0x0219,
                /// <summary>
                /// An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window. 
                /// </summary>
                MDICREATE = 0x0220,
                /// <summary>
                /// An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window. 
                /// </summary>
                MDIDESTROY = 0x0221,
                /// <summary>
                /// An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window. 
                /// </summary>
                MDIACTIVATE = 0x0222,
                /// <summary>
                /// An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size. 
                /// </summary>
                MDIRESTORE = 0x0223,
                /// <summary>
                /// An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window. 
                /// </summary>
                MDINEXT = 0x0224,
                /// <summary>
                /// An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window. 
                /// </summary>
                MDIMAXIMIZE = 0x0225,
                /// <summary>
                /// An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format. 
                /// </summary>
                MDITILE = 0x0226,
                /// <summary>
                /// An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format. 
                /// </summary>
                MDICASCADE = 0x0227,
                /// <summary>
                /// An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized. 
                /// </summary>
                MDIICONARRANGE = 0x0228,
                /// <summary>
                /// An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window. 
                /// </summary>
                MDIGETACTIVE = 0x0229,
                /// <summary>
                /// An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both. 
                /// </summary>
                MDISETMENU = 0x0230,
                /// <summary>
                /// The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns. 
                /// The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
                /// </summary>
                ENTERSIZEMOVE = 0x0231,
                /// <summary>
                /// The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns. 
                /// </summary>
                EXITSIZEMOVE = 0x0232,
                /// <summary>
                /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
                /// </summary>
                DROPFILES = 0x0233,
                /// <summary>
                /// An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window. 
                /// </summary>
                MDIREFRESHMENU = 0x0234,
                /// <summary>
                /// Sent to an application when a window is activated. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_SETCONTEXT = 0x0281,
                /// <summary>
                /// Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_NOTIFY = 0x0282,
                /// <summary>
                /// Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.
                /// </summary>
                IME_CONTROL = 0x0283,
                /// <summary>
                /// Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_COMPOSITIONFULL = 0x0284,
                /// <summary>
                /// Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_SELECT = 0x0285,
                /// <summary>
                /// Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_CHAR = 0x0286,
                /// <summary>
                /// Sent to an application to provide commands and request information. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_REQUEST = 0x0288,
                /// <summary>
                /// Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_KEYDOWN = 0x0290,
                /// <summary>
                /// Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function. 
                /// </summary>
                IME_KEYUP = 0x0291,
                /// <summary>
                /// The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
                /// </summary>
                MOUSEHOVER = 0x02A1,
                /// <summary>
                /// The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
                /// </summary>
                MOUSELEAVE = 0x02A3,
                /// <summary>
                /// The WM_NCMOUSEHOVER message is posted to a window when the cursor hovers over the nonclient area of the window for the period of time specified in a prior call to TrackMouseEvent.
                /// </summary>
                NCMOUSEHOVER = 0x02A0,
                /// <summary>
                /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
                /// </summary>
                NCMOUSELEAVE = 0x02A2,
                /// <summary>
                /// The WM_WTSSESSION_CHANGE message notifies applications of changes in session state.
                /// </summary>
                WTSSESSION_CHANGE = 0x02B1,
                TABLET_FIRST = 0x02c0,
                TABLET_LAST = 0x02df,
                /// <summary>
                /// An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format. 
                /// </summary>
                CUT = 0x0300,
                /// <summary>
                /// An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format. 
                /// </summary>
                COPY = 0x0301,
                /// <summary>
                /// An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format. 
                /// </summary>
                PASTE = 0x0302,
                /// <summary>
                /// An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control. 
                /// </summary>
                CLEAR = 0x0303,
                /// <summary>
                /// An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.
                /// </summary>
                UNDO = 0x0304,
                /// <summary>
                /// The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function. 
                /// </summary>
                RENDERFORMAT = 0x0305,
                /// <summary>
                /// The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function. 
                /// </summary>
                RENDERALLFORMATS = 0x0306,
                /// <summary>
                /// The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard. 
                /// </summary>
                DESTROYCLIPBOARD = 0x0307,
                /// <summary>
                /// The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard. 
                /// </summary>
                DRAWCLIPBOARD = 0x0308,
                /// <summary>
                /// The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting. 
                /// </summary>
                PAINTCLIPBOARD = 0x0309,
                /// <summary>
                /// The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values. 
                /// </summary>
                VSCROLLCLIPBOARD = 0x030A,
                /// <summary>
                /// The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size. 
                /// </summary>
                SIZECLIPBOARD = 0x030B,
                /// <summary>
                /// The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
                /// </summary>
                ASKCBFORMATNAME = 0x030C,
                /// <summary>
                /// The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain. 
                /// </summary>
                CHANGECBCHAIN = 0x030D,
                /// <summary>
                /// The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values. 
                /// </summary>
                HSCROLLCLIPBOARD = 0x030E,
                /// <summary>
                /// This message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus. 
                /// </summary>
                QUERYNEWPALETTE = 0x030F,
                /// <summary>
                /// The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette. 
                /// </summary>
                PALETTEISCHANGING = 0x0310,
                /// <summary>
                /// This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette. 
                /// This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
                /// </summary>
                PALETTECHANGED = 0x0311,
                /// <summary>
                /// The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key. 
                /// </summary>
                HOTKEY = 0x0312,
                /// <summary>
                /// The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
                /// </summary>
                PRINT = 0x0317,
                /// <summary>
                /// The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
                /// </summary>
                PRINTCLIENT = 0x0318,
                /// <summary>
                /// The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.
                /// </summary>
                APPCOMMAND = 0x0319,
                /// <summary>
                /// The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.
                /// </summary>
                THEMECHANGED = 0x031A,
                /// <summary>
                /// Sent when the contents of the clipboard have changed.
                /// </summary>
                CLIPBOARDUPDATE = 0x031D,
                /// <summary>
                /// The system will send a window the WM_DWMCOMPOSITIONCHANGED message to indicate that the availability of desktop composition has changed.
                /// </summary>
                DWMCOMPOSITIONCHANGED = 0x031E,
                /// <summary>
                /// WM_DWMNCRENDERINGCHANGED is called when the non-client area rendering status of a window has changed. Only windows that have set the flag DWM_BLURBEHIND.fTransitionOnMaximized to true will get this message. 
                /// </summary>
                DWMNCRENDERINGCHANGED = 0x031F,
                /// <summary>
                /// Sent to all top-level windows when the colorization color has changed. 
                /// </summary>
                DWMCOLORIZATIONCOLORCHANGED = 0x0320,
                /// <summary>
                /// WM_DWMWINDOWMAXIMIZEDCHANGE will let you know when a DWM composed window is maximized. You also have to register for this message as well. You'd have other windowd go opaque when this message is sent.
                /// </summary>
                DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
                /// <summary>
                /// Sent to request extended title bar information. A window receives this message through its WindowProc function.
                /// </summary>
                GETTITLEBARINFOEX = 0x033F,
                HANDHELDFIRST = 0x0358,
                HANDHELDLAST = 0x035F,
                AFXFIRST = 0x0360,
                AFXLAST = 0x037F,
                PENWINFIRST = 0x0380,
                PENWINLAST = 0x038F,
                /// <summary>
                /// The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value. 
                /// </summary>
                APP = 0x8000,
                /// <summary>
                /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value. 
                /// </summary>
                USER = 0x0400,

                /// <summary>
                /// An application sends the WM_CPL_LAUNCH message to Windows Control Panel to request that a Control Panel application be started. 
                /// </summary>
                CPL_LAUNCH = USER + 0x1000,
                /// <summary>
                /// The WM_CPL_LAUNCHED message is sent when a Control Panel application, started by the WM_CPL_LAUNCH message, has closed. The WM_CPL_LAUNCHED message is sent to the window identified by the wParam parameter of the WM_CPL_LAUNCH message that started the application. 
                /// </summary>
                CPL_LAUNCHED = USER + 0x1001,
                /// <summary>
                /// WM_SYSTIMER is a well-known yet still undocumented message. Windows uses WM_SYSTIMER for internal actions like scrolling.
                /// </summary>
                SYSTIMER = 0x118


            }
            #endregion
            #region SetHook
            public enum HookType : int
            {
                WH_JOURNALRECORD = 0,
                WH_JOURNALPLAYBACK = 1,
                WH_KEYBOARD = 2,
                WH_GETMESSAGE = 3,
                WH_CALLWNDPROC = 4,
                WH_CBT = 5,
                WH_SYSMSGFILTER = 6,
                WH_MOUSE = 7,
                WH_HARDWARE = 8,
                WH_DEBUG = 9,
                WH_SHELL = 10,
                WH_FOREGROUNDIDLE = 11,
                WH_CALLWNDPROCRET = 12,
                WH_KEYBOARD_LL = 13,
                WH_MOUSE_LL = 14
            }
            #endregion SetHook

            public enum WaitForSingleObjectReturnCode : int
            {
                WAIT_ABANDONED = 128,
                WAIT_OBJECT_0 = 0,
                WAIT_TIMEOUT = 258,
                WAIT_FAILED = -1
            }            

            public enum ACLineStatus : byte
            {
                Offline = 0, Online = 1, Unknown = 255
            }
            public enum BatteryFlag : byte
            {
                High = 1,
                Low = 2,
                Critical = 4,
                Charging = 8,
                NoSystemBattery = 128,
                Unknown = 255
            }
            #region KeyBoard
            public enum KEYBOARDEVENTCONSTANTS : uint
            {
                // For use with the INPUT struct, see SendInput for an example
                KEYEVENTF_KEYDOWN = 0x0,
                KEYEVENTF_EXTENDEDKEY = 0x1,
                KEYEVENTF_KEYUP = 0x2
            };
            public enum VK_KEYS : uint
            {
                /// <summary></summary>
                LeftButton = 0x01,
                /// <summary></summary>
                RightButton = 0x02,
                /// <summary></summary>
                Cancel = 0x03,
                /// <summary></summary>
                MiddleButton = 0x04,
                /// <summary></summary>
                ExtraButton1 = 0x05,
                /// <summary></summary>
                ExtraButton2 = 0x06,
                /// <summary></summary>
                Back = 0x08,
                /// <summary></summary>
                Tab = 0x09,
                /// <summary></summary>
                Clear = 0x0C,
                /// <summary></summary>
                Return = 0x0D,
                /// <summary></summary>
                Shift = 0x10,
                /// <summary></summary>
                Control = 0x11,
                /// <summary></summary>
                Menu = 0x12,
                /// <summary></summary>
                Pause = 0x13,
                /// <summary></summary>
                Kana = 0x15,
                /// <summary></summary>
                Hangeul = 0x15,
                /// <summary></summary>
                Hangul = 0x15,
                /// <summary></summary>
                Junja = 0x17,
                /// <summary></summary>
                Final = 0x18,
                /// <summary></summary>
                Hanja = 0x19,
                /// <summary></summary>
                Kanji = 0x19,
                /// <summary></summary>
                Escape = 0x1B,
                /// <summary></summary>
                Convert = 0x1C,
                /// <summary></summary>
                NonConvert = 0x1D,
                /// <summary></summary>
                Accept = 0x1E,
                /// <summary></summary>
                ModeChange = 0x1F,
                /// <summary></summary>
                Space = 0x20,
                /// <summary></summary>
                Prior = 0x21,
                /// <summary></summary>
                Next = 0x22,
                /// <summary></summary>
                End = 0x23,
                /// <summary></summary>
                Home = 0x24,
                /// <summary></summary>
                Left = 0x25,
                /// <summary></summary>
                Up = 0x26,
                /// <summary></summary>
                Right = 0x27,
                /// <summary></summary>
                Down = 0x28,
                /// <summary></summary>
                Select = 0x29,
                /// <summary></summary>
                Print = 0x2A,
                /// <summary></summary>
                Execute = 0x2B,
                /// <summary></summary>
                Snapshot = 0x2C,
                /// <summary></summary>
                Insert = 0x2D,
                /// <summary></summary>
                Delete = 0x2E,
                /// <summary></summary>
                Help = 0x2F,
                /// <summary></summary>
                N0 = 0x30,
                /// <summary></summary>
                N1 = 0x31,
                /// <summary></summary>
                N2 = 0x32,
                /// <summary></summary>
                N3 = 0x33,
                /// <summary></summary>
                N4 = 0x34,
                /// <summary></summary>
                N5 = 0x35,
                /// <summary></summary>
                N6 = 0x36,
                /// <summary></summary>
                N7 = 0x37,
                /// <summary></summary>
                N8 = 0x38,
                /// <summary></summary>
                N9 = 0x39,
                /// <summary></summary>
                A = 0x41,
                /// <summary></summary>
                B = 0x42,
                /// <summary></summary>
                C = 0x43,
                /// <summary></summary>
                D = 0x44,
                /// <summary></summary>
                E = 0x45,
                /// <summary></summary>
                F = 0x46,
                /// <summary></summary>
                G = 0x47,
                /// <summary></summary>
                H = 0x48,
                /// <summary></summary>
                I = 0x49,
                /// <summary></summary>
                J = 0x4A,
                /// <summary></summary>
                K = 0x4B,
                /// <summary></summary>
                L = 0x4C,
                /// <summary></summary>
                M = 0x4D,
                /// <summary></summary>
                N = 0x4E,
                /// <summary></summary>
                O = 0x4F,
                /// <summary></summary>
                P = 0x50,
                /// <summary></summary>
                Q = 0x51,
                /// <summary></summary>
                R = 0x52,
                /// <summary></summary>
                S = 0x53,
                /// <summary></summary>
                T = 0x54,
                /// <summary></summary>
                U = 0x55,
                /// <summary></summary>
                V = 0x56,
                /// <summary></summary>
                W = 0x57,
                /// <summary></summary>
                X = 0x58,
                /// <summary></summary>
                Y = 0x59,
                /// <summary></summary>
                Z = 0x5A,
                /// <summary></summary>
                LeftWindows = 0x5B,
                /// <summary></summary>
                RightWindows = 0x5C,
                /// <summary></summary>
                Application = 0x5D,
                /// <summary></summary>
                Sleep = 0x5F,
                /// <summary></summary>
                Numpad0 = 0x60,
                /// <summary></summary>
                Numpad1 = 0x61,
                /// <summary></summary>
                Numpad2 = 0x62,
                /// <summary></summary>
                Numpad3 = 0x63,
                /// <summary></summary>
                Numpad4 = 0x64,
                /// <summary></summary>
                Numpad5 = 0x65,
                /// <summary></summary>
                Numpad6 = 0x66,
                /// <summary></summary>
                Numpad7 = 0x67,
                /// <summary></summary>
                Numpad8 = 0x68,
                /// <summary></summary>
                Numpad9 = 0x69,
                /// <summary></summary>
                Multiply = 0x6A,
                /// <summary></summary>
                Add = 0x6B,
                /// <summary></summary>
                Separator = 0x6C,
                /// <summary></summary>
                Subtract = 0x6D,
                /// <summary></summary>
                Decimal = 0x6E,
                /// <summary></summary>
                Divide = 0x6F,
                /// <summary></summary>
                F1 = 0x70,
                /// <summary></summary>
                F2 = 0x71,
                /// <summary></summary>
                F3 = 0x72,
                /// <summary></summary>
                F4 = 0x73,
                /// <summary></summary>
                F5 = 0x74,
                /// <summary></summary>
                F6 = 0x75,
                /// <summary></summary>
                F7 = 0x76,
                /// <summary></summary>
                F8 = 0x77,
                /// <summary></summary>
                F9 = 0x78,
                /// <summary></summary>
                F10 = 0x79,
                /// <summary></summary>
                F11 = 0x7A,
                /// <summary></summary>
                F12 = 0x7B,
                /// <summary></summary>
                F13 = 0x7C,
                /// <summary></summary>
                F14 = 0x7D,
                /// <summary></summary>
                F15 = 0x7E,
                /// <summary></summary>
                F16 = 0x7F,
                /// <summary></summary>
                F17 = 0x80,
                /// <summary></summary>
                F18 = 0x81,
                /// <summary></summary>
                F19 = 0x82,
                /// <summary></summary>
                F20 = 0x83,
                /// <summary></summary>
                F21 = 0x84,
                /// <summary></summary>
                F22 = 0x85,
                /// <summary></summary>
                F23 = 0x86,
                /// <summary></summary>
                F24 = 0x87,
                /// <summary></summary>
                NumLock = 0x90,
                /// <summary></summary>
                ScrollLock = 0x91,
                /// <summary></summary>
                NEC_Equal = 0x92,
                /// <summary></summary>
                Fujitsu_Jisho = 0x92,
                /// <summary></summary>
                Fujitsu_Masshou = 0x93,
                /// <summary></summary>
                Fujitsu_Touroku = 0x94,
                /// <summary></summary>
                Fujitsu_Loya = 0x95,
                /// <summary></summary>
                Fujitsu_Roya = 0x96,
                /// <summary></summary>
                LeftShift = 0xA0,
                /// <summary></summary>
                RightShift = 0xA1,
                /// <summary></summary>
                LeftControl = 0xA2,
                /// <summary></summary>
                RightControl = 0xA3,
                /// <summary></summary>
                LeftMenu = 0xA4,
                /// <summary></summary>
                RightMenu = 0xA5,
                /// <summary></summary>
                BrowserBack = 0xA6,
                /// <summary></summary>
                BrowserForward = 0xA7,
                /// <summary></summary>
                BrowserRefresh = 0xA8,
                /// <summary></summary>
                BrowserStop = 0xA9,
                /// <summary></summary>
                BrowserSearch = 0xAA,
                /// <summary></summary>
                BrowserFavorites = 0xAB,
                /// <summary></summary>
                BrowserHome = 0xAC,
                /// <summary></summary>
                VolumeMute = 0xAD,
                /// <summary></summary>
                VolumeDown = 0xAE,
                /// <summary></summary>
                VolumeUp = 0xAF,
                /// <summary></summary>
                MediaNextTrack = 0xB0,
                /// <summary></summary>
                MediaPrevTrack = 0xB1,
                /// <summary></summary>
                MediaStop = 0xB2,
                /// <summary></summary>
                MediaPlayPause = 0xB3,
                /// <summary></summary>
                LaunchMail = 0xB4,
                /// <summary></summary>
                LaunchMediaSelect = 0xB5,
                /// <summary></summary>
                LaunchApplication1 = 0xB6,
                /// <summary></summary>
                LaunchApplication2 = 0xB7,
                /// <summary></summary>
                OEM1 = 0xBA,
                /// <summary></summary>
                OEMPlus = 0xBB,
                /// <summary></summary>
                OEMComma = 0xBC,
                /// <summary></summary>
                OEMMinus = 0xBD,
                /// <summary></summary>
                OEMPeriod = 0xBE,
                /// <summary></summary>
                OEM2 = 0xBF,
                /// <summary></summary>
                OEM3 = 0xC0,
                /// <summary></summary>
                OEM4 = 0xDB,
                /// <summary></summary>
                OEM5 = 0xDC,
                /// <summary></summary>
                OEM6 = 0xDD,
                /// <summary></summary>
                OEM7 = 0xDE,
                /// <summary></summary>
                OEM8 = 0xDF,
                /// <summary></summary>
                OEMAX = 0xE1,
                /// <summary></summary>
                OEM102 = 0xE2,
                /// <summary></summary>
                ICOHelp = 0xE3,
                /// <summary></summary>
                ICO00 = 0xE4,
                /// <summary></summary>
                ProcessKey = 0xE5,
                /// <summary></summary>
                ICOClear = 0xE6,
                /// <summary></summary>
                Packet = 0xE7,
                /// <summary></summary>
                OEMReset = 0xE9,
                /// <summary></summary>
                OEMJump = 0xEA,
                /// <summary></summary>
                OEMPA1 = 0xEB,
                /// <summary></summary>
                OEMPA2 = 0xEC,
                /// <summary></summary>
                OEMPA3 = 0xED,
                /// <summary></summary>
                OEMWSCtrl = 0xEE,
                /// <summary></summary>
                OEMCUSel = 0xEF,
                /// <summary></summary>
                OEMATTN = 0xF0,
                /// <summary></summary>
                OEMFinish = 0xF1,
                /// <summary></summary>
                OEMCopy = 0xF2,
                /// <summary></summary>
                OEMAuto = 0xF3,
                /// <summary></summary>
                OEMENLW = 0xF4,
                /// <summary></summary>
                OEMBackTab = 0xF5,
                /// <summary></summary>
                ATTN = 0xF6,
                /// <summary></summary>
                CRSel = 0xF7,
                /// <summary></summary>
                EXSel = 0xF8,
                /// <summary></summary>
                EREOF = 0xF9,
                /// <summary></summary>
                Play = 0xFA,
                /// <summary></summary>
                Zoom = 0xFB,
                /// <summary></summary>
                Noname = 0xFC,
                /// <summary></summary>
                PA1 = 0xFD,
                /// <summary></summary>
                OEMClear = 0xFE


            };
            #endregion KeyBoard

            #region windows
            public enum CREATEWINDOWFLAG : uint
            {
                CW_USEDEFAULT = 0x80000000,
                HWND_DESKTOP = 0,
                WS_VISIBLE = 0x10000000,
                WS_DISABLED = 0x08000000,
                WS_GROUP = 0x00020000,
                WS_TABSTOP = 0x00010000,
                WS_CLIPSIBLINGS = 0x04000000,
                WS_CLIPCHILDREN = 0x02000000,
                WS_BORDER = 0x00800000,
                WS_DLGFRAME = 0x00400000,
                WS_VSCROLL = 0x00200000,
                WS_HSCROLL = 0x00100000,
                WS_SYSMENU = 0x00080000,
                //WS_CAPTION = WS_BORDER WS_DLGFRAME))
                WS_POPUP = 0x80000000

            };
            public enum WINDOWS
            {
                WS_VISIBLE = 0x10000000,        // Window is not hidden
                WS_BORDER = 0x800000,          // Window has a border
                //Other bits that are normally set include:
                WS_CAPTION = 0xC00000,          // WS_BORDER Or WS_DLGFRAME
                WS_CHILD = 0x40000000,
                WS_CHILDWINDOW = (WS_CHILD),
                WS_CLIPSIBLINGS = 0x4000000,         // can clip windows
                WS_DLGFRAME = 0x400000,
                WS_GROUP = 0x20000,           // Window is top of group
                WS_SYSMENU = 0x80000,           // Window has system menu
                WS_THICKFRAME = 0x40000           // Window has thick border

            };
            public enum GETWINDOW
            {
                GW_HWNDFIRST = 0,
                GW_HWNDNEXT = 2,
                GW_HWNDPREV = 3,
                GW_OWNER = 4,
                GW_MAX = 5,
                GW_CHILD = 5,
                GWL_STYLE = (-16),

                WU_LOGPIXELSX = 88,
                WU_LOGPIXELSY = 90

            };
            public enum WINDOWINFOCMD
            {
                SW_HIDE = 0,
                SW_SHOWNORMAL = 1,
                SW_NORMAL = 1,
                SW_SHOWMINIMIZED = 2,
                GW_HWNDNEXT = 2,
                GW_HWNDPREV = 3,
                SW_MAXIMIZE = 3,
                SW_SHOWNOACTIVATE = 4,
                SW_SHOW = 5,
                SW_MINIMIZE = 6,
                SW_SHOWMINNOACTIVE = 7,
                SW_SHOWNA = 8,
                SW_RESTORE = 9,
                SW_SHOWDEFAULT = 10,
                SW_MAX = 10
            };
            public enum SETPOSFLAGS
            {
                SWP_NOMOVE = 0x2,
                SWP_NOSIZE = 0x1,
                SWP_NOZORDER = 0x4,
                SWP_NOREDRAW = 0x8,
                SWP_DRAWFRAME = 0x20,
                SWP_SHOWWINDOW = 0x40,
                SWP_HIDEWINDOW = 0x80
                //SWP_FLAGS = SWP_NOZORDER || SWP_NOSIZE || SWP_NOMOVE || SWP_DRAWFRAME

            };
            public enum INSERTAFTER
            {
                HWND_BOTTOM = 1, //Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
                HWND_NOTOPMOST = -1, // Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
                HWND_TOP, //Places the window at the top of the Z order.
                HWND_TOPMOST = -2 // Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated
            };
            public enum REDRAWFLAGS
            {
                RDW_INVALIDATE = 0x0001,
                RDW_INTERNALPAINT = 0x0002,
                RDW_ERASE = 0x0004,
                RDW_VALIDATE = 0x0008,
                RDW_NOINTERNALPAINT = 0x0010,
                RDW_NOERASE = 0x0020,
                RDW_NOCHILDREN = 0x0040,
                RDW_ALLCHILDREN = 0x0080,
                RDW_UPDATENOW = 0x0100,
                RDW_ERASENOW = 0x0200,
                RDW_FRAME = 0x0400,
                RDW_NOFRAME = 0x0800
            };
            public enum MENUITEM
            {
                MF_INSERT = 0x0,
                MF_CHANGE = 0x80,
                MF_APPEND = 0x100,
                MF_DELETE = 0x200,
                MF_REMOVE = 0x1000,

                MF_BYCOMMAND = 0,
                MF_BYPOSITION = 0x400,

                MF_ENABLED = 0x00,
                MF_STRING = 0x00,
                MF_UNCHECKED = 0x00,
                MF_UNHILITE = 0x00,

                MF_GRAYED = 0x01,
                MF_DISABLED = 0x02,
                MF_BITMAP = 0x04,
                MF_CHECKED = 0x08,
                MF_POPUP = 0x10,
                MF_MENUBARBREAK = 0x20,
                MF_MENUBREAK = 0x40,
                MF_HILITE = 0x80,

                MF_OWNERDRAW = 0x100,
                MF_USECHECKBITMAPS = 0x200,
                MF_SEPARATOR = 0x800,

                MF_SYSMENU = 0x2000,
                MF_HELP = 0x4000,
                MF_MOUSESELECT = 0x8000,

                MF_END = 0x80,
                MF_DEFAULT = 0x1000,

                MF_RIGHTJUSTIFY = 0x4000,

                MFS_CHECKED = MF_CHECKED,
                MFS_DEFAULT = MF_DEFAULT,
                MFS_ENABLED = MF_ENABLED,
                MFS_GRAYED = 0x3,
                MFS_DISABLED = MFS_GRAYED,
                MFS_HILITE = MF_HILITE,
                MFS_UNCHECKED = MF_UNCHECKED,
                MFS_UNHILITE = MF_UNHILITE,

                MFT_BITMAP = MF_BITMAP,
                MFT_MENUBARBREAK = MF_MENUBARBREAK,
                MFT_MENUBREAK = MF_MENUBREAK,
                MFT_OWNERDRAW = MF_OWNERDRAW,
                MFT_RADIOCHECK = 0x200,
                MFT_RIGHTJUSTIFY = MF_RIGHTJUSTIFY,
                MFT_RIGHTORDER = 0x2000,
                MFT_SEPARATOR = MF_SEPARATOR,
                MFT_STRING = MF_STRING,

                MIIM_STATE = 0x01,
                MIIM_ID = 0x02,
                MIIM_SUBMENU = 0x04,
                MIIM_CHECKMARKS = 0x08,
                MIIM_TYPE = 0x10,
                MIIM_DATA = 0x20,
                MIIM_EVERYTHING = 0x3F

            };
            #endregion windows

           

            public enum GWL
            {
                GWL_ID = (-12),
                GWL_STYLE = (-16),
                GWL_EXSTYLE = (-20)
            }

            public enum MISC
            {
                WS_VISIBLE = 0x10000000,
                WS_BORDER = 0x800000
               

            }

            #region Pipes
            public enum PIPEOPENMODE : uint
            {
                PIPE_ACCESS_INBOUND = 0x00000001,
                PIPE_ACCESS_OUTBOUND = 0x00000002,
                PIPE_ACCESS_DUPLEX = 0x00000003,
                FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000,
                FILE_FLAG_WRITE_THROUGH = 0x80000000,
                FILE_FLAG_OVERLAPPED = 0x40000000,
                //WRITE_DAC = 0x00040000L,
                //WRITE_OWNER = 0x00080000L,
                //ACCESS_SYSTEM_SECURITY = 0x01000000L
            };
            public enum PIPEMODE
            {
                PIPE_TYPE_BYTE = 0x00000000,
                PIPE_TYPE_MESSAGE = 0x00000004,
                PIPE_READMODE_BYTE = 0x00000000,
                PIPE_READMODE_MESSAGE = 0x00000002,
                PIPE_WAIT = 0x00000000,
                PIPE_NOWAIT = 0x00000001,
                PIPE_ACCEPT_REMOTE_CLIENTS = 0x00000000,
                PIPE_REJECT_REMOTE_CLIENTS = 0x00000008
            };
            public enum PIPETIMEOUT : uint
            {
                NMPWAIT_NOWAIT = 0x00000001,
                //Does not wait for the named pipe. If the named pipe is not available, the function returns an error.
                NMPWAIT_WAIT_FOREVER = 0xffffffff,
                //Waits indefinitely.
                NMPWAIT_USE_DEFAULT_WAIT = 0x00000000,
                //Uses the default time-out specified in a call to the CreateNamedPipe function. 
            };
            #endregion Pipes

            public enum TCN
            {
                TCN_FIRST = -550,
                TCN_LAST = -580,
                TCN_KEYDOWN = TCN_FIRST - 0,
                TCN_SELCHANGE = TCN_FIRST - 1,
                TCN_SELCHANGING = TCN_FIRST - 2
            }
            public enum TCS
            {
                TCS_SCROLLOPPOSITE = 0x0001,
                TCS_BOTTOM = 0x0002,
                TCS_RIGHT = 0x0002,
                TCS_MULTISELECT = 0x0004,
                TCS_FLATBUTTONS = 0x0008,
                TCS_FORCEICONLEFT = 0x0010,
                TCS_FORCELABELLEFT = 0x0020,
                TCS_HOTTRACK = 0x0040,
                TCS_VERTICAL = 0x0080,
                TCS_TABS = 0x0000,
                TCS_BUTTONS = 0x0100,
                TCS_SINGLELINE = 0x0000,
                TCS_MULTILINE = 0x0200,
                TCS_RIGHTJUSTIFY = 0x0000,
                TCS_FIXEDWIDTH = 0x0400,
                TCS_RAGGEDRIGHT = 0x0800,
                TCS_FOCUSONBUTTONDOWN = 0x1000,
                TCS_OWNERDRAWFIXED = 0x2000,
                TCS_TOOLTIPS = 0x4000,
                TCS_FOCUSNEVER = 0x8000
            }
            public enum TCIF
            {
                TCIF_TEXT = 0x0001,
                TCIF_IMAGE = 0x0002,
                TCIF_RTLREADING = 0x0004,
                TCIF_PARAM = 0x0008,
                TCIF_STATE = 0x0010,
                TCIF_ALL = 0x001F,
                TCIS_BUTTONPRESSED = 0x0001,
                TCIS_HIGHLIGHTED = 0x0002
            }
            public enum FILEOPEN
            {
                //used by GetOpenFileName
                OFN_READONLY = 0x1,
                OFN_OVERWRITEPROMPT = 0x2,
                OFN_HIDEREADONLY = 0x4,
                OFN_NOCHANGEDIR = 0x8,
                OFN_SHOWHELP = 0x10,
                OFN_ENABLEHOOK = 0x20,
                OFN_ENABLETEMPLATE = 0x40,
                OFN_ENABLETEMPLATEHANDLE = 0x80,
                OFN_NOVALIDATE = 0x100,
                OFN_ALLOWMULTISELECT = 0x200,
                OFN_EXTENSIONDIFFERENT = 0x400,
                OFN_PATHMUSTEXIST = 0x800,
                OFN_FILEMUSTEXIST = 0x1000,
                OFN_CREATEPROMPT = 0x2000,
                OFN_SHAREAWARE = 0x4000,
                OFN_NOREADONLYRETURN = 0x8000,
                OFN_NOTESTFILECREATE = 0x10000,
                OFN_SHAREFALLTHROUGH = 2,
                OFN_SHARENOWARN = 1,
                OFN_SHAREWARN = 0
            };
            public enum REGISTRY : uint
            {
                //* Function prototypes,  ants, and type definitions for Win32 Registry API
                // Registry hKey HIVE values 

                HKEY_CLASSES_ROOT = 0x80000000,
                HKEY_CURRENT_USER = 0x80000001,
                HKCU = 0x80000001,
                HKEY_LOCAL_MACHINE = 0x80000002,
                HKLM = 0x80000002,
                HKEY_USERS = 0x80000003,
                HKU = 0x80000003,
                HKEY_PERFORMANCE_DATA = 0x80000004,    //NT only
                HKEY_CURRENT_CONFIG = 0x80000005,
                HKCC = 0x80000005,
                HKEY_DYN_DATA = 0x80000006,   //95/98 only
                HKDD = 0x80000006,


                REG_NONE = 0,// ' No value Type
                REG_SZ = 1,// ' Unicode nul terminated String
                REG_EXPAND_SZ = 2,// ' Unicode nul terminated String
                REG_BINARY = 3,// ' Free form binary
                REG_DWORD = 4,// ' 32-bit number
                REG_DWORD_LITTLE_ENDIAN = 4,// ' 32-bit number (same as REG_DWORD)
                REG_DWORD_BIG_ENDIAN = 5,// ' 32-bit number
                REG_LINK = 6,// ' Symbolic Link (unicode)
                REG_MULTI_SZ = 7,// ' Multiple Unicode strings
                REG_RESOURCE_LIST = 8,// ' Resource list in the resource map
                REG_FULL_RESOURCE_DESCRIPTOR = 9,// ' Resource list in the hardware description
                REG_RESOURCE_REQUIREMENTS_LIST = 10,//
                REG_CREATED_NEW_KEY = 0x1,// ' New Registry Key created
                REG_OPENED_EXISTING_KEY = 0x2,// ' Existing Key opened
                REG_WHOLE_HIVE_VOLATILE = 0x1,// ' Restore whole hive volatile
                REG_REFRESH_HIVE = 0x2,// ' Unwind changes to last flush
                REG_NOTIFY_CHANGE_NAME = 0x1,// ' Create or delete (child)
                REG_NOTIFY_CHANGE_ATTRIBUTES = 0x2,//
                REG_NOTIFY_CHANGE_LAST_SET = 0x4,// ' Time stamp
                REG_NOTIFY_CHANGE_SECURITY = 0x8,//

                //* Reg Create Type Values...
                REG_OPTION_RESERVED = 0,// ' Parameter is reserved
                REG_OPTION_NON_VOLATILE = 0,// ' Key is preserved when system is rebooted
                REG_OPTION_VOLATILE = 1,// ' Key is not preserved when system is rebooted
                REG_OPTION_CREATE_LINK = 2,// ' Created key is a symbolic link
                REG_OPTION_BACKUP_RESTORE = 4,// ' open For backup or restore

                //REG_LEGAL_CHANGE_FILTER   = (REG_NOTIFY_CHANGE_NAME Or REG_NOTIFY_CHANGE_ATTRIBUTES Or REG_NOTIFY_CHANGE_LAST_SET Or REG_NOTIFY_CHANGE_SECURITY),//
                //REG_LEGAL_OPTION          = (REG_OPTION_RESERVED Or REG_OPTION_NON_VOLATILE Or REG_OPTION_VOLATILE Or REG_OPTION_CREATE_LINK Or REG_OPTION_BACKUP_RESTORE),//

                STANDARD_RIGHTS_READ = 0x20000,//
                STANDARD_RIGHTS_WRITE = 0x20000,//
                STANDARD_RIGHTS_EXECUTE = 0x20000,//
                STANDARD_RIGHTS_REQUIRED = 0xF0000,//
                STANDARD_RIGHTS_ALL = 0x1F0000,//
                DELETE = 0x10000,//
                READ_CONTROL = 0x20000,//
                WRITE_DAC = 0x40000,//
                WRITE_OWNER = 0x80000

            };
            public enum REGISTRYSECURITY
            {
                KEY_QUERY_VALUE = 0x1,
                KEY_SET_VALUE = 0x2,
                KEY_CREATE_SUB_KEY = 0x4,
                KEY_ENUMERATE_SUB_KEYS = 0x8,
                KEY_NOTIFY = 0x10,
                KEY_CREATE_LINK = 0x20

                //KEY_READ       = ((STANDARD_RIGHTS_READ Or KEY_QUERY_VALUE Or KEY_ENUMERATE_SUB_KEYS Or KEY_NOTIFY) And (Not SYNCHRONIZE))
                //KEY_WRITE      = ((STANDARD_RIGHTS_WRITE Or KEY_SET_VALUE Or KEY_CREATE_SUB_KEY) And (Not SYNCHRONIZE))
                //KEY_EXECUTE    = (KEY_READ)
                //KEY_ALL_ACCESS = ((STANDARD_RIGHTS_ALL Or KEY_QUERY_VALUE Or KEY_SET_VALUE Or KEY_CREATE_SUB_KEY Or KEY_ENUMERATE_SUB_KEYS Or KEY_NOTIFY Or KEY_CREATE_LINK) And (Not SYNCHRONIZE))
            };
            public enum SNDPLAYSOUND
            {
                //' flag values for uFlags parameter; used by sndPlaySound 
                SND_SYNC = 0x0000,//     '  play synchronously (default)
                SND_ASYNC = 0x0001,//     '  play asynchronously
                SND_NODEFAULT = 0x0002,//     '  silence not default, if sound not found
                SND_MEMORY = 0x0004,//     '  lpszSoundName points to a memory file
                SND_LOOP = 0x0008,//     '  loop the sound until next sndPlaySound
                SND_NOSTOP = 0x0010,//     '  don't stop any currently playing sound
                SND_ALIAS = 0x10000,//    '  name is a WIN.INI [sounds] entry
                SND_FILENAME = 0x20000,//    '  name is a file name
                SND_RESOURCE = 0x40004,//    '  name is a resource name or atom
                SND_ALIAS_ID = 0x110000,//   '  name is a WIN.INI [sounds] entry identifier
                SND_ALIAS_START = 0,//          '  must be > 4096 to keep strings in same section of resource file
                SND_VALID = 0x1F,//       '  valid flags 
                SND_NOWAIT = 0x2000,//     '  don't wait if the driver is busy
                SND_VALIDFLAGS = 0x17201F,//   '  set of valid flag bits; anything outside this range will raise an error
                //SND_RESERVED = 0xFF000000,// '  these flags are reserved
                SND_TYPE_MASK = 0x170007
            }
            public enum PROCESS
            {
                NILL = 0,
                WM_SYSCOMMAND = 0x112,
                SYNCHRONIZE = 0x100000,
                NORMAL_PRIORITY_CLASS = 0x20,
                PROCESS_TERMINATE = 0x1

            };
            public enum WINDOWSOSVERSION
            {
                PLATFORM_WIN32s = 0,//    'Win 3.1
                PLATFORM_WIN32_WINDOWS = 1,//    'Win95/98
                PLATFORM_WIN32_NT = 2//    'Win NT
            };
            public enum ENUMDISPLAYSETTING
            {
                DM_PELSWIDTH = 0x80000,
                DM_PELSHEIGHT = 0x100000,
                DM_DISPLAYFLAGS = 0x200000,
                DM_BITSPERPEL = 0x40000,
                DM_DISPLAYFREQUENCY = 0x400000
            };
            public enum DISPLAYSETTING
            {
                DM_GRAYSCALE = 0x1,
                DM_INTERLACED = 0x2
            };
            public enum DEVMODEENUM
            {
                CCDEVICENAME = 32,
                CCFORMNAME = 32
            };
            public enum CHANGEDISPLAYSETTING
            {
                CDS_UPDATEREGISTRY = 0x1,
                CDS_TEST = 0x2,
                CDS_FULLSCREEN = 0x4,
                CDS_GLOBAL = 0x8,
                CDS_SET_PRIMARY = 0x10,
                CDS_RESET = 0x40000000,
                CDS_SETRECT = 0x20000000,
                CDS_NORESET = 0x10000000,

                DISP_CHANGE_SUCCESSFUL = 0,
                DISP_CHANGE_RESTART = 1,
                DISP_CHANGE_FAILED = -1,
                DISP_CHANGE_BADMODE = -2,
                DISP_CHANGE_NOTUPDATED = -3,
                DISP_CHANGE_BADFLAGS = -4,
                DISP_CHANGE_BADPARAM = -5


            };
            public enum GETDEVICECAPS
            {
                PLANES = 14,
                BITSPIXEL = 12
            };
            public enum GETSYSTEMMETRICS
            {
                SM_CXSCREEN = 0,
                SM_CYSCREEN = 1
            };
            
            public enum SYSTEMMENUCOMMAND
            {
                SC_SIZE = 0xF000,
                SC_MOVE = 0xF010,
                SC_MINIMIZE = 0xF020,
                SC_MAXIMIZE = 0xF030,
                SC_NEXTWINDOW = 0xF040,
                SC_PREVWINDOW = 0xF050,
                SC_CLOSE = 0xF060,
                SC_VSCROLL = 0xF070,
                SC_HSCROLL = 0xF080,
                SC_MOUSEMENU = 0xF090,
                SC_KEYMENU = 0xF100,
                SC_ARRANGE = 0xF110,
                SC_RESTORE = 0xF120,
                SC_TASKLIST = 0xF130,
                SC_SCREENSAVE = 0xF140,
                SC_HOTKEY = 0xF150,

                SC_ICON = SC_MINIMIZE,
                SC_ZOOM = SC_MAXIMIZE
            }
            public enum CURSORID
            {
                IDC_ARROW = 32512,
                IDC_IBEAM = 32513,
                IDC_WAIT = 32514,
                IDC_CROSS = 32515,
                IDC_UPARROW = 32516,
                IDC_SIZE = 32640,
                IDC_ICON = 32641,
                IDC_SIZENWSE = 32642,
                IDC_SIZENESW = 32643,
                IDC_SIZEWE = 32644,
                IDC_SIZENS = 32645,
                IDC_SIZEALL = 32646,
                IDC_NO = 32648,
                IDC_APPSTARTING = 32650

            }
            public enum ERRORRETURNCODES : uint
            {
                APPLICATION_ERROR_MASK = 0x20000000,
                ERROR_SEVERITY_SUCCESS = 0x0,
                ERROR_SEVERITY_INFORMATIONAL = 0x40000000,
                ERROR_SEVERITY_WARNING = 0x80000000,
                ERROR_SEVERITY_ERROR = 0xC0000000,

                //'* Win32 API return codes
                NO_ERROR = 0,
                ERROR_SUCCESS = 0,
                ERROR_INVALID_FUNCTION = 1,
                ERROR_FILE_NOT_FOUND = 2, //
                ERROR_PATH_NOT_FOUND = 3, //
                ERROR_TOO_MANY_OPEN_FILES = 4, //
                ERROR_ACCESS_DENIED = 5, //
                ERROR_INVALID_HANDLE = 6, //
                ERROR_ARENA_TRASHED = 7, //
                ERROR_NOT_ENOUGH_MEMORY = 8,
                ERROR_INVALID_BLOCK = 9, //
                ERROR_BAD_ENVIRONMENT = 10, //
                ERROR_BAD_FORMAT = 11, //
                ERROR_INVALID_ACCESS = 12, //
                ERROR_INVALID_DATA = 13, //
                ERROR_OUTOFMEMORY = 14, //
                ERROR_INVALID_DRIVE = 15, //
                ERROR_CURRENT_DIRECTORY = 16, //
                ERROR_NOT_SAME_DEVICE = 17, //
                ERROR_NO_MORE_FILES = 18, //
                ERROR_WRITE_PROTECT = 19, //
                ERROR_BAD_UNIT = 20, //
                ERROR_NOT_READY = 21, //
                ERROR_BAD_COMMAND = 22, //
                ERROR_CRC = 23, //
                ERROR_BAD_LENGTH = 24, //
                ERROR_SEEK = 25, //
                ERROR_NOT_DOS_DISK = 26, //
                ERROR_SECTOR_NOT_FOUND = 27, //
                ERROR_OUT_OF_PAPER = 28, //
                ERROR_WRITE_FAULT = 29, //
                ERROR_READ_FAULT = 30, //
                ERROR_GEN_FAILURE = 31, //
                ERROR_SHARING_VIOLATION = 32, //
                ERROR_LOCK_VIOLATION = 33, //
                ERROR_WRONG_DISK = 34, //
                ERROR_SHARING_BUFFER_EXCEEDED = 36, //
                ERROR_HANDLE_EOF = 38, //
                ERROR_HANDLE_DISK_FULL = 39, //
                ERROR_NOT_SUPPORTED = 50, //
                ERROR_REM_NOT_LIST = 51, //   'network
                ERROR_DUP_NAME = 52, //   'network
                ERROR_BAD_NETPATH = 53, //   'network
                ERROR_NETWORK_BUSY = 54, //   'network
                ERROR_DEV_NOT_EXIST = 55, //   'network
                ERROR_TOO_MANY_CMDS = 56, //   'network
                ERROR_ADAP_HDW_ERR = 57, //   'network
                ERROR_BAD_NET_RESP = 58, //   'network
                ERROR_UNEXP_NET_ERR = 59, //   'network
                ERROR_BAD_REM_ADAP = 60, //   'network
                ERROR_PRINTQ_FULL = 61, //
                ERROR_NO_SPOOL_SPACE = 62, //
                ERROR_PRINT_CANCELLED = 63, //
                ERROR_NETNAME_DELETED = 64, //   'network
                ERROR_NETWORK_ACCESS_DENIED = 65, // 'network
                ERROR_BAD_DEV_TYPE = 66, //   'network
                ERROR_BAD_NET_NAME = 67, //   'network
                ERROR_TOO_MANY_NAMES = 68, //   'network
                ERROR_TOO_MANY_SESS = 69, //   'network
                ERROR_SHARING_PAUSED = 70, //   'network
                ERROR_REQ_NOT_ACCEP = 71, //   'network
                ERROR_REDIR_PAUSED = 72, //   'print
                ERROR_FILE_EXISTS = 80, //
                ERROR_CANNOT_MAKE = 82, //
                ERROR_FAIL_I24 = 83, //   'interrupt 24
                ERROR_OUT_OF_STRUCTURES = 84, //
                ERROR_ALREADY_ASSIGNED = 85, //   'device mapping
                ERROR_INVALID_PASSWORD = 86, //   'network
                ERROR_INVALID_PARAMETER = 87,
                ERROR_NET_WRITE_FAULT = 88, //
                ERROR_NO_PROC_SLOTS = 89, //   'process
                ERROR_TOO_MANY_SEMAPHORES = 100, //
                ERROR_EXCL_SEM_ALREADY_OWNED = 101, //
                ERROR_SEM_IS_SET = 102, //
                ERROR_TOO_MANY_SEM_REQUESTS = 103, //
                ERROR_INVALID_AT_INTERRUPT_TIME = 104, //  'semaphore set
                ERROR_SEM_OWNER_DIED = 105, //
                ERROR_SEM_USER_LIMIT = 106, //        'change diskette?
                ERROR_DISK_CHANGE = 107, //        'did not change diskette
                ERROR_DRIVE_LOCKED = 108, //
                ERROR_BROKEN_PIPE = 109, //
                ERROR_OPEN_FAILED = 110, //
                ERROR_BUFFER_OVERFLOW = 111, //        'file too long
                ERROR_DISK_FULL = 112, //
                ERROR_NO_MORE_SEARCH_HANDLES = 113, //    'file ids
                ERROR_INVALID_TARGET_HANDLE = 114, //    'file id
                ERROR_INVALID_CATEGORY = 117, //    'invalid IOCTL
                ERROR_INVALID_VERIFY_SWITCH = 118, //
                ERROR_BAD_DRIVER_LEVEL = 119, //    'unsupported command
                ERROR_CALL_NOT_IMPLEMENTED = 120, //    'call only NT valid
                ERROR_SEM_TIMEOUT = 121, //
                ERROR_INSUFFICIENT_BUFFER = 122,
                ERROR_INVALID_NAME = 123, //           
                ERROR_INVALID_LEVEL = 124, //
                ERROR_NO_VOLUME_LABEL = 125, //
                ERROR_MOD_NOT_FOUND = 126, //        'module (DLL?)
                ERROR_PROC_NOT_FOUND = 127, //
                ERROR_WAIT_NO_CHILDREN = 128, //
                ERROR_CHILD_NOT_COMPLETE = 129, //        'cannot run in NT mode
                ERROR_DIRECT_ACCESS_HANDLE = 130, //
                ERROR_NEGATIVE_SEEK = 131, //
                ERROR_SEEK_ON_DEVICE = 132, //
                ERROR_IS_JOIN_TARGET = 133, //        'cannot JOIN already JOINed
                ERROR_IS_JOINED = 134, //
                ERROR_IS_SUBSTED = 135, //
                ERROR_NOT_JOINED = 136, //
                ERROR_NOT_SUBSTED = 137, //
                ERROR_JOIN_TO_JOIN = 138, //
                ERROR_SUBST_TO_SUBST = 139, //
                ERROR_JOIN_TO_SUBST = 140, //
                ERROR_SUBST_TO_JOIN = 141, //
                ERROR_BUSY_DRIVE = 142, //
                ERROR_SAME_DRIVE = 143, //
                ERROR_DIR_NOT_ROOT = 144, //
                ERROR_DIR_NOT_EMPTY = 145, //
                ERROR_IS_SUBST_PATH = 146, //
                ERROR_IS_JOIN_PATH = 147, //
                ERROR_PATH_BUSY = 148, //
                ERROR_IS_SUBST_TARGET = 149, //
                ERROR_SYSTEM_TRACE = 150, //
                ERROR_INVALID_EVENT_COUNT = 151, //
                ERROR_TOO_MANY_MUXWAITERS = 152, //
                ERROR_INVALID_LIST_FORMAT = 153, //
                ERROR_LABEL_TOO_LONG = 154, //
                ERROR_TOO_MANY_TCBS = 155, //
                ERROR_SIGNAL_REFUSED = 156, //
                ERROR_DISCARDED = 157, //
                ERROR_NOT_LOCKED = 158, //
                ERROR_BAD_THREADID_ADDR = 159, //
                ERROR_BAD_ARGUMENTS = 160, //
                ERROR_BAD_PATHNAME = 161, //
                ERROR_SIGNAL_PENDING = 162, //
                ERROR_MAX_THRDS_REACHED = 164, //
                ERROR_LOCK_FAILED = 167, //
                ERROR_BUSY = 170, //
                ERROR_CANCEL_VIOLATION = 173, //
                ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174, //
                ERROR_INVALID_SEGMENT_NUMBER = 180, //
                ERROR_INVALID_ORDINAL = 182, //
                ERROR_ALREADY_EXISTS = 183, //
                ERROR_INVALID_FLAG_NUMBER = 186, //
                ERROR_SEM_NOT_FOUND = 187, //
                ERROR_INVALID_STARTING_CODESEG = 188, //
                ERROR_INVALID_STACKSEG = 189, //
                ERROR_INVALID_MODULETYPE = 190, //
                ERROR_INVALID_EXE_SIGNATURE = 191, //
                ERROR_EXE_MARKED_INVALID = 192, //
                ERROR_BAD_EXE_FORMAT = 193, //
                ERROR_ITERATED_DATA_EXCEEDS_64k = 194, //
                ERROR_INVALID_MINALLOCSIZE = 195, //
                ERROR_DYNLINK_FROM_INVALID_RING = 196, //
                ERROR_IOPL_NOT_ENABLED = 197, //
                ERROR_INVALID_SEGDPL = 198, //
                ERROR_AUTODATASEG_EXCEEDS_64k = 199, //
                ERROR_RING2SEG_MUST_BE_MOVABLE = 200, //
                ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201, //
                ERROR_INFLOOP_IN_RELOC_CHAIN = 202, //
                ERROR_ENVVAR_NOT_FOUND = 203, //
                ERROR_NO_SIGNAL_SENT = 205, //
                ERROR_FILENAME_EXCED_RANGE = 206, //
                ERROR_RING2_STACK_IN_USE = 207, //
                ERROR_META_EXPANSION_TOO_LONG = 208, //
                ERROR_INVALID_SIGNAL_NUMBER = 209, //
                ERROR_THREAD_1_INACTIVE = 210, //
                ERROR_LOCKED = 212, //
                ERROR_TOO_MANY_MODULES = 214, //
                ERROR_NESTING_NOT_ALLOWED = 215, //
                ERROR_BAD_PIPE = 230, //
                ERROR_PIPE_BUSY = 231, //
                ERROR_NO_DATA = 232, //
                ERROR_PIPE_NOT_CONNECTED = 233, //
                ERROR_MORE_DATA = 234, //           ' dderror
                ERROR_VC_DISCONNECTED = 240, //
                ERROR_INVALID_EA_NAME = 254, //
                ERROR_EA_LIST_INCONSISTENT = 255, //
                ERROR_NO_MORE_ITEMS = 259, //
                ERROR_CANNOT_COPY = 266, //
                ERROR_DIRECTORY = 267, //
                ERROR_EAS_DIDNT_FIT = 275, //
                ERROR_EA_FILE_CORRUPT = 276, //
                ERROR_EA_TABLE_FULL = 277, //
                ERROR_INVALID_EA_HANDLE = 278, //
                ERROR_EAS_NOT_SUPPORTED = 282, //
                ERROR_NOT_OWNER = 288, //
                ERROR_TOO_MANY_POSTS = 298, //
                ERROR_MR_MID_NOT_FOUND = 317, //
                ERROR_INVALID_ADDRESS = 487, //
                ERROR_ARITHMETIC_OVERFLOW = 534, //
                ERROR_PIPE_CONNECTED = 535, //
                ERROR_PIPE_LISTENING = 536, //
                ERROR_EA_ACCESS_DENIED = 994, //
                ERROR_OPERATION_ABORTED = 995, //
                ERROR_IO_INCOMPLETE = 996, //
                ERROR_IO_PENDING = 997, //           ' dderror
                ERROR_NOACCESS = 998, //
                ERROR_SWAPERROR = 999, //
                ERROR_STACK_OVERFLOW = 1001, //
                ERROR_INVALID_MESSAGE = 1002, //
                ERROR_CAN_NOT_COMPLETE = 1003, //
                ERROR_INVALID_FLAGS = 1004, //
                ERROR_UNRECOGNIZED_VOLUME = 1005, //
                ERROR_FILE_INVALID = 1006, //
                ERROR_FULLSCREEN_MODE = 1007, //
                ERROR_NO_TOKEN = 1008, //
                ERROR_BADDB = 1009, //
                ERROR_BADKEY = 1010, //
                ERROR_CANTOPEN = 1011, //
                ERROR_CANTREAD = 1012, //
                ERROR_CANTWRITE = 1013, //
                ERROR_REGISTRY_RECOVERED = 1014, //
                ERROR_REGISTRY_CORRUPT = 1015, //
                ERROR_REGISTRY_IO_FAILED = 1016, //
                ERROR_NOT_REGISTRY_FILE = 1017, //
                ERROR_KEY_DELETED = 1018, //
                ERROR_NO_LOG_SPACE = 1019, //
                ERROR_KEY_HAS_CHILDREN = 1020, //
                ERROR_CHILD_MUST_BE_VOLATILE = 1021, //
                ERROR_NOTIFY_ENUM_DIR = 1022, //
                ERROR_DEPENDENT_SERVICES_RUNNING = 1051, //
                ERROR_INVALID_SERVICE_CONTROL = 1052, //
                ERROR_SERVICE_REQUEST_TIMEOUT = 1053, //
                ERROR_SERVICE_NO_THREAD = 1054, //
                ERROR_SERVICE_DATABASE_LOCKED = 1055, //
                ERROR_SERVICE_ALREADY_RUNNING = 1056, //
                ERROR_INVALID_SERVICE_ACCOUNT = 1057, //
                ERROR_SERVICE_DISABLED = 1058, //
                ERROR_CIRCULAR_DEPENDENCY = 1059, //
                ERROR_SERVICE_DOES_NOT_EXIST = 1060, //
                ERROR_SERVICE_CANNOT_ACCEPT_CTRL = 1061, //
                ERROR_SERVICE_NOT_ACTIVE = 1062, //
                ERROR_FAILED_SERVICE_CONTROLLER_CONNECT = 1063, //
                ERROR_EXCEPTION_IN_SERVICE = 1064, //
                ERROR_DATABASE_DOES_NOT_EXIST = 1065, //
                ERROR_SERVICE_SPECIFIC_ERROR = 1066, //
                ERROR_PROCESS_ABORTED = 1067, //
                ERROR_SERVICE_DEPENDENCY_FAIL = 1068, //
                ERROR_SERVICE_LOGON_FAILED = 1069, //
                ERROR_SERVICE_START_HANG = 1070, //
                ERROR_INVALID_SERVICE_LOCK = 1071, //
                ERROR_SERVICE_MARKED_FOR_DELETE = 1072, //
                ERROR_SERVICE_EXISTS = 1073, //
                ERROR_ALREADY_RUNNING_LKG = 1074, //
                ERROR_SERVICE_DEPENDENCY_DELETED = 1075, //
                ERROR_BOOT_ALREADY_ACCEPTED = 1076, //
                ERROR_SERVICE_NEVER_STARTED = 1077, //
                ERROR_DUPLICATE_SERVICE_NAME = 1078, //
                ERROR_END_OF_MEDIA = 1100, //
                ERROR_FILEMARK_DETECTED = 1101, //
                ERROR_BEGINNING_OF_MEDIA = 1102, //
                ERROR_SETMARK_DETECTED = 1103, //
                ERROR_NO_DATA_DETECTED = 1104, //
                ERROR_PARTITION_FAILURE = 1105, //
                ERROR_INVALID_BLOCK_LENGTH = 1106, //
                ERROR_DEVICE_NOT_PARTITIONED = 1107, //
                ERROR_UNABLE_TO_LOCK_MEDIA = 1108, //
                ERROR_UNABLE_TO_UNLOAD_MEDIA = 1109, //
                ERROR_MEDIA_CHANGED = 1110, //
                ERROR_BUS_RESET = 1111, //
                ERROR_NO_MEDIA_IN_DRIVE = 1112, //
                ERROR_NO_UNICODE_TRANSLATION = 1113, //
                ERROR_DLL_INIT_FAILED = 1114, //
                ERROR_SHUTDOWN_IN_PROGRESS = 1115, //
                ERROR_NO_SHUTDOWN_IN_PROGRESS = 1116, //
                ERROR_IO_DEVICE = 1117, //
                ERROR_SERIAL_NO_DEVICE = 1118, //
                ERROR_IRQ_BUSY = 1119, //
                ERROR_MORE_WRITES = 1120, //
                ERROR_COUNTER_TIMEOUT = 1121, //
                ERROR_FLOPPY_ID_MARK_NOT_FOUND = 1122, //
                ERROR_FLOPPY_WRONG_CYLINDER = 1123, //
                ERROR_FLOPPY_UNKNOWN_ERROR = 1124, //
                ERROR_FLOPPY_BAD_REGISTERS = 1125, //
                ERROR_DISK_RECALIBRATE_FAILED = 1126, //
                ERROR_DISK_OPERATION_FAILED = 1127, //
                ERROR_DISK_RESET_FAILED = 1128, //
                ERROR_EOM_OVERFLOW = 1129, //
                ERROR_NOT_ENOUGH_SERVER_MEMORY = 1130, //
                ERROR_POSSIBLE_DEADLOCK = 1131, //
                ERROR_MAPPED_ALIGNMENT = 1132, //
                ERROR_INVALID_PIXEL_FORMAT = 2000,
                ERROR_BAD_DRIVER = 2001,
                ERROR_INVALID_WINDOW_STYLE = 2002,
                ERROR_METAFILE_NOT_SUPPORTED = 2003,
                ERROR_TRANSFORM_NOT_SUPPORTED = 2004,
                ERROR_CLIPPING_NOT_SUPPORTED = 2005,
                ERROR_UNKNOWN_PRINT_MONITOR = 3000,
                ERROR_PRINTER_DRIVER_IN_USE = 3001,
                ERROR_SPOOL_FILE_NOT_FOUND = 3002,
                ERROR_SPL_NO_STARTDOC = 3003,
                ERROR_SPL_NO_ADDJOB = 3004,
                ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED = 3005,
                ERROR_PRINT_MONITOR_ALREADY_INSTALLED = 3006,
                ERROR_WINS_INTERNAL = 4000,
                ERROR_CAN_NOT_DEL_LOCAL_WINS = 4001,
                ERROR_STATIC_INIT = 4002,
                ERROR_INC_BACKUP = 4003,
                ERROR_FULL_BACKUP = 4004,
                ERROR_REC_NON_EXISTENT = 4005,
                ERROR_RPL_NOT_ALLOWED = 4006,
                SEVERITY_SUCCESS = 0,
                SEVERITY_ERROR = 1,
                FACILITY_NT_BIT = 0x10000000,
                NOERROR = 0,
                //E_UNEXPECTED = 0x8000FFFF,
                //E_NOTIMPL = 0x80004001,
                //E_OUTOFMEMORY = 0x8007000E,
                //E_INVALIDARG = 0x80070057,
                //E_NOINTERFACE = 0x80004002,
                //E_POINTER = 0x80004003,
                //E_HANDLE = 0x80070006,
                //E_ABORT = 0x80004004,
                //E_FAIL = 0x80004005,
                //E_ACCESSDENIED = 0x80070005

            }
        }
        #endregion

        #region structs

        public class WindowsApiStructs
        {
            public struct MOUSEINPUT
            {
                public int dx;
                public int dy;
                public uint mouseData;
                public uint dwFlags;
                public uint time;
                public IntPtr dwExtraInfo;
            }

            public struct KEYBDINPUT
            {
                public ushort wVk;
                public ushort wScan;
                public uint dwFlags;
                public uint time;
                public IntPtr dwExtraInfo;
            }
            public struct HARDWAREINPUT
            {
                public int uMsg;
                public short wParamL;
                public short wParamH;
            }
            [StructLayout(LayoutKind.Explicit)]
            public struct MOUSEKEYBDHARDWAREINPUT
            {
                [FieldOffset(0)]
                public MOUSEINPUT mi;

                [FieldOffset(0)]
                public KEYBDINPUT ki;

                [FieldOffset(0)]
                public HARDWAREINPUT hi;
            }

            public struct INPUT
            {
                public int type;
                public MOUSEKEYBDHARDWAREINPUT mkhi;
            }

            #region public struct GUITHREADINFO
            [Serializable, StructLayout(LayoutKind.Sequential)]
            public struct GUITHREADINFO
            {
                public uint cbSize;
                public uint flags;
                public IntPtr hwndActive;
                public IntPtr hwndFocus;
                public IntPtr hwndCapture;
                public IntPtr hwndMenuOwner;
                public IntPtr hwndMoveSize;
                public IntPtr hwndCaret;
                public RECT rcCaret;
            }
            #endregion public struct GUITHREADINFO

            #region public struct POINT
            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int X;
                public int Y;

                public POINT(int x, int y)
                {
                    this.X = x;
                    this.Y = y;
                }

                public static implicit operator System.Drawing.Point(POINT p)
                {
                    return new System.Drawing.Point(p.X, p.Y);
                }

                public static implicit operator POINT(System.Drawing.Point p)
                {
                    return new POINT(p.X, p.Y);
                }
            }
            #endregion public struct POINT

            #region public class KBDLLHOOKSTRUCT
            [StructLayout(LayoutKind.Sequential)]
            public class KBDLLHOOKSTRUCT
            {
                public UInt32 vkCode;
                public UInt32 scanCode;
                public UInt32 flags;
                public UInt32 time;
                public IntPtr dwExtraInfo;
            }
            #endregion public class KBDLLHOOKSTRUCT

            #region public struct MSLLHOOKSTRUCT
            [StructLayout(LayoutKind.Sequential)]
            public struct MSLLHOOKSTRUCT
            {
                public POINT pt;
                public int mouseData;
                public int flags;
                public int time;
                public IntPtr dwExtraInfo;
            }
            #endregion public struct MSLLHOOKSTRUCT

            #region public class SystemPowerStatus
            [StructLayout(LayoutKind.Sequential)]
            public class SystemPowerStatus
            {
                public WindowsApiEnums.ACLineStatus _ACLineStatus;
                public WindowsApiEnums.BatteryFlag _BatteryFlag;
                public Byte _BatteryLifePercent;
                public Byte _Reserved1;
                public Int32 _BatteryLifeTime;
                public Int32 _BatteryFullLifeTime;
            }
            #endregion public class SystemPowerStatus

            #region public struct OVERLAPPED
            public struct OVERLAPPED
            {
                public IntPtr Internal;
                public IntPtr InternalHigh;
                public UInt32 Offset;
                public UInt32 OffsetHigh;
                public IntPtr EventHandle;
            };
            #endregion public struct OVERLAPPED



            #region public struct RECT
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            };
            #endregion public struct RECT

            #region public struct WINDOWINFO
            public struct WINDOWINFO
            {
                public int cbSize;
                public RECT rcWindow;
                public RECT rcClient;
                public int dwStyle;
                public int dwExStyle;
                public int dwWindowStatus;
                public uint cxWindowBorders;
                public uint cyWindowBorders;
                public int atomWindowType;
                public int wCreatorVersion;
            };
            #endregion public struct WINDOWINFO

            #region public struct WINDOWPLACEMENT
            public struct WINDOWPLACEMENT
            {
                public uint length;
                public uint flags;
                public WindowsApiEnums.WINDOWINFOCMD showCmd;
                //public uint showCmd;
                public Point ptMinPosition;
                public Point ptMaxPosition;
                public RECT rcNormalPosition;
            };
            #endregion public struct WINDOWPLACEMENT

            #region public struct MEMORYSTATUS
            public struct MEMORYSTATUS
            {
                public long dwLength;
                public long dwMemoryLoad;
                public long dwTotalPhys;
                public long dwAvailPhys;
                public long dwTotalPageFile;
                public long dwAvailPageFile;
                public long dwTotalVirtual;
                public long dwAvailVirtual;
            };
            #endregion public struct MEMORYSTATUS

            #region public struct OPENFILENAME
            public struct OPENFILENAME
            {
                public long lStructSize;
                public long hwndOwner;
                public long hInstance;
                public long lpstrFilter;
                public long lpstrCustomFilter;
                public long nMaxCustFilter;
                public long nFilterIndex;
                public long lpstrFile;
                public long nMaxFile;
                public long lpstrFileTitle;
                public long nMaxFileTitle;
                public long lpstrInitialDir;
                public long lpstrTitle;
                public long Flags;
                public int nFileOffset;
                public int nFileExtension;
                public long lpstrDefExt;
                public long lCustData;
                public long LpfnHook;
                public long lpTemplateName;
            };
            #endregion public struct OPENFILENAME

            #region public struct OSVERSIONINFO
            public struct OSVERSIONINFO
            {
                public long dwOSVersionInfoSize;
                //'   Specifies the size, in bytes, of this data structure.
                //'   Set this member to sizeof(OSVERSIONINFO) before calling the GetVersionEx function.
                public long dwMajorVersion;
                //'    Identifies the major version number of the operating system.
                //'    Examples:
                //'    3 = the major version number for Windows NT version 3.51
                //'    4 = the major version number for Windows NT version 4.0
                public long dwMinorVersion;
                //'    Identifies the minor version number of the operating system.
                //'    Examples:
                //'    51 = the minor version number for Windows NT version 3.51 
                //'     0 = the minor version number for Windows NT version 4.0 
                //'    For Windows 95, dwMinorVersion is zero.
                //'    For Windows 98, dwMinorVersion is greater than zero.
                public long dwBuildNumber;
                //'    Windows NT: Identifies the build number of the operating system.
                //'    Windows 95: Identifies the build number of the operating system 
                //'                in the low-order word.  The high-order word contains 
                //'                the major and minor version numbers.
                public long dwPlatformId;
                //'    Identifies the operating system platform.
                //'    This member can be one of the following values: Value Platform
                //'    PLATFORM_WIN32s Win32s on Windows 3.1.
                //'    PLATFORM_WIN32_WINDOWS Win32 on Windows 95 or Windows 98.
                //'    PLATFORM_WIN32_NT Win32 on Windows NT.
                public string szCSDVersion;//      '  Maintenance string for PSS usage
                //'    Windows NT: Contains a null-terminated string, such as "Service Pack 3",
                //'            that indicates the latest Service Pack installed on the system.
                //'            If no Service Pack has been installed, the string is empty.
                //'    Windows 95: Contains a null-terminated string
                //'            that provides arbitrary additional information about the operating system.

            };
            #endregion public struct OSVERSIONINFO

            #region public struct PROCESS_INFORMATION
            public struct PROCESS_INFORMATION
            {
                public long hProcess;
                public long hThread;
                public long dwProcessId;
                public long dwThreadId;
            };
            #endregion public struct PROCESS_INFORMATION

            #region public struct STARTUPINFO
            public struct STARTUPINFO
            {
                public long cb;
                public string lpReserved;
                public string lpDesktop;
                public string lpTitle;
                public long dwX;
                public long dwY;
                public long dwXSize;
                public long dwYSize;
                public long dwXCountChars;
                public long dwYCountChars;
                public long dwFillAttribute;
                public long dwFlags;
                public int wShowWindow;
                public int cbReserved2;
                public long lpReserved2;
                public long hStdInput;
                public long hStdOutput;
                public long hStdError;
            };
            #endregion public struct STARTUPINFO

            #region public struct DEVMODE
            public struct DEVMODE
            {
                public string dmDeviceName;
                public int dmSpecVersion;
                public int dmDriverVersion;
                public int dmSize;
                public int dmDriverExtra;
                public long dmFields;
                public int dmOrientation;
                public int dmPaperSize;
                public int dmPaperLength;
                public int dmPaperWidth;
                public int dmScale;
                public int dmCopies;
                public int dmDefaultSource;
                public int dmPrintQuality;
                public int dmColor;
                public int dmDuplex;
                public int dmYResolution;
                public int dmTTOption;
                public int dmCollate;
                public string dmFormName;
                public int dmUnusedPadding;
                public long dmBitsPerPel;
                public long dmPelsWidth;
                public long dmPelsHeight;
                public long dmDisplayFlags;
                public long dmDisplayFrequency;
            };
            #endregion public struct DEVMODE

            #region public struct MENUITEMINFO
            [StructLayout(LayoutKind.Sequential)]
            public struct MENUITEMINFO
            {
                public uint cbSize;
                public uint fMask;
                public uint fType;
                public uint fState;
                public int wID;
                public int hSubMenu;
                public int hbmpChecked;
                public int hbmpUnchecked;
                public int dwItemData;
                public string dwTypeData;
                public uint cch;
                public int hbmpItem;
            };
            #endregion public struct MENUITEMINFO

            #region public struct ACL
            public struct ACL
            {
                public string AclRevision;
                public string Sbz1;// 'was VB Byte
                public int AclSize;
                public int AceCount;
                public int Sbz2;
            };
            #endregion public struct ACL

            #region public struct SECURITY_ATTRIBUTES
            public struct SECURITY_ATTRIBUTES
            {
                public long nLength;
                public long lpSecurityDescriptor;
                public long bInheritHandle;
            };
            #endregion public struct SECURITY_ATTRIBUTES

            #region public struct FILETIME
            public struct FILETIME
            {
                public long dwLowDateTime;
                public long dwHighDateTime;
            };
            #endregion public struct FILETIME

            #region public struct SECURITY_DESCRIPTOR
            public struct SECURITY_DESCRIPTOR
            {
                public string Revision;//'was VB Byte
                public string Sbz1;//'was VB Byte
                public string Control;
                public string Owner;
                public string Group;
                public ACL Sacl;
                public ACL Dacl;
            }
            #endregion public struct SECURITY_DESCRIPTOR


            #region public struct SYSTEMTIME
            public struct SYSTEMTIME
            {
                public int wYear;
                public int wMonth;
                public int wDayOfWeek;
                public int wDay;
                public int wHour;
                public int wMinute;
                public int wSecond;
                public int wMilliseconds;
            };

            
            #endregion public struct SYSTEMTIME


        }
        #endregion     

    
}
