using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum ItemAuraType : byte
    {
        /// <summary>
        /// 0x0 - No sparkle and no light column (Default)
        /// </summary>
        Aura00 = 0x00,
        /// <summary>
        /// 0x1 - Small glint (with sparkle)
        /// </summary>
        Aura01 = 0x01,
        /// <summary>
        /// 0x2 - Small white/gold light column (with sparkle)
        /// </summary>
        Aura02 = 0x02,
        /// <summary>
        /// 0x3 - Small blue/white light column (no sparkle)
        /// </summary>
        Aura03 = 0x03,
        /// <summary>
        /// 0x4 - Small Green light column (herbs)
        /// </summary>
        Aura04 = 0x04,
        /// <summary>
        /// 0x5 - Small Red light column (ammo)
        /// </summary>
        Aura05 = 0x05,
        /// <summary>
        /// 0x6 - Small Red light column (ammo)
        /// </summary>
        Aura06 = 0x06,
        /// <summary>
        /// 0x7 - BIG blue/white light column
        /// </summary>
        Aura07 = 0x07,
        /// <summary>
        /// 0x8 - Small white light column (with sparkle)
        /// </summary>
        Aura08 = 0x08,
        /// <summary>
        /// 0x9 - BIG yellow light column (bonus time)
        /// </summary>
        Aura09 = 0x09,
        /// <summary>
        /// Aka 00
        /// </summary>
        AuraAnoterValue = 0xFF,

        /*
         0x0 - No sparkle and no light column (Default)
         0x1 - Small glint (with sparkle)
         0x2 - Small white/gold light column (with sparkle)
         0x3 - Small blue/white light column (no sparkle)
         0x4 - Small Green light column (herbs)
         0x5 - Small Red light column (ammo)
         0x6 - Small Red light column (ammo)
         0x7 - BIG blue/white light column
         0x8 - Small white light column (with sparkle)
         0x9 - BIG yellow light column (bonus time)

         */
    }
}
