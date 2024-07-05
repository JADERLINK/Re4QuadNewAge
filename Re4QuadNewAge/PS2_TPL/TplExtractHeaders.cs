using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace TPL_PS2_EXTRACT
{
    public class TplExtractHeaders
    {
        private BinaryReader br = null;
        private long mainOffset = 0;

        public TplExtractHeaders(BinaryReader br, long mainOffset = 0) 
        {
            this.br = br;
            this.mainOffset = mainOffset;
        }

        public (uint Magic, uint TplCount, uint StartOffset, uint HeaderUnk1) MainReader() 
        {
            br.BaseStream.Position = mainOffset;
            uint Magic = br.ReadUInt32();
            uint TplCount = br.ReadUInt32();
            uint StartOffset = br.ReadUInt32();
            uint HeaderUnk1 = br.ReadUInt32();
            return (Magic, TplCount, StartOffset, HeaderUnk1);
        }

        public TplImageHeader[] Extract(uint TplCount, uint StartOffset) 
        {
            br.BaseStream.Position = StartOffset + mainOffset;
            TplImageHeader[] tihs = new TplImageHeader[TplCount];

            uint PositionCount = 0;
            for (int i = 0; i < TplCount; i++)
            {
                TplImageHeader tih = Fill(ref br, PositionCount + StartOffset + mainOffset);
                PositionCount += 0x30;

                if (tih.MipmapStatus == 0x2)
                {
                    if (tih.MipmapHeader1Offset != 0)
                    {
                        TplImageHeader mipmap1 = Fill(ref br, tih.MipmapHeader1Offset + mainOffset);
                        tih.MipmapHeader1 = mipmap1;
                    }

                    if (tih.MipmapHeader2Offset != 0)
                    {
                        TplImageHeader mipmap2 = Fill(ref br, tih.MipmapHeader2Offset + mainOffset);
                        tih.MipmapHeader2 = mipmap2;
                    }

                }

                tihs[i] = tih;
            }

            return tihs;
        }

        private TplImageHeader Fill(ref BinaryReader br, long Offset)
        {
            TplImageHeader tih = new TplImageHeader();

            br.BaseStream.Position = Offset;

            byte[] subheader = br.ReadBytes(0x30);
            tih.Line = subheader;

            ushort width = BitConverter.ToUInt16(subheader, 0);
            tih.Width = width;

            ushort height = BitConverter.ToUInt16(subheader, 2);
            tih.Height = height;

            ushort bitDepth = BitConverter.ToUInt16(subheader, 4);
            tih.BitDepth = bitDepth;

            ushort interlace = BitConverter.ToUInt16(subheader, 6);
            tih.Interlace = interlace;

            ushort unk5 = BitConverter.ToUInt16(subheader, 8);
            tih.Next = unk5;

            //mipmapStatus
            //nesse campo somente existe dois valores atribuiveis:
            // 0x0: não tem conteudo mipmap
            // 0x2 contem o conteudo mipmap
            ushort mipmapStatus = BitConverter.ToUInt16(subheader, 0XA);
            tih.MipmapStatus = mipmapStatus;

            ushort unk7 = BitConverter.ToUInt16(subheader, 0xC);
            tih.Qwc = unk7;

            ushort unk8 = BitConverter.ToUInt16(subheader, 0xE);
            tih.Ref = unk8;

            uint mipmapHeader1Offset = BitConverter.ToUInt32(subheader, 0x10);
            tih.MipmapHeader1Offset = mipmapHeader1Offset;

            uint mipmapHeader2Offset = BitConverter.ToUInt32(subheader, 0x14);
            tih.MipmapHeader2Offset = mipmapHeader2Offset;

            ulong gsMip = BitConverter.ToUInt64(subheader, 0x18);
            tih.GsMip = gsMip;

            uint indexesOffset = BitConverter.ToUInt32(subheader, 0x20);
            tih.IndexesOffset = indexesOffset;

            uint paletteOffset = BitConverter.ToUInt32(subheader, 0x24);
            tih.PaletteOffset = paletteOffset;

            ulong gsTex = BitConverter.ToUInt64(subheader, 0x28);
            tih.GsTex = gsTex;

            return tih;
        }
    }
}
