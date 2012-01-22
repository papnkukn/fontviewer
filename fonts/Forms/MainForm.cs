using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Oxage.Fonts
{
	public partial class MainForm : Form
	{
		#region Members
		private string currentFontDirectory;
		private PropertiesForm frmProperties;
		private SaveFileDialog saveFileDialog;
		private FontCollection fonts;
		private FontLister fl;
		#endregion

		#region Constructors
		public MainForm() : this(null)
		{
		}

		public MainForm(string directory)
		{
			InitializeComponent();

			fonts = new FontCollection();

			saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Save image";
			saveFileDialog.Filter = "PNG (*.png)|*.png";
			saveFileDialog.FileName = "font-preview.png";
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
			{
				//Directory: %windir%\fonts
				currentFontDirectory = Path.Combine(Environment.GetEnvironmentVariable("windir"), "Fonts");
			}
			else
			{
				//Load from user defined directory
				currentFontDirectory = directory;
			}

			fl = new FontLister();
			fl.LoadFontsFromDirectoryCompleted += new EventHandler<FontListerEventArgs>(delegate(object sender, FontListerEventArgs e)
			{
				this.Status = e.Collection.Count + " fonts loaded.";
				this.fonts = e.Collection;

				if (fonts != null && fonts.Count > 0)
				{
					lstFonts.Enabled = true;
					lstFonts.SelectedIndex = 0;

					RefreshFontList();

					//Select Arial by default
					foreach (FontModel item in lstFonts.Items)
					{
						if (item.Name == "Arial")
						{
							lstFonts.SelectedItem = item;
							break;
						}
					}
				}
				else
				{
					lstFonts.DataSource = null;
					lstFonts.Items.Clear();
					lstFonts.Items.Add("No font found in the directory.");
					lstFonts.SelectedItem = null;
					lstFonts.Enabled = false;

					this.Status = "No font found in directory " + currentFontDirectory;
				}
			});
			fl.LoadFontsFromDirectoryAsync(currentFontDirectory, true);
			SetLoadingState();
		}
		#endregion

		#region Properties
		public string Status
		{
			get
			{
				return lblStatus.Text;
			}
			set
			{
				lblStatus.Text = value;
			}
		}

		public FontModel SelectedFont
		{
			get
			{
				return lstFonts.SelectedItem as FontModel;
			}
		}

		public float FontSizeValue
		{
			get
			{
				float result = 24;
				string raw = cmbFontSize.Text;
				if (string.IsNullOrEmpty(raw))
				{
					cmbFontSize.Text = "24 px";
					result = 24;
				}
				else
				{
					float tmp = 0;
					Match match = Regex.Match(raw, @"([\d\.,]+)", RegexOptions.IgnoreCase);
					if (match.Success && float.TryParse(match.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out tmp))
					{
						result = tmp;
					}
				}
				return result;
			}
			set
			{
				string size = value.ToString("N0");

				string unit = null;
				switch (this.FontSizeUnits)
				{
					case GraphicsUnit.Pixel:
						unit = "px";
						break;

					case GraphicsUnit.Point:
						unit = "pt";
						break;

					case GraphicsUnit.Millimeter:
						unit = "mm";
						break;

					case GraphicsUnit.Inch:
						unit = "in";
						break;
				}

				cmbFontSize.Text = size + (unit != null ? " " + unit : null);
			}
		}

		public GraphicsUnit FontSizeUnits
		{
			get
			{
				string raw = cmbFontSize.Text;
				if (string.IsNullOrEmpty(raw))
				{
					cmbFontSize.Text = "24 px";
					return GraphicsUnit.Pixel;
				}
				else
				{
					Match match = Regex.Match(raw, @"([\d\.,]+)\s*(pt|px|mm|in)", RegexOptions.IgnoreCase);
					if (match.Success && match.Groups.Count == 3)
					{
						switch (match.Groups[2].Value.ToLower())
						{
							case "pt":
								return GraphicsUnit.Point;

							default:
							case "px":
								return GraphicsUnit.Pixel;

							case "mm":
								return GraphicsUnit.Millimeter;

							case "in":
								return GraphicsUnit.Inch;
						}
					}
				}
				return GraphicsUnit.Point;
			}
		}
		#endregion

		#region Methods
		protected void RefreshFontList()
		{
			lstFonts.DataSource = null;
			lstFonts.DataSource = this.fonts;

			if (lstFonts.Items.Count > 0)
			{
				lstFonts.SelectedIndex = 0;
			}
		}

		protected void RefreshFontPreview()
		{
			//Build font by given parameters
			Font font = GetFont();

			//Refresh preview box
			if (font != null)
			{
				txtSampleText.ForeColor = SystemColors.WindowText;
				txtSampleText.Font = font;
				this.Status = this.SelectedFont.Path;
			}
			else
			{
				txtSampleText.ForeColor = Color.Red;
				txtSampleText.Font = new Font(new FontFamily("Arial"), this.FontSizeValue, this.FontSizeUnits);
				this.Status = "This font style is not available.";
			}

			//Refresh properties
			if (frmProperties != null)
			{
				frmProperties.LoadFont(font, this.SelectedFont);
			}
		}

		protected Font GetFont()
		{
			FontModel item = this.SelectedFont;

			//Font style
			FontStyle style = FontStyle.Regular;
			if (chbBold.Checked) style |= FontStyle.Bold;
			if (chbItalic.Checked) style |= FontStyle.Italic;
			if (chbUnderline.Checked) style |= FontStyle.Underline;
			//if (chbStrikeout.Checked) style |= FontStyle.Strikeout;

			try
			{
				//Try to load font with the specified style
				Font font = new Font(item.Font, this.FontSizeValue, style, this.FontSizeUnits);
				return font;
			}
			catch
			{
				try
				{
					//Try load font with the default style
					Font font = new Font(item.Font, this.FontSizeValue, this.FontSizeUnits);
					return font;
				}
				catch
				{
					try
					{
						//Finally try the default size
						Font font = new Font(item.Font, 24, GraphicsUnit.Pixel);
						return font;
					}
					catch
					{
					}
				}
			}

			//Font cannot be loaded
			return null;
		}

		protected void Filter(string text)
		{
			if (string.IsNullOrEmpty(text) || text.Trim().Length == 0)
			{
				//Display all fonts, no filtering
				lstFonts.SelectedItem = null;
				lstFonts.DataSource = null;
				lstFonts.DataSource = this.fonts;
			}
			else
			{
				//Search by font family name
				FontCollection collection = new FontCollection();
				foreach (FontModel model in this.fonts)
				{
					if (model.Name.ToLower().Contains(text.ToLower()))
					{
						collection.Add(model);
					}
				}

				lstFonts.SelectedItem = null;
				lstFonts.DataSource = null;
				lstFonts.DataSource = collection;
			}
		}

		protected void Browse()
		{
			//TODO: Replace with more friendly dialog where user can copy-paste directory
			//FolderBrowserDialog dialog = new FolderBrowserDialog();
			//dialog.Description = "Select a folder with fonts. Folder should contain *.ttf or *.otf files.";
			//dialog.SelectedPath = currentFontDirectory;

			//Reference: http://stackoverflow.com/questions/45988/choosing-a-folder-with-net-3-5
			Ionic.Utils.FolderBrowserDialogEx dialog = new Ionic.Utils.FolderBrowserDialogEx();
			dialog.Description = "Select a folder with fonts.";
			dialog.ShowNewFolderButton = true;
			dialog.ShowEditBox = true;
			dialog.SelectedPath = currentFontDirectory;
			dialog.ShowFullPathInEditBox = true;
			//dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					this.currentFontDirectory = dialog.SelectedPath;
					fl.LoadFontsFromDirectoryAsync(dialog.SelectedPath, true);
					SetLoadingState();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		protected void Export()
		{
			try
			{
				if (this.fonts == null || this.fonts.Families == null || this.fonts.Families.Length == 0)
				{
					if (string.IsNullOrEmpty(currentFontDirectory) || currentFontDirectory.Trim().Length == 0)
					{
						MessageBox.Show("Please, select a directory with fonts.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					this.fonts = fl.LoadFontsFromDirectory(currentFontDirectory, true);
					RefreshFontList();
				}

				if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					string path = this.saveFileDialog.FileName;
					Bitmap bmp = fl.CreateFontList(txtSampleText.Text, this.fonts.Collection, this.FontSizeValue, this.FontSizeUnits, chbBold.Checked, chbItalic.Checked, chbUnderline.Checked, false /* chbStrikeout.Checked */);
					bmp.Save(path, ImageFormat.Png);

					//bool showPreview = chbShowPreview.Checked;
					const bool showPreview = true;
					if (showPreview && File.Exists(path))
					{
						//Open in default image viewer
						Process.Start(path);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		protected bool FollowKeyboardShortcut(KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift)
			{
				switch (e.KeyCode)
				{
					//Find - focus filter
					case Keys.F:
						txtFilter.Focus();
						return true;

					//Edit - focus text editor
					case Keys.E:
						txtSampleText.Focus();
						return true;

					//Focus list of fonts
					case Keys.W:
						lstFonts.Focus();
						return true;

					//Open font directory
					case Keys.O:
						Browse();
						return true;

					//Save image
					case Keys.S:
						Export();
						return true;

					//Toggle bold
					case Keys.B:
						chbBold.Checked ^= true;
						return true;

					//Toggle italic
					case Keys.I:
						chbItalic.Checked ^= true;
						return true;

					//Toggle underline
					case Keys.U:
						chbUnderline.Checked ^= true;
						return true;

					//Increase font size
					case Keys.Add:
						this.FontSizeValue = 1.1f * this.FontSizeValue; //10% increase
						return true;

					//Decrease font size
					case Keys.Subtract:
						this.FontSizeValue = 0.9f * this.FontSizeValue; //10% decrease
						return true;
				}
			}

			return false;
		}

		protected void SetLoadingState()
		{
			lstFonts.DataSource = null;
			lstFonts.Items.Clear();
			lstFonts.Items.Add("Loading fonts, please wait...");
			lstFonts.Enabled = false;

			this.Status = "Loading fonts, please wait...";
		}
		#endregion

		#region Event handlers
		private void MainForm_Resize(object sender, EventArgs e)
		{
			lstFonts.Height = this.ClientSize.Height - 85;
			txtSampleText.Width = this.ClientSize.Width - 248;
			txtSampleText.Height = this.ClientSize.Height - 61;
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			Browse();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			Export();
		}

		private void lstFonts_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshFontPreview();
		}

		private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshFontPreview();
		}

		private void cmbFontSize_TextChanged(object sender, EventArgs e)
		{
			RefreshFontPreview();
		}

		private void chbBold_CheckedChanged(object sender, EventArgs e)
		{
			RefreshFontPreview();
		}

		private void chbItalic_CheckedChanged(object sender, EventArgs e)
		{
			RefreshFontPreview();
		}

		private void chbUnderline_CheckedChanged(object sender, EventArgs e)
		{
			RefreshFontPreview();
		}

		private void btnProperties_Click(object sender, EventArgs e)
		{
			if (frmProperties == null)
			{
				frmProperties = new PropertiesForm();
				frmProperties.FormClosed += new FormClosedEventHandler(delegate
				{
					frmProperties = null;
				});
				frmProperties.LoadFont(GetFont(), this.SelectedFont);
				frmProperties.Show();
			}
			else
			{
				frmProperties.BringToFront();
			}
		}

		private void txtFilter_Enter(object sender, EventArgs e)
		{
			//Hide watermark
			lblFilterWatermark.Visible = false;
		}

		private void txtFilter_Leave(object sender, EventArgs e)
		{
			//Display watermark only if filter TextBox is empty
			lblFilterWatermark.Visible = string.IsNullOrEmpty(txtFilter.Text);
		}

		private void txtFilter_TextChanged(object sender, EventArgs e)
		{
			Filter(txtFilter.Text);
		}

		private void lblFilterWatermark_Click(object sender, EventArgs e)
		{
			//Click on watermark is the same as click on filter TextBox
			txtFilter.Focus();
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = FollowKeyboardShortcut(e);
		}

		private void lstFonts_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = FollowKeyboardShortcut(e);
		}

		private void lstFonts_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Workaround to prevent auto jump on CTRL+B press or similar action
			e.Handled = true;
		}

		private void txtFilter_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Up:
					//UX: Select previous font
					if (lstFonts.Enabled)
					{
						int up = lstFonts.SelectedIndex - 1;
						if (up >= 0 && up < lstFonts.Items.Count)
						{
							lstFonts.SelectedIndex = up;
						}
					}
					break;

				case Keys.Down:
					//UX: Select next font
					if (lstFonts.Enabled)
					{
						int down = lstFonts.SelectedIndex + 1;
						if (down >= 0 && down < lstFonts.Items.Count)
						{
							lstFonts.SelectedIndex = down;
						}
					}
					break;

				default:
					e.Handled = FollowKeyboardShortcut(e);
					break;
			}
		}

		private bool ctrl = false;
		private void txtSampleText_KeyDown(object sender, KeyEventArgs e)
		{
			ctrl = e.Control && !e.Alt && !e.Shift;
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
				txtSampleText.SelectAll();
			}
			else
			{
				e.Handled = FollowKeyboardShortcut(e);
			}
		}

		private void txtSampleText_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Workaround to prevent inserting 'tab' when CTRL+I is pressed, CTRL+I is reserved to toggle italic font
			e.Handled = ctrl && e.KeyChar == '\t';
		}

		private void toolbar_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = FollowKeyboardShortcut(e);
		}
		#endregion
	}
}
