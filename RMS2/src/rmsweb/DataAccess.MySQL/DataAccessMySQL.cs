using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess.MySQL
{
    public class DataAccessMySQL
    {
        public string ConnectionString { get; set; }


        /// <summary>
        /// 建立一個新的資料庫陳述式參數
        /// </summary>
        /// <param name="ParameterFieldName">欄位名稱</param>
        /// <param name="DBFieldType">欄位型態</param>
        /// <param name="ParameterValue">欄位值</param>
        /// <returns>DbParameter Object</returns>
        public static DbParameter CreateParameter(string ParameterFieldName, DbType DBFieldType, object ParameterValue)
        {
            DbParameter param = new MySqlParameter();
            param.ParameterName = ParameterFieldName;
            param.DbType = DBFieldType;
            param.Value = ParameterValue;

            return param;
        }

        /// <summary>
        /// 建立一個新的DbConnection型態物件
        /// </summary>
        /// <returns>DbConnection Object</returns>
        public DbConnection CreateDbConnection()
        {
            DbConnection objDbConn = null;

            if (objDbConn == null)
            {
                objDbConn = new MySqlConnection();
                objDbConn.ConnectionString = this.ConnectionString;
            }

            return objDbConn;
        }

        protected DbCommand CreateDbCommand()
        {
            DbCommand objDbCmd = null;

            if (objDbCmd == null)
            {
                objDbCmd = CreateDbConnection().CreateCommand();
            }

            return objDbCmd;
        }

        public DbTransaction CreateDbTransaction()
        {
            DbConnection objDbConn = this.CreateDbConnection();
            objDbConn.Open();
            return objDbConn.BeginTransaction();
        }

        /// <summary>
        /// 執行SQL陳述式，並傳回受影響的資料列數。
        /// </summary>
        /// <param name="commandText">SQL陳述式</param>
        /// <param name="parameters">SQL陳述式參數</param>
        /// <returns>
        public int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, null, parameters);
        }

        public int ExecuteNonQuery(string commandText, DbTransaction objTrans, params DbParameter[] parameters)
        {
            return this.ExecuteNonQuery(CommandType.Text, commandText, objTrans, parameters);
        }

        public int ExecuteNonQuery(CommandType cmdType, string commandText, DbTransaction objTrans, params DbParameter[] parameters)
        {
            DbCommand objDbCmd = null;

            try
            {
                objDbCmd = CreateDbCommand();
                objDbCmd.CommandType = cmdType;
                objDbCmd.CommandText = commandText;

                if (objTrans != null)
                {
                    objDbCmd.Connection = objTrans.Connection;
                    objDbCmd.Transaction = objTrans;
                }

                objDbCmd.Parameters.Clear();

                if (parameters != null)
                    if (parameters.Length > 0)
                    {
                        foreach (DbParameter param in parameters)
                        {
                            objDbCmd.Parameters.Add(param);
                        }
                    }


                if (objDbCmd.Connection.State == ConnectionState.Closed)
                    objDbCmd.Connection.Open();


                return objDbCmd.ExecuteNonQuery();
            }
            finally
            {
                if (objTrans == null && objDbCmd != null)
                {
                    if (objDbCmd.Connection.State == ConnectionState.Open)
                    {
                        objDbCmd.Connection.Close();
                        objDbCmd.Connection.Dispose();
                    }

                    objDbCmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 執行SQL陳述式，並傳回DataTable型別物件。
        /// </summary>
        /// <param name="commandText">SQL陳述式</param>
        /// <param name="parameters">SQL陳述式參數</param>
        /// <returns>DataTable 物件。</returns>
        public DataTable ExecuteDataTable(string commandText, params DbParameter[] parameters)
        {
            return ExecuteDataTable(CommandType.Text, commandText, null, parameters);
        }

        public DataTable ExecuteDataTable(string commandText, DbTransaction objTrans, params DbParameter[] parameters)
        {
            return ExecuteDataTable(CommandType.Text, commandText, objTrans, parameters);
        }

        public DataTable ExecuteDataTable(CommandType cmdType, string commandText, DbTransaction objTrans, params DbParameter[] parameters)
        {
            DbCommand objDbCmd = null;
            DbDataAdapter da = new MySqlDataAdapter();
            try
            {
                objDbCmd = CreateDbCommand();

                DataTable _tmpTable = new DataTable("tmpTable");
                objDbCmd.CommandType = cmdType;
                objDbCmd.CommandText = commandText;

                if (objTrans != null)
                {
                    objDbCmd.Connection = objTrans.Connection;
                    objDbCmd.Transaction = objTrans;
                }

                objDbCmd.Parameters.Clear();

                if (parameters != null)
                    if (parameters.Length > 0)
                    {
                        foreach (DbParameter param in parameters)
                        {
                            objDbCmd.Parameters.Add(param);
                        }
                    }

                da.SelectCommand = objDbCmd;
                da.Fill(_tmpTable);
                return _tmpTable;
            }
            finally
            {
                da.Dispose();

                if (objTrans == null && objDbCmd != null)
                {
                    if (objDbCmd.Connection.State == ConnectionState.Open)
                    {
                        objDbCmd.Connection.Close();
                        objDbCmd.Connection.Dispose();
                    }

                    objDbCmd.Dispose();
                }
            }
        }

        public object ExecuteScalar(string commandText, params DbParameter[] parameters)
        {
            return this.ExecuteScalar(CommandType.Text, commandText, null, parameters);
        }

        public object ExecuteScalar(string commandText, DbTransaction objTrans, params DbParameter[] parameters)
        {
            return this.ExecuteScalar(CommandType.Text, commandText, objTrans, parameters);
        }

        public object ExecuteScalar(CommandType cmdType, string commandText, DbTransaction objTrans, params DbParameter[] parameters)
        {
            object obj;
            DbCommand objDbCmd = null;
            try
            {
                objDbCmd = this.CreateDbCommand();
                objDbCmd.CommandType = cmdType;
                objDbCmd.CommandText = commandText;
                if (objTrans != null)
                {
                    objDbCmd.Connection = objTrans.Connection;
                    objDbCmd.Transaction = objTrans;
                }
                objDbCmd.Parameters.Clear();
                if (parameters != null && (int)parameters.Length > 0)
                {
                    DbParameter[] dbParameterArray = parameters;
                    for (int i = 0; i < (int)dbParameterArray.Length; i++)
                    {
                        DbParameter dbParameter = dbParameterArray[i];
                        objDbCmd.Parameters.Add(dbParameter);
                    }
                }

                if (objDbCmd.Connection.State == ConnectionState.Closed)
                {
                    objDbCmd.Connection.Open();
                }
                obj = objDbCmd.ExecuteScalar();
            }
            finally
            {
                if (objTrans == null && objDbCmd != null)
                {
                    if (objDbCmd.Connection.State == ConnectionState.Open)
                    {
                        objDbCmd.Connection.Close();
                        objDbCmd.Connection.Dispose();
                    }
                }
                objDbCmd.Dispose();

            }
            return obj;
        }
    }
}
