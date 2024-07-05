using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RE4_UHD_BIN_TOOL.ALL;

namespace RE4_UHD_BIN_TOOL.EXTRACT
{
    public static class UhdBinDecoder
    {
        public static UhdBIN Decoder(Stream stream, long startOffset, out long endOffset) 
        {
            UhdBIN uhdBIN = new UhdBIN();

            BinaryReader br = new BinaryReader(stream);
            br.BaseStream.Position = startOffset;

            uhdBIN.Header = GetHeader(br);

            //Quantidade real de vertices/normals, calculado pela montagem de faces
            int True_Vertex_Count;

            //material
            uhdBIN.Materials = Materials(br, uhdBIN.Header.material_offset + startOffset, uhdBIN.Header.material_count, out True_Vertex_Count);
            endOffset = br.BaseStream.Position;

            //--------

            uhdBIN.Vertex_Position_Array = Get_Vertex_Position_Array(br, uhdBIN.Header.vertex_position_offset + startOffset, True_Vertex_Count);
            uhdBIN.Vertex_Normal_Array = Get_Vertex_Normal_Array(br, uhdBIN.Header.vertex_normal_offset + startOffset, True_Vertex_Count);
            uhdBIN.Vertex_UV_Array = Get_Vertex_UV_Array(br, uhdBIN.Header.vertex_texcoord_offset + startOffset, True_Vertex_Count);
            if (uhdBIN.Header.vertex_colour_offset != 0)
            {
                uhdBIN.Vertex_Color_Array = Get_Vertex_Color_Array(br, uhdBIN.Header.vertex_colour_offset + startOffset, True_Vertex_Count);
            }
            else 
            {
                uhdBIN.Vertex_Color_Array = new (byte a, byte r, byte g, byte b)[0];
            }

            uhdBIN.Bones = Get_Bones(br, uhdBIN.Header.bone_offset + startOffset, uhdBIN.Header.bone_count);

            //WeightMap
            if (uhdBIN.Header.weight_offset != 0 && (uhdBIN.Header.weight_count > 0 || uhdBIN.Header.weight2_count > 0))
            {

                if (uhdBIN.Header.weight2_count > 255)
                {
                    uhdBIN.WeightMaps = fmtBIN_Weight_Ext(br, uhdBIN.Header.weight_offset + startOffset, uhdBIN.Header.weight2_count);
                }
                else
                {
                    uhdBIN.WeightMaps = fmtBIN_Weight(br, uhdBIN.Header.weight_offset + startOffset, uhdBIN.Header.weight_count);
                }
            }

            //WeightIndex
            if (uhdBIN.Header.vertex_weight_index_offset != 0)
            {
                uhdBIN.WeightIndex = Get_WeightIndex(br, uhdBIN.Header.vertex_weight_index_offset + startOffset, True_Vertex_Count);
            }
            else 
            {
                uhdBIN.WeightIndex = new ushort[0];
            }

            //Weight2Index
            if (uhdBIN.Header.vertex_weight2_index_offset != 0)
            {
                uhdBIN.Weight2Index = Get_WeightIndex(br, uhdBIN.Header.vertex_weight2_index_offset + startOffset, True_Vertex_Count);
            }
            else
            {
                uhdBIN.Weight2Index = new ushort[0];
            }

            //bonepairLines
            if (uhdBIN.Header.bonepair_offset != 0)
            {
                uhdBIN.bonepairLines = Get_bonepairLines(br, uhdBIN.Header.bonepair_offset + startOffset);
            }

            //adjacent_offset
            if (uhdBIN.Header.adjacent_offset != 0)
            {
                uhdBIN.adjacent_bone = Get_adjacent_bone(br, uhdBIN.Header.adjacent_offset + startOffset);
            }

            //-----------

            br.BaseStream.Position = endOffset;
            return uhdBIN;
        }


