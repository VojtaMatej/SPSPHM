namespace SPSPHM
{
    partial class FrmHelp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbHelp = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbHelp
            // 
            this.wbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbHelp.Location = new System.Drawing.Point(0, 0);
            this.wbHelp.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbHelp.Name = "wbHelp";
            this.wbHelp.Size = new System.Drawing.Size(800, 450);
            this.wbHelp.TabIndex = 0;
            this.wbHelp.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // FrmHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wbHelp);
            this.Name = "FrmHelp";
            this.Text = "Nápověda";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser wbHelp;
    }
}