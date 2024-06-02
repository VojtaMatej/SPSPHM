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
    public class Chain : BaseModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long CountryID { get; set; }
        public string IsChecked { get; set; }
        public string IsVisible { get; set; }

        public Chain() : base()
        {
        }

        public static List<Chain> GetAllChain()
        {
            string sql = "SELECT * FROM chain";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<Chain> list;
                cmd.CommandType = CommandType.Text;

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<Chain> ids = new List<Chain>();
                    while (dataReader.Read())
                    {
                        Chain chain = new Chain();
                        chain.PopulateFromReader(dataReader);
                        ids.Add(chain);
                    }
                    list = ids;
                }
                return list;
            }
        }


        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            Name = (string)dataReader.GetValue("name");
            CountryID = (long)dataReader.GetValue("countryid");
            IsChecked = (string)dataReader.GetValue("ischecked");
            IsVisible = (string)dataReader.GetValue("isvisible");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
