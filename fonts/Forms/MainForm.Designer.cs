namespace Oxage.Fonts
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.lstFonts = new System.Windows.Forms.ListBox();
			this.txtSampleText = new System.Windows.Forms.TextBox();
			this.toolbar = new System.Windows.Forms.ToolStrip();
			this.btnBrowse = new System.Windows.Forms.ToolStripButton();
			this.btnExport = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.chbBold = new System.Windows.Forms.ToolStripButton();
			this.chbItalic = new System.Windows.Forms.ToolStripButton();
			this.chbUnderline = new System.Windows.Forms.ToolStripButton();
			this.cmbFontSize = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnProperties = new System.Windows.Forms.ToolStripButton();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.lblFilterWatermark = new System.Windows.Forms.Label();
			this.status = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolbar.SuspendLayout();
			this.status.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstFonts
			// 
			this.lstFonts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstFonts.FormattingEnabled = true;
			this.lstFonts.Location = new System.Drawing.Point(9, 57);
			this.lstFonts.Name = "lstFonts";
			this.lstFonts.Size = new System.Drawing.Size(220, 483);
			this.lstFonts.TabIndex = 2;
			this.lstFonts.SelectedIndexChanged += new System.EventHandler(this.lstFonts_SelectedIndexChanged);
			this.lstFonts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFonts_KeyDown);
			this.lstFonts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstFonts_KeyPress);
			// 
			// txtSampleText
			// 
			this.txtSampleText.BackColor = System.Drawing.SystemColors.Window;
			this.txtSampleText.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
			this.txtSampleText.Location = new System.Drawing.Point(239, 31);
			this.txtSampleText.Multiline = true;
			this.txtSampleText.Name = "txtSampleText";
			this.txtSampleText.Size = new System.Drawing.Size(544, 509);
			this.txtSampleText.TabIndex = 3;
			this.txtSampleText.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\r\nabcdefghijklmnopqrstuvwxyz\r\n1234567890\r\n\r\n/\\|!?%$&()[" +
    "]{}<>+-~=*@;:,._\r\n\r\nLorem ipsum dolor sit amet";
			this.txtSampleText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSampleText_KeyDown);
			this.txtSampleText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSampleText_KeyPress);
			// 
			// toolbar
			// 
			this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBrowse,
            this.btnExport,
            this.toolStripSeparator1,
            this.chbBold,
            this.chbItalic,
            this.chbUnderline,
            this.cmbFontSize,
            this.toolStripSeparator2,
            this.btnProperties});
			this.toolbar.Location = new System.Drawing.Point(0, 0);
			this.toolbar.Name = "toolbar";
			this.toolbar.Size = new System.Drawing.Size(792, 25);
			this.toolbar.TabIndex = 90;
			this.toolbar.Text = "toolStrip1";
			this.toolbar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolbar_KeyDown);
			// 
			// btnBrowse
			// 
			this.btnBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnBrowse.Image = global::Oxage.Fonts.Properties.Resources.open;
			this.btnBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(23, 22);
			this.btnBrowse.Text = "Browse for fonts";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// btnExport
			// 
			this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnExport.Image = global::Oxage.Fonts.Properties.Resources.image;
			this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(23, 22);
			this.btnExport.Text = "Export image (generate preview with all fonts)";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// chbBold
			// 
			this.chbBold.CheckOnClick = true;
			this.chbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.chbBold.Image = global::Oxage.Fonts.Properties.Resources.bold;
			this.chbBold.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.chbBold.Name = "chbBold";
			this.chbBold.Size = new System.Drawing.Size(23, 22);
			this.chbBold.Text = "Bold";
			this.chbBold.CheckedChanged += new System.EventHandler(this.chbBold_CheckedChanged);
			// 
			// chbItalic
			// 
			this.chbItalic.CheckOnClick = true;
			this.chbItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.chbItalic.Image = global::Oxage.Fonts.Properties.Resources.italic;
			this.chbItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.chbItalic.Name = "chbItalic";
			this.chbItalic.Size = new System.Drawing.Size(23, 22);
			this.chbItalic.Text = "Italic";
			this.chbItalic.CheckedChanged += new System.EventHandler(this.chbItalic_CheckedChanged);
			// 
			// chbUnderline
			// 
			this.chbUnderline.CheckOnClick = true;
			this.chbUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.chbUnderline.Image = global::Oxage.Fonts.Properties.Resources.underline;
			this.chbUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.chbUnderline.Name = "chbUnderline";
			this.chbUnderline.Size = new System.Drawing.Size(23, 22);
			this.chbUnderline.Text = "Underline";
			this.chbUnderline.CheckedChanged += new System.EventHandler(this.chbUnderline_CheckedChanged);
			// 
			// cmbFontSize
			// 
			this.cmbFontSize.Items.AddRange(new object[] {
            "8 px",
            "9 px",
            "10 px",
            "11 px",
            "12 px",
            "14 px",
            "16 px",
            "18 px",
            "20 px",
            "22 px",
            "24 px",
            "26 px",
            "28 px",
            "36 px",
            "48 px",
            "72 px"});
			this.cmbFontSize.Name = "cmbFontSize";
			this.cmbFontSize.Size = new System.Drawing.Size(75, 25);
			this.cmbFontSize.Text = "24 px";
			this.cmbFontSize.TextChanged += new System.EventHandler(this.cmbFontSize_TextChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnProperties
			// 
			this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnProperties.Image = global::Oxage.Fonts.Properties.Resources.metrics;
			this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.Size = new System.Drawing.Size(23, 22);
			this.btnProperties.Text = "Font Properties";
			this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(9, 31);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(220, 20);
			this.txtFilter.TabIndex = 1;
			this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
			this.txtFilter.Enter += new System.EventHandler(this.txtFilter_Enter);
			this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
			this.txtFilter.Leave += new System.EventHandler(this.txtFilter_Leave);
			// 
			// lblFilterWatermark
			// 
			this.lblFilterWatermark.AutoSize = true;
			this.lblFilterWatermark.BackColor = System.Drawing.SystemColors.Window;
			this.lblFilterWatermark.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lblFilterWatermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblFilterWatermark.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.lblFilterWatermark.Location = new System.Drawing.Point(12, 34);
			this.lblFilterWatermark.Name = "lblFilterWatermark";
			this.lblFilterWatermark.Size = new System.Drawing.Size(101, 13);
			this.lblFilterWatermark.TabIndex = 53;
			this.lblFilterWatermark.Text = "Filter fonts, e.g. arial";
			this.lblFilterWatermark.Click += new System.EventHandler(this.lblFilterWatermark_Click);
			// 
			// status
			// 
			this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.status.Location = new System.Drawing.Point(0, 549);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(792, 22);
			this.status.TabIndex = 54;
			this.status.Text = "statusStrip1";
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(48, 17);
			this.lblStatus.Text = "Ready...";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 571);
			this.Controls.Add(this.status);
			this.Controls.Add(this.lblFilterWatermark);
			this.Controls.Add(this.txtFilter);
			this.Controls.Add(this.toolbar);
			this.Controls.Add(this.txtSampleText);
			this.Controls.Add(this.lstFonts);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(480, 240);
			this.Name = "MainForm";
			this.Text = "Font Viewer 1.1";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.toolbar.ResumeLayout(false);
			this.toolbar.PerformLayout();
			this.status.ResumeLayout(false);
			this.status.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstFonts;
		private System.Windows.Forms.TextBox txtSampleText;
		private System.Windows.Forms.ToolStrip toolbar;
		private System.Windows.Forms.ToolStripButton btnBrowse;
		private System.Windows.Forms.ToolStripButton btnExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton chbBold;
		private System.Windows.Forms.ToolStripButton chbItalic;
		private System.Windows.Forms.ToolStripButton chbUnderline;
		private System.Windows.Forms.ToolStripComboBox cmbFontSize;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton btnProperties;
		private System.Windows.Forms.Label lblFilterWatermark;
		private System.Windows.Forms.StatusStrip status;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
	}
}

