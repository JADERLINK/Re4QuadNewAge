using System;
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

        // add new object
        AddNewETS,
        AddNewITA,
        AddNewAEV,
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
        DiretoryESL,
        DiretoryETS,
        DiretoryITA,
        DiretoryAEV,

        // room
        SelectedRoom,
        SelectRoom,
        NoneRoom,

        // comboBoxMoveMode
        MoveMode_Enemy_PositionAndRotationAll,
        MoveMode_EtcModel_PositionAndRotationAll,
        MoveMode_EtcModel_Scale,
        MoveMode_Item_PositionAndRotationAll,
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
        toolStripMenuItemSaveETS_2007_PS2,
        toolStripMenuItemSaveITA_2007_PS2,
        toolStripMenuItemSaveAEV_2007_PS2,
        toolStripMenuItemSaveETS_UHD,
        toolStripMenuItemSaveITA_UHD,
        toolStripMenuItemSaveAEV_UHD,

        // subsubmenu Save As...,
        toolStripMenuItemSaveAsETS,
        toolStripMenuItemSaveAsITA,
        toolStripMenuItemSaveAsAEV,
        toolStripMenuItemSaveAsETS_2007_PS2,
        toolStripMenuItemSaveAsITA_2007_PS2,
        toolStripMenuItemSaveAsAEV_2007_PS2,
        toolStripMenuItemSaveAsETS_UHD,
        toolStripMenuItemSaveAsITA_UHD,
        toolStripMenuItemSaveAsAEV_UHD,

        // subsubmenu Save As (Convert),
        toolStripMenuItemSaveConverterETS,
        toolStripMenuItemSaveConverterITA,
        toolStripMenuItemSaveConverterAEV,
        toolStripMenuItemSaveConverterETS_2007_PS2,
        toolStripMenuItemSaveConverterITA_2007_PS2,
        toolStripMenuItemSaveConverterAEV_2007_PS2,
        toolStripMenuItemSaveConverterETS_UHD,
        toolStripMenuItemSaveConverterITA_UHD,
        toolStripMenuItemSaveConverterAEV_UHD,


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
        toolStripMenuItemNewETS_2007_PS2,
        toolStripMenuItemNewITA_2007_PS2,
        toolStripMenuItemNewAEV_2007_PS2,
        toolStripMenuItemNewETS_UHD,
        toolStripMenuItemNewITA_UHD,
        toolStripMenuItemNewAEV_UHD,
        // subsubmenu Open,
        toolStripMenuItemOpenESL,
        toolStripMenuItemOpenETS_2007_PS2,
        toolStripMenuItemOpenITA_2007_PS2,
        toolStripMenuItemOpenAEV_2007_PS2,
        toolStripMenuItemOpenETS_UHD,
        toolStripMenuItemOpenITA_UHD,
        toolStripMenuItemOpenAEV_UHD,
        // subsubmenu Save,
        toolStripMenuItemSaveESL,
        toolStripMenuItemSaveDirectories,
        // subsubmenu Save As...,
        toolStripMenuItemSaveAsESL,
        // subsubmenu Clear,
        toolStripMenuItemClearESL,
        toolStripMenuItemClearETS,
        toolStripMenuItemClearITA,
        toolStripMenuItemClearAEV,

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
        toolStripMenuItemHideRoomModel,
        toolStripMenuItemHideEnemyESL,
        toolStripMenuItemHideEtcmodelETS,
        toolStripMenuItemHideItemsITA,
        toolStripMenuItemHideEventsAEV,
        toolStripMenuItemHideLateralMenu,
        toolStripMenuItemHideBottomMenu,
        toolStripMenuItemSubMenuRoom,
        toolStripMenuItemSubMenuModels,
        toolStripMenuItemSubMenuEnemy,
        toolStripMenuItemSubMenuItem,
        toolStripMenuItemSubMenuSpecial,
        toolStripMenuItemSubMenuEtcModel,
        toolStripMenuItemNodeDisplayNameInHex,
        toolStripMenuItemCameraMenu,
        toolStripMenuItemResetCamera,
        toolStripMenuItemRefresh,

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

        //save and open windows

        openFileDialogAEV,
        openFileDialogESL,
        openFileDialogETS,
        openFileDialogITA,
        saveFileDialogAEV,
        saveFileDialogConvertAEV,
        saveFileDialogConvertETS,
        saveFileDialogConvertITA,
        saveFileDialogESL,
        saveFileDialogETS,
        saveFileDialogITA,


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
        checkBoxKeepOnGround,
        checkBoxLockMoveSquareHorizontal,
        checkBoxLockMoveSquareVertical,
        checkBoxMoveRelativeCam,


        //AddNewObjForm
        AddNewObjForm,
        buttonCancel,
        buttonOK,
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
        buttonLoad,
        buttonCancel2,


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
        groupBoxTheme,
        labelThemeWarning,
        checkBoxUseDarkerGrayTheme,


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
