using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute
{
    public class CustomDescriptionAttribute : DescriptionAttribute
    {
        public CustomDescriptionAttribute(aLang AttributeTextId) : base(Lang.GetAttributeText(AttributeTextId)){}
    }

}
