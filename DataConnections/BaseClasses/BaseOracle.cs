using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.SqlTypes;





namespace DataConnections.Connections
{
    public class BaseOracle : IDatabaseConnection
    {
        #region Database Connection Methods and Properties
        #region class variables        

        private OracleConnection _conn;
        public OracleConnection Connection
        {
            get { return this._conn; }
        }
        private DataSet _ds;
        public DataSet dataSet
        {
            get
            {
                return this._ds;
            }
        }

        private OracleCommand _command;
        public OracleCommand Command
        {
            get { return this._command; }
            set { this._command = value; }
        }

        private string _mode;
        //private string _connectionString;
        private string _connectionString = @"Server=ddwprod01:1543;Data Source=VOADHOC;User ID=hco1a;Password=IDIMCO1;";
        public string ConnectionString
        {
            get { return this._connectionString; }
            set { this._connectionString = value; }
        }
        /// <summary>
        /// err string with object
        /// </summary>
        public string errString
        {
            get
            {
                return this._errString;
            }
        }
        protected string _errString;
        /// <summary>
        /// boolean if there is an err
        /// </summary>
        public bool isErr
        {
            get
            {
                return this._isErr;
            }
        }
        protected bool _isErr;

        /// <summary>
        /// datatable of database
        /// </summary>
        public DataTable dataTable
        {
            get
            {
                if (this.dataSet != null)
                    if (this.dataSet.Tables.Count > 0)
                        return this.dataSet.Tables[0];
                return null;
            }
        }

        //EventLogWriter eventLogWriter = null;

        #endregion

        #region Constructor

        public BaseOracle()
        {
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="connectionString"></param>
        public BaseOracle(string mode, string connectionString)
        {
            this._mode = mode;
            this._connectionString = connectionString;
            this._errString = "";
            this._isErr = false;
            //if (!connectionString.Trim().Equals("") && connectionString.Length > 10)
            {
                try
                {
                    this._conn = new OracleConnection(connectionString);
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString());}
            }

            this._ds = new DataSet();
            //this.eventLogWriter = new EventLogWriter("BaseOracle", EventLogWriter.EventViewerLogType.Application);
        }
        public BaseOracle(string mode, string connectionString, string eventLogWriterSourceID)
        {
            this._mode = mode;
            this._connectionString = connectionString;
            this._errString = "";
            this._isErr = false;
            //if (!connectionString.Trim().Equals("") && connectionString.Length > 10)
            {
                try
                {
                    this._conn = new OracleConnection(connectionString);
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString());}
            }

            this._ds = new DataSet();
            //this.eventLogWriter = new EventLogWriter(eventLogWriterSourceID, EventLogWriter.EventViewerLogType.Application);
        }
        #endregion

        #region Deconstructor
        /// <summary>
        /// destructor
        /// </summary>
        ~BaseOracle()
        {
        }
        #endregion

