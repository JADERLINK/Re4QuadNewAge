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
    public class NewAge_FSE_Property : BaseTriggerZoneProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(NewAge_FSE_Property);
        }

        private const GroupType groupType = GroupType.FSE;

        private ushort InternalID = ushort.MaxValue;

        private NewAge_FSE_Methods Methods = null;

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

        }

        private void SetPropertyTexts() 
        {
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "132"));
        }

        public NewAge_FSE_Property(NewAge_FSE_Property prop, bool ForMultiSelection = false)
        {
            NewAge_FSE_PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public NewAge_FSE_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_FSE_Methods Methods, bool ForMultiSelection = false) : base()
        {
            NewAge_FSE_PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void NewAge_FSE_PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_FSE_Methods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;

            if (!ForMultiSelection)
            {
                SetThis(this);
            }

            SetFloatType(Globals.PropertyGridUseHexFloat);
            SetTriggerZoneDescription(0x14);
            SetPropertyTexts();
        }


        #region Category Ids
        private const int CategoryID_UnderDevelopment = 0;
        private const int CategoryID_InternalLineID = 1;
        private const int CategoryID_LineArray = 2;
        private const int CategoryID_FSE = 11;
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
                byte[] _set = new byte[132];
                byte[] insert = value.Take(132).ToArray();
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);

                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion
    }
}
