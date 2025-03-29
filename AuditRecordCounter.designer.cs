
namespace Sdmsols.XTB.AuditRecordCounterByTable
{
    partial class AuditRecordCounterByTable
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditRecordCounterByTable));
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbAudit = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslAbout = new System.Windows.Forms.ToolStripLabel();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.auditGridView = new System.Windows.Forms.DataGridView();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.auditGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAudit,
            this.tssSeparator1,
            this.tsbExportToExcel,
            this.toolStripSeparator1,
            this.tslAbout,
            this.tsbClose});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1025, 31);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbAudit
            // 
            this.tsbAudit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAudit.Image = ((System.Drawing.Image)(resources.GetObject("tsbAudit.Image")));
            this.tsbAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAudit.Name = "tsbAudit";
            this.tsbAudit.Size = new System.Drawing.Size(239, 28);
            this.tsbAudit.Text = "Load Audit Record Count By Table";
            this.tsbAudit.Click += new System.EventHandler(this.tsbAudit_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbExportToExcel
            // 
            this.tsbExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportToExcel.Image")));
            this.tsbExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportToExcel.Name = "tsbExportToExcel";
            this.tsbExportToExcel.Size = new System.Drawing.Size(299, 28);
            this.tsbExportToExcel.Text = "Export Audit Record Count By Table To CSV";
            this.tsbExportToExcel.Click += new System.EventHandler(this.tsbExportToExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tslAbout
            // 
            this.tslAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslAbout.IsLink = true;
            this.tslAbout.Name = "tslAbout";
            this.tslAbout.Size = new System.Drawing.Size(88, 28);
            this.tslAbout.Text = "by MayankP";
            this.tslAbout.Click += new System.EventHandler(this.tslAbout_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(107, 28);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // auditGridView
            // 
            this.auditGridView.AllowUserToAddRows = false;
            this.auditGridView.AllowUserToDeleteRows = false;
            this.auditGridView.AllowUserToOrderColumns = true;
            this.auditGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.auditGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auditGridView.Location = new System.Drawing.Point(0, 31);
            this.auditGridView.Name = "auditGridView";
            this.auditGridView.ReadOnly = true;
            this.auditGridView.RowHeadersWidth = 51;
            this.auditGridView.RowTemplate.Height = 24;
            this.auditGridView.Size = new System.Drawing.Size(1025, 650);
            this.auditGridView.TabIndex = 38;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1019, 480);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 8);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1025, 36);
            this.progressBar.TabIndex = 26;
            this.progressBar.Visible = false;
            // 
            // StatusText
            // 
            this.StatusText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusText.Location = new System.Drawing.Point(3, 74);
            this.StatusText.Margin = new System.Windows.Forms.Padding(4);
            this.StatusText.Multiline = true;
            this.StatusText.Name = "StatusText";
            this.StatusText.ReadOnly = true;
            this.StatusText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusText.Size = new System.Drawing.Size(1019, 42);
            this.StatusText.TabIndex = 27;
            // 
            // AuditRecordCounterByTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.auditGridView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AuditRecordCounterByTable";
            this.Size = new System.Drawing.Size(1025, 681);
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.AuditRecordCounterByTable_ConnectionUpdated);
            this.Load += new System.EventHandler(this.AuditRecordCounterByTable_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.auditGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ToolStripLabel tslAbout;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView auditGridView;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.ToolStripButton tsbAudit;
        private System.Windows.Forms.ToolStripButton tsbExportToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox StatusText;
       
    }
}
