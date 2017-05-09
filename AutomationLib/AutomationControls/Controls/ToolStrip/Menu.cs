using System;
using System.Collections.Generic;
using System.Text;
using WindowsApis.Data;
using WindowsApis;


using AutomationLib.AutomationControls;

namespace AutomationLib.AutomationControls.Controls
{
    public class Menu : ToolStripBase
    {
        public Menu() : base(IntPtr.Zero) { }

        List<MenuItem> _MenuItemList;

        public List<MenuItem> MenuItemList
        {
            get { return _MenuItemList; }
            set { _MenuItemList = value; }
        }


    }
}