        #region OpenConnection
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            if (this._conn == null)
            {
                try
                {
                    //this._conn.Open();
                    this._conn = new OracleConnection(this._connectionString);
                    this._conn.Open();
                }
                catch (Exception ex)
                {
                    this._isErr = true;
                    this._errString = ex.ToString();
                    return false;
                }
            }
            else if (this._conn.State == ConnectionState.Closed)
            {
                try
                {
                    this._conn.Open();
                }
                catch (Exception ex)
                {
                    this._isErr = true;
                    this._errString = ex.ToString();
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region CloseConnection
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CloseConnection()
        {
            this._conn.Close();
            return true;
        }
        #endregion

        #region getData
        /// <summary>
        /// get the data from the database
        /// </summary>
        /// <param name="sql">select sql statment</param>
        /// <param name="closeConnection">boolean to close connection after retreive, 
        ///                               set to false for large amount of transactions then 
        ///                               manually close the connection</param>
        /// <param name="timeOut">command timeout</param>
        /// <returns>true if successfull, false otherwise</returns>
        public bool getData(string sql, bool closeConnection, int timeOut)
        {
            if (this._conn.State == ConnectionState.Closed)
            {
                if (!OpenConnection())
                    return false;
            }

            this.FillDataSet(sql, timeOut);

            if (closeConnection)
                CloseConnection();

            if (this._isErr)
            {
                return false;
            }
            return true;
        }
        public bool getData(OracleCommand cmd, bool closeConnection, int timeOut)
        {
            if (this._conn.State == ConnectionState.Closed)
            {
                if (!OpenConnection())
                    return false;
            }

            this.FillDataSet(cmd, timeOut);

            if (closeConnection)
                CloseConnection();

            if (this._isErr)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region getDataTable
        /// <summary>
        /// get the data from the database
        /// </summary>
        /// <param name="sql">select sql statment</param>
        /// <param name="closeConnection">boolean to close connection after retreive, 
        ///                               set to false for large amount of transactions then 
        ///                               manually close the connection</param>
        /// <param name="timeOut">command timeout</param>
        /// <returns>true if successfull, false otherwise</returns>
        public DataTable getDataTable(string sql, bool closeConnection, int timeOut)
        {
            if (this._conn.State == ConnectionState.Closed)
            {
                if (!OpenConnection())
                    return null;
            }

            this.FillDataSet(sql, timeOut);

            if (closeConnection)
                CloseConnection();

            if (this._isErr)
            {
                return null;
            }
            return this.dataTable;
        }
        public DataTable getDataTable(OracleCommand cmd, bool closeConnection, int timeOut)
        {
            if (this._conn.State == ConnectionState.Closed)
            {
                if (!OpenConnection())
                    return null;
            }

            this.FillDataSet(cmd, timeOut);

            if (closeConnection)
                CloseConnection();

            if (this._isErr)
            {
                return null;
            }
            return this.dataTable;
        }
        #endregion

        #region getDataToReader
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="closeConnection"></param>
        /// <returns></returns>
        public OracleDataReader getDataToReader(string sql, bool closeConnection)
        {
            if (this._conn.State == ConnectionState.Closed)
            {
                if (!OpenConnection())
                    return null;
            }

            this._command = new OracleCommand(sql, this._conn);
            OracleDataReader rdr = this._command.ExecuteReader();

            if (closeConnection)
                CloseConnection();
            return rdr;
        }
        public OracleDataReader getDataToReader(OracleCommand cmd, bool closeConnection)
        {
            if (this._conn.State == ConnectionState.Closed)
            {
                if (!OpenConnection())
                    return null;
            }

            this._command = cmd;
            OracleDataReader rdr = this._command.ExecuteReader();

            if (closeConnection)
                CloseConnection();
            return rdr;
        }
        #endregion getDataToReader

        #region executeSQL
        /// <summary>
        /// executes a non select sql statement
        /// </summary>
        /// <param name="sql">sql command to execute</param>
        /// <param name="closeConnection">close connection after processing</param>
        /// <returns></returns>
        public int executeSQL(string sql, bool closeConnection)
        {
            if (!OpenConnection())
                return -1;

            this._errString = "";
            this._isErr = false;

            int x = 0;
            lock (this)
            {
                x = executeSQLLocal(sql);
            }
            if (closeConnection)
                CloseConnection();

            if (this._isErr)
            {
                return -1;
            }
            return x;
        }
        public int executeSQL(OracleCommand cmd, bool closeConnection)
        {
            if (!OpenConnection())
                return -1;

            this._errString = "";
            this._isErr = false;

            int x = 0;
            lock (this)
            {
                x = executeSQLLocal(cmd);
            }
            if (closeConnection)
                CloseConnection();

            if (this._isErr)
            {
                return -1;
            }
            return x;
        }
        #endregion



        #region public IDataReader executeSP(string SpName, DbParameterCollection parms)
        /// <summary>
        /// executes a stored procedure and maintains all commits and errors
        /// </summary>
        /// <param name="SpName">stored procedure name</param>
        /// <param name="parms">database variable collection</param>
        /// <returns></returns>
        public IDataReader executeSP(string SpName, DbParameterCollection parms)
        {

            this._isErr = false;
            this._errString = "";
            if (!OpenConnection())
            {
                return null;
            }

            this._command = new OracleCommand(SpName, this._conn);
            this._command.CommandType = CommandType.StoredProcedure;
            if (parms != null)
                for (int i = 0; i < parms.Count; ++i)
                {
                    OracleParameter p = new OracleParameter(parms[i].ParameterName, parms[i].Value);
                    this._command.Parameters.Add(p);
                }

            OracleDataReader rdr;
            try
            {
                rdr = this._command.ExecuteReader();
            }
            catch (Exception ex)
            {
                this._isErr = true;
                this._errString = ex.ToString();
                return null;
            }
            return (IDataReader)rdr;
        }
        #endregion public IDataReader executeSP(string SpName, DbParameterCollection parms)

        

        #region public void executeSPNonQuery(string SpName, DbParameterCollection parms)
        /// <summary>
        /// executes a stored procedure and maintains all commits and errors
        /// </summary>
        /// <param name="SpName">stored procedure name</param>
        /// <param name="parms">database variable collection</param>
        /// <returns></returns>
        public int executeSPNonQuery(string SpName, DbParameterCollection parms)
        {

            this._isErr = false;
            this._errString = "";
            if (!OpenConnection())
            {
                return -1;
            }
            this._command = new OracleCommand(SpName, this._conn);
            this._command.CommandType = CommandType.StoredProcedure;

            if (parms != null)
                for (int i = 0; i < parms.Count; ++i)
                {
                    OracleParameter p = new OracleParameter(parms[i].ParameterName, parms[i].Value);
                    this._command.Parameters.Add(p);
                }

            int rdr;
            try
            {
                rdr = this._command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this._isErr = true;
                this._errString = ex.ToString();
                return -1;
            }
            return rdr;
        }

        #endregion public IDataReader executeSP(string SpName, DbParameterCollection parms)

        #region public DataSet executeSPDataSet(string SpName, DbParameterCollection parms)
        /// <summary>
        /// executes a stored procedure and maintains all commits and errors
        /// </summary>
        /// <param name="SpName">stored procedure name</param>
        /// <param name="parms">database variable collection</param>
        /// <returns>Dataset of the stored procedure</returns>
        public DataSet executeSPDataSet(string SpName, DbParameterCollection parms)
        {
            this._isErr = false;
            this._errString = "";
            DataSet ds = new DataSet();

            if (!OpenConnection())
            {
                return null;
            }

            this._command = new OracleCommand(SpName, this._conn);
            this._command.CommandType = CommandType.StoredProcedure;
            if (parms != null)
                for (int i = 0; i < parms.Count; ++i)
                {
                    OracleParameter p = new OracleParameter(parms[i].ParameterName, parms[i].Value);
                    this._command.Parameters.Add(p);
                }

            OracleDataAdapter adp = new OracleDataAdapter(this._command);
            int j = 0;
            try
            {
                if ((j = adp.Fill(ds)) < 0)
                {
                    //this._isErr = true;
                    //this._errString = this._conn.errorString;
                    adp.Dispose();
                    return null;
                }
            }
            catch (Exception ex)
            {
                this._isErr = true;
                this._errString = ex.ToString();
                adp.Dispose();
                return null;
            }

            adp.Dispose();
            return ds;
        }
        #endregion public DataSet executeSPDataSet(string SpName, DbParameterCollection parms)

        #region public string getSingleValue(string sql)
        public string getSingleValue(string sql)
        {
            this._isErr = false;
            this._errString = "";
            this.getData(sql, false, 300);
            if (this._isErr)
                return "";

            DataTable dt2 = this.dataTable;
            if (dt2 != null)
            {
                if (dt2.Rows.Count > 0)
                {
                    try
                    {
                        string v = dt2.Rows[0][0].ToString().Trim();
                        return dt2.Rows[0][0].ToString().Trim();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString());}
                }
            }
            return "";
        }
        public string getSingleValue(OracleCommand cmd)
        {
            this._isErr = false;
            this._errString = "";
            this.getData(cmd, false, 300);
            if (this._isErr)
                return "";

            DataTable dt2 = this.dataTable;
            if (dt2 != null)
            {
                if (dt2.Rows.Count > 0)
                {
                    try
                    {
                        string v = dt2.Rows[0][0].ToString().Trim();
                        return dt2.Rows[0][0].ToString().Trim();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString());}
                }
            }
            return "";
        }
        #endregion

        public int RowCount
        {
            get
            {
                if (this.dataTable == null)
                    return 0;
                else
                    return this.dataTable.Rows.Count;
            }
        }


        private bool FillDataSet(string sql, int timeOut)
        {
            this._errString = "";
            this._isErr = false;
            if (this._conn == null)
            {
                this._errString = "NULL_CONNECTION";
                this._isErr = true;
                return false;
            }
            if (this._conn.State != ConnectionState.Open)
            {
                this._errString = "NOT_CONNECTED";
                this._isErr = true;
                return false;
            }
            try
            {
                this._command = new OracleCommand(sql, this._conn);
                if (timeOut == 0)
                    this._command.ResetCommandTimeout();
                else
                    this._command.CommandTimeout = timeOut;
                OracleDataAdapter sqlAdapter = new OracleDataAdapter();
                sqlAdapter.TableMappings.Add("Table", "Table1");
                sqlAdapter.SelectCommand = this._command;

                this._ds = new DataSet();
                sqlAdapter.Fill(this._ds);
            }
            catch (Exception ex)
            {
                this._errString = ex.ToString();
                this._isErr = true;
                return false;
            }

            return true;
        }
        private bool FillDataSet(OracleCommand cmd, int timeOut)
        {
            this._errString = "";
            this._isErr = false;
            if (this._conn == null)
            {
                this._errString = "NULL_CONNECTION";
                this._isErr = true;
                return false;
            }
            if (this._conn.State != ConnectionState.Open)
            {
                this._errString = "NOT_CONNECTED";
                this._isErr = true;
                return false;
            }
            try
            {
                this._command = cmd;
                if (timeOut == 0)
                    this._command.ResetCommandTimeout();
                else
                    this._command.CommandTimeout = timeOut;
                OracleDataAdapter sqlAdapter = new OracleDataAdapter();
                sqlAdapter.TableMappings.Add("Table", "Table1");
                sqlAdapter.SelectCommand = this._command;

                this._ds = new DataSet();
                sqlAdapter.Fill(this._ds);
            }
            catch (Exception ex)
            {
                this._errString = ex.ToString();
                this._isErr = true;
                return false;
            }

            return true;
        }

        private int executeSQLLocal(string sql)
        {
            int x = 0;

            this._errString = "";
            this._isErr = false;


            if (this._conn == null)
            {
                this._errString = "Connection is null";
                this._isErr = true;
                return -1;
            }
            if (this._conn.State != ConnectionState.Open)
            {
                this._errString = "Connection state not open: " + this._conn.State.ToString();
                this._isErr = true;
                return -1;
            }

            this._command = this._conn.CreateCommand();
            OracleTransaction trans = this._conn.BeginTransaction(IsolationLevel.ReadCommitted);

            this._command.Connection = this._conn;
            this._command.Transaction = trans;
            this._command.CommandText = sql;

            try
            {
                x = this._command.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    trans.Rollback();
                    this._errString += " " + ex.ToString();
                    this._isErr = true;
                }
                catch (Exception ex2)
                {
                    this._errString += " " + ex2.ToString();
                    this._isErr = true;
                }
                return x;
            }
            return x;
        }
        private int executeSQLLocal(OracleCommand cmd)
        {
            int x = 0;

            this._errString = "";
            this._isErr = false;


            if (this._conn == null)
            {
                this._errString = "Connection is null";
                this._isErr = true;
                return -1;
            }
            if (this._conn.State != ConnectionState.Open)
            {
                this._errString = "Connection state not open: " + this._conn.State.ToString();
                this._isErr = true;
                return -1;
            }

            this._command = cmd;
            OracleTransaction trans = this._conn.BeginTransaction(IsolationLevel.ReadCommitted);

            this._command.Connection = this._conn;
            this._command.Transaction = trans;
            //this._command.CommandText = sql;

            try
            {
                x = this._command.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    trans.Rollback();
                    this._errString += " " + ex.ToString();
                    this._isErr = true;
                }
                catch (Exception ex2)
                {
                    this._errString += " " + ex2.ToString();
                    this._isErr = true;
                }
                return x;
            }
            return x;
        }

        #endregion Database Connection Methods and Properties
    }
}
