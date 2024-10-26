using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED_UHD_BIN.ALL
{
    /// <summary>
    /// representa um material do .bin
    /// </summary>
    public class MaterialPart
    {
        public byte unk_min_11;
        public byte unk_min_10;
        public byte unk_min_09;
        public byte unk_min_08;
        public byte unk_min_07;
        public byte unk_min_06;
        public byte unk_min_05;
        public byte unk_min_04;
        public byte unk_min_03;
        public byte unk_min_02;
        public byte unk_min_01;
        public byte material_flag;
        public byte diffuse_map;
        public byte bump_map;
        public byte opacity_map; //alpha
        public byte generic_specular_map;
        public byte intensity_specular_r;
        public byte intensity_specular_g;
        public byte intensity_specular_b;
        public byte unk_08;
        public byte unk_09;
        public byte specular_scale;
        public byte unk_11;
        public byte custom_specular_map;

        public byte[] GetArray()
        {
            byte[] b = new byte[24];
            b[00] = unk_min_11;
            b[01] = unk_min_10;
            b[02] = unk_min_09;
            b[03] = unk_min_08;
            b[04] = unk_min_07;
            b[05] = unk_min_06;
            b[06] = unk_min_05;
            b[07] = unk_min_04;
            b[08] = unk_min_03;
            b[09] = unk_min_02;
            b[10] = unk_min_01;
            b[11] = material_flag;
            b[12] = diffuse_map;
            b[13] = bump_map;
            b[14] = opacity_map;
            b[15] = generic_specular_map;
            b[16] = intensity_specular_r;
            b[17] = intensity_specular_g;
            b[18] = intensity_specular_b;
            b[19] = unk_08;
            b[20] = unk_09;
            b[21] = specular_scale;
            b[22] = unk_11;
            b[23] = custom_specular_map;
            return b;
        }

        public override bool Equals(object obj)
        {
            return obj is MaterialPart m
                && m.material_flag == material_flag
                && m.diffuse_map == diffuse_map
                && m.bump_map == bump_map
                && m.opacity_map == opacity_map
                && m.generic_specular_map == generic_specular_map
                && m.intensity_specular_r == intensity_specular_r
                && m.intensity_specular_g == intensity_specular_g
                && m.intensity_specular_b == intensity_specular_b
                && m.unk_08 == unk_08
                && m.unk_09 == unk_09
                && m.specular_scale == specular_scale
                && m.unk_11 == unk_11
                && m.custom_specular_map == custom_specular_map
                && m.unk_min_01 == unk_min_01
                && m.unk_min_02 == unk_min_02
                && m.unk_min_03 == unk_min_03
                && m.unk_min_04 == unk_min_04
                && m.unk_min_05 == unk_min_05
                && m.unk_min_06 == unk_min_06
                && m.unk_min_07 == unk_min_07
                && m.unk_min_08 == unk_min_08
                && m.unk_min_09 == unk_min_09
                && m.unk_min_10 == unk_min_10
                && m.unk_min_11 == unk_min_11
                ;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + material_flag.GetHashCode();
                hash = hash * 23 + diffuse_map.GetHashCode();
                hash = hash * 23 + bump_map.GetHashCode();
                hash = hash * 23 + opacity_map.GetHashCode();
                hash = hash * 23 + generic_specular_map.GetHashCode();
                hash = hash * 23 + intensity_specular_r.GetHashCode();
                hash = hash * 23 + intensity_specular_g.GetHashCode();
                hash = hash * 23 + intensity_specular_b.GetHashCode();
                hash = hash * 23 + unk_08.GetHashCode();
                hash = hash * 23 + unk_09.GetHashCode();
                hash = hash * 23 + specular_scale.GetHashCode();
                hash = hash * 23 + unk_11.GetHashCode();
                hash = hash * 23 + custom_specular_map.GetHashCode();
                hash = hash * 23 + unk_min_01.GetHashCode();
                hash = hash * 23 + unk_min_02.GetHashCode();
                hash = hash * 23 + unk_min_03.GetHashCode();
                hash = hash * 23 + unk_min_04.GetHashCode();
                hash = hash * 23 + unk_min_05.GetHashCode();
                hash = hash * 23 + unk_min_06.GetHashCode();
                hash = hash * 23 + unk_min_07.GetHashCode();
                hash = hash * 23 + unk_min_08.GetHashCode();
                hash = hash * 23 + unk_min_09.GetHashCode();
                hash = hash * 23 + unk_min_10.GetHashCode();
                hash = hash * 23 + unk_min_11.GetHashCode();
                return hash;
            }
        }

    }

}
