using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using System.Reflection.Emit;
/*
 * to Add a new Database object simple create the database and inherit from the base classes
 * 1. BaseSQL -> microsoft sql database
 * 2. BaseDB2 -> db2 database
 * 3. BaseOracle -> Oracle database
 * add the new object as a local variable to the dal object
 * mod the constructors to get the connection string from the database
 * create the individual connections function for the database objec
 * add the call to the connect all databases
 * add close to destructor
 * add close to close function
*/

using DataConnections.Connections;

namespace DataConnections.Collections
{
    /// <summary>
    /// class that contains all the database connections
    /// after constructor inialization the connections string can be reset.
    /// no need to disconnect. the destructor closess and destroys all connections and objects.
    /// <example>
    /// Dal dal = new dal()
    /// dal.dsnpConnectionString = connectionstring
    /// ConnectAllDB()  
    /// </example>
    /// </summary>
    public class BaseDAL
    {
        /// <summary>
        /// boolean if there is an error
        /// </summary>
        public Boolean isErr;
        /// <summary>
        /// string containing last error
        /// </summary>
        public string errString;


       //EventLogWriter eventLogWriter = null;

        #region contstuctor
        /// <summary>
        /// constructor
        /// </summary>
        

        public enum EnvironmentMode
        {
            DEV,
            STAGE,
            PROD
        }

        public BaseDAL()
        {
            //this.eventLogWriter = new EventLogWriter("BaseDAL", EventLogWriter.EventViewerLogType.Application);
        }

        public BaseDAL(string eventLogWritersourceId)
        {
            //this.eventLogWriter = new EventLogWriter(eventLogWritersourceId, EventLogWriter.EventViewerLogType.Application);
        }  

        #endregion 

        #region ~BaseDAL()
        ~BaseDAL()
        {
            this.CloseAllDB();           
        }
        #endregion

        #region public bool ConnectAllDB()
        /// <summary>
        /// connects to all database.
        /// if any errors the errString is appended with the new connection error.
        /// </summary>
        /// <returns>true if no errors, false otherwise</returns>
        public bool ConnectAllDB()
        {
            this.errString = "";
            this.isErr = false;
            string type = "";

            FieldInfo[] fi = this.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {                
                if(f.FieldType.BaseType.Equals(typeof(BaseDB2)) ||
                   f.FieldType.BaseType.Equals(typeof(BaseOracle)) ||
                   f.FieldType.BaseType.Equals(typeof(BaseODBC)) ||
                   f.FieldType.BaseType.Equals(typeof(BaseSQL)))
                //if(f.FieldType.Name.Contains("DB2DSNP"))
                {
                    Type t = null;
                    //Object ActualApp = null;
                    try
                    {
                        Assembly amb = Assembly.GetAssembly(this.GetType());
                        //type = f.FieldType.FullName + ","+amb.ToString();
                        type = f.FieldType.BaseType.FullName;// +"," + amb.ToString();
                        t = Type.GetType(type, true);
                        object value = f.GetValue(this);
                        MethodInfo mi = t.GetMethod("OpenConnection");

                        //EventLogWriter.Write("CONNECTALLDB", f.Name);

                        mi.Invoke(value, new object[] { });
                        PropertyInfo p = t.GetProperty("errString");
                        if (!p.GetValue(value, null).ToString().Trim().Equals(""))
                        {
                            this.isErr = true;
                            this.errString += f.FieldType.Name + ": " + p.GetValue(value, null).ToString() + Environment.NewLine + Environment.NewLine;
                        }
                        f.SetValue(this, value);                        
                    }
                    catch (Exception ex)
                    {
                        this.errString += f.FieldType.Name + ": " + ex.ToString() + Environment.NewLine + Environment.NewLine;
                    }
                }                
            }     
            return !this.isErr;
        }
        #endregion       

        #region public void CloseAllDB()
        /// <summary>
        /// closes off the database connections
        /// </summary>
        public void CloseAllDB()
        {
            string type = "";
            this.errString = "";
            this.isErr = false;
            FieldInfo[] fi = this.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {
                if (f.FieldType.BaseType.Equals(typeof(BaseDB2)) ||
                   f.FieldType.BaseType.Equals(typeof(BaseOracle)) ||
                   f.FieldType.BaseType.Equals(typeof(BaseSQL)))
                //if(f.FieldType.Name.Contains("DB2DSNP"))
                {
                    Type t = null;
                    //Object ActualApp = null;
                    try
                    {
                        Assembly amb = Assembly.GetAssembly(this.GetType());
                        type = f.FieldType.FullName + "," + amb.ToString();
                        t = Type.GetType(type, true);
                        object value = f.GetValue(this);
                        MethodInfo mi = t.GetMethod("CloseConnection");
                        mi.Invoke(value, new object[] { });


                        PropertyInfo p = t.GetProperty("errString");

                        if (!p.GetValue(value, null).ToString().Trim().Equals(""))
                        {
                            this.isErr = true;
                            this.errString += f.FieldType.Name + ": " + p.GetValue(value, null).ToString() + Environment.NewLine + Environment.NewLine;
                        }
                        f.SetValue(this, value);
                    }
                    catch (Exception ex)
                    {
                        string errMessage = ex.ToString();
                    }
                }
            }
        }
        #endregion       

    }
}
