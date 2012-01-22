using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace Oxage.Fonts
{
	public class FontCollection : List<FontModel>
	{
		private FontFamily[] families = new FontFamily[0];
		private PrivateFontCollection collection = new PrivateFontCollection();

		public PrivateFontCollection Collection
		{
			get
			{
				return collection;
			}
		}

		public FontFamily[] Families
		{
			get
			{
				//return families;
				return collection.Families;
			}
		}

		public void AddFontFile(string fontFile)
		{
			//Load font to private collection
			collection.AddFontFile(fontFile);

			//Find new added font families
			foreach (var family in collection.Families)
			{
				if (!ContainsFontFamily(family))
				{
					FontModel model = new FontModel();
					model.Name = family.Name;
					model.Path = fontFile;
					model.Font = family;
					this.Add(model);
				}
			}

			//Update array of loaded fonts
			families = collection.Families;
		}

		public void ClearFontFiles()
		{
			collection.Dispose();
			collection = null;

			collection = new PrivateFontCollection();
			families = new FontFamily[0];

			base.Clear();
		}

		public bool ContainsFontFamily(FontFamily family)
		{
			foreach (var item in families)
			{
				//if (item == family) //Does not work
				if (item.GetHashCode() == family.GetHashCode())
				{
					return true;
				}
			}
			return false;
		}
	}
}
