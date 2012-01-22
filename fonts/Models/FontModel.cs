using System;
using System.Drawing;

namespace Oxage.Fonts
{
	public class FontModel
	{
		/// <summary>
		/// Gets or sets a font.
		/// </summary>
		public FontFamily Font
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a path to font file.
		/// </summary>
		public string Path
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets font name.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		public override string ToString()
		{
			return (this.Font != null ? this.Font.Name : base.ToString());
		}
	}
}
