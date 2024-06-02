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
    public class AclRole : BaseModel
    {
        public long ID { get; set; }
        public string RoleName { get; set; }

        protected Dictionary<string, bool> permissions;

        public AclRole() : base()
        {
            this.permissions = new Dictionary<string, bool>();
        }

        public static void GetRolePerms(long roleID)
        {
            string sql = "SELECT acl_object.object_name,acl_operation.operation_name FROM acl_role";
            sql += " INNER JOIN acl_role_permission on acl_role_permission.role_id = acl_role.id INNER JOIN acl_permission on ";
            sql += " acl_permission.id = acl_role_permission.permission_id INNER JOIN acl_operation on acl_operation.id = ";
            sql += " acl_permission.operation_id INNER JOIN acl_object on acl_object.id = acl_permission.object_id";
            sql += " WHERE acl_role.id = @roleID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@roleID", roleID);

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

        public bool HasPerm(string objectName, string permission)
        {
            return false; //isset($this->permissions[$object_name][$permission]);
        }

        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            RoleName = (string)dataReader.GetValue("role_name");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }

        protected void PopulateFromReaderRolePerm(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            RoleName = (string)dataReader.GetValue("role_name");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
