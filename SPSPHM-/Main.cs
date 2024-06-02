using Microsoft.Data.Sqlite;
using SPSPHM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Text.Json;
using System.IO;
using System.Reflection;

namespace SPSPHM
{
    public partial class Main : Form
    {
        FrmFilter frmFilter;
        enum PriceLevel { low, middle, high }
        PrivilegedUser privilegedUser = null;
        DataTable dtCountry = null;
        DataTable dtProduct = null;
        DataTable dtCategory = null;

        string _DefaultProduct = "1";
        string _DefaultCountry = "1";
        string _DefaultPriceMinMax = "0:100";
        string _DefaultPriceColor = "1";
        bool isInitalized = false;
        bool isSyncStarted = false;

        Dictionary<long, PriceLevel> priceLevel = new Dictionary<long, PriceLevel>();

        public delegate void PassDataDelegate(string countryID, string productID, string priceColor, string priceMinMax);
        public static event Action OperationCompleted;

        public Main(PrivilegedUser privilegedUser)
        {
            this.privilegedUser = privilegedUser;
            InitializeComponent();
            InitializeUserSetting();
            RebuildSites();
            isInitalized = true;
        }

        private void InitializeUserSetting()
        {
            dtCountry = SqlDBHelper.ExecuteDataTable("select id,description from country inner join localiseddescriptions ld on ld.value = country.id and ld.columnname = 'country.id' and ld.culture = 'cs-CZ'", CommandType.Text, new SqliteParameter[] { });
            dtProduct = SqlDBHelper.ExecuteDataTable("select id,description from product inner join localiseddescriptions ld on ld.value = product.id and ld.columnname = 'product.id' and ld.culture = 'cs-CZ'", CommandType.Text, new SqliteParameter[] { });
            DataTable dtUserAppSetting = SqlDBHelper.ExecuteDataTable("select key,value from userappsetting where key in ('DEFAULT_PRODUCT','DEFAULT_COUNTRY','DEFAULT_CULTURE','DEFAULT_PRICE_MIN_MAX','DEFAULT_PRICE_COLOR') AND userid = " + privilegedUser.ID, CommandType.Text, new SqliteParameter[] { });
            foreach (DataRow row in dtUserAppSetting.Rows)
            {
                if (row["key"].ToString().ToUpper().Equals("DEFAULT_PRODUCT"))
                {
                    this._DefaultProduct = row["value"].ToString();
                    continue;
                }
                if (row["key"].ToString().ToUpper().Equals("DEFAULT_COUNTRY"))
                {
                    this._DefaultCountry = row["value"].ToString();
                    continue;
                }
                if (row["key"].ToString().ToUpper().Equals("DEFAULT_PRICE_MIN_MAX"))
                {
                    this._DefaultPriceMinMax = row["value"].ToString();
                    continue;
                }
                if (row["key"].ToString().ToUpper().Equals("DEFAULT_PRICE_COLOR"))
                {
                    this._DefaultPriceColor = row["value"].ToString();
                    continue;
                }
            }
            dtCategory = SqlDBHelper.ExecuteDataTable("select id,name from sitecategory", CommandType.Text, new SqliteParameter[] { });
            cbCategory.DataSource = dtCategory;
            cbCategory.DisplayMember = "name";
            cbCategory.ValueMember = "id";
            cbCategory.SelectedValue = 1;
        }

