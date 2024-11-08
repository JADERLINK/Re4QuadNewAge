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
    public class NewAge_LIT_Group_Property : BaseProperty, IInternalID
    {
        private const GroupType groupType = GroupType.LIT_GROUPS;

        private ushort InternalID = ushort.MaxValue;
        private Re4Version version = Re4Version.NULL;

        private NewAge_LIT_Group_Methods Methods = null;
        private UpdateMethods updateMethods = null;

        public override Type GetClassType()
        {
            return typeof(NewAge_LIT_Group_Property);
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
        }

        private void SetPropertyTexts()
        {
            //Line
            if (version == Re4Version.UHD)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "260"));
            }
            else if (version == Re4Version.V2007PS2)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "100"));
            }
        }

        public NewAge_LIT_Group_Property(NewAge_LIT_Group_Property prop, bool ForMultiSelection = false)
        {
            NewAge_ESE_PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public NewAge_LIT_Group_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_LIT_Group_Methods Methods, bool ForMultiSelection = false) : base()
        {
            NewAge_ESE_PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void NewAge_ESE_PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_LIT_Group_Methods Methods, bool ForMultiSelection = false)
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
        private const int CategoryID_Order = 2;
        private const int CategoryID_LineArray = 3;
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
                byte[] _set = new byte[100];
                byte[] insert = value.Take(100).ToArray();

                if (version == Re4Version.UHD)
                {
                    _set = new byte[260];
                    insert = value.Take(260).ToArray();
                }

                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                _set[0x4] = 0;
                _set[0x5] = 0;
                _set[0x6] = 0;
                _set[0x7] = 0;
                Methods.SetLine(InternalID, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region order

        [CustomCategory(aLang.LIT_Group_Order_Category)]
        [CustomDisplayName(aLang.LIT_Group_GroupOrderID_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
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

        [CustomCategory(aLang.LIT_Group_Order_Category)]
        [CustomDisplayName(aLang.LIT_Group_LIT_GetEntryCountInGroup_Int_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID_Order)]
        public int LIT_GetEntryCountInGroup
        {
            get => Methods.GetEntryCountInGroup(InternalID);
        }

        #endregion

    }
}
