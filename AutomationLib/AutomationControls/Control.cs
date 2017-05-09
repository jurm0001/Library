using System;
using System.Collections.Generic;
using System.Text;

using WindowsApis;
using WindowsApis.Data;
using WindowsApis;



namespace AutomationLib.AutomationControls
{
    public  class Control 
    {
        private List<Control> _controlList;
        private IntPtr _parentHandle;        
        private IntPtr _handle;
        private string _Text;
        private string _className;
        Type _controlType;

       

        public Control() { }

        public Control(IntPtr handle)
        {
            this._handle = handle;
            this._controlList = new List<Control>();
        }

        public Control(IntPtr handle, string text)
        {
            this._handle = handle;
            this._Text = text;
            this._controlList = new List<Control>();
        }
        public Control(IntPtr handle, string text, string classname)
        {
            this._handle = handle;
            this._Text = text;
            this._className = classname; this._controlList = new List<Control>();
        }
        public Control(IntPtr handle, string text, string classname, IntPtr parentHandle)
        {
            this._handle = handle;
            this._Text = text;
            this._className = classname;
            this._parentHandle = parentHandle;
            this._controlList = new List<Control>();
        }
        public Control(IntPtr handle, string text, string classname, IntPtr parentHandle, Type ControlType)
        {
            this._handle = handle;
            this._Text = text;
            this._className = classname;
            this._parentHandle = parentHandle;
            _controlType = ControlType;
            this._controlList = new List<Control>();
        }
        // click object
        // get text
        public Type ControlType
        {
            get { return _controlType; }
            set { _controlType = value; }
        }
        public IntPtr ParentHandle
        {
            get { return _parentHandle; }
            set { _parentHandle = value; }
        }
        public IntPtr Handle
        {
            get
            {
                return this._handle;
            }
        }

        public string Text
        {
            get
            {
                return this._Text;
            }
            set { this._Text = value; }
        }

        public string ClassName
        {
            get
            {
                return this._className;
            }
            set { this._className = value; }
        }
        public List<Control> ControlList
        {
            get { return _controlList; }
            set { _controlList = value; }
        }

    }
}
