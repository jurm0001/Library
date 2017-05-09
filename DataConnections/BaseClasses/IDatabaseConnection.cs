using System;
using System.Collections.Generic;

using System.Text;

using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DataConnections.Connections
{
    /// <summary>
    /// based database connection and funtion interface 
    /// </summary>
    public interface IDatabaseConnection
    {
        #region bool getData(string sql, bool closeConnection, int timeOut)
        /// <summary>
        /// gets the data from the database
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="closeConnection">bool to close connection after use</param>
        /// <param name="timeOut">command timeout</param>
        /// <returns>true if successful</returns>        
        bool getData(string sql, bool closeConnection, int timeOut);
        #endregion

        #region DataTable getDataTable(string sql, bool closeConnection, int timeOut);
        DataTable getDataTable(string sql, bool closeConnection, int timeOut);
        #endregion DataTable getDataTable(string sql, bool closeConnection, int timeOut);

        //public SqlDataReader getDataToReader(string sql, bool closeConnection);

        #region int executeSQL(string sql, bool closeConnection)
        /// <summary>
        /// executes the sql statement
        /// </summary>
        /// <param name="sql">sql to execute</param>
        /// <param name="closeConnection">close connection after </param>
        /// <returns>number rows affected</returns>
        int executeSQL(string sql, bool closeConnection);
        #endregion               

        #region bool OpenConnection()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool OpenConnection();
        #endregion

        #region bool CloseConnection()
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool CloseConnection();
        #endregion

        #region IDataReader executeSP(string SpName, DbParameterCollection parms);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SpName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        IDataReader executeSP(string SpName, DbParameterCollection parms);
        #endregion

        #region DataSet executeSPDataSet(string SpName, DbParameterCollection parms);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SpName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        DataSet executeSPDataSet(string SpName, DbParameterCollection parms);
        #endregion

        #region int executeSPNonQuery(string SpName, DbParameterCollection parms);
        int executeSPNonQuery(string SpName, DbParameterCollection parms);
        #endregion int executeSPNonQuery(string SpName, DbParameterCollection parms);

        #region string getSingleValue(string sql);
        string getSingleValue(string sql);
        #endregion string getSingleValue(string sql);


    }
}
