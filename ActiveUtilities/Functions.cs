using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;

using System.Drawing;
using System.Drawing.Imaging;

using System.Windows.Forms;
using System.IO;


using System.Configuration;

//using Microsoft.Office.Core;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

using System.Runtime.Serialization.Formatters.Binary;

using System.Runtime.InteropServices;

namespace Utilities
{
    public class Functions
    {
        #region public static void ReleaseComObject(object obj)
        public static void ReleaseComObject(object obj)
        {            
            Marshal.ReleaseComObject(obj);
            GC.Collect();
        }
        #endregion public static void ReleaseComObject(object obj)

        #region public static string RemoveSpecialChar(string SpecialString, char[] SpecialChar)
        public static string RemoveSpecialChar(string SpecialString, char[] SpecialChar)
        {
            int indexof = 0;

            for (int x = 0; x <= SpecialChar.GetUpperBound(0); x++)
            {
                if (SpecialString.Contains(Convert.ToString(SpecialChar[x].ToString())))
                {
                    do
                    {
                        indexof = SpecialString.IndexOf(SpecialChar[x].ToString());

                        if (indexof >= 0)
                        {
                            SpecialString = SpecialString.Remove(indexof, 1);
                        }
                    } while (indexof >= 0);
                }
            }
            return SpecialString;
        }
        #endregion public static string RemoveSpecialChar(string SpecialString, char[] SpecialChar)

        #region private void ProtectSection(String sSectionName)
        public static void ProtectSection(String sSectionName)
        {
            // Open the app.config file.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // Get the section in the file.
            ConfigurationSection section = config.GetSection(sSectionName);
            // If the section exists and the section is not readonly, then protect the section.
            if (section != null)
            {
                if (!section.IsReadOnly())
                {                    
                    // Protect the section.
                    section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    section.SectionInformation.ForceSave = true;
                    // Save the change.
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
        }
        #endregion private void ProtectSection(String sSectionName)


        #region private XmlElement createElement(string tag, string value)
        private XmlElement createElement(string tag, string value)
        {
            //XmlElement temp = XmlDocument.CreateElement(tag);
            //temp.InnerText = value;
            //return temp;
            return null;
        }
        #endregion private XmlElement createElement(string tag, string value)        

        #region string[] ConvertToStringArray(System.Array values)
        string[] ConvertToStringArray(System.Array values)
        {
            // create a new string array
            string[] theArray = new string[values.Length];
            // loop through the 2-D System.Array and populate the 1-D String Array
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }
            return theArray;
        }
        #endregion string[] ConvertToStringArray(System.Array values)

        public enum StringDateFormat
        {
            MMSDDSYY,
            MMSDDSYYYY,
            MMDDYY,
            MMDDYYYY,
            YYSMMSDD,
            YYYYSMMSDD,
            YYMMDD,
            YYYYMMDD
        }

        public enum StringTimeFormat
        {
            HHSSSSYY,
            MMSDDSYYYY,
            MMDDYY,
            MMDDYYYY,
            YYSMMSDD,
            YYYYSMMSDD,
            YYMMDD,
            YYYYMMDD
        }

        #region public static string getDesktopPath()
        public static string getDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\";
        }
        #endregion public static string getDesktopPath()

