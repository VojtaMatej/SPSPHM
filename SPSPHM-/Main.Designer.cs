namespace SPSPHM
{
    partial class Main
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.mnuStripmain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uživateléToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produktyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vlastníciOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nasaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tblPnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblPnlDataEntry = new System.Windows.Forms.TableLayoutPanel();
            this.lblSiteCategory = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dgwSite = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.progressFuelSync = new System.Windows.Forms.ProgressBar();
            this.lblRouCount = new System.Windows.Forms.Label();
            this.btnFuelSync = new System.Windows.Forms.Button();
            this.lblRowSelected = new System.Windows.Forms.Label();
            this.mnuStripmain.SuspendLayout();
            this.tblPnlMain.SuspendLayout();
            this.tblPnlDataEntry.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwSite)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuStripmain
            // 
            this.mnuStripmain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuStripmain.Location = new System.Drawing.Point(0, 0);
            this.mnuStripmain.Name = "mnuStripmain";
            this.mnuStripmain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnuStripmain.Size = new System.Drawing.Size(910, 24);
            this.mnuStripmain.TabIndex = 3;
            this.mnuStripmain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeDBToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "&Menu";
            // 
            // closeDBToolStripMenuItem
            // 
            this.closeDBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uživateléToolStripMenuItem,
            this.produktyToolStripMenuItem,
            this.vlastníciOMToolStripMenuItem,
            this.nasaToolStripMenuItem});
            this.closeDBToolStripMenuItem.Name = "closeDBToolStripMenuItem";
            this.closeDBToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeDBToolStripMenuItem.Text = "&Nastavení";
            // 
            // uživateléToolStripMenuItem
            // 
            this.uživateléToolStripMenuItem.Name = "uživateléToolStripMenuItem";
            this.uživateléToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uživateléToolStripMenuItem.Text = "Uživatelé";
            this.uživateléToolStripMenuItem.Click += new System.EventHandler(this.uživateléToolStripMenuItem_Click);
            // 
            // produktyToolStripMenuItem
            // 
            this.produktyToolStripMenuItem.Name = "produktyToolStripMenuItem";
            this.produktyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.produktyToolStripMenuItem.Text = "Produkty";
            this.produktyToolStripMenuItem.Click += new System.EventHandler(this.produktyToolStripMenuItem_Click);
            // 
            // vlastníciOMToolStripMenuItem
            // 
            this.vlastníciOMToolStripMenuItem.Name = "vlastníciOMToolStripMenuItem";
            this.vlastníciOMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vlastníciOMToolStripMenuItem.Text = "Vlastníci OM";
            this.vlastníciOMToolStripMenuItem.Click += new System.EventHandler(this.vlastníciOMToolStripMenuItem_Click);
            // 
            // nasaToolStripMenuItem
            // 
            this.nasaToolStripMenuItem.Name = "nasaToolStripMenuItem";
            this.nasaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nasaToolStripMenuItem.Text = "Akce OM";
            this.nasaToolStripMenuItem.Click += new System.EventHandler(this.nasaToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topicsToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.helpToolStripMenuItem.Text = "Nápověda";
            // 
            // topicsToolStripMenuItem
            // 
            this.topicsToolStripMenuItem.Name = "topicsToolStripMenuItem";
            this.topicsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.topicsToolStripMenuItem.Text = "&Nápověda";
            this.topicsToolStripMenuItem.Click += new System.EventHandler(this.topicsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.aboutToolStripMenuItem.Text = "&Informace o programu";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tblPnlMain
            // 
            this.tblPnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblPnlMain.ColumnCount = 1;
            this.tblPnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlMain.Controls.Add(this.tblPnlDataEntry, 0, 0);
            this.tblPnlMain.Controls.Add(this.dgwSite, 0, 1);
            this.tblPnlMain.Controls.Add(this.panelFooter, 0, 2);
            this.tblPnlMain.Location = new System.Drawing.Point(12, 27);
            this.tblPnlMain.Name = "tblPnlMain";
            this.tblPnlMain.RowCount = 3;
            this.tblPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblPnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblPnlMain.Size = new System.Drawing.Size(886, 701);
            this.tblPnlMain.TabIndex = 4;
            // 
            // tblPnlDataEntry
            // 
            this.tblPnlDataEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblPnlDataEntry.AutoSize = true;
            this.tblPnlDataEntry.ColumnCount = 5;
            this.tblPnlDataEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPnlDataEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.10526F));
            this.tblPnlDataEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPnlDataEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.89474F));
            this.tblPnlDataEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPnlDataEntry.Controls.Add(this.lblSiteCategory, 0, 0);
            this.tblPnlDataEntry.Controls.Add(this.lblSearch, 2, 0);
            this.tblPnlDataEntry.Controls.Add(this.txtSearch, 3, 0);
            this.tblPnlDataEntry.Controls.Add(this.tableLayoutPanel1, 3, 1);
            this.tblPnlDataEntry.Controls.Add(this.cbCategory, 1, 0);
            this.tblPnlDataEntry.Controls.Add(this.btnFilter, 4, 0);
            this.tblPnlDataEntry.Location = new System.Drawing.Point(3, 3);
            this.tblPnlDataEntry.Name = "tblPnlDataEntry";
            this.tblPnlDataEntry.RowCount = 2;
            this.tblPnlDataEntry.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPnlDataEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPnlDataEntry.Size = new System.Drawing.Size(880, 63);
            this.tblPnlDataEntry.TabIndex = 0;
            // 
            // lblSiteCategory
            // 
            this.lblSiteCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSiteCategory.AutoSize = true;
            this.lblSiteCategory.Location = new System.Drawing.Point(3, 8);
            this.lblSiteCategory.Name = "lblSiteCategory";
            this.lblSiteCategory.Size = new System.Drawing.Size(45, 13);
            this.lblSiteCategory.TabIndex = 0;
            this.lblSiteCategory.Text = "Typ OM";
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(366, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(48, 13);
            this.lblSearch.TabIndex = 3;
            this.lblSearch.Text = "Vyhledat";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(420, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(424, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(644, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 28);
            this.tableLayoutPanel1.TabIndex = 12;
            this.tableLayoutPanel1.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(41, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.Location = new System.Drawing.Point(122, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 22);
            this.button3.TabIndex = 1;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cbCategory
            // 
            this.cbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(54, 3);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(306, 21);
            this.cbCategory.TabIndex = 13;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.AutoSize = true;
            this.btnFilter.Image = global::SPSPHM.Properties.Resources.icons8_menu_vertical_16;
            this.btnFilter.Location = new System.Drawing.Point(850, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(26, 23);
            this.btnFilter.TabIndex = 14;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dgwSite
            // 
            this.dgwSite.AllowUserToAddRows = false;
            this.dgwSite.AllowUserToDeleteRows = false;
            this.dgwSite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwSite.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgwSite.Location = new System.Drawing.Point(3, 72);
            this.dgwSite.MultiSelect = false;
            this.dgwSite.Name = "dgwSite";
            this.dgwSite.ReadOnly = true;
            this.dgwSite.Size = new System.Drawing.Size(880, 590);
            this.dgwSite.TabIndex = 3;
            this.dgwSite.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Price_Color);
            this.dgwSite.Sorted += new System.EventHandler(this.Cell_Sorted_Back);
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.progressFuelSync);
            this.panelFooter.Controls.Add(this.lblRouCount);
            this.panelFooter.Controls.Add(this.btnFuelSync);
            this.panelFooter.Controls.Add(this.lblRowSelected);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFooter.Location = new System.Drawing.Point(3, 668);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(880, 30);
            this.panelFooter.TabIndex = 4;
            // 
            // progressFuelSync
            // 
            this.progressFuelSync.Location = new System.Drawing.Point(586, 4);
            this.progressFuelSync.Name = "progressFuelSync";
            this.progressFuelSync.Size = new System.Drawing.Size(197, 23);
            this.progressFuelSync.TabIndex = 3;
            this.progressFuelSync.Visible = false;
            // 
            // lblRouCount
            // 
            this.lblRouCount.AutoSize = true;
            this.lblRouCount.Location = new System.Drawing.Point(51, 9);
            this.lblRouCount.Name = "lblRouCount";
            this.lblRouCount.Size = new System.Drawing.Size(13, 13);
            this.lblRouCount.TabIndex = 2;
            this.lblRouCount.Text = "0";
            // 
            // btnFuelSync
            // 
            this.btnFuelSync.Location = new System.Drawing.Point(789, 4);
            this.btnFuelSync.Name = "btnFuelSync";
            this.btnFuelSync.Size = new System.Drawing.Size(88, 23);
            this.btnFuelSync.TabIndex = 1;
            this.btnFuelSync.Text = "Aktualizace dat";
            this.btnFuelSync.UseVisualStyleBackColor = true;
            this.btnFuelSync.Click += new System.EventHandler(this.btnFuelSync_Click);
            // 
            // lblRowSelected
            // 
            this.lblRowSelected.AutoSize = true;
            this.lblRowSelected.Location = new System.Drawing.Point(3, 9);
            this.lblRowSelected.Name = "lblRowSelected";
            this.lblRowSelected.Size = new System.Drawing.Size(49, 13);
            this.lblRowSelected.TabIndex = 0;
            this.lblRowSelected.Text = "Vybráno:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 732);
            this.Controls.Add(this.tblPnlMain);
            this.Controls.Add(this.mnuStripmain);
            this.MinimumSize = new System.Drawing.Size(677, 343);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPSPHM";
            this.mnuStripmain.ResumeLayout(false);
            this.mnuStripmain.PerformLayout();
            this.tblPnlMain.ResumeLayout(false);
            this.tblPnlMain.PerformLayout();
            this.tblPnlDataEntry.ResumeLayout(false);
            this.tblPnlDataEntry.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwSite)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mnuStripmain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tblPnlMain;
        private System.Windows.Forms.TableLayoutPanel tblPnlDataEntry;
        private System.Windows.Forms.Label lblSiteCategory;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dgwSite;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ToolStripMenuItem uživateléToolStripMenuItem;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnFuelSync;
        private System.Windows.Forms.Label lblRowSelected;
        private System.Windows.Forms.Label lblRouCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ProgressBar progressFuelSync;
        private System.Windows.Forms.ToolStripMenuItem produktyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vlastníciOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nasaToolStripMenuItem;
    }
}

