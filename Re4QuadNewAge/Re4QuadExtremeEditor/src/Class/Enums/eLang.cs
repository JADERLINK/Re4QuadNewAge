﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum eLang : uint
    {
        #region necessarios

        // MessageBox texts
        MessageBoxErrorTitle,
        MessageBoxWarningTitle,
        MessageBoxFile16MB,
        MessageBoxFile16Bytes,
        MessageBoxFile4Bytes,
        MessageBoxFile0MB,
        MessageBoxFileNotOpen,

        MessageBoxFormClosingTitle,
        MessageBoxFormClosingDialog,

        // nodes names
        NodeESL,
        NodeETS,
        NodeITA,
        NodeAEV,
        NodeEXTRAS,
        NodeDSE,
        NodeEMI,
        NodeSAR,
        NodeEAR,
        NodeESE,
        NodeFSE,
        NodeLIT_GROUPS,
        NodeLIT_ENTRYS,
        NodeQuadCustom,
        NodeEFF_Table1,
        NodeEFF_Table2,
        NodeEFF_Table3,
        NodeEFF_Table4,
        NodeEFF_Table6,
        NodeEFF_Table0,
        NodeEFF_Table7_Effect_0,
        NodeEFF_Table8_Effect_1,
        NodeEFF_EffectEntry,
        NodeEFF_Table9,

        // add new object
        AddNewETS,
        AddNewITA,
        AddNewAEV,
        AddNewDSE,
        AddNewFSE,
        AddNewSAR,
        AddNewEAR,
        AddNewESE,
        AddNewEMI,
        AddNewLIT_ENTRYS,
        AddNewLIT_GROUPS,
        AddNewEFF_Table0,
        AddNewEFF_Table1,
        AddNewEFF_Table2,
        AddNewEFF_Table3,
        AddNewEFF_Table4,
        AddNewEFF_Table6,
        AddNewEFF_Table7,
        AddNewEFF_Table8,
        AddNewEFF_Table9,
        AddNewEFF_EffectEntry7,
        AddNewEFF_EffectEntry8,
        AddNewQuadCustom,
        AddNewNull,

        // delete object dialog
        DeleteObjWarning,
        DeleteObjDialog,

        //MultiSelectEditor Finish MessageBox
        MultiSelectEditorFinishMessageBoxTitle,
        MultiSelectEditorFinishMessageBoxDialog,

        //OptionsForm Warning Load Models
        OptionsFormWarningLoadModelsMessageBoxTitle,
        OptionsFormWarningLoadModelsMessageBoxDialog,

        //OptionsFormSelectDiretory
        OptionsFormSelectDiretory,

        // options Use internal language
        OptionsUseInternalLanguage,

        // labels
        labelCamSpeedPercentage,
        labelObjSpeed,

        // DiretorySalvepatch
        DirectoryESL,
        DirectoryEFFBLOB,
        DirectoryEFFBLOBBIG,
        DirectoryETS,
        DirectoryITA,
        DirectoryAEV,
        DirectoryDSE,
        DirectoryFSE,
        DirectorySAR,
        DirectoryEAR,
        DirectoryEMI,
        DirectoryESE,
        DirectoryLIT,
        DirectoryQuadCustom,

        // room
        SelectedRoom,
        SelectRoom,
        NoneRoom,

        // comboBoxMoveMode
        MoveMode_Enemy_PositionAndRotationAll,
        MoveMode_EseEntry_PositionPoint,
        MoveMode_LitEntry_PositionPoint,
        MoveMode_EffEntry_PositionAndRotationAll,
        MoveMode_EffEffTable9_PositionPoint,
        MoveMode_EmiEntry_PositionAndAnglePoint,
        MoveMode_EtcModel_PositionAndRotationAll,
        MoveMode_EtcModel_Scale,
        MoveMode_Item_PositionAndRotationAll,
        MoveMode_QuadCustomPoint_PositionAndRotationAll,
        MoveMode_QuadCustomPoint_Scale,
        MoveMode_TriggerZone_MoveAll,
        MoveMode_TriggerZone_Point0,
        MoveMode_TriggerZone_Point1,
        MoveMode_TriggerZone_Point2,
        MoveMode_TriggerZone_Point3,
        MoveMode_TriggerZone_Wall01,
        MoveMode_TriggerZone_Wall12,
        MoveMode_TriggerZone_Wall23,
        MoveMode_TriggerZone_Wall30,
        MoveMode_SpecialObj_Position,
        MoveMode_Obj_PositionAndRotationAll,
        MoveMode_Obj_PositionAndRotationY,
        MoveMode_Obj_Position,
        MoveMode_Ashley_Position,
        MoveMode_AshleyZone_MoveAll,
        MoveMode_AshleyZone_Point0,
        MoveMode_AshleyZone_Point1,
        MoveMode_AshleyZone_Point2,
        MoveMode_AshleyZone_Point3,
        MoveMode_TriggerZone_MoveAll_Obj_Position,

        // item rotation options
        RotationXYZ,
        RotationXZY,
        RotationYXZ,
        RotationYZX,
        RotationZYX,
        RotationZXY,
        RotationXY,
        RotationXZ,
        RotationYX,
        RotationYZ,
        RotationZX,
        RotationZY,
        RotationX,
        RotationY,
        RotationZ,


        // menus
        // subsubmenu Save,
        toolStripMenuItemSaveETS,
        toolStripMenuItemSaveITA,
        toolStripMenuItemSaveAEV,
        toolStripMenuItemSaveEMI,
        toolStripMenuItemSaveESE,
        toolStripMenuItemSaveETS_2007_PS2,
        toolStripMenuItemSaveITA_2007_PS2,
        toolStripMenuItemSaveAEV_2007_PS2,
        toolStripMenuItemSaveEMI_2007_PS2,
        toolStripMenuItemSaveESE_2007_PS2,
        toolStripMenuItemSaveETS_UHD,
        toolStripMenuItemSaveITA_UHD,
        toolStripMenuItemSaveAEV_UHD,
        toolStripMenuItemSaveEMI_UHD,
        toolStripMenuItemSaveESE_UHD,
        toolStripMenuItemSaveITA_PS4_NS,
        toolStripMenuItemSaveAEV_PS4_NS,
        toolStripMenuItemSaveLIT_2007_PS2,
        toolStripMenuItemSaveLIT_UHD,
        toolStripMenuItemSaveLIT,
        toolStripMenuItemSaveEFFBLOB_LittleEndian,
        toolStripMenuItemSaveEFFBLOB_BigEndian,
        toolStripMenuItemSaveEFFBLOB,



        // subsubmenu Save As...,
        toolStripMenuItemSaveAsETS,
        toolStripMenuItemSaveAsITA,
        toolStripMenuItemSaveAsAEV,
        toolStripMenuItemSaveAsEMI,
        toolStripMenuItemSaveAsESE,
        toolStripMenuItemSaveAsETS_2007_PS2,
        toolStripMenuItemSaveAsITA_2007_PS2,
        toolStripMenuItemSaveAsAEV_2007_PS2,
        toolStripMenuItemSaveAsEMI_2007_PS2,
        toolStripMenuItemSaveAsESE_2007_PS2,
        toolStripMenuItemSaveAsETS_UHD,
        toolStripMenuItemSaveAsITA_UHD,
        toolStripMenuItemSaveAsAEV_UHD,
        toolStripMenuItemSaveAsEMI_UHD,
        toolStripMenuItemSaveAsESE_UHD,
        toolStripMenuItemSaveAsITA_PS4_NS,
        toolStripMenuItemSaveAsAEV_PS4_NS,
        toolStripMenuItemSaveAsLIT_2007_PS2,
        toolStripMenuItemSaveAsLIT_UHD,
        toolStripMenuItemSaveAsLIT,
        toolStripMenuItemSaveAsEFFBLOB_LittleEndian,
        toolStripMenuItemSaveAsEFFBLOB_BigEndian,
        toolStripMenuItemSaveAsEFFBLOB,

        // subsubmenu Save As (Convert),
        toolStripMenuItemSaveConverterETS,
        toolStripMenuItemSaveConverterITA,
        toolStripMenuItemSaveConverterAEV,
        toolStripMenuItemSaveConverterEMI,
        toolStripMenuItemSaveConverterESE,
        toolStripMenuItemSaveConverterETS_2007_PS2,
        toolStripMenuItemSaveConverterITA_2007_PS2,
        toolStripMenuItemSaveConverterAEV_2007_PS2,
        toolStripMenuItemSaveConverterEMI_2007_PS2,
        toolStripMenuItemSaveConverterESE_2007_PS2,
        toolStripMenuItemSaveConverterETS_UHD,
        toolStripMenuItemSaveConverterITA_UHD,
        toolStripMenuItemSaveConverterAEV_UHD,
        toolStripMenuItemSaveConverterEMI_UHD,
        toolStripMenuItemSaveConverterESE_UHD,


        // enemy Sets

        EnemyExtraSegmentSegund,
        EnemyExtraSegmentThird,
        EnemyExtraSegmentNoSound,
        


        #endregion

        #region usado somente quando ah uma tradução carregada

        // main form

        // menu principal,
        toolStripMenuItemFile,
        toolStripMenuItemEdit,
        toolStripMenuItemView,
        toolStripMenuItemMisc,
        //submenu File,
        toolStripMenuItemNewFile,
        toolStripMenuItemOpen,
        toolStripMenuItemSave,
        toolStripMenuItemSaveAs,
        toolStripMenuItemSaveConverter,
        toolStripMenuItemClear,
        toolStripMenuItemClose,
        // subsubmenu New,
        toolStripMenuItemNewESL,
        toolStripMenuItemNewDSE,
        toolStripMenuItemNewFSE,
        toolStripMenuItemNewSAR,
        toolStripMenuItemNewEAR,
        toolStripMenuItemNewETS_2007_PS2,
        toolStripMenuItemNewITA_2007_PS2,
        toolStripMenuItemNewAEV_2007_PS2,
        toolStripMenuItemNewEMI_2007_PS2,
        toolStripMenuItemNewESE_2007_PS2,
        toolStripMenuItemNewETS_UHD_PS4NS,
        toolStripMenuItemNewITA_UHD,
        toolStripMenuItemNewAEV_UHD,
        toolStripMenuItemNewEMI_UHD_PS4NS,
        toolStripMenuItemNewESE_UHD_PS4NS,
        toolStripMenuItemNewQuadCustom,
        toolStripMenuItemNewITA_PS4_NS,
        toolStripMenuItemNewAEV_PS4_NS,
        toolStripMenuItemNewLIT_2007_PS2,
        toolStripMenuItemNewLIT_UHD_PS4NS,
        toolStripMenuItemNewEFFBLOB,
        // subsubmenu New  Big,
        toolStripMenuItemNewBigEndianFiles,
        toolStripMenuItemNewEFFBLOBBIG,
        // subsubmenu Open,
        toolStripMenuItemOpenESL,
        toolStripMenuItemOpenDSE,
        toolStripMenuItemOpenFSE,
        toolStripMenuItemOpenSAR,
        toolStripMenuItemOpenEAR,
        toolStripMenuItemOpenETS_2007_PS2,
        toolStripMenuItemOpenITA_2007_PS2,
        toolStripMenuItemOpenAEV_2007_PS2,
        toolStripMenuItemOpenEMI_2007_PS2,
        toolStripMenuItemOpenESE_2007_PS2,
        toolStripMenuItemOpenETS_UHD_PS4NS,
        toolStripMenuItemOpenITA_UHD,
        toolStripMenuItemOpenAEV_UHD,
        toolStripMenuItemOpenEMI_UHD_PS4NS,
        toolStripMenuItemOpenESE_UHD_PS4NS,
        toolStripMenuItemOpenQuadCustom,
        toolStripMenuItemOpenITA_PS4_NS,
        toolStripMenuItemOpenAEV_PS4_NS,
        toolStripMenuItemOpenLIT_2007_PS2,
        toolStripMenuItemOpenLIT_UHD_PS4NS,
        toolStripMenuItemOpenEFFBLOB,
        // subsubmenu Open Big,
        toolStripMenuItemOpenBigEndianFiles,
        toolStripMenuItemOpenEFFBLOBBIG,
        // subsubmenu Save,
        toolStripMenuItemSaveESL,
        toolStripMenuItemSaveDSE,
        toolStripMenuItemSaveFSE,
        toolStripMenuItemSaveSAR,
        toolStripMenuItemSaveEAR,
        toolStripMenuItemSaveQuadCustom,
        toolStripMenuItemSaveDirectories,
        // subsubmenu Save As...,
        toolStripMenuItemSaveAsESL,
        toolStripMenuItemSaveAsDSE,
        toolStripMenuItemSaveAsFSE,
        toolStripMenuItemSaveAsSAR,
        toolStripMenuItemSaveAsEAR,
        toolStripMenuItemSaveAsQuadCustom,
        // subsubmenu Clear,
        toolStripMenuItemClearESL,
        toolStripMenuItemClearEFFBLOB,
        toolStripMenuItemClearDSE,
        toolStripMenuItemClearFSE,
        toolStripMenuItemClearSAR,
        toolStripMenuItemClearEAR,
        toolStripMenuItemClearETS,
        toolStripMenuItemClearITA,
        toolStripMenuItemClearAEV,
        toolStripMenuItemClearEMI,
        toolStripMenuItemClearESE,
        toolStripMenuItemClearLIT,
        toolStripMenuItemClearQuadCustom,

        // sub menu edit,
        toolStripMenuItemAddNewObj,
        toolStripMenuItemDeleteSelectedObj,
        toolStripMenuItemMoveUp,
        toolStripMenuItemMoveDown,
        toolStripMenuItemSearch,

        // sub menu Misc,
        toolStripMenuItemOptions,
        toolStripMenuItemCredits,

        // sub menu View,
        toolStripMenuItemSubMenuHide, 
        toolStripMenuItemSubMenuRoom,
        toolStripMenuItemSubMenuModels,
        toolStripMenuItemSubMenuEnemy,
        toolStripMenuItemSubMenuItem,
        toolStripMenuItemSubMenuSpecial,
        toolStripMenuItemSubMenuEtcModel,
        toolStripMenuItemSubMenuLight,
        toolStripMenuItemSubMenuEffect,
        toolStripMenuItemNodeDisplayNameInHex,
        toolStripMenuItemCameraMenu,
        toolStripMenuItemResetCamera,
        toolStripMenuItemRefresh,

        // sub menu hide
        toolStripMenuItemHideRoomModel,
        toolStripMenuItemHideEnemyESL,
        toolStripMenuItemHideEtcmodelETS,
        toolStripMenuItemHideItemsITA,
        toolStripMenuItemHideEventsAEV,
        toolStripMenuItemHideLateralMenu,
        toolStripMenuItemHideBottomMenu,
        toolStripMenuItemHideFileFSE,
        toolStripMenuItemHideFileSAR,
        toolStripMenuItemHideFileEAR,
        toolStripMenuItemHideFileESE,
        toolStripMenuItemHideFileEMI,
        toolStripMenuItemHideFileLIT,
        toolStripMenuItemHideFileEFF,
        toolStripMenuItemHideQuadCustom,

        // sub menus de view,
        toolStripMenuItemHideDesabledEnemy,
        toolStripMenuItemShowOnlyDefinedRoom,
        toolStripMenuItemAutoDefineRoom,
        toolStripMenuItemItemPositionAtAssociatedObjectLocation,
        toolStripMenuItemHideItemTriggerZone,
        toolStripMenuItemHideItemTriggerRadius,
        toolStripMenuItemHideSpecialTriggerZone,
        toolStripMenuItemHideExtraObjs,
        toolStripMenuItemHideOnlyWarpDoor,
        toolStripMenuItemHideExtraExceptWarpDoor,
        toolStripMenuItemUseMoreSpecialColors,
        toolStripMenuItemEtcModelUseScale,
        toolStripMenuItemSubMenuQuadCustom,
        toolStripMenuItemUseCustomColors,
        toolStripMenuItemShowOnlySelectedGroup,
        toolStripMenuItemSelectedGroupUp,
        toolStripMenuItemSelectedGroupDown,
        toolStripMenuItemEnableLightColor,
        toolStripMenuItemShowOnlySelectedGroup_EFF,
        toolStripMenuItemSelectedGroupUp_EFF,
        toolStripMenuItemSelectedGroupDown_EFF,
        toolStripMenuItemHideTable7_EFF,
        toolStripMenuItemHideTable8_EFF,
        toolStripMenuItemHideTable9_EFF,
        toolStripMenuItemDisableGroupPositionEFF,

        // sub menu de view room and model
        toolStripMenuItemModelsHideTextures,
        toolStripMenuItemModelsWireframe,
        toolStripMenuItemModelsRenderNormals,
        toolStripMenuItemModelsOnlyFrontFace,
        toolStripMenuItemModelsVertexColor,
        toolStripMenuItemModelsAlphaChannel,
        toolStripMenuItemRoomHideTextures,
        toolStripMenuItemRoomWireframe,
        toolStripMenuItemRoomRenderNormals,
        toolStripMenuItemRoomOnlyFrontFace,
        toolStripMenuItemRoomVertexColor,
        toolStripMenuItemRoomAlphaChannel,

        toolStripMenuItemRoomTextureIsNearest,
        toolStripMenuItemRoomTextureIsLinear,
        toolStripMenuItemModelsTextureIsNearest,
        toolStripMenuItemModelsTextureIsLinear,


        //save and open windows

        openFileDialogAEV,
        openFileDialogESL,
        openFileDialogETS,
        openFileDialogITA,
        openFileDialogDSE,
        openFileDialogFSE,
        openFileDialogSAR,
        openFileDialogEAR,
        openFileDialogEMI,
        openFileDialogESE,
        openFileDialogLIT,
        openFileDialogEFFBLOB,
        openFileDialogEFFBLOBBIG,
        openFileDialogQuadCustom,

        saveFileDialogConvertAEV,
        saveFileDialogConvertETS,
        saveFileDialogConvertITA,
        saveFileDialogConvertEMI,
        saveFileDialogConvertESE,

        saveFileDialogAEV,
        saveFileDialogESL,
        saveFileDialogETS,
        saveFileDialogITA,
        saveFileDialogDSE,
        saveFileDialogFSE,
        saveFileDialogSAR,
        saveFileDialogEAR,
        saveFileDialogEMI,
        saveFileDialogESE,
        saveFileDialogLIT,
        saveFileDialogEFFBLOB,
        saveFileDialogEFFBLOBBIG,
        saveFileDialogQuadCustom,

        //cameraMove
        buttonGrid,
        labelCamModeText,
        labelMoveCamText,
        CameraMode_Fly,
        CameraMode_Orbit,
        CameraMode_Top,
        CameraMode_Bottom,
        CameraMode_Left,
        CameraMode_Right,
        CameraMode_Front,
        CameraMode_Back,

        //objectMove
        buttonDropToGround,
        checkBoxObjKeepOnGround,
        checkBoxTriggerZoneKeepOnGround,
        checkBoxLockMoveSquareHorizontal,
        checkBoxLockMoveSquareVertical,
        checkBoxMoveRelativeCam,


        //AddNewObjForm
        AddNewObjForm,
        AddNewObjButtonCancel,
        AddNewObjButtonOK,
        labelAmountInfo,
        labelTypeInfo,


        //MultiSelectEditor
        MultiSelectEditor,
        labelValueSumText2,
        labelValueSumText,
        labelPropertyDescriptionText,
        labelChoiseText,
        checkBoxProgressiveSum,
        checkBoxHexadecimal,
        checkBoxDecimal,
        checkBoxCurrentValuePlusValueToAdd,
        buttonSetValue,
        buttonClose,



        //SelectRoomForm
        SelectRoomForm,
        labelInfo,
        labelSelectAList,
        labelSelectARoom,
        SelectRoomButtonLoad,
        SelectRoomButtonCancel,


        //OptionsForm
        OptionsForm,
        Options_buttonCancel,
        Options_buttonOK,
        checkBoxDisableItemRotations,
        checkBoxForceReloadModels,
        checkBoxIgnoreRotationForZeroXYZ,
        checkBoxIgnoreRotationForZisNotGreaterThanZero,
        groupBoxColors,
        groupBoxDirectory,
        groupBoxFloatStyle,
        groupBoxFractionalPart,
        groupBoxInputFractionalSymbol,
        groupBoxItemRotations,
        groupBoxLanguage,
        groupBoxOutputFractionalSymbol,
        labelDivider,
        labelItemExtraCalculation,
        labelitemRotationOrderText,
        labelLanguageWarning,
        labelMultiplier,
        labelSkyColor,
        labelOptionsDirectory,
        radioButtonAcceptsCommaAndPeriod,
        radioButtonOnlyAcceptComma,
        radioButtonOnlyAcceptPeriod,
        radioButtonOutputComma,
        radioButtonOutputPeriod,
        tabPageDiretory,
        tabPageOthers,
        tabPageLists,
        groupBoxLists,
        labelEnemies,
        labelEtcModels,
        labelItems,
        labelQuadCustom,
        groupBoxTheme,
        labelThemeWarning,
        checkBoxUseDarkerGrayTheme,
        groupBoxInvertedMouseButtons,
        labelInvertedMouseButtonsWarning,
        checkBoxUseInvertedMouseButtons,


        //SearchForm
        SearchForm,
        checkBoxFilterMode,

        //CameraForm
        CameraForm,
        CameraLabelInfo,
        CameraButtonClose,
        CameraButtonGetPos,
        CamaraButtonSetPos,

        #endregion

        Null
    }
}
