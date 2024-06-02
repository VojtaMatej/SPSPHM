using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace SPSPHM.Model
{
    public class SitePrices : BaseModel 
    {
        public long SiteID { get; set; }
        public long ProductID { get; set; }
        public long CurrencyID { get; set; }
        public decimal Price { get; set; }
        public long VatRate { get; set; }

        public SitePrices() : base()
        {
        }

        public static SitePrices GetSiteProduct(long siteID, long productID)
        {
            string sql = "SELECT * FROM site_prices";
            sql += " WHERE site_prices.siteid = @siteID and ite_products.productid = @productID ";

            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@siteID", siteID);
                cmd.Parameters.AddWithValue("@productID", productID);

                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        SitePrices siteProducts = new SitePrices();
                        siteProducts.PopulateFromReader(dataReader);
                        return siteProducts;
                    }
                    return null;
                }
            }
        }

        public static List<SitePrices> GetAllSiteProducts(long siteID)
        {
            string sql = "SELECT * FROM site_prices WHERE siteid = @siteid";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                List<SitePrices> list;
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    List<SitePrices> ids = new List<SitePrices>();
                    while (dataReader.Read())
                    {
                        SitePrices result = new SitePrices();
                        result.PopulateFromReader(dataReader);
                        ids.Add(result);
                    }
                    list = ids;
                }
                return list;
            }
        }

        public static bool UpdatePriceProduct(long siteID, long productID, decimal priceAmount, string updatedAt, string updatedBy)
        {
            string sql = "update site_prices set price = @priceAmount, updated_at = @updatedAt, updated_by = @updatedBy WHERE siteid = @siteID and productid = @productID";
            using (SqliteConnection connStr = new SqliteConnection(ConfigurationManager.ConnectionStrings["SQLiteConn"].ConnectionString))
            using (SqliteCommand cmd = new SqliteCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@priceAmount", priceAmount);
                cmd.Parameters.AddWithValue("@updatedAt", updatedAt);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@siteID", siteID);
                cmd.Parameters.AddWithValue("@productID", productID);

                var row = cmd.ExecuteNonQuery();
                return (row == 0);
            }
        }

        protected void PopulateFromReader(IDataReader dataReader)
        {
            SiteID = (long)dataReader.GetValue("siteid");
            ProductID = (long)dataReader.GetValue("productid");
            CurrencyID = (long)dataReader.GetValue("currencyid");
            Price = (decimal)dataReader.GetValue("price");
            VatRate = (long)dataReader.GetValue("vatrate");
            CreatedAt = (string)dataReader.GetValue("created_at");
            CreatedBy = (string)dataReader.GetValue("created_by");
            UpdatedAt = (string)dataReader.GetValue("created_at");
            UpdatedBy = (string)dataReader.GetValue("updated_by");
        }

    }
}
