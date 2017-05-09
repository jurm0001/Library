using System;
using System.Collections.Generic;

using System.Text;



using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;

namespace Utilities.Document
{
    public class XMLValidator
    {
        // variable declarations 
        private string _XMLFileName;
        private string _SchemaFileName;
        private XmlDocument _doc;

        private bool _bValid;
        public bool Valid
        {
            get
            {
                return this._bValid;
            }
            set
            {
                this._bValid = value;
            }
        }
        private string _errString;
        public string errString
        {
            get
            {
                return this._errString;
            }
            set
            {
                this._errString = value;
            }
        }

        private List<string> _errorList;
        public List<string> ErrorList
        {
            get { return _errorList; }
            set { _errorList = value; }
        }

        public XMLValidator(string sXMLFileName, string sSchemaFileName)
        {
            this._XMLFileName = sXMLFileName;
            this._SchemaFileName = sSchemaFileName;
            this._bValid = true;
            this._errString = "";
            this._errorList = new List<string>();
        }

        public XMLValidator(XmlDocument doc, string sSchemaFileName)
        {
            this._doc = doc;
            this._SchemaFileName = sSchemaFileName;
            this._bValid = true;
            this._errString = "";
            this._errorList = new List<string>();
        }

        public void ValidationHandler(object sender, ValidationEventArgs args)
        {
            this._bValid = false;
            this._errString = args.Message;
            this._errorList = new List<string>();
        }

        public Boolean ValidateDoc()
        {
            XmlTextReader xReader;
            /*************** Load Schema File *************************/
            XmlSchemaCollection m_schSchemas = new XmlSchemaCollection();
            xReader = new XmlTextReader(_SchemaFileName);
            XmlSchema xmlSchema = XmlSchema.Read(xReader, new ValidationEventHandler(SchemaReadError));
            try
            {
                m_schSchemas.Add(xmlSchema);
                xReader.Close();
            }
            catch (Exception ex1)
            {
                this._bValid = false;
                this._errString = ex1.ToString();
                return this._bValid;
            }
            /*************** Load Schema File *************************/

            /*************** Load XML File and set validation Method **/
            XmlValidatingReader xValidator = new XmlValidatingReader(this._doc.InnerXml, XmlNodeType.Document, null);
            try
            {
                xValidator.Schemas.Add(m_schSchemas);
                xValidator.ValidationType = ValidationType.Auto; //set the ValidationType               
                xValidator.ValidationEventHandler += new ValidationEventHandler(ValidationError);
            }
            catch (Exception ex2)
            {
                this._bValid = false;
                this._errString = ex2.ToString();
                return this._bValid;
            }
            /*************** Load XML File and set validation Method **/

            /************ Validate Document Node By Node **************/
            while (xValidator.Read()) ; //empty body              
            _bValid = true; //reset variable            
            xValidator.Close();
            /************ Validate Document Node By Node **************/

            return _bValid;
        }

        public Boolean Validate()
        {
            XmlTextReader xReader;

            /*************** Load Schema File *************************/
            XmlSchemaCollection m_schSchemas = new XmlSchemaCollection();
            xReader = new XmlTextReader(_SchemaFileName);
            XmlSchema xmlSchema = XmlSchema.Read(xReader, new ValidationEventHandler(SchemaReadError));
            try
            {
                m_schSchemas.Add(xmlSchema);
                xReader.Close();
            }
            catch (Exception ex1)
            {
                this._bValid = false;
                this._errString = ex1.ToString();
                return this._bValid;
            }
            /*************** Load Schema File *************************/

            /*************** Load XML File and set validation Method **/
            xReader = new XmlTextReader(_XMLFileName);

            XmlValidatingReader xValidator = new XmlValidatingReader(xReader);
            try
            {
                xValidator.Schemas.Add(m_schSchemas);
                xValidator.ValidationType = ValidationType.Auto; //set the ValidationType               
                xValidator.ValidationEventHandler += new ValidationEventHandler(ValidationError);
            }
            catch (Exception ex2)
            {
                this._bValid = false;
                this._errString = ex2.ToString();
                return this._bValid;
            }
            /*************** Load XML File and set validation Method **/

            /************ Validate Document Node By Node **************/
            while (xValidator.Read()) ; //empty body              
            //_bValid = true; //reset variable            
            xValidator.Close();
            /************ Validate Document Node By Node **************/

            return _bValid;
        }

        private void ValidationError(object sender, ValidationEventArgs arguments)
        {
            this._errString = arguments.Message;
            this._errorList.Add("Line: " + arguments.Exception.LineNumber + " Position: " + arguments.Exception.LinePosition + " " + arguments.Message);
            _bValid = false; //validation failed5
        }

        private void SchemaReadError(object sender, ValidationEventArgs arguments)
        {
            this._errString = arguments.Message;
            _bValid = false; //validation failed
        }
    }
}
