using System;
using System.Collections.Generic;
using System.Text;
using Re4QuadExtremeEditor.src.Class.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace NewAgeTheRender
{
    public static class Grid
    {
        public static void RenderViewer(float PosY, int SpaceBetween, Vector4 Color)
        {
            GL.Disable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.FrontAndBack);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            DataShader.ShaderGrid.Use();
            DataShader.ShaderGrid.SetVector4("mColor", Color);
            DataShader.ShaderGrid.SetFloat("PositionY", PosY);
            DataShader.ShaderGrid.SetInt("SpaceBetween", SpaceBetween);

            DataShader.ShaderGrid.SetInt("Flip", 0);
            DataShader.ShaderGrid.SetMatrix4("RspFixRotation", Matrix4.CreateRotationY(0));
            DataShader.GridLineModel.Render();
            DataShader.ShaderGrid.SetMatrix4("RspFixRotation", Matrix4.CreateRotationY(MathHelper.DegreesToRadians(90)));
            DataShader.GridLineModel.Render();

            DataShader.ShaderGrid.SetInt("Flip", 1);
            DataShader.ShaderGrid.SetMatrix4("RspFixRotation", Matrix4.CreateRotationY(0));
            DataShader.GridLineModel.Render();
            DataShader.ShaderGrid.SetMatrix4("RspFixRotation", Matrix4.CreateRotationY(MathHelper.DegreesToRadians(90)));
            DataShader.GridLineModel.Render();
        }

        public static byte[] CreateBinaryModel() 
        {
            List<(float x, float y, float z)> vert = new List<(float x, float y, float z)>();
            List<uint> indices = new List<uint>();

            int numberLines = 3277;
            uint indexPos = 0;
            float spaceBetweenTheLines = 1f;
            float endPoint = numberLines -1;
            float positionX = 0;
            for (int i = 0; i < numberLines; i++)
            {
                vert.Add((positionX, 0, -endPoint));
                vert.Add((positionX, 0, endPoint));

                positionX += spaceBetweenTheLines;

                indices.Add(indexPos);
                indices.Add(indexPos + 1);
                indices.Add(indexPos + 1);

                indexPos += 2;
            }

            int IndexesLength = indices.Count;

            int tempIndexDiv = indices.Count / (16 / sizeof(uint));
            int tempIndexRest = indices.Count % (16 / sizeof(uint));
            tempIndexDiv += tempIndexRest != 0 ? 1 : 0;
            int FinalIndexLength = tempIndexDiv * (16 / sizeof(uint));

            float[] vertices = new float[vert.Count * 12];

            int vOffset = 0;
            for (int iv = 0; iv < vert.Count; iv++)
            {
                vertices[vOffset + 0] = vert[iv].x;
                vertices[vOffset + 1] = vert[iv].y;
                vertices[vOffset + 2] = vert[iv].z;

                vertices[vOffset + 3] = 0;
                vertices[vOffset + 4] = 1;
                vertices[vOffset + 5] = 0;

                vertices[vOffset + 6] = 0;
                vertices[vOffset + 7] = 0;

                vertices[vOffset + 8] = 1;
                vertices[vOffset + 9] = 1;
                vertices[vOffset + 10] = 1;
                vertices[vOffset + 11] = 1;

                vOffset += 12;
            }

            byte[] res = new byte[0x10 + (vertices.Length * sizeof(float)) + (FinalIndexLength * sizeof(uint))];

            BitConverter.GetBytes(0x21443321).CopyTo(res, 0x00); //magic
            BitConverter.GetBytes(vertices.Length).CopyTo(res, 0x04);
            BitConverter.GetBytes(FinalIndexLength).CopyTo(res, 0x08);
            BitConverter.GetBytes(IndexesLength).CopyTo(res, 0x0C);

            int offset = 0x10;
            for (int i = 0; i < vertices.Length; i++)
            {
                BitConverter.GetBytes(vertices[i]).CopyTo(res, offset);
                offset += sizeof(float);
            }

            for (int i = 0; i < IndexesLength; i++)
            {
                BitConverter.GetBytes(indices[i]).CopyTo(res, offset);
                offset += sizeof(float);
            }

            return res;
        }

    }
}
