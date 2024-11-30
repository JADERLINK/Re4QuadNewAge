using System;
using System.Collections.Generic;
using System.Text;
using Re4QuadExtremeEditor.src.Class.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace NewAgeTheRender
{
    public static class RenderAppModel
    {
        private static readonly Vector3 boundOff = new Vector3(1f, 1f, 1f);

        private static void BoundingBox(Vector3 max, Vector3 min, RspFix fix, Vector4 Color) 
        {
            DataShader.ShaderBoundaryBox.Use();
            DataShader.ShaderBoundaryBox.SetMatrix4("RspFixRotation", fix.Rotation);
            DataShader.ShaderBoundaryBox.SetVector3("RspFixPosition", fix.Position);
            DataShader.ShaderBoundaryBox.SetVector4("mColor", Color);
            DataShader.ShaderBoundaryBox.SetVector3("MaxBoundary", max);
            DataShader.ShaderBoundaryBox.SetVector3("MinBoundary", min);
            DataShader.BoxModel.Render();
        }

        private static void BoundingBox(Vector3 max, Vector3 min, RspFix fixEntry, RspFix fixGroup, Vector4 Color)
        {
            DataShader.ShaderBoundaryBoxPlus.Use();
            DataShader.ShaderBoundaryBoxPlus.SetMatrix4("RspFix2Rotation", fixGroup.Rotation);
            DataShader.ShaderBoundaryBoxPlus.SetVector3("RspFix2Position", fixGroup.Position);
            DataShader.ShaderBoundaryBoxPlus.SetMatrix4("RspFixRotation", fixEntry.Rotation);
            DataShader.ShaderBoundaryBoxPlus.SetVector3("RspFixPosition", fixEntry.Position);
            DataShader.ShaderBoundaryBoxPlus.SetVector4("mColor", Color);
            DataShader.ShaderBoundaryBoxPlus.SetVector3("MaxBoundary", max);
            DataShader.ShaderBoundaryBoxPlus.SetVector3("MinBoundary", min);
            DataShader.BoxModel.Render();
        }

        private static void BoundingBox(BoundingBoxLimit box, RspFix fix, Vector4 Color, PolygonMode mode)
        {
            Vector3 boundOffFix = boundOff;
            Vector3 scale = fix.Scale;
            if (scale.X < 0) { boundOffFix.X *= -1; }
            if (scale.Y < 0) { boundOffFix.Y *= -1; }
            if (scale.Z < 0) { boundOffFix.Z *= -1; }

            Vector3 Max = box.UpperBoundary * scale + boundOffFix;
            Vector3 Min = box.LowerBoundary * scale - boundOffFix;

            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, mode);

            BoundingBox(Max, Min, fix, Color);
        }

        private static void BoundingBox(BoundingBoxLimit box, RspFix fixEntry, RspFix fixGroup, Vector4 Color, PolygonMode mode)
        {
            Vector3 boundOffFix = boundOff;
            Vector3 scale = fixEntry.Scale * fixGroup.Scale;
            if (scale.X < 0) { boundOffFix.X *= -1; }
            if (scale.Y < 0) { boundOffFix.Y *= -1; }
            if (scale.Z < 0) { boundOffFix.Z *= -1; }

            Vector3 Max = box.UpperBoundary * scale + boundOffFix;
            Vector3 Min = box.LowerBoundary * scale - boundOffFix;

            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, mode);

            BoundingBox(Max, Min, fixEntry, fixGroup, Color);
        }

        public static void BoundingBoxViewer(BoundingBoxLimit box, RspFix fix, Vector4 Color) 
        {
            BoundingBox(box, fix, Color, PolygonMode.Line);
        }
        public static void BoundingBoxToSelect(BoundingBoxLimit box, RspFix fix, Vector4 Color)
        {
            BoundingBox(box, fix, Color, PolygonMode.Fill);
        }

        public static void BoundingBoxViewer(BoundingBoxLimit box, RspFix fixEntry, RspFix fixGroup, Vector4 Color)
        {
            BoundingBox(box, fixEntry, fixGroup, Color, PolygonMode.Line);
        }
        public static void BoundingBoxToSelect(BoundingBoxLimit box, RspFix fixEntry, RspFix fixGroup, Vector4 Color)
        {
            BoundingBox(box, fixEntry, fixGroup, Color, PolygonMode.Fill);
        }

        private static void NoneBoundingBox(Vector3 max, Vector3 min, RspFix fix, Vector4 Color, PolygonMode mode) 
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, mode);
            BoundingBox(max, min, fix, Color);
        }
        public static void NoneBoundingBoxViewer(Vector3 max, Vector3 min, RspFix fix, Vector4 Color) 
        {
            NoneBoundingBox(max, min, fix, Color, PolygonMode.Line);
        }
        public static void NoneBoundingBoxToSelect(Vector3 max, Vector3 min, RspFix fix, Vector4 Color)
        {
            NoneBoundingBox(max, min, fix, Color, PolygonMode.Fill);
        }
        private static void NoneBoundingBox(Vector3 max, Vector3 min, RspFix fixEntry, RspFix fixGroup, Vector4 Color, PolygonMode mode)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, mode);
            BoundingBox(max, min, fixEntry, fixGroup, Color);
        }
        public static void NoneBoundingBoxViewer(Vector3 max, Vector3 min, RspFix fixEntry, RspFix fixGroup, Vector4 Color)
        {
            NoneBoundingBox(max, min, fixEntry, fixGroup, Color, PolygonMode.Line);
        }
        public static void NoneBoundingBoxToSelect(Vector3 max, Vector3 min, RspFix fixEntry, RspFix fixGroup, Vector4 Color)
        {
            NoneBoundingBox(max, min, fixEntry, fixGroup, Color, PolygonMode.Fill);
        }

        public static void TriggerZoneBoxViewer(Matrix4 TriggerZone, Vector4 Color) 
        {            
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderTriggerZoneBox.Use();
            DataShader.ShaderTriggerZoneBox.SetVector4("mColor", Color);
            DataShader.ShaderTriggerZoneBox.SetMatrix4("TriggerZone", TriggerZone);
            DataShader.BoxModel.Render();
        }

        public static void TriggerZoneBoxSolid(Matrix4 TriggerZone, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderTriggerZoneBox.Use();
            DataShader.ShaderTriggerZoneBox.SetVector4("mColor", Color);
            DataShader.ShaderTriggerZoneBox.SetMatrix4("TriggerZone", TriggerZone);
            DataShader.BoxModel.Render();
        }

        public static void TriggerZoneBoxTransparentSolid(Matrix4 TriggerZone, Vector4 frontColor, Vector4 backColor)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0f);

            GL.Enable(EnableCap.CullFace);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderTriggerZoneBox.Use();
            DataShader.ShaderTriggerZoneBox.SetMatrix4("TriggerZone", TriggerZone);

            GL.CullFace(CullFaceMode.Front);
            DataShader.ShaderTriggerZoneBox.SetVector4("mColor", frontColor);
            DataShader.BoxModel.Render();

            GL.CullFace(CullFaceMode.Back);
            DataShader.ShaderTriggerZoneBox.SetVector4("mColor", backColor);
            DataShader.BoxModel.Render();

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.AlphaTest);
        }

        public static void TriggerZoneCircleViewer(Matrix4 TriggerZone, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderTriggerZoneCircle.Use();
            DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", Color);
            DataShader.ShaderTriggerZoneCircle.SetMatrix4("TriggerZone", TriggerZone);
            DataShader.CylinderDownXModel.Render();
            DataShader.CylinderSidesModel.Render();
            DataShader.CylinderTopXModel.Render();
        }

        public static void TriggerZoneCircleTransparentSolid(Matrix4 TriggerZone, Vector4 frontColor, Vector4 backColor)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0f);

            GL.Enable(EnableCap.CullFace);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderTriggerZoneCircle.Use();
            DataShader.ShaderTriggerZoneCircle.SetMatrix4("TriggerZone", TriggerZone);

            if (TriggerZone[2,1] < 0) //invertido
            {
                GL.CullFace(CullFaceMode.Back);
                DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", frontColor);
                DataShader.CylinderFullModel.Render();

                GL.CullFace(CullFaceMode.Front);
                DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", backColor);
                DataShader.CylinderFullModel.Render();
            }
            else // renderização normal
            {
                GL.CullFace(CullFaceMode.Front);
                DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", frontColor);
                DataShader.CylinderFullModel.Render();

                GL.CullFace(CullFaceMode.Back);
                DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", backColor);
                DataShader.CylinderFullModel.Render();
            }
  
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.AlphaTest);
        }

        public static void TriggerZoneCircleSolid(Matrix4 TriggerZone, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderTriggerZoneCircle.Use();
            DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", Color);
            DataShader.ShaderTriggerZoneCircle.SetMatrix4("TriggerZone", TriggerZone);
            DataShader.CylinderFullModel.Render();
        }

        public static void PlaneZoneViewer(Matrix4 PlaneZone, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderPlaneZone.Use();
            DataShader.ShaderPlaneZone.SetVector4("mColor", Color);
            DataShader.ShaderPlaneZone.SetMatrix4("PlaneZone", PlaneZone);
            DataShader.PlaneModel.Render();
        }

        public static void PlaneZoneSolid(Matrix4 PlaneZone, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderPlaneZone.Use();
            DataShader.ShaderPlaneZone.SetVector4("mColor", Color);
            DataShader.ShaderPlaneZone.SetMatrix4("PlaneZone", PlaneZone);
            DataShader.PlaneModel.Render();
        }

        public static void ItemTrigggerRadiusViewer(Vector4 PosPlusItemRadius, Vector4 Color) 
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderItemTrigggerRadius.Use();
            DataShader.ShaderItemTrigggerRadius.SetVector4("mColor", Color);
            DataShader.ShaderItemTrigggerRadius.SetVector4("PosPlusItemRadius", PosPlusItemRadius);
            DataShader.ItemTrigggerRadiusModel.Render();

        }

        public static void RenderLitPointColor(Vector3 Position, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderLitPoint.Use();
            DataShader.ShaderLitPoint.SetVector4("mColor", Color);
            DataShader.ShaderLitPoint.SetVector3("Position", Position);
            DataShader.LIT_Point_Model.Render();
        }

        public static void RenderLitPointBorder(Vector3 Position, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderLitPoint.Use();
            DataShader.ShaderLitPoint.SetVector4("mColor", Color);
            DataShader.ShaderLitPoint.SetVector3("Position", Position);
            DataShader.LIT_Point_Model.Render();
        }
    }
}
