using System;
using System.Collections.Generic;

using System.IO;

using System.Data;

using System.Xml;
using System.Xml.Schema;
using System.Text;

/*
 * The Layout file is designed a specific way
 * there is two sections SEGMENTMAPPING and SEGMENTLISTING
 * SEGMENTMAPPING section depicts each section along with the identifier
 * SEGMENTLISTING section has the break down for the start and end points for each peice
 */

namespace Utilities.Document
{
    /// <summary>
    /// <example>
    ///     NSFParser nsfp = new NSFParser();
    ///     nsfp.Parse(Filename, Path_to_nsf_layout_xmls);
    ///     nsfp.buildNSFXml();
    ///     nsfp.NSFXmlSaveAs(@"C:\Documents and Settings\b5c5\Desktop\A9002001_NSF.xml");
    /// </example>
    /// </summary>
    public class SingleRecordFlatFileParser
    {
        public string ErrorMessage = "";

        #region Constructor
        public SingleRecordFlatFileParser()
        {
        }
        #endregion        

        #region public Boolean Parse(string FlatFileFileName, string LayoutFileName)
        /// <summary>
        /// main function to parse the nsf file into a dataset
        /// where each table is another segment of the nsf file
        /// </summary>
        /// <param name="Filename">filename of the nsf file</param>
        /// <param name="layoutDir">directory where the layout xmls reside</param>
        /// <returns>true is successful, false otherwise</returns>
        public Boolean Parse(string FlatFileFileName, string LayoutFileName)
        {
            if (!File.Exists(LayoutFileName))
            {
                this.ErrorMessage = "XML Layout File Not Found";
                return false;
            }
            if (!File.Exists(FlatFileFileName))
            {
                this.ErrorMessage = "Input File Not Found";
                return false;
            }

            this._FFFilename = FlatFileFileName;
            if (!FlatFileLoadLayout(LayoutFileName))
            {
                return false;
            }

            // get segment listing from flat file layout file
            XmlNodeList segmentList = this._FFLayout.GetElementsByTagName("SEGMENTLISTING")[0].ChildNodes;

            try
            {
                this._streamReader = File.OpenText(FlatFileFileName);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }

            string oneline = "";
            while ((oneline = this._streamReader.ReadLine()) != null)
            {
                parseSegment(oneline);                
            }
            this._streamReader.Close();
            return true;
        }
        #endregion        

        #region public XmlNodeList GetNodeList(string tagName)
        public XmlNodeList GetNodeList(string tagName)
        {
            return this._FFXMLDoc.GetElementsByTagName(tagName);
        }
        #endregion public XmlNodeList GetNodeList(string tagName)

        #region public Boolean buildXml()
        public Boolean buildXml()
        {
            this._FFXMLDoc = new XmlDocument();
            XmlDeclaration dec = this._FFXMLDoc.CreateXmlDeclaration("1.0", null, null);
            this._FFXMLDoc.AppendChild(dec);// Create the root element
            this._root = this._FFXMLDoc.CreateElement("FLATFILE");
            this._FFXMLDoc.AppendChild(this._root);

            string filename = "";
            // get file name
            if (this._FFFilename.Contains("\\"))
            {
                FileInfo fileI = new FileInfo(this._FFFilename);
                filename = fileI.Name;
            }
            if (this._FFFilename.Contains("."))
            {
                filename = filename.Substring(0, filename.IndexOf('.'));
            }

            for (int i = 0; i < this._ffDataset.Tables.Count; ++i)
            {   
                this._root.AppendChild(buildXmlFromTable(this._ffDataset.Tables[i], null));               
            }
            return true;
        }
        #endregion public Boolean buildXml()

        #region public Boolean SaveAs(string filename)
        /// <summary>
        /// saves the created xml file as an xml file to a location
        /// **** must have called "buildNSFXml before calling this function
        /// </summary>
        /// <param name="filename">filename of the xml to be saved</param>
        /// <returns>true if successful, false otherwise</returns>
        public Boolean SaveAs(string filename)
        {
            try
            {
                this._FFXMLDoc.Save(filename);
            }
            catch (Exception ex)
            {
                this._errString = ex.ToString();
                this._isErr = true;
                return false;
            }
            return true;
        }
        #endregion



        #region Private Methods
        #region private XmlElement buildXmlFromTable(DataTable dt, XmlElement root)
        /// <summary>
        /// builds an xml segment from the datatable segment
        /// </summary>
        /// <param name="dt">table to get converted to an xmlelement</param>
        /// <param name="root">xmlelement to attach the newly created element to</param>
        /// <returns>returns the newly created element</returns>
        private XmlElement buildXmlFromTable(DataTable dt, XmlElement root)
        {
            string tag;
            
            int tag1 = dt.TableName.IndexOf('_', dt.TableName.IndexOf('_') + 1);
            tag = dt.TableName.Substring(0, dt.TableName.IndexOf('_', dt.TableName.IndexOf('_')));
            
            XmlNodeList nodelist = this._FFLayout.GetElementsByTagName(tag.ToUpper());

            string ap = "";
            try
            {
                ap = nodelist[1].Attributes["name"].Value.ToString();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); ap = tag; }

            XmlElement xmlElem = this._FFXMLDoc.CreateElement(createXMLTag(ap));

