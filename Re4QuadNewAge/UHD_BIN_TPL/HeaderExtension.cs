using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED_UHD_BIN.ALL;

namespace SHARED_UHD_BIN.EXTRACT
{
    public static class HeaderExtension
    {

        //no arquivo bin, tem uma flag(bit), que define o proporção da vertex_normal (no caso a normal existe em duas escala)
        public static bool ReturnsIfItIsNormalsExtended(this UhdBinHeader header)
        {
            return (header.texture2_flags & 0x2000) == 0x2000; // 0x2000 representa qual é o bit ativa para as normals extendidas (escala maior)
        }

        // o metodo de cima verifica, esse metodo retorna no valor a ser usado.
        public static float ReturnsNormalsFixValue(this UhdBinHeader header)
        {
            return ReturnsIfItIsNormalsExtended(header) ? CONSTs.GLOBAL_NORMAL_FIX_EXTENDED : CONSTs.GLOBAL_NORMAL_FIX_REDUCED;
        }


        //----
        public static bool ReturnsEnableBonepairTag(this UhdBinHeader header)
        {
            return (header.texture1_flags & 0x0100) == 0x0100; // BonepairTag
        }

        public static bool ReturnsEnableAdjacentBoneTag(this UhdBinHeader header)
        {
            return (header.texture1_flags & 0x0200) == 0x0200; // AdjacentBoneTag
        }

        public static bool ReturnsIsEnableVertexColors(this UhdBinHeader header)
        {
            return (header.texture2_flags & 0x4000) == 0x4000; // 0x4000 representa qual é o bit ativo para abilitar as vertex colors
        }

    }
}
