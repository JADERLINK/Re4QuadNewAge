using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum aLang : uint
    {      
        // ListBox values
        ListBoxEnable,
        ListBoxDisable,
        ListBoxAnotherValue,
        ListBoxUnknownEnemy,
        ListBoxUnknownItem,
        ListBoxUnknownEtcModel,
        ListBoxUnknownQuadCustomPoint,

        //ListBoxFloatType
        ListBoxFloatTypeEnable,
        ListBoxFloatTypeDisable,

        //ListBoxSpecialZoneCategory
        ListBoxSpecialZoneCategory00,
        ListBoxSpecialZoneCategory01,
        ListBoxSpecialZoneCategory02,
        ListBoxSpecialZoneCategoryAnotherValue,

        //ListBoxItemAuraType
        ListBoxItemAuraType00,
        ListBoxItemAuraType01,
        ListBoxItemAuraType02,
        ListBoxItemAuraType03,
        ListBoxItemAuraType04,
        ListBoxItemAuraType05,
        ListBoxItemAuraType06,
        ListBoxItemAuraType07,
        ListBoxItemAuraType08,
        ListBoxItemAuraType09,
        ListBoxItemAuraTypeAnotherValue,

        //ListBoxRefInteractionType
        ListBoxRefInteractionType00,
        ListBoxRefInteractionType01Enemy,
        ListBoxRefInteractionType02EtcModel,
        ListBoxRefInteractionTypeAnotherValue,


        //ListBoxPromptMessageType
        ListBoxPromptMessageTypeAnotherValue,

        //ListBox QuadCustom
        ListBoxQuadCustomPointStatus00,
        ListBoxQuadCustomPointStatus01,
        ListBoxQuadCustomPointStatus02,
        ListBoxQuadCustomPointStatusAnotherValue,



        //FloatType
        FloatTypeCategory,
        FloatType_DisplayName,
        FloatType_Description,


        // special types
        SpecialType00_GeneralPurpose,
        SpecialType01_WarpDoor,
        SpecialType02_CutSceneEvents,
        SpecialType03_Items,
        SpecialType04_GroupedEnemyTrigger,
        SpecialType05_Message,
        //SpecialType06_Unused,
        //SpecialType07_Unused,
        SpecialType08_TypeWriter,
        //SpecialType09_Unused,
        SpecialType0A_DamagesThePlayer,
        SpecialType0B_FalseCollision,
        //SpecialType0C_Unused,
        SpecialType0D_FieldInfo,
        SpecialType0E_Crouch,
        //SpecialType0F_Unused,
        SpecialType10_FixedLadderClimbUp,
        SpecialType11_ItemDependentEvents,
        SpecialType12_AshleyHideCommand,
        SpecialType13_LocalTeleportation,
        SpecialType14_UsedForElevators,
        SpecialType15_AdaGrappleGun,
        SpecialTypeUnspecifiedType,


        // EnemyProperty
        Enemy_OrderCategory,
        Enemy_OrderDisplayName,
        Enemy_OrderDescription,
        Enemy_AssociatedSpecialEventCategory,
        Enemy_AssociatedSpecialEventObjNameDisplayName,
        Enemy_AssociatedSpecialEventObjNameDescription,
        Enemy_AssociatedSpecialEventTypeDisplayName,
        Enemy_AssociatedSpecialEventTypeDescription,
        Enemy_AssociatedSpecialEventFromSpecialIndexDisplayName,
        Enemy_AssociatedSpecialEventFromSpecialIndexFromDescription,
        Enemy_AssociatedSpecialEventFromFileDisplayName,
        Enemy_AssociatedSpecialEventFromFileFromDescription,
        Enemy_LineArrayCategory,
        Enemy_LineArrayDisplayName,
        Enemy_LineArrayDescription,
        EnemyCategory,
        ESL_ENABLE_List_Name,
        ESL_ENABLE_Byte_Name,
        ESL_ENABLE_Byte_Description,
        ESL_ENEMY_ID_List_Name,
        ESL_ENEMY_ID_UshotUnflip_Name,
        ESL_ENEMY_ID_UshotUnflip_Description,
        ESL_HX03_Byte_Name,
        ESL_HX03_Byte_Description,
        ESL_HX04_Byte_Name,
        ESL_HX04_Byte_Description,
        ESL_HX05_Byte_Name,
        ESL_HX05_Byte_Description,
        ESL_HX06_Byte_Name,
        ESL_HX06_Byte_Description,
        ESL_HX07_Byte_Name,
        ESL_HX07_Byte_Description,
        ESL_EnemyLifeAmount_Short_Name,
        ESL_EnemyLifeAmount_Short_Description,
        ESL_HX0A_Byte_Name,
        ESL_HX0A_Byte_Description,
        ESL_HX0B_Byte_Name,
        ESL_HX0B_Byte_Description,
        ESL_PositionX_Short_Name,
        ESL_PositionX_Short_Description,
        ESL_PositionY_Short_Name,
        ESL_PositionY_Short_Description,
        ESL_PositionZ_Short_Name,
        ESL_PositionZ_Short_Description,
        ESL_AngleX_Short_Name,
        ESL_AngleX_Short_Description,
        ESL_AngleY_Short_Name,
        ESL_AngleY_Short_Description,
        ESL_AngleZ_Short_Name,
        ESL_AngleZ_Short_Description,
        ESL_ROOM_ID_Ushort_Name,
        ESL_ROOM_ID_Ushort_Description,
        ESL_HX1A_Byte_Name,
        ESL_HX1A_Byte_Description,
        ESL_HX1B_Byte_Name,
        ESL_HX1B_Byte_Description,
        ESL_HX1C_Byte_Name,
        ESL_HX1C_Byte_Description,
        ESL_HX1D_Byte_Name,
        ESL_HX1D_Byte_Description,
        ESL_HX1E_Byte_Name,
        ESL_HX1E_Byte_Description,
        ESL_HX1F_Byte_Name,
        ESL_HX1F_Byte_Description,
        
        // EtcModelProperty
        EtcModel_AssociatedSpecialEventCategory,
        EtcModel_AssociatedSpecialEventObjNameDisplayName,
        EtcModel_AssociatedSpecialEventObjNameDescription,
        EtcModel_AssociatedSpecialEventTypeDisplayName,
        EtcModel_AssociatedSpecialEventTypeDescription,
        EtcModel_AssociatedSpecialEventFromSpecialIndexDisplayName,
        EtcModel_AssociatedSpecialEventFromSpecialIndexFromDescription,
        EtcModel_AssociatedSpecialEventFromFileDisplayName,
        EtcModel_AssociatedSpecialEventFromFileDescription,
        EtcModelCategory,

        EtcModelID_Ushort_DisplayName,
        EtcModelID_List_DisplayName,
        EtcModelID_Ushort_Description,

        ETS_ID_Ushort_DisplayName,
        ETS_ID_Ushort_Description,

        //Unknown_TTS  -  not Scale
        ETCM_Unknown_TTS_X_Float_DisplayName,
        ETCM_Unknown_TTS_X_Hex_DisplayName,
        ETCM_Unknown_TTS_X_Description,

        ETCM_Unknown_TTS_Y_Float_DisplayName,
        ETCM_Unknown_TTS_Y_Hex_DisplayName,
        ETCM_Unknown_TTS_Y_Description,

        ETCM_Unknown_TTS_Z_Float_DisplayName,
        ETCM_Unknown_TTS_Z_Hex_DisplayName,
        ETCM_Unknown_TTS_Z_Description,

        //Angle
        ETCM_AngleX_Float_DisplayName,
        ETCM_AngleX_Hex_DisplayName,
        ETCM_AngleX_Description,

        ETCM_AngleY_Float_DisplayName,
        ETCM_AngleY_Hex_DisplayName,
        ETCM_AngleY_Description,

        ETCM_AngleZ_Float_DisplayName,
        ETCM_AngleZ_Hex_DisplayName,
        ETCM_AngleZ_Description,

        //Position
        ETCM_PositionX_Float_DisplayName,
        ETCM_PositionX_Hex_DisplayName,
        ETCM_PositionX_Description,

        ETCM_PositionY_Float_DisplayName,
        ETCM_PositionY_Hex_DisplayName,
        ETCM_PositionY_Description,

        ETCM_PositionZ_Float_DisplayName,
        ETCM_PositionZ_Hex_DisplayName,
        ETCM_PositionZ_Description,

        //unused values only classic 

        ETCM_UnusedsInfo_Text,
        ETCM_UnusedsInfo_DisplayName,
        ETCM_UnusedsInfo_Description,

        ETCM_Unknown_TTS_W_Float_DisplayName,
        ETCM_Unknown_TTS_W_Hex_DisplayName,
        ETCM_Unknown_TTS_W_Description,

        ETCM_AngleW_Float_DisplayName,
        ETCM_AngleW_Hex_DisplayName,
        ETCM_AngleW_Description,

        ETCM_PositionW_Float_DisplayName,
        ETCM_PositionW_Hex_DisplayName,
        ETCM_PositionW_Description,

        ETCM_Unknown_TTJ_ByteArray4_DisplayName,
        ETCM_Unknown_TTJ_ByteArray4_Description,

        ETCM_Unknown_TTH_ByteArray4_DisplayName,
        ETCM_Unknown_TTH_ByteArray4_Description,

        ETCM_Unknown_TTG_ByteArray4_DisplayName,
        ETCM_Unknown_TTG_ByteArray4_Description,


        // Special

        SpecialCategory,
        SpecialTypeCategory,
        SpecialTriggerZoneCategory,
        SpecialGeneralCategory, // geral


        //SpecialTypeCategory

        SpecialTypeID_Byte_DisplayName,
        SpecialTypeID_List_DisplayName,
        SpecialTypeID_Byte_Description,

        SpecialIndex_Byte_DisplayName,
        SpecialIndex_Byte_Description,


        //Special Geral //"general"

        Unknown_GG_ByteArray4_DisplayName,
        Unknown_GG_ByteArray4_Description,

        Unknown_KG_Byte_DisplayName,
        Unknown_KG_Byte_Description,

        Unknown_KJ_Byte_DisplayName,
        Unknown_KJ_Byte_Description,

        Unknown_LI_Byte_DisplayName,
        Unknown_LI_Byte_Description,

        Unknown_LO_Byte_DisplayName,
        Unknown_LO_Byte_Description,

        Unknown_LU_Byte_DisplayName,
        Unknown_LU_Byte_Description,

        Unknown_LH_Byte_DisplayName,
        Unknown_LH_Byte_Description,

        Unknown_MI_ByteArray2_DisplayName,
        Unknown_MI_ByteArray2_Description,

        Unknown_MO_ByteArray2_DisplayName,
        Unknown_MO_ByteArray2_Description,

        Unknown_MU_ByteArray2_DisplayName,
        Unknown_MU_ByteArray2_Description,

        Unknown_NI_ByteArray2_DisplayName,
        Unknown_NI_ByteArray2_Description,

        Unknown_NO_Byte_DisplayName,
        Unknown_NO_Byte_Description,

        Unknown_NS_Byte_DisplayName,
        Unknown_NS_Byte_Description,

        RefInteractionType_Byte_DisplayName,
        RefInteractionType_List_DisplayName,
        RefInteractionType_Byte_Description,

        RefInteractionIndex_Byte_DisplayName,
        RefInteractionIndex_Byte_Description,

        Unknown_NT_Byte_DisplayName,
        Unknown_NT_Byte_Description,

        Unknown_NU_Byte_DisplayName,
        Unknown_NU_Byte_Description,

        PromptMessage_Byte_DisplayName,
        PromptMessage_List_DisplayName,
        PromptMessage_Byte_Description,

        Unknown_PI_Byte_DisplayName,
        Unknown_PI_Byte_Description,

        Unknown_PO_ByteArray4_DisplayName,
        Unknown_PO_ByteArray4_Description,

        Unknown_PU_ByteArray2_DisplayName,
        Unknown_PU_ByteArray2_Description,

        Unknown_PK_Byte_DisplayName,
        Unknown_PK_Byte_Description,

        MessageColor_Byte_DisplayName,
        MessageColor_Byte_Description,

        Unknown_QI_ByteArray4_DisplayName,
        Unknown_QI_ByteArray4_Description,

        Unknown_QO_ByteArray4_DisplayName,
        Unknown_QO_ByteArray4_Description,

        Unknown_QU_ByteArray4_DisplayName,
        Unknown_QU_ByteArray4_Description,

        //Unknown/geral types

        Unknown_HH_ByteArray2_DisplayName,
        Unknown_HH_ByteArray2_Description,

        Unknown_HK_ByteArray2_DisplayName,
        Unknown_HK_ByteArray2_Description,

        Unknown_HL_ByteArray2_DisplayName,
        Unknown_HL_ByteArray2_Description,

        Unknown_HM_ByteArray2_DisplayName,
        Unknown_HM_ByteArray2_Description,

        Unknown_HN_ByteArray2_DisplayName,
        Unknown_HN_ByteArray2_Description,

        Unknown_HR_ByteArray2_DisplayName,
        Unknown_HR_ByteArray2_Description,

        Unknown_RH_ByteArray2_DisplayName,
        Unknown_RH_ByteArray2_Description,

        Unknown_RJ_ByteArray2_DisplayName,
        Unknown_RJ_ByteArray2_Description,

        Unknown_RK_ByteArray2_DisplayName,
        Unknown_RK_ByteArray2_Description,

        Unknown_RL_ByteArray2_DisplayName,
        Unknown_RL_ByteArray2_Description,

        Unknown_RM_ByteArray2_DisplayName,
        Unknown_RM_ByteArray2_Description,

        Unknown_RN_ByteArray2_DisplayName,
        Unknown_RN_ByteArray2_Description,

        Unknown_RP_ByteArray2_DisplayName,
        Unknown_RP_ByteArray2_Description,

        Unknown_RQ_ByteArray2_DisplayName,
        Unknown_RQ_ByteArray2_Description,

        Unknown_TG_ByteArray4_DisplayName,
        Unknown_TG_ByteArray4_Description,

        Unknown_TH_ByteArray4_DisplayName,
        Unknown_TH_ByteArray4_Description,

        Unknown_TJ_ByteArray4_DisplayName,
        Unknown_TJ_ByteArray4_Description,

        Unknown_TK_ByteArray4_DisplayName,
        Unknown_TK_ByteArray4_Description,

        Unknown_TL_ByteArray4_DisplayName,
        Unknown_TL_ByteArray4_Description,

        Unknown_TM_ByteArray4_DisplayName,
        Unknown_TM_ByteArray4_Description,

        Unknown_TN_ByteArray4_DisplayName,
        Unknown_TN_ByteArray4_Description,

        Unknown_TP_ByteArray4_DisplayName,
        Unknown_TP_ByteArray4_Description,

        Unknown_TQ_ByteArray4_DisplayName,
        Unknown_TQ_ByteArray4_Description,

        //end only ITA Classic

        Unknown_VS_ByteArray4_DisplayName,
        Unknown_VS_ByteArray4_Description,

        Unknown_VT_ByteArray4_DisplayName,
        Unknown_VT_ByteArray4_Description,

        Unknown_VI_ByteArray4_DisplayName,
        Unknown_VI_ByteArray4_Description,

        Unknown_VO_ByteArray4_DisplayName,
        Unknown_VO_ByteArray4_Description,


        //ObjPoint

        ObjPointX_Float_DisplayName,
        ObjPointX_Hex_DisplayName,
        ObjPointX_Description,

        ObjPointY_Float_DisplayName,
        ObjPointY_Hex_DisplayName,
        ObjPointY_Description,

        ObjPointZ_Float_DisplayName,
        ObjPointZ_Hex_DisplayName,
        ObjPointZ_Description,

        ObjPointW_ByteArray4_DisplayName,
        ObjPointW_ByteArray4_Description,

        ObjPointW_onlyClassic_ByteArray4_DisplayName,
        ObjPointW_onlyClassic_ByteArray4_Description,


        // outros types
        //TYPE 0x04, 0x05, 0x0A and 0x11

        NeededItemNumber_Ushort_DisplayName,
        NeededItemNumber_List_DisplayName,
        NeededItemNumber_Ushort_Description,


        EnemyGroup_Ushort_DisplayName,
        EnemyGroup_Ushort_Description,


        RoomMessage_Ushort_DisplayName,
        RoomMessage_Ushort_Description,

        MessageCutSceneID_Ushort_DisplayName,
        MessageCutSceneID_Ushort_Description,

        MessageID_Ushort_DisplayName,
        MessageID_Ushort_Description,


        ActivationType_Byte_DisplayName,
        ActivationType_Byte_Description,

        DamageType_Byte_DisplayName,
        DamageType_Byte_Description,

        BlockingType_Byte_DisplayName,
        BlockingType_Byte_Description,

        Unknown_SJ_Byte_DisplayName,
        Unknown_SJ_Byte_Description,

        DamageAmount_Ushort_DisplayName,
        DamageAmount_Ushort_Description,


        // Type 0x01 warp door

        DestinationFacingAngle_Float_DisplayName,
        DestinationFacingAngle_Hex_DisplayName,
        DestinationFacingAngle_Description,

        DestinationRoom_UshortUnflip_DisplayName,
        DestinationRoom_UshortUnflip_Description,

        LockedDoorType_Byte_DisplayName,
        LockedDoorType_Byte_Description,

        LockedDoorIndex_Byte_DisplayName,
        LockedDoorIndex_Byte_Description,


        //Type 0x13 Teleportation

        TeleportationFacingAngle_Float_DisplayName,
        TeleportationFacingAngle_Hex_DisplayName,
        TeleportationFacingAngle_Description,

        //Type 0x10 LadderClimbUp

        LadderFacingAngle_Float_DisplayName,
        LadderFacingAngle_Hex_DisplayName,
        LadderFacingAngle_Description,

        LadderStepCount_Sbyte_DisplayName,
        LadderStepCount_Sbyte_Description,

        LadderParameter0_Byte_DisplayName,
        LadderParameter0_Byte_Description,

        LadderParameter1_Byte_DisplayName,
        LadderParameter1_Byte_Description,

        LadderParameter2_Byte_DisplayName,
        LadderParameter2_Byte_Description,

        LadderParameter3_Byte_DisplayName,
        LadderParameter3_Byte_Description,

        Unknown_SG_Byte_DisplayName,
        Unknown_SG_Byte_Description,

        Unknown_SH_ByteArray2_DisplayName,
        Unknown_SH_ByteArray2_Description,

        //// type 0x03 itens

        Unknown_RI_X_Float_DisplayName,
        Unknown_RI_X_Hex_DisplayName,
        Unknown_RI_X_Description,

        Unknown_RI_Y_Float_DisplayName,
        Unknown_RI_Y_Hex_DisplayName,
        Unknown_RI_Y_Description,

        Unknown_RI_Z_Float_DisplayName,
        Unknown_RI_Z_Hex_DisplayName,
        Unknown_RI_Z_Description,

        Unknown_RI_W_ByteArray4_DisplayName,
        Unknown_RI_W_ByteArray4_Description,

        Unknown_RO_ByteArray4_DisplayName,
        Unknown_RO_ByteArray4_Description,

        ItemNumber_Ushort_DisplayName,
        ItemNumber_List_DisplayName,
        ItemNumber_Ushort_Description,

        Unknown_RU_ByteArray2_DisplayName,
        Unknown_RU_ByteArray2_Description,

        ItemAmount_Ushort_DisplayName,
        ItemAmount_Ushort_Description,

        SecundIndex_Ushort_DisplayName,
        SecundIndex_Ushort_Description,

        ItemAuraType_Ushort_DisplayName,
        ItemAuraType_List_DisplayName,
        ItemAuraType_Ushort_Description,

        Unknown_QM_Byte_DisplayName,
        Unknown_QM_Byte_Description,

        Unknown_QL_Byte_DisplayName,
        Unknown_QL_Byte_Description,

        Unknown_QR_Byte_DisplayName,
        Unknown_QR_Byte_Description,

        Unknown_QH_Byte_DisplayName,
        Unknown_QH_Byte_Description,

        Unknown_QG_ByteArray2_DisplayName,
        Unknown_QG_ByteArray2_Description,

        ItemTriggerRadius_Float_DisplayName,
        ItemTriggerRadius_Hex_DisplayName,
        ItemTriggerRadius_Description,

        ItemAngleX_Float_DisplayName,
        ItemAngleX_Hex_DisplayName,
        ItemAngleX_Description,

        ItemAngleY_Float_DisplayName,
        ItemAngleY_Hex_DisplayName,
        ItemAngleY_Description,

        ItemAngleZ_Float_DisplayName,
        ItemAngleZ_Hex_DisplayName,
        ItemAngleZ_Description,

        ItemAngleW_ByteArray4_DisplayName,
        ItemAngleW_ByteArray4_Description,


        // type 0x14 AshleyHiding


        AshleyHidingPointX_Float_DisplayName,
        AshleyHidingPointX_Hex_DisplayName,
        AshleyHidingPointX_Description,

        AshleyHidingPointY_Float_DisplayName,
        AshleyHidingPointY_Hex_DisplayName,
        AshleyHidingPointY_Description,

        AshleyHidingPointZ_Float_DisplayName,
        AshleyHidingPointZ_Hex_DisplayName,
        AshleyHidingPointZ_Description,

        AshleyHidingZoneCorner0_X_Float_DisplayName,
        AshleyHidingZoneCorner0_X_Hex_DisplayName,
        AshleyHidingZoneCorner0_X_Description,

        AshleyHidingZoneCorner0_Z_Float_DisplayName,
        AshleyHidingZoneCorner0_Z_Hex_DisplayName,
        AshleyHidingZoneCorner0_Z_Description,

        AshleyHidingZoneCorner1_X_Float_DisplayName,
        AshleyHidingZoneCorner1_X_Hex_DisplayName,
        AshleyHidingZoneCorner1_X_Description,

        AshleyHidingZoneCorner1_Z_Float_DisplayName,
        AshleyHidingZoneCorner1_Z_Hex_DisplayName,
        AshleyHidingZoneCorner1_Z_Description,

        AshleyHidingZoneCorner2_X_Float_DisplayName,
        AshleyHidingZoneCorner2_X_Hex_DisplayName,
        AshleyHidingZoneCorner2_X_Description,

        AshleyHidingZoneCorner2_Z_Float_DisplayName,
        AshleyHidingZoneCorner2_Z_Hex_DisplayName,
        AshleyHidingZoneCorner2_Z_Description,

        AshleyHidingZoneCorner3_X_Float_DisplayName,
        AshleyHidingZoneCorner3_X_Hex_DisplayName,
        AshleyHidingZoneCorner3_X_Description,

        AshleyHidingZoneCorner3_Z_Float_DisplayName,
        AshleyHidingZoneCorner3_Z_Hex_DisplayName,
        AshleyHidingZoneCorner3_Z_Description,

        Unknown_SM_ByteArray4_DisplayName,
        Unknown_SM_ByteArray4_Description,

        Unknown_SN_ByteArray4_DisplayName,
        Unknown_SN_ByteArray4_Description,

        Unknown_SP_Byte_DisplayName,
        Unknown_SP_Byte_Description,

        Unknown_SQ_Byte_DisplayName,
        Unknown_SQ_Byte_Description,

        Unknown_SR_ByteArray2_DisplayName,
        Unknown_SR_ByteArray2_Description,

        Unknown_SS_ByteArray4_DisplayName,
        Unknown_SS_ByteArray4_Description,

        // type 0x15 Ada Grapple Gun

        GrappleGunEndPointX_Float_DisplayName,
        GrappleGunEndPointX_Hex_DisplayName,
        GrappleGunEndPointX_Description,

        GrappleGunEndPointY_Float_DisplayName,
        GrappleGunEndPointY_Hex_DisplayName,
        GrappleGunEndPointY_Description,

        GrappleGunEndPointZ_Float_DisplayName,
        GrappleGunEndPointZ_Hex_DisplayName,
        GrappleGunEndPointZ_Description,

        GrappleGunEndPointW_ByteArray4_DisplayName,
        GrappleGunEndPointW_ByteArray4_Description,

        GrappleGunThirdPointX_Float_DisplayName,
        GrappleGunThirdPointX_Hex_DisplayName,
        GrappleGunThirdPointX_Description,

        GrappleGunThirdPointY_Float_DisplayName,
        GrappleGunThirdPointY_Hex_DisplayName,
        GrappleGunThirdPointY_Description,

        GrappleGunThirdPointZ_Float_DisplayName,
        GrappleGunThirdPointZ_Hex_DisplayName,
        GrappleGunThirdPointZ_Description,

        GrappleGunThirdPointW_ByteArray4_DisplayName,
        GrappleGunThirdPointW_ByteArray4_Description,

        GrappleGunFacingAngle_Float_DisplayName,
        GrappleGunFacingAngle_Hex_DisplayName,
        GrappleGunFacingAngle_Description,

        GrappleGunParameter0_Byte_DisplayName,
        GrappleGunParameter0_Byte_Description,

        GrappleGunParameter1_Byte_DisplayName,
        GrappleGunParameter1_Byte_Description,

        GrappleGunParameter2_Byte_DisplayName,
        GrappleGunParameter2_Byte_Description,

        GrappleGunParameter3_Byte_DisplayName,
        GrappleGunParameter3_Byte_Description,

        Unknown_SK_ByteArray4_DisplayName,
        Unknown_SK_ByteArray4_Description,

        Unknown_SL_ByteArray4_DisplayName,
        Unknown_SL_ByteArray4_Description,


        //MultiSelect
        MultiSelectCategory,
        MultiSelectAmountSelected,

        MultiSelectInfoDisplayName,
        MultiSelectInfoValueText,
        MultiSelectInfoDescription,

        //DSE Property
        NewAge_DSE_Category,

        DSE_HX00_Ushort_DisplayName,
        DSE_HX00_Ushort_Description,

        DSE_HX02_Ushort_DisplayName,
        DSE_HX02_Ushort_Description,

        DSE_HX04_Ushort_DisplayName,
        DSE_HX04_Ushort_Description,

        DSE_HX06_Ushort_DisplayName,
        DSE_HX06_Ushort_Description,

        DSE_HX08_Ushort_DisplayName,
        DSE_HX08_Ushort_Description,

        DSE_HX0A_Ushort_DisplayName,
        DSE_HX0A_Ushort_Description,

        // Property gerais para todos

        UnderDevelopment_Category,
        UnderDevelopment_DisplayName,
        UnderDevelopment_Description,
        UnderDevelopment_Value,

        NewAge_InternalLineIDCategory,
        NewAge_InternalLineIDDisplayName,
        NewAge_InternalLineIDDescription,
        NewAge_LineArrayCategory,
        NewAge_LineArrayDisplayName,
        NewAge_LineArrayDescription,

        //TriggerZoneCategory

        Unknown_GH_Byte_DisplayName,
        Unknown_GH_Byte_Description,

        TriggerZoneCategory_Byte_DisplayName,
        TriggerZoneCategory_List_DisplayName,
        TriggerZoneCategory_Byte_Description,

        Unknown_GK_ByteArray2_DisplayName,
        Unknown_GK_ByteArray2_Description,

        TriggerZoneTrueY_Float_DisplayName,
        TriggerZoneTrueY_Hex_DisplayName,
        TriggerZoneTrueY_Description,

        TriggerZoneMoreHeight_Float_DisplayName,
        TriggerZoneMoreHeight_Hex_DisplayName,
        TriggerZoneMoreHeight_Description,

        TriggerZoneCircleRadius_Float_DisplayName,
        TriggerZoneCircleRadius_Hex_DisplayName,
        TriggerZoneCircleRadius_Description,

        TriggerZoneCorner0_X_Float_DisplayName,
        TriggerZoneCorner0_X_Hex_DisplayName,
        TriggerZoneCorner0_X_Description,

        TriggerZoneCorner0_Z_Float_DisplayName,
        TriggerZoneCorner0_Z_Hex_DisplayName,
        TriggerZoneCorner0_Z_Description,


        TriggerZoneCorner1_X_Float_DisplayName,
        TriggerZoneCorner1_X_Hex_DisplayName,
        TriggerZoneCorner1_X_Description,

        TriggerZoneCorner1_Z_Float_DisplayName,
        TriggerZoneCorner1_Z_Hex_DisplayName,
        TriggerZoneCorner1_Z_Description,

        TriggerZoneCorner2_X_Float_DisplayName,
        TriggerZoneCorner2_X_Hex_DisplayName,
        TriggerZoneCorner2_X_Description,

        TriggerZoneCorner2_Z_Float_DisplayName,
        TriggerZoneCorner2_Z_Hex_DisplayName,
        TriggerZoneCorner2_Z_Description,

        TriggerZoneCorner3_X_Float_DisplayName,
        TriggerZoneCorner3_X_Hex_DisplayName,
        TriggerZoneCorner3_X_Description,

        TriggerZoneCorner3_Z_Float_DisplayName,
        TriggerZoneCorner3_Z_Hex_DisplayName,
        TriggerZoneCorner3_Z_Description,

        //quadCuston

        QuadCustom_Category,
        QuadCustom_ObjectName_Category,
        QuadCustom_Point_Position_Category,
        QuadCustom_Point_Position_Div100_Category,
        QuadCustom_Point_Scale_Category,
        QuadCustom_Point_Angle_Category,
        QuadCustom_Point_Info_Category,
        QuadCustom_Color_Category,

        //Scale
        QuadCustom_ScaleX_Float_DisplayName,
        QuadCustom_ScaleX_Hex_DisplayName,
        QuadCustom_ScaleX_Description,

        QuadCustom_ScaleY_Float_DisplayName,
        QuadCustom_ScaleY_Hex_DisplayName,
        QuadCustom_ScaleY_Description,

        QuadCustom_ScaleZ_Float_DisplayName,
        QuadCustom_ScaleZ_Hex_DisplayName,
        QuadCustom_ScaleZ_Description,

        //Angle
        QuadCustom_AngleX_Float_DisplayName,
        QuadCustom_AngleX_Hex_DisplayName,
        QuadCustom_AngleX_Description,

        QuadCustom_AngleY_Float_DisplayName,
        QuadCustom_AngleY_Hex_DisplayName,
        QuadCustom_AngleY_Description,

        QuadCustom_AngleZ_Float_DisplayName,
        QuadCustom_AngleZ_Hex_DisplayName,
        QuadCustom_AngleZ_Description,

        //Position
        QuadCustom_PositionX_Float_DisplayName,
        QuadCustom_PositionX_Hex_DisplayName,
        QuadCustom_PositionX_Description,

        QuadCustom_PositionY_Float_DisplayName,
        QuadCustom_PositionY_Hex_DisplayName,
        QuadCustom_PositionY_Description,

        QuadCustom_PositionZ_Float_DisplayName,
        QuadCustom_PositionZ_Hex_DisplayName,
        QuadCustom_PositionZ_Description,

        //Position 1/100
        QuadCustom_PositionX_Div100_Float_DisplayName,
        QuadCustom_PositionX_Div100_Hex_DisplayName,
        QuadCustom_PositionX_Div100_Description,

        QuadCustom_PositionY_Div100_Float_DisplayName,
        QuadCustom_PositionY_Div100_Hex_DisplayName,
        QuadCustom_PositionY_Div100_Description,

        QuadCustom_PositionZ_Div100_Float_DisplayName,
        QuadCustom_PositionZ_Div100_Hex_DisplayName,
        QuadCustom_PositionZ_Div100_Description,

        //quadCuston outros
        QuadCustom_PointStatus_Byte_DisplayName,
        QuadCustom_PointStatus_List_DisplayName,
        QuadCustom_PointStatus_Description,

        QuadCustom_PointModelID_Uint_DisplayName,
        QuadCustom_PointModelID_List_DisplayName,
        QuadCustom_PointModelID_Description,

        QuadCustom_ColorRGB_ByteArray_DisplayName,
        QuadCustom_ColorRGB_ByteArray_Description,

        QuadCustom_ObjectName_String_DisplayName,
        QuadCustom_ObjectName_String_Description,

        //lines
        QuadCustom_PartArray_Category,

        QuadCustom_TriggerZoneArray_DisplayName,
        QuadCustom_TriggerZoneArray_Description,

        QuadCustom_PositionArray_DisplayName,
        QuadCustom_PositionArray_Description,

        QuadCustom_AngleArray_DisplayName,
        QuadCustom_angleArray_Description,

        QuadCustom_ScaleArray_DisplayName,
        QuadCustom_ScaleArray_Description,

        //ESE
        ESE_Position_Category,
        ESE_PositionX_Float_DisplayName,
        ESE_PositionX_Hex_DisplayName,
        ESE_PositionX_Description,

        ESE_PositionY_Float_DisplayName,
        ESE_PositionY_Hex_DisplayName,
        ESE_PositionY_Description,

        ESE_PositionZ_Float_DisplayName,
        ESE_PositionZ_Hex_DisplayName,
        ESE_PositionZ_Description,

        //EMI
        EMI_Position_Category,
        EMI_PositionX_Float_DisplayName,
        EMI_PositionX_Hex_DisplayName,
        EMI_PositionX_Description,

        EMI_PositionY_Float_DisplayName,
        EMI_PositionY_Hex_DisplayName,
        EMI_PositionY_Description,

        EMI_PositionZ_Float_DisplayName,
        EMI_PositionZ_Hex_DisplayName,
        EMI_PositionZ_Description,

        EMI_Angle_Category,
        EMI_AngleY_Float_DisplayName,
        EMI_AngleY_Hex_DisplayName,
        EMI_AngleY_Description,

        //LIT entry

        LIT_Light_Metadata_Category,

        LIT_Light_Format_Byte_DisplayName,
        LIT_Light_Type_Byte_DisplayName,
        LIT_Light_Attributes_Byte_DisplayName,
        LIT_Light_Mask_Byte_DisplayName,

        LIT_Light_Definition_Category,

        LIT_PositionX_Float_DisplayName,
        LIT_PositionX_Hex_DisplayName,
        LIT_PositionY_Float_DisplayName,
        LIT_PositionY_Hex_DisplayName,
        LIT_PositionZ_Float_DisplayName,
        LIT_PositionZ_Hex_DisplayName,
        LIT_RangeRadius_Float_DisplayName,
        LIT_RangeRadius_Hex_DisplayName,
        LIT_ColorRGB_ByteArray3_DisplayName,
        LIT_ColorAlfa_Byte_DisplayName,
        LIT_Intensity_Float_DisplayName,
        LIT_Intensity_Hex_DisplayName,

        LIT_Object_Settings_Category,

        LIT_Parent_Byte_DisplayName,
        LIT_Subgroup_Byte_DisplayName,
        LIT_Flag_Byte_DisplayName,
        LIT_Priority_Byte_DisplayName,
        LIT_PartNumber_Ushort_DisplayName,
        LIT_ParentID_Ushort_DisplayName,

        LIT_Group_Order_Category,
        LIT_Entry_Order_Category,

        LIT_Group_GroupOrderID_Ushort_DisplayName,
        LIT_Group_LIT_GetEntryCountInGroup_Int_DisplayName,
        LIT_Entry_GroupOrderID_Ushort_DisplayName,
        LIT_Entry_EntryOrderID_Ushort_DisplayName,

        Null
    }
}
