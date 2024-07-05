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

        public static void BoundingBoxViewer(BoundingBoxLimit box, RspFix fix, Vector4 Color) 
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
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderBoundaryBox.Use();
            DataShader.ShaderBoundaryBox.SetMatrix4("RspFixRotation", fix.Rotation);
            DataShader.ShaderBoundaryBox.SetVector3("RspFixPosition", fix.Position);
            DataShader.ShaderBoundaryBox.SetVector4("mColor", Color);
            DataShader.ShaderBoundaryBox.SetVector3("MaxBoundary", Max);
            DataShader.ShaderBoundaryBox.SetVector3("MinBoundary", Min);
            DataShader.BoxModel.Render();
        }

        public static void NoneBoundingBoxViewer(Vector3 max, Vector3 min, RspFix fix, Vector4 Color) 
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderBoundaryBox.Use();
            DataShader.ShaderBoundaryBox.SetMatrix4("RspFixRotation", fix.Rotation);
            DataShader.ShaderBoundaryBox.SetVector3("RspFixPosition", fix.Position);
            DataShader.ShaderBoundaryBox.SetVector4("mColor", Color);
            DataShader.ShaderBoundaryBox.SetVector3("MaxBoundary", max);
            DataShader.ShaderBoundaryBox.SetVector3("MinBoundary", min);
            DataShader.BoxModel.Render();
        }

        public static void BoundingBoxToSelect(BoundingBoxLimit box, RspFix fix, Vector4 Color)
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
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderBoundaryBox.Use();
            DataShader.ShaderBoundaryBox.SetMatrix4("RspFixRotation", fix.Rotation);
            DataShader.ShaderBoundaryBox.SetVector3("RspFixPosition", fix.Position);
            DataShader.ShaderBoundaryBox.SetVector4("mColor", Color);
            DataShader.ShaderBoundaryBox.SetVector3("MaxBoundary", Max);
            DataShader.ShaderBoundaryBox.SetVector3("MinBoundary", Min);
            DataShader.BoxModel.Render();
        }

        public static void NoneBoundingBoxToSelect(Vector3 max, Vector3 min, RspFix fix, Vector4 Color)
        {           
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderBoundaryBox.Use();
            DataShader.ShaderBoundaryBox.SetMatrix4("RspFixRotation", fix.Rotation);
            DataShader.ShaderBoundaryBox.SetVector3("RspFixPosition", fix.Position);
            DataShader.ShaderBoundaryBox.SetVector4("mColor", Color);
            DataShader.ShaderBoundaryBox.SetVector3("MaxBoundary", max);
            DataShader.ShaderBoundaryBox.SetVector3("MinBoundary", min);
            DataShader.BoxModel.Render();
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

        public static void TriggerZoneBoxTransparentSolid(Matrix4 TriggerZone, Vector4 Color)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0f);

            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderTriggerZoneBox.Use();
            DataShader.ShaderTriggerZoneBox.SetVector4("mColor", Color);
            DataShader.ShaderTriggerZoneBox.SetMatrix4("TriggerZone", TriggerZone);
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

        public static void TriggerZoneCircleTransparentSolid(Matrix4 TriggerZone, Vector4 Color)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0f);

            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            DataShader.ShaderTriggerZoneCircle.Use();
            DataShader.ShaderTriggerZoneCircle.SetVector4("mColor", Color);
            DataShader.ShaderTriggerZoneCircle.SetMatrix4("TriggerZone", TriggerZone);
            DataShader.CylinderDownModel.Render();
            DataShader.CylinderSidesModel.Render();
            DataShader.CylinderTopModel.Render();

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

    }
}
