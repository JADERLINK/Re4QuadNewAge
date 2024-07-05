using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ViewerBase
{
    public class RenderedObjModel
    {
        //Id exclusivo do modelo, tem que ser diferente de todos os outros
        public string ModelID { get; set; }

        // id para SMD para caso tiver, caso não é -1
        public int BIN_ID { get; set; } = -1;

        // lista de meshes desse modelo/objeto
        public List<string> MeshNames { get; set; }

        //coordenadas dos limites do modelo
        public Vector3 MinBoundary { get; set; }
        public Vector3 MaxBoundary { get; set; }
        public Vector3 CenterBoundary { get; set; }
    }

  
}
