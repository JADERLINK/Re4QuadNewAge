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
    [DefaultProperty(nameof(InternalLineID))]
    public class NewAge_DSE_Property : GenericProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(NewAge_DSE_Property);
        }

        private const GroupType groupType = GroupType.DSE;

        private ushort InternalID = ushort.MaxValue;

        private NewAge_DSE_Methods Methods = null;
        private UpdateMethods updateMethods = null;

        public ushort GetInternalID()
        {
            return InternalID;
        }

        public GroupType GetGroupType()
        {
            return groupType;
        }

        private void SetPropertyTexts()
        {
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "12"));
        }
        public NewAge_DSE_Property(NewAge_DSE_Property prop, bool ForMultiSelection = false)
        {
            NewAge_DSE_PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public NewAge_DSE_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_DSE_Methods Methods, bool ForMultiSelection = false) : base()
        {
            NewAge_DSE_PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void NewAge_DSE_PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_DSE_Methods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;

            if (!ForMultiSelection)
            {
                SetThis(this);
            }

            SetPropertyTexts();
        }

        #region Category Ids
        private const int CategoryID0_InternalLineID = 0;
        private const int CategoryID2_LineArray = 2;
        private const int CategoryID3_DSE = 3;
        #endregion

        #region firt propertys

        [CustomCategory(aLang.NewAge_InternalLineIDCategory)]
        [CustomDisplayName(aLang.NewAge_InternalLineIDDisplayName)]
        [CustomDescription(aLang.NewAge_InternalLineIDDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(1, CategoryID0_InternalLineID)]
        public string InternalLineID { get => GetInternalID().ToString(); }


        [CustomCategory(aLang.NewAge_LineArrayCategory)]
        [CustomDisplayName(aLang.NewAge_LineArrayDisplayName)]
        [CustomDescription(aLang.NewAge_LineArrayDescription)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(8, CategoryID2_LineArray)]
        public byte[] Line
        {
            get => Methods.ReturnLine(InternalID);
            set
            {
                byte[] _set = new byte[12];
                byte[] insert = value.Take(12).ToArray();
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);
            }
        }

        #endregion

        #region values

        [CustomCategory(aLang.NewAge_DSE_Category)]
        [CustomDisplayName(aLang.DSE_HX00_Ushort_DisplayName)]
        [CustomDescription(aLang.DSE_HX00_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x100, CategoryID3_DSE)]
        public ushort DSE_HX00
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x00);
            set 
            {
                Methods.SetUshortFromPosition(InternalID, 0x00, value);
            }
        }

        [CustomCategory(aLang.NewAge_DSE_Category)]
        [CustomDisplayName(aLang.DSE_HX02_Ushort_DisplayName)]
        [CustomDescription(aLang.DSE_HX02_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x200, CategoryID3_DSE)]
        public ushort DSE_HX02
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x02);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x02, value);
            }
        }

        [CustomCategory(aLang.NewAge_DSE_Category)]
        [CustomDisplayName(aLang.DSE_HX04_Ushort_DisplayName)]
        [CustomDescription(aLang.DSE_HX04_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x400, CategoryID3_DSE)]
        public ushort DSE_HX04
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x04);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x04, value);
            }
        }

        [CustomCategory(aLang.NewAge_DSE_Category)]
        [CustomDisplayName(aLang.DSE_HX06_Ushort_DisplayName)]
        [CustomDescription(aLang.DSE_HX06_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x600, CategoryID3_DSE)]
        public ushort DSE_HX06
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x06);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x06, value);
            }
        }

        [CustomCategory(aLang.NewAge_DSE_Category)]
        [CustomDisplayName(aLang.DSE_HX08_Ushort_DisplayName)]
        [CustomDescription(aLang.DSE_HX08_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x800, CategoryID3_DSE)]
        public ushort DSE_HX08
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x08);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x08, value);
            }
        }

        [CustomCategory(aLang.NewAge_DSE_Category)]
        [CustomDisplayName(aLang.DSE_HX0A_Ushort_DisplayName)]
        [CustomDescription(aLang.DSE_HX0A_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0xA00, CategoryID3_DSE)]
        public ushort DSE_HX0A
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x0A);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x0A, value);
            }
        }

        #endregion
    }
}
