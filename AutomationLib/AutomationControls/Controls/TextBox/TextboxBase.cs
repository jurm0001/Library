using System;
using System.Collections.Generic;
using System.Text;

using WindowsApis;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class TextboxBase : Control
    {
        public static string CLASSNAME = "TEXTBOX";
        public TextboxBase(IntPtr handle)
            : base(handle)
        {
        }
        public TextboxBase(IntPtr handle, string text)
            : base(handle, text)
        {            
        }
      
    }
}
