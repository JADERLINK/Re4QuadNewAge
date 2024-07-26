using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter
{
    public class ByteArrayTypeConverter: TypeConverter
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
            if (destinationType == typeof(string) && (value.GetType() == typeof(byte[]) ))
            {
                byte[] b = (byte[])value;

                return BitConverter.ToString(b, 0).Replace("-", "");
            }
            else if (destinationType == typeof(string) && value.GetType() == typeof(byte))
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
                if (text.Length > 1000)
                {
                    text = text.Substring(0, 1000);
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

                if (input.Length % 2 != 0)
                {
                    input+= "0";
                }

                if (propType == typeof(byte[]))
                {
                    List<byte> bList = new List<byte>();
                    for (int i = 0; i < input.Length; i+=2)
                    {
                        string b = input[i].ToString() + input[i + 1].ToString();
                        bList.Add(byte.Parse(b, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat));
                    }
                    return bList.ToArray(); 
                }

                if (input.Length == 0)
                {
                    input = "00";
                }

                if (input.Length > 2)
                {
                    input = input.Substring(0, 2);
                }

                if (propType == typeof(byte))
                { return byte.Parse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat); }

                return input;
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }

        }


    }
}
