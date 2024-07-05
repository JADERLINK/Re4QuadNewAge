using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4_UHD_BIN_TOOL.EXTRACT
{
    public class UhdTPL
    {
        public TplInfo[] TplArray;
    }

    public class TplInfo
    {
        public ushort width;
        public ushort height;
        public uint PixelFormatType;

        public uint wrap_s; //default: 1
        public uint wrap_t; //default: 1
        public uint min_filter; //default: 1
        public uint mag_filter; //default: 1
        public float lod_bias; //default: 0
        public byte enable_lod; //default: 0
        public byte min_lod; //default: 0
        public byte max_lod; //default: 0
        public byte is_compressed; //default: 0

        public uint PackID;
        public uint TextureID;

        public override string ToString() // apenas informacional
        {
            return width.ToString("X4") + "  "
                 + height.ToString("X4") + "  "
                 + PixelFormatType.ToString("X8") + "  "
                 + wrap_s.ToString("X8") + "  "
                 + wrap_t.ToString("X8") + "  "
                 + min_filter.ToString("X8") + "  "
                 + mag_filter.ToString("X8") + "  "
                 + lod_bias.ToString("F9") + "  "
                 + enable_lod.ToString("X2") + "  "
                 + min_lod.ToString("X2") + "  "
                 + max_lod.ToString("X2") + "  "
                 + is_compressed.ToString("X2") + "  "
                 + PackID.ToString("X8") + "  "
                 + TextureID.ToString("X8")
               ;
        }

        public override bool Equals(object obj)
        {
            return obj is TplInfo info && info.TextureID == TextureID && info.PackID == PackID;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)(PackID + TextureID);
            }          
        }
    }
}
