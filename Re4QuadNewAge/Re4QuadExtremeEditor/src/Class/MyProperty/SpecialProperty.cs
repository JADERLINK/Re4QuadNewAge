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
    [DefaultProperty("InternalLineID")]
    [TypeConverter(typeof(GenericConverter))]
    public class SpecialProperty : GenericProperty, IInternalID
    {
        public event CustomDelegates.ActivateMethod UpdateSetFloatTypeEvent;

        private ushort InternalID = ushort.MaxValue;
        private GroupType groupType = GroupType.NULL;
        private SpecialFileFormat specialFileFormat = SpecialFileFormat.NULL;
        private Re4Version version = Re4Version.Null;

        private SpecialMethods Methods = null;
        private UpdateMethods updateMethods = null;

        private bool IsExtra = false;

        public ushort GetInternalID()
        {
            return InternalID;
        }

        public GroupType GetGroupType()
        {
            return groupType;
        }

        public SpecialFileFormat GetSpecialFileFormat()
        {
            return specialFileFormat;
        }

        public void UpdateSetFloatType() 
        {
            SetFloatType(Globals.PropertyGridUseHexFloat);
        }

        void SetFloatType(bool IsHex)
        {
            ChangePropertyIsBrowsable("TriggerZoneTrueY", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneMoreHeight", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCircleRadius", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner0_X", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner0_Z", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner1_X", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner1_Z", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner2_X", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner2_Z", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner3_X", !IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner3_Z", !IsHex && !IsExtra);

            ChangePropertyIsBrowsable("TriggerZoneTrueY_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneMoreHeight_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCircleRadius_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner0_X_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner0_Z_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner1_X_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner1_Z_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner2_X_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner2_Z_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner3_X_Hex", IsHex && !IsExtra);
            ChangePropertyIsBrowsable("TriggerZoneCorner3_Z_Hex", IsHex && !IsExtra);

            // types
            bool ifItem = Methods.GetSpecialType(InternalID) == SpecialType.T03_Items;
            bool IsWarpDoor = Methods.GetSpecialType(InternalID) == SpecialType.T01_WarpDoor;
            bool IsLocalTeleportation = Methods.GetSpecialType(InternalID) == SpecialType.T13_LocalTeleportation;
            bool IsLadderUp = Methods.GetSpecialType(InternalID) == SpecialType.T10_FixedLadderClimbUp;
            bool IsAshley = Methods.GetSpecialType(InternalID) == SpecialType.T12_AshleyHideCommand;
            bool IsGrappleGun = Methods.GetSpecialType(InternalID) == SpecialType.T15_AdaGrappleGun;

            bool ifClassicAev = !(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV);
            bool ItemFloat = ifItem && !IsHex;
            bool ItemHex = ifItem && IsHex;

            ChangePropertyIsBrowsable("ObjPointX", (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && !IsHex);
            ChangePropertyIsBrowsable("ObjPointY", (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && !IsHex);
            ChangePropertyIsBrowsable("ObjPointZ", (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && !IsHex);
            ChangePropertyIsBrowsable("ObjPointX_Hex", (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && IsHex);
            ChangePropertyIsBrowsable("ObjPointY_Hex", (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && IsHex);
            ChangePropertyIsBrowsable("ObjPointZ_Hex", (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && IsHex);


            ChangePropertyIsBrowsable("Unknown_RI_X", ItemFloat);
            ChangePropertyIsBrowsable("Unknown_RI_Y", ItemFloat);
            ChangePropertyIsBrowsable("Unknown_RI_Z", ItemFloat);
            ChangePropertyIsBrowsable("Unknown_RI_X_Hex", ItemHex);
            ChangePropertyIsBrowsable("Unknown_RI_Y_Hex", ItemHex);
            ChangePropertyIsBrowsable("Unknown_RI_Z_Hex", ItemHex);


            ChangePropertyIsBrowsable("ItemTriggerRadius", ItemFloat);
            ChangePropertyIsBrowsable("ItemAngleX", ItemFloat);
            ChangePropertyIsBrowsable("ItemAngleY", ItemFloat);
            ChangePropertyIsBrowsable("ItemAngleZ", ItemFloat && ifClassicAev);
            ChangePropertyIsBrowsable("ItemTriggerRadius_Hex", ItemHex);
            ChangePropertyIsBrowsable("ItemAngleX_Hex", ItemHex);
            ChangePropertyIsBrowsable("ItemAngleY_Hex", ItemHex);
            ChangePropertyIsBrowsable("ItemAngleZ_Hex", ItemHex && ifClassicAev);


            ChangePropertyIsBrowsable("DestinationFacingAngle", IsWarpDoor && !IsHex);
            ChangePropertyIsBrowsable("DestinationFacingAngle_Hex", IsWarpDoor && IsHex);

            ChangePropertyIsBrowsable("LocalTeleportationFacingAngle", IsLocalTeleportation && !IsHex);
            ChangePropertyIsBrowsable("LocalTeleportationFacingAngle_Hex", IsLocalTeleportation && IsHex);

            ChangePropertyIsBrowsable("LadderFacingAngle", IsLadderUp && !IsHex);
            ChangePropertyIsBrowsable("LadderFacingAngle_Hex", IsLadderUp && IsHex);

            //IsAshley
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner0_X", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner0_Z", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner1_X", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner1_Z", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner2_X", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner2_Z", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner3_X", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner3_Z", IsAshley && !IsHex);

            ChangePropertyIsBrowsable("AshleyHidingZoneCorner0_X_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner0_Z_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner1_X_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner1_Z_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner2_X_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner2_Z_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner3_X_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingZoneCorner3_Z_Hex", IsAshley && IsHex);

            ChangePropertyIsBrowsable("AshleyHidingPointX", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingPointY", IsAshley && !IsHex);
            ChangePropertyIsBrowsable("AshleyHidingPointZ", IsAshley && !IsHex);

            ChangePropertyIsBrowsable("AshleyHidingPointX_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingPointY_Hex", IsAshley && IsHex);
            ChangePropertyIsBrowsable("AshleyHidingPointZ_Hex", IsAshley && IsHex);

            //IsGrappleGun
            ChangePropertyIsBrowsable("GrappleGunEndPointX", IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable("GrappleGunEndPointY", IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable("GrappleGunEndPointZ", IsGrappleGun && !IsHex);

            ChangePropertyIsBrowsable("GrappleGunEndPointX_Hex", IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable("GrappleGunEndPointY_Hex", IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable("GrappleGunEndPointZ_Hex", IsGrappleGun && IsHex);

            ChangePropertyIsBrowsable("GrappleGunThirdPointX", IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable("GrappleGunThirdPointY", IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable("GrappleGunThirdPointZ", IsGrappleGun && !IsHex);

            ChangePropertyIsBrowsable("GrappleGunThirdPointX_Hex", IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable("GrappleGunThirdPointY_Hex", IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable("GrappleGunThirdPointZ_Hex", IsGrappleGun && IsHex);

            ChangePropertyIsBrowsable("GrappleGunFacingAngle", IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable("GrappleGunFacingAngle_Hex", IsGrappleGun && IsHex);

        }

        void SetPropertyTypeEnable()
        {
            bool IsItem = Methods.GetSpecialType(InternalID) == SpecialType.T03_Items;
            bool IsWarpDoor = Methods.GetSpecialType(InternalID) == SpecialType.T01_WarpDoor;
            bool IsLocalTeleportation = Methods.GetSpecialType(InternalID) == SpecialType.T13_LocalTeleportation;
            bool IsLadderUp = Methods.GetSpecialType(InternalID) == SpecialType.T10_FixedLadderClimbUp;
            bool IsMessage = Methods.GetSpecialType(InternalID) == SpecialType.T05_Message;
            bool IsDamages = Methods.GetSpecialType(InternalID) == SpecialType.T0A_DamagesThePlayer;
            bool IsAshley = Methods.GetSpecialType(InternalID) == SpecialType.T12_AshleyHideCommand;
            bool IsGrappleGun = Methods.GetSpecialType(InternalID) == SpecialType.T15_AdaGrappleGun;
            bool IsT11 = Methods.GetSpecialType(InternalID) == SpecialType.T11_ItemDependentEvents;
            bool IsT04 = Methods.GetSpecialType(InternalID) == SpecialType.T04_GroupedEnemyTrigger;

            bool ifNotClassicAev = !(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV);

            // only itens
            ChangePropertyIsBrowsable("ObjPointW", IsItem);
            ChangePropertyIsBrowsable("Unknown_RI_W", IsItem && version == Re4Version.Classic);
            ChangePropertyIsBrowsable("Unknown_RO", IsItem && version == Re4Version.Classic);
            ChangePropertyIsBrowsable("ItemNumber", IsItem);
            ChangePropertyIsBrowsable("ItemNumber_ListBox", IsItem);
            ChangePropertyIsBrowsable("Unknown_RU", IsItem);
            ChangePropertyIsBrowsable("ItemAmount", IsItem);
            ChangePropertyIsBrowsable("SecundIndex", IsItem);
            ChangePropertyIsBrowsable("ItemAuraType", IsItem);
            ChangePropertyIsBrowsable("ItemAuraType_ListBox", IsItem);
            ChangePropertyIsBrowsable("Unknown_QM", IsItem);
            ChangePropertyIsBrowsable("Unknown_QL", IsItem);
            ChangePropertyIsBrowsable("Unknown_QR", IsItem);
            ChangePropertyIsBrowsable("Unknown_QH", IsItem);
            ChangePropertyIsBrowsable("Unknown_QG", IsItem);
            ChangePropertyIsBrowsable("ItemAngleW", IsItem && ifNotClassicAev);

            // ita classic
            ChangePropertyIsBrowsable("Unknown_VS", version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.ITA && !IsItem);
            ChangePropertyIsBrowsable("Unknown_VT", version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.ITA && !IsItem);
            ChangePropertyIsBrowsable("Unknown_VI", version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.ITA);
            ChangePropertyIsBrowsable("Unknown_VO", version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.ITA);

            ChangePropertyIsBrowsable("DestinationRoom", IsWarpDoor);
            ChangePropertyIsBrowsable("LockedDoorType", IsWarpDoor);
            ChangePropertyIsBrowsable("LockedDoorIndex", IsWarpDoor);
            ChangePropertyIsBrowsable("NeededItemNumber", IsT11);
            ChangePropertyIsBrowsable("NeededItemNumber_ListBox", IsT11);
            ChangePropertyIsBrowsable("EnemyGroup", IsT04);
            ChangePropertyIsBrowsable("RoomMessage", IsMessage);
            ChangePropertyIsBrowsable("MessageCutSceneID", IsMessage);
            ChangePropertyIsBrowsable("MessageID", IsMessage);
            ChangePropertyIsBrowsable("ActivationType", IsDamages);
            ChangePropertyIsBrowsable("DamageType", IsDamages);
            ChangePropertyIsBrowsable("BlockingType", IsDamages);
            ChangePropertyIsBrowsable("Unknown_SJ", IsDamages);
            ChangePropertyIsBrowsable("DamageAmount", IsDamages);

            //IsLadderUp || IsAshley || IsGrappleGun || IsLocalTeleportation
            ChangePropertyIsBrowsable("ObjPointW_onlyClassic", (IsLocalTeleportation || IsLadderUp || IsAshley || IsGrappleGun) && version == Re4Version.Classic);

            //IsLadderUp
            ChangePropertyIsBrowsable("LadderStepCount", IsLadderUp);
            ChangePropertyIsBrowsable("LadderParameter0", IsLadderUp);
            ChangePropertyIsBrowsable("LadderParameter1", IsLadderUp);
            ChangePropertyIsBrowsable("LadderParameter2", IsLadderUp);
            ChangePropertyIsBrowsable("LadderParameter3", IsLadderUp);
            ChangePropertyIsBrowsable("Unknown_SG", IsLadderUp);
            ChangePropertyIsBrowsable("Unknown_SH", IsLadderUp);

            //IsAshley
            ChangePropertyIsBrowsable("Unknown_SM", IsAshley);
            ChangePropertyIsBrowsable("Unknown_SN", IsAshley);
            ChangePropertyIsBrowsable("Unknown_SP", IsAshley);
            ChangePropertyIsBrowsable("Unknown_SQ", IsAshley);
            ChangePropertyIsBrowsable("Unknown_SR", IsAshley);
            ChangePropertyIsBrowsable("Unknown_SS", IsAshley);


            //IsGrappleGun
            ChangePropertyIsBrowsable("GrappleGunEndPointW", IsGrappleGun && version == Re4Version.Classic);
            ChangePropertyIsBrowsable("GrappleGunThirdPointW", IsGrappleGun && version == Re4Version.Classic);
            ChangePropertyIsBrowsable("GrappleGunParameter0", IsGrappleGun);
            ChangePropertyIsBrowsable("GrappleGunParameter1", IsGrappleGun);
            ChangePropertyIsBrowsable("GrappleGunParameter2", IsGrappleGun);
            ChangePropertyIsBrowsable("GrappleGunParameter3", IsGrappleGun);
            ChangePropertyIsBrowsable("Unknown_SK", IsGrappleGun);
            ChangePropertyIsBrowsable("Unknown_SL", IsGrappleGun);


            //OUTROS
            ChangePropertyIsBrowsable("Unknown_HH", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsT11));
            ChangePropertyIsBrowsable("Unknown_HK", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsT04 || IsMessage));
            ChangePropertyIsBrowsable("Unknown_HL", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsDamages || IsMessage));
            ChangePropertyIsBrowsable("Unknown_HM", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsDamages || IsMessage));
            ChangePropertyIsBrowsable("Unknown_HN", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsDamages));
            ChangePropertyIsBrowsable("Unknown_HR", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation));
            ChangePropertyIsBrowsable("Unknown_RH", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation));
            ChangePropertyIsBrowsable("Unknown_RJ", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation));
            ChangePropertyIsBrowsable("Unknown_RK", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || (IsLocalTeleportation && version == Re4Version.Classic)));
            ChangePropertyIsBrowsable("Unknown_RL", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || (IsLocalTeleportation && version == Re4Version.Classic)));
            ChangePropertyIsBrowsable("Unknown_RM", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp));
            ChangePropertyIsBrowsable("Unknown_RN", !(IsItem || IsGrappleGun || IsAshley || IsLadderUp));
            ChangePropertyIsBrowsable("Unknown_RP", !(IsItem || IsGrappleGun || IsAshley || (IsLadderUp && version == Re4Version.Classic)));
            ChangePropertyIsBrowsable("Unknown_RQ", !(IsItem || IsGrappleGun || IsAshley || (IsLadderUp && version == Re4Version.Classic)));
            ChangePropertyIsBrowsable("Unknown_TG", !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable("Unknown_TH", !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable("Unknown_TJ", !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable("Unknown_TK", !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable("Unknown_TL", !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable("Unknown_TM", !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable("Unknown_TN", !(IsItem || IsAshley || (IsGrappleGun && version == Re4Version.Classic)));
            ChangePropertyIsBrowsable("Unknown_TP", !(IsItem || IsAshley || (IsGrappleGun && version == Re4Version.Classic)));
            ChangePropertyIsBrowsable("Unknown_TQ", !(IsItem || (IsGrappleGun && version == Re4Version.Classic) || (IsAshley && version == Re4Version.Classic)));
        }

        void SetPropertyCategory()
        {
            var SpecialType = Methods.GetSpecialType(InternalID);
            string CategoryText = "Null";
            switch (SpecialType)
            {
                case Enums.SpecialType.T00_GeneralPurpose: CategoryText = Lang.GetAttributeText(aLang.SpecialType00_GeneralPurpose); break;
                case Enums.SpecialType.T01_WarpDoor: CategoryText = Lang.GetAttributeText(aLang.SpecialType01_WarpDoor); break;
                case Enums.SpecialType.T02_CutSceneEvents: CategoryText = Lang.GetAttributeText(aLang.SpecialType02_CutSceneEvents); break;
                case Enums.SpecialType.T03_Items: CategoryText = Lang.GetAttributeText(aLang.SpecialType03_Items); break;
                case Enums.SpecialType.T04_GroupedEnemyTrigger: CategoryText = Lang.GetAttributeText(aLang.SpecialType04_GroupedEnemyTrigger); break;
                case Enums.SpecialType.T05_Message: CategoryText = Lang.GetAttributeText(aLang.SpecialType05_Message); break;
                case Enums.SpecialType.T08_TypeWriter: CategoryText = Lang.GetAttributeText(aLang.SpecialType08_TypeWriter); break;
                case Enums.SpecialType.T0A_DamagesThePlayer: CategoryText = Lang.GetAttributeText(aLang.SpecialType0A_DamagesThePlayer); break;
                case Enums.SpecialType.T0B_FalseCollision: CategoryText = Lang.GetAttributeText(aLang.SpecialType0B_FalseCollision); break;
                case Enums.SpecialType.T0D_Unknown: CategoryText = Lang.GetAttributeText(aLang.SpecialType0D_Unknown); break;
                case Enums.SpecialType.T0E_Crouch: CategoryText = Lang.GetAttributeText(aLang.SpecialType0E_Crouch); break;
                case Enums.SpecialType.T10_FixedLadderClimbUp: CategoryText = Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp); break;
                case Enums.SpecialType.T11_ItemDependentEvents: CategoryText = Lang.GetAttributeText(aLang.SpecialType11_ItemDependentEvents); break;
                case Enums.SpecialType.T12_AshleyHideCommand: CategoryText = Lang.GetAttributeText(aLang.SpecialType12_AshleyHideCommand); break;
                case Enums.SpecialType.T13_LocalTeleportation: CategoryText = Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation); break;
                case Enums.SpecialType.T14_UsedForElevators: CategoryText = Lang.GetAttributeText(aLang.SpecialType14_UsedForElevators); break;
                case Enums.SpecialType.T15_AdaGrappleGun: CategoryText = Lang.GetAttributeText(aLang.SpecialType15_AdaGrappleGun); break;
                default: CategoryText = Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType); break;
            }

            categorySpecialTypes = CategoryText;
            CategoryText = CategoryText.PadRight(1000);

            ChangePropertyCategory("Unknown_HH", CategoryText);
            ChangePropertyCategory("Unknown_HK", CategoryText);
            ChangePropertyCategory("Unknown_HL", CategoryText);
            ChangePropertyCategory("Unknown_HM", CategoryText);
            ChangePropertyCategory("Unknown_HN", CategoryText);
            ChangePropertyCategory("Unknown_HR", CategoryText);
            ChangePropertyCategory("Unknown_RH", CategoryText);
            ChangePropertyCategory("Unknown_RJ", CategoryText);
            ChangePropertyCategory("Unknown_RK", CategoryText);
            ChangePropertyCategory("Unknown_RL", CategoryText);
            ChangePropertyCategory("Unknown_RM", CategoryText);
            ChangePropertyCategory("Unknown_RN", CategoryText);
            ChangePropertyCategory("Unknown_RP", CategoryText);
            ChangePropertyCategory("Unknown_RQ", CategoryText);
            ChangePropertyCategory("Unknown_TG", CategoryText);
            ChangePropertyCategory("Unknown_TH", CategoryText);
            ChangePropertyCategory("Unknown_TJ", CategoryText);
            ChangePropertyCategory("Unknown_TK", CategoryText);
            ChangePropertyCategory("Unknown_TL", CategoryText);
            ChangePropertyCategory("Unknown_TM", CategoryText);
            ChangePropertyCategory("Unknown_TN", CategoryText);
            ChangePropertyCategory("Unknown_TP", CategoryText);
            ChangePropertyCategory("Unknown_TQ", CategoryText);

            ChangePropertyCategory("Unknown_VS", CategoryText);
            ChangePropertyCategory("Unknown_VT", CategoryText);
            ChangePropertyCategory("Unknown_VI", CategoryText);
            ChangePropertyCategory("Unknown_VO", CategoryText);

            ChangePropertyCategory("ObjPointX", CategoryText);
            ChangePropertyCategory("ObjPointY", CategoryText);
            ChangePropertyCategory("ObjPointZ", CategoryText);
            ChangePropertyCategory("ObjPointX_Hex", CategoryText);
            ChangePropertyCategory("ObjPointY_Hex", CategoryText);
            ChangePropertyCategory("ObjPointZ_Hex", CategoryText);

            ChangePropertyCategory("ObjPointW_onlyClassic", CategoryText);

            ChangePropertyCategory("Category_SpecialTypes", CategoryText);
        }

        void SetPropertyId() 
        {
            bool IsAshley = Methods.GetSpecialType(InternalID) == SpecialType.T12_AshleyHideCommand;
            if (IsAshley && version == Re4Version.Classic)
            {
                ChangePropertyId("AshleyHidingPointX", 0x6711);
                ChangePropertyId("AshleyHidingPointY", 0x6712);
                ChangePropertyId("AshleyHidingPointZ", 0x6713);
                ChangePropertyId("AshleyHidingPointX_Hex", 0x6711);
                ChangePropertyId("AshleyHidingPointY_Hex", 0x6712);
                ChangePropertyId("AshleyHidingPointZ_Hex", 0x6713);
            }
            else
            {
                ChangePropertyId("AshleyHidingPointX", 0x8000);
                ChangePropertyId("AshleyHidingPointY", 0x8400);
                ChangePropertyId("AshleyHidingPointZ", 0x8800);
                ChangePropertyId("AshleyHidingPointX_Hex", 0x8000);
                ChangePropertyId("AshleyHidingPointY_Hex", 0x8400);
                ChangePropertyId("AshleyHidingPointZ_Hex", 0x8800);
            }
        }

        void SetIsExtra() 
        {
            ChangePropertyIsBrowsable("InternalLineID", !IsExtra);
            ChangePropertyIsBrowsable("Line", !IsExtra);
            ChangePropertyIsBrowsable("SpecialTypeID", !IsExtra);
            ChangePropertyIsBrowsable("SpecialTypeID_ListBox", !IsExtra);
            ChangePropertyIsBrowsable("SpecialIndex", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_GG", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_GH", !IsExtra);
            ChangePropertyIsBrowsable("Category", !IsExtra);
            ChangePropertyIsBrowsable("Category_ListBox", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_GK", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_KG", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_KJ", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_LI", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_LO", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_LU", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_LH", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_MI", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_MO", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_MU", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_NI", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_NO", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_NS", !IsExtra);
            ChangePropertyIsBrowsable("RefInteractionType", !IsExtra);
            ChangePropertyIsBrowsable("RefInteractionType_ListBox", !IsExtra);
            ChangePropertyIsBrowsable("RefInteractionIndex", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_NT", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_NU", !IsExtra);
            ChangePropertyIsBrowsable("PromptMessage", !IsExtra);
            ChangePropertyIsBrowsable("PromptMessage_ListBox", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_PI", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_PO", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_PU", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_PK", !IsExtra);
            ChangePropertyIsBrowsable("MessageColor", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_QI", !IsExtra);
            ChangePropertyIsBrowsable("Unknown_QO", !IsExtra);
         

            ChangePropertyIsBrowsable("Unknown_QU", version == Re4Version.Classic && !IsExtra);
        }

        void SetForMultiSelection(bool ForMultiSelection) 
        {
            ChangePropertyIsBrowsable("Category_InternalLineIDCategory", ForMultiSelection);
            ChangePropertyIsBrowsable("Category_LineArrayCategory", ForMultiSelection);
            ChangePropertyIsBrowsable("Category_SpecialTypeCategory", ForMultiSelection);
            ChangePropertyIsBrowsable("Category_SpecialTriggerZoneCategory", ForMultiSelection);
            ChangePropertyIsBrowsable("Category_SpecialGeneralCategory", ForMultiSelection);
            ChangePropertyIsBrowsable("Category_SpecialTypes", ForMultiSelection);
            ChangePropertyIsBrowsable("Category_FloatTypeCategory", ForMultiSelection);
        }

        void SetClassicPropertyTexts() 
        {
            //Line
            if (specialFileFormat == SpecialFileFormat.AEV)
            {
                ChangePropertyName("Line", Lang.GetAttributeText(aLang.Special_LineArrayDisplayName).Replace("<<Lenght>>", "160"));
            }
            else if (specialFileFormat == SpecialFileFormat.ITA)
            {
                ChangePropertyName("Line", Lang.GetAttributeText(aLang.Special_LineArrayDisplayName).Replace("<<Lenght>>", "176"));
            }


            ChangePropertyDescription("SpecialTypeID", Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription("SpecialTypeID_ListBox", Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription("SpecialIndex", Lang.GetAttributeText(aLang.SpecialIndex_Byte_Description));
            ChangePropertyDescription("Category", Lang.GetAttributeText(aLang.Special_Category_Byte_Description));
            ChangePropertyDescription("Category_ListBox", Lang.GetAttributeText(aLang.Special_Category_Byte_Description));


            ChangePropertyDescription("TriggerZoneTrueY", Lang.GetAttributeText(aLang.TriggerZoneTrueY_Description));
            ChangePropertyDescription("TriggerZoneTrueY_Hex", Lang.GetAttributeText(aLang.TriggerZoneTrueY_Description));
            ChangePropertyDescription("TriggerZoneMoreHeight", Lang.GetAttributeText(aLang.TriggerZoneMoreHeight_Description));
            ChangePropertyDescription("TriggerZoneMoreHeight_Hex", Lang.GetAttributeText(aLang.TriggerZoneMoreHeight_Description));
            ChangePropertyDescription("TriggerZoneCircleRadius", Lang.GetAttributeText(aLang.TriggerZoneCircleRadius_Description));
            ChangePropertyDescription("TriggerZoneCircleRadius_Hex", Lang.GetAttributeText(aLang.TriggerZoneCircleRadius_Description));
            ChangePropertyDescription("TriggerZoneCorner0_X", Lang.GetAttributeText(aLang.TriggerZoneCorner0_X_Description));
            ChangePropertyDescription("TriggerZoneCorner0_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner0_X_Description));
            ChangePropertyDescription("TriggerZoneCorner0_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner0_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner0_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner0_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner1_X", Lang.GetAttributeText(aLang.TriggerZoneCorner1_X_Description));
            ChangePropertyDescription("TriggerZoneCorner1_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner1_X_Description));
            ChangePropertyDescription("TriggerZoneCorner1_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner1_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner1_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner1_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner2_X", Lang.GetAttributeText(aLang.TriggerZoneCorner2_X_Description));
            ChangePropertyDescription("TriggerZoneCorner2_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner2_X_Description));
            ChangePropertyDescription("TriggerZoneCorner2_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner2_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner2_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner2_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner3_X", Lang.GetAttributeText(aLang.TriggerZoneCorner3_X_Description));
            ChangePropertyDescription("TriggerZoneCorner3_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner3_X_Description));
            ChangePropertyDescription("TriggerZoneCorner3_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner3_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner3_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner3_Z_Description));


            ChangePropertyDescription("Unknown_GG", Lang.GetAttributeText(aLang.Unknown_GG_ByteArray4_Description));
            ChangePropertyDescription("Unknown_GH", Lang.GetAttributeText(aLang.Unknown_GH_Byte_Description));
            ChangePropertyDescription("Unknown_GK", Lang.GetAttributeText(aLang.Unknown_GK_ByteArray2_Description));

            ChangePropertyDescription("Unknown_KG", Lang.GetAttributeText(aLang.Unknown_KG_Byte_Description));
            ChangePropertyDescription("Unknown_KJ", Lang.GetAttributeText(aLang.Unknown_KJ_Byte_Description));
            ChangePropertyDescription("Unknown_LI", Lang.GetAttributeText(aLang.Unknown_LI_Byte_Description));
            ChangePropertyDescription("Unknown_LO", Lang.GetAttributeText(aLang.Unknown_LO_Byte_Description));
            ChangePropertyDescription("Unknown_LU", Lang.GetAttributeText(aLang.Unknown_LU_Byte_Description));
            ChangePropertyDescription("Unknown_LH", Lang.GetAttributeText(aLang.Unknown_LH_Byte_Description));
            ChangePropertyDescription("Unknown_MI", Lang.GetAttributeText(aLang.Unknown_MI_ByteArray2_Description));
            ChangePropertyDescription("Unknown_MO", Lang.GetAttributeText(aLang.Unknown_MO_ByteArray2_Description));
            ChangePropertyDescription("Unknown_MU", Lang.GetAttributeText(aLang.Unknown_MU_ByteArray2_Description));
            ChangePropertyDescription("Unknown_NI", Lang.GetAttributeText(aLang.Unknown_NI_ByteArray2_Description));
            ChangePropertyDescription("Unknown_NO", Lang.GetAttributeText(aLang.Unknown_NO_Byte_Description));
            ChangePropertyDescription("Unknown_NS", Lang.GetAttributeText(aLang.Unknown_NS_Byte_Description));
            ChangePropertyDescription("RefInteractionType", Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription("RefInteractionType_ListBox", Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription("RefInteractionIndex", Lang.GetAttributeText(aLang.RefInteractionIndex_Byte_Description));
            ChangePropertyDescription("Unknown_NT", Lang.GetAttributeText(aLang.Unknown_NT_Byte_Description));
            ChangePropertyDescription("Unknown_NU", Lang.GetAttributeText(aLang.Unknown_NU_Byte_Description));
            ChangePropertyDescription("PromptMessage", Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription("PromptMessage_ListBox", Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription("Unknown_PI", Lang.GetAttributeText(aLang.Unknown_PI_Byte_Description));
            ChangePropertyDescription("Unknown_PO", Lang.GetAttributeText(aLang.Unknown_PO_ByteArray4_Description));
            ChangePropertyDescription("Unknown_PU", Lang.GetAttributeText(aLang.Unknown_PU_ByteArray2_Description));
            ChangePropertyDescription("Unknown_PK", Lang.GetAttributeText(aLang.Unknown_PK_Byte_Description));
            ChangePropertyDescription("MessageColor", Lang.GetAttributeText(aLang.MessageColor_Byte_Description));
            ChangePropertyDescription("Unknown_QI", Lang.GetAttributeText(aLang.Unknown_QI_ByteArray4_Description));
            ChangePropertyDescription("Unknown_QO", Lang.GetAttributeText(aLang.Unknown_QO_ByteArray4_Description));
            ChangePropertyDescription("Unknown_QU", Lang.GetAttributeText(aLang.Unknown_QU_ByteArray4_Description).Replace("<<Offset1>>", "0x5C").Replace("<<Offset2>>", "0x5D").Replace("<<Offset3>>", "0x5E").Replace("<<Offset4>>", "0x5F"));


            ChangePropertyDescription("Unknown_HH", Lang.GetAttributeText(aLang.Unknown_HH_ByteArray2_Description).Replace("<<Offset1>>", "0x60").Replace("<<Offset2>>", "0x61"));
            ChangePropertyDescription("Unknown_HK", Lang.GetAttributeText(aLang.Unknown_HK_ByteArray2_Description).Replace("<<Offset1>>", "0x62").Replace("<<Offset2>>", "0x63"));
            ChangePropertyDescription("Unknown_HL", Lang.GetAttributeText(aLang.Unknown_HL_ByteArray2_Description).Replace("<<Offset1>>", "0x64").Replace("<<Offset2>>", "0x65"));
            ChangePropertyDescription("Unknown_HM", Lang.GetAttributeText(aLang.Unknown_HM_ByteArray2_Description).Replace("<<Offset1>>", "0x66").Replace("<<Offset2>>", "0x67"));
            ChangePropertyDescription("Unknown_HN", Lang.GetAttributeText(aLang.Unknown_HN_ByteArray2_Description).Replace("<<Offset1>>", "0x68").Replace("<<Offset2>>", "0x69"));
            ChangePropertyDescription("Unknown_HR", Lang.GetAttributeText(aLang.Unknown_HR_ByteArray2_Description).Replace("<<Offset1>>", "0x6A").Replace("<<Offset2>>", "0x6B"));
            ChangePropertyDescription("Unknown_RH", Lang.GetAttributeText(aLang.Unknown_RH_ByteArray2_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D"));
            ChangePropertyDescription("Unknown_RJ", Lang.GetAttributeText(aLang.Unknown_RJ_ByteArray2_Description).Replace("<<Offset1>>", "0x6E").Replace("<<Offset2>>", "0x6F"));
            ChangePropertyDescription("Unknown_RK", Lang.GetAttributeText(aLang.Unknown_RK_ByteArray2_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71"));
            ChangePropertyDescription("Unknown_RL", Lang.GetAttributeText(aLang.Unknown_RL_ByteArray2_Description).Replace("<<Offset1>>", "0x72").Replace("<<Offset2>>", "0x73"));
            ChangePropertyDescription("Unknown_RM", Lang.GetAttributeText(aLang.Unknown_RM_ByteArray2_Description).Replace("<<Offset1>>", "0x74").Replace("<<Offset2>>", "0x75"));
            ChangePropertyDescription("Unknown_RN", Lang.GetAttributeText(aLang.Unknown_RN_ByteArray2_Description).Replace("<<Offset1>>", "0x76").Replace("<<Offset2>>", "0x77"));
            ChangePropertyDescription("Unknown_RP", Lang.GetAttributeText(aLang.Unknown_RP_ByteArray2_Description).Replace("<<Offset1>>", "0x78").Replace("<<Offset2>>", "0x79"));
            ChangePropertyDescription("Unknown_RQ", Lang.GetAttributeText(aLang.Unknown_RQ_ByteArray2_Description).Replace("<<Offset1>>", "0x7A").Replace("<<Offset2>>", "0x7B"));
            ChangePropertyDescription("Unknown_TG", Lang.GetAttributeText(aLang.Unknown_TG_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription("Unknown_TH", Lang.GetAttributeText(aLang.Unknown_TH_ByteArray4_Description).Replace("<<Offset1>>", "0x80").Replace("<<Offset2>>", "0x81").Replace("<<Offset3>>", "0x82").Replace("<<Offset4>>", "0x83"));
            ChangePropertyDescription("Unknown_TJ", Lang.GetAttributeText(aLang.Unknown_TJ_ByteArray4_Description).Replace("<<Offset1>>", "0x84").Replace("<<Offset2>>", "0x85").Replace("<<Offset3>>", "0x86").Replace("<<Offset4>>", "0x87"));
            ChangePropertyDescription("Unknown_TK", Lang.GetAttributeText(aLang.Unknown_TK_ByteArray4_Description).Replace("<<Offset1>>", "0x88").Replace("<<Offset2>>", "0x89").Replace("<<Offset3>>", "0x8A").Replace("<<Offset4>>", "0x8B"));
            ChangePropertyDescription("Unknown_TL", Lang.GetAttributeText(aLang.Unknown_TL_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription("Unknown_TM", Lang.GetAttributeText(aLang.Unknown_TM_ByteArray4_Description).Replace("<<Offset1>>", "0x90").Replace("<<Offset2>>", "0x91").Replace("<<Offset3>>", "0x92").Replace("<<Offset4>>", "0x93"));
            ChangePropertyDescription("Unknown_TN", Lang.GetAttributeText(aLang.Unknown_TN_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));
            ChangePropertyDescription("Unknown_TP", Lang.GetAttributeText(aLang.Unknown_TP_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));
            ChangePropertyDescription("Unknown_TQ", Lang.GetAttributeText(aLang.Unknown_TQ_ByteArray4_Description).Replace("<<Offset1>>", "0x9C").Replace("<<Offset2>>", "0x9D").Replace("<<Offset3>>", "0x9E").Replace("<<Offset4>>", "0x9F"));
            ChangePropertyDescription("Unknown_VS", Lang.GetAttributeText(aLang.Unknown_VS_ByteArray4_Description).Replace("<<Offset1>>", "0xA0").Replace("<<Offset2>>", "0xA1").Replace("<<Offset3>>", "0xA2").Replace("<<Offset4>>", "0xA3"));
            ChangePropertyDescription("Unknown_VT", Lang.GetAttributeText(aLang.Unknown_VT_ByteArray4_Description).Replace("<<Offset1>>", "0xA4").Replace("<<Offset2>>", "0xA5").Replace("<<Offset3>>", "0xA6").Replace("<<Offset4>>", "0xA7"));
            ChangePropertyDescription("Unknown_VI", Lang.GetAttributeText(aLang.Unknown_VI_ByteArray4_Description).Replace("<<Offset1>>", "0xA8").Replace("<<Offset2>>", "0xA9").Replace("<<Offset3>>", "0xAA").Replace("<<Offset4>>", "0xAB"));
            ChangePropertyDescription("Unknown_VO", Lang.GetAttributeText(aLang.Unknown_VO_ByteArray4_Description).Replace("<<Offset1>>", "0xAC").Replace("<<Offset2>>", "0xAD").Replace("<<Offset3>>", "0xAE").Replace("<<Offset4>>", "0xAF"));

            ChangePropertyDescription("ObjPointX", Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("ObjPointX_Hex", Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("ObjPointY", Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("ObjPointY_Hex", Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("ObjPointZ", Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("ObjPointZ_Hex", Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("ObjPointW", Lang.GetAttributeText(aLang.ObjPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D").Replace("<<Offset3>>", "0x6E").Replace("<<Offset4>>", "0x6F"));
            ChangePropertyDescription("ObjPointW_onlyClassic", Lang.GetAttributeText(aLang.ObjPointW_onlyClassic_ByteArray4_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D").Replace("<<Offset3>>", "0x6E").Replace("<<Offset4>>", "0x6F"));


            ChangePropertyDescription("NeededItemNumber", Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x61").Replace("<<Offset2>>", "0x60"));
            ChangePropertyDescription("NeededItemNumber_ListBox", Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x61").Replace("<<Offset2>>", "0x60"));
            ChangePropertyDescription("EnemyGroup", Lang.GetAttributeText(aLang.EnemyGroup_Ushort_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62"));
            ChangePropertyDescription("RoomMessage", Lang.GetAttributeText(aLang.RoomMessage_Ushort_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62"));
            ChangePropertyDescription("MessageCutSceneID", Lang.GetAttributeText(aLang.MessageCutSceneID_Ushort_Description).Replace("<<Offset1>>", "0x65").Replace("<<Offset2>>", "0x64"));
            ChangePropertyDescription("MessageID", Lang.GetAttributeText(aLang.MessageID_Ushort_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66"));
            ChangePropertyDescription("ActivationType", Lang.GetAttributeText(aLang.ActivationType_Byte_Description).Replace("<<Offset1>>", "0x64"));
            ChangePropertyDescription("DamageType", Lang.GetAttributeText(aLang.DamageType_Byte_Description).Replace("<<Offset1>>", "0x65"));
            ChangePropertyDescription("BlockingType", Lang.GetAttributeText(aLang.BlockingType_Byte_Description).Replace("<<Offset1>>", "0x66"));
            ChangePropertyDescription("Unknown_SJ", Lang.GetAttributeText(aLang.Unknown_SJ_Byte_Description).Replace("<<Offset1>>", "0x67"));
            ChangePropertyDescription("DamageAmount", Lang.GetAttributeText(aLang.DamageAmount_Ushort_Description).Replace("<<Offset1>>", "0x69").Replace("<<Offset2>>", "0x68"));
            ChangePropertyDescription("DestinationFacingAngle", Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("DestinationFacingAngle_Hex", Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("DestinationRoom", Lang.GetAttributeText(aLang.DestinationRoom_UshortUnflip_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71"));
            ChangePropertyDescription("LockedDoorType", Lang.GetAttributeText(aLang.LockedDoorType_Byte_Description).Replace("<<Offset1>>", "0x72"));
            ChangePropertyDescription("LockedDoorIndex", Lang.GetAttributeText(aLang.LockedDoorIndex_Byte_Description).Replace("<<Offset1>>", "0x73"));
            ChangePropertyDescription("LocalTeleportationFacingAngle", Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("LocalTeleportationFacingAngle_Hex", Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("LadderFacingAngle", Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("LadderFacingAngle_Hex", Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("LadderStepCount", Lang.GetAttributeText(aLang.LadderStepCount_Sbyte_Description).Replace("<<Offset1>>", "0x74"));
            ChangePropertyDescription("LadderParameter0", Lang.GetAttributeText(aLang.LadderParameter0_Byte_Description).Replace("<<Offset1>>", "0x75"));
            ChangePropertyDescription("LadderParameter1", Lang.GetAttributeText(aLang.LadderParameter1_Byte_Description).Replace("<<Offset1>>", "0x76"));
            ChangePropertyDescription("LadderParameter2", Lang.GetAttributeText(aLang.LadderParameter2_Byte_Description).Replace("<<Offset1>>", "0x77"));
            ChangePropertyDescription("LadderParameter3", Lang.GetAttributeText(aLang.LadderParameter3_Byte_Description).Replace("<<Offset1>>", "0x78"));
            ChangePropertyDescription("Unknown_SG", Lang.GetAttributeText(aLang.Unknown_SG_Byte_Description).Replace("<<Offset1>>", "0x79"));
            ChangePropertyDescription("Unknown_SH", Lang.GetAttributeText(aLang.Unknown_SH_ByteArray2_Description).Replace("<<Offset1>>", "0x7A").Replace("<<Offset2>>", "0x7B"));


            ChangePropertyDescription("Unknown_RI_X", Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("Unknown_RI_X_Hex", Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("Unknown_RI_Y", Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("Unknown_RI_Y_Hex", Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("Unknown_RI_Z", Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("Unknown_RI_Z_Hex", Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("Unknown_RI_W", Lang.GetAttributeText(aLang.Unknown_RI_W_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription("Unknown_RO", Lang.GetAttributeText(aLang.Unknown_RO_ByteArray4_Description).Replace("<<Offset1>>", "0x80").Replace("<<Offset2>>", "0x81").Replace("<<Offset3>>", "0x82").Replace("<<Offset4>>", "0x83"));
            ChangePropertyDescription("ItemNumber", Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x85").Replace("<<Offset2>>", "0x84"));
            ChangePropertyDescription("ItemNumber_ListBox", Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x85").Replace("<<Offset2>>", "0x84"));
            ChangePropertyDescription("Unknown_RU", Lang.GetAttributeText(aLang.Unknown_RU_ByteArray2_Description).Replace("<<Offset1>>", "0x86").Replace("<<Offset2>>", "0x87"));
            ChangePropertyDescription("ItemAmount", Lang.GetAttributeText(aLang.ItemAmount_Ushort_Description).Replace("<<Offset1>>", "0x89").Replace("<<Offset2>>", "0x88"));
            ChangePropertyDescription("SecundIndex", Lang.GetAttributeText(aLang.SecundIndex_Ushort_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A"));
            ChangePropertyDescription("ItemAuraType", Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x8D").Replace("<<Offset2>>", "0x8C"));
            ChangePropertyDescription("ItemAuraType_ListBox", Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x8D").Replace("<<Offset2>>", "0x8C"));
            ChangePropertyDescription("Unknown_QM", Lang.GetAttributeText(aLang.Unknown_QM_Byte_Description).Replace("<<Offset1>>", "0x8E"));
            ChangePropertyDescription("Unknown_QL", Lang.GetAttributeText(aLang.Unknown_QL_Byte_Description).Replace("<<Offset1>>", "0x8F"));
            ChangePropertyDescription("Unknown_QR", Lang.GetAttributeText(aLang.Unknown_QR_Byte_Description).Replace("<<Offset1>>", "0x90"));
            ChangePropertyDescription("Unknown_QH", Lang.GetAttributeText(aLang.Unknown_QH_Byte_Description).Replace("<<Offset1>>", "0x91"));
            ChangePropertyDescription("Unknown_QG", Lang.GetAttributeText(aLang.Unknown_QG_ByteArray2_Description).Replace("<<Offset1>>", "0x92").Replace("<<Offset2>>", "0x93"));
            ChangePropertyDescription("ItemTriggerRadius", Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription("ItemTriggerRadius_Hex", Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription("ItemAngleX", Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x9B").Replace("<<Offset2>>", "0x9A").Replace("<<Offset3>>", "0x99").Replace("<<Offset4>>", "0x98"));
            ChangePropertyDescription("ItemAngleX_Hex", Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x9B").Replace("<<Offset2>>", "0x9A").Replace("<<Offset3>>", "0x99").Replace("<<Offset4>>", "0x98"));
            ChangePropertyDescription("ItemAngleY", Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x9F").Replace("<<Offset2>>", "0x9E").Replace("<<Offset3>>", "0x9D").Replace("<<Offset4>>", "0x9C"));
            ChangePropertyDescription("ItemAngleY_Hex", Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x9F").Replace("<<Offset2>>", "0x9E").Replace("<<Offset3>>", "0x9D").Replace("<<Offset4>>", "0x9C"));
            ChangePropertyDescription("ItemAngleZ", Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0xA3").Replace("<<Offset2>>", "0xA2").Replace("<<Offset3>>", "0xA1").Replace("<<Offset4>>", "0xA0"));
            ChangePropertyDescription("ItemAngleZ_Hex", Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0xA3").Replace("<<Offset2>>", "0xA2").Replace("<<Offset3>>", "0xA1").Replace("<<Offset4>>", "0xA0"));
            ChangePropertyDescription("ItemAngleW", Lang.GetAttributeText(aLang.ItemAngleW_ByteArray4_Description).Replace("<<Offset1>>", "0xA4").Replace("<<Offset2>>", "0xA5").Replace("<<Offset3>>", "0xA6").Replace("<<Offset4>>", "0xA7"));


            ChangePropertyDescription("AshleyHidingPointX", Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("AshleyHidingPointX_Hex", Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("AshleyHidingPointY", Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("AshleyHidingPointY_Hex", Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("AshleyHidingPointZ", Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("AshleyHidingPointZ_Hex", Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription("Unknown_SM", Lang.GetAttributeText(aLang.Unknown_SM_ByteArray4_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71").Replace("<<Offset3>>", "0x72").Replace("<<Offset4>>", "0x73"));
            ChangePropertyDescription("Unknown_SN", Lang.GetAttributeText(aLang.Unknown_SN_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));
            ChangePropertyDescription("Unknown_SP", Lang.GetAttributeText(aLang.Unknown_SP_Byte_Description).Replace("<<Offset1>>", "0x98"));
            ChangePropertyDescription("Unknown_SQ", Lang.GetAttributeText(aLang.Unknown_SQ_Byte_Description).Replace("<<Offset1>>", "0x99"));
            ChangePropertyDescription("Unknown_SR", Lang.GetAttributeText(aLang.Unknown_SR_ByteArray2_Description).Replace("<<Offset1>>", "0x9A").Replace("<<Offset2>>", "0x9B"));
            ChangePropertyDescription("Unknown_SS", Lang.GetAttributeText(aLang.Unknown_SS_ByteArray4_Description).Replace("<<Offset1>>", "0x9C").Replace("<<Offset2>>", "0x9D").Replace("<<Offset3>>", "0x9E").Replace("<<Offset4>>", "0x9F"));


            ChangePropertyDescription("GrappleGunEndPointX", Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("GrappleGunEndPointX_Hex", Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("GrappleGunEndPointY", Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("GrappleGunEndPointY_Hex", Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("GrappleGunEndPointZ", Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("GrappleGunEndPointZ_Hex", Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("GrappleGunEndPointW", Lang.GetAttributeText(aLang.GrappleGunEndPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription("GrappleGunThirdPointX", Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("GrappleGunThirdPointX_Hex", Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("GrappleGunThirdPointY", Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription("GrappleGunThirdPointY_Hex", Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription("GrappleGunThirdPointZ", Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("GrappleGunThirdPointZ_Hex", Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("GrappleGunThirdPointW", Lang.GetAttributeText(aLang.GrappleGunThirdPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription("GrappleGunFacingAngle", Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription("GrappleGunFacingAngle_Hex", Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription("GrappleGunParameter0", Lang.GetAttributeText(aLang.GrappleGunParameter0_Byte_Description).Replace("<<Offset1>>", "0x94"));
            ChangePropertyDescription("GrappleGunParameter1", Lang.GetAttributeText(aLang.GrappleGunParameter1_Byte_Description).Replace("<<Offset1>>", "0x95"));
            ChangePropertyDescription("GrappleGunParameter2", Lang.GetAttributeText(aLang.GrappleGunParameter2_Byte_Description).Replace("<<Offset1>>", "0x96"));
            ChangePropertyDescription("GrappleGunParameter3", Lang.GetAttributeText(aLang.GrappleGunParameter3_Byte_Description).Replace("<<Offset1>>", "0x97"));
            ChangePropertyDescription("Unknown_SK", Lang.GetAttributeText(aLang.Unknown_SK_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));
            ChangePropertyDescription("Unknown_SL", Lang.GetAttributeText(aLang.Unknown_SL_ByteArray4_Description).Replace("<<Offset1>>", "0x9C").Replace("<<Offset2>>", "0x9D").Replace("<<Offset3>>", "0x9E").Replace("<<Offset4>>", "0x9F"));


            //ChangePropertyDescription("", Lang.GetAttributeText(aLang.));

            /*
        



            */
        }

        void SetUHDPropertyTexts()
        {
            ChangePropertyName("Line", Lang.GetAttributeText(aLang.Special_LineArrayDisplayName).Replace("<<Lenght>>", "156"));


            ChangePropertyDescription("SpecialTypeID", Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription("SpecialTypeID_ListBox", Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription("SpecialIndex", Lang.GetAttributeText(aLang.SpecialIndex_Byte_Description));
            ChangePropertyDescription("Category", Lang.GetAttributeText(aLang.Special_Category_Byte_Description));
            ChangePropertyDescription("Category_ListBox", Lang.GetAttributeText(aLang.Special_Category_Byte_Description));


            ChangePropertyDescription("TriggerZoneTrueY", Lang.GetAttributeText(aLang.TriggerZoneTrueY_Description));
            ChangePropertyDescription("TriggerZoneTrueY_Hex", Lang.GetAttributeText(aLang.TriggerZoneTrueY_Description));
            ChangePropertyDescription("TriggerZoneMoreHeight", Lang.GetAttributeText(aLang.TriggerZoneMoreHeight_Description));
            ChangePropertyDescription("TriggerZoneMoreHeight_Hex", Lang.GetAttributeText(aLang.TriggerZoneMoreHeight_Description));
            ChangePropertyDescription("TriggerZoneCircleRadius", Lang.GetAttributeText(aLang.TriggerZoneCircleRadius_Description));
            ChangePropertyDescription("TriggerZoneCircleRadius_Hex", Lang.GetAttributeText(aLang.TriggerZoneCircleRadius_Description));
            ChangePropertyDescription("TriggerZoneCorner0_X", Lang.GetAttributeText(aLang.TriggerZoneCorner0_X_Description));
            ChangePropertyDescription("TriggerZoneCorner0_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner0_X_Description));
            ChangePropertyDescription("TriggerZoneCorner0_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner0_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner0_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner0_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner1_X", Lang.GetAttributeText(aLang.TriggerZoneCorner1_X_Description));
            ChangePropertyDescription("TriggerZoneCorner1_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner1_X_Description));
            ChangePropertyDescription("TriggerZoneCorner1_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner1_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner1_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner1_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner2_X", Lang.GetAttributeText(aLang.TriggerZoneCorner2_X_Description));
            ChangePropertyDescription("TriggerZoneCorner2_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner2_X_Description));
            ChangePropertyDescription("TriggerZoneCorner2_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner2_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner2_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner2_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner3_X", Lang.GetAttributeText(aLang.TriggerZoneCorner3_X_Description));
            ChangePropertyDescription("TriggerZoneCorner3_X_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner3_X_Description));
            ChangePropertyDescription("TriggerZoneCorner3_Z", Lang.GetAttributeText(aLang.TriggerZoneCorner3_Z_Description));
            ChangePropertyDescription("TriggerZoneCorner3_Z_Hex", Lang.GetAttributeText(aLang.TriggerZoneCorner3_Z_Description));


            ChangePropertyDescription("Unknown_GG", Lang.GetAttributeText(aLang.Unknown_GG_ByteArray4_Description));
            ChangePropertyDescription("Unknown_GH", Lang.GetAttributeText(aLang.Unknown_GH_Byte_Description));
            ChangePropertyDescription("Unknown_GK", Lang.GetAttributeText(aLang.Unknown_GK_ByteArray2_Description));

            ChangePropertyDescription("Unknown_KG", Lang.GetAttributeText(aLang.Unknown_KG_Byte_Description));
            ChangePropertyDescription("Unknown_KJ", Lang.GetAttributeText(aLang.Unknown_KJ_Byte_Description));
            ChangePropertyDescription("Unknown_LI", Lang.GetAttributeText(aLang.Unknown_LI_Byte_Description));
            ChangePropertyDescription("Unknown_LO", Lang.GetAttributeText(aLang.Unknown_LO_Byte_Description));
            ChangePropertyDescription("Unknown_LU", Lang.GetAttributeText(aLang.Unknown_LU_Byte_Description));
            ChangePropertyDescription("Unknown_LH", Lang.GetAttributeText(aLang.Unknown_LH_Byte_Description));
            ChangePropertyDescription("Unknown_MI", Lang.GetAttributeText(aLang.Unknown_MI_ByteArray2_Description));
            ChangePropertyDescription("Unknown_MO", Lang.GetAttributeText(aLang.Unknown_MO_ByteArray2_Description));
            ChangePropertyDescription("Unknown_MU", Lang.GetAttributeText(aLang.Unknown_MU_ByteArray2_Description));
            ChangePropertyDescription("Unknown_NI", Lang.GetAttributeText(aLang.Unknown_NI_ByteArray2_Description));
            ChangePropertyDescription("Unknown_NO", Lang.GetAttributeText(aLang.Unknown_NO_Byte_Description));
            ChangePropertyDescription("Unknown_NS", Lang.GetAttributeText(aLang.Unknown_NS_Byte_Description));
            ChangePropertyDescription("RefInteractionType", Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription("RefInteractionType_ListBox", Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription("RefInteractionIndex", Lang.GetAttributeText(aLang.RefInteractionIndex_Byte_Description));
            ChangePropertyDescription("Unknown_NT", Lang.GetAttributeText(aLang.Unknown_NT_Byte_Description));
            ChangePropertyDescription("Unknown_NU", Lang.GetAttributeText(aLang.Unknown_NU_Byte_Description));
            ChangePropertyDescription("PromptMessage", Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription("PromptMessage_ListBox", Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription("Unknown_PI", Lang.GetAttributeText(aLang.Unknown_PI_Byte_Description));
            ChangePropertyDescription("Unknown_PO", Lang.GetAttributeText(aLang.Unknown_PO_ByteArray4_Description));
            ChangePropertyDescription("Unknown_PU", Lang.GetAttributeText(aLang.Unknown_PU_ByteArray2_Description));
            ChangePropertyDescription("Unknown_PK", Lang.GetAttributeText(aLang.Unknown_PK_Byte_Description));
            ChangePropertyDescription("MessageColor", Lang.GetAttributeText(aLang.MessageColor_Byte_Description));
            ChangePropertyDescription("Unknown_QI", Lang.GetAttributeText(aLang.Unknown_QI_ByteArray4_Description));
            ChangePropertyDescription("Unknown_QO", Lang.GetAttributeText(aLang.Unknown_QO_ByteArray4_Description));
            ChangePropertyDescription("Unknown_QU", Lang.GetAttributeText(aLang.Unknown_QU_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));


            ChangePropertyDescription("Unknown_HH", Lang.GetAttributeText(aLang.Unknown_HH_ByteArray2_Description).Replace("<<Offset1>>", "0x5C").Replace("<<Offset2>>", "0x5D"));
            ChangePropertyDescription("Unknown_HK", Lang.GetAttributeText(aLang.Unknown_HK_ByteArray2_Description).Replace("<<Offset1>>", "0x5E").Replace("<<Offset2>>", "0xEF"));
            ChangePropertyDescription("Unknown_HL", Lang.GetAttributeText(aLang.Unknown_HL_ByteArray2_Description).Replace("<<Offset1>>", "0x60").Replace("<<Offset2>>", "0x61"));
            ChangePropertyDescription("Unknown_HM", Lang.GetAttributeText(aLang.Unknown_HM_ByteArray2_Description).Replace("<<Offset1>>", "0x62").Replace("<<Offset2>>", "0x63"));
            ChangePropertyDescription("Unknown_HN", Lang.GetAttributeText(aLang.Unknown_HN_ByteArray2_Description).Replace("<<Offset1>>", "0x64").Replace("<<Offset2>>", "0x65"));
            ChangePropertyDescription("Unknown_HR", Lang.GetAttributeText(aLang.Unknown_HR_ByteArray2_Description).Replace("<<Offset1>>", "0x66").Replace("<<Offset2>>", "0x67"));
            ChangePropertyDescription("Unknown_RH", Lang.GetAttributeText(aLang.Unknown_RH_ByteArray2_Description).Replace("<<Offset1>>", "0x68").Replace("<<Offset2>>", "0x69"));
            ChangePropertyDescription("Unknown_RJ", Lang.GetAttributeText(aLang.Unknown_RJ_ByteArray2_Description).Replace("<<Offset1>>", "0x6A").Replace("<<Offset2>>", "0x6B"));
            ChangePropertyDescription("Unknown_RK", Lang.GetAttributeText(aLang.Unknown_RK_ByteArray2_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D"));
            ChangePropertyDescription("Unknown_RL", Lang.GetAttributeText(aLang.Unknown_RL_ByteArray2_Description).Replace("<<Offset1>>", "0x6E").Replace("<<Offset2>>", "0x6F"));
            ChangePropertyDescription("Unknown_RM", Lang.GetAttributeText(aLang.Unknown_RM_ByteArray2_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71"));
            ChangePropertyDescription("Unknown_RN", Lang.GetAttributeText(aLang.Unknown_RN_ByteArray2_Description).Replace("<<Offset1>>", "0x72").Replace("<<Offset2>>", "0x73"));
            ChangePropertyDescription("Unknown_RP", Lang.GetAttributeText(aLang.Unknown_RP_ByteArray2_Description).Replace("<<Offset1>>", "0x74").Replace("<<Offset2>>", "0x75"));
            ChangePropertyDescription("Unknown_RQ", Lang.GetAttributeText(aLang.Unknown_RQ_ByteArray2_Description).Replace("<<Offset1>>", "0x76").Replace("<<Offset2>>", "0x77"));
            ChangePropertyDescription("Unknown_TG", Lang.GetAttributeText(aLang.Unknown_TG_ByteArray4_Description).Replace("<<Offset1>>", "0x78").Replace("<<Offset2>>", "0x79").Replace("<<Offset3>>", "0x7A").Replace("<<Offset4>>", "0x7B"));
            ChangePropertyDescription("Unknown_TH", Lang.GetAttributeText(aLang.Unknown_TH_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription("Unknown_TJ", Lang.GetAttributeText(aLang.Unknown_TJ_ByteArray4_Description).Replace("<<Offset1>>", "0x80").Replace("<<Offset2>>", "0x81").Replace("<<Offset3>>", "0x82").Replace("<<Offset4>>", "0x83"));
            ChangePropertyDescription("Unknown_TK", Lang.GetAttributeText(aLang.Unknown_TK_ByteArray4_Description).Replace("<<Offset1>>", "0x84").Replace("<<Offset2>>", "0x85").Replace("<<Offset3>>", "0x86").Replace("<<Offset4>>", "0x87"));
            ChangePropertyDescription("Unknown_TL", Lang.GetAttributeText(aLang.Unknown_TL_ByteArray4_Description).Replace("<<Offset1>>", "0x88").Replace("<<Offset2>>", "0x89").Replace("<<Offset3>>", "0x8A").Replace("<<Offset4>>", "0x8B"));
            ChangePropertyDescription("Unknown_TM", Lang.GetAttributeText(aLang.Unknown_TM_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription("Unknown_TN", Lang.GetAttributeText(aLang.Unknown_TN_ByteArray4_Description).Replace("<<Offset1>>", "0x90").Replace("<<Offset2>>", "0x91").Replace("<<Offset3>>", "0x92").Replace("<<Offset4>>", "0x93"));
            ChangePropertyDescription("Unknown_TP", Lang.GetAttributeText(aLang.Unknown_TP_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));
            ChangePropertyDescription("Unknown_TQ", Lang.GetAttributeText(aLang.Unknown_TQ_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));
            ChangePropertyDescription("Unknown_VS", Lang.GetAttributeText(aLang.Unknown_VS_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("Unknown_VT", Lang.GetAttributeText(aLang.Unknown_VT_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("Unknown_VI", Lang.GetAttributeText(aLang.Unknown_VI_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("Unknown_VO", Lang.GetAttributeText(aLang.Unknown_VO_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription("ObjPointX", Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E").Replace("<<Offset3>>", "0x5D").Replace("<<Offset4>>", "0x5C"));
            ChangePropertyDescription("ObjPointX_Hex", Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E").Replace("<<Offset3>>", "0x5D").Replace("<<Offset4>>", "0x5C"));
            ChangePropertyDescription("ObjPointY", Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("ObjPointY_Hex", Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("ObjPointZ", Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("ObjPointZ_Hex", Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("ObjPointW", Lang.GetAttributeText(aLang.ObjPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x68").Replace("<<Offset2>>", "0x69").Replace("<<Offset3>>", "0x6A").Replace("<<Offset4>>", "0x6B"));
            ChangePropertyDescription("ObjPointW_onlyClassic", Lang.GetAttributeText(aLang.ObjPointW_onlyClassic_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));


            ChangePropertyDescription("NeededItemNumber", Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x5D").Replace("<<Offset2>>", "0x5C"));
            ChangePropertyDescription("NeededItemNumber_ListBox", Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x5D").Replace("<<Offset2>>", "0x5C"));
            ChangePropertyDescription("EnemyGroup", Lang.GetAttributeText(aLang.EnemyGroup_Ushort_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E"));
            ChangePropertyDescription("RoomMessage", Lang.GetAttributeText(aLang.RoomMessage_Ushort_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E"));
            ChangePropertyDescription("MessageCutSceneID", Lang.GetAttributeText(aLang.MessageCutSceneID_Ushort_Description).Replace("<<Offset1>>", "0x61").Replace("<<Offset2>>", "0x60"));
            ChangePropertyDescription("MessageID", Lang.GetAttributeText(aLang.MessageID_Ushort_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62"));
            ChangePropertyDescription("ActivationType", Lang.GetAttributeText(aLang.ActivationType_Byte_Description).Replace("<<Offset1>>", "0x60"));
            ChangePropertyDescription("DamageType", Lang.GetAttributeText(aLang.DamageType_Byte_Description).Replace("<<Offset1>>", "0x61"));
            ChangePropertyDescription("BlockingType", Lang.GetAttributeText(aLang.BlockingType_Byte_Description).Replace("<<Offset1>>", "0x62"));
            ChangePropertyDescription("Unknown_SJ", Lang.GetAttributeText(aLang.Unknown_SJ_Byte_Description).Replace("<<Offset1>>", "0x63"));
            ChangePropertyDescription("DamageAmount", Lang.GetAttributeText(aLang.DamageAmount_Ushort_Description).Replace("<<Offset1>>", "0x65").Replace("<<Offset2>>", "0x64"));
            ChangePropertyDescription("DestinationFacingAngle", Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("DestinationFacingAngle_Hex", Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("DestinationRoom", Lang.GetAttributeText(aLang.DestinationRoom_UshortUnflip_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D"));
            ChangePropertyDescription("LockedDoorType", Lang.GetAttributeText(aLang.LockedDoorType_Byte_Description).Replace("<<Offset1>>", "0x6E"));
            ChangePropertyDescription("LockedDoorIndex", Lang.GetAttributeText(aLang.LockedDoorIndex_Byte_Description).Replace("<<Offset1>>", "0x6F"));
            ChangePropertyDescription("LocalTeleportationFacingAngle", Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("LocalTeleportationFacingAngle_Hex", Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("LadderFacingAngle", Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("LadderFacingAngle_Hex", Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("LadderStepCount", Lang.GetAttributeText(aLang.LadderStepCount_Sbyte_Description).Replace("<<Offset1>>", "0x6C"));
            ChangePropertyDescription("LadderParameter0", Lang.GetAttributeText(aLang.LadderParameter0_Byte_Description).Replace("<<Offset1>>", "0x6D"));
            ChangePropertyDescription("LadderParameter1", Lang.GetAttributeText(aLang.LadderParameter1_Byte_Description).Replace("<<Offset1>>", "0x6E"));
            ChangePropertyDescription("LadderParameter2", Lang.GetAttributeText(aLang.LadderParameter2_Byte_Description).Replace("<<Offset1>>", "0x6F"));
            ChangePropertyDescription("LadderParameter3", Lang.GetAttributeText(aLang.LadderParameter3_Byte_Description).Replace("<<Offset1>>", "0x70"));
            ChangePropertyDescription("Unknown_SG", Lang.GetAttributeText(aLang.Unknown_SG_Byte_Description).Replace("<<Offset1>>", "0x71"));
            ChangePropertyDescription("Unknown_SH", Lang.GetAttributeText(aLang.Unknown_SH_ByteArray2_Description).Replace("<<Offset1>>", "0x72").Replace("<<Offset2>>", "0x73"));


            ChangePropertyDescription("Unknown_RI_X", Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("Unknown_RI_X_Hex", Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("Unknown_RI_Y", Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("Unknown_RI_Y_Hex", Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("Unknown_RI_Z", Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("Unknown_RI_Z_Hex", Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("Unknown_RI_W", Lang.GetAttributeText(aLang.Unknown_RI_W_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("Unknown_RO", Lang.GetAttributeText(aLang.Unknown_RO_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("ItemNumber", Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x79").Replace("<<Offset2>>", "0x78"));
            ChangePropertyDescription("ItemNumber_ListBox", Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x79").Replace("<<Offset2>>", "0x78"));
            ChangePropertyDescription("Unknown_RU", Lang.GetAttributeText(aLang.Unknown_RU_ByteArray2_Description).Replace("<<Offset1>>", "0x7A").Replace("<<Offset2>>", "0x7B"));
            ChangePropertyDescription("ItemAmount", Lang.GetAttributeText(aLang.ItemAmount_Ushort_Description).Replace("<<Offset1>>", "0x7D").Replace("<<Offset2>>", "0x7C"));
            ChangePropertyDescription("SecundIndex", Lang.GetAttributeText(aLang.SecundIndex_Ushort_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E"));
            ChangePropertyDescription("ItemAuraType", Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x81").Replace("<<Offset2>>", "0x80"));
            ChangePropertyDescription("ItemAuraType_ListBox", Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x81").Replace("<<Offset2>>", "0x80"));
            ChangePropertyDescription("Unknown_QM", Lang.GetAttributeText(aLang.Unknown_QM_Byte_Description).Replace("<<Offset1>>", "0x82"));
            ChangePropertyDescription("Unknown_QL", Lang.GetAttributeText(aLang.Unknown_QL_Byte_Description).Replace("<<Offset1>>", "0x83"));
            ChangePropertyDescription("Unknown_QR", Lang.GetAttributeText(aLang.Unknown_QR_Byte_Description).Replace("<<Offset1>>", "0x84"));
            ChangePropertyDescription("Unknown_QH", Lang.GetAttributeText(aLang.Unknown_QH_Byte_Description).Replace("<<Offset1>>", "0x85"));
            ChangePropertyDescription("Unknown_QG", Lang.GetAttributeText(aLang.Unknown_QG_ByteArray2_Description).Replace("<<Offset1>>", "0x86").Replace("<<Offset2>>", "0x87"));
            ChangePropertyDescription("ItemTriggerRadius", Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x9B").Replace("<<Offset2>>", "0x9A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("ItemTriggerRadius_Hex", Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("ItemAngleX", Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription("ItemAngleX_Hex", Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription("ItemAngleY", Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription("ItemAngleY_Hex", Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription("ItemAngleZ", Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription("ItemAngleZ_Hex", Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription("ItemAngleW", Lang.GetAttributeText(aLang.ItemAngleW_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));


            ChangePropertyDescription("AshleyHidingPointX", Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("AshleyHidingPointX_Hex", Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("AshleyHidingPointY", Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription("AshleyHidingPointY_Hex", Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription("AshleyHidingPointZ", Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("AshleyHidingPointZ_Hex", Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("AshleyHidingZoneCorner0_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("AshleyHidingZoneCorner1_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("AshleyHidingZoneCorner2_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_X", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_X_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_Z", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription("AshleyHidingZoneCorner3_Z_Hex", Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription("Unknown_SM", Lang.GetAttributeText(aLang.Unknown_SM_ByteArray4_Description).Replace("<<Offset1>>", "0x5C").Replace("<<Offset2>>", "0x5D").Replace("<<Offset3>>", "0x5E").Replace("<<Offset4>>", "0x5F"));
            ChangePropertyDescription("Unknown_SN", Lang.GetAttributeText(aLang.Unknown_SN_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription("Unknown_SP", Lang.GetAttributeText(aLang.Unknown_SP_Byte_Description).Replace("<<Offset1>>", "0x90"));
            ChangePropertyDescription("Unknown_SQ", Lang.GetAttributeText(aLang.Unknown_SQ_Byte_Description).Replace("<<Offset1>>", "0x91"));
            ChangePropertyDescription("Unknown_SR", Lang.GetAttributeText(aLang.Unknown_SR_ByteArray2_Description).Replace("<<Offset1>>", "0x92").Replace("<<Offset2>>", "0x93"));
            ChangePropertyDescription("Unknown_SS", Lang.GetAttributeText(aLang.Unknown_SS_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));


            ChangePropertyDescription("GrappleGunEndPointX", Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("GrappleGunEndPointX_Hex", Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription("GrappleGunEndPointY", Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("GrappleGunEndPointY_Hex", Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription("GrappleGunEndPointZ", Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("GrappleGunEndPointZ_Hex", Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription("GrappleGunEndPointW", Lang.GetAttributeText(aLang.GrappleGunEndPointW_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("GrappleGunThirdPointX", Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("GrappleGunThirdPointX_Hex", Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription("GrappleGunThirdPointY", Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("GrappleGunThirdPointY_Hex", Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription("GrappleGunThirdPointZ", Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription("GrappleGunThirdPointZ_Hex", Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription("GrappleGunThirdPointW", Lang.GetAttributeText(aLang.GrappleGunThirdPointW_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription("GrappleGunFacingAngle", Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("GrappleGunFacingAngle_Hex", Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription("GrappleGunParameter0", Lang.GetAttributeText(aLang.GrappleGunParameter0_Byte_Description).Replace("<<Offset1>>", "0x84"));
            ChangePropertyDescription("GrappleGunParameter1", Lang.GetAttributeText(aLang.GrappleGunParameter1_Byte_Description).Replace("<<Offset1>>", "0x85"));
            ChangePropertyDescription("GrappleGunParameter2", Lang.GetAttributeText(aLang.GrappleGunParameter2_Byte_Description).Replace("<<Offset1>>", "0x86"));
            ChangePropertyDescription("GrappleGunParameter3", Lang.GetAttributeText(aLang.GrappleGunParameter3_Byte_Description).Replace("<<Offset1>>", "0x87"));
            ChangePropertyDescription("Unknown_SK", Lang.GetAttributeText(aLang.Unknown_SK_ByteArray4_Description).Replace("<<Offset1>>", "0x88").Replace("<<Offset2>>", "0x89").Replace("<<Offset3>>", "0x8A").Replace("<<Offset4>>", "0x8B"));
            ChangePropertyDescription("Unknown_SL", Lang.GetAttributeText(aLang.Unknown_SL_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));

        }

        public SpecialProperty(SpecialProperty prop)
        {
            SpecialPropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, prop.IsExtra, false);
        }

        public SpecialProperty(ushort InternalID, UpdateMethods updateMethods, SpecialMethods Methods, bool IsExtra = false, bool ForMultiSelection = false) : base()
        {
            SpecialPropertyConstructor(InternalID, updateMethods, Methods, IsExtra, ForMultiSelection);
        }

        private void SpecialPropertyConstructor(ushort InternalID, UpdateMethods updateMethods, SpecialMethods Methods, bool IsExtra = false, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.IsExtra = IsExtra;
            this.Methods = Methods;
            this.updateMethods = updateMethods;
            version = Methods.ReturnRe4Version();
            specialFileFormat = Methods.GetSpecialFileFormat();

            switch (specialFileFormat)
            {
                case SpecialFileFormat.AEV:
                    groupType = GroupType.AEV;
                    break;
                case SpecialFileFormat.ITA:
                    groupType = GroupType.ITA;
                    break;
            }

            if (!ForMultiSelection)
            {
                SetThis(this);
            }
         
            SetIsExtra();
            SetFloatType(Globals.PropertyGridUseHexFloat);
            SetPropertyTypeEnable();
            SetPropertyCategory();
            SetPropertyId();
            SetForMultiSelection(false); //a remover

            if (version == Re4Version.Classic)
            {
                SetClassicPropertyTexts();
            }
            else if (version == Re4Version.UHD)
            {
                SetUHDPropertyTexts();
            }
        }

        #region Category Ids
        private const int CategoryID0_InternalLineID = 0;
        private const int CategoryID1_LineArray = 1;
        private const int CategoryID2_SpecialType = 2;
        private const int CategoryID3_SpecialTriggerZone = 3;
        private const int CategoryID4_SpecialGeneral = 4;
        private const int CategoryID5_SpecialTypes = 5;
        private const int CategoryID6_FloatType = 6;
        #endregion

        #region Category Property

        [CustomCategory(aLang.Special_InternalLineIDCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0, CategoryID0_InternalLineID)]
        public string Category_InternalLineIDCategory { get => Lang.GetAttributeText(aLang.Special_InternalLineIDCategory); set { } }


        [CustomCategory(aLang.Special_LineArrayCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2, CategoryID1_LineArray)]
        public string Category_LineArrayCategory { get => Lang.GetAttributeText(aLang.Special_LineArrayCategory); set { } }

        [CustomCategory(aLang.SpecialTypeCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(4, CategoryID2_SpecialType)]
        public string Category_SpecialTypeCategory { get => Lang.GetAttributeText(aLang.SpecialTypeCategory); set { } }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(18, CategoryID3_SpecialTriggerZone)]
        public string Category_SpecialTriggerZoneCategory { get => Lang.GetAttributeText(aLang.SpecialTriggerZoneCategory); set { } }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(39, CategoryID4_SpecialGeneral)]
        public string Category_SpecialGeneralCategory { get => Lang.GetAttributeText(aLang.SpecialGeneralCategory); set { } }

        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x5BFF, CategoryID5_SpecialTypes)]
        public string Category_SpecialTypes { get => categorySpecialTypes; set { } }
        private string categorySpecialTypes = null;

        [CustomCategory(aLang.FloatTypeCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(99998, CategoryID6_FloatType)]
        public string Category_FloatTypeCategory { get => Lang.GetAttributeText(aLang.FloatTypeCategory); set { } }

        #endregion


        #region firt propertys

        [CustomCategory(aLang.Special_InternalLineIDCategory)]
        [CustomDisplayName(aLang.Special_InternalLineIDDisplayName)]
        [CustomDescription(aLang.Special_InternalLineIDDescription)]
        [DefaultValue(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        [DynamicTypeDescriptor.Id(1, CategoryID0_InternalLineID)]
        public string InternalLineID { get => GetInternalID().ToString(); }


        [CustomCategory(aLang.Special_LineArrayCategory)]
        [CustomDisplayName(aLang.Special_LineArrayDisplayName)]
        [CustomDescription(aLang.Special_LineArrayDescription)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumberAttribute()]
        [DefaultValue(null)]
        [ReadOnlyAttribute(false)]
        [BrowsableAttribute(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID1_LineArray)]
        public byte[] Line
        {
            get => Methods.ReturnLine(InternalID);
            set
            {
                // classic ITA
                byte[] _set = new byte[176];
                byte[] insert = value.Take(176).ToArray();

                // Classic AEV
                if (specialFileFormat == SpecialFileFormat.AEV)
                {
                    _set = new byte[160];
                    insert = value.Take(160).ToArray();
                }
                // UHD  ITA e AEV
                if (version == Re4Version.UHD)
                {
                    _set = new byte[156];
                    insert = value.Take(156).ToArray();
                }

                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);

                Methods.SetLine(InternalID, _set);
                //
                SetFloatType(Globals.PropertyGridUseHexFloat);
                SetPropertyTypeEnable();
                SetPropertyCategory();
                SetPropertyId();
                DataBase.Extras.UpdateExtraNodes(InternalID, Methods.GetSpecialType(InternalID), specialFileFormat);
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region // special property

        [CustomCategory(aLang.SpecialTypeCategory)]
        [CustomDisplayName(aLang.SpecialTypeID_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(5, CategoryID2_SpecialType)]
        public byte SpecialTypeID
        {
            get => Methods.ReturnSpecialType(InternalID);
            set
            {
                Methods.SetSpecialType(InternalID, value);
                SetFloatType(Globals.PropertyGridUseHexFloat);
                SetPropertyTypeEnable();
                SetPropertyCategory();
                SetPropertyId();
                DataBase.Extras.UpdateExtraNodes(InternalID, Utils.ToSpecialType(value), specialFileFormat);
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTypeCategory)]
        [CustomDisplayName(aLang.SpecialTypeID_List_DisplayName)]
        [Editor(typeof(SpecialTypeGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(6, CategoryID2_SpecialType)]
        public ByteObjForListBox SpecialTypeID_ListBox
        {
            get
            {
                var v = Methods.GetSpecialType(InternalID);
                if (ListBoxProperty.SpecialTypeList.ContainsKey(v))
                {
                    return ListBoxProperty.SpecialTypeList[v];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    Methods.SetSpecialType(InternalID, value.ID);
                    SetFloatType(Globals.PropertyGridUseHexFloat);
                    SetPropertyTypeEnable();
                    SetPropertyCategory();
                    SetPropertyId();
                    DataBase.Extras.UpdateExtraNodes(InternalID, Utils.ToSpecialType(value.ID), specialFileFormat);
                    updateMethods.UpdateMoveObjSelection();
                    updateMethods.UpdateGL();
                }
            }
        }

        [CustomCategory(aLang.SpecialTypeCategory)]
        [CustomDisplayName(aLang.SpecialIndex_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(7, CategoryID2_SpecialType)]
        public byte SpecialIndex
        {
            get => Methods.ReturnSpecialIndex(InternalID);
            set
            {
                Methods.SetSpecialIndex(InternalID, value);
                
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.Special_Category_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(19, CategoryID3_SpecialTriggerZone)]
        public byte Category
        {
            get => Methods.ReturnCategoy(InternalID);
            set
            {
                Methods.SetCategoy(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.Special_Category_List_DisplayName)]
        [Editor(typeof(SpecialZoneCategoryGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(20, CategoryID3_SpecialTriggerZone)]
        public ByteObjForListBox Category_ListBox
        {
            get
            {
                byte v = Methods.ReturnCategoy(InternalID);
                if (v <= 0x02)
                {
                    return ListBoxProperty.SpecialZoneCategoryList[v];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxSpecialZoneCategoryAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    Methods.SetCategoy(InternalID, value.ID);
                    updateMethods.UpdateGL();
                }
            }
        }

        #endregion

        #region SpecialTriggerZoneCategory float

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneTrueY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(21, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneTrueY
        {
            get => Methods.ReturnTriggerZoneTrueY(InternalID);
            set
            {
                Methods.SetTriggerZoneTrueY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneMoreHeight_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(22, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneMoreHeight
        {
            get => Methods.ReturnTriggerZoneMoreHeight(InternalID);
            set
            {
                Methods.SetTriggerZoneMoreHeight(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCircleRadius_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(23, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCircleRadius
        {
            get => Methods.ReturnTriggerZoneCircleRadius(InternalID);
            set
            {
                Methods.SetTriggerZoneCircleRadius(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(24, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner0_X
        {
            get => Methods.ReturnTriggerZoneCorner0_X(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner0_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(25, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner0_Z
        {
            get => Methods.ReturnTriggerZoneCorner0_Z(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner0_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(26, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner1_X
        {
            get => Methods.ReturnTriggerZoneCorner1_X(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner1_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(27, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner1_Z
        {
            get => Methods.ReturnTriggerZoneCorner1_Z(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner1_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(28, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner2_X
        {
            get => Methods.ReturnTriggerZoneCorner2_X(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner2_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(29, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner2_Z
        {
            get => Methods.ReturnTriggerZoneCorner2_Z(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner2_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(30, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner3_X
        {
            get => Methods.ReturnTriggerZoneCorner3_X(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner3_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(31, CategoryID3_SpecialTriggerZone)]
        public float TriggerZoneCorner3_Z
        {
            get => Methods.ReturnTriggerZoneCorner3_Z(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner3_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region SpecialTriggerZoneCategory hex

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneTrueY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(21, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneTrueY_Hex
        {
            get => Methods.ReturnTriggerZoneTrueY_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneTrueY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneMoreHeight_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(22, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneMoreHeight_Hex
        {
            get => Methods.ReturnTriggerZoneMoreHeight_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneMoreHeight_Hex(InternalID, value); 
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCircleRadius_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(23, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCircleRadius_Hex
        {
            get => Methods.ReturnTriggerZoneCircleRadius_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCircleRadius_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(24, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner0_X_Hex
        {
            get => Methods.ReturnTriggerZoneCorner0_X_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner0_X_Hex(InternalID, value); 
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(25, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner0_Z_Hex
        {
            get => Methods.ReturnTriggerZoneCorner0_Z_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner0_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(26, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner1_X_Hex
        {
            get => Methods.ReturnTriggerZoneCorner1_X_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner1_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(27, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner1_Z_Hex
        {
            get => Methods.ReturnTriggerZoneCorner1_Z_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner1_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(28, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner2_X_Hex
        {
            get => Methods.ReturnTriggerZoneCorner2_X_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner2_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(29, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner2_Z_Hex
        {
            get => Methods.ReturnTriggerZoneCorner2_Z_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner2_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(30, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner3_X_Hex
        {
            get => Methods.ReturnTriggerZoneCorner3_X_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner3_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(31, CategoryID3_SpecialTriggerZone)]
        public uint TriggerZoneCorner3_Z_Hex
        {
            get => Methods.ReturnTriggerZoneCorner3_Z_Hex(InternalID);
            set
            {
                Methods.SetTriggerZoneCorner3_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region Special Geral //"general"

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_GG_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(40, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_GG
        {
            get => Methods.ReturnUnknown_GG(InternalID);
            set 
            {
                byte[] _set = new byte[4];
                Unknown_GG.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 4; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_GG(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_GH_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(41, CategoryID4_SpecialGeneral)]
        public byte Unknown_GH 
        {
            get => Methods.ReturnUnknown_GH(InternalID);
            set
            {
                Methods.SetUnknown_GH(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_GK_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(42, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_GK
        {
            get => Methods.ReturnUnknown_GK(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_GK.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_GK(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_KG_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3400, CategoryID4_SpecialGeneral)]
        public byte Unknown_KG
        {
            get => Methods.ReturnUnknown_KG(InternalID);
            set
            {
                Methods.SetUnknown_KG(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_KJ_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3700, CategoryID4_SpecialGeneral)]
        public byte Unknown_KJ
        {
            get => Methods.ReturnUnknown_KJ(InternalID);
            set
            {
                Methods.SetUnknown_KJ(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_LI_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3800, CategoryID4_SpecialGeneral)]
        public byte Unknown_LI
        {
            get => Methods.ReturnUnknown_LI(InternalID);
            set
            {
                Methods.SetUnknown_LI(InternalID, value);
                
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_LO_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3900, CategoryID4_SpecialGeneral)]
        public byte Unknown_LO
        {
            get => Methods.ReturnUnknown_LO(InternalID);
            set
            {
                Methods.SetUnknown_LO(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_LU_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3A00, CategoryID4_SpecialGeneral)]
        public byte Unknown_LU
        {
            get => Methods.ReturnUnknown_LU(InternalID);
            set
            {
                Methods.SetUnknown_LU(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_LH_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3B00, CategoryID4_SpecialGeneral)]
        public byte Unknown_LH
        {
            get => Methods.ReturnUnknown_LH(InternalID);
            set
            {
                Methods.SetUnknown_LH(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_MI_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3C00, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_MI
        {
            get => Methods.ReturnUnknown_MI(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_MI.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_MI(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_MO_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x3E00, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_MO
        {
            get => Methods.ReturnUnknown_MO(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_MO.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_MO(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_MU_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4000, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_MU
        {
            get => Methods.ReturnUnknown_MU(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_MU.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_MU(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_NI_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4200, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_NI
        {
            get => Methods.ReturnUnknown_NI(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_NI.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_NI(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_NO_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4400, CategoryID4_SpecialGeneral)]
        public byte Unknown_NO
        {
            get => Methods.ReturnUnknown_NO(InternalID);
            set
            {
                Methods.SetUnknown_NO(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_NS_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4500, CategoryID4_SpecialGeneral)]
        public byte Unknown_NS
        {
            get => Methods.ReturnUnknown_NS(InternalID);
            set
            {
                Methods.SetUnknown_NS(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.RefInteractionType_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4600, CategoryID4_SpecialGeneral)]
        public byte RefInteractionType
        {
            get => Methods.ReturnRefInteractionType(InternalID);
            set
            {
                Methods.SetRefInteractionType(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.RefInteractionType_List_DisplayName)]
        [Editor(typeof(RefInteractionTypeGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(0x4601, CategoryID4_SpecialGeneral)]
        public ByteObjForListBox RefInteractionType_ListBox
        {
            get
            {
                byte v = Methods.ReturnRefInteractionType(InternalID);
                if (v <= 0x02)
                {
                    return ListBoxProperty.RefInteractionTypeList[v];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxRefInteractionTypeAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    Methods.SetRefInteractionType(InternalID, value.ID);
                    updateMethods.UpdateGL();
                }
            }
        }



        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.RefInteractionIndex_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4700, CategoryID4_SpecialGeneral)]
        public byte RefInteractionIndex
        {
            get => Methods.ReturnRefInteractionIndex(InternalID);
            set
            {
                Methods.SetRefInteractionIndex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_NT_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4800, CategoryID4_SpecialGeneral)]
        public byte Unknown_NT
        {
            get => Methods.ReturnUnknown_NT(InternalID);
            set
            {
                Methods.SetUnknown_NT(InternalID, value);
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_NU_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4900, CategoryID4_SpecialGeneral)]
        public byte Unknown_NU
        {
            get => Methods.ReturnUnknown_NU(InternalID);
            set
            {
                Methods.SetUnknown_NU(InternalID, value);
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.PromptMessage_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4A00, CategoryID4_SpecialGeneral)]
        public byte PromptMessage
        {
            get => Methods.ReturnPromptMessage(InternalID);
            set
            {
                Methods.SetPromptMessage(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.PromptMessage_List_DisplayName)]
        [Editor(typeof(PromptMessageGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(0x4A01, CategoryID4_SpecialGeneral)]
        public ByteObjForListBox PromptMessage_ListBox
        {
            get
            {
                byte v = Methods.ReturnPromptMessage(InternalID);
                if (ListBoxProperty.PromptMessageList.ContainsKey(v))
                {
                    return ListBoxProperty.PromptMessageList[v];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxPromptMessageTypeAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    Methods.SetPromptMessage(InternalID, value.ID);
                }
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_PI_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4B00, CategoryID4_SpecialGeneral)]
        public byte Unknown_PI
        {
            get => Methods.ReturnUnknown_PI(InternalID);
            set
            {
                Methods.SetUnknown_PI(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_PO_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x4C00, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_PO
        {
            get => Methods.ReturnUnknown_PO(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_PO.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 4; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_PO(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_PU_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5000, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_PU
        {
            get => Methods.ReturnUnknown_PU(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_PU.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_PU(InternalID, _set);
                
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_PK_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5200, CategoryID4_SpecialGeneral)]
        public byte Unknown_PK
        {
            get => Methods.ReturnUnknown_PK(InternalID);
            set
            {
                Methods.SetUnknown_PK(InternalID, value);
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.MessageColor_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5300, CategoryID4_SpecialGeneral)]
        public byte MessageColor
        {
            get => Methods.ReturnMessageColor(InternalID);
            set
            {
                Methods.SetMessageColor(InternalID, value);
            }
        }


        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_QI_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5400, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_QI
        {
            get => Methods.ReturnUnknown_QI(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_QI.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 4; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_QI(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_QO_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5800, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_QO
        {
            get => Methods.ReturnUnknown_QO(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_QO.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 4; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_QO(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialGeneralCategory)]
        [CustomDisplayName(aLang.Unknown_QU_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x58FF, CategoryID4_SpecialGeneral)]
        public byte[] Unknown_QU // Somente para o Classic
        {
            get => Methods.ReturnUnknown_QU(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_QU.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 4; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_QU(InternalID, _set);
                
            }
        }
        #endregion


        #region Unknown/geral types

        [CustomDisplayName(aLang.Unknown_HH_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5C00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_HH
        {
            get => Methods.ReturnUnknown_HH(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_HH.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_HH(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_HK_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5E00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_HK
        {
            get => Methods.ReturnUnknown_HK(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_HK.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_HK(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_HL_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6000, CategoryID5_SpecialTypes)]
        public byte[] Unknown_HL
        {
            get => Methods.ReturnUnknown_HL(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_HL.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_HL(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_HM_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6200, CategoryID5_SpecialTypes)]
        public byte[] Unknown_HM
        {
            get => Methods.ReturnUnknown_HM(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_HM.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_HM(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_HN_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6400, CategoryID5_SpecialTypes)]
        public byte[] Unknown_HN
        {
            get => Methods.ReturnUnknown_HN(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_HN.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_HN(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_HR_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6600, CategoryID5_SpecialTypes)]
        public byte[] Unknown_HR
        {
            get => Methods.ReturnUnknown_HR(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_HR.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_HR(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RH_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RH
        {
            get => Methods.ReturnUnknown_RH(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RH.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RH(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RJ_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6A00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RJ
        {
            get => Methods.ReturnUnknown_RJ(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RJ.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RJ(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RK_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RK
        {
            get => Methods.ReturnUnknown_RK(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RK.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RK(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RL_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6E00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RL
        {
            get => Methods.ReturnUnknown_RL(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RL.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RL(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RM_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7000, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RM
        {
            get => Methods.ReturnUnknown_RM(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RM.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RM(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RN_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7200, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RN
        {
            get => Methods.ReturnUnknown_RN(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RN.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RN(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RP_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7400, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RP
        {
            get => Methods.ReturnUnknown_RP(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RP.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RP(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_RQ_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7600, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RQ
        {
            get => Methods.ReturnUnknown_RQ(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RQ.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RQ(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TG_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7800, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TG
        {
            get => Methods.ReturnUnknown_TG(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TH.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TG(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TH_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7C00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TH
        {
            get => Methods.ReturnUnknown_TH(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TH.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TH(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TJ_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8000, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TJ
        {
            get => Methods.ReturnUnknown_TJ(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TJ.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TJ(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TK_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8400, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TK
        {
            get => Methods.ReturnUnknown_TK(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TK.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TK(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TL_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8800, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TL
        {
            get => Methods.ReturnUnknown_TL(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TL.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TL(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TM_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8C00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TM
        {
            get => Methods.ReturnUnknown_TM(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TM.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TM(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TN_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9000, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TN
        {
            get => Methods.ReturnUnknown_TN(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TN.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TN(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TP_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9400, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TP
        {
            get => Methods.ReturnUnknown_TP(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TP.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TP(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_TQ_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9800, CategoryID5_SpecialTypes)]
        public byte[] Unknown_TQ
        {
            get => Methods.ReturnUnknown_TQ(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_TQ.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_TQ(InternalID, _set);

            }
        }

        #endregion

        #region end only ITA Classic

        [CustomDisplayName(aLang.Unknown_VS_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0xA000, CategoryID5_SpecialTypes)]
        public byte[] Unknown_VS // Somente para o ITA Classic 
        {
            get => Methods.ReturnUnknown_VS(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_VS.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_VS(InternalID, _set);

            }
        }

         [CustomDisplayName(aLang.Unknown_VT_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0xA400, CategoryID5_SpecialTypes)]
        public byte[] Unknown_VT // Somente para o ITA Classic 
        {
            get => Methods.ReturnUnknown_VT(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_VT.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_VT(InternalID, _set);

            }
        }

        [CustomDisplayName(aLang.Unknown_VI_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0xA800, CategoryID5_SpecialTypes)]
        public byte[] Unknown_VI // Somente para o ITA Classic 
        {
            get => Methods.ReturnUnknown_VI(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_VI.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_VI(InternalID, _set);

            }
        }


        [CustomDisplayName(aLang.Unknown_VO_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0xAC00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_VO // Somente para o ITA Classic 
        {
            get => Methods.ReturnUnknown_VO(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_VO.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_VO(InternalID, _set);

            }
        }

        #endregion


        #region ObjPoint for Type 0x03, 0x10, 0x12, 0x13, 0x15, float

        [CustomDisplayName(aLang.ObjPointX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5C00, CategoryID5_SpecialTypes)]
        public float ObjPointX
        {
            get => Methods.ReturnObjPointX(InternalID);
            set
            {
                Methods.SetObjPointX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomDisplayName(aLang.ObjPointY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6000, CategoryID5_SpecialTypes)]
        public float ObjPointY
        {
            get => Methods.ReturnObjPointY(InternalID);
            set
            {
                Methods.SetObjPointY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomDisplayName(aLang.ObjPointZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6400, CategoryID5_SpecialTypes)]
        public float ObjPointZ
        {
            get => Methods.ReturnObjPointZ(InternalID);
            set
            {
                Methods.SetObjPointZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region ObjPoint for Type 0x03, 0x10, 0x12, 0x13, 0x15, hex

        [CustomDisplayName(aLang.ObjPointX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x5C00, CategoryID5_SpecialTypes)]
        public uint ObjPointX_Hex
        {
            get => Methods.ReturnObjPointX_Hex(InternalID);
            set
            {
                Methods.SetObjPointX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomDisplayName(aLang.ObjPointY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6000, CategoryID5_SpecialTypes)]
        public uint ObjPointY_Hex
        {
            get => Methods.ReturnObjPointY_Hex(InternalID);
            set
            {
                Methods.SetObjPointY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomDisplayName(aLang.ObjPointZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6400, CategoryID5_SpecialTypes)]
        public uint ObjPointZ_Hex
        {
            get => Methods.ReturnObjPointZ_Hex(InternalID);
            set
            {
                Methods.SetObjPointZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region ObjPoint.W

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ObjPointW_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public byte[] ObjPointW
        {
            get => Methods.ReturnObjPointW(InternalID);
            set
            {
                byte[] _set = new byte[4];
                ObjPointW.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetObjPointW(InternalID, _set);

            }
        }


        [CustomDisplayName(aLang.ObjPointW_onlyClassic_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x67F0, CategoryID5_SpecialTypes)]
        public byte[] ObjPointW_onlyClassic
        {
            get => Methods.ReturnObjPointW_onlyClassic(InternalID);
            set
            {
                byte[] _set = new byte[4];
                ObjPointW_onlyClassic.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetObjPointW_onlyClassic(InternalID, _set);

            }
        }

        #endregion



        #region only Itens Property // P1 floats

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public float Unknown_RI_X
        {
            get => Methods.ReturnUnknown_RI_X(InternalID);
            set
            {
                Methods.SetUnknown_RI_X(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_Y_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7000, CategoryID5_SpecialTypes)]
        public float Unknown_RI_Y
        {
            get => Methods.ReturnUnknown_RI_Y(InternalID);
            set
            {
                Methods.SetUnknown_RI_Y(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7400, CategoryID5_SpecialTypes)]
        public float Unknown_RI_Z
        {
            get => Methods.ReturnUnknown_RI_Z(InternalID);
            set
            {
                Methods.SetUnknown_RI_Z(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region only Itens Property // P1 hex

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public uint Unknown_RI_X_Hex
        {
            get => Methods.ReturnUnknown_RI_X_Hex(InternalID);
            set
            {
                Methods.SetUnknown_RI_X_Hex(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_Y_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7000, CategoryID5_SpecialTypes)]
        public uint Unknown_RI_Y_Hex
        {
            get => Methods.ReturnUnknown_RI_Y_Hex(InternalID);
            set
            {
                Methods.SetUnknown_RI_Y_Hex(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7400, CategoryID5_SpecialTypes)]
        public uint Unknown_RI_Z_Hex
        {
            get => Methods.ReturnUnknown_RI_Z_Hex(InternalID);
            set
            {
                Methods.SetUnknown_RI_Z_Hex(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region only Itens Property // P2 meio

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RI_W_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7500, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RI_W // somente em Classic
        {
            get => Methods.ReturnUnknown_RI_W(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_RI_W.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_RI_W(InternalID, _set);

            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RO_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7600, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RO // Somente para o Classic
        {
            get => Methods.ReturnUnknown_RO(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_RO.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 4; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_RO(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemNumber_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7800, CategoryID5_SpecialTypes)]
        public ushort ItemNumber
        {
            get => Methods.ReturnItemNumber(InternalID);
            set
            {
                Methods.SetItemNumber(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemNumber_List_DisplayName)]
        [Editor(typeof(ItemIDGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7900, CategoryID5_SpecialTypes)]
        public UshortObjForListBox ItemNumber_ListBox
        {
            get
            {
                ushort v = Methods.ReturnItemNumber(InternalID);
                if (ListBoxProperty.ItemsList.ContainsKey(v))
                {
                    return ListBoxProperty.ItemsList[v];
                }
                else
                {
                    return new UshortObjForListBox(0xFFFF, "XXXX: " + Lang.GetAttributeText(aLang.ListBoxUnknownItem));
                }
            }
            set
            {
                if (value.ID < 0xFFFF)
                {
                    Methods.SetItemNumber(InternalID, value.ID);
                    updateMethods.UpdateGL();
                }
            }
        }


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_RU_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7A00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_RU
        {
            get => Methods.ReturnUnknown_RU(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_RU.CopyTo(_set, 0);
                for (int i = 0; i < value.Length && i < 2; i++)
                {
                    _set[i] = value[i];
                }
                Methods.SetUnknown_RU(InternalID, _set);
                
            }
        }


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAmount_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7C00, CategoryID5_SpecialTypes)]
        public ushort ItemAmount
        {
            get => Methods.ReturnItemAmount(InternalID);
            set
            {
                Methods.SetItemAmount(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.SecundIndex_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7E00, CategoryID5_SpecialTypes)]
        public ushort SecundIndex
        {
            get => Methods.ReturnSecundIndex(InternalID);
            set
            {
                Methods.SetSecundIndex(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAuraType_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8000, CategoryID5_SpecialTypes)]
        public ushort ItemAuraType
        {
            get => Methods.ReturnItemAuraType(InternalID);
            set
            {
                Methods.SetItemAuraType(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAuraType_List_DisplayName)]
        [Editor(typeof(ItemAuraTypeGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(0x8100, CategoryID5_SpecialTypes)]
        public UshortObjForListBox ItemAuraType_ListBox
        {
            get
            {
                ushort v = Methods.ReturnItemAuraType(InternalID);
                if (v <= 0x09)
                {
                    return ListBoxProperty.ItemAuraTypeList[v];
                }
                else
                {
                    return new UshortObjForListBox(0xFFFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxItemAuraTypeAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFFFF)
                {
                    Methods.SetItemAuraType(InternalID, value.ID);
                    
                    updateMethods.UpdateGL();
                }
            }
        }


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_QM_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8200, CategoryID5_SpecialTypes)]
        public byte Unknown_QM
        {
            get => Methods.ReturnUnknown_QM(InternalID);
            set
            {
                Methods.SetUnknown_QM(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_QL_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8300, CategoryID5_SpecialTypes)]
        public byte Unknown_QL
        {
            get => Methods.ReturnUnknown_QL(InternalID);
            set
            {
                Methods.SetUnknown_QL(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_QR_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8400, CategoryID5_SpecialTypes)]
        public byte Unknown_QR
        {
            get => Methods.ReturnUnknown_QR(InternalID);
            set
            {
                Methods.SetUnknown_QR(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_QH_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8500, CategoryID5_SpecialTypes)]
        public byte Unknown_QH
        {
            get => Methods.ReturnUnknown_QH(InternalID);
            set
            {
                Methods.SetUnknown_QH(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.Unknown_QG_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8600, CategoryID5_SpecialTypes)]
        public byte[] Unknown_QG
        {
            get => Methods.ReturnUnknown_QG(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_QG.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_QG(InternalID, _set);
                
            }
        }



        #endregion

        #region only Itens Property // P3 floats


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemTriggerRadius_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8800, CategoryID5_SpecialTypes)]
        public float ItemTriggerRadius
        {
            get => Methods.ReturnItemTriggerRadius(InternalID);
            set
            {
                Methods.SetItemTriggerRadius(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8C00, CategoryID5_SpecialTypes)]
        public float ItemAngleX
        {
            get => Methods.ReturnItemAngleX(InternalID);
            set
            {
                Methods.SetItemAngleX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9000, CategoryID5_SpecialTypes)]
        public float ItemAngleY
        {
            get => Methods.ReturnItemAngleY(InternalID);
            set
            {
                Methods.SetItemAngleY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9400, CategoryID5_SpecialTypes)]
        public float ItemAngleZ
        {
            get
            {
                if (!(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV))
                {
                    return Methods.ReturnItemAngleZ(InternalID);
                }
                return 0;
            }
            set
            {
                if (!(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV))
                {
                    Methods.SetItemAngleZ(InternalID, value);
                    updateMethods.UpdateGL();
                }
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleW_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9800, CategoryID5_SpecialTypes)]
        public byte[] ItemAngleW
        {
            get
            {
                if (!(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV))
                {
                    return Methods.ReturnItemAngleW(InternalID);
                }
                return new byte[4];
            }
            set
            {
                if (!(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV))
                {
                    byte[] _set = new byte[4];
                    ItemAngleW.CopyTo(_set, 0);
                    value.Take(4).ToArray().CopyTo(_set, 0);
                    Methods.SetItemAngleW(InternalID, _set);
                }
            }
        }
        #endregion

        #region only Itens Property // P3 hex


        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemTriggerRadius_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x8800, CategoryID5_SpecialTypes)]
        public uint ItemTriggerRadius_Hex
        {
            get => Methods.ReturnItemTriggerRadius_Hex(InternalID);
            set
            {
                Methods.SetItemTriggerRadius_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x8C00, CategoryID5_SpecialTypes)]
        public uint ItemAngleX_Hex
        {
            get => Methods.ReturnItemAngleX_Hex(InternalID);
            set
            {
                Methods.SetItemAngleX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x9000, CategoryID5_SpecialTypes)]
        public uint ItemAngleY_Hex
        {
            get => Methods.ReturnItemAngleY_Hex(InternalID);
            set
            {
                Methods.SetItemAngleY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType03_Items)]
        [CustomDisplayName(aLang.ItemAngleZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x9400, CategoryID5_SpecialTypes)]
        public uint ItemAngleZ_Hex
        {
            get
            {
                if (!(version == Re4Version.Classic && specialFileFormat == SpecialFileFormat.AEV))
                {
                    return Methods.ReturnItemAngleZ_Hex(InternalID);
                }
                return 0;
            }
            set
            {
                Methods.SetItemAngleZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion



        #region TYPE 0x04, 0x05, 0x0A and 0x11

        [CustomCategory(aLang.SpecialType11_ItemDependentEvents)]
        [CustomDisplayName(aLang.NeededItemNumber_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5C00, CategoryID5_SpecialTypes)]
        public ushort NeededItemNumber
        {
            get => Methods.ReturnNeededItemNumber(InternalID);
            set
            {
                Methods.SetNeededItemNumber(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType11_ItemDependentEvents)]
        [CustomDisplayName(aLang.NeededItemNumber_List_DisplayName)]
        [Editor(typeof(ItemIDGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x5D01, CategoryID5_SpecialTypes)]
        public UshortObjForListBox NeededItemNumber_ListBox
        {
            get
            {
                ushort v = Methods.ReturnNeededItemNumber(InternalID);
                if (ListBoxProperty.ItemsList.ContainsKey(v))
                {
                    return ListBoxProperty.ItemsList[v];
                }
                else
                {
                    return new UshortObjForListBox(0xFFFF, "XXXX: " + Lang.GetAttributeText(aLang.ListBoxUnknownItem));
                }
            }
            set
            {
                if (value.ID < 0xFFFF)
                {
                    Methods.SetNeededItemNumber(InternalID, value.ID);
                }
            }
        }


        [CustomCategory(aLang.SpecialType04_GroupedEnemyTrigger)]
        [CustomDisplayName(aLang.EnemyGroup_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5E00, CategoryID5_SpecialTypes)]
        public ushort EnemyGroup
        {
            get => Methods.ReturnEnemyGroup(InternalID);
            set
            {
                Methods.SetEnemyGroup(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType05_Message)]
        [CustomDisplayName(aLang.RoomMessage_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x5E00, CategoryID5_SpecialTypes)]
        public ushort RoomMessage
        {
            get => Methods.ReturnRoomMessage(InternalID);
            set
            {
                Methods.SetRoomMessage(InternalID, value);
                
            }
        }


        [CustomCategory(aLang.SpecialType05_Message)]
        [CustomDisplayName(aLang.MessageCutSceneID_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6000, CategoryID5_SpecialTypes)]
        public ushort MessageCutSceneID
        {
            get => Methods.ReturnMessageCutSceneID(InternalID);
            set
            {
                Methods.SetMessageCutSceneID(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType05_Message)]
        [CustomDisplayName(aLang.MessageID_Ushort_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6200, CategoryID5_SpecialTypes)]
        public ushort MessageID
        {
            get => Methods.ReturnMessageID(InternalID);
            set
            {
                Methods.SetMessageID(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType0A_DamagesThePlayer)]
        [CustomDisplayName(aLang.ActivationType_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6000, CategoryID5_SpecialTypes)]
        public byte ActivationType
        {
            get => Methods.ReturnActivationType(InternalID);
            set
            {
                Methods.SetActivationType(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType0A_DamagesThePlayer)]
        [CustomDisplayName(aLang.DamageType_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6100, CategoryID5_SpecialTypes)]
        public byte DamageType
        {
            get => Methods.ReturnDamageType(InternalID);
            set
            {
                Methods.SetDamageType(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType0A_DamagesThePlayer)]
        [CustomDisplayName(aLang.BlockingType_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6200, CategoryID5_SpecialTypes)]
        public byte BlockingType
        {
            get => Methods.ReturnBlockingType(InternalID);
            set
            {
                Methods.SetBlockingType(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType0A_DamagesThePlayer)]
        [CustomDisplayName(aLang.Unknown_SJ_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6300, CategoryID5_SpecialTypes)]
        public byte Unknown_SJ
        {
            get => Methods.ReturnUnknown_SJ(InternalID);
            set
            {
                Methods.SetUnknown_SJ(InternalID, value);
                
            }
        }


        [CustomCategory(aLang.SpecialType0A_DamagesThePlayer)]
        [CustomDisplayName(aLang.DamageAmount_Ushort_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6400, CategoryID5_SpecialTypes)]
        public ushort DamageAmount
        {
            get => Methods.ReturnDamageAmount(InternalID);
            set
            {
                Methods.SetDamageAmount(InternalID, value);
                
            }
        }


        #endregion


        #region type 0x13 LocalTeleportation

        [CustomCategory(aLang.SpecialType13_LocalTeleportation)]
        [CustomDisplayName(aLang.TeleportationFacingAngle_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public float LocalTeleportationFacingAngle
        {
            get => Methods.ReturnTeleportationFacingAngle(InternalID);
            set
            {
                Methods.SetTeleportationFacingAngle(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType13_LocalTeleportation)]
        [CustomDisplayName(aLang.TeleportationFacingAngle_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public uint LocalTeleportationFacingAngle_Hex
        {
            get => Methods.ReturnTeleportationFacingAngle_Hex(InternalID);
            set
            {
                Methods.SetTeleportationFacingAngle_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        #endregion


        #region Type 0x01 WarpDoor

        [CustomCategory(aLang.SpecialType01_WarpDoor)]
        [CustomDisplayName(aLang.DestinationFacingAngle_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public float DestinationFacingAngle
        {
            get => Methods.ReturnDestinationFacingAngle(InternalID);
            set
            {
                Methods.SetDestinationFacingAngle(InternalID, value);
                updateMethods.UpdateGL();
            }
        }
        [CustomCategory(aLang.SpecialType01_WarpDoor)]
        [CustomDisplayName(aLang.DestinationFacingAngle_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public uint DestinationFacingAngle_Hex
        {
            get => Methods.ReturnDestinationFacingAngle_Hex(InternalID);
            set
            {
                Methods.SetDestinationFacingAngle_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }



        [CustomCategory(aLang.SpecialType01_WarpDoor)]
        [CustomDisplayName(aLang.DestinationRoom_UshortUnflip_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public ushort DestinationRoom
        {
            get => Methods.ReturnDestinationRoom(InternalID);
            set
            {
                Methods.SetDestinationRoom(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType01_WarpDoor)]
        [CustomDisplayName(aLang.LockedDoorType_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6E00, CategoryID5_SpecialTypes)]
        public byte LockedDoorType
        {
            get => Methods.ReturnLockedDoorType(InternalID);
            set
            {
                Methods.SetLockedDoorType(InternalID, value);
            }
        }


        [CustomCategory(aLang.SpecialType01_WarpDoor)]
        [CustomDisplayName(aLang.LockedDoorIndex_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6F00, CategoryID5_SpecialTypes)]
        public byte LockedDoorIndex
        {
            get => Methods.ReturnLockedDoorIndex(InternalID);
            set
            {
                Methods.SetLockedDoorIndex(InternalID, value);
            }
        }



        #endregion 


        #region Type 0x10 LadderClimbUp

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderFacingAngle_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public float LadderFacingAngle
        {
            get => Methods.ReturnLadderFacingAngle(InternalID);
            set
            {
                Methods.SetLadderFacingAngle(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderFacingAngle_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public uint LadderFacingAngle_Hex
        {
            get => Methods.ReturnLadderFacingAngle_Hex(InternalID);
            set
            {
                Methods.SetLadderFacingAngle_Hex(InternalID, value);
                
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderStepCount_Sbyte_DisplayName)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public sbyte LadderStepCount
        {
            get => Methods.ReturnLadderStepCount(InternalID);
            set
            {
                Methods.SetLadderStepCount(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderParameter0_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6D00, CategoryID5_SpecialTypes)]
        public byte LadderParameter0
        {
            get => Methods.ReturnLadderParameter0(InternalID);
            set
            {
                Methods.SetLadderParameter0(InternalID, value);

            }
        }


        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderParameter1_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6E00, CategoryID5_SpecialTypes)]
        public byte LadderParameter1
        {
            get => Methods.ReturnLadderParameter1(InternalID);
            set
            {
                Methods.SetLadderParameter1(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderParameter2_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6F00, CategoryID5_SpecialTypes)]
        public byte LadderParameter2
        {
            get => Methods.ReturnLadderParameter2(InternalID);
            set
            {
                Methods.SetLadderParameter2(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.LadderParameter3_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7000, CategoryID5_SpecialTypes)]
        public byte LadderParameter3
        {
            get => Methods.ReturnLadderParameter3(InternalID);
            set
            {
                Methods.SetLadderParameter3(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.Unknown_SG_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7100, CategoryID5_SpecialTypes)]
        public byte Unknown_SG
        {
            get => Methods.ReturnUnknown_SG(InternalID);
            set
            {
                Methods.SetUnknown_SG(InternalID, value);
                
            }
        }

        [CustomCategory(aLang.SpecialType10_FixedLadderClimbUp)]
        [CustomDisplayName(aLang.Unknown_SH_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7200, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SH
        {
            get => Methods.ReturnUnknown_SH(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_SH.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SH(InternalID, _set);
                
            }
        }

        #endregion


        #region AshleyHiding

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.Unknown_SM_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7010, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SM
        {
            get => Methods.ReturnUnknown_SM(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_SM.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SM(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner0_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7020, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner0_X_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner0_X_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner0_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner0_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7030, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner0_Z_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner0_Z_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner0_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner1_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7040, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner1_X_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner1_X_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner1_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner1_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7050, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner1_Z_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner1_Z_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner1_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner2_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7060, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner2_X_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner2_X_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner2_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner2_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7070, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner2_Z_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner2_Z_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner2_Z_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner3_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7080, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner3_X_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner3_X_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner3_X_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner3_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7090, CategoryID5_SpecialTypes)]
        public uint AshleyHidingZoneCorner3_Z_Hex
        {
            get => Methods.ReturnAshleyHidingZoneCorner3_Z_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner3_Z_Hex(InternalID, value); 
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingPointX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x8000, CategoryID5_SpecialTypes)]
        public uint AshleyHidingPointX_Hex
        {
            get => Methods.ReturnAshleyHidingPointX_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingPointX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingPointY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x8400, CategoryID5_SpecialTypes)]
        public uint AshleyHidingPointY_Hex
        {
            get => Methods.ReturnAshleyHidingPointY_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingPointY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingPointZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x8800, CategoryID5_SpecialTypes)]
        public uint AshleyHidingPointZ_Hex
        {
            get => Methods.ReturnAshleyHidingPointZ_Hex(InternalID);
            set
            {
                Methods.SetAshleyHidingPointZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }




        // floats
        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner0_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7020, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner0_X
        {
            get => Methods.ReturnAshleyHidingZoneCorner0_X(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner0_X(InternalID, value); 
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner0_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7030, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner0_Z
        {
            get => Methods.ReturnAshleyHidingZoneCorner0_Z(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner0_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner1_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7040, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner1_X
        {
            get => Methods.ReturnAshleyHidingZoneCorner1_X(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner1_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner1_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7050, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner1_Z
        {
            get => Methods.ReturnAshleyHidingZoneCorner1_Z(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner1_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner2_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7060, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner2_X
        {
            get => Methods.ReturnAshleyHidingZoneCorner2_X(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner2_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner2_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7070, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner2_Z
        {
            get => Methods.ReturnAshleyHidingZoneCorner2_Z(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner2_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner3_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7080, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner3_X
        {
            get => Methods.ReturnAshleyHidingZoneCorner3_X(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner3_X(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingZoneCorner3_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7090, CategoryID5_SpecialTypes)]
        public float AshleyHidingZoneCorner3_Z
        {
            get => Methods.ReturnAshleyHidingZoneCorner3_Z(InternalID);
            set
            {
                Methods.SetAshleyHidingZoneCorner3_Z(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingPointX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8000, CategoryID5_SpecialTypes)]
        public float AshleyHidingPointX
        {
            get => Methods.ReturnAshleyHidingPointX(InternalID);
            set
            {
                Methods.SetAshleyHidingPointX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingPointY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8400, CategoryID5_SpecialTypes)]
        public float AshleyHidingPointY
        {
            get => Methods.ReturnAshleyHidingPointY(InternalID);
            set
            {
                Methods.SetAshleyHidingPointY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.AshleyHidingPointZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8800, CategoryID5_SpecialTypes)]
        public float AshleyHidingPointZ
        {
            get => Methods.ReturnAshleyHidingPointZ(InternalID);
            set
            {
                Methods.SetAshleyHidingPointZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.Unknown_SN_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8C00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SN
        {
            get => Methods.ReturnUnknown_SN(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_SN.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SN(InternalID, _set);
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.Unknown_SP_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9000, CategoryID5_SpecialTypes)]
        public byte Unknown_SP
        {
            get => Methods.ReturnUnknown_SP(InternalID);
            set
            {
                Methods.SetUnknown_SP(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.Unknown_SQ_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9100, CategoryID5_SpecialTypes)]
        public byte Unknown_SQ
        {
            get => Methods.ReturnUnknown_SQ(InternalID);
            set
            {
                Methods.SetUnknown_SQ(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.Unknown_SR_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9200, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SR
        {
            get => Methods.ReturnUnknown_SR(InternalID);
            set
            {
                byte[] _set = new byte[2];
                Unknown_SR.CopyTo(_set, 0);
                value.Take(2).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SR(InternalID, _set); 
            }
        }

        [CustomCategory(aLang.SpecialType12_AshleyHideCommand)]
        [CustomDisplayName(aLang.Unknown_SS_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x9400, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SS
        {
            get => Methods.ReturnUnknown_SS(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_SS.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SS(InternalID, _set);
            }
        }


        #endregion


        #region GrappleGun

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public uint GrappleGunEndPointX_Hex
        {
            get => Methods.ReturnGrappleGunEndPointX_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunEndPointX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public uint GrappleGunEndPointY_Hex
        {
            get => Methods.ReturnGrappleGunEndPointY_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunEndPointY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7000, CategoryID5_SpecialTypes)]
        public uint GrappleGunEndPointZ_Hex
        {
            get => Methods.ReturnGrappleGunEndPointZ_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunEndPointZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointX_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7400, CategoryID5_SpecialTypes)]
        public uint GrappleGunThirdPointX_Hex
        {
            get => Methods.ReturnGrappleGunThirdPointX_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunThirdPointX_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7800, CategoryID5_SpecialTypes)]
        public uint GrappleGunThirdPointY_Hex
        {
            get => Methods.ReturnGrappleGunThirdPointY_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunThirdPointY_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointZ_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x7C00, CategoryID5_SpecialTypes)]
        public uint GrappleGunThirdPointZ_Hex
        {
            get => Methods.ReturnGrappleGunThirdPointZ_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunThirdPointZ_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunFacingAngle_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0x8000, CategoryID5_SpecialTypes)]
        public uint GrappleGunFacingAngle_Hex
        {
            get => Methods.ReturnGrappleGunFacingAngle_Hex(InternalID);
            set
            {
                Methods.SetGrappleGunFacingAngle_Hex(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6800, CategoryID5_SpecialTypes)]
        public float GrappleGunEndPointX
        {
            get => Methods.ReturnGrappleGunEndPointX(InternalID);
            set
            {
                Methods.SetGrappleGunEndPointX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x6C00, CategoryID5_SpecialTypes)]
        public float GrappleGunEndPointY
        {
            get => Methods.ReturnGrappleGunEndPointY(InternalID);
            set
            {
                Methods.SetGrappleGunEndPointY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7000, CategoryID5_SpecialTypes)]
        public float GrappleGunEndPointZ
        {
            get => Methods.ReturnGrappleGunEndPointZ(InternalID);
            set
            {
                Methods.SetGrappleGunEndPointZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunEndPointW_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7300, CategoryID5_SpecialTypes)]
        public byte[] GrappleGunEndPointW
        {
            get => Methods.ReturnGrappleGunEndPointW(InternalID);
            set
            {
                byte[] _set = new byte[4];
                GrappleGunEndPointW.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetGrappleGunEndPointW(InternalID, _set);

            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointX_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7400, CategoryID5_SpecialTypes)]
        public float GrappleGunThirdPointX
        {
            get => Methods.ReturnGrappleGunThirdPointX(InternalID);
            set
            {
                Methods.SetGrappleGunThirdPointX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7800, CategoryID5_SpecialTypes)]
        public float GrappleGunThirdPointY
        {
            get => Methods.ReturnGrappleGunThirdPointY(InternalID);
            set
            {
                Methods.SetGrappleGunThirdPointY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointZ_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7C00, CategoryID5_SpecialTypes)]
        public float GrappleGunThirdPointZ
        {
            get => Methods.ReturnGrappleGunThirdPointZ(InternalID);
            set
            {
                Methods.SetGrappleGunThirdPointZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunThirdPointW_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x7F00, CategoryID5_SpecialTypes)]
        public byte[] GrappleGunThirdPointW
        {
            get => Methods.ReturnGrappleGunThirdPointW(InternalID);
            set
            {
                byte[] _set = new byte[4];
                GrappleGunThirdPointW.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetGrappleGunThirdPointW(InternalID, _set);

            }
        }


        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunFacingAngle_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8000, CategoryID5_SpecialTypes)]
        public float GrappleGunFacingAngle
        {
            get => Methods.ReturnGrappleGunFacingAngle(InternalID);
            set
            {
                Methods.SetGrappleGunFacingAngle(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunParameter0_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8400, CategoryID5_SpecialTypes)]
        public byte GrappleGunParameter0
        {
            get => Methods.ReturnGrappleGunParameter0(InternalID);
            set
            {
                Methods.SetGrappleGunParameter0(InternalID, value);      
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunParameter1_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8500, CategoryID5_SpecialTypes)]
        public byte GrappleGunParameter1
        {
            get => Methods.ReturnGrappleGunParameter1(InternalID);
            set
            {
                Methods.SetGrappleGunParameter1(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunParameter2_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8600, CategoryID5_SpecialTypes)]
        public byte GrappleGunParameter2
        {
            get => Methods.ReturnGrappleGunParameter2(InternalID);
            set
            {
                Methods.SetGrappleGunParameter2(InternalID, value);
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.GrappleGunParameter3_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8700, CategoryID5_SpecialTypes)]
        public byte GrappleGunParameter3
        {
            get => Methods.ReturnGrappleGunParameter3(InternalID);
            set
            {
                Methods.SetGrappleGunParameter3(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.Unknown_SK_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8800, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SK
        {
            get => Methods.ReturnUnknown_SK(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_SK.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SK(InternalID, _set);
                
            }
        }

        [CustomCategory(aLang.SpecialType15_AdaGrappleGun)]
        [CustomDisplayName(aLang.Unknown_SL_ByteArray4_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(0x8C00, CategoryID5_SpecialTypes)]
        public byte[] Unknown_SL
        {
            get => Methods.ReturnUnknown_SL(InternalID);
            set
            {
                byte[] _set = new byte[4];
                Unknown_SL.CopyTo(_set, 0);
                value.Take(4).ToArray().CopyTo(_set, 0);
                Methods.SetUnknown_SL(InternalID, _set);
                
            }
        }

        #endregion




        #region Change float/hex type
        // float type
        [CustomCategory(aLang.FloatTypeCategory)]
        [CustomDisplayName(aLang.FloatType_DisplayName)]
        [CustomDescription(aLang.FloatType_Description)]
        [Editor(typeof(HexFloatEnableGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(99999, CategoryID6_FloatType)]
        public BoolObjForListBox FloatType
        {
            get
            {
                return ListBoxProperty.FloatTypeList[Globals.PropertyGridUseHexFloat];
            }
            set
            {
                Globals.PropertyGridUseHexFloat = value.ID;
                SetFloatType(Globals.PropertyGridUseHexFloat);
                UpdateSetFloatTypeEvent?.Invoke();
            }
        }
        #endregion

        #region Search Methods


        public SpecialType GetSpecialType()
        {
            return Methods.GetSpecialType(InternalID);
        }

        public ushort ReturnUshortFirstSearchSelect()
        {
            var specialType = Methods.GetSpecialType(InternalID);
            if (specialType == SpecialType.T03_Items)
            {
                return Methods.ReturnItemNumber(InternalID);
            }
            else if (specialType == SpecialType.T11_ItemDependentEvents)
            {
                return Methods.ReturnNeededItemNumber(InternalID);
            }
            return ushort.MaxValue;
        }

        public void Searched(object obj)
        {
            if (obj is UshortObjForListBox ushortObj)
            {
                var specialType = Methods.GetSpecialType(InternalID);
                if (specialType == SpecialType.T03_Items)
                {
                    Methods.SetItemNumber(InternalID, ushortObj.ID);
                }
                else if (specialType == SpecialType.T11_ItemDependentEvents)
                {
                    Methods.SetNeededItemNumber(InternalID, ushortObj.ID);
                }         
                updateMethods.UpdateTreeViewObjs();
                updateMethods.UpdatePropertyGrid();
                updateMethods.UpdateGL();
            }
        }

        #endregion

    }
}
