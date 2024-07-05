using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter  // baseado em https://github.com/DavidSM64/Quad64/blob/master/src/LevelInfo/HexNumberTypeConverter.cs
{
    public class DecNumberTypeConverter : TypeConverter
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
            if (destinationType == typeof(string))
            {
                return value.ToString();
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
                bool isMinus = false;
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsDigit(text[i]))
                    {
                        input += text[i];
                    }
                    if (isMinus == false && text[i] == '-')
                    {
                        isMinus = true;
                        input = "-" + input;
                    }
                }

                if (input.Length > 18)
                {
                    input = input.Substring(0, 18);
                }
                if (input.Length == 0)
                {
                    input = "0";
                }

                long NunInput = 0;
                try { NunInput = long.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat); } catch (Exception){ }
                input = NunInput.ToString();

                if (propType == typeof(uint))
                {
                    if (NunInput > uint.MaxValue)
                    {
                        input = uint.MaxValue.ToString();
                    }
                    if (NunInput < 0)
                    {
                        long inv = NunInput * -1;
                        input = inv.ToString();
                        if (inv > uint.MaxValue)
                        {
                            input = uint.MaxValue.ToString();
                        }
                    }

                    return uint.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);             
                }

        
                if (propType == typeof(int))
                {
                    if (NunInput > int.MaxValue)
                    {
                        input = int.MaxValue.ToString();
                    }
                    if (NunInput < int.MinValue)
                    {
                        input = int.MinValue.ToString();
                    }

                    return int.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat); 
                }


                if (propType == typeof(ushort))
                {
                    if (NunInput > ushort.MaxValue)
                    {
                        input = ushort.MaxValue.ToString();
                    }
                    if (NunInput < 0)
                    {
                        long inv = NunInput * -1;
                        input = inv.ToString();
                        if (inv > ushort.MaxValue)
                        {
                            input = ushort.MaxValue.ToString();
                        }
                    }
                    return ushort.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);
                }

                if (propType == typeof(short))
                {
                    if (NunInput > short.MaxValue)
                    {
                        input = short.MaxValue.ToString();
                    }
                    if (NunInput < short.MinValue)
                    {
                        input = short.MinValue.ToString();
                    }

                    return short.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat); 
                }

                if (propType == typeof(byte))
                {
                    if (NunInput > byte.MaxValue)
                    {
                        input = byte.MaxValue.ToString();
                    }
                    if (NunInput < 0)
                    {
                        long inv = NunInput * -1;
                        input = inv.ToString();
                        if (inv > byte.MaxValue)
                        {
                            input = byte.MaxValue.ToString();
                        }
                    }

                    return byte.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat); 
                }

                if (propType == typeof(sbyte))
                {
                    if (NunInput > sbyte.MaxValue)
                    {
                        input = sbyte.MaxValue.ToString();
                    }
                    if (NunInput < sbyte.MinValue)
                    {
                        input = sbyte.MinValue.ToString();
                    }

                    return sbyte.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);
                }


                return input;
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }

        }


    }
}
