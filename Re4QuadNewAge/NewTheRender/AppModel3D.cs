using System;
using System.Collections.Generic;
using System.Text;
using ViewerBase;

namespace NewAgeTheRender
{
    public class AppModel3D
    {
        private GLMeshPart _mesh;

        public AppModel3D(byte[] hex) 
        {
            MeshPart meshPart = new MeshPart();
            meshPart.RefModelID = "null";
            meshPart.MeshID = "null";
            meshPart.MaterialRef = "null";

            meshPart.IndexesLength = BitConverter.ToInt32(hex, 0xC);
            int verticesAmount = BitConverter.ToInt32(hex, 0x4);
            int IndexesAmount = BitConverter.ToInt32(hex, 0x8);

            float[] vertices = new float[verticesAmount];
            uint[] indexes = new uint[IndexesAmount];

            int tempOffset = 0x10;
            for (int i = 0; i < verticesAmount; i++)
            {
                vertices[i] = BitConverter.ToSingle(hex, tempOffset);
                tempOffset += sizeof(int);
            }

            for (int i = 0; i < IndexesAmount; i++)
            {
                indexes[i] = BitConverter.ToUInt32(hex, tempOffset);
                tempOffset += sizeof(int);
            }

            meshPart.Indexes = indexes;
            meshPart.Vertex = vertices;

            _mesh = new GLMeshPart(meshPart);
        }

        public void Render() 
        {
            _mesh.Render();
        }

        public void Unload() 
        {
            _mesh.Unload();
        }
    }
}
