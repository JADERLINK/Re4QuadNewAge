using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Interfaces;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection;

namespace Re4QuadExtremeEditor.src.Class.MyProperty
{
    public abstract class BaseProperty : GenericProperty
    {
        protected byte[] GetNewByteArrayValue(byte[] value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            byte[] original = (byte[])GetPropertyValue(propertyName);
            byte[] _set = new byte[original.Length];
            original.CopyTo(_set, 0);
            value.Take(original.Length).ToArray().CopyTo(_set, 0);
            return _set;
        }

        protected abstract void SetFloatType(bool IsHex);

        private const int CategoryID_FloatType = 999999;

        #region Change float/hex type
        // float type
        [CustomCategory(aLang.FloatTypeCategory)]
        [CustomDisplayName(aLang.FloatType_DisplayName)]
        [CustomDescription(aLang.FloatType_Description)]
        [Editor(typeof(HexFloatEnableGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(CategoryID_FloatType, CategoryID_FloatType)]
        public BoolObjForListBox FloatType
        {
            get
            {
                return ListBoxProperty.FloatTypeList[Globals.PropertyGridUseHexFloat];
            }
            set
            {
                Globals.PropertyGridUseHexFloat = value.ID;
                SetFloatType(Globals.PropertyGridUseHexFloat);
            }
        }
        #endregion
    }
}
