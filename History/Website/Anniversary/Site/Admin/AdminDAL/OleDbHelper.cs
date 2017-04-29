using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data.OleDb;
using DAL;

namespace Site.Admin.AdminDAL
{
    public class OleDbHelper
    {
        public static OleDbCommand cmd = null;
        public static OleDbConnection conn = null;

        public static readonly string connstr = string.Format("Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Persist Security Info=True;", D.DB.DbInfo);

        public OleDbHelper()
        { }
        #region 建立数据库连接对象
        /// <summary>   
        /// 建立数据库连接   
        /// </summary>   
        /// <returns>返回一个数据库的连接OleDbConnection对象</returns>   
        public static OleDbConnection init()
        {
            try
            {
                conn = new OleDbConnection(connstr);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return conn;
        }
        #endregion

        #region 设置OleDbCommand对象
        /// <summary>   
        /// 设置OleDbCommand对象          
        /// </summary>   
        /// <param name="cmd">OleDbCommand对象 </param>   
        /// <param name="cmdText">命令文本</param>   
        /// <param name="cmdType">命令类型</param>   
        /// <param name="cmdParms">参数集合</param>   
        private static void SetCommand(OleDbCommand cmd, string cmdText, CommandType cmdType, OleDbParameter[] cmdParms)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
        #endregion

        #region 执行相应的OleDb语句，返回相应的DataSet对象
        /// <summary>   
        /// 执行相应的OleDb语句，返回相应的DataSet对象   
        /// </summary>   
        /// <param name="OleDbstr">OleDb语句</param>   
        /// <returns>返回相应的DataSet对象</returns>   
        public static DataSet GetDataSet(string OleDbstr)
        {
            DataSet set = new DataSet();
            try
            {
                init();
                OleDbDataAdapter adp = new OleDbDataAdapter(OleDbstr, conn);
                adp.Fill(set);
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return set;
        }
        #endregion

        #region 执行相应的OleDb语句，返回相应的DataSet对象
        /// <summary>   
        /// 执行相应的OleDb语句，返回相应的DataSet对象   
        /// </summary>   
        /// <param name="OleDbstr">OleDb语句</param>   
        /// <param name="tableName">表名</param>   
        /// <returns>返回相应的DataSet对象</returns>   
        public static DataSet GetDataSet(string OleDbstr, string tableName)
        {
            DataSet set = new DataSet();
            try
            {
                init();
                OleDbDataAdapter adp = new OleDbDataAdapter(OleDbstr, conn);
                adp.Fill(set, tableName);
                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return set;
        }
        #endregion

        #region 执行不带参数OleDb语句，返回所影响的行数
        /// <summary>   
        /// 执行不带参数OleDb语句，返回所影响的行数   
        /// </summary>   
        /// <param name="cmdstr">增，删，改OleDb语句</param>   
        /// <returns>返回所影响的行数</returns>   
        public static int ExecuteNonQuery(string cmdText)
        {
            int count;
            try
            {
                init();
                cmd = new OleDbCommand(cmdText, conn);
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行带参数OleDb语句或存储过程，返回所影响的行数
        /// <summary>   
        ///  执行带参数OleDb语句或存储过程，返回所影响的行数   
        /// </summary>   
        /// <param name="cmdText">带参数的OleDb语句和存储过程名</param>   
        /// <param name="cmdType">命令类型</param>   
        /// <param name="cmdParms">参数集合</param>   
        /// <returns>返回所影响的行数</returns>   
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, OleDbParameter[] cmdParms)
        {
            int count;
            try
            {
                init();
                cmd = new OleDbCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return count;
        }
        #endregion

        #region 执行不带参数OleDb语句，返回一个从数据源读取数据的OleDbDataReader对象
        /// <summary>   
        /// 执行不带参数OleDb语句，返回一个从数据源读取数据的OleDbDataReader对象   
        /// </summary>   
        /// <param name="cmdstr">相应的OleDb语句</param>   
        /// <returns>返回一个从数据源读取数据的OleDbDataReader对象</returns>   
        public static OleDbDataReader ExecuteReader(string cmdText)
        {
            OleDbDataReader reader;
            try
            {
                init();
                cmd = new OleDbCommand(cmdText, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行带参数的OleDb语句或存储过程，返回一个从数据源读取数据的OleDbDataReader对象
        /// <summary>   
        /// 执行带参数的OleDb语句或存储过程，返回一个从数据源读取数据的OleDbDataReader对象   
        /// </summary>   
        /// <param name="cmdText">OleDb语句或存储过程名</param>   
        /// <param name="cmdType">命令类型</param>   
        /// <param name="cmdParms">参数集合</param>   
        /// <returns>返回一个从数据源读取数据的OleDbDataReader对象</returns>   
        public static OleDbDataReader ExecuteReader(string cmdText, CommandType cmdType, OleDbParameter[] cmdParms)
        {
            OleDbDataReader reader;
            try
            {
                init();
                cmd = new OleDbCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return reader;
        }
        #endregion

        #region 执行不带参数OleDb语句，返回一个DataTable对象
        /// <summary>   
        /// 执行不带参数OleDb语句，返回一个DataTable对象   
        /// </summary>   
        /// <param name="cmdText">相应的OleDb语句</param>   
        /// <returns>返回一个DataTable对象</returns>   
        public static DataTable GetDataTable(string cmdText)
        {

            OleDbDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                init();
                cmd = new OleDbCommand(cmdText, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行带参数的OleDb语句或存储过程，返回一个DataTable对象
        /// <summary>   
        /// 执行带参数的OleDb语句或存储过程，返回一个DataTable对象   
        /// </summary>   
        /// <param name="cmdText">OleDb语句或存储过程名</param>   
        /// <param name="cmdType">命令类型</param>   
        /// <param name="cmdParms">参数集合</param>   
        /// <returns>返回一个DataTable对象</returns>   
        public static DataTable GetDataTable(string cmdText, CommandType cmdType, OleDbParameter[] cmdParms)
        {
            OleDbDataReader reader;
            DataTable dt = new DataTable();
            try
            {
                init();
                cmd = new OleDbCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return dt;
        }
        #endregion

        #region 执行不带参数OleDb语句,返回结果集首行首列的值object
        /// <summary>   
        /// 执行不带参数OleDb语句,返回结果集首行首列的值object   
        /// </summary>   
        /// <param name="cmdstr">相应的OleDb语句</param>   
        /// <returns>返回结果集首行首列的值object</returns>   
        public static object ExecuteScalar(string cmdText)
        {
            object obj;
            try
            {
                init();
                cmd = new OleDbCommand(cmdText, conn);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion

        #region 执行带参数OleDb语句或存储过程,返回结果集首行首列的值object
        /// <summary>   
        /// 执行带参数OleDb语句或存储过程,返回结果集首行首列的值object   
        /// </summary>   
        /// <param name="cmdText">OleDb语句或存储过程名</param>   
        /// <param name="cmdType">命令类型</param>   
        /// <param name="cmdParms">返回结果集首行首列的值object</param>   
        /// <returns></returns>   
        public static object ExecuteScalar(string cmdText, CommandType cmdType, OleDbParameter[] cmdParms)
        {
            object obj;
            try
            {
                init();
                cmd = new OleDbCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms);
                obj = cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return obj;
        }
        #endregion
    }  

}