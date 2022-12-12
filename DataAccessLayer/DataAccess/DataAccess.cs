using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DataAccess
{
    public class DataAccess : IDataAccess
    {
        SqlConnection _sqlconnection;
        IConfiguration _config;
        public DataAccess(IConfiguration config)
        {
            _config = config;
            _sqlconnection = new SqlConnection(GetConnectionString());
        }
        public string GetConnectionString()
        {
            return _config["ConnectionStrings:DefaultConnection"];
        }
        public int GetConnectionTimeOut()
        {
            return Convert.ToInt32(_config["ConnectionStrings:ConnectionTimeOut"]);
        }
        public void OpenConnection()
        {
            if (_sqlconnection.State == ConnectionState.Open)
            {
                _sqlconnection.Close();
            }
            _sqlconnection.Open();
        }
        public void CloseConnection()
        {
            if (_sqlconnection.State == ConnectionState.Open)
            {
                _sqlconnection.Close();
            }
        }
        public DataSet ExecuteDataSet(string sql)
        {
            var ds = new DataSet();
            var cmd = new SqlCommand(sql, _sqlconnection);
            SqlDataAdapter da;
            try
            {
                OpenConnection();
                cmd.CommandTimeout = GetConnectionTimeOut();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                CloseConnection();
            }
            catch (Exception)
            {

            }
            finally
            {
                da = null;
                CloseConnection();
            }
            return ds;
        }
        public DataTable ExecuteDataTable(string sql)
        {
            using (var ds = ExecuteDataSet(sql))
            {
                var dt = new DataTable();
                if (ds == null)
                {
                    dt = null;
                }
                else if (ds.Tables[0].Rows.Count <= 0)
                {
                    dt = null;
                }
                else
                {
                    dt = ds.Tables[0];
                }
                return dt;
            }
        }
        public string FilterString(string str)
        {
            var strval = "";
            if (string.IsNullOrEmpty(str))
            {
                strval = "null";
            }
            else
            {
                strval = "'" + str + "'";
            }
            return strval;
        }
    }
}
