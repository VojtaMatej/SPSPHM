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
    public class AclPermission : BaseModel
    {
        public long ID { get; set; }
        public long OperationID { get; set; }
        public long ObjectID { get; set; }

        public AclPermission() : base()
        {
        }

        public static List<AclPermission> GetAllPermissions(long objectID)
        {
            string sql = "SELECT * FROM acl_permission";
            sql += " WHERE acl_permission.object_id = @objectID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<AclPermission> list;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@objectID", objectID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<AclPermission> ids = new List<AclPermission>();
                    while (dataReader.Read())
                    {
                        AclPermission aclPermission = new AclPermission();
                        aclPermission.PopulateFromReader(dataReader);
                        ids.Add(aclPermission);
                    }
                    list = ids;
                }
                return list;
            }
        }


        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            OperationID = (long)dataReader.GetValue("operation_id");
            ObjectID = (long)dataReader.GetValue("object_id");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
