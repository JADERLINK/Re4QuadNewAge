using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute
{
    public class CustomCategoryAttribute : CategoryAttribute
    {
        public CustomCategoryAttribute(aLang AttributeTextId): base(Lang.GetAttributeText(AttributeTextId).PadRight(1000)){}
    }
}
