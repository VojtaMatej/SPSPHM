using SPSPHM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPSPHM
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtUserName.Text;
            string pwd = txtPassword.Text;

            PrivilegedUser privilegedUser = PrivilegedUser.GetByLoginname(login);

            if (privilegedUser != null && User.VerifyPassword(privilegedUser.Password, pwd))
            {
                var frmMain = new Main(privilegedUser);
                frmMain.Show();
                this.Hide();
                frmMain.FormClosing += FrmMain_FormClosing;
            }
            else
            {
                MessageBox.Show("Neplatné přihlašovací údaje", "Zpráva", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.txtUserName.Clear();
            this.txtPassword.Clear();
            this.Show();
        }
    }
}
