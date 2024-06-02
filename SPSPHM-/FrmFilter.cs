using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SPSPHM
{
    public partial class FrmFilter : Form
    {
        public Main.PassDataDelegate PassData;

        public FrmFilter()
        {
            InitializeComponent();

        }

        public void SetCountryDataTable(DataTable dtCountry, string countryValue)
        {
            cbCountry.DataSource = dtCountry;
            cbCountry.DisplayMember = "description";
            cbCountry.ValueMember = "id";
            cbCountry.SelectedValue = countryValue;
        }

        public void SetProductDataTable(DataTable dtProduct, string productValue)
        {
            cbProduct.DataSource = dtProduct;
            cbProduct.DisplayMember = "description";
            cbProduct.ValueMember = "id";
            cbProduct.SelectedValue = productValue;
        }

        public void SetPriceColor(String priceColor)
        {
            if ("1".Equals(priceColor)) chkColor.Checked = true;
        }

        public void SetPriceMinMax(String priceMinMax)
        {
            string[] _priceMinMax = priceMinMax.Split(':');

            if (_priceMinMax.Length == 2)
            {
                numMin.Value = decimal.Parse(_priceMinMax[0], CultureInfo.InvariantCulture);
                numMax.Value = decimal.Parse(_priceMinMax[1], CultureInfo.InvariantCulture);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (PassData != null)
            {
                // Pass the data from a textbox to the delegate
                PassData(cbCountry.SelectedValue.ToString()
                    ,cbProduct.SelectedValue.ToString()
                    ,(chkColor.Checked ? "1" : "0")
                    ,String.Format("{0}:{1}", numMin.Value.ToString(System.Globalization.CultureInfo.InvariantCulture),numMax.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)));
            }
            this.Close();
        }
    }
}
