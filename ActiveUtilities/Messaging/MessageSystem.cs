using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Threading;

namespace Utilities.UI
{
    #region class MessageSystem
    /// <summary>
    /// class to hold all messages for a system.
    /// if assign to a datagridview it will auto 
    /// update the rows during processing.
    /// </summary>
    public class MessageSystem
    {
        #region Constructor
        /// <summary>
        /// simply creates the Datatable to hold all messages
        /// </summary>
        /// <param name="sleeptime">time to sleep after generating a new message</param>
        /// <param name="mode">set mode DEV, STAGE, PROD</param>
        public MessageSystem(int sleeptime, string mode)
        {
            this._mode = mode;

            this._msgTable = new DataTable("MessageTable");
            this._msgTable.Columns.Add("MessageType");
            this._msgTable.Columns.Add("Message 1");
            this._msgTable.Columns.Add("Message 2");
            this._msgTable.Columns.Add("Message 3");
            this._msgTable.Columns.Add("Message 4");

            this._logFileName = "";
            this._writeFile = false;

            this.GroupBoxCounts = null;
            this.writeTextbox = false;
            this._sleepTime = sleeptime;

        }       

        /// <summary>
        ///  creates the Datatable to hold all messages and 
        /// create the object to write to the sopecified filename
        /// </summary>
        /// <param name="filename">name of the file to write the message to</param>
        /// <param name="sleeptime">time to sleep after generating a new message</param>
        /// <param name="mode">set mode DEV, STAGE, PROD</param>
        public MessageSystem(string filename, int sleeptime, string mode)
        {
            this._mode = mode;

            this._msgTable = new DataTable("MessageTable");
            this._msgTable.Columns.Add("MessageType");
            this._msgTable.Columns.Add("Message 1");
            this._msgTable.Columns.Add("Message 2");
            this._msgTable.Columns.Add("Message 3");
            this._msgTable.Columns.Add("Message 4");

            this._logFileName = filename;

            try
            {
                //this._msgFileWriter = new StreamWriter(this._logFileName);
                this._writeFile = true;
            }
            catch (Exception ex)
            {
                this._isErr = true;
                this._errString = ex.ToString();
                this._writeFile = true;
            }

            this.GroupBoxCounts = null;
            this.writeTextbox = false;
            this._sleepTime = sleeptime;
        }

        /// <summary>
        /// creates a file object to write to, adds a ref to the groupbox counts object for updates, and datatable to store messages
        /// </summary>
        /// <param name="filename">name of file to write messages to</param>
        /// <param name="groupBox">groupboxcounts object from the windows form</param>
        /// <param name="sleeptime">time to sleep after message</param>
        /// <param name="mode">environment mode</param>
        public MessageSystem(string filename, ref AutomationCountsGroupbox groupBox, int sleeptime, string mode)
        {
            this._mode = mode;

            this._msgTable = new DataTable("MessageTable");
            this._msgTable.Columns.Add("MessageType");
            this._msgTable.Columns.Add("Message 1");
            this._msgTable.Columns.Add("Message 2");
            this._msgTable.Columns.Add("Message 3");
            this._msgTable.Columns.Add("Message 4");

            this._logFileName = filename;

            try
            {
                //this._msgFileWriter = new StreamWriter(this._logFileName);
                this._writeFile = true;
            }
            catch (Exception ex)
            {
                this._isErr = true;
                this._errString = ex.ToString();
                this._writeFile = true;
            }

            this.GroupBoxCounts = groupBox;            
            this.writeTextbox = true;
            this._sleepTime = sleeptime;
        }

        /// <summary>
        /// creates and datatable for messages storing, groupboxcount from form
        /// </summary>
        /// <param name="groupBox">groupboxcounts object from the windows form</param>
        /// <param name="sleeptime">time to sleep after message</param>
        /// <param name="mode">environment mode</param>
        public MessageSystem(AutomationCountsGroupbox groupBox, int sleeptime, string mode)
        {
            this._mode = mode;

            this._msgTable = new DataTable("MessageTable");
            this._msgTable.Columns.Add("MessageType");
            this._msgTable.Columns.Add("Message 1");
            this._msgTable.Columns.Add("Message 2");
            this._msgTable.Columns.Add("Message 3");
            this._msgTable.Columns.Add("Message 4");

            this._logFileName = "";
            this._writeFile = false;            

            this.GroupBoxCounts = groupBox;
            this._sleepTime = sleeptime;
        }

