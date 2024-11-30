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
using SimpleEndianBinaryIO;

namespace Re4QuadExtremeEditor.src.Class.MyProperty._EFF_Property
{
    [DefaultProperty(nameof(InternalLineID))]
    public class EFF_TableEffectEntry_Property : BaseProperty, IInternalID
    {
        private const GroupType groupType = GroupType.EFF_EffectEntry;

        private ushort InternalID = ushort.MaxValue;

        private NewAge_EFF_EffectEntry_Methods Methods = null;
        private UpdateMethods updateMethods = null;

        public override Type GetClassType()
        {
            return typeof(EFF_TableEffectEntry_Property);
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
            ChangePropertyIsBrowsable(nameof(PROP_AngleX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ), !IsHex);

            ChangePropertyIsBrowsable(nameof(PROP_AngleX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleZ_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Hex), IsHex);
        }

        protected void SetPropertyId()
        {
        }

        private void SetPropertyTexts()
        {
            //Line
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "300"));
        }

        public EFF_TableEffectEntry_Property(EFF_TableEffectEntry_Property prop, bool ForMultiSelection = false)
        {
            PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public EFF_TableEffectEntry_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_EFF_EffectEntry_Methods Methods, bool ForMultiSelection = false) : base()
        {
            PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_EFF_EffectEntry_Methods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;

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
        private const int CategoryID_Position = 4;
        private const int CategoryID_Angle = 5;
        #endregion

        #region first props

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
                byte[] _set = new byte[300];
                byte[] insert = value.Take(300).ToArray();
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EFF_Entry_Order_Category)]
        [CustomDisplayName(aLang.EFF_Entry_EntryOrderID_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID_Order)]
        public ushort EFF_Entry_EntryOrderID
        {
            get => Methods.GetEntryOrderID(InternalID);
        }

        [CustomCategory(aLang.EFF_Entry_Order_Category)]
        [CustomDisplayName(aLang.EFF_Entry_GroupOrderID_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2, CategoryID_Order)]
        public ushort EFF_Group_GroupOrderID
        {
            get => Methods.GetGroupOrderID(InternalID);
            set
            {
                Methods.SetGroupOrderID(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EFF_Entry_Order_Category)]
        [CustomDisplayName(aLang.EFF_TableID_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2, CategoryID_Order)]
        public byte EFF_TableID
        {
            get => Methods.GetTableID(InternalID);
            set
            {
                Methods.SetTableID(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region property position Angle

        [CustomCategory(aLang.EFF_EffectEntry_Angle_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_AngleX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(700, CategoryID_Angle)]
        public float PROP_AngleX
        {
            get => Methods.ReturnAngleX(InternalID);
            set
            {
                Methods.SetAngleX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EFF_EffectEntry_Angle_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_AngleY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(800, CategoryID_Angle)]
        public float PROP_AngleY
        {
            get => Methods.ReturnAngleY(InternalID);
            set
            {
                Methods.SetAngleY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EFF_EffectEntry_Angle_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_AngleZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(900, CategoryID_Angle)]
        public float PROP_AngleZ
        {
            get => Methods.ReturnAngleZ(InternalID);
            set
            {
                Methods.SetAngleZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EFF_EffectEntry_Position_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_PositionX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1000, CategoryID_Position)]
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

        [CustomCategory(aLang.EFF_EffectEntry_Position_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_PositionY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1100, CategoryID_Position)]
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


        [CustomCategory(aLang.EFF_EffectEntry_Position_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_PositionZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1200, CategoryID_Position)]
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

        [CustomCategory(aLang.EFF_EffectEntry_Angle_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_AngleX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1600, CategoryID_Angle)]
        public uint PROP_AngleX_Hex
        {
            get => Methods.ReturnAngleX_Hex(InternalID);
            set
            {
                Methods.SetAngleX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EFF_EffectEntry_Angle_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_AngleY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1700, CategoryID_Angle)]
        public uint PROP_AngleY_Hex
        {
            get => Methods.ReturnAngleY_Hex(InternalID);
            set
            {
                Methods.SetAngleY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EFF_EffectEntry_Angle_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_AngleZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1800, CategoryID_Angle)]
        public uint PROP_AngleZ_Hex
        {
            get => Methods.ReturnAngleZ_Hex(InternalID);
            set
            {
                Methods.SetAngleZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EFF_EffectEntry_Position_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_PositionX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1900, CategoryID_Position)]
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

        [CustomCategory(aLang.EFF_EffectEntry_Position_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_PositionY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2000, CategoryID_Position)]
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

        [CustomCategory(aLang.EFF_EffectEntry_Position_Category)]
        [CustomDisplayName(aLang.EFF_EffectEntry_PositionZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2100, CategoryID_Position)]
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

        #endregion
    }
}
