using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlUtils
{
    public class FloatBox : TextBox
    {
        public FloatBox() 
        {
            MaxLength = 100;
            Value = 0;
        }

        private float value = 0f;

        public float Value 
        { 
            get 
            {
                return value;
            }
            set 
            { 
                this.value = value;
                lastText = floatToText(value);
                Text = lastText;
            }
        }

        public float MaxValue { get; set; } = float.MaxValue;
        public float MinValue { get; set; } = float.MinValue;

        private string lastText = "";

        private int lastSelectionStart = 0;

        protected override void OnTextChanged(EventArgs e) 
        {
            if (Text != lastText)
            {
                this.SuspendLayout();

                lastSelectionStart = this.SelectionStart;

                string temp = "";
                bool asminus = false;
                bool asDot = false;

                for (int i = 0; i < Text.Length; i++)
                {
                    if (char.IsDigit(Text[i]))
                    {
                        temp += Text[i];
                    }
                    else if (!asDot && (Text[i] == '.' || Text[i] == ','))
                    {
                        temp += '.';
                        asDot = true;
                    }
                    else if (!asminus && Text[i] == '-') 
                    {
                        temp = '-' + temp;
                        asminus = true;
                    }
                }
                if (temp.Length == 0)
                {
                    temp = "0";
                }
                else if (temp[temp.Length -1] == '.')
                {
                    temp += '0';
                }

                float tempvalue = 0;
                try
                {
                    tempvalue = float.Parse(temp, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                }
                if (tempvalue < MinValue)
                {
                    tempvalue = MinValue;
                }
                else if (tempvalue > MaxValue) 
                {
                    tempvalue = MaxValue;
                }
                value = tempvalue;
                lastText = floatToText(tempvalue);
                Text = lastText;

                this.SelectionStart = lastSelectionStart;
                this.SelectionLength = 0;

                this.ResumeLayout();
            }

            base.OnTextChanged(e);
        }

        private string floatToText(float value) 
        {
            return value.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);
        }
   
    }
}