        private void ReceiveData(string countryID, string productID, string priceColor, string priceMinMax)
        {
            // Display the received data in a label
            this._DefaultCountry = countryID;
            this._DefaultProduct = productID;
            this._DefaultPriceColor = priceColor;
            this._DefaultPriceMinMax = priceMinMax;

            UserAppSetting userAppSetting = new UserAppSetting();
            userAppSetting.UpdateValue(privilegedUser.ID, "DEFAULT_COUNTRY", countryID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), privilegedUser.DisplayName);
            userAppSetting.UpdateValue(privilegedUser.ID, "DEFAULT_PRODUCT", productID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), privilegedUser.DisplayName);
            userAppSetting.UpdateValue(privilegedUser.ID, "DEFAULT_PRICE_COLOR", priceColor, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), privilegedUser.DisplayName);
            userAppSetting.UpdateValue(privilegedUser.ID, "DEFAULT_PRICE_MIN_MAX", priceMinMax, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), privilegedUser.DisplayName);
            RebuildSites();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            frmFilter = new FrmFilter();
            frmFilter.PassData = new PassDataDelegate(ReceiveData);
            frmFilter.SetCountryDataTable(dtCountry, this._DefaultCountry);
            frmFilter.SetProductDataTable(dtProduct, this._DefaultProduct);
            frmFilter.SetPriceColor(this._DefaultPriceColor);
            frmFilter.SetPriceMinMax(this._DefaultPriceMinMax);
            frmFilter.BringToFront();
            frmFilter.ShowDialog();
        }

        private void RebuildSites()
        {
            string query = "";
            SqliteParameter[] sqlPar;
            decimal priceMin = 0m;
            decimal priceMax = 100m;
            string[] _priceMinMax = _DefaultPriceMinMax.Split(':');

            if (_priceMinMax.Length == 2)
            {
                priceMin = decimal.Parse(_priceMinMax[0], CultureInfo.InvariantCulture);
                priceMax = decimal.Parse(_priceMinMax[1], CultureInfo.InvariantCulture);
            }

            if ("1".Equals(cbCategory.SelectedValue.ToString()) || "2".Equals(cbCategory.SelectedValue.ToString()))
            {
                query += "select s.id,chain.name as Majitel, s.name as Název,s.city as Město,s.street as Ulice,ld1.description as Produkt, sp.price as Cena, cu.isocode as Měna, date(sp.updated_at) as Aktualizováno,  s.url as URL from site s";
                query += " inner join chain on chain.id = s.chainid ";
                query += " inner join site_prices sp on s.id = sp.siteid and sp.productid = @productid and sp.price between @priceMin and @priceMax";
                query += " inner join localiseddescriptions ld1 on ld1.value = sp.productid and ld1.columnname = 'product.id' and ld1.culture = 'cs-CZ'";
                query += " inner join currency cu on cu.id = sp.currencyid ";
            } else { 
                query += "select s.id,chain.name as Majitel, s.name as Název,s.city as Město,s.street as Ulice, s.url as URL from site s";
                query += " inner join chain on chain.id = s.chainid ";
            }
            query += " where s.categoryid = @categoryid ";
            query += " and (chain.name||s.name||s.city||s.street) like @search";
            query += " and (0 = @countryID or s.countryid = @countryID)";

            if ("1".Equals(cbCategory.SelectedValue.ToString()) || "2".Equals(cbCategory.SelectedValue.ToString()))
            {
                query += " order by sp.price";
                sqlPar = new[] { new SqliteParameter("categoryid"
                    , cbCategory.SelectedValue)
                    , new SqliteParameter("productid", _DefaultProduct)
                    , new SqliteParameter("search"
                    , (string.IsNullOrEmpty(txtSearch.Text) ? "%" : "%" + txtSearch.Text + "%"))
                    , new SqliteParameter("countryID", _DefaultCountry)
                    , new SqliteParameter("priceMin", priceMin)
                    , new SqliteParameter("priceMax", priceMax)
                };
            }
            else
                sqlPar = new[] { new SqliteParameter("categoryid"
                    , cbCategory.SelectedValue)
                    , new SqliteParameter("productid", _DefaultProduct)
                    , new SqliteParameter("search", (string.IsNullOrEmpty(txtSearch.Text) ? "%" : "%" + txtSearch.Text + "%"))
                    , new SqliteParameter("countryID", _DefaultCountry)
                };

            
            DataTable dt = SqlDBHelper.ExecuteDataTable(query, CommandType.Text, sqlPar);
            Console.WriteLine("sortedColumn:" + dgwSite.SortedColumn);
            Console.WriteLine("sorted:" + dgwSite.SortOrder);

            if ("1".Equals(cbCategory.SelectedValue.ToString()) || "2".Equals(cbCategory.SelectedValue.ToString()))
            {
                if (dt.Rows.Count > 0)
                {
                    priceLevel.Clear();
                    DataRow firstRow = dt.Rows[0];
                    DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                    double minCZ = (double) firstRow[6];
                    double maxCZ = (double) lastRow[6];

                    double diffCZ = (maxCZ - minCZ) / 3;
                    double leftMiddleCZ = minCZ + diffCZ;
                    double rightMiddleCZ = leftMiddleCZ + diffCZ;
                    double price = 0.0;
                
                    foreach (DataRow row in dt.Rows)
                    {
                        price = (double) row[6];
                        long siteID = (long)row[0];
                        if (price < leftMiddleCZ)
                            priceLevel.Add(siteID, PriceLevel.low);
                        else if (price >= leftMiddleCZ && price <= rightMiddleCZ)
                            priceLevel.Add(siteID, PriceLevel.middle);
                        else
                            priceLevel.Add(siteID, PriceLevel.high);
                    }
                }
            }

            lblRouCount.Text = dt.Rows.Count.ToString();
            dgwSite.DataSource = dt;
            //dgwSite.CellMouseDown += dgwSite_CellMouseDown;

        }

        private void dgwSite_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Nastavení aktuální buňky
                dgwSite.CurrentCell = dgwSite[e.ColumnIndex, e.RowIndex];
                // Zobrazení kontextového menu
                //dgwSite.Show(Cursor.Position);
            }
        }


        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitalized)
            {
                RebuildSites();
            }
                
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmAboout = new AboutBox();
            frmAboout.ShowDialog();
        }

        public void SetUser(PrivilegedUser user) 
        {
            this.privilegedUser = user;
        }

        public void MdiChild(Form mdiParent, Form mdiChild)
        {
            foreach (Form frm in mdiParent.MdiChildren)
            {
                if (frm.Name == mdiChild.Name)
                {

                    frm.Focus();
                    return;
                }
            }

            mdiChild.MdiParent = mdiParent;
            mdiChild.Show();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine("txtSearch_TextChanged");
            RebuildSites();
        }

        private void Cell_Sorted_Back(object sender, EventArgs e)
        {

        }

        private void Price_Color(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ("1".Equals(_DefaultPriceColor) && ("1".Equals(cbCategory.SelectedValue.ToString()) || "2".Equals(cbCategory.SelectedValue.ToString())))
            {
                if (dgwSite.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    long id = (long)dgwSite.Rows[e.RowIndex].Cells[0].Value;
                    if (priceLevel.ContainsKey(id))
                    {
                        if (priceLevel[id] == PriceLevel.low)
                        {
                            dgwSite.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Green;
                            dgwSite.Rows[e.RowIndex].Cells[6].Style.ForeColor = Color.White;
                        }
                        else if (priceLevel[id] == PriceLevel.middle)
                        {
                            dgwSite.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.Navy;
                            dgwSite.Rows[e.RowIndex].Cells[6].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            dgwSite.Rows[e.RowIndex].Cells[6].Style.BackColor = Color.OrangeRed;
                            dgwSite.Rows[e.RowIndex].Cells[6].Style.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        private async void btnFuelSync_Click(object sender, EventArgs e)
        {
            isSyncStarted = true;
            OperationCompleted += OnOperationCompleted;
            btnFuelSync.Enabled = false;
            progressFuelSync.Visible = true;

            AppSetting urlAppSetting = AppSetting.GetAppSetting("DEFAULT_PRICE_API");
            AppSetting lastSyncAppSetting = AppSetting.GetAppSetting("DEFAULT_LAST_SYNC_DATE_PRICE");

            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
            };


            try
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    int syncValue = 10;
                    progressFuelSync.Value = syncValue;
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("SPSPHM/1.0");
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(urlAppSetting.Value, lastSyncAppSetting.Value));

                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("Accept-Language", "en");
                    request.Headers.Add("X-DEVICE-ID", Environment.MachineName);
                    request.Headers.Add("X-TOKEN", "96da031f-d31f-4f8f-8fe3-f5e4cc52935c");
                    request.Headers.Add("X-APP-ID", "SPSPHM");
                 
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        JsonElement root = doc.RootElement;

                        // Získání základních vlastností
                        int status = root.GetProperty("status").GetInt32();
                        string msg = root.GetProperty("msg").GetString();
                        long stamp = root.GetProperty("stamp").GetInt64();

                        Console.WriteLine($"Status: {status}");
                        Console.WriteLine($"Message: {msg}");
                        Console.WriteLine($"Timestamp: {stamp}");

                        if (status == 0)
                        {
                            // Získání dat z "data" objektu
                            JsonElement data = root.GetProperty("data");
                            JsonElement pricesArray = data.GetProperty("prices");
                            int cntPrice = pricesArray.EnumerateArray().Count();
                            int tt = cntPrice / 8;

                            int i = 0;
                            foreach (JsonElement priceData in pricesArray.EnumerateArray())
                            {
                                if (i++ % tt == 0) 
                                {
                                    syncValue += 10;
                                    progressFuelSync.Value = syncValue;
                                }
                                int siteId = priceData.GetProperty("id").GetInt32();
                                JsonElement prices = priceData.GetProperty("prices");

                                foreach (JsonElement price in prices.EnumerateArray())
                                {
                                    decimal priceAmount = price.GetProperty("price").GetDecimal();
                                    int productId = price.GetProperty("id").GetInt32();
                                    int currencyId = price.GetProperty("currencyid").GetInt32();
                                    long updatedAt = price.GetProperty("updatedat").GetInt64();
                                    SitePrices.UpdatePriceProduct(siteId, productId, priceAmount, (DateTime.MinValue.UnixSecondsToDateTime(updatedAt)).ToString("yyyy-MM-dd HH:mm:ss"), privilegedUser.DisplayName);
                                }
                            }
                        }
                        AppSetting.UpdateValueByKey("DEFAULT_LAST_SYNC_DATE_PRICE", stamp.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), privilegedUser.DisplayName);
                    }
                    progressFuelSync.Value = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Vyvolání události po dokončení asynchronní operace
            OperationCompleted?.Invoke();
        }

        private void OnOperationCompleted()
        {
            isSyncStarted = false;
            btnFuelSync.Enabled = true;
            progressFuelSync.Visible = false;
            RebuildSites();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void uživateléToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tato funkcionalita není implementována", "Tato funkcionalita není implementována", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            //FrmUser frmUser = new FrmUser();
            //frmUser.ShowDialog();
        }

        private void produktyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tato funkcionalita není implementována", "Tato funkcionalita není implementována", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void vlastníciOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tato funkcionalita není implementována", "Tato funkcionalita není implementována", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void nasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tato funkcionalita není implementována", "Tato funkcionalita není implementována", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void topicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var uri = new Uri(curDirectory);
            string helpFile = Path.Combine(uri.AbsolutePath, @"Help\help.html");
            Uri helpUri = new Uri(helpFile);
            FrmHelp frmHelp = new FrmHelp();
            frmHelp.wbHelp.Url = helpUri;
            frmHelp.Show();
        }
    }
}
