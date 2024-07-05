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
        EXTRAS = 0x5,
        END_UNUSED = 0xFF
    }
}
