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
    public class SpecialProperty : BaseTriggerZoneProperty, IInternalID
    {
        public override Type GetClassType()
        {
            return typeof(SpecialProperty);
        }

        private ushort InternalID = ushort.MaxValue;
        private GroupType groupType = GroupType.NULL;
        private SpecialFileFormat specialFileFormat = SpecialFileFormat.NULL;
        private Re4Version version = Re4Version.NULL;

        private SpecialMethods Methods = null;

        private bool IsExtra = false;

        public override ushort GetInternalID()
        {
            return InternalID;
        }

        public override GroupType GetGroupType()
        {
            return groupType;
        }

        public SpecialFileFormat GetSpecialFileFormat()
        {
            return specialFileFormat;
        }

        protected override BaseTriggerZoneMethods GetTriggerZoneMethods()
        {
            return Methods;
        }

        protected override void SetFloatType(bool IsHex)
        {
            SetTriggerZoneFloatType(Globals.PropertyGridUseHexFloat);

            ChangePropertyIsBrowsable(nameof(TriggerZoneTrueY), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneMoreHeight), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCircleRadius), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_X), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_Z), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_X), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_Z), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_X), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_Z), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_X), !IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_Z), !IsHex && !IsExtra);

            ChangePropertyIsBrowsable(nameof(TriggerZoneTrueY_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneMoreHeight_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCircleRadius_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_X_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_Z_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_X_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_Z_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_X_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_Z_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_X_Hex), IsHex && !IsExtra);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_Z_Hex), IsHex && !IsExtra);

            // types
            bool ifItem = Methods.GetSpecialType(InternalID) == SpecialType.T03_Items;
            bool IsWarpDoor = Methods.GetSpecialType(InternalID) == SpecialType.T01_WarpDoor;
            bool IsLocalTeleportation = Methods.GetSpecialType(InternalID) == SpecialType.T13_LocalTeleportation;
            bool IsLadderUp = Methods.GetSpecialType(InternalID) == SpecialType.T10_FixedLadderClimbUp;
            bool IsAshley = Methods.GetSpecialType(InternalID) == SpecialType.T12_AshleyHideCommand;
            bool IsGrappleGun = Methods.GetSpecialType(InternalID) == SpecialType.T15_AdaGrappleGun;

            bool ifClassicAev = !(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV);
            bool ItemFloat = ifItem && !IsHex;
            bool ItemHex = ifItem && IsHex;

            ChangePropertyIsBrowsable(nameof(ObjPointX), (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && !IsHex);
            ChangePropertyIsBrowsable(nameof(ObjPointY), (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && !IsHex);
            ChangePropertyIsBrowsable(nameof(ObjPointZ), (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && !IsHex);
            ChangePropertyIsBrowsable(nameof(ObjPointX_Hex), (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && IsHex);
            ChangePropertyIsBrowsable(nameof(ObjPointY_Hex), (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && IsHex);
            ChangePropertyIsBrowsable(nameof(ObjPointZ_Hex), (ifItem || IsWarpDoor || IsLocalTeleportation || IsGrappleGun || IsLadderUp) && IsHex);


            ChangePropertyIsBrowsable(nameof(Unknown_RI_X), ItemFloat);
            ChangePropertyIsBrowsable(nameof(Unknown_RI_Y), ItemFloat);
            ChangePropertyIsBrowsable(nameof(Unknown_RI_Z), ItemFloat);
            ChangePropertyIsBrowsable(nameof(Unknown_RI_X_Hex), ItemHex);
            ChangePropertyIsBrowsable(nameof(Unknown_RI_Y_Hex), ItemHex);
            ChangePropertyIsBrowsable(nameof(Unknown_RI_Z_Hex), ItemHex);


            ChangePropertyIsBrowsable(nameof(ItemTriggerRadius), ItemFloat);
            ChangePropertyIsBrowsable(nameof(ItemAngleX), ItemFloat);
            ChangePropertyIsBrowsable(nameof(ItemAngleY), ItemFloat);
            ChangePropertyIsBrowsable(nameof(ItemAngleZ), ItemFloat && ifClassicAev);
            ChangePropertyIsBrowsable(nameof(ItemTriggerRadius_Hex), ItemHex);
            ChangePropertyIsBrowsable(nameof(ItemAngleX_Hex), ItemHex);
            ChangePropertyIsBrowsable(nameof(ItemAngleY_Hex), ItemHex);
            ChangePropertyIsBrowsable(nameof(ItemAngleZ_Hex), ItemHex && ifClassicAev);


            ChangePropertyIsBrowsable(nameof(DestinationFacingAngle), IsWarpDoor && !IsHex);
            ChangePropertyIsBrowsable(nameof(DestinationFacingAngle_Hex), IsWarpDoor && IsHex);

            ChangePropertyIsBrowsable(nameof(LocalTeleportationFacingAngle), IsLocalTeleportation && !IsHex);
            ChangePropertyIsBrowsable(nameof(LocalTeleportationFacingAngle_Hex), IsLocalTeleportation && IsHex);

            ChangePropertyIsBrowsable(nameof(LadderFacingAngle), IsLadderUp && !IsHex);
            ChangePropertyIsBrowsable(nameof(LadderFacingAngle_Hex), IsLadderUp && IsHex);

            //IsAshley
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner0_X), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner0_Z), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner1_X), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner1_Z), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner2_X), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner2_Z), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner3_X), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner3_Z), IsAshley && !IsHex);

            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner0_X_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner0_Z_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner1_X_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner1_Z_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner2_X_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner2_Z_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner3_X_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingZoneCorner3_Z_Hex), IsAshley && IsHex);

            ChangePropertyIsBrowsable(nameof(AshleyHidingPointX), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingPointY), IsAshley && !IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingPointZ), IsAshley && !IsHex);

            ChangePropertyIsBrowsable(nameof(AshleyHidingPointX_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingPointY_Hex), IsAshley && IsHex);
            ChangePropertyIsBrowsable(nameof(AshleyHidingPointZ_Hex), IsAshley && IsHex);

            //IsGrappleGun
            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointX), IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointY), IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointZ), IsGrappleGun && !IsHex);

            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointX_Hex), IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointY_Hex), IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointZ_Hex), IsGrappleGun && IsHex);

            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointX), IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointY), IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointZ), IsGrappleGun && !IsHex);

            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointX_Hex), IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointY_Hex), IsGrappleGun && IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointZ_Hex), IsGrappleGun && IsHex);

            ChangePropertyIsBrowsable(nameof(GrappleGunFacingAngle), IsGrappleGun && !IsHex);
            ChangePropertyIsBrowsable(nameof(GrappleGunFacingAngle_Hex), IsGrappleGun && IsHex);

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

            bool ifNotClassicAev = !(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV);

            // only itens
            ChangePropertyIsBrowsable(nameof(ObjPointW), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_RI_W), IsItem && version == Re4Version.V2007PS2);
            ChangePropertyIsBrowsable(nameof(Unknown_RO), IsItem && version == Re4Version.V2007PS2);
            ChangePropertyIsBrowsable(nameof(ItemNumber), IsItem);
            ChangePropertyIsBrowsable(nameof(ItemNumber_ListBox), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_RU), IsItem);
            ChangePropertyIsBrowsable(nameof(ItemAmount), IsItem);
            ChangePropertyIsBrowsable(nameof(SecundIndex), IsItem);
            ChangePropertyIsBrowsable(nameof(ItemAuraType), IsItem);
            ChangePropertyIsBrowsable(nameof(ItemAuraType_ListBox), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_QM), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_QL), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_QR), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_QH), IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_QG), IsItem);
            ChangePropertyIsBrowsable(nameof(ItemAngleW), IsItem && ifNotClassicAev);

            // ita classic
            ChangePropertyIsBrowsable(nameof(Unknown_VS), version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.ITA && !IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_VT), version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.ITA && !IsItem);
            ChangePropertyIsBrowsable(nameof(Unknown_VI), version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.ITA);
            ChangePropertyIsBrowsable(nameof(Unknown_VO), version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.ITA);

            ChangePropertyIsBrowsable(nameof(DestinationRoom), IsWarpDoor);
            ChangePropertyIsBrowsable(nameof(LockedDoorType), IsWarpDoor);
            ChangePropertyIsBrowsable(nameof(LockedDoorIndex), IsWarpDoor);
            ChangePropertyIsBrowsable(nameof(NeededItemNumber), IsT11);
            ChangePropertyIsBrowsable(nameof(NeededItemNumber_ListBox), IsT11);
            ChangePropertyIsBrowsable(nameof(EnemyGroup), IsT04);
            ChangePropertyIsBrowsable(nameof(RoomMessage), IsMessage);
            ChangePropertyIsBrowsable(nameof(MessageCutSceneID), IsMessage);
            ChangePropertyIsBrowsable(nameof(MessageID), IsMessage);
            ChangePropertyIsBrowsable(nameof(ActivationType), IsDamages);
            ChangePropertyIsBrowsable(nameof(DamageType), IsDamages);
            ChangePropertyIsBrowsable(nameof(BlockingType), IsDamages);
            ChangePropertyIsBrowsable(nameof(Unknown_SJ), IsDamages);
            ChangePropertyIsBrowsable(nameof(DamageAmount), IsDamages);

            //IsLadderUp || IsAshley || IsGrappleGun || IsLocalTeleportation
            ChangePropertyIsBrowsable(nameof(ObjPointW_onlyClassic), (IsLocalTeleportation || IsLadderUp || IsAshley || IsGrappleGun) && version == Re4Version.V2007PS2);

            //IsLadderUp
            ChangePropertyIsBrowsable(nameof(LadderStepCount), IsLadderUp);
            ChangePropertyIsBrowsable(nameof(LadderParameter0), IsLadderUp);
            ChangePropertyIsBrowsable(nameof(LadderParameter1), IsLadderUp);
            ChangePropertyIsBrowsable(nameof(LadderParameter2), IsLadderUp);
            ChangePropertyIsBrowsable(nameof(LadderParameter3), IsLadderUp);
            ChangePropertyIsBrowsable(nameof(Unknown_SG), IsLadderUp);
            ChangePropertyIsBrowsable(nameof(Unknown_SH), IsLadderUp);

            //IsAshley
            ChangePropertyIsBrowsable(nameof(Unknown_SM), IsAshley);
            ChangePropertyIsBrowsable(nameof(Unknown_SN), IsAshley);
            ChangePropertyIsBrowsable(nameof(Unknown_SP), IsAshley);
            ChangePropertyIsBrowsable(nameof(Unknown_SQ), IsAshley);
            ChangePropertyIsBrowsable(nameof(Unknown_SR), IsAshley);
            ChangePropertyIsBrowsable(nameof(Unknown_SS), IsAshley);


            //IsGrappleGun
            ChangePropertyIsBrowsable(nameof(GrappleGunEndPointW), IsGrappleGun && version == Re4Version.V2007PS2);
            ChangePropertyIsBrowsable(nameof(GrappleGunThirdPointW), IsGrappleGun && version == Re4Version.V2007PS2);
            ChangePropertyIsBrowsable(nameof(GrappleGunParameter0), IsGrappleGun);
            ChangePropertyIsBrowsable(nameof(GrappleGunParameter1), IsGrappleGun);
            ChangePropertyIsBrowsable(nameof(GrappleGunParameter2), IsGrappleGun);
            ChangePropertyIsBrowsable(nameof(GrappleGunParameter3), IsGrappleGun);
            ChangePropertyIsBrowsable(nameof(Unknown_SK), IsGrappleGun);
            ChangePropertyIsBrowsable(nameof(Unknown_SL), IsGrappleGun);


            //OUTROS
            ChangePropertyIsBrowsable(nameof(Unknown_HH), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsT11));
            ChangePropertyIsBrowsable(nameof(Unknown_HK), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsT04 || IsMessage));
            ChangePropertyIsBrowsable(nameof(Unknown_HL), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsDamages || IsMessage));
            ChangePropertyIsBrowsable(nameof(Unknown_HM), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsDamages || IsMessage));
            ChangePropertyIsBrowsable(nameof(Unknown_HN), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation || IsDamages));
            ChangePropertyIsBrowsable(nameof(Unknown_HR), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation));
            ChangePropertyIsBrowsable(nameof(Unknown_RH), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation));
            ChangePropertyIsBrowsable(nameof(Unknown_RJ), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || IsLocalTeleportation));
            ChangePropertyIsBrowsable(nameof(Unknown_RK), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || (IsLocalTeleportation && version == Re4Version.V2007PS2)));
            ChangePropertyIsBrowsable(nameof(Unknown_RL), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp || IsWarpDoor || (IsLocalTeleportation && version == Re4Version.V2007PS2)));
            ChangePropertyIsBrowsable(nameof(Unknown_RM), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp));
            ChangePropertyIsBrowsable(nameof(Unknown_RN), !(IsItem || IsGrappleGun || IsAshley || IsLadderUp));
            ChangePropertyIsBrowsable(nameof(Unknown_RP), !(IsItem || IsGrappleGun || IsAshley || (IsLadderUp && version == Re4Version.V2007PS2)));
            ChangePropertyIsBrowsable(nameof(Unknown_RQ), !(IsItem || IsGrappleGun || IsAshley || (IsLadderUp && version == Re4Version.V2007PS2)));
            ChangePropertyIsBrowsable(nameof(Unknown_TG), !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable(nameof(Unknown_TH), !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable(nameof(Unknown_TJ), !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable(nameof(Unknown_TK), !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable(nameof(Unknown_TL), !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable(nameof(Unknown_TM), !(IsItem || IsGrappleGun || IsAshley));
            ChangePropertyIsBrowsable(nameof(Unknown_TN), !(IsItem || IsAshley || (IsGrappleGun && version == Re4Version.V2007PS2)));
            ChangePropertyIsBrowsable(nameof(Unknown_TP), !(IsItem || IsAshley || (IsGrappleGun && version == Re4Version.V2007PS2)));
            ChangePropertyIsBrowsable(nameof(Unknown_TQ), !(IsItem || (IsGrappleGun && version == Re4Version.V2007PS2) || (IsAshley && version == Re4Version.V2007PS2)));
        }

        void SetPropertyCategory()
        {
            var SpecialType = Methods.GetSpecialType(InternalID);
            string CategoryText;
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
                case Enums.SpecialType.T0D_FieldInfo: CategoryText = Lang.GetAttributeText(aLang.SpecialType0D_FieldInfo); break;
                case Enums.SpecialType.T0E_Crouch: CategoryText = Lang.GetAttributeText(aLang.SpecialType0E_Crouch); break;
                case Enums.SpecialType.T10_FixedLadderClimbUp: CategoryText = Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp); break;
                case Enums.SpecialType.T11_ItemDependentEvents: CategoryText = Lang.GetAttributeText(aLang.SpecialType11_ItemDependentEvents); break;
                case Enums.SpecialType.T12_AshleyHideCommand: CategoryText = Lang.GetAttributeText(aLang.SpecialType12_AshleyHideCommand); break;
                case Enums.SpecialType.T13_LocalTeleportation: CategoryText = Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation); break;
                case Enums.SpecialType.T14_UsedForElevators: CategoryText = Lang.GetAttributeText(aLang.SpecialType14_UsedForElevators); break;
                case Enums.SpecialType.T15_AdaGrappleGun: CategoryText = Lang.GetAttributeText(aLang.SpecialType15_AdaGrappleGun); break;
                default: CategoryText = Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType); break;
            }

            CategoryText = CategoryText.PadRight(1000);

            ChangePropertyCategory(nameof(Unknown_HH), CategoryText);
            ChangePropertyCategory(nameof(Unknown_HK), CategoryText);
            ChangePropertyCategory(nameof(Unknown_HL), CategoryText);
            ChangePropertyCategory(nameof(Unknown_HM), CategoryText);
            ChangePropertyCategory(nameof(Unknown_HN), CategoryText);
            ChangePropertyCategory(nameof(Unknown_HR), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RH), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RJ), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RK), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RL), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RM), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RN), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RP), CategoryText);
            ChangePropertyCategory(nameof(Unknown_RQ), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TG), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TH), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TJ), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TK), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TL), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TM), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TN), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TP), CategoryText);
            ChangePropertyCategory(nameof(Unknown_TQ), CategoryText);

            ChangePropertyCategory(nameof(Unknown_VS), CategoryText);
            ChangePropertyCategory(nameof(Unknown_VT), CategoryText);
            ChangePropertyCategory(nameof(Unknown_VI), CategoryText);
            ChangePropertyCategory(nameof(Unknown_VO), CategoryText);

            ChangePropertyCategory(nameof(ObjPointX), CategoryText);
            ChangePropertyCategory(nameof(ObjPointY), CategoryText);
            ChangePropertyCategory(nameof(ObjPointZ), CategoryText);
            ChangePropertyCategory(nameof(ObjPointX_Hex), CategoryText);
            ChangePropertyCategory(nameof(ObjPointY_Hex), CategoryText);
            ChangePropertyCategory(nameof(ObjPointZ_Hex), CategoryText);

            ChangePropertyCategory(nameof(ObjPointW_onlyClassic), CategoryText);
        }

        void SetPropertyId() 
        {
            bool IsAshley = Methods.GetSpecialType(InternalID) == SpecialType.T12_AshleyHideCommand;
            if (IsAshley && version == Re4Version.V2007PS2)
            {
                ChangePropertyId(nameof(AshleyHidingPointX), 0x6711);
                ChangePropertyId(nameof(AshleyHidingPointY), 0x6712);
                ChangePropertyId(nameof(AshleyHidingPointZ), 0x6713);
                ChangePropertyId(nameof(AshleyHidingPointX_Hex), 0x6711);
                ChangePropertyId(nameof(AshleyHidingPointY_Hex), 0x6712);
                ChangePropertyId(nameof(AshleyHidingPointZ_Hex), 0x6713);
            }
            else
            {
                ChangePropertyId(nameof(AshleyHidingPointX), 0x8000);
                ChangePropertyId(nameof(AshleyHidingPointY), 0x8400);
                ChangePropertyId(nameof(AshleyHidingPointZ), 0x8800);
                ChangePropertyId(nameof(AshleyHidingPointX_Hex), 0x8000);
                ChangePropertyId(nameof(AshleyHidingPointY_Hex), 0x8400);
                ChangePropertyId(nameof(AshleyHidingPointZ_Hex), 0x8800);
            }
        }

        void SetIsExtra() 
        {
            ChangePropertyIsBrowsable(nameof(InternalLineID), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Line), !IsExtra);
            ChangePropertyIsBrowsable(nameof(SpecialTypeID), !IsExtra);
            ChangePropertyIsBrowsable(nameof(SpecialTypeID_ListBox), !IsExtra);
            ChangePropertyIsBrowsable(nameof(SpecialIndex), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_GG), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_GH), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Category), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Category_ListBox), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_GK), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_KG), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_KJ), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_LI), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_LO), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_LU), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_LH), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_MI), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_MO), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_MU), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_NI), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_NO), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_NS), !IsExtra);
            ChangePropertyIsBrowsable(nameof(RefInteractionType), !IsExtra);
            ChangePropertyIsBrowsable(nameof(RefInteractionType_ListBox), !IsExtra);
            ChangePropertyIsBrowsable(nameof(RefInteractionIndex), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_NT), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_NU), !IsExtra);
            ChangePropertyIsBrowsable(nameof(PromptMessage), !IsExtra);
            ChangePropertyIsBrowsable(nameof(PromptMessage_ListBox), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_PI), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_PO), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_PU), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_PK), !IsExtra);
            ChangePropertyIsBrowsable(nameof(MessageColor), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_QI), !IsExtra);
            ChangePropertyIsBrowsable(nameof(Unknown_QO), !IsExtra);

            ChangePropertyIsBrowsable(nameof(Unknown_QU), version == Re4Version.V2007PS2 && !IsExtra);
        }


        void SetClassicPropertyTexts() 
        {
            //Line
            if (specialFileFormat == SpecialFileFormat.AEV)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "160"));
            }
            else if (specialFileFormat == SpecialFileFormat.ITA)
            {
                ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "176"));
            }

            ChangePropertyDescription(nameof(SpecialTypeID), Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription(nameof(SpecialTypeID_ListBox), Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription(nameof(SpecialIndex), Lang.GetAttributeText(aLang.SpecialIndex_Byte_Description));


            ChangePropertyDescription(nameof(Unknown_GG), Lang.GetAttributeText(aLang.Unknown_GG_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_KG), Lang.GetAttributeText(aLang.Unknown_KG_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_KJ), Lang.GetAttributeText(aLang.Unknown_KJ_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LI), Lang.GetAttributeText(aLang.Unknown_LI_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LO), Lang.GetAttributeText(aLang.Unknown_LO_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LU), Lang.GetAttributeText(aLang.Unknown_LU_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LH), Lang.GetAttributeText(aLang.Unknown_LH_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_MI), Lang.GetAttributeText(aLang.Unknown_MI_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_MO), Lang.GetAttributeText(aLang.Unknown_MO_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_MU), Lang.GetAttributeText(aLang.Unknown_MU_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_NI), Lang.GetAttributeText(aLang.Unknown_NI_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_NO), Lang.GetAttributeText(aLang.Unknown_NO_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_NS), Lang.GetAttributeText(aLang.Unknown_NS_Byte_Description));
            ChangePropertyDescription(nameof(RefInteractionType), Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription(nameof(RefInteractionType_ListBox), Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription(nameof(RefInteractionIndex), Lang.GetAttributeText(aLang.RefInteractionIndex_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_NT), Lang.GetAttributeText(aLang.Unknown_NT_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_NU), Lang.GetAttributeText(aLang.Unknown_NU_Byte_Description));
            ChangePropertyDescription(nameof(PromptMessage), Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription(nameof(PromptMessage_ListBox), Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_PI), Lang.GetAttributeText(aLang.Unknown_PI_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_PO), Lang.GetAttributeText(aLang.Unknown_PO_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_PU), Lang.GetAttributeText(aLang.Unknown_PU_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_PK), Lang.GetAttributeText(aLang.Unknown_PK_Byte_Description));
            ChangePropertyDescription(nameof(MessageColor), Lang.GetAttributeText(aLang.MessageColor_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_QI), Lang.GetAttributeText(aLang.Unknown_QI_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_QO), Lang.GetAttributeText(aLang.Unknown_QO_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_QU), Lang.GetAttributeText(aLang.Unknown_QU_ByteArray4_Description).Replace("<<Offset1>>", "0x5C").Replace("<<Offset2>>", "0x5D").Replace("<<Offset3>>", "0x5E").Replace("<<Offset4>>", "0x5F"));


            ChangePropertyDescription(nameof(Unknown_HH), Lang.GetAttributeText(aLang.Unknown_HH_ByteArray2_Description).Replace("<<Offset1>>", "0x60").Replace("<<Offset2>>", "0x61"));
            ChangePropertyDescription(nameof(Unknown_HK), Lang.GetAttributeText(aLang.Unknown_HK_ByteArray2_Description).Replace("<<Offset1>>", "0x62").Replace("<<Offset2>>", "0x63"));
            ChangePropertyDescription(nameof(Unknown_HL), Lang.GetAttributeText(aLang.Unknown_HL_ByteArray2_Description).Replace("<<Offset1>>", "0x64").Replace("<<Offset2>>", "0x65"));
            ChangePropertyDescription(nameof(Unknown_HM), Lang.GetAttributeText(aLang.Unknown_HM_ByteArray2_Description).Replace("<<Offset1>>", "0x66").Replace("<<Offset2>>", "0x67"));
            ChangePropertyDescription(nameof(Unknown_HN), Lang.GetAttributeText(aLang.Unknown_HN_ByteArray2_Description).Replace("<<Offset1>>", "0x68").Replace("<<Offset2>>", "0x69"));
            ChangePropertyDescription(nameof(Unknown_HR), Lang.GetAttributeText(aLang.Unknown_HR_ByteArray2_Description).Replace("<<Offset1>>", "0x6A").Replace("<<Offset2>>", "0x6B"));
            ChangePropertyDescription(nameof(Unknown_RH), Lang.GetAttributeText(aLang.Unknown_RH_ByteArray2_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D"));
            ChangePropertyDescription(nameof(Unknown_RJ), Lang.GetAttributeText(aLang.Unknown_RJ_ByteArray2_Description).Replace("<<Offset1>>", "0x6E").Replace("<<Offset2>>", "0x6F"));
            ChangePropertyDescription(nameof(Unknown_RK), Lang.GetAttributeText(aLang.Unknown_RK_ByteArray2_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71"));
            ChangePropertyDescription(nameof(Unknown_RL), Lang.GetAttributeText(aLang.Unknown_RL_ByteArray2_Description).Replace("<<Offset1>>", "0x72").Replace("<<Offset2>>", "0x73"));
            ChangePropertyDescription(nameof(Unknown_RM), Lang.GetAttributeText(aLang.Unknown_RM_ByteArray2_Description).Replace("<<Offset1>>", "0x74").Replace("<<Offset2>>", "0x75"));
            ChangePropertyDescription(nameof(Unknown_RN), Lang.GetAttributeText(aLang.Unknown_RN_ByteArray2_Description).Replace("<<Offset1>>", "0x76").Replace("<<Offset2>>", "0x77"));
            ChangePropertyDescription(nameof(Unknown_RP), Lang.GetAttributeText(aLang.Unknown_RP_ByteArray2_Description).Replace("<<Offset1>>", "0x78").Replace("<<Offset2>>", "0x79"));
            ChangePropertyDescription(nameof(Unknown_RQ), Lang.GetAttributeText(aLang.Unknown_RQ_ByteArray2_Description).Replace("<<Offset1>>", "0x7A").Replace("<<Offset2>>", "0x7B"));
            ChangePropertyDescription(nameof(Unknown_TG), Lang.GetAttributeText(aLang.Unknown_TG_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription(nameof(Unknown_TH), Lang.GetAttributeText(aLang.Unknown_TH_ByteArray4_Description).Replace("<<Offset1>>", "0x80").Replace("<<Offset2>>", "0x81").Replace("<<Offset3>>", "0x82").Replace("<<Offset4>>", "0x83"));
            ChangePropertyDescription(nameof(Unknown_TJ), Lang.GetAttributeText(aLang.Unknown_TJ_ByteArray4_Description).Replace("<<Offset1>>", "0x84").Replace("<<Offset2>>", "0x85").Replace("<<Offset3>>", "0x86").Replace("<<Offset4>>", "0x87"));
            ChangePropertyDescription(nameof(Unknown_TK), Lang.GetAttributeText(aLang.Unknown_TK_ByteArray4_Description).Replace("<<Offset1>>", "0x88").Replace("<<Offset2>>", "0x89").Replace("<<Offset3>>", "0x8A").Replace("<<Offset4>>", "0x8B"));
            ChangePropertyDescription(nameof(Unknown_TL), Lang.GetAttributeText(aLang.Unknown_TL_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription(nameof(Unknown_TM), Lang.GetAttributeText(aLang.Unknown_TM_ByteArray4_Description).Replace("<<Offset1>>", "0x90").Replace("<<Offset2>>", "0x91").Replace("<<Offset3>>", "0x92").Replace("<<Offset4>>", "0x93"));
            ChangePropertyDescription(nameof(Unknown_TN), Lang.GetAttributeText(aLang.Unknown_TN_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));
            ChangePropertyDescription(nameof(Unknown_TP), Lang.GetAttributeText(aLang.Unknown_TP_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));
            ChangePropertyDescription(nameof(Unknown_TQ), Lang.GetAttributeText(aLang.Unknown_TQ_ByteArray4_Description).Replace("<<Offset1>>", "0x9C").Replace("<<Offset2>>", "0x9D").Replace("<<Offset3>>", "0x9E").Replace("<<Offset4>>", "0x9F"));
            ChangePropertyDescription(nameof(Unknown_VS), Lang.GetAttributeText(aLang.Unknown_VS_ByteArray4_Description).Replace("<<Offset1>>", "0xA0").Replace("<<Offset2>>", "0xA1").Replace("<<Offset3>>", "0xA2").Replace("<<Offset4>>", "0xA3"));
            ChangePropertyDescription(nameof(Unknown_VT), Lang.GetAttributeText(aLang.Unknown_VT_ByteArray4_Description).Replace("<<Offset1>>", "0xA4").Replace("<<Offset2>>", "0xA5").Replace("<<Offset3>>", "0xA6").Replace("<<Offset4>>", "0xA7"));
            ChangePropertyDescription(nameof(Unknown_VI), Lang.GetAttributeText(aLang.Unknown_VI_ByteArray4_Description).Replace("<<Offset1>>", "0xA8").Replace("<<Offset2>>", "0xA9").Replace("<<Offset3>>", "0xAA").Replace("<<Offset4>>", "0xAB"));
            ChangePropertyDescription(nameof(Unknown_VO), Lang.GetAttributeText(aLang.Unknown_VO_ByteArray4_Description).Replace("<<Offset1>>", "0xAC").Replace("<<Offset2>>", "0xAD").Replace("<<Offset3>>", "0xAE").Replace("<<Offset4>>", "0xAF"));

            ChangePropertyDescription(nameof(ObjPointX), Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(ObjPointX_Hex), Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(ObjPointY), Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(ObjPointY_Hex), Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(ObjPointZ), Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(ObjPointZ_Hex), Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(ObjPointW), Lang.GetAttributeText(aLang.ObjPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D").Replace("<<Offset3>>", "0x6E").Replace("<<Offset4>>", "0x6F"));
            ChangePropertyDescription(nameof(ObjPointW_onlyClassic), Lang.GetAttributeText(aLang.ObjPointW_onlyClassic_ByteArray4_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D").Replace("<<Offset3>>", "0x6E").Replace("<<Offset4>>", "0x6F"));


            ChangePropertyDescription(nameof(NeededItemNumber), Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x61").Replace("<<Offset2>>", "0x60"));
            ChangePropertyDescription(nameof(NeededItemNumber_ListBox), Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x61").Replace("<<Offset2>>", "0x60"));
            ChangePropertyDescription(nameof(EnemyGroup), Lang.GetAttributeText(aLang.EnemyGroup_Ushort_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62"));
            ChangePropertyDescription(nameof(RoomMessage), Lang.GetAttributeText(aLang.RoomMessage_Ushort_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62"));
            ChangePropertyDescription(nameof(MessageCutSceneID), Lang.GetAttributeText(aLang.MessageCutSceneID_Ushort_Description).Replace("<<Offset1>>", "0x65").Replace("<<Offset2>>", "0x64"));
            ChangePropertyDescription(nameof(MessageID), Lang.GetAttributeText(aLang.MessageID_Ushort_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66"));
            ChangePropertyDescription(nameof(ActivationType), Lang.GetAttributeText(aLang.ActivationType_Byte_Description).Replace("<<Offset1>>", "0x64"));
            ChangePropertyDescription(nameof(DamageType), Lang.GetAttributeText(aLang.DamageType_Byte_Description).Replace("<<Offset1>>", "0x65"));
            ChangePropertyDescription(nameof(BlockingType), Lang.GetAttributeText(aLang.BlockingType_Byte_Description).Replace("<<Offset1>>", "0x66"));
            ChangePropertyDescription(nameof(Unknown_SJ), Lang.GetAttributeText(aLang.Unknown_SJ_Byte_Description).Replace("<<Offset1>>", "0x67"));
            ChangePropertyDescription(nameof(DamageAmount), Lang.GetAttributeText(aLang.DamageAmount_Ushort_Description).Replace("<<Offset1>>", "0x69").Replace("<<Offset2>>", "0x68"));
            ChangePropertyDescription(nameof(DestinationFacingAngle), Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(DestinationFacingAngle_Hex), Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(DestinationRoom), Lang.GetAttributeText(aLang.DestinationRoom_UshortUnflip_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71"));
            ChangePropertyDescription(nameof(LockedDoorType), Lang.GetAttributeText(aLang.LockedDoorType_Byte_Description).Replace("<<Offset1>>", "0x72"));
            ChangePropertyDescription(nameof(LockedDoorIndex), Lang.GetAttributeText(aLang.LockedDoorIndex_Byte_Description).Replace("<<Offset1>>", "0x73"));
            ChangePropertyDescription(nameof(LocalTeleportationFacingAngle), Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(LocalTeleportationFacingAngle_Hex), Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(LadderFacingAngle), Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(LadderFacingAngle_Hex), Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(LadderStepCount), Lang.GetAttributeText(aLang.LadderStepCount_Sbyte_Description).Replace("<<Offset1>>", "0x74"));
            ChangePropertyDescription(nameof(LadderParameter0), Lang.GetAttributeText(aLang.LadderParameter0_Byte_Description).Replace("<<Offset1>>", "0x75"));
            ChangePropertyDescription(nameof(LadderParameter1), Lang.GetAttributeText(aLang.LadderParameter1_Byte_Description).Replace("<<Offset1>>", "0x76"));
            ChangePropertyDescription(nameof(LadderParameter2), Lang.GetAttributeText(aLang.LadderParameter2_Byte_Description).Replace("<<Offset1>>", "0x77"));
            ChangePropertyDescription(nameof(LadderParameter3), Lang.GetAttributeText(aLang.LadderParameter3_Byte_Description).Replace("<<Offset1>>", "0x78"));
            ChangePropertyDescription(nameof(Unknown_SG), Lang.GetAttributeText(aLang.Unknown_SG_Byte_Description).Replace("<<Offset1>>", "0x79"));
            ChangePropertyDescription(nameof(Unknown_SH), Lang.GetAttributeText(aLang.Unknown_SH_ByteArray2_Description).Replace("<<Offset1>>", "0x7A").Replace("<<Offset2>>", "0x7B"));


            ChangePropertyDescription(nameof(Unknown_RI_X), Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(Unknown_RI_X_Hex), Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(Unknown_RI_Y), Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(Unknown_RI_Y_Hex), Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(Unknown_RI_Z), Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(Unknown_RI_Z_Hex), Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(Unknown_RI_W), Lang.GetAttributeText(aLang.Unknown_RI_W_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription(nameof(Unknown_RO), Lang.GetAttributeText(aLang.Unknown_RO_ByteArray4_Description).Replace("<<Offset1>>", "0x80").Replace("<<Offset2>>", "0x81").Replace("<<Offset3>>", "0x82").Replace("<<Offset4>>", "0x83"));
            ChangePropertyDescription(nameof(ItemNumber), Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x85").Replace("<<Offset2>>", "0x84"));
            ChangePropertyDescription(nameof(ItemNumber_ListBox), Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x85").Replace("<<Offset2>>", "0x84"));
            ChangePropertyDescription(nameof(Unknown_RU), Lang.GetAttributeText(aLang.Unknown_RU_ByteArray2_Description).Replace("<<Offset1>>", "0x86").Replace("<<Offset2>>", "0x87"));
            ChangePropertyDescription(nameof(ItemAmount), Lang.GetAttributeText(aLang.ItemAmount_Ushort_Description).Replace("<<Offset1>>", "0x89").Replace("<<Offset2>>", "0x88"));
            ChangePropertyDescription(nameof(SecundIndex), Lang.GetAttributeText(aLang.SecundIndex_Ushort_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A"));
            ChangePropertyDescription(nameof(ItemAuraType), Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x8D").Replace("<<Offset2>>", "0x8C"));
            ChangePropertyDescription(nameof(ItemAuraType_ListBox), Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x8D").Replace("<<Offset2>>", "0x8C"));
            ChangePropertyDescription(nameof(Unknown_QM), Lang.GetAttributeText(aLang.Unknown_QM_Byte_Description).Replace("<<Offset1>>", "0x8E"));
            ChangePropertyDescription(nameof(Unknown_QL), Lang.GetAttributeText(aLang.Unknown_QL_Byte_Description).Replace("<<Offset1>>", "0x8F"));
            ChangePropertyDescription(nameof(Unknown_QR), Lang.GetAttributeText(aLang.Unknown_QR_Byte_Description).Replace("<<Offset1>>", "0x90"));
            ChangePropertyDescription(nameof(Unknown_QH), Lang.GetAttributeText(aLang.Unknown_QH_Byte_Description).Replace("<<Offset1>>", "0x91"));
            ChangePropertyDescription(nameof(Unknown_QG), Lang.GetAttributeText(aLang.Unknown_QG_ByteArray2_Description).Replace("<<Offset1>>", "0x92").Replace("<<Offset2>>", "0x93"));
            ChangePropertyDescription(nameof(ItemTriggerRadius), Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription(nameof(ItemTriggerRadius_Hex), Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription(nameof(ItemAngleX), Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x9B").Replace("<<Offset2>>", "0x9A").Replace("<<Offset3>>", "0x99").Replace("<<Offset4>>", "0x98"));
            ChangePropertyDescription(nameof(ItemAngleX_Hex), Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x9B").Replace("<<Offset2>>", "0x9A").Replace("<<Offset3>>", "0x99").Replace("<<Offset4>>", "0x98"));
            ChangePropertyDescription(nameof(ItemAngleY), Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x9F").Replace("<<Offset2>>", "0x9E").Replace("<<Offset3>>", "0x9D").Replace("<<Offset4>>", "0x9C"));
            ChangePropertyDescription(nameof(ItemAngleY_Hex), Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x9F").Replace("<<Offset2>>", "0x9E").Replace("<<Offset3>>", "0x9D").Replace("<<Offset4>>", "0x9C"));
            ChangePropertyDescription(nameof(ItemAngleZ), Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0xA3").Replace("<<Offset2>>", "0xA2").Replace("<<Offset3>>", "0xA1").Replace("<<Offset4>>", "0xA0"));
            ChangePropertyDescription(nameof(ItemAngleZ_Hex), Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0xA3").Replace("<<Offset2>>", "0xA2").Replace("<<Offset3>>", "0xA1").Replace("<<Offset4>>", "0xA0"));
            ChangePropertyDescription(nameof(ItemAngleW), Lang.GetAttributeText(aLang.ItemAngleW_ByteArray4_Description).Replace("<<Offset1>>", "0xA4").Replace("<<Offset2>>", "0xA5").Replace("<<Offset3>>", "0xA6").Replace("<<Offset4>>", "0xA7"));


            ChangePropertyDescription(nameof(AshleyHidingPointX), Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(AshleyHidingPointX_Hex), Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(AshleyHidingPointY), Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(AshleyHidingPointY_Hex), Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(AshleyHidingPointZ), Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(AshleyHidingPointZ_Hex), Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription(nameof(Unknown_SM), Lang.GetAttributeText(aLang.Unknown_SM_ByteArray4_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71").Replace("<<Offset3>>", "0x72").Replace("<<Offset4>>", "0x73"));
            ChangePropertyDescription(nameof(Unknown_SN), Lang.GetAttributeText(aLang.Unknown_SN_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));
            ChangePropertyDescription(nameof(Unknown_SP), Lang.GetAttributeText(aLang.Unknown_SP_Byte_Description).Replace("<<Offset1>>", "0x98"));
            ChangePropertyDescription(nameof(Unknown_SQ), Lang.GetAttributeText(aLang.Unknown_SQ_Byte_Description).Replace("<<Offset1>>", "0x99"));
            ChangePropertyDescription(nameof(Unknown_SR), Lang.GetAttributeText(aLang.Unknown_SR_ByteArray2_Description).Replace("<<Offset1>>", "0x9A").Replace("<<Offset2>>", "0x9B"));
            ChangePropertyDescription(nameof(Unknown_SS), Lang.GetAttributeText(aLang.Unknown_SS_ByteArray4_Description).Replace("<<Offset1>>", "0x9C").Replace("<<Offset2>>", "0x9D").Replace("<<Offset3>>", "0x9E").Replace("<<Offset4>>", "0x9F"));


            ChangePropertyDescription(nameof(GrappleGunEndPointX), Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(GrappleGunEndPointX_Hex), Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(GrappleGunEndPointY), Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(GrappleGunEndPointY_Hex), Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(GrappleGunEndPointZ), Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(GrappleGunEndPointZ_Hex), Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(GrappleGunEndPointW), Lang.GetAttributeText(aLang.GrappleGunEndPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointX), Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointX_Hex), Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointY), Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointY_Hex), Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointZ), Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointZ_Hex), Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointW), Lang.GetAttributeText(aLang.GrappleGunThirdPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription(nameof(GrappleGunFacingAngle), Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription(nameof(GrappleGunFacingAngle_Hex), Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription(nameof(GrappleGunParameter0), Lang.GetAttributeText(aLang.GrappleGunParameter0_Byte_Description).Replace("<<Offset1>>", "0x94"));
            ChangePropertyDescription(nameof(GrappleGunParameter1), Lang.GetAttributeText(aLang.GrappleGunParameter1_Byte_Description).Replace("<<Offset1>>", "0x95"));
            ChangePropertyDescription(nameof(GrappleGunParameter2), Lang.GetAttributeText(aLang.GrappleGunParameter2_Byte_Description).Replace("<<Offset1>>", "0x96"));
            ChangePropertyDescription(nameof(GrappleGunParameter3), Lang.GetAttributeText(aLang.GrappleGunParameter3_Byte_Description).Replace("<<Offset1>>", "0x97"));
            ChangePropertyDescription(nameof(Unknown_SK), Lang.GetAttributeText(aLang.Unknown_SK_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));
            ChangePropertyDescription(nameof(Unknown_SL), Lang.GetAttributeText(aLang.Unknown_SL_ByteArray4_Description).Replace("<<Offset1>>", "0x9C").Replace("<<Offset2>>", "0x9D").Replace("<<Offset3>>", "0x9E").Replace("<<Offset4>>", "0x9F"));

        }

        void SetUHDPropertyTexts()
        {
            ChangePropertyName(nameof(Line), Lang.GetAttributeText(aLang.NewAge_LineArrayDisplayName).Replace("<<Lenght>>", "156"));

            ChangePropertyDescription(nameof(SpecialTypeID), Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription(nameof(SpecialTypeID_ListBox), Lang.GetAttributeText(aLang.SpecialTypeID_Byte_Description));
            ChangePropertyDescription(nameof(SpecialIndex), Lang.GetAttributeText(aLang.SpecialIndex_Byte_Description));


            ChangePropertyDescription(nameof(Unknown_GG), Lang.GetAttributeText(aLang.Unknown_GG_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_KG), Lang.GetAttributeText(aLang.Unknown_KG_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_KJ), Lang.GetAttributeText(aLang.Unknown_KJ_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LI), Lang.GetAttributeText(aLang.Unknown_LI_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LO), Lang.GetAttributeText(aLang.Unknown_LO_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LU), Lang.GetAttributeText(aLang.Unknown_LU_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_LH), Lang.GetAttributeText(aLang.Unknown_LH_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_MI), Lang.GetAttributeText(aLang.Unknown_MI_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_MO), Lang.GetAttributeText(aLang.Unknown_MO_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_MU), Lang.GetAttributeText(aLang.Unknown_MU_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_NI), Lang.GetAttributeText(aLang.Unknown_NI_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_NO), Lang.GetAttributeText(aLang.Unknown_NO_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_NS), Lang.GetAttributeText(aLang.Unknown_NS_Byte_Description));
            ChangePropertyDescription(nameof(RefInteractionType), Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription(nameof(RefInteractionType_ListBox), Lang.GetAttributeText(aLang.RefInteractionType_Byte_Description));
            ChangePropertyDescription(nameof(RefInteractionIndex), Lang.GetAttributeText(aLang.RefInteractionIndex_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_NT), Lang.GetAttributeText(aLang.Unknown_NT_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_NU), Lang.GetAttributeText(aLang.Unknown_NU_Byte_Description));
            ChangePropertyDescription(nameof(PromptMessage), Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription(nameof(PromptMessage_ListBox), Lang.GetAttributeText(aLang.PromptMessage_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_PI), Lang.GetAttributeText(aLang.Unknown_PI_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_PO), Lang.GetAttributeText(aLang.Unknown_PO_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_PU), Lang.GetAttributeText(aLang.Unknown_PU_ByteArray2_Description));
            ChangePropertyDescription(nameof(Unknown_PK), Lang.GetAttributeText(aLang.Unknown_PK_Byte_Description));
            ChangePropertyDescription(nameof(MessageColor), Lang.GetAttributeText(aLang.MessageColor_Byte_Description));
            ChangePropertyDescription(nameof(Unknown_QI), Lang.GetAttributeText(aLang.Unknown_QI_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_QO), Lang.GetAttributeText(aLang.Unknown_QO_ByteArray4_Description));
            ChangePropertyDescription(nameof(Unknown_QU), Lang.GetAttributeText(aLang.Unknown_QU_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));


            ChangePropertyDescription(nameof(Unknown_HH), Lang.GetAttributeText(aLang.Unknown_HH_ByteArray2_Description).Replace("<<Offset1>>", "0x5C").Replace("<<Offset2>>", "0x5D"));
            ChangePropertyDescription(nameof(Unknown_HK), Lang.GetAttributeText(aLang.Unknown_HK_ByteArray2_Description).Replace("<<Offset1>>", "0x5E").Replace("<<Offset2>>", "0x5F"));
            ChangePropertyDescription(nameof(Unknown_HL), Lang.GetAttributeText(aLang.Unknown_HL_ByteArray2_Description).Replace("<<Offset1>>", "0x60").Replace("<<Offset2>>", "0x61"));
            ChangePropertyDescription(nameof(Unknown_HM), Lang.GetAttributeText(aLang.Unknown_HM_ByteArray2_Description).Replace("<<Offset1>>", "0x62").Replace("<<Offset2>>", "0x63"));
            ChangePropertyDescription(nameof(Unknown_HN), Lang.GetAttributeText(aLang.Unknown_HN_ByteArray2_Description).Replace("<<Offset1>>", "0x64").Replace("<<Offset2>>", "0x65"));
            ChangePropertyDescription(nameof(Unknown_HR), Lang.GetAttributeText(aLang.Unknown_HR_ByteArray2_Description).Replace("<<Offset1>>", "0x66").Replace("<<Offset2>>", "0x67"));
            ChangePropertyDescription(nameof(Unknown_RH), Lang.GetAttributeText(aLang.Unknown_RH_ByteArray2_Description).Replace("<<Offset1>>", "0x68").Replace("<<Offset2>>", "0x69"));
            ChangePropertyDescription(nameof(Unknown_RJ), Lang.GetAttributeText(aLang.Unknown_RJ_ByteArray2_Description).Replace("<<Offset1>>", "0x6A").Replace("<<Offset2>>", "0x6B"));
            ChangePropertyDescription(nameof(Unknown_RK), Lang.GetAttributeText(aLang.Unknown_RK_ByteArray2_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D"));
            ChangePropertyDescription(nameof(Unknown_RL), Lang.GetAttributeText(aLang.Unknown_RL_ByteArray2_Description).Replace("<<Offset1>>", "0x6E").Replace("<<Offset2>>", "0x6F"));
            ChangePropertyDescription(nameof(Unknown_RM), Lang.GetAttributeText(aLang.Unknown_RM_ByteArray2_Description).Replace("<<Offset1>>", "0x70").Replace("<<Offset2>>", "0x71"));
            ChangePropertyDescription(nameof(Unknown_RN), Lang.GetAttributeText(aLang.Unknown_RN_ByteArray2_Description).Replace("<<Offset1>>", "0x72").Replace("<<Offset2>>", "0x73"));
            ChangePropertyDescription(nameof(Unknown_RP), Lang.GetAttributeText(aLang.Unknown_RP_ByteArray2_Description).Replace("<<Offset1>>", "0x74").Replace("<<Offset2>>", "0x75"));
            ChangePropertyDescription(nameof(Unknown_RQ), Lang.GetAttributeText(aLang.Unknown_RQ_ByteArray2_Description).Replace("<<Offset1>>", "0x76").Replace("<<Offset2>>", "0x77"));
            ChangePropertyDescription(nameof(Unknown_TG), Lang.GetAttributeText(aLang.Unknown_TG_ByteArray4_Description).Replace("<<Offset1>>", "0x78").Replace("<<Offset2>>", "0x79").Replace("<<Offset3>>", "0x7A").Replace("<<Offset4>>", "0x7B"));
            ChangePropertyDescription(nameof(Unknown_TH), Lang.GetAttributeText(aLang.Unknown_TH_ByteArray4_Description).Replace("<<Offset1>>", "0x7C").Replace("<<Offset2>>", "0x7D").Replace("<<Offset3>>", "0x7E").Replace("<<Offset4>>", "0x7F"));
            ChangePropertyDescription(nameof(Unknown_TJ), Lang.GetAttributeText(aLang.Unknown_TJ_ByteArray4_Description).Replace("<<Offset1>>", "0x80").Replace("<<Offset2>>", "0x81").Replace("<<Offset3>>", "0x82").Replace("<<Offset4>>", "0x83"));
            ChangePropertyDescription(nameof(Unknown_TK), Lang.GetAttributeText(aLang.Unknown_TK_ByteArray4_Description).Replace("<<Offset1>>", "0x84").Replace("<<Offset2>>", "0x85").Replace("<<Offset3>>", "0x86").Replace("<<Offset4>>", "0x87"));
            ChangePropertyDescription(nameof(Unknown_TL), Lang.GetAttributeText(aLang.Unknown_TL_ByteArray4_Description).Replace("<<Offset1>>", "0x88").Replace("<<Offset2>>", "0x89").Replace("<<Offset3>>", "0x8A").Replace("<<Offset4>>", "0x8B"));
            ChangePropertyDescription(nameof(Unknown_TM), Lang.GetAttributeText(aLang.Unknown_TM_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription(nameof(Unknown_TN), Lang.GetAttributeText(aLang.Unknown_TN_ByteArray4_Description).Replace("<<Offset1>>", "0x90").Replace("<<Offset2>>", "0x91").Replace("<<Offset3>>", "0x92").Replace("<<Offset4>>", "0x93"));
            ChangePropertyDescription(nameof(Unknown_TP), Lang.GetAttributeText(aLang.Unknown_TP_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));
            ChangePropertyDescription(nameof(Unknown_TQ), Lang.GetAttributeText(aLang.Unknown_TQ_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));
            ChangePropertyDescription(nameof(Unknown_VS), Lang.GetAttributeText(aLang.Unknown_VS_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(Unknown_VT), Lang.GetAttributeText(aLang.Unknown_VT_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(Unknown_VI), Lang.GetAttributeText(aLang.Unknown_VI_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(Unknown_VO), Lang.GetAttributeText(aLang.Unknown_VO_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));

            ChangePropertyDescription(nameof(ObjPointX), Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E").Replace("<<Offset3>>", "0x5D").Replace("<<Offset4>>", "0x5C"));
            ChangePropertyDescription(nameof(ObjPointX_Hex), Lang.GetAttributeText(aLang.ObjPointX_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E").Replace("<<Offset3>>", "0x5D").Replace("<<Offset4>>", "0x5C"));
            ChangePropertyDescription(nameof(ObjPointY), Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(ObjPointY_Hex), Lang.GetAttributeText(aLang.ObjPointY_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(ObjPointZ), Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(ObjPointZ_Hex), Lang.GetAttributeText(aLang.ObjPointZ_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(ObjPointW), Lang.GetAttributeText(aLang.ObjPointW_ByteArray4_Description).Replace("<<Offset1>>", "0x68").Replace("<<Offset2>>", "0x69").Replace("<<Offset3>>", "0x6A").Replace("<<Offset4>>", "0x6B"));
            ChangePropertyDescription(nameof(ObjPointW_onlyClassic), Lang.GetAttributeText(aLang.ObjPointW_onlyClassic_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));


            ChangePropertyDescription(nameof(NeededItemNumber), Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x5D").Replace("<<Offset2>>", "0x5C"));
            ChangePropertyDescription(nameof(NeededItemNumber_ListBox), Lang.GetAttributeText(aLang.NeededItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x5D").Replace("<<Offset2>>", "0x5C"));
            ChangePropertyDescription(nameof(EnemyGroup), Lang.GetAttributeText(aLang.EnemyGroup_Ushort_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E"));
            ChangePropertyDescription(nameof(RoomMessage), Lang.GetAttributeText(aLang.RoomMessage_Ushort_Description).Replace("<<Offset1>>", "0x5F").Replace("<<Offset2>>", "0x5E"));
            ChangePropertyDescription(nameof(MessageCutSceneID), Lang.GetAttributeText(aLang.MessageCutSceneID_Ushort_Description).Replace("<<Offset1>>", "0x61").Replace("<<Offset2>>", "0x60"));
            ChangePropertyDescription(nameof(MessageID), Lang.GetAttributeText(aLang.MessageID_Ushort_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62"));
            ChangePropertyDescription(nameof(ActivationType), Lang.GetAttributeText(aLang.ActivationType_Byte_Description).Replace("<<Offset1>>", "0x60"));
            ChangePropertyDescription(nameof(DamageType), Lang.GetAttributeText(aLang.DamageType_Byte_Description).Replace("<<Offset1>>", "0x61"));
            ChangePropertyDescription(nameof(BlockingType), Lang.GetAttributeText(aLang.BlockingType_Byte_Description).Replace("<<Offset1>>", "0x62"));
            ChangePropertyDescription(nameof(Unknown_SJ), Lang.GetAttributeText(aLang.Unknown_SJ_Byte_Description).Replace("<<Offset1>>", "0x63"));
            ChangePropertyDescription(nameof(DamageAmount), Lang.GetAttributeText(aLang.DamageAmount_Ushort_Description).Replace("<<Offset1>>", "0x65").Replace("<<Offset2>>", "0x64"));
            ChangePropertyDescription(nameof(DestinationFacingAngle), Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(DestinationFacingAngle_Hex), Lang.GetAttributeText(aLang.DestinationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(DestinationRoom), Lang.GetAttributeText(aLang.DestinationRoom_UshortUnflip_Description).Replace("<<Offset1>>", "0x6C").Replace("<<Offset2>>", "0x6D"));
            ChangePropertyDescription(nameof(LockedDoorType), Lang.GetAttributeText(aLang.LockedDoorType_Byte_Description).Replace("<<Offset1>>", "0x6E"));
            ChangePropertyDescription(nameof(LockedDoorIndex), Lang.GetAttributeText(aLang.LockedDoorIndex_Byte_Description).Replace("<<Offset1>>", "0x6F"));
            ChangePropertyDescription(nameof(LocalTeleportationFacingAngle), Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(LocalTeleportationFacingAngle_Hex), Lang.GetAttributeText(aLang.TeleportationFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(LadderFacingAngle), Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(LadderFacingAngle_Hex), Lang.GetAttributeText(aLang.LadderFacingAngle_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(LadderStepCount), Lang.GetAttributeText(aLang.LadderStepCount_Sbyte_Description).Replace("<<Offset1>>", "0x6C"));
            ChangePropertyDescription(nameof(LadderParameter0), Lang.GetAttributeText(aLang.LadderParameter0_Byte_Description).Replace("<<Offset1>>", "0x6D"));
            ChangePropertyDescription(nameof(LadderParameter1), Lang.GetAttributeText(aLang.LadderParameter1_Byte_Description).Replace("<<Offset1>>", "0x6E"));
            ChangePropertyDescription(nameof(LadderParameter2), Lang.GetAttributeText(aLang.LadderParameter2_Byte_Description).Replace("<<Offset1>>", "0x6F"));
            ChangePropertyDescription(nameof(LadderParameter3), Lang.GetAttributeText(aLang.LadderParameter3_Byte_Description).Replace("<<Offset1>>", "0x70"));
            ChangePropertyDescription(nameof(Unknown_SG), Lang.GetAttributeText(aLang.Unknown_SG_Byte_Description).Replace("<<Offset1>>", "0x71"));
            ChangePropertyDescription(nameof(Unknown_SH), Lang.GetAttributeText(aLang.Unknown_SH_ByteArray2_Description).Replace("<<Offset1>>", "0x72").Replace("<<Offset2>>", "0x73"));


            ChangePropertyDescription(nameof(Unknown_RI_X), Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(Unknown_RI_X_Hex), Lang.GetAttributeText(aLang.Unknown_RI_X_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(Unknown_RI_Y), Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(Unknown_RI_Y_Hex), Lang.GetAttributeText(aLang.Unknown_RI_Y_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(Unknown_RI_Z), Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(Unknown_RI_Z_Hex), Lang.GetAttributeText(aLang.Unknown_RI_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(Unknown_RI_W), Lang.GetAttributeText(aLang.Unknown_RI_W_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(Unknown_RO), Lang.GetAttributeText(aLang.Unknown_RO_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(ItemNumber), Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x79").Replace("<<Offset2>>", "0x78"));
            ChangePropertyDescription(nameof(ItemNumber_ListBox), Lang.GetAttributeText(aLang.ItemNumber_Ushort_Description).Replace("<<Offset1>>", "0x79").Replace("<<Offset2>>", "0x78"));
            ChangePropertyDescription(nameof(Unknown_RU), Lang.GetAttributeText(aLang.Unknown_RU_ByteArray2_Description).Replace("<<Offset1>>", "0x7A").Replace("<<Offset2>>", "0x7B"));
            ChangePropertyDescription(nameof(ItemAmount), Lang.GetAttributeText(aLang.ItemAmount_Ushort_Description).Replace("<<Offset1>>", "0x7D").Replace("<<Offset2>>", "0x7C"));
            ChangePropertyDescription(nameof(SecundIndex), Lang.GetAttributeText(aLang.SecundIndex_Ushort_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E"));
            ChangePropertyDescription(nameof(ItemAuraType), Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x81").Replace("<<Offset2>>", "0x80"));
            ChangePropertyDescription(nameof(ItemAuraType_ListBox), Lang.GetAttributeText(aLang.ItemAuraType_Ushort_Description).Replace("<<Offset1>>", "0x81").Replace("<<Offset2>>", "0x80"));
            ChangePropertyDescription(nameof(Unknown_QM), Lang.GetAttributeText(aLang.Unknown_QM_Byte_Description).Replace("<<Offset1>>", "0x82"));
            ChangePropertyDescription(nameof(Unknown_QL), Lang.GetAttributeText(aLang.Unknown_QL_Byte_Description).Replace("<<Offset1>>", "0x83"));
            ChangePropertyDescription(nameof(Unknown_QR), Lang.GetAttributeText(aLang.Unknown_QR_Byte_Description).Replace("<<Offset1>>", "0x84"));
            ChangePropertyDescription(nameof(Unknown_QH), Lang.GetAttributeText(aLang.Unknown_QH_Byte_Description).Replace("<<Offset1>>", "0x85"));
            ChangePropertyDescription(nameof(Unknown_QG), Lang.GetAttributeText(aLang.Unknown_QG_ByteArray2_Description).Replace("<<Offset1>>", "0x86").Replace("<<Offset2>>", "0x87"));
            ChangePropertyDescription(nameof(ItemTriggerRadius), Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x9B").Replace("<<Offset2>>", "0x9A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(ItemTriggerRadius_Hex), Lang.GetAttributeText(aLang.ItemTriggerRadius_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(ItemAngleX), Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription(nameof(ItemAngleX_Hex), Lang.GetAttributeText(aLang.ItemAngleX_Description).Replace("<<Offset1>>", "0x8F").Replace("<<Offset2>>", "0x8E").Replace("<<Offset3>>", "0x8D").Replace("<<Offset4>>", "0x8C"));
            ChangePropertyDescription(nameof(ItemAngleY), Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription(nameof(ItemAngleY_Hex), Lang.GetAttributeText(aLang.ItemAngleY_Description).Replace("<<Offset1>>", "0x93").Replace("<<Offset2>>", "0x92").Replace("<<Offset3>>", "0x91").Replace("<<Offset4>>", "0x90"));
            ChangePropertyDescription(nameof(ItemAngleZ), Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription(nameof(ItemAngleZ_Hex), Lang.GetAttributeText(aLang.ItemAngleZ_Description).Replace("<<Offset1>>", "0x97").Replace("<<Offset2>>", "0x96").Replace("<<Offset3>>", "0x95").Replace("<<Offset4>>", "0x94"));
            ChangePropertyDescription(nameof(ItemAngleW), Lang.GetAttributeText(aLang.ItemAngleW_ByteArray4_Description).Replace("<<Offset1>>", "0x98").Replace("<<Offset2>>", "0x99").Replace("<<Offset3>>", "0x9A").Replace("<<Offset4>>", "0x9B"));


            ChangePropertyDescription(nameof(AshleyHidingPointX), Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(AshleyHidingPointX_Hex), Lang.GetAttributeText(aLang.AshleyHidingPointX_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(AshleyHidingPointY), Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription(nameof(AshleyHidingPointY_Hex), Lang.GetAttributeText(aLang.AshleyHidingPointY_Description).Replace("<<Offset1>>", "0x87").Replace("<<Offset2>>", "0x86").Replace("<<Offset3>>", "0x85").Replace("<<Offset4>>", "0x84"));
            ChangePropertyDescription(nameof(AshleyHidingPointZ), Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(AshleyHidingPointZ_Hex), Lang.GetAttributeText(aLang.AshleyHidingPointZ_Description).Replace("<<Offset1>>", "0x8B").Replace("<<Offset2>>", "0x8A").Replace("<<Offset3>>", "0x89").Replace("<<Offset4>>", "0x88"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_X_Description).Replace("<<Offset1>>", "0x63").Replace("<<Offset2>>", "0x62").Replace("<<Offset3>>", "0x61").Replace("<<Offset4>>", "0x60"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner0_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner0_Z_Description).Replace("<<Offset1>>", "0x67").Replace("<<Offset2>>", "0x66").Replace("<<Offset3>>", "0x65").Replace("<<Offset4>>", "0x64"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_X_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner1_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner1_Z_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_X_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner2_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner2_Z_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_X), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_X_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_X_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_Z), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription(nameof(AshleyHidingZoneCorner3_Z_Hex), Lang.GetAttributeText(aLang.AshleyHidingZoneCorner3_Z_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription(nameof(Unknown_SM), Lang.GetAttributeText(aLang.Unknown_SM_ByteArray4_Description).Replace("<<Offset1>>", "0x5C").Replace("<<Offset2>>", "0x5D").Replace("<<Offset3>>", "0x5E").Replace("<<Offset4>>", "0x5F"));
            ChangePropertyDescription(nameof(Unknown_SN), Lang.GetAttributeText(aLang.Unknown_SN_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));
            ChangePropertyDescription(nameof(Unknown_SP), Lang.GetAttributeText(aLang.Unknown_SP_Byte_Description).Replace("<<Offset1>>", "0x90"));
            ChangePropertyDescription(nameof(Unknown_SQ), Lang.GetAttributeText(aLang.Unknown_SQ_Byte_Description).Replace("<<Offset1>>", "0x91"));
            ChangePropertyDescription(nameof(Unknown_SR), Lang.GetAttributeText(aLang.Unknown_SR_ByteArray2_Description).Replace("<<Offset1>>", "0x92").Replace("<<Offset2>>", "0x93"));
            ChangePropertyDescription(nameof(Unknown_SS), Lang.GetAttributeText(aLang.Unknown_SS_ByteArray4_Description).Replace("<<Offset1>>", "0x94").Replace("<<Offset2>>", "0x95").Replace("<<Offset3>>", "0x96").Replace("<<Offset4>>", "0x97"));


            ChangePropertyDescription(nameof(GrappleGunEndPointX), Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(GrappleGunEndPointX_Hex), Lang.GetAttributeText(aLang.GrappleGunEndPointX_Description).Replace("<<Offset1>>", "0x6B").Replace("<<Offset2>>", "0x6A").Replace("<<Offset3>>", "0x69").Replace("<<Offset4>>", "0x68"));
            ChangePropertyDescription(nameof(GrappleGunEndPointY), Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(GrappleGunEndPointY_Hex), Lang.GetAttributeText(aLang.GrappleGunEndPointY_Description).Replace("<<Offset1>>", "0x6F").Replace("<<Offset2>>", "0x6E").Replace("<<Offset3>>", "0x6D").Replace("<<Offset4>>", "0x6C"));
            ChangePropertyDescription(nameof(GrappleGunEndPointZ), Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(GrappleGunEndPointZ_Hex), Lang.GetAttributeText(aLang.GrappleGunEndPointZ_Description).Replace("<<Offset1>>", "0x73").Replace("<<Offset2>>", "0x72").Replace("<<Offset3>>", "0x71").Replace("<<Offset4>>", "0x70"));
            ChangePropertyDescription(nameof(GrappleGunEndPointW), Lang.GetAttributeText(aLang.GrappleGunEndPointW_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointX), Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointX_Hex), Lang.GetAttributeText(aLang.GrappleGunThirdPointX_Description).Replace("<<Offset1>>", "0x77").Replace("<<Offset2>>", "0x76").Replace("<<Offset3>>", "0x75").Replace("<<Offset4>>", "0x74"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointY), Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointY_Hex), Lang.GetAttributeText(aLang.GrappleGunThirdPointY_Description).Replace("<<Offset1>>", "0x7B").Replace("<<Offset2>>", "0x7A").Replace("<<Offset3>>", "0x79").Replace("<<Offset4>>", "0x78"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointZ), Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointZ_Hex), Lang.GetAttributeText(aLang.GrappleGunThirdPointZ_Description).Replace("<<Offset1>>", "0x7F").Replace("<<Offset2>>", "0x7E").Replace("<<Offset3>>", "0x7D").Replace("<<Offset4>>", "0x7C"));
            ChangePropertyDescription(nameof(GrappleGunThirdPointW), Lang.GetAttributeText(aLang.GrappleGunThirdPointW_ByteArray4_Description).Replace("<<Offset1>>", "??").Replace("<<Offset2>>", "??").Replace("<<Offset3>>", "??").Replace("<<Offset4>>", "??"));
            ChangePropertyDescription(nameof(GrappleGunFacingAngle), Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(GrappleGunFacingAngle_Hex), Lang.GetAttributeText(aLang.GrappleGunFacingAngle_Description).Replace("<<Offset1>>", "0x83").Replace("<<Offset2>>", "0x82").Replace("<<Offset3>>", "0x81").Replace("<<Offset4>>", "0x80"));
            ChangePropertyDescription(nameof(GrappleGunParameter0), Lang.GetAttributeText(aLang.GrappleGunParameter0_Byte_Description).Replace("<<Offset1>>", "0x84"));
            ChangePropertyDescription(nameof(GrappleGunParameter1), Lang.GetAttributeText(aLang.GrappleGunParameter1_Byte_Description).Replace("<<Offset1>>", "0x85"));
            ChangePropertyDescription(nameof(GrappleGunParameter2), Lang.GetAttributeText(aLang.GrappleGunParameter2_Byte_Description).Replace("<<Offset1>>", "0x86"));
            ChangePropertyDescription(nameof(GrappleGunParameter3), Lang.GetAttributeText(aLang.GrappleGunParameter3_Byte_Description).Replace("<<Offset1>>", "0x87"));
            ChangePropertyDescription(nameof(Unknown_SK), Lang.GetAttributeText(aLang.Unknown_SK_ByteArray4_Description).Replace("<<Offset1>>", "0x88").Replace("<<Offset2>>", "0x89").Replace("<<Offset3>>", "0x8A").Replace("<<Offset4>>", "0x8B"));
            ChangePropertyDescription(nameof(Unknown_SL), Lang.GetAttributeText(aLang.Unknown_SL_ByteArray4_Description).Replace("<<Offset1>>", "0x8C").Replace("<<Offset2>>", "0x8D").Replace("<<Offset3>>", "0x8E").Replace("<<Offset4>>", "0x8F"));

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
            SetTriggerZoneDescription(0x04);

            if (version == Re4Version.V2007PS2)
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
        private const int CategoryID4_SpecialGeneral = 11;
        private const int CategoryID5_SpecialTypes = 12;
        #endregion

        #region firt propertys

        [CustomCategory(aLang.NewAge_InternalLineIDCategory)]
        [CustomDisplayName(aLang.NewAge_InternalLineIDDisplayName)]
        [CustomDescription(aLang.NewAge_InternalLineIDDescription)]
        [DefaultValue(null)]
        [ReadOnlyAttribute(true)]
        [BrowsableAttribute(true)]
        [DynamicTypeDescriptor.Id(1, CategoryID0_InternalLineID)]
        public string InternalLineID { get => GetInternalID().ToString(); }


        [CustomCategory(aLang.NewAge_LineArrayCategory)]
        [CustomDisplayName(aLang.NewAge_LineArrayDisplayName)]
        [CustomDescription(aLang.NewAge_LineArrayDescription)]
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                    updateMethods.UpdateOrbitCamera();
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
                Methods.SetUnknown_GG(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_MI(InternalID, GetNewByteArrayValue(value)); 
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
                Methods.SetUnknown_MO(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_MU(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_NI(InternalID, GetNewByteArrayValue(value));
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
                updateMethods.UpdateOrbitCamera();
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
                Methods.SetUnknown_PO(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_PU(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_QI(InternalID, GetNewByteArrayValue(value)); 
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
                Methods.SetUnknown_QO(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_QU(InternalID, GetNewByteArrayValue(value));  
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
                Methods.SetUnknown_HH(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_HK(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_HL(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_HM(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_HN(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_HR(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RH(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RJ(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RK(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RL(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RM(InternalID, GetNewByteArrayValue(value));

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
                Methods.SetUnknown_RN(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RP(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RQ(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TG(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TH(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TJ(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TK(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TL(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TM(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TN(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TP(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_TQ(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_VS(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_VT(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_VI(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_VO(InternalID, GetNewByteArrayValue(value));
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdatePropertyGrid();
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
                Methods.SetObjPointW(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetObjPointW_onlyClassic(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RI_W(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_RO(InternalID, GetNewByteArrayValue(value));
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
                if (ListBoxProperty.ItemsList.ContainsKey(v) && v != 0xFFFF)
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
                Methods.SetUnknown_RU(InternalID, GetNewByteArrayValue(value));
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
                if (ListBoxProperty.ItemAuraTypeList.ContainsKey(v))
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
                Methods.SetUnknown_QG(InternalID, GetNewByteArrayValue(value));  
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
                if (!(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV))
                {
                    return Methods.ReturnItemAngleZ(InternalID);
                }
                return 0;
            }
            set
            {
                if (!(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV))
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
                if (!(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV))
                {
                    return Methods.ReturnItemAngleW(InternalID);
                }
                return new byte[4];
            }
            set
            {
                if (!(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV))
                {
                    Methods.SetItemAngleW(InternalID, GetNewByteArrayValue(value));
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
                if (!(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV))
                {
                    return Methods.ReturnItemAngleZ_Hex(InternalID);
                }
                return 0;
            }
            set
            {
                if (!(version == Re4Version.V2007PS2 && specialFileFormat == SpecialFileFormat.AEV))
                {
                    Methods.SetItemAngleZ_Hex(InternalID, value);
                    updateMethods.UpdateGL();
                } 
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
                if (ListBoxProperty.ItemsList.ContainsKey(v) && v != 0xFFFF)
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
                Methods.SetUnknown_SH(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_SM(InternalID, GetNewByteArrayValue(value)); 
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                Methods.SetUnknown_SN(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_SR(InternalID, GetNewByteArrayValue(value)); 
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
                Methods.SetUnknown_SS(InternalID, GetNewByteArrayValue(value));
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                Methods.SetGrappleGunEndPointW(InternalID, GetNewByteArrayValue(value));
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                updateMethods.UpdateOrbitCamera();
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
                Methods.SetGrappleGunThirdPointW(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_SK(InternalID, GetNewByteArrayValue(value));
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
                Methods.SetUnknown_SL(InternalID, GetNewByteArrayValue(value));
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
