using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PMD_API
{
    // Code Version: B.1.0.1.2
    public static class PmdDecoder
    {
        public static PMD GetPMD(string FilePath) 
        {
            return GetPMD(new FileStream(FilePath, FileMode.Open));
        }

        public static PMD GetPMD(Stream SRC)
        {

            //headerText
            SRC.Read(new byte[58], 0, 58);

            byte[] temp = new byte[4];
            SRC.Read(temp, 0, 4); // Quantidade de nome de Bones
            uint SkeletonBoneNames_Length = BitConverter.ToUInt32(temp, 0);

            string[] SkeletonBoneNames = new string[SkeletonBoneNames_Length];

            for (int i = 0; i < SkeletonBoneNames_Length; i++)
            {
                string name;
                int namelen;

                temp = new byte[4];
                SRC.Read(temp, 0, 4); //aqui contem o valor que representa o tamanho da string
                namelen = BitConverter.ToInt32(temp, 0);

                temp = new byte[namelen];
                SRC.Read(temp, 0, namelen); // nome
                name = Encoding.ASCII.GetString(temp, 0, namelen);

                temp = new byte[4];
                SRC.Read(temp, 0, 4);// # conteudo int, representa o id do nome (vem depois do nome)
                SkeletonBoneNames[BitConverter.ToUInt32(temp, 0)] = name;
            }

            temp = new byte[4];
            SRC.Read(temp, 0, 4);// # tamanho da quantidade de nome de NodeGroup
            uint NodeGroupNames_Length = BitConverter.ToUInt32(temp, 0);

            string[] NodeGroupNames = new string[NodeGroupNames_Length];

            for (int i = 0; i < NodeGroupNames_Length; i++) // o mesmo que o de cima so que para as NodeGroup
            {
                string name;
                int namelen;

                temp = new byte[4];
                SRC.Read(temp, 0, 4);
                namelen = BitConverter.ToInt32(temp, 0);

                temp = new byte[namelen];
                SRC.Read(temp, 0, namelen); // nome
                name = Encoding.ASCII.GetString(temp, 0, namelen);

                temp = new byte[4];
                SRC.Read(temp, 0, 4);//
                NodeGroupNames[BitConverter.ToUInt32(temp, 0)] = name;
            }

            temp = new byte[4];
            SRC.Read(temp, 0, 4); //    # zeros
            SRC.Read(temp, 0, 4); // quantidade de skeletons/bones
            uint skeleton_length = BitConverter.ToUInt32(temp, 0); // quantidade de skeletons/bones

            float[][] SkeletonBoneData = new float[skeleton_length][];
            int[] SkeletonBoneParents = new int[skeleton_length];

            for (int i = 0; i < skeleton_length; i++)
            {
                temp = new byte[4];
                SRC.Read(temp, 0, 4);
                int parent = BitConverter.ToInt32(temp, 0);

                SkeletonBoneData[i] = new float[26];

                for (int iv = 0; iv < 26; iv++)
                {
                    temp = new byte[4];
                    SRC.Read(temp, 0, 4);
                    SkeletonBoneData[i][iv] = BitConverter.ToSingle(temp, 0);

                }

                SkeletonBoneParents[i] = parent;

                // legenda
                //  Skeleton[skeleton][floats]
                //  Skeleton[i][7] = x
                //  Skeleton[i][8] = y
                //  Skeleton[i][9] = z
            }

            temp = new byte[4];
            SRC.Read(temp, 0, 4);
            uint objNodes_Length = BitConverter.ToUInt32(temp, 0);

            PMDnode[] Nodes = new PMDnode[objNodes_Length];

            Dictionary<string, int> ObjRefBones = new Dictionary<string, int>();

            for (int obj_i = 0; obj_i < objNodes_Length; obj_i++)
            {
                temp = new byte[4];
                SRC.Read(temp, 0, 4);
                int Skeleton_i = BitConverter.ToInt32(temp, 0);

                SRC.Read(new byte[32], 0, 32); //     # all zeros

                temp = new byte[4];
                SRC.Read(temp, 0, 4);
                uint mesh_lenght = BitConverter.ToUInt32(temp, 0); // seria a quantidade de materials

                Nodes[obj_i] = new PMDnode();
                Nodes[obj_i].Meshs = new PMDmesh[mesh_lenght];

                for (int im = 0; im < mesh_lenght; im++)
                {
                    Nodes[obj_i].Meshs[im] = new PMDmesh();
                    temp = new byte[4];
                    SRC.Read(temp, 0, 4);
                    Nodes[obj_i].Meshs[im].TextureIndex = BitConverter.ToInt32(temp, 0);
                }

                temp = new byte[4];
                SRC.Read(temp, 0, 4); // # zeros
                SRC.Read(temp, 0, 4); // # number of meshes again 

                Nodes[obj_i].SkeletonIndex = Skeleton_i;
                Nodes[obj_i].ObjNodeId = obj_i;

                for (int im = 0; im < mesh_lenght; im++)
                {
                    temp = new byte[4];
                    SRC.Read(temp, 0, 4); // # 40 00 00 00

                    if (BitConverter.ToInt32(temp, 0) != 64)
                    {
                        continue;
                    }

                    temp = new byte[4];
                    SRC.Read(temp, 0, 4);
                    uint indexAmount = BitConverter.ToUInt32(temp, 0);

                    ushort[] Order = new ushort[indexAmount];

                    for (int i = 0; i < indexAmount; i++)
                    {
                        temp = new byte[2];
                        SRC.Read(temp, 0, 2);
                        ushort id = BitConverter.ToUInt16(temp, 0);
                        Order[i] = id;
                    }

                    temp = new byte[4];
                    SRC.Read(temp, 0, 4);
                    uint vertexAmount = BitConverter.ToUInt32(temp, 0);

                    PMDvertex[] meshVertices = new PMDvertex[vertexAmount];

                    for (int i = 0; i < vertexAmount; i++)
                    {
                        byte[] vertices = new byte[64];
                        SRC.Read(vertices, 0, 64); //# create array of vertice data

                        PMDvertex v = new PMDvertex();
                        v.x = BitConverter.ToSingle(vertices, 0);
                        v.y = BitConverter.ToSingle(vertices, 4);
                        v.z = BitConverter.ToSingle(vertices, 8);
                        v.w0 = BitConverter.ToSingle(vertices, 12);
                        v.w1 = BitConverter.ToSingle(vertices, 16);
                        v.i0 = BitConverter.ToSingle(vertices, 20);
                        v.i1 = BitConverter.ToSingle(vertices, 24);
                        v.nx = BitConverter.ToSingle(vertices, 28);
                        v.ny = BitConverter.ToSingle(vertices, 32);
                        v.nz = BitConverter.ToSingle(vertices, 36);
                        v.tu = BitConverter.ToSingle(vertices, 40);
                        v.tv = BitConverter.ToSingle(vertices, 44);
                        v.r = BitConverter.ToSingle(vertices, 48);
                        v.g = BitConverter.ToSingle(vertices, 52);
                        v.b = BitConverter.ToSingle(vertices, 56);
                        v.a = BitConverter.ToSingle(vertices, 60);
                        meshVertices[i] = v;
                    }
                    Nodes[obj_i].Meshs[im].Vertexs = meshVertices;
                    Nodes[obj_i].Meshs[im].Orders = Order;

                }


                temp = new byte[4];
                SRC.Read(temp, 0, 4);
                uint num_bones = BitConverter.ToUInt32(temp, 0);

                Nodes[obj_i].Bones = new PMDnodeBone[num_bones];
                for (int i = 0; i < num_bones; i++)
                {
                    PMDnodeBone b = new PMDnodeBone();

                    temp = new byte[4];
                    SRC.Read(temp, 0, 4);
                    int boneid = BitConverter.ToInt32(temp, 0);
                    b.boneId = boneid;

                    temp = new byte[52];
                    SRC.Read(temp, 0, 52); // # unused

                    b.unknown = new float[13];
                    int offset = 0;
                    for (int ib = 0; ib < 13; ib++)
                    {
                        b.unknown[ib] = BitConverter.ToSingle(temp, offset);
                        offset += 4;
                    }

                    byte[] bone = new byte[16];
                    SRC.Read(bone, 0, 16); //   # bone's xyz, but redundant

                    b.x = BitConverter.ToSingle(bone, 0);
                    b.y = BitConverter.ToSingle(bone, 4);
                    b.z = BitConverter.ToSingle(bone, 8);
                    b.w = BitConverter.ToSingle(bone, 12);
                    Nodes[obj_i].Bones[i] = b;

                    ObjRefBones.Add(obj_i + "_" + (3 * i), boneid);
                }

            }

            temp = new byte[4];
            SRC.Read(temp, 0, 4); // # number of textures
            uint numberOfTextures = BitConverter.ToUInt32(temp, 0);
            PMDmaterial[] Materials = new PMDmaterial[numberOfTextures];

            for (int i = 0; i < numberOfTextures; i++)
            {
                Materials[i] = new PMDmaterial();

                temp = new byte[72];
                SRC.Read(temp, 0, 72); // # texture data + other

                Materials[i].TextureData = new float[17];
                int offset = 0;
                for (int f = 0; f < 17; f++)
                {
                    Materials[i].TextureData[f] = BitConverter.ToSingle(temp, offset);
                    offset += 4;
                }
                Materials[i].TextureEnable = BitConverter.ToInt32(temp, offset);


                temp = new byte[4];
                SRC.Read(temp, 0, 4); //  # filename size
                int texNameLenght = BitConverter.ToInt32(temp, 0);
                temp = new byte[texNameLenght];
                SRC.Read(temp, 0, texNameLenght);
                Materials[i].TextureName = Encoding.ASCII.GetString(temp, 0, texNameLenght);
            }

            SRC.Close();

            PMD pmd = new PMD();
            pmd.NodeGroupNames = NodeGroupNames;
            pmd.SkeletonBoneNames = SkeletonBoneNames;
            pmd.SkeletonBoneParents = SkeletonBoneParents;
            pmd.SkeletonBoneData = SkeletonBoneData;
            pmd.ObjRefBones = ObjRefBones;
            pmd.Materials = Materials;
            pmd.Nodes = Nodes;

            return pmd;
        }



    }
}
