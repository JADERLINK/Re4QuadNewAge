using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SHARED_UHD_BIN.EXTRACT
{
    public static class UhdTplDecoder
    {
        public static UhdTPL Decoder(Stream stream, long startOffset, out long endOffset, bool IsPs4NS)
        {
            UhdTPL tpl = new UhdTPL();

            BinaryReader br = new BinaryReader(stream);
            br.BaseStream.Position = startOffset;

            uint magic = br.ReadUInt32();
            if ( ! (magic == 0x78563412 || magic == 0x12345678))
            {
                throw new ArgumentException("Invalid TPL file!");
            }
            uint TplAmount = br.ReadUInt32();
            uint StartOffset = br.ReadUInt32();

            br.BaseStream.Position = StartOffset + startOffset;

            uint[] offsets = new uint[TplAmount];

            for (int i = 0; i < TplAmount; i++)
            {
                uint image_data_offset = br.ReadUInt32();
                if (IsPs4NS)
                {
                    _ = br.ReadUInt32(); // image_data_offset part2
                }

                uint palette_offset = br.ReadUInt32(); // não usado
                if (IsPs4NS)
                {
                    _ = br.ReadUInt32(); // palette_offset part2
                }

                offsets[i] = image_data_offset;
            }

            TplInfo[] tplArray = new TplInfo[TplAmount]; 

            for (int i = 0; i < TplAmount; i++)
            {
                br.BaseStream.Position = offsets[i] + startOffset;

                TplInfo tplInfo = new TplInfo();

                tplInfo.width = br.ReadUInt16();
                tplInfo.height = br.ReadUInt16();
                tplInfo.PixelFormatType = br.ReadUInt32();

                uint secundOffset = br.ReadUInt32();
                if (IsPs4NS)
                {
                    _ = br.ReadUInt32(); // secundOffset part2
                }

                tplInfo.wrap_s = br.ReadUInt32();
                tplInfo.wrap_t = br.ReadUInt32();
                tplInfo.min_filter = br.ReadUInt32();
                tplInfo.mag_filter = br.ReadUInt32();
                tplInfo.lod_bias = br.ReadSingle();

                tplInfo.enable_lod = br.ReadByte();
                tplInfo.min_lod = br.ReadByte();
                tplInfo.max_lod = br.ReadByte();
                tplInfo.is_compressed = br.ReadByte();

                br.BaseStream.Position = secundOffset + startOffset;

                tplInfo.PackID = br.ReadUInt32();
                tplInfo.TextureID = br.ReadUInt32();

                tplArray[i] = tplInfo;
            }

            tpl.TplArray = tplArray;

            endOffset = br.BaseStream.Position;
            return tpl;
        }


    }
}
