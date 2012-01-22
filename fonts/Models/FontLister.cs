using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace Oxage.Fonts
{
	public class FontLister
	{
		/// <summary>
		/// Draws a text for each font in private font collection.
		/// </summary>
		/// <param name="sampleText">Text to be drawn.</param>
		/// <param name="fonts">List of fonts.</param>
		/// <param name="fontSize">Font size in the defined units.</param>
		/// <returns>Returns a bitmap image with all drawn text variants.</returns>
		public Bitmap CreateFontList(string sampleText, PrivateFontCollection fonts, float fontSize, GraphicsUnit fontSizeUnits, bool bold, bool italic, bool underline, bool strikeout)
		{
			SolidBrush brush = new SolidBrush(Color.Black);
			float x = 0;
			float y = 0;

			if (string.IsNullOrEmpty(sampleText))
			{
				sampleText = "Lorem Ipsum";
			}

			//Font style
			FontStyle style = FontStyle.Regular;
			if (bold) style |= FontStyle.Bold;
			if (italic) style |= FontStyle.Italic;
			if (underline) style |= FontStyle.Underline;
			if (strikeout) style |= FontStyle.Strikeout;

			//Font for font name
			Font normal = new Font("Arial", 12);
			Brush left = new SolidBrush(Color.Brown);

			//Predicted dimensions
			//int width = (int)(1.2 * fontSize * sampleText.Length); //20% just for sure
			//int height = (int)(1.3 * fontSize * fonts.Families.Length); //30% spacing
			int width = 0;
			int height = 0;
			int spacing = (int)(1.1 * fontSize);

			//Get maximum width and total hight
			foreach (var f in fonts.Families)
			{
				try
				{
					Font font = new Font(f, fontSize, style, fontSizeUnits);
					Size size = TextRenderer.MeasureText(sampleText, font);
					width = Math.Max(width, size.Width);
					height += (size.Height > 12 ? size.Height : 12) + spacing; //12 px is minimum height per font

					//Max width of font name rendered with normal font
					size = TextRenderer.MeasureText(f.Name, normal);
					x = Math.Max(x, size.Width + spacing);
				}
				catch
				{
					//Font does not support 'regular' style, size, etc.
				}
			}

			width = (int)(1.2 * width); //20% larger, just for sure

			Bitmap bmp = new Bitmap(width, height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				//White background
				g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));

				foreach (var f in fonts.Families)
				{
					try
					{
						Font font = new Font(f, fontSize, style, fontSizeUnits);
						Size size = TextRenderer.MeasureText(sampleText, font);
						g.DrawString(f.Name, normal, left, 0, y);
						g.DrawString(sampleText, font, brush, x, y);
						y += size.Height + spacing;
					}
					catch
					{
						//Font does not support 'regular' style, size, etc.
					}
				}
			}

			return bmp;
		}

		/// <summary>
		/// Loads fonts from files into a private font collection (i.e. not installed as system fonts).
		/// </summary>
		/// <param name="fontDirectory">Directory with *.ttf and *.otf files.</param>
		/// <param name="includeSubdirectories">A value indicating whether to include subdirectories for font search.</param>
		/// <returns>Returns a private collection of fons.</returns>
		public FontCollection LoadFontsFromDirectory(string fontDirectory, bool includeSubdirectories)
		{
			if (string.IsNullOrEmpty(fontDirectory))
			{
				throw new ArgumentException("fontDirectory must be specified!");
			}

			if (!Directory.Exists(fontDirectory))
			{
				throw new DirectoryNotFoundException("Directory does not exist! " + fontDirectory);
			}

			//Font from file: http://www.codeguru.com/forum/showthread.php?t=302191
			FontCollection result = new FontCollection();
			SearchOption options = (includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			string[] fontFiles = Directory.GetFiles(fontDirectory, "*.ttf", options); //TrueType fonts

			//Load fonts to the collection
			foreach (string fontFile in fontFiles)
			{
				try
				{
					result.AddFontFile(fontFile);
				}
				catch
				{
					//Not a font file, corrupted font file, file does not exist, etc.
				}
			}

			//Do the same for OpenType fonts
			fontFiles = Directory.GetFiles(fontDirectory, "*.otf", options);
			foreach (string fontFile in fontFiles)
			{
				try
				{
					result.AddFontFile(fontFile);
				}
				catch
				{
					//Not a font file, corrupted font file, file does not exist, etc.
				}
			}

			return result;
		}

		public void LoadFontsFromDirectoryAsync(string fontDirectory, bool includeSubdirectories)
		{
			FontCollection collection = null;
			BackgroundWorker worker = new BackgroundWorker();
			worker.DoWork += new DoWorkEventHandler(delegate(object sender, DoWorkEventArgs e)
			{
				collection = LoadFontsFromDirectory(fontDirectory, includeSubdirectories);
			});
			worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				OnLoadFontsFromDirectoryCompleted(collection);
			});
			worker.RunWorkerAsync();
		}

		public event EventHandler<FontListerEventArgs> LoadFontsFromDirectoryCompleted;
		protected virtual void OnLoadFontsFromDirectoryCompleted(FontCollection collection)
		{
			if (LoadFontsFromDirectoryCompleted != null)
			{
				LoadFontsFromDirectoryCompleted(this, new FontListerEventArgs() { Collection = collection });
			}
		}
	}

	public class FontListerEventArgs : EventArgs
	{
		public FontCollection Collection
		{
			get;
			set;
		}
	}
}
