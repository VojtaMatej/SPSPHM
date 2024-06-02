using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SPSPHM.Model
{
    public class AppSetting : BaseModel
    {
        public long ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public string Description { get; set; }

        public AppSetting() : base()
        {
        }

        public static List<AppSetting> GetAllAppSetting()
        {
            string sql = "SELECT * FROM appsetting";
            
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<AppSetting> list;
                cmd.CommandType = CommandType.Text;

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<AppSetting> ids = new List<AppSetting>();
                    while (dataReader.Read())
                    {
                        AppSetting appSetting = new AppSetting();
                        appSetting.PopulateFromReader(dataReader);
                        ids.Add(appSetting);
                    }
                    list = ids;
                }
                return list;
            }
        }

        public override void Update()
        {
            string sql = "update appsetting set key = @key, value = @value, value = @description, updated_at = @updated_at, updated_by = @updated_by";
            sql += " WHERE appsetting.id = @id ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.Parameters.AddWithValue("@key", this.Key);
                cmd.Parameters.AddWithValue("@value", this.Value);
                cmd.Parameters.AddWithValue("@description", this.Description);
                cmd.Parameters.AddWithValue("@updated_at", this.UpdatedAt);
                cmd.Parameters.AddWithValue("@updated_by", this.UpdatedBy);
                var row = cmd.ExecuteNonQuery();

            }
        }

        public void UpdateValue(string key, string value, string updated_at, string updated_by)
        {
            string sql = "update appsetting set value = @value, updated_at = @updated_at, updated_by = @updated_by";
            sql += " WHERE appsetting.key = @key ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@updated_at", updated_at);
                cmd.Parameters.AddWithValue("@updated_by", updated_by);
                var row = cmd.ExecuteNonQuery();

            }
        }


        public static AppSetting GetAppSetting(string key)
        {
            string sql = "SELECT * FROM appsetting";
            sql += " WHERE appsetting.key = @key ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@key", key);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        AppSetting appSetting = new AppSetting();
                        appSetting.PopulateFromReader(dataReader);
                        return appSetting;
                    }
                    return null;
                }
            }
        }

        public static bool UpdateValueByKey(string key, string value, string updatedAt, string updatedBy)
        {
            string sql = "update appsetting set value = @value, updated_at = @updatedAt, updated_by = @updatedBy WHERE key = @key";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@updatedAt", updatedAt);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@key", key);

                var row = cmd.ExecuteNonQuery();
                return (row == 0);
            }
        }

        protected virtual void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            Key = (string)dataReader.GetValue("key");
            Value = (string)dataReader.GetValue("value");
            Description = (string)dataReader.GetValue("description");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
