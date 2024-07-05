using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter  // baseado em https://github.com/DavidSM64/Quad64/blob/master/src/LevelInfo/HexNumberTypeConverter.cs
{
    public class HexNumberTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && (value.GetType() == typeof(uint) || value.GetType() == typeof(int)))
            {
                return string.Format("{0:X8}", value);
            }
            else if (destinationType == typeof(string) && (value.GetType() == typeof(ushort) || value.GetType() == typeof(short)))
            {
                return string.Format("{0:X4}", value);
            }
            else if (destinationType == typeof(string) && (value.GetType() == typeof(byte) || value.GetType() == typeof(sbyte)))
            {
                return string.Format("{0:X2}", value);
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //Console.WriteLine("Value = " + value + ", type = " + value.GetType().ToString());
            //Console.WriteLine("Context = " + context.PropertyDescriptor.PropertyType);
            Type propType = context.PropertyDescriptor.PropertyType;
            if (value.GetType() == typeof(string))
            {
                string text = (string)value;
                string input = "";
                if (text.Length > 100)
                {
                    text = text.Substring(0, 100);
                }
                text = text.ToUpper();
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsDigit(text[i]) 
                        || text[i] == 'A'
                        || text[i] == 'B'
                        || text[i] == 'D'
                        || text[i] == 'C'
                        || text[i] == 'E'
                        || text[i] == 'F')
                    {
                        input += text[i];
                    }         
                }

                if (input.Length == 0)
                {
                    input = "0";
                }

                if (input.Length > 8)
                {
                    input = input.Substring(0, 8);
                }

                if (propType == typeof(uint))
                {return uint.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }
                if (propType == typeof(int))
                { return int.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }

                if (input.Length > 4)
                {
                    input = input.Substring(0, 4);
                }

                if (propType == typeof(ushort))
                { return ushort.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }
                if (propType == typeof(short))
                { return short.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }

                if (input.Length > 2)
                {
                    input = input.Substring(0, 2);
                }

                if (propType == typeof(byte))
                { return byte.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }

                if (propType == typeof(sbyte))
                { return sbyte.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }

                return input;
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }

        }


    }
}
