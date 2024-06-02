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
    public class AclOperation : BaseModel
    {
        public long ID { get; set; }
        public string OperationName { get; set; }

        public AclOperation() : base()
        {
        }

        public static AclOperation GetAclOperation(long operationID)
        {
            string sql = "SELECT * FROM acl_operation";
            sql += " WHERE acl_operation.id = @operationID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@operationID", operationID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        AclOperation aclOperation = new AclOperation();
                        aclOperation.PopulateFromReader(dataReader);
                        return aclOperation;
                    }
                    return null;
                }
            }
        }

        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            OperationName = (string)dataReader.GetValue("operation_name");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
