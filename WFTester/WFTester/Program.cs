using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utilities.Extensions;
using AutomationLib;

namespace WFTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            AutomationLib.WindowsAdapter lib = new AutomationLib.WindowsAdapter("NotePad");
            lib.StartApplication(@"C:\Windows\System32\Calc.exe");
            IntPtr h = lib.FindWindowByCaption("Calculator");

            lib.BuildControlList(h);

            //lib.CloseWin(h);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
