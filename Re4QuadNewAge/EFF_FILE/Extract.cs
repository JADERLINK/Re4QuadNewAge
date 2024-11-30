using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SimpleEndianBinaryIO;

namespace EFF_SPLIT
{
    internal static class Extract
    {
        public static TablesGroup ExtractFile(EndianBinaryReader br, Endianness endianness)
        {
            uint Magic = br.ReadUInt32(); //sempre 0x0B
            if (Magic != 0x0B)
            {
                throw new ArgumentException("Invalid file!");
            }

            uint offset_0_TPL_Texture_IDs = br.ReadUInt32();
            uint offset_1_Ref_Effect_0_IDs = br.ReadUInt32();
            uint offset_2_EAR_Links = br.ReadUInt32();
            uint offset_3_Effect_Path_IDs = br.ReadUInt32();
            uint offset_4_BIN_Model_IDs = br.ReadUInt32();
            uint offset_5_TPL_Offsets = br.ReadUInt32(); // não usado.
            uint offset_6_Texture_Metadata = br.ReadUInt32();
            uint offset_7_Effect_0_Type = br.ReadUInt32();
            uint offset_8_Effect_1_Type = br.ReadUInt32();
            uint offset_9_Paths = br.ReadUInt32();
            uint offset_10_Data_Offset = br.ReadUInt32(); // não usado.

            TablesGroup tables = new TablesGroup();
            tables.Table00 = Separate.TableIndexEntry(br, offset_0_TPL_Texture_IDs, out _);
            tables.Table01 = Separate.TableIndexEntry(br, offset_1_Ref_Effect_0_IDs, out _);
            tables.Table02 = Separate.TableIndexEntry(br, offset_2_EAR_Links, out _);
            tables.Table03 = Separate.TableIndexEntry(br, offset_3_Effect_Path_IDs, out _);
            tables.Table04 = Separate.TableIndexEntry(br, offset_4_BIN_Model_IDs, out _);
            tables.Table06 = Separate.Table06(br, offset_6_Texture_Metadata, out _, true);
            tables.Table09 = Separate.Table09(br, offset_9_Paths, out _);

            tables.Table07_Effect_0_Type = Separate.Effect_Type(br, offset_7_Effect_0_Type, out _, endianness);
            tables.Table08_Effect_1_Type = Separate.Effect_Type(br, offset_8_Effect_1_Type, out _, endianness);
            return tables;
        }

    }
}
