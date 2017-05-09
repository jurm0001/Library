/*
 * 
 * Usage: SoaapObjects.SendMail mail = new SoaapObjects.SendMail("imscorp.bcbsfl.com", 25);
 *        mail.AddToEmailAddress(toEmail);
 *        mail.AddCCEmailAddress(ccEmail);
 *        mail.AddBCCEmailAddress(ccEmail);
 *        
 *        mail.AddAttachment(filename);
 *              OR
 *        mail.AddAttachment(new MemoryStream(stream of file));
 *        
 *        // default is current user credentials
 *        mail.Credentials = new System.Net.NetworkCredential("username", "pwd");
 * 
 *        mail.SendMessage("** Test Subject Email for Soaap (Disregard)", "Empty Test Body", fromEmail); 
 *        
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Utilities.Net
{   
    public class SendMail
    {
        public SendMail(string host, int port)
        {
            this._Host = host;
            this._Port = port;

            this._ToEmailAddressList = new AddressList();
            this._CCEmailAddressList = new AddressList();
            this._BCCEmailAddressList = new AddressList();
            this._AttachmentList = new AttachmentList();

            this._Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        }

        public void AddToEmailAddress(string emailAddress){this._ToEmailAddressList.Add(emailAddress);}

        public void AddCCEmailAddress(string emailAddress) { this._CCEmailAddressList.Add(emailAddress); }
        public void AddBCCEmailAddress(string emailAddress) { this._BCCEmailAddressList.Add(emailAddress); }

        public void AddAttachment(Attachment a) { this._AttachmentList.Add(a); }
        public void AddAttachment(string filename) { this._AttachmentList.Add(filename); }
        public void AddAttachment(System.IO.MemoryStream ms,string fileName, string fileType) 
        { 
            this._AttachmentList.Add(ms,fileName, fileType); 
        }
        
        
        
        public int SendMessage(string subject, string body, string fromEmail)
        {
            string To = "";
            string cc = "";
            string bcc = "";

            this._Message = new System.Net.Mail.MailMessage();
            //this._Message.cc

            // set from
            this._Message.From = new MailAddress(fromEmail);

            // set all to
            foreach (object o in this._ToEmailAddressList)
                this._Message.To.Add(o.ToString());
            foreach (object o in this._CCEmailAddressList)
                this._Message.CC.Add(o.ToString());
            foreach (object o in this._BCCEmailAddressList)
                this._Message.Bcc.Add(o.ToString());

            // set subject
            this._Message.Subject = subject;

            // set body 
            this._Message.Body = body;

            if (this._AttachmentList.AttachmentCount > 0)
            {
                foreach (Attachment a in this._AttachmentList)
                    this._Message.Attachments.Add(a);
            }
            
            this._MailClient = new System.Net.Mail.SmtpClient(this._Host, this._Port);
            
            this._MailClient.Credentials = this._Credentials;
            
            this._MailClient.Send(this._Message);
            return 1;
        }

        private string _Host;
        private int _Port;

        private System.Net.Mail.MailMessage _Message; 
        private System.Net.Mail.SmtpClient _MailClient;

        private System.Net.NetworkCredential _Credentials;
        public System.Net.NetworkCredential Credentials
        {
            get { return _Credentials; }
            set { _Credentials = value; }
        }

        private AddressList _ToEmailAddressList;
        private AddressList _CCEmailAddressList;
        private AddressList _BCCEmailAddressList;

        private AttachmentList _AttachmentList;

        private class AddressList
        {
            ArrayList _AddressList;
            public AddressList() { this._AddressList = new ArrayList(); }
            public AddressList(ArrayList list) { this._AddressList = list; }
            public void Add(string emailAddress) { this._AddressList.Add(emailAddress); }
            public void Remove(string emailAddress) { this._AddressList.Remove(emailAddress); }
            public IEnumerator GetEnumerator() { return this._AddressList.GetEnumerator(); }

        }

        private class AttachmentList
        {
            List<System.Net.Mail.Attachment> _AttachmentList;
            public AttachmentList() { this._AttachmentList = new List<System.Net.Mail.Attachment>(); }
            public AttachmentList(List<System.Net.Mail.Attachment> list) { this._AttachmentList = list; }
            public void Add(Attachment a) { this._AttachmentList.Add(a); }
            public void Add(string filename) { this._AttachmentList.Add(new Attachment(filename)); }
            //public void Add(System.IO.Stream stream) { this._AttachmentList.Add(new Attachment(stream, String.Empty)); }   
            public void Add(System.IO.Stream stream, string fileName, string fileType) 
            { 
                this._AttachmentList.Add(new Attachment(stream, fileName,fileType)); 
            } 
            public IEnumerator GetEnumerator() { return this._AttachmentList.GetEnumerator(); }
            public int AttachmentCount
            {
                get { return this._AttachmentList.Count; }
            }
        }

    }
}
