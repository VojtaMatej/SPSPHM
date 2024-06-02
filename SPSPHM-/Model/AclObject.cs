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
    public class AclObject : BaseModel
    {
        public long ID { get; set; }
        public string ObjectName { get; set; }

        public AclObject() : base()
        {
        }

        public static AclObject GetAclObject(long objectID)
        {
            string sql = "SELECT * FROM acl_object";
            sql += " WHERE acl_object.id = @objectID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@objectID", objectID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        AclObject aclObject = new AclObject();
                        aclObject.PopulateFromReader(dataReader);
                        return aclObject;
                    }
                    return null;
                }
            }
        }

        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            ObjectName = (string)dataReader.GetValue("object_name");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
