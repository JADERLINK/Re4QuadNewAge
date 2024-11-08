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
    public class NewAge_LIT_Entry_Property : BaseProperty, IInternalID
    {
        private const GroupType groupType = GroupType.LIT_ENTRYS;

        private ushort InternalID = ushort.MaxValue;
        private Re4Version version = Re4Version.NULL;

        private NewAge_LIT_Entry_Methods Methods = null;
        private UpdateMethods updateMethods = null;

        public override Type GetClassType()
        {
            return typeof(NewAge_LIT_Entry_Property);
        }

        public GroupType GetGroupType()
        {
            return groupType;
        }

        public ushort GetInternalID()
        {
            return InternalID;
        }

        protected override void SetFloatType(bool IsHex)
        {
            ChangePropertyIsBrowsable(nameof(LIT_RangeRadius), !IsHex);
            ChangePropertyIsBrowsable(nameof(LIT_Intensity), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(LIT_RangeRadius_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(LIT_Intensity_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Hex), IsHex);
        }

        void SetPropertyId()
        {
            if (version == Re4Version.V2007PS2)
            {
                ChangePropertyId(nameof(PROP_PositionX), 0x1000);
                ChangePropertyId(nameof(PROP_PositionY), 0x1400);
                ChangePropertyId(nameof(PROP_PositionZ), 0x1800);
                ChangePropertyId(nameof(PROP_PositionX_Hex), 0x1001);
                ChangePropertyId(nameof(PROP_PositionY_Hex), 0x1401);
                ChangePropertyId(nameof(PROP_PositionZ_Hex), 0x1801);
                ChangePropertyId(nameof(LIT_RangeRadius), 0x0400);
                ChangePropertyId(nameof(LIT_RangeRadius_Hex), 0x0401);
                ChangePropertyId(nameof(LIT_ColorRGB), 0x0800);
                ChangePropertyId(nameof(LIT_ColorAlfa), 0x0B00);
                ChangePropertyId(nameof(LIT_Intensity), 0x0C00);
                ChangePropertyId(nameof(LIT_Intensity_Hex), 0x0C01);
            }
            else
            {
                ChangePropertyId(nameof(PROP_PositionX), 0x0400);
                ChangePropertyId(nameof(PROP_PositionY), 0x0800);
                ChangePropertyId(nameof(PROP_PositionZ), 0x0C00);
                ChangePropertyId(nameof(PROP_PositionX_Hex), 0x0401);
                ChangePropertyId(nameof(PROP_PositionY_Hex), 0x0801);
                ChangePropertyId(nameof(PROP_PositionZ_Hex), 0x0C01);
                ChangePropertyId(nameof(LIT_RangeRadius), 0x1000);
                ChangePropertyId(nameof(LIT_RangeRadius_Hex), 0x1001);
                ChangePropertyId(nameof(LIT_ColorRGB), 0x1400);
                ChangePropertyId(nameof(LIT_ColorAlfa), 0x1700);
                ChangePropertyId(nameof(LIT_Intensity), 0x1800);
                ChangePropertyId(nameof(LIT_Intensity_Hex), 0x1801);
            }
        }

        private void SetPropertyTexts()
        {
            //Line
            if (version == Re4Version.UHD)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "300"));
            }
            else if (version == Re4Version.V2007PS2)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "112"));
            }
        }

        public NewAge_LIT_Entry_Property(NewAge_LIT_Entry_Property prop, bool ForMultiSelection = false)
        {
            NewAge_ESE_PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public NewAge_LIT_Entry_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_LIT_Entry_Methods Methods, bool ForMultiSelection = false) : base()
        {
            NewAge_ESE_PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void NewAge_ESE_PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_LIT_Entry_Methods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;
            version = Methods.ReturnRe4Version();

            if (!ForMultiSelection)
            {
                SetThis(this);
            }

            SetFloatType(Globals.PropertyGridUseHexFloat);
            SetPropertyId();
            SetPropertyTexts();
        }



        #region Category Ids
        private const int CategoryID_UnderDevelopment = 0;
        private const int CategoryID_InternalLineID = 1;
        private const int CategoryID_Order = 2;
        private const int CategoryID_LineArray = 3;
        private const int CategoryID_LIT_Light_Metadata = 4;
        private const int CategoryID_LIT_Light_Definition = 5;
        private const int CategoryID_LIT_Object_Settings = 6;
        #endregion



        #region firt propertys

        [CustomCategory(aLang.UnderDevelopment_Category)]
        [CustomDisplayName(aLang.UnderDevelopment_DisplayName)]
        [CustomDescription(aLang.UnderDevelopment_Description)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(0, CategoryID_UnderDevelopment)]
        public string UnderDevelopment { get => Lang.GetAttributeText(aLang.UnderDevelopment_Value); }

        [CustomCategory(aLang.NewAge_InternalLineIDCategory)]
        [CustomDisplayName(aLang.NewAge_InternalLineIDDisplayName)]
        [CustomDescription(aLang.NewAge_InternalLineIDDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(1, CategoryID_InternalLineID)]
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
        [DynamicTypeDescriptor.Id(4, CategoryID_LineArray)]
        public byte[] Line
        {
            get => Methods.ReturnLine(InternalID);
            set
            {
                //2007ps2
                byte[] _set = new byte[112];
                byte[] insert = value.Take(112).ToArray();

                if (version == Re4Version.UHD)
                {
                    _set = new byte[300];
                    insert = value.Take(300).ToArray();
                }

                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region CategoryID_LIT_Light_Metadata

        [CustomCategory(aLang.LIT_Light_Metadata_Category)]
        [CustomDisplayName(aLang.LIT_Light_Format_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0000, CategoryID_LIT_Light_Metadata)]
        public byte LIT_0x00
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x00);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x00, value);
            }
        }

        [CustomCategory(aLang.LIT_Light_Metadata_Category)]
        [CustomDisplayName(aLang.LIT_Light_Type_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0100, CategoryID_LIT_Light_Metadata)]
        public byte LIT_0x01
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x01);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x01, value);
            }
        }

        [CustomCategory(aLang.LIT_Light_Metadata_Category)]
        [CustomDisplayName(aLang.LIT_Light_Attributes_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0200, CategoryID_LIT_Light_Metadata)]
        public byte LIT_0x02
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x02);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x02, value);
            }
        }


        [CustomCategory(aLang.LIT_Light_Metadata_Category)]
        [CustomDisplayName(aLang.LIT_Light_Mask_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0300, CategoryID_LIT_Light_Metadata)]
        public byte LIT_0x03
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x03);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x03, value);
            }
        }

        #endregion

        #region LIT_Light_Definition_Category

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_PositionX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1000, CategoryID_LIT_Light_Definition)]
        public float PROP_PositionX
        {
            get => Methods.ReturnPositionX(InternalID);
            set
            {
                Methods.SetPositionX(InternalID, value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_PositionY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1100, CategoryID_LIT_Light_Definition)]
        public float PROP_PositionY
        {
            get => Methods.ReturnPositionY(InternalID);
            set
            {
                Methods.SetPositionY(InternalID, value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_PositionZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1200, CategoryID_LIT_Light_Definition)]
        public float PROP_PositionZ
        {
            get => Methods.ReturnPositionZ(InternalID);
            set
            {
                Methods.SetPositionZ(InternalID, value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_PositionX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1010, CategoryID_LIT_Light_Definition)]
        public uint PROP_PositionX_Hex
        {
            get => Methods.ReturnPositionX_Hex(InternalID);
            set
            {
                Methods.SetPositionX_Hex(InternalID, value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_PositionY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1110, CategoryID_LIT_Light_Definition)]
        public uint PROP_PositionY_Hex
        {
            get => Methods.ReturnPositionY_Hex(InternalID);
            set
            {
                Methods.SetPositionY_Hex(InternalID, value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_PositionZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1210, CategoryID_LIT_Light_Definition)]
        public uint PROP_PositionZ_Hex
        {
            get => Methods.ReturnPositionZ_Hex(InternalID);
            set
            {
                Methods.SetPositionZ_Hex(InternalID, value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_RangeRadius_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1300, CategoryID_LIT_Light_Definition)]
        public float LIT_RangeRadius
        {
            get => Methods.ReturnRangeRadius(InternalID);
            set
            {
                Methods.SetRangeRadius(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_RangeRadius_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1310, CategoryID_LIT_Light_Definition)]
        public uint LIT_RangeRadius_Hex
        {
            get => Methods.ReturnRangeRadius_Hex(InternalID);
            set
            {
                Methods.SetRangeRadius_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_ColorRGB_ByteArray3_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(SelectColorUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1400, CategoryID_LIT_Light_Definition)]
        public byte[] LIT_ColorRGB
        {
            get => Methods.ReturnColorRGB(InternalID);
            set
            {
                Methods.SetColorRGB(InternalID, GetNewByteArrayValue(value));
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_ColorAlfa_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1500, CategoryID_LIT_Light_Definition)]
        public byte LIT_ColorAlfa
        {
            get => Methods.ReturnColorAlfa(InternalID);
            set
            {
                Methods.SetColorAlfa(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_Intensity_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1600, CategoryID_LIT_Light_Definition)]
        public float LIT_Intensity
        {
            get => Methods.ReturnIntensity(InternalID);
            set
            {
                Methods.SetIntensity(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Light_Definition_Category)]
        [CustomDisplayName(aLang.LIT_Intensity_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1610, CategoryID_LIT_Light_Definition)]
        public uint LIT_Intensity_Hex
        {
            get => Methods.ReturnIntensity_Hex(InternalID);
            set
            {
                Methods.SetIntensity_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region CategoryID_LIT_Object_Settings

        [CustomCategory(aLang.LIT_Object_Settings_Category)]
        [CustomDisplayName(aLang.LIT_Parent_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x1C00, CategoryID_LIT_Object_Settings)]
        public byte LIT_0x1C
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1C);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1C, value);
            }
        }

        [CustomCategory(aLang.LIT_Object_Settings_Category)]
        [CustomDisplayName(aLang.LIT_Subgroup_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x1D00, CategoryID_LIT_Object_Settings)]
        public byte LIT_0x1D
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1D);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1D, value);
            }
        }

        [CustomCategory(aLang.LIT_Object_Settings_Category)]
        [CustomDisplayName(aLang.LIT_Flag_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x1E00, CategoryID_LIT_Object_Settings)]
        public byte LIT_0x1E
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1E);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1E, value);
            }
        }

        [CustomCategory(aLang.LIT_Object_Settings_Category)]
        [CustomDisplayName(aLang.LIT_Priority_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x1F00, CategoryID_LIT_Object_Settings)]
        public byte LIT_0x1F
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1F);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1F, value);
            }
        }

        [CustomCategory(aLang.LIT_Object_Settings_Category)]
        [CustomDisplayName(aLang.LIT_PartNumber_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x2000, CategoryID_LIT_Object_Settings)]
        public ushort LIT_0x20
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x20);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x20, value);
            }
        }

        [CustomCategory(aLang.LIT_Object_Settings_Category)]
        [CustomDisplayName(aLang.LIT_ParentID_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x2200, CategoryID_LIT_Object_Settings)]
        public ushort LIT_0x22
        {
            get => Methods.ReturnUshortFromPosition(InternalID, 0x22);
            set
            {
                Methods.SetUshortFromPosition(InternalID, 0x22, value);
            }
        }

        #endregion

        #region order

        [CustomCategory(aLang.LIT_Entry_Order_Category)]
        [CustomDisplayName(aLang.LIT_Entry_GroupOrderID_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2, CategoryID_Order)]
        public ushort LIT_Group_GroupOrderID
        {
            get => Methods.GetGroupOrderID(InternalID);
            set
            {
                Methods.SetGroupOrderID(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.LIT_Entry_Order_Category)]
        [CustomDisplayName(aLang.LIT_Entry_EntryOrderID_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID_Order)]
        public ushort LIT_Entry_EntryOrderID
        {
            get => Methods.GetEntryOrderID(InternalID);
        }

        #endregion
    }
}
