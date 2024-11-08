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
    public class QuadCustomProperty : BaseTriggerZoneProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(QuadCustomProperty);
        }

        private const GroupType groupType = GroupType.QUAD_CUSTOM;

        private ushort InternalID = ushort.MaxValue;
       
        private QuadCustomMethods Methods = null;

        public override ushort GetInternalID()
        {
            return InternalID;
        }

        public override GroupType GetGroupType()
        {
            return groupType;
        }

        protected override BaseTriggerZoneMethods GetTriggerZoneMethods()
        {
            return Methods;
        }

        protected override void SetFloatType(bool IsHex)
        {
            SetTriggerZoneFloatType(Globals.PropertyGridUseHexFloat);

            ChangePropertyIsBrowsable(nameof(PROP_ScaleX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_ScaleY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_ScaleZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Div100), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Div100), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Div100), !IsHex);

            ChangePropertyIsBrowsable(nameof(PROP_ScaleX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_ScaleY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_ScaleZ_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleZ_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Div100_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Div100_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Div100_Hex), IsHex);
        }

        private void SetPropertyTexts()
        {
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", Consts.QuadCustomLineArrayLength.ToString()));
        }

        public QuadCustomProperty(QuadCustomProperty prop, bool ForMultiSelection = false)
        {
            QuadCustomPropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public QuadCustomProperty(ushort InternalID, UpdateMethods updateMethods, QuadCustomMethods Methods, bool ForMultiSelection = false) : base()
        {
            QuadCustomPropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void QuadCustomPropertyConstructor(ushort InternalID, UpdateMethods updateMethods, QuadCustomMethods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;

            if (!ForMultiSelection)
            {
                SetThis(this);
            }

            SetFloatType(Globals.PropertyGridUseHexFloat);
            SetTriggerZoneDescription(0x00);
            SetPropertyTexts();
        }

        #region Category Ids
        private const int CategoryID_InternalLineID = 1;
        private const int CategoryID_LineArray = 2;
        private const int CategoryID_PartArray = 3;
        private const int CategoryID_ObjectName = 4;
        private const int CategoryID_Color = 5;
        private const int CategoryID_QuadCustom = 11;
        private const int CategoryID_Point_Position = 12;
        private const int CategoryID_Point_Position_Div100 = 13;
        private const int CategoryID_Point_Angle = 14;
        private const int CategoryID_Point_Scale = 15;
        private const int CategoryID_Point_Info = 16;
        #endregion

        #region firt propertys

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
        [DynamicTypeDescriptor.Id(2, CategoryID_LineArray)]
        public byte[] Line
        {
            get => Methods.ReturnLine(InternalID);
            set
            {
                byte[] _set = new byte[Consts.QuadCustomLineArrayLength];
                byte[] insert = value.Take(Consts.QuadCustomLineArrayLength).ToArray();
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_PartArray_Category)]
        [CustomDisplayName(aLang.QuadCustom_TriggerZoneArray_DisplayName)]
        [CustomDescription(aLang.QuadCustom_TriggerZoneArray_Description)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID_PartArray)]
        public byte[] TriggerZoneArray
        {
            get => Methods.ReturnByteArrayFromPosition(InternalID, 0x00, 48);
            set
            {
                byte[] _set = new byte[48];
                byte[] insert = value.Take(48).ToArray();
                TriggerZoneArray.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetByteArrayFromPosition(InternalID, 0x00, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_PartArray_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionArray_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionArray_Description)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(4, CategoryID_PartArray)]
        public byte[] PositionArray
        {
            get => Methods.ReturnByteArrayFromPosition(InternalID, 0x30, 12);
            set
            {
                byte[] _set = new byte[12];
                byte[] insert = value.Take(12).ToArray();
                PositionArray.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetByteArrayFromPosition(InternalID, 0x30, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_PartArray_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleArray_DisplayName)]
        [CustomDescription(aLang.QuadCustom_angleArray_Description)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(4, CategoryID_PartArray)]
        public byte[] AngleArray
        {
            get => Methods.ReturnByteArrayFromPosition(InternalID, 0x3C, 12);
            set
            {
                byte[] _set = new byte[12];
                byte[] insert = value.Take(12).ToArray();
                AngleArray.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetByteArrayFromPosition(InternalID, 0x3C, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_PartArray_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleArray_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleArray_Description)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(4, CategoryID_PartArray)]
        public byte[] ScaleArray
        {
            get => Methods.ReturnByteArrayFromPosition(InternalID, 0x48, 12);
            set
            {
                byte[] _set = new byte[12];
                byte[] insert = value.Take(12).ToArray();
                ScaleArray.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetByteArrayFromPosition(InternalID, 0x48, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_ObjectName_Category)]
        [CustomDisplayName(aLang.QuadCustom_ObjectName_String_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ObjectName_String_Description)]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(7, CategoryID_ObjectName)]
        public string ObjectName
        {
            get => Methods.ReturnObjectName(InternalID);
            set
            {
                string res = value;
                if (res.Length > Consts.QuadCustomTextLength)
                {
                    res.Substring(0, Consts.QuadCustomTextLength);
                }
                Methods.SetObjectName(InternalID, res);
            }
        }


        [CustomCategory(aLang.QuadCustom_Color_Category)]
        [CustomDisplayName(aLang.QuadCustom_ColorRGB_ByteArray_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ColorRGB_ByteArray_Description)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(SelectColorUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(8, CategoryID_Color)]
        public byte[] ColorRGB
        {
            get => Methods.ReturnColorRGB(InternalID);
            set
            {
                Methods.SetColorRGB(InternalID, GetNewByteArrayValue(value));
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region float Scale, Angle, Position

        [CustomCategory(aLang.QuadCustom_Point_Scale_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleX_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleX_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(400, CategoryID_Point_Scale)]
        public float PROP_ScaleX
        {
            get => Methods.ReturnScaleX(InternalID);
            set
            {
                Methods.SetScaleX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Scale_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleY_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleY_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(500, CategoryID_Point_Scale)]
        public float PROP_ScaleY
        {
            get => Methods.ReturnScaleY(InternalID);
            set
            {
                Methods.SetScaleY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Scale_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleZ_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleZ_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(600, CategoryID_Point_Scale)]
        public float PROP_ScaleZ
        {
            get => Methods.ReturnScaleZ(InternalID);
            set
            {
                Methods.SetScaleZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Angle_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleX_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_AngleX_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(700, CategoryID_Point_Angle)]
        public float PROP_AngleX
        {
            get => Methods.ReturnAngleX(InternalID);
            set
            {
                Methods.SetAngleX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Angle_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleY_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_AngleY_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(800, CategoryID_Point_Angle)]
        public float PROP_AngleY
        {
            get => Methods.ReturnAngleY(InternalID);
            set
            {
                Methods.SetAngleY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Angle_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleZ_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_AngleZ_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(900, CategoryID_Point_Angle)]
        public float PROP_AngleZ
        {
            get => Methods.ReturnAngleZ(InternalID);
            set
            {
                Methods.SetAngleZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Position_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionX_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionX_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1000, CategoryID_Point_Position)]
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

        [CustomCategory(aLang.QuadCustom_Point_Position_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionY_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionY_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1100, CategoryID_Point_Position)]
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


        [CustomCategory(aLang.QuadCustom_Point_Position_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionZ_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionZ_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1200, CategoryID_Point_Position)]
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

        //div 100


        [CustomCategory(aLang.QuadCustom_Point_Position_Div100_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionX_Div100_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionX_Div100_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1300, CategoryID_Point_Position_Div100)]
        public float PROP_PositionX_Div100
        {
            get => Methods.ReturnPositionX(InternalID) / 100f;
            set
            {
                Methods.SetPositionX(InternalID, value * 100f);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Position_Div100_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionY_Div100_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionY_Div100_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1400, CategoryID_Point_Position_Div100)]
        public float PROP_PositionY_Div100
        {
            get => Methods.ReturnPositionY(InternalID) / 100f;
            set
            {
                Methods.SetPositionY(InternalID, value * 100f);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Position_Div100_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionZ_Div100_Float_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionZ_Div100_Description)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1500, CategoryID_Point_Position_Div100)]
        public float PROP_PositionZ_Div100
        {
            get => Methods.ReturnPositionZ(InternalID) / 100f;
            set
            {
                Methods.SetPositionZ(InternalID, value * 100f);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        #endregion

        #region uint Hex Scale, Angle, Position

        [CustomCategory(aLang.QuadCustom_Point_Scale_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleX_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleX_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1300, CategoryID_Point_Scale)]
        public uint PROP_ScaleX_Hex
        {
            get => Methods.ReturnScaleX_Hex(InternalID);
            set
            {
                Methods.SetScaleX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Scale_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleY_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleY_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1400, CategoryID_Point_Scale)]
        public uint PROP_ScaleY_Hex
        {
            get => Methods.ReturnScaleY_Hex(InternalID);
            set
            {
                Methods.SetScaleY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Scale_Category)]
        [CustomDisplayName(aLang.QuadCustom_ScaleZ_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_ScaleZ_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1500, CategoryID_Point_Scale)]
        public uint PROP_ScaleZ_Hex
        {
            get => Methods.ReturnScaleZ_Hex(InternalID);
            set
            {
                Methods.SetScaleZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Angle_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleX_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_AngleX_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1600, CategoryID_Point_Angle)]
        public uint PROP_AngleX_Hex
        {
            get => Methods.ReturnAngleX_Hex(InternalID);
            set
            {
                Methods.SetAngleX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Angle_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleY_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_AngleY_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1700, CategoryID_Point_Angle)]
        public uint PROP_AngleY_Hex
        {
            get => Methods.ReturnAngleY_Hex(InternalID);
            set
            {
                Methods.SetAngleY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Angle_Category)]
        [CustomDisplayName(aLang.QuadCustom_AngleZ_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_AngleZ_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1800, CategoryID_Point_Angle)]
        public uint PROP_AngleZ_Hex
        {
            get => Methods.ReturnAngleZ_Hex(InternalID);
            set
            {
                Methods.SetAngleZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Position_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionX_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionX_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1900, CategoryID_Point_Position)]
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

        [CustomCategory(aLang.QuadCustom_Point_Position_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionY_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionY_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2000, CategoryID_Point_Position)]
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

        [CustomCategory(aLang.QuadCustom_Point_Position_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionZ_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionZ_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2100, CategoryID_Point_Position)]
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

        //  div 100

        [CustomCategory(aLang.QuadCustom_Point_Position_Div100_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionX_Div100_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionX_Div100_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2200, CategoryID_Point_Position_Div100)]
        public uint PROP_PositionX_Div100_Hex
        {
            get => BitConverter.ToUInt32(BitConverter.GetBytes(Methods.ReturnPositionX(InternalID) / 100f), 0);
            set
            {
                Methods.SetPositionX(InternalID, BitConverter.ToSingle(BitConverter.GetBytes(value), 0) * 100f);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Position_Div100_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionY_Div100_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionY_Div100_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2300, CategoryID_Point_Position_Div100)]
        public uint PROP_PositionY_Div100_Hex
        {
            get => BitConverter.ToUInt32(BitConverter.GetBytes(Methods.ReturnPositionY(InternalID) / 100f), 0);
            set
            {
                Methods.SetPositionY(InternalID, BitConverter.ToSingle(BitConverter.GetBytes(value), 0) * 100f);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Position_Div100_Category)]
        [CustomDisplayName(aLang.QuadCustom_PositionZ_Div100_Hex_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PositionZ_Div100_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2400, CategoryID_Point_Position_Div100)]
        public uint PROP_PositionZ_Div100_Hex
        {
            get => BitConverter.ToUInt32(BitConverter.GetBytes(Methods.ReturnPositionZ(InternalID) / 100f), 0);
            set
            {
                Methods.SetPositionZ(InternalID, BitConverter.ToSingle(BitConverter.GetBytes(value), 0) * 100f);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region point

        [CustomCategory(aLang.QuadCustom_Point_Info_Category)]
        [CustomDisplayName(aLang.QuadCustom_PointStatus_Byte_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PointStatus_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(100, CategoryID_Point_Info)]
        public byte PointStatus
        {
            get => Methods.ReturnPointStatus(InternalID);
            set
            {
                Methods.SetPointStatus(InternalID, value);
                SetFloatType(Globals.PropertyGridUseHexFloat);
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Info_Category)]
        [CustomDisplayName(aLang.QuadCustom_PointStatus_List_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PointStatus_Description)]
        [Editor(typeof(QuadCustomPointStatusGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(101, CategoryID_Point_Info)]
        public ByteObjForListBox PointStatus_ListBox
        {
            get
            {
                var v = Methods.ReturnPointStatus(InternalID);
                if (ListBoxProperty.QuadCustomPointStatusList.ContainsKey(v))
                {
                    return ListBoxProperty.QuadCustomPointStatusList[v];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxQuadCustomPointStatusAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    Methods.SetPointStatus(InternalID, value.ID);
                    SetFloatType(Globals.PropertyGridUseHexFloat);
                    updateMethods.UpdateMoveObjSelection();
                    updateMethods.UpdateOrbitCamera();
                    updateMethods.UpdateGL();
                }
            }
        }


        [CustomCategory(aLang.QuadCustom_Point_Info_Category)]
        [CustomDisplayName(aLang.QuadCustom_PointModelID_Uint_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PointModelID_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(102, CategoryID_Point_Info)]
        public uint PointModelID
        {
            get => Methods.ReturnPointModelID(InternalID);
            set
            {
                Methods.SetPointModelID(InternalID, value);
                SetFloatType(Globals.PropertyGridUseHexFloat);
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.QuadCustom_Point_Info_Category)]
        [CustomDisplayName(aLang.QuadCustom_PointModelID_List_DisplayName)]
        [CustomDescription(aLang.QuadCustom_PointModelID_Description)]
        [Editor(typeof(QuadCustomModelIDGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(103, CategoryID_Point_Info)]
        public UintObjForListBox PointModelID_ListBox
        {
            get
            {
                uint v = Methods.ReturnPointModelID(InternalID);
                if (ListBoxProperty.QuadCustomModelIDList.ContainsKey(v) && v != 0xFFFFFFFF)
                {
                    return ListBoxProperty.QuadCustomModelIDList[v];
                }
                else
                {
                    return new UintObjForListBox(0xFFFFFFFF, "" + Lang.GetAttributeText(aLang.ListBoxUnknownQuadCustomPoint));
                }
            }
            set
            {
                if (value.ID < 0xFFFFFFFF)
                {
                    Methods.SetPointModelID(InternalID, value.ID);
                    SetFloatType(Globals.PropertyGridUseHexFloat);
                    updateMethods.UpdateMoveObjSelection();
                    updateMethods.UpdateOrbitCamera();
                    updateMethods.UpdateGL();
                }
            }
        }

        #endregion


        #region Search Methods


        public uint ReturnUshortFirstSearchSelect()
        {
            return Methods.ReturnPointModelID(InternalID);
        }

        public void Searched(object obj)
        {
            if (obj is UintObjForListBox ushortObj)
            {
                Methods.SetPointModelID(InternalID, ushortObj.ID);
                updateMethods.UpdateTreeViewObjs();
                updateMethods.UpdatePropertyGrid();
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion
    }
}
