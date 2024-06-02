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
    public class UserAppSetting : AppSetting
    {
        public long UserID { get; set; }

        public static List<UserAppSetting> GetAllUserAppSetting(long userID)
        {
            string sql = "SELECT * FROM userappsetting where userID = @userID";
            
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<UserAppSetting> list;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@userID", userID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<UserAppSetting> ids = new List<UserAppSetting>();
                    while (dataReader.Read())
                    {
                        UserAppSetting appSetting = new UserAppSetting();
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
            string sql = "update UserAppSetting set key = @key, value = @value, value = @description, updated_at = @updated_at, updated_by = @updated_by";
            sql += " WHERE UserAppSetting.id = @id ";

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

        public void UpdateValue(long userID, string key, string value, string updated_at, string updated_by)
        {
            string sql = "update UserAppSetting set value = @value, updated_at = @updated_at, updated_by = @updated_by";
            sql += " WHERE UserAppSetting.userid = @userID and UserAppSetting.key = @key ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@updated_at", updated_at);
                cmd.Parameters.AddWithValue("@updated_by", updated_by);
                var row = cmd.ExecuteNonQuery();

            }
        }


        public static UserAppSetting GetUserAppSetting(long userID, string key)
        {
            string sql = "SELECT * FROM UserAppSetting";
            sql += " WHERE UserAppSetting.userID = @userID AND UserAppSetting.key = @key ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@key", key);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        UserAppSetting appSetting = new UserAppSetting();
                        appSetting.PopulateFromReader(dataReader);
                        return appSetting;
                    }
                    return null;
                }
            }
        }

        protected override void PopulateFromReader(IDataReader dataReader)
        {
            base.PopulateFromReader(dataReader);
            UserID = (long)dataReader.GetValue("userid");
        }
    }
}
