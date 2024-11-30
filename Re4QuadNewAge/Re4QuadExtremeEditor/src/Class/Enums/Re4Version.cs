using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum Re4Version : byte
    {
        NULL = 0,
        V2007PS2 = 1,
        UHD = 2
    }

    public enum IsRe4Version : byte
    {
        NULL = 0,
        V2007PS2 = 1,
        UHD = 2,
        PS4NS = 3, //caso tiver
        BIG_ENDIAN = 4
    }
}
