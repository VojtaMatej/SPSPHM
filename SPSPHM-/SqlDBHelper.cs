using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SPSPHM
{
    public static class SqlDBHelper
    {
        public static DataSet ExecuteDataSet(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                catch (SqlException ex)
                {
                    //log to a file or Throw a message ex.Message;
                }
                return ds;
            }
        }

        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqliteParameter[] parameters)
        {
            using (DataTable dt = new DataTable())
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    SqliteDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    //log to a file or Throw a message ex.Message;
                }
                return dt;
            }
        }
    }
}