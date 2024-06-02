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
    public class AclUserRole : BaseModel
    {
        public long UserID { get; set; }
        public long RoleID { get; set; }


        public AclUserRole() : base()
        {
            
        }

        public static void GetAllUserRole(long userID)
        {
            string sql = "SELECT * from acl_user_role";
            sql += " WHERE acl_user_role.user_id = @userID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@userID", userID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        //permissions.Add(String.Format("{0}{1}", (string)dataReader.GetValue("object_name"), (string)dataReader.GetValue("operation_name")), true);
                    }
                }
            }
        }


        protected void PopulateFromReader(IDataReader dataReader)
        {
            UserID = (long)dataReader.GetValue("user_id");
            RoleID = (long)dataReader.GetValue("role_id");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
