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
    public class NewAge_EMI_Property : BaseProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(NewAge_EMI_Property);
        }

        private const GroupType groupType = GroupType.EMI;

        private ushort InternalID = ushort.MaxValue;
        private Re4Version version = Re4Version.NULL;

        private NewAge_EMI_Methods Methods = null;
        private UpdateMethods updateMethods = null;

        public ushort GetInternalID()
        {
            return InternalID;
        }

        public GroupType GetGroupType()
        {
            return groupType;
        }

        protected override void SetFloatType(bool IsHex)
        {
            ChangePropertyIsBrowsable(nameof(PROP_AngleY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Hex), IsHex);
        }

        private void SetPropertyTexts()
        {
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "64"));
        }


        public NewAge_EMI_Property(NewAge_EMI_Property prop, bool ForMultiSelection = false)
        {
            NewAge_ESE_PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public NewAge_EMI_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_EMI_Methods Methods, bool ForMultiSelection = false) : base()
        {
            NewAge_ESE_PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void NewAge_ESE_PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_EMI_Methods Methods, bool ForMultiSelection = false)
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
            SetPropertyTexts();
        }

     
        #region Category Ids
        private const int CategoryID_UnderDevelopment = 0;
        private const int CategoryID_InternalLineID = 1;
        private const int CategoryID_LineArray = 3;
        private const int CategoryID_Position = 4;
        private const int CategoryID_Angle = 5;
        private const int CategoryID_EMI = 6;
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
        [DynamicTypeDescriptor.Id(8, CategoryID_LineArray)]
        public byte[] Line
        {
            get => Methods.ReturnLine(InternalID);
            set
            {
                //2007ps2 e UHD
                byte[] _set = new byte[64];
                byte[] insert = value.Take(64).ToArray();
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion


        #region Position Angle

        [CustomCategory(aLang.EMI_Position_Category)]
        [CustomDisplayName(aLang.EMI_PositionX_Float_DisplayName)]
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

        [CustomCategory(aLang.EMI_Position_Category)]
        [CustomDisplayName(aLang.EMI_PositionY_Float_DisplayName)]
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


        [CustomCategory(aLang.EMI_Position_Category)]
        [CustomDisplayName(aLang.EMI_PositionZ_Float_DisplayName)]
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

        [CustomCategory(aLang.EMI_Position_Category)]
        [CustomDisplayName(aLang.EMI_PositionX_Hex_DisplayName)]
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

        [CustomCategory(aLang.EMI_Position_Category)]
        [CustomDisplayName(aLang.EMI_PositionY_Hex_DisplayName)]
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

        [CustomCategory(aLang.EMI_Position_Category)]
        [CustomDisplayName(aLang.EMI_PositionZ_Hex_DisplayName)]
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

        [CustomCategory(aLang.EMI_Angle_Category)]
        [CustomDisplayName(aLang.EMI_AngleY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2200, CategoryID_Angle)]
        public float PROP_AngleY
        {
            get => Methods.ReturnAngleY(InternalID);
            set
            {
                Methods.SetAngleY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EMI_Angle_Category)]
        [CustomDisplayName(aLang.EMI_AngleY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2300, CategoryID_Angle)]
        public uint PROP_AngleY_Hex
        {
            get => Methods.ReturnAngleY_Hex(InternalID);
            set
            {
                Methods.SetAngleY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }




        #endregion
    }
}
