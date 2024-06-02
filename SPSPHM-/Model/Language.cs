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
    public class Language : BaseModel
    {
        public long ID { get; set; }
        public string LanguageCode { get; set; }
        public string IsoCode2 { get; set; }
        public Language() : base()
        {
        }

        public static List<Language> GetAllLanguage()
        {
            string sql = "SELECT * FROM language";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<Language> list;
                cmd.CommandType = CommandType.Text;

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<Language> ids = new List<Language>();
                    while (dataReader.Read())
                    {
                        Language language = new Language();
                        language.PopulateFromReader(dataReader);
                        ids.Add(language);
                    }
                    list = ids;
                }
                return list;
            }
        }


        protected void PopulateFromReader(IDataReader dataReader)
        {
            ID = (long)dataReader.GetValue("id");
            LanguageCode = (string)dataReader.GetValue("languagecode");
            IsoCode2 = (string)dataReader.GetValue("isocode2");
            this.CreatedAt = (string)dataReader.GetValue("created_at");
            this.CreatedBy = (string)dataReader.GetValue("created_by");
            this.UpdatedAt = (string)dataReader.GetValue("created_at");
            this.UpdatedBy = (string)dataReader.GetValue("updated_by");
        }
    }
}
