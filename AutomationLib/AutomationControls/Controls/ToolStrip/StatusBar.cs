using System;
using System.Collections.Generic;
using System.Text;
using WindowsApis.Data;
using WindowsApis;

using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class StatusBar : ToolStripBase
    {
        public StatusBar(IntPtr handle) : base(handle) { }

    }
}