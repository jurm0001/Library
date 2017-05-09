using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Outlook = Microsoft.Office.Interop.Outlook;
using System.Diagnostics;

using System.Runtime.InteropServices;

using System.Reflection;

namespace Utilities.Net
{
    public class Mail
    {
        private Outlook._Application olApp;
        private Outlook._NameSpace olNS;
        private Outlook.MailItem MailMessage;
        private Boolean isRunning;

        private string _subject;
        public string Subject 
        {
            get { return this._subject; }
            set { this._subject = value; } 
        }

        private string _body;
        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }

        private List<string> _recipients;
        public List<string> Recipients
        {
            get { return this._recipients; }
            set { this._recipients = value; }
        }

        private List<string> _attachmates;
        public List<string> Attachments
        {
            get { return this._attachmates; }
            set { this._attachmates = value; }
        }


        public enum MessageBodyTypeEnum
        {
            TEXT,
            HTML
        }

        private MessageBodyTypeEnum _MessageBodyType;
        public MessageBodyTypeEnum MessageBodyType
        {
            get { return this._MessageBodyType; }
            set { this._MessageBodyType = value; }
        }


        public Mail()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Equals("OUTLOOK"))
                    isRunning = true;
            }

            olApp = new Outlook.Application();
            olNS = olApp.GetNamespace("MAPI");
            
            this._MessageBodyType = MessageBodyTypeEnum.TEXT;

        }

        ~Mail()
        {
            CleanUp();
        }

        #region public static void Send()
        public Boolean Send()
        {

            

            if(!this.isRunning)
                olNS.Logon(Missing.Value, Missing.Value, true, true);

            MailMessage = (Outlook.MailItem)olApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            

            // add recipients
            try
            {
                foreach(string r in this.Recipients)
                    MailMessage.Recipients.Add(r);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
            MailMessage.Recipients.ResolveAll();

            // add subject
            MailMessage.Subject = this.Subject;

            // add body
            if (this._MessageBodyType == MessageBodyTypeEnum.TEXT)
                MailMessage.Body = this.Body;
            else
                MailMessage.HTMLBody = this.Body;

            // add attachments
            if (this.Attachments != null)
            {
                String sDisplayName = "";
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                foreach (string a in this.Attachments)
                {
                    if (System.IO.File.Exists(a))
                    {
                        int iPosition = (int)this.MailMessage.Body.Length + 1;
                        MailMessage.Attachments.Add(a, iPosition, iAttachType, sDisplayName);
                    }
                }
            }

            // send it on
            MailMessage.Send();

            olNS.Logoff();

            // clean up com object
            CleanUp();

            return true;

        }
        #endregion public static void Send()

        public void Forward()
        {            
        }

        private void CleanUp()
        {
            try
            {
                if (!isRunning)
                {
                    foreach (Process p in Process.GetProcesses())
                    {
                        if (p.ProcessName.Equals("OUTLOOK"))
                            p.Kill();
                    }
                    olApp.Quit();
                }
                Marshal.ReleaseComObject(olApp);
                System.GC.Collect();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
        }

    }
}