        #region public static DateTime FormatStringTimeToTime(string time, Boolean colon)
        public static DateTime FormatStringTimeToTime(string time, Boolean colon)
        {
            DateTime ti = new DateTime();
            if(colon)
            {

                ti = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(3, 2)), 0); 
               
            }
            else
            {
                ti = new DateTime(1900, 1, 1, int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(2, 2)), 0); 
            }
            return ti;
        }
        #endregion public static DateTime FormatStringTimeToTime(string time, Boolean colon)
        
        #region byte array Conversion Functions
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();            
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        #endregion byte array Conversion Functions


        #region public static DateTime FormatStringDateToDate(string date, Boolean slashes)
        public static DateTime FormatStringDateToDate(string date, Boolean slashes)
        {
            DateTime dt = new DateTime();
            if (slashes)
            {
                switch (date.Length)
                {
                    case 8:
                        try
                        {
                            dt = new DateTime(int.Parse("20" + date.Substring(6, 2)),
                                              int.Parse(date.Substring(0, 2)),
                                              int.Parse(date.Substring(2, 2)));
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(ex1.ToString());
                        }
                        break;
                    case 10:
                        try
                        {
                            dt = new DateTime(int.Parse(date.Substring(6, 4)),
                                              int.Parse(date.Substring(0, 2)),
                                              int.Parse(date.Substring(2, 2)));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                }
            }
            else
            {
                switch (date.Length)
                {
                    case 6:
                        try
                        {
                            dt = new DateTime(int.Parse("20" + date.Substring(4, 2)),
                                              int.Parse(date.Substring(0, 2)),
                                              int.Parse(date.Substring(2, 2)));
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(ex1.ToString());
                        }
                        break;
                    case 8:
                        try
                        {
                            dt = new DateTime(int.Parse(date.Substring(4, 4)),
                                              int.Parse(date.Substring(0, 2)),
                                              int.Parse(date.Substring(2, 2)));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        break;
                }
            }
            return dt;
        }
        #endregion public static DateTime FormatStringDateToDate(string date, Boolean slashes)        
        
        #region public static string dateToJulianDate(DateTime date)
        public static string dateToJulianDate(DateTime date)
        {
            string month = date.Month.ToString();
            string day = date.Day.ToString();
            string year = date.Year.ToString();

            int days = 0;

            for (int i = 1; i < date.Month; ++i)
            {
                days += DateTime.DaysInMonth(date.Year, i);
            }

            days += int.Parse(day);

            
            string dayString = "000" + days.ToString();
            string returnstring = year.Substring(3, 1) + dayString.Substring(dayString.Length - 3);
            return returnstring;
        }
        #endregion public static string dateToJulianDate(DateTime date)

        #region Tokenizer to List functions
        public static List<string> getStringList(string data, string delimiter)
        {
            string temp = "";
            List<string> templist = new List<string>();
            if (!data.Substring(data.Length - 1, 1).Equals(delimiter))
                data += delimiter;

            while (data.Length > 0)
            {
                int index = data.IndexOf(delimiter);
                temp = data.Substring(0, index);
                templist.Add(temp);
                data = data.Substring(index + 1, data.Length - (index + 1));
            }
            return templist;
        }

        public static List<int> getIntList(string data, string delimiter)
        {
            string temp = "";
            List<int> templist = new List<int>();
            if (!data.Substring(data.Length - 1, 1).Equals(delimiter))
                data += delimiter;

            while (data.Length > 0)
            {
                int index = data.IndexOf(delimiter);
                temp = data.Substring(0, index);
                templist.Add(int.Parse(temp));
                data = data.Substring(index + 1, data.Length - (index + 1));
            }
            return templist;
        }

        public static List<double> getDoubleList(string data, string delimiter)
        {
            string temp;
            List<double> templist = new List<double>();
            if (!data.Substring(data.Length - 1, 1).Equals(delimiter))
                data += delimiter;

            while (data.Length > 0)
            {
                int index = data.IndexOf(delimiter);
                temp = data.Substring(0, index);
                templist.Add(double.Parse(temp));
                data = data.Substring(index + 1, data.Length - (index + 1));
            }
            return templist;
        }
        #endregion Tokenizer to List functions

        #region Validation Functions
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");

            return !objNotNaturalPattern.IsMatch(strNumber) &&
                    objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive
        public static bool IsWholeNumber(string strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");

            return !objNotWholePattern.IsMatch(strNumber);
        }
        // Function to Test for Integers both Positive & Negative
        public static bool IsInteger(string strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");

            return !objNotIntPattern.IsMatch(strNumber) &&
                    objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real
        public static bool IsPositiveNumber(string strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");

            return !objNotPositivePattern.IsMatch(strNumber) &&
                   objPositivePattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets.
        public static bool IsAlpha(string strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");

            return !objAlphaPattern.IsMatch(strToCheck);
        }
        // Function to Check for AlphaNumeric.
        public static bool IsAlphaNumeric(string strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }
        #endregion Validation Functions

        

        #region public static string getEmptyString(int len)
        /// <summary>
        /// returns an empty string with len spaces.
        /// </summary>
        /// <param name="len">number of blank spaces</param>
        /// <returns></returns>
        public static string getEmptyString(int len)
        {
            string empty = "";
            return empty.PadLeft(len, ' ');
        }
        #endregion public static string getEmptyString(int len)

        

        #region public Boolean launchFunctions(Object obj, string function, object[] parms)
        /// <summary>
        /// uses reflection to launch a function that returns a boolean
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="function"></param>
        /// <param name="parms"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public static Boolean launchFunctions(Object obj, string function, object[] parms, ref string errMessage)
        {
            Type t = obj.GetType();
            MethodInfo MI = t.GetMethod(function);
            Boolean ValidateResult = false;
            try
            {
                ValidateResult = Convert.ToBoolean(MI.Invoke(obj, parms));
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
            }
            return ValidateResult;
        }
        #endregion public Boolean launchFunctions(Object obj, string function, object[] parms)

        #region public string launchFunctionsString(Object obj, string function, object[] parms)
        /// <summary>
        /// uses reflection to launch a functions that returns a string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="function"></param>
        /// <param name="parms"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public static string launchFunctionsString(Object obj, string function, object[] parms, ref string errMessage)
        {
            Type t = obj.GetType();
            MethodInfo MI = t.GetMethod(function);
            string ValidateResult = "";
            try
            {
                ValidateResult = Convert.ToString(MI.Invoke(obj, parms));
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
            }
            return ValidateResult;
        }
        #endregion public string launchFunctionsString(Object obj, string function, object[] parms)

        #region public static Object launchFunctionsObject(Object obj, string function, object[] parms, ref string errMessage)
        /// <summary>
        /// uses reflection to launch a function that erturns an object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="function"></param>
        /// <param name="parms"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public static Object launchFunctionsObject(Object obj, string function, object[] parms, ref string errMessage)
        {
            Type t = obj.GetType();
            MethodInfo MI = t.GetMethod(function);
            Object ValidateResult = null;
            try
            {
                ValidateResult = Convert.ToString(MI.Invoke(obj, parms));
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
            }
            return ValidateResult;
        }
        #endregion public static Object launchFunctionsObject(Object obj, string function, object[] parms, ref string errMessage)

        #region public static long LineCount(string source, bool isFileName)
        /// <summary>
        /// return the line count of a file
        /// </summary>
        /// <param name="source"></param>
        /// <param name="isFileName"></param>
        /// <returns></returns>
        public static long LineCount(string source, bool isFileName)
        {
            if (source != null)
            {
                string text = source;
                if (isFileName)
                {
                    FileStream FS = new FileStream(source, FileMode.Open,
                    FileAccess.Read, FileShare.Read);
                    StreamReader SR = new StreamReader(FS);
                    text = SR.ReadToEnd();
                    SR.Close();
                    FS.Close();
                }
                Regex RE = new Regex("\n", RegexOptions.Multiline);
                MatchCollection theMatches = RE.Matches(text);
                // Needed for files with zero length
                // Note that a string will always have a line terminator
                // and thus will always have a length of 1 or more
                if (isFileName)
                {
                    return (theMatches.Count);
                }
                else
                {
                    return (theMatches.Count) + 1;
                }
            }
            else
            {
                // Handle a null source here
                return (0);
            }
        }
        #endregion public static long LineCount(string source, bool isFileName)

        #region public DateTime GetLastDayOfMonth(DateTime dtDate)
        public static DateTime GetLastDayOfMonth(DateTime dtDate)
        {
            // set return value to the last day of the month 
            // for any date passed in to the method 
            // create a datetime variable set to the passed in date 
            DateTime dtTo = dtDate;
            // overshoot the date by a month 
            dtTo = dtTo.AddMonths(1);
            // remove all of the days in the next month 
            // to get bumped down to the last day of the
            // previous month 
            dtTo = dtTo.AddDays(-(dtTo.Day));
            // return the last day of the month 
            return dtTo;
        }
        #endregion public DateTime GetLastDayOfMonth(DateTime dtDate)
    }
}
