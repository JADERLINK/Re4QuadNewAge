using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
   public enum GroupType : byte
    {
        NULL = 0x00,
        ESL = 0x01,
        ETS = 0x02,
        ITA = 0x03,
        AEV = 0x04,
        EXTRAS = 0x05,
        FSE = 0x06,
        SAR = 0x07,
        EAR = 0x08,
        EMI = 0x09,
        ESE = 0x10,

        LIT = 0x20,
        LIT_GROUPS = 0x21,
        LIT_ENTRYS = 0x22,

        QUAD_CUSTOM = 0x50,

        DSE = 0xAA,

        END_BACKGROUND = 0xFF
    }
}
