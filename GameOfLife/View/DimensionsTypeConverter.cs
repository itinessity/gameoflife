using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.View
{
	public class DimensionsTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				char separator = ' ';// allow either space, comma or x as a separator, e.g. 32 x 20
				if (text.Contains(','))
					separator = ',';
				else if (text.Contains('x'))
					separator = 'x';
				string[] args = text.Split(separator);
				if (args.Length != 2)
					throw new ArgumentException("Must have two comma separated numbers.");
				else
				{
					int width = 0;
					int height = 0;
					if (!int.TryParse(args[0].Trim(), out width) ||
						!int.TryParse(args[1].Trim(), out height))
						throw new ArgumentException("Either width or height is not an integer.");
					return new Dimensions(width, height);
				}
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(Dimensions))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Dimensions)
			{
				Dimensions dim = (Dimensions)value;
				return string.Format("{0} x {1}", dim.Width, dim.Height);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