        private static MaterialBin[] Materials(BinaryReader br, long offset, ushort MatCount, out int True_Vertex_count) 
        {
            MaterialBin[] materials = new MaterialBin[MatCount];
            br.BaseStream.Position = offset;

            int indexpos = 0;

            for (int i = 0; i < MatCount; i++)
            {
                materials[i] = Get_Material(br, ref indexpos);
            }

            True_Vertex_count = indexpos;
            return materials;
        }

        private static MaterialBin Get_Material(BinaryReader br, ref int indexPos) 
        {
            MaterialBin group = new MaterialBin();
            MaterialPart mat = new MaterialPart();
            mat.unk_min_11 = br.ReadByte();
            mat.unk_min_10 = br.ReadByte();
            mat.unk_min_09 = br.ReadByte();
            mat.unk_min_08 = br.ReadByte();
            mat.unk_min_07 = br.ReadByte();
            mat.unk_min_06 = br.ReadByte();
            mat.unk_min_05 = br.ReadByte();
            mat.unk_min_04 = br.ReadByte();
            mat.unk_min_03 = br.ReadByte();
            mat.unk_min_02 = br.ReadByte();
            mat.unk_min_01 = br.ReadByte();
            mat.material_flag = br.ReadByte();
            mat.diffuse_map = br.ReadByte();
            mat.bump_map = br.ReadByte();
            mat.opacity_map = br.ReadByte();
            mat.generic_specular_map = br.ReadByte();
            mat.intensity_specular_r = br.ReadByte();
            mat.intensity_specular_g = br.ReadByte();
            mat.intensity_specular_b = br.ReadByte();
            mat.unk_08 = br.ReadByte();
            mat.unk_09 = br.ReadByte();
            mat.specular_scale = br.ReadByte();
            mat.unk_11 = br.ReadByte();
            mat.custom_specular_map = br.ReadByte();
            group.material = mat;

            group.face_index_array = Get_face_index(br, ref indexPos);

            return group;
        }

        private static (int i1, int i2, int i3)[] Get_face_index(BinaryReader br, ref int indexPos) 
        {
            List<(int i1, int i2, int i3)> face_index_list = new List<(int i1, int i2, int i3)>();

            uint buffer_size = br.ReadUInt32();
            uint count = br.ReadUInt32(); //unused

            long tempOffset = br.BaseStream.Position;

            uint strip_count = br.ReadUInt32();

            for (int i = 0; i < strip_count; i++)
            {
                ushort ftype = br.ReadUInt16();
                ushort fcount = br.ReadUInt16();

                Get_face_index_parts(ref face_index_list, ref indexPos, fcount, ftype);
            }

            br.BaseStream.Position = tempOffset + buffer_size;
            return face_index_list.ToArray();
        }


        private static void Get_face_index_parts(ref List<(int i1, int i2, int i3)> face_index_list, ref int indexPos, ushort fcount, ushort ftype)
        {
            if (ftype == 5) //--trianglelist
            {
                int a = indexPos;
                for (int i = 0; i < (fcount / 3); i++)
                {
                    int p1 = a;
                    int p2 = a + 1;
                    int p3 = a + 2;
                    a = a + 3;
                    indexPos = indexPos + 3;

                    if ((p1 != p2) && (p1 != p3) && (p2 != p3))
                    {
                        face_index_list.Add((p1, p2, p3));
                    }

                }
            }

            // ---

            else if (ftype == 6) // --triangle strip
            {
                int bkface = -1;
                int a = indexPos;

                int p1 = a;
                int p2 = a + 1;
                int p3 = a + 2;
                a = a + 3;
                bkface = bkface * -1; // --reboot face strip for each strips grrrr
                indexPos = indexPos + 3;

                face_index_list.Add((p1, p2, p3));

                for (int i = 1; i < fcount - 2; i++)
                {
                    bkface = bkface * -1;
                    p1 = p2;
                    p2 = p3;
                    p3 = a;
                    a = a + 1;
                    indexPos = indexPos + 1;

                    if ((p1 != p2) && (p1 != p3) && (p2 != p3))
                    {
                        if (bkface == 1)
                        {
                            face_index_list.Add((p1, p2, p3));
                        }
                        if (bkface == -1)
                        {
                            face_index_list.Add((p3, p2, p1));
                        }
                    }

                }

            }


            // ---

            else if (ftype == 8) // --quad
            {
                int a = indexPos;

                for (int i = 0; i < (fcount / 4); i++)
                {

                    int p1 = a;
                    int p2 = a + 1;
                    int p3 = a + 2;
                    int p4 = a + 3;
                    a = a + 4;
                    indexPos = indexPos + 4;

                    if ((p1 != p2) && (p1 != p3) && (p2 != p3))
                    {
                        face_index_list.Add((p1, p2, p3));
                        face_index_list.Add((p1, p3, p4));
                    }


                }

            }

            // -----

            else if (ftype == 7) // trangle fan
            {
                int a = indexPos;

                for (int i = 2; i < fcount; i++)
                {
                    int p1 = a;
                    int p2 = a + i-1;
                    int p3 = a + i;

                    if ((p1 != p2) && (p1 != p3) && (p2 != p3))
                    {
                        face_index_list.Add((p1, p2, p3));
                    }
                }

                indexPos += fcount;
            }
        }

