using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace SPSPHM.Model
{
    public class PrivilegedUser : User
    {
        private Dictionary<string, string> roles = null;

        public PrivilegedUser() : base()
        {
            //other stuff here
        }

        public static PrivilegedUser GetByLoginname(string loginName)
        {
            string sql = "SELECT * FROM users where users.login_name = @loginName";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        PrivilegedUser privUser = new PrivilegedUser();
                        privUser.PopulateFromReader(dataReader);
                        privUser.InitRoles();
                        return privUser;
                    }
                    return null;
                }
            }
        }

        protected void InitRoles()
        {
            string sql = "SELECT acl_user_role.role_id,acl_role.role_name from acl_user_role";
            sql += " INNER JOIN acl_role ON acl_role.id = acl_user_role.role_id";
            sql += " WHERE acl_user_role.user_id = @user_id ";
            roles = null;

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@user_id", ID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        //roles.Add((string)dataReader.GetValue("role_name"), AclRole.GetRolePerms((long)dataReader.GetValue("roleID")));
                    }
                }
            }
        }

        // check if user has a specific privilege
        public bool HasPrivilege(string objectName, string perm)
        {
            /*
            foreach ($this->roles as $role) {
                if ($role->hasPerm($object_name, $perm)) {
                    return true;
                }
            }
            */
            return false;
        }

        // check if a user has a specific role
        public bool HasRole(string roleName)
        {
            return true; //isset($this->roles[$role_name]);
        }

        // check if a user has a specific role
        public List<string> GetRoles()
        {
            return this.roles.Keys.ToList();
        }
    }
}
