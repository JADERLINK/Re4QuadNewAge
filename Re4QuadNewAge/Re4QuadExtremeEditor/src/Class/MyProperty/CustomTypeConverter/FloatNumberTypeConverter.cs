using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter
{
    public class FloatNumberTypeConverter : TypeConverter
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
                CultureInfo c = new CultureInfo(CultureInfo.InvariantCulture.Name, false);
                c.NumberFormat.NumberDecimalDigits = Globals.FrationalAmount;
                if (Globals.FrationalSymbol == Enums.ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputComma || Globals.FrationalSymbol == Enums.ConfigFrationalSymbol.OnlyAcceptComma)
                {
                    c.NumberFormat.NumberDecimalSeparator = ",";
                }
                

                return Math.Round((float)value, 9, MidpointRounding.AwayFromZero).ToString("F", c.NumberFormat);
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
                CultureInfo c = new CultureInfo(CultureInfo.InvariantCulture.Name, false);
                c.NumberFormat.NumberDecimalDigits = Globals.FrationalAmount;

                bool UseComma = (Globals.FrationalSymbol == Enums.ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputComma || Globals.FrationalSymbol == Enums.ConfigFrationalSymbol.OnlyAcceptComma);
                bool UsePeriod = (Globals.FrationalSymbol == Enums.ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputPeriod || Globals.FrationalSymbol == Enums.ConfigFrationalSymbol.OnlyAcceptPeriod);

                string text = (string)value;
                string input = "";
                if (text.Length > 300)
                {
                    text = text.Substring(0, 300);
                }

                string _decimal = "";
                string _fractional = "";

                bool isMinus = false;
                bool isComma = false;
                for (int i = 0; i < text.Length; i++)
                {
                    if (isComma == false)
                    {
                        if (char.IsDigit(text[i]))
                        {
                            _decimal += text[i];
                        }
                    }
                    if (isComma == true)
                    {
                        if (char.IsDigit(text[i]))
                        {
                            _fractional += text[i];
                        }
                    }
                    if (isMinus == false && text[i] == '-')
                    {
                        isMinus = true;
                    }
                    if (isComma == false && ((text[i] == ',' && UseComma) || (text[i] == '.' && UsePeriod)))
                    {
                        isComma = true;
                    }
                }

                if (_decimal.Length > 12)
                {
                    _decimal = _decimal.Substring(_decimal.Length -12, 12);
                }
                if (_fractional.Length > Globals.FrationalAmount)
                {
                    _fractional = _fractional.Substring(0, Globals.FrationalAmount);
                }

                if (isMinus == true)
                {
                    input = "-";
                }
                if (_decimal.Length == 0)
                {
                    input += "0";
                }
                else 
                {
                    input += _decimal;
                }

                if (_fractional.Length != 0)
                {
                    input += "." + _fractional;
                }


                double NunInput = 0;
                try { NunInput = Math.Round(double.Parse(input, NumberStyles.Any, c.NumberFormat), 9, MidpointRounding.AwayFromZero); } catch (Exception) { }
                input = NunInput.ToString("F", c.NumberFormat);

                if (propType == typeof(float))
                {
                    
                    if (NunInput > float.MaxValue)
                    {
                        input = float.MaxValue.ToString();
                    }
                    if (NunInput < float.MinValue)
                    {               
                        input = float.MinValue.ToString();               
                    }
                    

                    return float.Parse(input, c.NumberFormat);
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