        private static ushort[] Get_adjacent_bone(BinaryReader br, long offset) 
        {
            br.BaseStream.Position = offset;

            byte[] big_count = new byte[4];
            br.BaseStream.Read(big_count, 0, big_count.Length);
            big_count = big_count.Reverse().ToArray();
            uint count = BitConverter.ToUInt32(big_count, 0);

            ushort[] adjacents = new ushort[count];

            for (int i = 0; i < count; i++)
            {
                byte[] big_adjacent = new byte[2];
                br.BaseStream.Read(big_adjacent, 0, big_adjacent.Length);
                big_adjacent = big_adjacent.Reverse().ToArray();
                ushort adjacent = BitConverter.ToUInt16(big_adjacent, 0);
                adjacents[i] = adjacent;
            }

            return adjacents;
        }

        private static byte[][] Get_bonepairLines(BinaryReader br, long offset) 
        {
            br.BaseStream.Position = offset;

            uint count = br.ReadUInt32();

            byte[][] bonepairLines = new byte[count][];

            for (int i = 0; i < count; i++)
            {
                byte[] line = new byte[8];
                br.BaseStream.Read(line, 0, line.Length);
                bonepairLines[i] = line;
            }
            return bonepairLines;
        }


        private static ushort[] Get_WeightIndex(BinaryReader br, long offset, int count) 
        {
            ushort[] weightIndex = new ushort[count];

            br.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                weightIndex[i] = br.ReadUInt16();
            }
            return weightIndex;

        }

        private static Bone[] Get_Bones(BinaryReader br, long offset, ushort count) 
        {
            Bone[] bones = new Bone[count];
           
            br.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                Bone bone = new Bone();
                bone.boneLine = br.ReadBytes(0x10);
                bones[i] = bone;
            }

