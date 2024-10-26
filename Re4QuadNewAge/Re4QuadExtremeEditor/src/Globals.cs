using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src
{
    /// <summary>
    /// Representa todos os status (configurações/opções) do programa;
    /// </summary>
    public static class Globals
    {

        #region Configs

        // diretorios
        public static string DirectoryXFILE = @"";
        public static string Directory2007RE4 = @"";
        public static string DirectoryPS2RE4 = @"";
        public static string DirectoryUHDRE4 = @"";
        public static string DirectoryPS4NSRE4 = @"";
        public static string DirectoryCustom1 = @"";
        public static string DirectoryCustom2 = @"";
        public static string DirectoryCustom3 = @"";

        //listagens json
        public static string FileDiretoryItemsList = Consts.DefaultItemsListFileDirectory;
        public static string FileDiretoryEtcModelsList = Consts.DefaultEtcModelsListFileDirectory;
        public static string FileDiretoryEnemiesList = Consts.DefaultEnemiesListFileDirectory;
        public static string FileDiretoryQuadCustomList = Consts.DefaultQuadCustomModelsListFileDirectory;

        // a cor do ceu
        public static Color SkyColor = Color.FromArgb(0xFF, 0x94, 0xD2, 0xFF);

        // float
        public static ConfigFrationalSymbol FrationalSymbol = ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputPeriod;
        public static int FrationalAmount = 9;

        // itens rotations options

        public static bool ItemDisableRotationAll = false;
        public static bool ItemDisableRotationIfXorYorZequalZero = false;
        public static bool ItemDisableRotationIfZisNotGreaterThanZero = true;
        public static ObjRotationOrder ItemRotationOrder = ObjRotationOrder.RotationXY;
        public static float ItemRotationCalculationMultiplier = 1;
        public static float ItemRotationCalculationDivider = 1;

        #endregion

        #region Colors

        // cores
        public static Color NodeColorEntry = Color.Black;
        public static Color NodeColorHided = Color.SlateGray;
        public static Color NodeColorESL = Color.FromArgb(192, 0, 0);
        public static Color NodeColorETS = Color.Maroon;
        public static Color NodeColorITA = Color.FromArgb(0, 0, 192);
        public static Color NodeColorAEV = Color.FromArgb(0, 192, 0);
        public static Color NodeColorEXTRAS = Color.FromArgb(0x0062707E);
        public static Color NodeColorDSE = Color.FromArgb(192, 192, 0);
        public static Color NodeColorEMI = Color.Goldenrod;
        public static Color NodeColorSAR = Color.FromArgb(0, 192, 192); 
        public static Color NodeColorEAR = Color.DodgerBlue;
        public static Color NodeColorESE = Color.Violet;
        public static Color NodeColorFSE = Color.FromArgb(161, 192, 192);
        public static Color NodeColorLIT_GROUPS = Color.FromArgb(0, 0, 0);
        public static Color NodeColorLIT_ENTRYS = Color.FromArgb(0, 0, 0);
        public static Color NodeColorQuadCustom = Color.DimGray;

        // color GL
        // cores
        public static Vector4 GL_ColorESL = Utils.ColorToVector4(Color.Red);
        public static Vector4 GL_ColorETS = Utils.ColorToVector4(Color.Maroon);
        public static Vector4 GL_ColorITA = Utils.ColorToVector4(Color.Blue);
        public static Vector4 GL_ColorAEV = Utils.ColorToVector4(Color.Lime);
        public static Vector4 GL_ColorEXTRAS = Utils.ColorToVector4(Color.SlateGray);
        public static Vector4 GL_ColorSelected = Utils.ColorToVector4(Color.Yellow);
        public static Vector4 GL_ColorItemTriggerZone = Utils.ColorToVector4(Color.Fuchsia);
        public static Vector4 GL_ColorItemTriggerZoneSelected = Utils.ColorToVector4(Color.Pink);
        public static Vector4 GL_ColorItemTrigggerRadius = Utils.ColorToVector4(Color.DeepPink);
        public static Vector4 GL_ColorItemTrigggerRadiusSelected = Utils.ColorToVector4(Color.Plum);
        public static Vector4 GL_ColorGrid = Utils.ColorToVector4(Color.DarkGray);

        public static Vector4 GL_ColorFSE = Utils.ColorToVector4(Color.LightCyan);
        public static Vector4 GL_ColorEAR = Utils.ColorToVector4(Color.DodgerBlue);
        public static Vector4 GL_ColorSAR = Utils.ColorToVector4(Color.Cyan);
        public static Vector4 GL_ColorEMI = Utils.ColorToVector4(Color.Goldenrod);
        public static Vector4 GL_ColorESE = Utils.ColorToVector4(Color.Violet);
        public static Vector4 GL_ColorQuadCustom = Utils.ColorToVector4(Color.DimGray);

        // more Colors
        public static Vector4 GL_MoreColor_T00_GeneralPurpose = Utils.ColorToVector4(Color.Green);
        public static Vector4 GL_MoreColor_T01_DoorWarp = Utils.ColorToVector4(Color.DarkOrange); //DarkOrange
        public static Vector4 GL_MoreColor_T02_CutSceneEvents = Utils.ColorToVector4(Color.Olive);
        public static Vector4 GL_MoreColor_T04_GroupedEnemyTrigger = Utils.ColorToVector4(Color.Sienna); //Thistle //DarkMagenta
        public static Vector4 GL_MoreColor_T05_Message = Utils.ColorToVector4(Color.MediumPurple);
        public static Vector4 GL_MoreColor_T08_TypeWriter = Utils.ColorToVector4(Color.Indigo);
        public static Vector4 GL_MoreColor_T0A_DamagesThePlayer = Utils.ColorToVector4(Color.LightSteelBlue); //Tomato
        public static Vector4 GL_MoreColor_T0B_FalseCollision = Utils.ColorToVector4(Color.Crimson); //Crimson
        public static Vector4 GL_MoreColor_T0D_FieldInfo = Utils.ColorToVector4(Color.DarkSeaGreen);
        public static Vector4 GL_MoreColor_T0E_Crouch = Utils.ColorToVector4(Color.BlanchedAlmond); //DarkSlateGray //DarkSalmon
        public static Vector4 GL_MoreColor_T10_FixedLadderClimbUp = Utils.ColorToVector4(Color.SteelBlue); //Chocolate
        public static Vector4 GL_MoreColor_T11_ItemDependentEvents = Utils.ColorToVector4(Color.DarkViolet);//DarkViolet //BlueViolet //DarkSlateBlue //Goldenrod //BlanchedAlmond
        public static Vector4 GL_MoreColor_T12_AshleyHideCommand = Utils.ColorToVector4(Color.Lavender);
        public static Vector4 GL_MoreColor_T13_LocalTeleportation = Utils.ColorToVector4(Color.DarkSalmon); //Wheat //DarkViolet
        public static Vector4 GL_MoreColor_T14_UsedForElevators = Utils.ColorToVector4(Color.YellowGreen);
        public static Vector4 GL_MoreColor_T15_AdaGrappleGun = Utils.ColorToVector4(Color.Navy);

        #endregion

        // backup da class config
        public static Re4QuadExtremeEditor.src.JSON.Configs BackupConfigs = null;


        #region Menu options
        // se pode renderizar o modelo 3d da room
        public static bool RenderRoom = true;

        public static bool RenderEnemyESL = true;
        public static bool RenderEtcmodelETS = true;
        public static bool RenderItemsITA = true;
        public static bool RenderEventsAEV = true;
        public static bool RenderFileFSE = true;
        public static bool RenderFileSAR = true;
        public static bool RenderFileEAR = true;
        public static bool RenderFileEMI = true;
        public static bool RenderFileESE = true;
        public static bool RenderFileQuadCustom = true;
        public static bool RenderFileLIT = true;

        //enemy renders
        public static bool RenderDisabledEnemy = true;
        public static bool RenderDontShowOnlyDefinedRoom = true;
        public static ushort RenderEnemyFromDefinedRoom = 0x0000;
        public static bool AutoDefinedRoom = false;

        // items render
        public static bool RenderItemTriggerZone = true;
        public static bool RenderItemPositionAtAssociatedObjectLocation = false;
        public static bool RenderItemTriggerRadius = true;

        //special render
        public static bool RenderSpecialTriggerZone = true;
        public static bool RenderExtraObjs = true;
        public static bool UseMoreSpecialColors = false;
        public static bool RenderExtraWarpDoor = true;
        public static bool HideExtraExceptWarpDoor = false;

        //QuadCustom
        public static bool UseMoreQuadCustomColors = false;

        //Etcmodel
        public static bool RenderEtcmodelUsingScale = false;


        public static bool TreeNodeRenderHexValues = false;

        // opção que muda no propetyGrid
        public static bool PropertyGridUseHexFloat = false;

        //search
        public static bool SearchFilterMode = false;


        #endregion


        #region patch Files, diretorios dos arquivos

        public static string FilePathESL = null;
        public static string FilePathETS = null;
        public static string FilePathITA = null;
        public static string FilePathAEV = null;
        public static string FilePathDSE = null;
        public static string FilePathFSE = null;
        public static string FilePathSAR = null;
        public static string FilePathEAR = null;
        public static string FilePathEMI = null;
        public static string FilePathESE = null;
        public static string FilePathQuadCustom = null;
        public static string FilePathEFFBLOB = null;
        public static string FilePathLIT = null;
        #endregion

        // Render Options
        public static int FOV = 60; // field of view (in degrees)

        //opção de lista de inimigos extra sets.
        public static bool CreateEnemyExtraSegmentList = true;

        //cam grid
        public static bool CamGridEnable = false;
        public static int CamGridvalue = 100;


        // treenode fonts
        public static Font TreeNodeFontText = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
        public static Font TreeNodeFontHex = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold);

        //OpenGLVersion
        public static string OpenGLVersion = "";
    }
}
