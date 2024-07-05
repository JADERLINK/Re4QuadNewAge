using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMD_API
{
    // Code Version: B.1.0.1.2

    public class PMD
    {
        public string[] NodeGroupNames;

        public string[] SkeletonBoneNames;
        public int[] SkeletonBoneParents;
        public float[][] SkeletonBoneData;
        public Dictionary<string, int> ObjRefBones;

        public PMDnode[] Nodes;
        public PMDmaterial[] Materials;
    }

    public class PMDmaterial 
    {
        public float[] TextureData;
        public int TextureEnable;
        public string TextureName;
    }

    public class PMDnode 
    {
        public PMDnodeBone[] Bones;
        public PMDmesh[] Meshs;
        public int SkeletonIndex;
        public int ObjNodeId;
    }

    public class PMDmesh 
    {
        public PMDvertex[] Vertexs;
        public ushort[] Orders;
        public int TextureIndex;
    }


    public class PMDvertex
    {
        public float x;
        public float y;
        public float z;
        public float w0;
        public float w1;
        public float i0;
        public float i1;
        public float nx;
        public float ny;
        public float nz;
        public float tu;
        public float tv;
        public float r;
        public float g;
        public float b;
        public float a;
    }

    public class PMDnodeBone
    {
        public int boneId;
        public float[] unknown;
        public float x;
        public float y;
        public float z;
        public float w;
    }
}