            return bones;
        }


        private static WeightMap[] fmtBIN_Weight_Ext(BinaryReader br, long offset, ushort count) 
        {
            br.BaseStream.Position = offset;

            WeightMap[] weightMaps = new WeightMap[count];
            for (int i = 0; i < count; i++)
            {
                WeightMap weightMap = new WeightMap();
                weightMap.boneId1 = br.ReadUInt16();
                weightMap.boneId2 = br.ReadUInt16();
                weightMap.boneId3 = br.ReadUInt16();
                weightMap.count = br.ReadUInt16();
                weightMap.weight1 = br.ReadByte();
                weightMap.weight2 = br.ReadByte();
                weightMap.weight3 = br.ReadByte();
                weightMap.unk018 = br.ReadByte();
                weightMaps[i] = weightMap;
            }
            return weightMaps;
        }

        private static WeightMap[] fmtBIN_Weight(BinaryReader br, long offset, ushort count) 
        {
            br.BaseStream.Position = offset;

            WeightMap[] weightMaps = new WeightMap[count];
            for (int i = 0; i < count; i++)
            {
                WeightMap weightMap = new WeightMap();
                weightMap.boneId1 = br.ReadByte();
                weightMap.boneId2 = br.ReadByte();
                weightMap.boneId3 = br.ReadByte();
                weightMap.count = br.ReadByte();
                weightMap.weight1 = br.ReadByte();
                weightMap.weight2 = br.ReadByte();
                weightMap.weight3 = br.ReadByte();
                weightMap.unk018 = br.ReadByte();
                weightMaps[i] = weightMap;
            }
            return weightMaps;
        }


        private static (byte a, byte r, byte g, byte b)[] Get_Vertex_Color_Array(BinaryReader br, long offset, int count) 
        {
            (byte a, byte r, byte g, byte b)[] colors = new (byte a, byte r, byte g, byte b)[count];

            br.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                byte a = br.ReadByte();
                byte r = br.ReadByte();
                byte g = br.ReadByte();
                byte b = br.ReadByte();
                colors[i] = (a, r, g, b);
            }
            return colors;
        }

        private static (float tu, float tv)[] Get_Vertex_UV_Array(BinaryReader br, long offset, int count) 
        {
            (float tu, float tv)[] uvs = new (float tu, float tv)[count];

            br.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                float tu = br.ReadSingle();
                float tv = br.ReadSingle();
                uvs[i] = (tu, tv);

            }
            return uvs;
        }

        private static (float nx, float ny, float nz)[] Get_Vertex_Normal_Array(BinaryReader br, long offset, int count) 
        {
            (float nx, float ny, float nz)[] normals = new (float nx, float ny, float nz)[count];
            
            br.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                float nx = br.ReadSingle();
                float ny = br.ReadSingle();
                float nz = br.ReadSingle();
                normals[i] = (nx, ny, nz);

            }
            return normals;
        }

        private static (float vx, float vy, float vz)[] Get_Vertex_Position_Array(BinaryReader br, long offset, int count) 
        {
            (float vx, float vy, float vz)[] positions = new (float vx, float vy, float vz)[count];

            br.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                float vx = br.ReadSingle();
                float vy = br.ReadSingle();
                float vz = br.ReadSingle();
                positions[i] = (vx, vy, vz);
            }
            return positions;
        }

        private static UhdBinHeader GetHeader(BinaryReader br) 
        {
            UhdBinHeader header = new UhdBinHeader();

            header.bone_offset = br.ReadUInt32(); //--headersize // 60 00 00 00
            header.unknown_x04 = br.ReadUInt32(); //--zeros
            header.unknown_x08 = br.ReadUInt32(); // offset // 50 00 00 00
            header.vertex_colour_offset = br.ReadUInt32();


            header.vertex_texcoord_offset = br.ReadUInt32();
            header.weight_offset = br.ReadUInt32();
            header.weight_count = br.ReadByte();
            header.bone_count = br.ReadByte();
            header.material_count = br.ReadUInt16();
            header.material_offset = br.ReadUInt32();


            header.texture1_flags = br.ReadUInt16();
            header.texture2_flags = br.ReadUInt16();
            header.TPL_count = br.ReadUInt32();
            header.vertex_scale = br.ReadByte();
            header.unknown_x29 = br.ReadByte();
            header.weight2_count = br.ReadUInt16(); //--same as weightcount
            header.morph_offset = br.ReadUInt32();


            header.vertex_position_offset = br.ReadUInt32();
            header.vertex_normal_offset = br.ReadUInt32();
            header.vertex_position_count = br.ReadUInt16();
            header.vertex_normal_count = br.ReadUInt16();
            header.version_flags = br.ReadUInt32();


            header.bonepair_offset = br.ReadUInt32();
            header.adjacent_offset = br.ReadUInt32();
            header.vertex_weight_index_offset = br.ReadUInt32();  //--vertex weights id's array (2 words )* numvertex
            header.vertex_weight2_index_offset = br.ReadUInt32(); //--vertex weights array (2 words )* numvertex


            return header;
        }


    }
}
