using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace ViewerBase
{
    public class GLMeshPart
    {
        //etapa 1;
        public string RefModelID { get; private set; } // ID do modelo que usa essa mesh

        //Id exclusivo da Mesh, tem que ser diferente de todos os outros
        public string MeshID { get; private set; }

        // nome do material que faz referencia
        public string MaterialRef { get; private set; }

        // conjunto de vertices, normals, UVs, e colors
        //public float[] Vertex { get; private set; }

        // conjunto de Índices
        //public uint[] Indexes { get; private set; }

        // quantidade de Índices
        public int IndexesLength { get; private set; }

        //coordenadas dos limites da mesh
        public Vector3 MinBoundary { get; private set; }
        public Vector3 MaxBoundary { get; private set; }
        public Vector3 CenterBoundary { get; private set; }
        //-----------------

        // etapa 2;
        private int vertexBufferObject; // vertex  VBO
        private int elementBufferObject; // Indexes  EBO
        private int vertexArrayObject; // VAO (conjunto dos dois)

        //----------
        public GLMeshPart(MeshPart mesh)
        {
            //copy
            RefModelID = mesh.RefModelID;
            MeshID = mesh.MeshID;
            MaterialRef = mesh.MaterialRef;
            //Vertex = mesh.Vertex;
            //Indexes = mesh.Indexes;
            IndexesLength = mesh.IndexesLength;
            MinBoundary = mesh.MinBoundary;
            MaxBoundary = mesh.MaxBoundary;
            CenterBoundary = mesh.CenterBoundary;

            //openGL

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.Vertex.Length * sizeof(float), mesh.Vertex, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.Indexes.Length * sizeof(uint), mesh.Indexes, BufferUsageHint.StaticDraw);

            var positionLocation = (int)AttribLocation.aPosition;
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 0);

            var normalLocation = (int)AttribLocation.aNormal;
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 3 * sizeof(float));

            var texCoordLocation = (int)AttribLocation.aTexCoord;
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 12 * sizeof(float), 6 * sizeof(float));

            var colorLocation = (int)AttribLocation.aColor;
            GL.EnableVertexAttribArray(colorLocation);
            GL.VertexAttribPointer(colorLocation, 4, VertexAttribPointerType.Float, false, 12 * sizeof(float), 8 * sizeof(float));

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            IsLoaded = true;
        }

        public void Render()
        {
            if (IsLoaded)
            {
                GL.BindVertexArray(vertexArrayObject);
                GL.DrawElements(PrimitiveType.Triangles, IndexesLength, DrawElementsType.UnsignedInt, 0);
            }
        
        }

        private bool IsLoaded = false;

        public void Unload() 
        {
            if (IsLoaded)
            {
                IsLoaded = false;
                GL.BindVertexArray(vertexArrayObject);
                GL.DeleteBuffer(vertexArrayObject);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                GL.DeleteBuffer(vertexBufferObject);
                GL.DeleteBuffer(elementBufferObject);
                //----
                vertexBufferObject = -1;
                elementBufferObject = -1;
                vertexArrayObject = -1;
            }
        }

    }

}
