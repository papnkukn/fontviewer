using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Oxage.Fonts
{
	public partial class PropertiesForm : Form
	{
		public PropertiesForm()
		{
			InitializeComponent();
		}

		public void LoadFont(Font font, FontModel model)
		{
			PropertiesModel prop = new PropertiesModel();

			if (model != null)
			{
				prop.Name = model.Name;
				prop.Path = model.Path;
				prop.FontType = GetFontType(model.Path);
			}

			if (font != null)
			{
				try
				{
					string unit = GetUnitName(font.Unit);
					if (unit != null) unit = " " + unit;

					//Properties
					prop.Name = font.Name;

					//Font Style
					prop.Bold = font.Bold;
					prop.Italic = font.Italic;
					prop.Underline = font.Underline;
					prop.Strikethrough = font.Strikeout;
					prop.Size = font.Size + unit;
					prop.LineHeight = font.Height + " px";

					//Font Metrics
					prop.Ascent = font.FontFamily.GetCellAscent(font.Style).ToString();
					prop.Descent = font.FontFamily.GetCellDescent(font.Style).ToString();
					prop.LineSpacing = font.FontFamily.GetLineSpacing(font.Style).ToString();
					prop.EmHeight = font.FontFamily.GetEmHeight(font.Style).ToString();
				}
				catch
				{
				}
			}

			pg.SelectedObject = prop;
		}

		protected string GetFontType(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}

			string ext = Path.GetExtension(path).ToLower();
			switch (ext)
			{
				case ".ttf":
					return "TrueType (*.ttf)";

				case ".otf":
					return "OpenType (*.otf)";

				default:
					return ext;
			}
		}

		protected string GetUnitName(GraphicsUnit unit)
		{
			switch (unit)
			{
				case GraphicsUnit.Pixel:
					return "px";

				case GraphicsUnit.Point:
					return "pt";

				case GraphicsUnit.Millimeter:
					return "mm";

				case GraphicsUnit.Inch:
					return "in";

				default:
					return null;
			}
		}
	}
}
