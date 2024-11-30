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
    public class EFF_Table3_Property : BaseProperty, IInternalID
    {
        private const GroupType groupType = GroupType.EFF_Table3;

        private ushort InternalID = ushort.MaxValue;

        private NewAge_EFF_Methods Methods = null;
        private UpdateMethods updateMethods = null;

        public override Type GetClassType()
        {
            return typeof(EFF_Table3_Property);
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
            ChangePropertyIsBrowsable(nameof(FloatType), false);
        }

        protected void SetPropertyId()
        {
        }

        private void SetPropertyTexts()
        {
            //Line
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "8"));
        }

        public EFF_Table3_Property(EFF_Table3_Property prop, bool ForMultiSelection = false)
        {
            PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public EFF_Table3_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_EFF_Methods Methods, bool ForMultiSelection = false) : base()
        {
            PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_EFF_Methods Methods, bool ForMultiSelection = false)
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
                byte[] _set = new byte[8];
                byte[] insert = value.Take(8).ToArray();
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

        #endregion
    }
}
