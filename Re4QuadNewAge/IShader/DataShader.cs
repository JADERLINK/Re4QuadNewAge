using System;
using System.Collections.Generic;
using System.Text;
using NewAgeTheRender;

namespace Re4QuadExtremeEditor.src.Class.Shaders
{
    public static class DataShader
    {
        //shaders
        public static IShader ShaderObjModel = null;
        public static IShader ShaderObjModelPlus = null;
        public static IShader ShaderBoundaryBox = null;
        public static IShader ShaderBoundaryBoxPlus = null;
        public static IShader ShaderTriggerZoneBox = null;
        public static IShader ShaderTriggerZoneCircle = null;
        public static IShader ShaderPlaneZone = null;
        public static IShader ShaderItemTrigggerRadius = null;
        public static IShader ShaderRoom = null;
        public static IShader ShaderRoomSelectMode = null;
        public static IShader ShaderGrid = null;
        public static IShader ShaderLitPoint = null;

        // models 3d do programa
        public static AppModel3D BoxModel = null;
        public static AppModel3D PlaneModel = null;
        public static AppModel3D CircleModel = null;
        public static AppModel3D ItemTrigggerRadiusModel = null;
        public static AppModel3D CylinderFullModel = null;
        public static AppModel3D CylinderDownModel = null;
        public static AppModel3D CylinderDownXModel = null;
        public static AppModel3D CylinderSidesModel = null;
        public static AppModel3D CylinderTopModel = null;
        public static AppModel3D CylinderTopXModel = null;
        public static AppModel3D GridLineModel = null;
        public static AppModel3D LIT_Point_Model = null;

        // texturas padrões
        public static ViewerBase.TextureRef WhiteTexture = null;
        public static ViewerBase.TextureRef NoTexture = null;
        public static ViewerBase.TextureRef TransparentTexture = null;
        public static ViewerBase.TextureRef BlackTexture = null;


        /// <summary>
        /// carrega os shader ao iniciar o programa
        /// </summary>
        public static void StartLoad()
        {
            WhiteTexture = new ViewerBase.TextureRef(Properties.Resources.WhiteTexture);
            NoTexture = new ViewerBase.TextureRef(Properties.Resources.NoTexture);
            TransparentTexture = new ViewerBase.TextureRef(Properties.Resources.Transparent);
            BlackTexture = new ViewerBase.TextureRef(Properties.Resources.BlackTexture);

            BoxModel = new AppModel3D(Properties.Resources.Box);
            PlaneModel = new AppModel3D(Properties.Resources.Plane);
            CircleModel = new AppModel3D(Properties.Resources.Circle);
            ItemTrigggerRadiusModel = new AppModel3D(Properties.Resources.ItemTrigggerRadius);
            CylinderFullModel = new AppModel3D(Properties.Resources.CylinderFull);
            CylinderDownModel = new AppModel3D(Properties.Resources.CylinderDown);
            CylinderDownXModel = new AppModel3D(Properties.Resources.CylinderDownX);
            CylinderSidesModel = new AppModel3D(Properties.Resources.CylinderSides);
            CylinderTopModel = new AppModel3D(Properties.Resources.CylinderTop);
            CylinderTopXModel = new AppModel3D(Properties.Resources.CylinderTopX);
            LIT_Point_Model = new AppModel3D(Properties.Resources.LIT_Point);
            GridLineModel = new AppModel3D(Grid.CreateBinaryModel());

            ShaderRoom = new Shader(Encoding.UTF8.GetString(Properties.Resources.RoomShaderVert), Encoding.UTF8.GetString(Properties.Resources.RoomShaderFrag));
            ShaderRoom.Use();
            ShaderRoom.SetInt("texture0", 0);
            ShaderRoom.SetInt("texture1", 1);

            ShaderObjModel = new Shader(Encoding.UTF8.GetString(Properties.Resources.ObjModelShaderVert), Encoding.UTF8.GetString(Properties.Resources.ObjModelShaderFrag));
            ShaderObjModel.Use();
            ShaderObjModel.SetInt("texture0", 0);
            ShaderObjModel.SetInt("texture1", 1);

            ShaderObjModelPlus = new Shader(Encoding.UTF8.GetString(Properties.Resources.ObjModelShaderPlusVert), Encoding.UTF8.GetString(Properties.Resources.ObjModelShaderFrag));
            ShaderObjModelPlus.Use();
            ShaderObjModelPlus.SetInt("texture0", 0);
            ShaderObjModelPlus.SetInt("texture1", 1);

            ShaderRoomSelectMode = new Shader(Encoding.UTF8.GetString(Properties.Resources.RoomSelectModeShaderVert), Encoding.UTF8.GetString(Properties.Resources.RoomSelectModeShaderFrag));
            ShaderRoomSelectMode.Use();

            ShaderBoundaryBox = new Shader(Encoding.UTF8.GetString(Properties.Resources.BoundaryBoxShaderVert), Encoding.UTF8.GetString(Properties.Resources.BoundaryBoxShaderFrag));
            ShaderBoundaryBox.Use();

            ShaderBoundaryBoxPlus = new Shader(Encoding.UTF8.GetString(Properties.Resources.BoundaryBoxShaderPlusVert), Encoding.UTF8.GetString(Properties.Resources.BoundaryBoxShaderFrag));
            ShaderBoundaryBoxPlus.Use();

            ShaderTriggerZoneBox = new Shader(Encoding.UTF8.GetString(Properties.Resources.TriggerZoneShaderVert), Encoding.UTF8.GetString(Properties.Resources.TriggerZoneShaderFrag));
            ShaderTriggerZoneBox.Use();

            ShaderTriggerZoneCircle = new Shader(Encoding.UTF8.GetString(Properties.Resources.CircleTriggerZoneShaderVert), Encoding.UTF8.GetString(Properties.Resources.CircleTriggerZoneShaderFrag));
            ShaderTriggerZoneCircle.Use();

            ShaderPlaneZone = new Shader(Encoding.UTF8.GetString(Properties.Resources.PlaneZoneShaderVert), Encoding.UTF8.GetString(Properties.Resources.PlaneZoneShaderFrag));
            ShaderPlaneZone.Use();

            ShaderItemTrigggerRadius = new Shader(Encoding.UTF8.GetString(Properties.Resources.ItemTrigggerRadiusShaderVert), Encoding.UTF8.GetString(Properties.Resources.ItemTrigggerRadiusShaderFrag));
            ShaderItemTrigggerRadius.Use();

            ShaderLitPoint = new Shader(Encoding.UTF8.GetString(Properties.Resources.LitPointShaderVert), Encoding.UTF8.GetString(Properties.Resources.LitPointShaderFrag));
            ShaderLitPoint.Use();

            ShaderGrid = new Shader(Encoding.UTF8.GetString(Properties.Resources.GridShaderVert), Encoding.UTF8.GetString(Properties.Resources.GridShaderFrag));
            ShaderGrid.Use();
        }

        public static void EndUnload() 
        {
            WhiteTexture?.Unload();
            BlackTexture?.Unload();
            NoTexture?.Unload();
            TransparentTexture?.Unload();

            BoxModel?.Unload();
            PlaneModel?.Unload();
            CircleModel?.Unload();
            ItemTrigggerRadiusModel?.Unload();
            CylinderFullModel?.Unload();
            CylinderDownModel?.Unload();
            CylinderDownXModel?.Unload();
            CylinderSidesModel?.Unload();
            CylinderTopModel?.Unload();
            CylinderTopXModel?.Unload();
            GridLineModel?.Unload();
        }
    
    }
}
