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
    public class AclRolePermission : BaseModel
    {
        public long ID { get; set; }
        public long RoleID { get; set; }
        public long PermissionID { get; set; }

        protected Dictionary<string, bool> permissions;

        public AclRolePermission() : base()
        {
        }

        public static List<AclRolePermission> GetRolePermission(long roleID)
        {
            string sql = "SELECT * FROM acl_role_permission";
            sql += " WHERE acl_role_permission.role_id = @roleID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<AclRolePermission> list;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@roleID", roleID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<AclRolePermission> ids = new List<AclRolePermission>();
                    while (dataReader.Read())
                    {
                        AclRolePermission rolePermission = new AclRolePermission();
                        rolePermission.PopulateFromReader(dataReader);
                        ids.Add(rolePermission);
                    }
                    list = ids;
                }
                return list;
            }
        }

        protected void PopulateFromReader(IDataReader dataReader)
        {
            RoleID = (long)dataReader.GetValue("role_id");
            PermissionID = (long)dataReader.GetValue("permission_id");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
