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
    public class NewAge_ESAR_Property : BaseTriggerZoneProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(NewAge_ESAR_Property);
        }

        private ushort InternalID = ushort.MaxValue;
        private GroupType groupType = GroupType.NULL;
        private EsarFileFormat esarFileFormat = EsarFileFormat.NULL;

        private NewAge_ESAR_Methods Methods = null;

        public override ushort GetInternalID()
        {
            return InternalID;
        }

        public override GroupType GetGroupType()
        {
            return groupType;
        }

        public EsarFileFormat GetEsarFileFormat()
        {
            return esarFileFormat;
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
            if (esarFileFormat == EsarFileFormat.EAR)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "152"));
            }
            else if (esarFileFormat == EsarFileFormat.SAR)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "216"));
            }
        }

        public NewAge_ESAR_Property(NewAge_ESAR_Property prop, bool ForMultiSelection = false)
        {
            NewAge_ESAR_PropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public NewAge_ESAR_Property(ushort InternalID, UpdateMethods updateMethods, NewAge_ESAR_Methods Methods, bool ForMultiSelection = false) : base()
        {
            NewAge_ESAR_PropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void NewAge_ESAR_PropertyConstructor(ushort InternalID, UpdateMethods updateMethods, NewAge_ESAR_Methods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;

            esarFileFormat = Methods.GetEsarFileFormat();

            switch (esarFileFormat)
            {
                case EsarFileFormat.SAR:
                    groupType = GroupType.SAR;
                    break;
                case EsarFileFormat.EAR:
                    groupType = GroupType.EAR;
                    break;
            }

            if (!ForMultiSelection)
            {
                SetThis(this);
            }

            SetFloatType(Globals.PropertyGridUseHexFloat);
            SetTriggerZoneDescription(0x04);
            SetPropertyTexts();
        }


        #region Category Ids
        private const int CategoryID_UnderDevelopment = 0;
        private const int CategoryID_InternalLineID = 1;
        private const int CategoryID_LineArray = 2;
        private const int CategoryID_ESAR = 11;
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
                //sar
                byte[] _set = new byte[216];
                byte[] insert = value.Take(216).ToArray();

                if (esarFileFormat == EsarFileFormat.EAR)
                {
                    _set = new byte[152];
                    insert = value.Take(152).ToArray();
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
    }
}
