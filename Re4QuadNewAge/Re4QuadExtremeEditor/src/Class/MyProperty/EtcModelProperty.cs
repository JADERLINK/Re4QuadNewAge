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
    public class EtcModelProperty : BaseProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(EtcModelProperty);
        }

        private const GroupType groupType = GroupType.ETS;

        private ushort InternalID = ushort.MaxValue;
        private Re4Version version = Re4Version.NULL;

        private EtcModelMethods Methods = null;
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
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_X), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_Y), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_Z), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleZ), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY), !IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ), !IsHex);

            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_X_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_Y_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_Z_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_AngleZ_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionX_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(PROP_PositionZ_Hex), IsHex);

            if (version == Re4Version.V2007PS2)
            {
                ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_W), !IsHex);
                ChangePropertyIsBrowsable(nameof(PROP_AngleW), !IsHex);
                ChangePropertyIsBrowsable(nameof(PROP_PositionW), !IsHex);

                ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_W_Hex), IsHex);
                ChangePropertyIsBrowsable(nameof(PROP_AngleW_Hex), IsHex);
                ChangePropertyIsBrowsable(nameof(PROP_PositionW_Hex), IsHex);
            }
        }

        private void SetClassic() 
        {
            ChangePropertyIsBrowsable(nameof(UnusedsInfo), true);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTJ), true);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTH), true);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTG), true);

            ChangePropertyDescription(nameof(PROP_EtcModelID), Lang.GetAttributeText(aLang.EtcModelID_Ushort_Description).Replace(" <<Offset1>>", "0x31").Replace("<<Offset2>>", "0x30"));
            ChangePropertyDescription(nameof(PROP_EtcModelID_ListBox), Lang.GetAttributeText(aLang.EtcModelID_Ushort_Description).Replace(" <<Offset1>>", "0x31").Replace("<<Offset2>>", "0x30"));
            ChangePropertyDescription(nameof(PROP_ETS_ID), Lang.GetAttributeText(aLang.ETS_ID_Ushort_Description).Replace(" <<Offset1>>", "0x33").Replace("<<Offset2>>", "0x32"));

            ChangePropertyDescription(nameof(PROP_Unknown_TTS_X), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_X_Description).Replace(" <<Offset1>>", "0x03").Replace("<<Offset2>>", "0x02").Replace("<<Offset3>>", "0x01").Replace("<<Offset4>>", "0x00"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Y), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Y_Description).Replace("<<Offset1>>", "0x07").Replace("<<Offset2>>", "0x06").Replace("<<Offset3>>", "0x05").Replace("<<Offset4>>", "0x04"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Z), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Z_Description).Replace("<<Offset1>>", "0x0B").Replace("<<Offset2>>", "0x0A").Replace("<<Offset3>>", "0x09").Replace("<<Offset4>>", "0x08"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_W), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_W_Description).Replace("<<Offset1>>", "0x0F").Replace("<<Offset2>>", "0x0E").Replace("<<Offset3>>", "0x0D").Replace("<<Offset4>>", "0x0C"));

            ChangePropertyDescription(nameof(PROP_AngleX), Lang.GetAttributeText(aLang.ETCM_AngleX_Description).Replace("<<Offset1>>", "0x13").Replace("<<Offset2>>", "0x12").Replace("<<Offset3>>", "0x11").Replace("<<Offset4>>", "0x10"));
            ChangePropertyDescription(nameof(PROP_AngleY), Lang.GetAttributeText(aLang.ETCM_AngleY_Description).Replace("<<Offset1>>", "0x17").Replace("<<Offset2>>", "0x16").Replace("<<Offset3>>", "0x15").Replace("<<Offset4>>", "0x14"));
            ChangePropertyDescription(nameof(PROP_AngleZ), Lang.GetAttributeText(aLang.ETCM_AngleZ_Description).Replace("<<Offset1>>", "0x1B").Replace("<<Offset2>>", "0x1A").Replace("<<Offset3>>", "0x19").Replace("<<Offset4>>", "0x18"));
            ChangePropertyDescription(nameof(PROP_AngleW), Lang.GetAttributeText(aLang.ETCM_AngleW_Description).Replace("<<Offset1>>", "0x1F").Replace("<<Offset2>>", "0x1E").Replace("<<Offset3>>", "0x1D").Replace("<<Offset4>>", "0x1C"));

            ChangePropertyDescription(nameof(PROP_PositionX), Lang.GetAttributeText(aLang.ETCM_PositionX_Description).Replace("<<Offset1>>", "0x23").Replace("<<Offset2>>", "0x22").Replace("<<Offset3>>", "0x21").Replace("<<Offset4>>", "0x20"));
            ChangePropertyDescription(nameof(PROP_PositionY), Lang.GetAttributeText(aLang.ETCM_PositionY_Description).Replace("<<Offset1>>", "0x27").Replace("<<Offset2>>", "0x26").Replace("<<Offset3>>", "0x25").Replace("<<Offset4>>", "0x24"));
            ChangePropertyDescription(nameof(PROP_PositionZ), Lang.GetAttributeText(aLang.ETCM_PositionZ_Description).Replace("<<Offset1>>", "0x2B").Replace("<<Offset2>>", "0x2A").Replace("<<Offset3>>", "0x29").Replace("<<Offset4>>", "0x28"));
            ChangePropertyDescription(nameof(PROP_PositionW), Lang.GetAttributeText(aLang.ETCM_PositionW_Description).Replace("<<Offset1>>", "0x2F").Replace("<<Offset2>>", "0x2E").Replace("<<Offset3>>", "0x2D").Replace("<<Offset4>>", "0x2C"));

            ChangePropertyDescription(nameof(PROP_Unknown_TTS_X_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_X_Description).Replace("<<Offset1>>", "0x03").Replace("<<Offset2>>", "0x02").Replace("<<Offset3>>", "0x01").Replace("<<Offset4>>", "0x00"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Y_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Y_Description).Replace("<<Offset1>>", "0x07").Replace("<<Offset2>>", "0x06").Replace("<<Offset3>>", "0x05").Replace("<<Offset4>>", "0x04"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Z_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Z_Description).Replace("<<Offset1>>", "0x0B").Replace("<<Offset2>>", "0x0A").Replace("<<Offset3>>", "0x09").Replace("<<Offset4>>", "0x08"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_W_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_W_Description).Replace("<<Offset1>>", "0x0F").Replace("<<Offset2>>", "0x0E").Replace("<<Offset3>>", "0x0D").Replace("<<Offset4>>", "0x0C"));

            ChangePropertyDescription(nameof(PROP_AngleX_Hex), Lang.GetAttributeText(aLang.ETCM_AngleX_Description).Replace("<<Offset1>>", "0x13").Replace("<<Offset2>>", "0x12").Replace("<<Offset3>>", "0x11").Replace("<<Offset4>>", "0x10"));
            ChangePropertyDescription(nameof(PROP_AngleY_Hex), Lang.GetAttributeText(aLang.ETCM_AngleY_Description).Replace("<<Offset1>>", "0x17").Replace("<<Offset2>>", "0x16").Replace("<<Offset3>>", "0x15").Replace("<<Offset4>>", "0x14"));
            ChangePropertyDescription(nameof(PROP_AngleZ_Hex), Lang.GetAttributeText(aLang.ETCM_AngleZ_Description).Replace("<<Offset1>>", "0x1B").Replace("<<Offset2>>", "0x1A").Replace("<<Offset3>>", "0x19").Replace("<<Offset4>>", "0x18"));
            ChangePropertyDescription(nameof(PROP_AngleW_Hex), Lang.GetAttributeText(aLang.ETCM_AngleW_Description).Replace("<<Offset1>>", "0x1F").Replace("<<Offset2>>", "0x1E").Replace("<<Offset3>>", "0x1D").Replace("<<Offset4>>", "0x1C"));

            ChangePropertyDescription(nameof(PROP_PositionX_Hex), Lang.GetAttributeText(aLang.ETCM_PositionX_Description).Replace("<<Offset1>>", "0x23").Replace("<<Offset2>>", "0x22").Replace("<<Offset3>>", "0x21").Replace("<<Offset4>>", "0x20"));
            ChangePropertyDescription(nameof(PROP_PositionY_Hex), Lang.GetAttributeText(aLang.ETCM_PositionY_Description).Replace("<<Offset1>>", "0x27").Replace("<<Offset2>>", "0x26").Replace("<<Offset3>>", "0x25").Replace("<<Offset4>>", "0x24"));
            ChangePropertyDescription(nameof(PROP_PositionZ_Hex), Lang.GetAttributeText(aLang.ETCM_PositionZ_Description).Replace("<<Offset1>>", "0x2B").Replace("<<Offset2>>", "0x2A").Replace("<<Offset3>>", "0x29").Replace("<<Offset4>>", "0x28"));
            ChangePropertyDescription(nameof(PROP_PositionW_Hex), Lang.GetAttributeText(aLang.ETCM_PositionW_Description).Replace("<<Offset1>>", "0x2F").Replace("<<Offset2>>", "0x2E").Replace("<<Offset3>>", "0x2D").Replace("<<Offset4>>", "0x2C"));

            ChangePropertyDescription(nameof(PROP_Unknown_TTJ), Lang.GetAttributeText(aLang.ETCM_Unknown_TTJ_ByteArray4_Description).Replace("<<Offset1>>", "0x34").Replace("<<Offset2>>", "0x35").Replace("<<Offset3>>", "0x36").Replace("<<Offset4>>", "0x37"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTH), Lang.GetAttributeText(aLang.ETCM_Unknown_TTH_ByteArray4_Description).Replace("<<Offset1>>", "0x38").Replace("<<Offset2>>", "0x39").Replace("<<Offset3>>", "0x3A").Replace("<<Offset4>>", "0x3B"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTG), Lang.GetAttributeText(aLang.ETCM_Unknown_TTG_ByteArray4_Description).Replace("<<Offset1>>", "0x3C").Replace("<<Offset2>>", "0x3D").Replace("<<Offset3>>", "0x3E").Replace("<<Offset4>>", "0x3F"));

            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "64"));
        }

        private void SetUHD()
        {
            ChangePropertyIsBrowsable(nameof(UnusedsInfo), false);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_W), false);
            ChangePropertyIsBrowsable(nameof(PROP_AngleW), false);
            ChangePropertyIsBrowsable(nameof(PROP_PositionW), false);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTS_W_Hex), false);
            ChangePropertyIsBrowsable(nameof(PROP_AngleW_Hex), false);
            ChangePropertyIsBrowsable(nameof(PROP_PositionW_Hex), false);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTJ), false);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTH), false);
            ChangePropertyIsBrowsable(nameof(PROP_Unknown_TTG), false);

            ChangePropertyDescription(nameof(PROP_EtcModelID), Lang.GetAttributeText(aLang.EtcModelID_Ushort_Description).Replace("<<Offset1>>", "0x01").Replace("<<Offset2>>", "0x00"));
            ChangePropertyDescription(nameof(PROP_EtcModelID_ListBox), Lang.GetAttributeText(aLang.EtcModelID_Ushort_Description).Replace("<<Offset1>>", "0x01").Replace("<<Offset2>>", "0x00"));
            ChangePropertyDescription(nameof(PROP_ETS_ID), Lang.GetAttributeText(aLang.ETS_ID_Ushort_Description).Replace("<<Offset1>>", "0x03").Replace("<<Offset2>>", "0x02"));

            ChangePropertyDescription(nameof(PROP_Unknown_TTS_X), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_X_Description).Replace("<<Offset1>>", "0x07").Replace("<<Offset2>>", "0x06").Replace("<<Offset3>>", "0x05").Replace("<<Offset4>>", "0x04"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Y), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Y_Description).Replace("<<Offset1>>", "0x0B").Replace("<<Offset2>>", "0x0A").Replace("<<Offset3>>", "0x09").Replace("<<Offset4>>", "0x08"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Z), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Z_Description).Replace("<<Offset1>>", "0x0F").Replace("<<Offset2>>", "0x0E").Replace("<<Offset3>>", "0x0D").Replace("<<Offset4>>", "0x0C"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_W), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_W_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(PROP_AngleX), Lang.GetAttributeText(aLang.ETCM_AngleX_Description).Replace("<<Offset1>>", "0x13").Replace("<<Offset2>>", "0x12").Replace("<<Offset3>>", "0x11").Replace("<<Offset4>>", "0x10"));
            ChangePropertyDescription(nameof(PROP_AngleY), Lang.GetAttributeText(aLang.ETCM_AngleY_Description).Replace("<<Offset1>>", "0x17").Replace("<<Offset2>>", "0x16").Replace("<<Offset3>>", "0x15").Replace("<<Offset4>>", "0x14"));
            ChangePropertyDescription(nameof(PROP_AngleZ), Lang.GetAttributeText(aLang.ETCM_AngleZ_Description).Replace("<<Offset1>>", "0x1B").Replace("<<Offset2>>", "0x1A").Replace("<<Offset3>>", "0x19").Replace("<<Offset4>>", "0x18"));
            ChangePropertyDescription(nameof(PROP_AngleW), Lang.GetAttributeText(aLang.ETCM_AngleW_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(PROP_PositionX), Lang.GetAttributeText(aLang.ETCM_PositionX_Description).Replace("<<Offset1>>", "0x1F").Replace("<<Offset2>>", "0x1E").Replace("<<Offset3>>", "0x1D").Replace("<<Offset4>>", "0x1C"));
            ChangePropertyDescription(nameof(PROP_PositionY), Lang.GetAttributeText(aLang.ETCM_PositionY_Description).Replace("<<Offset1>>", "0x23").Replace("<<Offset2>>", "0x22").Replace("<<Offset3>>", "0x21").Replace("<<Offset4>>", "0x20"));
            ChangePropertyDescription(nameof(PROP_PositionZ), Lang.GetAttributeText(aLang.ETCM_PositionZ_Description).Replace("<<Offset1>>", "0x27").Replace("<<Offset2>>", "0x26").Replace("<<Offset3>>", "0x25").Replace("<<Offset4>>", "0x24"));
            ChangePropertyDescription(nameof(PROP_PositionW), Lang.GetAttributeText(aLang.ETCM_PositionW_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(PROP_Unknown_TTS_X_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_X_Description).Replace("<<Offset1>>", "0x07").Replace("<<Offset2>>", "0x06").Replace("<<Offset3>>", "0x05").Replace("<<Offset4>>", "0x04"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Y_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Y_Description).Replace("<<Offset1>>", "0x0B").Replace("<<Offset2>>", "0x0A").Replace("<<Offset3>>", "0x09").Replace("<<Offset4>>", "0x08"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_Z_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_Z_Description).Replace("<<Offset1>>", "0x0F").Replace("<<Offset2>>", "0x0E").Replace("<<Offset3>>", "0x0D").Replace("<<Offset4>>", "0x0C"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTS_W_Hex), Lang.GetAttributeText(aLang.ETCM_Unknown_TTS_W_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(PROP_AngleX_Hex), Lang.GetAttributeText(aLang.ETCM_AngleX_Description).Replace("<<Offset1>>", "0x13").Replace("<<Offset2>>", "0x12").Replace("<<Offset3>>", "0x11").Replace("<<Offset4>>", "0x10"));
            ChangePropertyDescription(nameof(PROP_AngleY_Hex), Lang.GetAttributeText(aLang.ETCM_AngleY_Description).Replace("<<Offset1>>", "0x17").Replace("<<Offset2>>", "0x16").Replace("<<Offset3>>", "0x15").Replace("<<Offset4>>", "0x14"));
            ChangePropertyDescription(nameof(PROP_AngleZ_Hex), Lang.GetAttributeText(aLang.ETCM_AngleZ_Description).Replace("<<Offset1>>", "0x1B").Replace("<<Offset2>>", "0x1A").Replace("<<Offset3>>", "0x19").Replace("<<Offset4>>", "0x18"));
            ChangePropertyDescription(nameof(PROP_AngleW_Hex), Lang.GetAttributeText(aLang.ETCM_AngleW_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(PROP_PositionX_Hex), Lang.GetAttributeText(aLang.ETCM_PositionX_Description).Replace("<<Offset1>>", "0x1F").Replace("<<Offset2>>", "0x1E").Replace("<<Offset3>>", "0x1D").Replace("<<Offset4>>", "0x1C"));
            ChangePropertyDescription(nameof(PROP_PositionY_Hex), Lang.GetAttributeText(aLang.ETCM_PositionY_Description).Replace("<<Offset1>>", "0x23").Replace("<<Offset2>>", "0x22").Replace("<<Offset3>>", "0x21").Replace("<<Offset4>>", "0x20"));
            ChangePropertyDescription(nameof(PROP_PositionZ_Hex), Lang.GetAttributeText(aLang.ETCM_PositionZ_Description).Replace("<<Offset1>>", "0x27").Replace("<<Offset2>>", "0x26").Replace("<<Offset3>>", "0x25").Replace("<<Offset4>>", "0x24"));
            ChangePropertyDescription(nameof(PROP_PositionW_Hex), Lang.GetAttributeText(aLang.ETCM_PositionW_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(PROP_Unknown_TTJ), Lang.GetAttributeText(aLang.ETCM_Unknown_TTJ_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTH), Lang.GetAttributeText(aLang.ETCM_Unknown_TTH_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(PROP_Unknown_TTG), Lang.GetAttributeText(aLang.ETCM_Unknown_TTG_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "40"));
        }

        private void UpdatePropertyStatus() 
        {
            SetFloatType(Globals.PropertyGridUseHexFloat);
            if (version == Re4Version.UHD) { SetUHD(); }
            else if (version == Re4Version.V2007PS2) { SetClassic(); }
        }

        public EtcModelProperty(EtcModelProperty prop, bool ForMultiSelection = false)
        {
            EtcModelPropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public EtcModelProperty(ushort InternalID, UpdateMethods updateMethods, EtcModelMethods Methods, bool ForMultiSelection = false) : base()
        {
            EtcModelPropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void EtcModelPropertyConstructor(ushort InternalID, UpdateMethods updateMethods, EtcModelMethods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.updateMethods = updateMethods;
            this.Methods = Methods;
            version = Methods.ReturnRe4Version();

            if (!ForMultiSelection)
            {
                SetThis(this);
            }

            UpdatePropertyStatus();
        }

        #region Category Ids
        private const int CategoryID0_InternalLineID = 0;
        private const int CategoryID1_AssociatedSpecialEvent = 1;
        private const int CategoryID2_LineArray = 2;
        private const int CategoryID3_EtcModel = 3;
        private const int CategoryID4_FloatType = 4;
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


        [CustomCategory(aLang.EtcModel_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.EtcModel_AssociatedSpecialEventTypeDisplayName)]
        [CustomDescription(aLang.EtcModel_AssociatedSpecialEventTypeDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventType { get { return DataBase.Extras.AssociatedSpecialEventType(RefInteractionType.EtcModel, PROP_ETS_ID); } }


        [CustomCategory(aLang.EtcModel_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.EtcModel_AssociatedSpecialEventFromSpecialIndexDisplayName)]
        [CustomDescription(aLang.EtcModel_AssociatedSpecialEventFromSpecialIndexFromDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(4, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventFromSpecialIndex { get { return DataBase.Extras.AssociatedSpecialEventFromSpecialIndex(RefInteractionType.EtcModel, PROP_ETS_ID); } }


        [CustomCategory(aLang.EtcModel_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.EtcModel_AssociatedSpecialEventObjNameDisplayName)]
        [CustomDescription(aLang.EtcModel_AssociatedSpecialEventObjNameDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(5, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventObjName { get { return DataBase.Extras.AssociatedSpecialEventObjName(RefInteractionType.EtcModel, PROP_ETS_ID); } }

        [CustomCategory(aLang.EtcModel_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.EtcModel_AssociatedSpecialEventFromFileDisplayName)]
        [CustomDescription(aLang.EtcModel_AssociatedSpecialEventFromFileDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(6, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventFromFile { get { return DataBase.Extras.AssociatedSpecialEventFromFile(RefInteractionType.EtcModel, PROP_ETS_ID); } }



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
                byte[] _set = new byte[64];
                byte[] insert = value.Take(64).ToArray();
                if (version == Re4Version.UHD)
                {
                    _set = new byte[40];
                    insert = value.Take(40).ToArray();
                }
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion
       
        // propriedades do etcmodel
        #region Etcmodel ID and ETS ID

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.EtcModelID_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(100, CategoryID3_EtcModel)]
        public ushort PROP_EtcModelID
        {
            get => Methods.ReturnEtcModelID(InternalID);
            set
            {
                Methods.SetEtcModelID(InternalID, value);
                updateMethods.UpdateGL();
            }
        }



        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.EtcModelID_List_DisplayName)]
        [Editor(typeof(EtcModelIDGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(200, CategoryID3_EtcModel)]
        public UshortObjForListBox PROP_EtcModelID_ListBox
        {
            get
            {
                ushort v = Methods.ReturnEtcModelID(InternalID);
                if (ListBoxProperty.EtcmodelsList.ContainsKey(v) && v != 0xFFFF)
                {
                    return ListBoxProperty.EtcmodelsList[v];
                }
                else
                {
                    return new UshortObjForListBox(0xFFFF, "XXXX: " + Lang.GetAttributeText(aLang.ListBoxUnknownEtcModel));
                }
            }
            set
            {
                if (value.ID < 0xFFFF)
                {
                    Methods.SetEtcModelID(InternalID, value.ID);
                    updateMethods.UpdateGL();
                }
            }
        }



        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETS_ID_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(300, CategoryID3_EtcModel)]
        public ushort PROP_ETS_ID
        {
            get => Methods.ReturnETS_ID(InternalID);
            set
            {
                Methods.SetETS_ID(InternalID, value);
            }
        }

        #endregion

        #region float Scale, Angle, Position

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(400, CategoryID3_EtcModel)]
        public float PROP_Unknown_TTS_X
        {
            get => Methods.ReturnUnknown_TTS_X(InternalID);
            set
            {
                Methods.SetUnknown_TTS_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_Y_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(500, CategoryID3_EtcModel)]
        public float PROP_Unknown_TTS_Y
        {
            get => Methods.ReturnUnknown_TTS_Y(InternalID);
            set
            {
                Methods.SetUnknown_TTS_Y(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(600, CategoryID3_EtcModel)]
        public float PROP_Unknown_TTS_Z
        {
            get => Methods.ReturnUnknown_TTS_Z(InternalID);
            set
            {
                Methods.SetUnknown_TTS_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(700, CategoryID3_EtcModel)]
        public float PROP_AngleX
        {
            get => Methods.ReturnAngleX(InternalID);
            set
            {
                Methods.SetAngleX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(800, CategoryID3_EtcModel)]
        public float PROP_AngleY
        {
            get => Methods.ReturnAngleY(InternalID);
            set
            {
                Methods.SetAngleY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(900, CategoryID3_EtcModel)]
        public float PROP_AngleZ
        {
            get => Methods.ReturnAngleZ(InternalID);
            set
            {
                Methods.SetAngleZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1000, CategoryID3_EtcModel)]
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

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1100, CategoryID3_EtcModel)]
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


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1200, CategoryID3_EtcModel)]
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

        #endregion

        #region uint Hex Scale, Angle, Position

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1300, CategoryID3_EtcModel)]
        public uint PROP_Unknown_TTS_X_Hex
        {
            get => Methods.ReturnUnknown_TTS_X_Hex(InternalID);
            set
            {
                Methods.SetUnknown_TTS_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_Y_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1400, CategoryID3_EtcModel)]
        public uint PROP_Unknown_TTS_Y_Hex
        {
            get => Methods.ReturnUnknown_TTS_Y_Hex(InternalID);
            set
            {
                Methods.SetUnknown_TTS_Y_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1500, CategoryID3_EtcModel)]
        public uint PROP_Unknown_TTS_Z_Hex
        {
            get => Methods.ReturnUnknown_TTS_Z_Hex(InternalID);
            set
            {
                Methods.SetUnknown_TTS_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1600, CategoryID3_EtcModel)]
        public uint PROP_AngleX_Hex
        {
            get => Methods.ReturnAngleX_Hex(InternalID);
            set
            {
                Methods.SetAngleX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1700, CategoryID3_EtcModel)]
        public uint PROP_AngleY_Hex
        {
            get => Methods.ReturnAngleY_Hex(InternalID);
            set
            {
                Methods.SetAngleY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1800, CategoryID3_EtcModel)]
        public uint PROP_AngleZ_Hex
        {
            get => Methods.ReturnAngleZ_Hex(InternalID);
            set
            {
                Methods.SetAngleZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(1900, CategoryID3_EtcModel)]
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

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2000, CategoryID3_EtcModel)]
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

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2100, CategoryID3_EtcModel)]
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

        #region unuseds bytes

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_UnusedsInfo_DisplayName)]
        [CustomDescription(aLang.ETCM_UnusedsInfo_Description)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(2200, CategoryID3_EtcModel)]
        public string UnusedsInfo { get => Lang.GetAttributeText(aLang.ETCM_UnusedsInfo_Text); }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_W_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2300, CategoryID3_EtcModel)]
        public float PROP_Unknown_TTS_W
        {
            get => Methods.ReturnUnknown_TTS_W(InternalID);
            set
            {
                Methods.SetUnknown_TTS_W(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleW_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2400, CategoryID3_EtcModel)]
        public float PROP_AngleW
        {
            get => Methods.ReturnAngleW(InternalID);
            set
            {
                Methods.SetAngleW(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionW_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2500, CategoryID3_EtcModel)]
        public float PROP_PositionW
        {
            get => Methods.ReturnPositionW(InternalID);
            set
            {
                Methods.SetPositionW(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTS_W_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2600, CategoryID3_EtcModel)]
        public uint PROP_Unknown_TTS_W_Hex
        {
            get => Methods.ReturnUnknown_TTS_W_Hex(InternalID);
            set
            {
                Methods.SetUnknown_TTS_W_Hex(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_AngleW_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2700, CategoryID3_EtcModel)]
        public uint PROP_AngleW_Hex
        {
            get => Methods.ReturnAngleW_Hex(InternalID);
            set
            {
                Methods.SetAngleW_Hex(InternalID, value);
                
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_PositionW_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2800, CategoryID3_EtcModel)]
        public uint PROP_PositionW_Hex
        {
            get => Methods.ReturnPositionW_Hex(InternalID);
            set
            {
                Methods.SetPositionW_Hex(InternalID, value);
                
            }
        }


        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTJ_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2901, CategoryID3_EtcModel)]
        public byte[] PROP_Unknown_TTJ
        {
            get => Methods.ReturnUnknown_TTJ(InternalID);
            set
            {
                byte[] _set = new byte[4];
                PROP_Unknown_TTJ.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TTJ(InternalID, _set);

            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTH_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2902, CategoryID3_EtcModel)]
        public byte[] PROP_Unknown_TTH
        {
            get => Methods.ReturnUnknown_TTH(InternalID);
            set
            {
                byte[] _set = new byte[4];
                PROP_Unknown_TTH.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TTH(InternalID, _set);

            }
        }

        [CustomCategory(aLang.EtcModelCategory)]
        [CustomDisplayName(aLang.ETCM_Unknown_TTG_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2903, CategoryID3_EtcModel)]
        public byte[] PROP_Unknown_TTG
        {
            get => Methods.ReturnUnknown_TTG(InternalID);
            set
            {
                byte[] _set = new byte[4];
                PROP_Unknown_TTG.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TTG(InternalID, _set);

            }
        }

        #endregion

        #region Search Methods


        public ushort ReturnUshortFirstSearchSelect()
        {
            return Methods.ReturnEtcModelID(InternalID);
        }

        public void Searched(object obj)
        {
            if (obj is UshortObjForListBox ushortObj)
            {
                Methods.SetEtcModelID(InternalID, ushortObj.ID);
                updateMethods.UpdateTreeViewObjs();
                updateMethods.UpdatePropertyGrid();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion

    }
}
