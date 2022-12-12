using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessLayer.DataAccess
{
    public interface IDataAccess
    {
        string GetConnectionString();
        void OpenConnection();
        void CloseConnection();
        DataSet ExecuteDataSet(string sql);
        DataTable ExecuteDataTable(string sql);
        string FilterString(string str);
    }
}