        public MessageSystem(EnvironmentModeEnum en)
        {
            this.EnvironmentMode = en;

            this._msgTable = new DataTable("MessageTable");
            this._msgTable.Columns.Add("MessageType");
            this._msgTable.Columns.Add("Message 1");
            this._msgTable.Columns.Add("Message 2");
            this._msgTable.Columns.Add("Message 3");
            this._msgTable.Columns.Add("Message 4");

            this._logFileName = "";
            this._writeFile = false;

            this.GroupBoxCounts = null;
            this._sleepTime = 3;
        }
        #endregion

        #region Destructor
        ~MessageSystem()
        {
            try
            {
                this._msgFileWriter.Close();
            }
            catch (Exception ex)
            {
                this._errString = ex.ToString();
            }
        }
        #endregion        

        #region messsage no messagetype
        public virtual void WriteMsg(string msg1)
        {
            this._msgTable.Rows.Add(MessageType.INFO.ToString(),
                                   msg1, "", "", "");
            if (this._writeFile)
            {
                
                string fileMessage = MessageType.INFO.ToString() + "\t" +
                                     msg1 + "\t";
                this.WriteToFile(fileMessage);                
            }
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        public virtual void WriteMsg(string msg1, string msg2)
        {
            this._msgTable.Rows.Add(MessageType.INFO.ToString(),
                                   msg1, msg2, "", "");
            if (this._writeFile)
            {                
                string fileMessage = MessageType.INFO.ToString() + "\t" +
                                     msg1 + "\t" +
                                     msg2 + "\t";
                this.WriteToFile(fileMessage);                
            }
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        public virtual void WriteMsg(string msg1, string msg2, string msg3)
        {
            this._msgTable.Rows.Add(MessageType.INFO.ToString(),
                                   msg1, msg2, msg3, "");
            if (this._writeFile)
            {                
                string fileMessage = MessageType.INFO.ToString() + "\t" +
                                     msg1 + "\t" +
                                     msg2 + "\t" +
                                     msg3 + "\t";
                this.WriteToFile(fileMessage);  
            }
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        public virtual void WriteMsg(string msg1, string msg2, string msg3, string msg4)
        {
            this._msgTable.Rows.Add(MessageType.INFO.ToString(),
                                   msg1, msg2, msg3, msg4);
            if (this._writeFile)
            {                
                string fileMessage = MessageType.INFO.ToString() + "\t" +
                                     msg1 + "\t" +
                                     msg2 + "\t" +
                                     msg3 + "\t" +
                                     msg4 + "\t";
                this.WriteToFile(fileMessage);  
            }
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        #endregion

        #region message with messagetype
        public virtual void WriteMsg(MessageType msgType, string msg1)
        {
            this._msgTable.Rows.Add(msgType.ToString(),
                                   msg1, "", "", "");
            if (this._writeFile)
            {
                string fileMessage = msgType.ToString() + "\t" +
                                     msg1 + "\t";
                this.WriteToFile(fileMessage);  
            }

            if (this.writeTextbox)
                this.WriteToTextBox(msgType);

            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        public virtual void WriteMsg(MessageType msgType, string msg1, string msg2)
        {
            this._msgTable.Rows.Add(msgType.ToString(),
                                   msg1, msg2, "", "");
            if (this._writeFile)
            {
                string fileMessage = msgType.ToString() + "\t" +
                                     msg1 + "\t" +
                                     msg2 + "\t";
                this.WriteToFile(fileMessage);  
            }

            if (this.writeTextbox)
                this.WriteToTextBox(msgType);

            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        public virtual void WriteMsg(MessageType msgType, string msg1, string msg2, string msg3)
        {
            this._msgTable.Rows.Add(msgType.ToString(),
                                   msg1, msg2, msg3, "");
            if (this._writeFile)
            {
                string fileMessage = msgType.ToString() + "\t" +
                                     msg1 + "\t" +
                                     msg2 + "\t" +
                                     msg3 + "\t";
                this.WriteToFile(fileMessage);  
            }

            if (this.writeTextbox)
                this.WriteToTextBox(msgType);

            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        public virtual void WriteMsg(MessageType msgType, string msg1, string msg2, string msg3, string msg4)
        {
            this._msgTable.Rows.Add(msgType.ToString(),
                                   msg1, msg2, msg3, msg4);
            if (this._writeFile)
            {
                string fileMessage = msgType.ToString() + "\t" +
                                     msg1 + "\t" +
                                     msg2 + "\t" +
                                     msg3 + "\t" +
                                     msg4 + "\t";
                this.WriteToFile(fileMessage);  
            }
            if (this.writeTextbox)
                this.WriteToTextBox(msgType);

            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(this._sleepTime);
        }
        #endregion

        #region public void WriteResults()
        public void WriteResults()
        {
            this.WriteMsg("Total:", this.GroupBoxCounts.textBox_Total.Text);
            this.WriteMsg("Reviewed:", this.GroupBoxCounts.textBox_reviewed.Text);
            this.WriteMsg("Processed:", this.GroupBoxCounts.textBox_processed.Text);
            this.WriteMsg("Skipped:", this.GroupBoxCounts.textBox_skipped.Text);
            this.WriteMsg("Failed:", this.GroupBoxCounts.textBox_failed.Text);
            this.WriteMsg("Fatal:", this.GroupBoxCounts.textBox_fatal.Text);
        }
        #endregion public void WriteResults()

        #region private void WriteToFile(string msg)
        private void WriteToFile(string msg)
        {
            try
            {
                
                FileStream file = new FileStream(this._logFileName, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file, Encoding.ASCII);                
                try
                {
                    msg += "\n";
                    while (msg.Length > 1)
                    {
                        string temp = "";
                        int index = msg.IndexOf('\n');
                        temp = msg.Substring(0, index);
                        sw.WriteLine(temp);
                        msg = "\t" + msg.Substring(index + 1, msg.Length - (index + 1));
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.ToString()); }

                try
                {
                    sw.Close();
                }
                catch (Exception ex3) { Console.WriteLine(ex3.ToString()); }
            }
            catch (Exception ex)
            {
                this._isErr = true;
                this._errString = ex.ToString();
            }
        }
        #endregion private void WriteToFile(string msg)

        #region private void WriteToTextBox(MessageType type)
        private void WriteToTextBox(MessageType type)
        {
            switch (type)
            {
                case MessageType.FAILED:
                    try
                    {
                        GroupBoxCounts.textBox_failed.Text = (long.Parse(GroupBoxCounts.textBox_failed.Text) + 1).ToString();
                    }
                    catch (Exception ex1) { Console.WriteLine(ex1.ToString()); }
                    break;
                case MessageType.PROCESSED:
                case MessageType.PROCESSED2:
                    try
                    {
                        GroupBoxCounts.textBox_processed.Text = (long.Parse(GroupBoxCounts.textBox_processed.Text) + 1).ToString();
                    }
                    catch (Exception ex2) { Console.WriteLine(ex2.ToString()); }
                    break;
                case MessageType.SKIPPED:
                    try
                    {
                        GroupBoxCounts.textBox_skipped.Text = (long.Parse(GroupBoxCounts.textBox_skipped.Text) + 1).ToString();
                    }
                    catch (Exception ex3) { Console.WriteLine(ex3.ToString()); }
                    break;
                case MessageType.FATAL:
                    try
                    {
                        GroupBoxCounts.textBox_fatal.Text = (long.Parse(GroupBoxCounts.textBox_fatal.Text) + 1).ToString();
                    }
                    catch (Exception ex4) { Console.WriteLine(ex4.ToString()); }
                    break;
            }
        }
        #endregion private void WriteToTextBox(MessageType type)

        #region class variables

        public enum EnvironmentModeEnum
        {
            DEV,
            NAV,
            STAGE,
            PROD
        }

        public EnvironmentModeEnum EnvironmentMode;

        //private Message _msg;
        protected string _logFileName;

        protected TextWriter _msgFileWriter = null;
        protected Boolean _writeFile;

        protected DataTable _msgTable;
        public DataTable msgTable
        {
            get
            {
                return this._msgTable;
            }
        }

        protected Boolean _isErr;
        public Boolean isErr
        {
            get
            {
                return this._isErr;
            }
        }
        protected string _errString;
        public string errString
        {
            get
            {
                return this._errString;
            }
        }

        public AutomationCountsGroupbox GroupBoxCounts;

        protected Boolean writeTextbox;

        protected int _sleepTime;

        protected string _mode;

        #region enum MessageType
        public enum MessageType
        {
            INFO = 1,
            DEBUG,
            PROCESSED,
            FATAL,
            FAILED,
            SKIPPED,
            MAIL,
            REPORT,
            BEGIN,
            ENDOK,
            FILEOPENCOUNT,
            READNEXTRECORD,
            OUTPUT,
            NEWICN,
            SPLITICN,
            ICNFAILED,
            ICNSPLITFAILED,
            SETLOCATION,
            FILEMOVEEFAILED,
            FILEMOVESUCCESS,
            FILECOPYFAILED,
            FILECOPYSUCCESS,
            FILEDELETEEFAILED,
            FILEDELETESUCCESS,
            ENDERROR,
            ENDUSER,
            PROCESSED2
        }
        #endregion

        #endregion

    }// end messageSystem class
    #endregion
}
