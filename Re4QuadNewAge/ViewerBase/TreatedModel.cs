using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace ViewerBase
{
    public class TreatedModel
    {
        //Id exclusivo do modelo, tem que ser diferente de todos os outros
        public string ModelID { get; private set; }

        // id para SMD para caso tiver, caso não é -1
        public int BIN_ID { get; set; } = -1;

        // lista de meshes desse modelo/objeto
        public List<MeshPart> Meshes { get; set; }

        //coordenadas dos limites do modelo
        public Vector3 MinBoundary { get; set; }
        public Vector3 MaxBoundary { get; set; }
        public Vector3 CenterBoundary { get; set; }

        public TreatedModel(string ModelID) 
        {
            this.ModelID = ModelID;
            Meshes = new List<MeshPart>();
        }
    }

    public class MeshPart 
    {
        public string RefModelID { get; set; } // ID do modelo que usa essa mesh

        //Id exclusivo da Mesh, tem que ser diferente de todos os outros
        public string MeshID { get; set; }

        // nome do material que faz referencia
        public string MaterialRef { get; set; }

        // conjunto de vertices, normals, UVs, e colors
        public float[] Vertex { get; set; }

        // conjunto de Índices
        public uint[] Indexes { get; set; }

        // quantidade de Índices
        public int IndexesLength { get; set; }

        //coordenadas dos limites da mesh
        public Vector3 MinBoundary { get; set; }
        public Vector3 MaxBoundary { get; set; }
        public Vector3 CenterBoundary { get; set; }
    }

    public class Material
    {
        //nome do material, o mesmo nome usado em "MeshPart.MaterialRef"
        public string MaterialName { get; private set; }

        public string DiffuseMatTex { get; set; }

        public string AlphaMatTex { get; set; }

        public bool AsAlphaTex { get; set; }

        public Vector4 MatColor { get; set; }

        public Material(string MaterialName) 
        {
            this.MaterialName = MaterialName;
            DiffuseMatTex = "";
            AlphaMatTex = "";
            AsAlphaTex = false;
            MatColor = Vector4.One;
        }
    }

    public class MatTex // vamos dizer que representa uma entry do UHD TPL
    {
        public string MatTexName { get; set; } // o nome desse objeto
        public string TextureName { get; set; } // o nome verdadeiro da textura

        public MatTex(string MatTexName) 
        {
            this.MatTexName = MatTexName;
            TextureName = "";
        }
    }

    public class MatLinker // classe designada a fazer o vinculo entre Material e MatTex
    {
        public string ModelID { get; set; } //nome do modelo que vai usar o vinculo
        public string MatTexGroupName { get; set; } // nome do grupo que contem os MatTex

        public MatLinker(string ModelID, string MatTexGroupName) 
        {
            this.ModelID = ModelID;
            this.MatTexGroupName = MatTexGroupName;
        }

        public MatLinker() 
        {
            ModelID = "";
            MatTexGroupName = "";
        }
    }

    public class MatTexGroup 
    {
        public string MatTexGroupName { get; private set; } // o nome desse grupo
        public Dictionary<string, MatTex> MatTexDic { get; set; }

        public MatTexGroup(string MatTexGroupName) 
        {
            this.MatTexGroupName = MatTexGroupName;
            MatTexDic = new Dictionary<string, MatTex>();
        }
    }

    public class MaterialGroup
    {
        public string MaterialGroupName { get; set; } // o nome desse grupo
        public Dictionary<string, Material> MaterialsDic { get; set; }

        public MaterialGroup(string MaterialGroupName)
        {
            this.MaterialGroupName = MaterialGroupName;
            MaterialsDic = new Dictionary<string, Material>();
        }
    }
}
