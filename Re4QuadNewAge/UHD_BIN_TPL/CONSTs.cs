using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED_UHD_BIN.ALL
{
    public static class CONSTs
    {
        public const float GLOBAL_POSITION_SCALE = 100f;
        public const float GLOBAL_NORMAL_FIX_EXTENDED = 545460800000f;
        public const float GLOBAL_NORMAL_FIX_REDUCED = 16384f;
        //                                     545460800000.00
        //                                            16384.00
        public const string UHD_MATERIAL = "UHD_MATERIAL_";
        public const string SCENARIO_MATERIAL = "ROOM_MATERIAL_";

        public const byte FACE_TYPE_TRIANGLE_LIST = 0x05;
        public const byte FACE_TYPE_TRIANGLE_STRIP = 0x06;
        public const byte FACE_TYPE_QUAD_LIST = 0x08;

    }
}