            for (int i = 0; i < dt.Rows.Count; ++i)
            {

                XmlElement loop_claim = this._FFXMLDoc.CreateElement(createXMLTag(dt.Rows[i]["segment Name"].ToString()));
                loop_claim.InnerText = dt.Rows[i]["segment Value"].ToString();
                xmlElem.AppendChild(loop_claim);
            }
            return xmlElem;
        }
        #endregion

        #region private string createXMLTag(string potentialTag)
        /// <summary>
        /// creats a syntactically corrert xml tag (generic)
        /// </summary>
        /// <param name="potentialTag"></param>
        /// <returns></returns>
        private string createXMLTag(string potentialTag)
        {
            string temp = "";
            char[] x = potentialTag.ToCharArray();
            for (int i = 0; i < x.Length; ++i)
            {
                if (x[i] == ' ')
                {
                    x[i] = '_';
                }
                temp += x[i];
            }
            return temp;
        }
        #endregion        

        #region private Boolean parseSegment(string oneline)
        /// <summary>
        /// parses a complete segment into a datatables and adds it to the dataset
        /// </summary>
        /// <param name="oneline">current line of the nsf file</param>
        /// <returns>true if successful, false otherwise</returns>
        private Boolean parseSegment(string oneline)
        {
            // loop through segment mapping finding a value match to oneline
            XmlNodeList segMap = this._FFLayout.GetElementsByTagName("SEGMENTMAPPING");
            // once found - get tag name and that euqla segment header

            string segmentHeader = "";
            foreach (XmlNode n in segMap[0])
            {
                string value = n.InnerText;
                if (oneline.Substring(0, value.Length).Equals(value))
                {
                    segmentHeader = n.Name;
                    break;
                }
            }

            XmlNodeList nodelist = this._FFLayout.GetElementsByTagName(segmentHeader.ToUpper());
            XmlNode segNode = nodelist[1];

            DataTable segmentTable = new DataTable(segmentHeader + "_" + this._ffDataset.Tables.Count.ToString());
            segmentTable.Columns.Add("segment Name");
            segmentTable.Columns.Add("segment Value");

            foreach (XmlNode segPeice in segNode)
            {

                string start = "";
                string end = "";
                string req = "";
                string name = "";
                string cobol = "";
                //string map = "";

                int startIndex = 0;
                int endIndex = 0;
                string peice = "";

                XmlNode temp = segPeice;

                if (temp.ChildNodes[0].FirstChild != null)
                    start = temp.ChildNodes[0].FirstChild.Value;
                if (temp.ChildNodes[1].FirstChild != null)
                    end = temp.ChildNodes[1].FirstChild.Value;
                if (temp.ChildNodes[2].FirstChild != null)
                    req = temp.ChildNodes[2].FirstChild.Value;
                if (temp.ChildNodes[3].FirstChild != null)
                    name = temp.ChildNodes[3].FirstChild.Value;
                if (temp.ChildNodes[4].FirstChild != null)
                    cobol = temp.ChildNodes[4].FirstChild.Value;

                startIndex = Int32.Parse(start) - 1;
                endIndex = Int32.Parse(end) - 1;
                try
                {
                    peice = oneline.Substring(startIndex, (endIndex - startIndex) + 1);

                    segmentTable.Rows.Add(name, peice);
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); continue; }
            }
            this._ffDataset.Tables.Add(segmentTable);
            return true;
        }
        #endregion

        #region private Boolean FlatFileLoadLayout(string filename)
        /// <summary>
        /// loads the corresponding layout file (ub or hcfa)
        /// </summary>
        /// <param name="filename">filename either ub or hcfa</param>
        /// <param name="layoutDir">directory where these file are located</param>
        /// <returns>true if successful, false otherwise</returns>
        private Boolean FlatFileLoadLayout(string filename)
        {
            this._FFLayout = new XmlDocument();
            try
            {
                //this._FFLayout.Load(filename);
                this._FFLayout.Load(filename);
            }
            catch (Exception ex)
            {
                this._errString = ex.ToString();
                this._isErr = true;
                return false;
            }

            this._FFXMLDoc = new XmlDocument();

            this._ffDataset = new DataSet("FF");

            return true;
        }
        #endregion
        #endregion Private Methods

        #region Instance Variables
        /// <summary>
        /// contains the xml layout for the NSF file
        /// </summary>
        private XmlDocument _FFLayout;
        


        /// <summary>
        /// contains the xml version of the nsf files
        /// </summary>
        private XmlDocument _FFXMLDoc;
        public XmlDocument FFXMLDoc
        {
            get
            {
                return this._FFXMLDoc;
            }
        }

        /// <summary>
        /// used to read each line of the nsf file
        /// </summary>
        private StreamReader _streamReader;

        /// <summary>
        /// holds the tables of each segment of the nsf file
        /// </summary>
        private DataSet _ffDataset;
        public DataSet ffDataset
        {
            get
            {
                return this._ffDataset;
            }
        }               

        private string _errString;
        public string errString
        {
            get
            {
                return this._errString;
            }
        }

        private Boolean _isErr;
        public Boolean isErr
        {
            get
            {
                return this._isErr;
            }
        }

        private XmlElement _root;
        private string _FFFilename;        

        #endregion
    }
}
