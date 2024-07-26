using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection //from https://www.codeproject.com/Articles/4448/Customized-Display-of-Collection-Data-in-a-Propert
{
	// This is a special type converter which will be associated with the any class.
	// It converts an any object to string representation for use in a property grid.
	internal class GenericConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
		{
            if (destType == typeof(string) && value is Interfaces.IDisplay display)
            {
				return display.Text_Value;
			}
			else if(destType == typeof(string) && value != null)
            {
				return value.ToString();
			}
			return "ERROR"; // base.ConvertTo(context, culture, value, destType);
		}
	}

	// This is a special type converter which will be associated with the GenericCollection class.
	// It converts an GenericCollection object to a string representation for use in a property grid.
	internal class GenericCollectionConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
		{
			if (destType == typeof(string) && value is GenericCollection generic)
			{
				return Lang.GetAttributeText(aLang.MultiSelectAmountSelected) +": " + generic.Count.ToString();
			}
			else if (destType == typeof(string) && value != null)
			{
				return value.ToString();
			}
			return "ERROR"; // base.ConvertTo(context, culture, value, destType);
		}
	}
}
