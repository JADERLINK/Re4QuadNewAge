using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    [Flags]
    public enum MoveObjCombos : uint
    {
        Null = 0x00,
        DisableMoveObject = 0x01,
        Enemy = 0x02,
        Etcmodel = 0x04,
        Item = 0x08, // Item position angle
        TriggerZone = 0x10, //only TriggerZone (aev, ita, sar, ear, fse, QuadCustom)
        ExtraSpecialWarpLadderGrappleGun = 0x20,
        ExtraSpecialAshley = 0x40,
        QuadCustom = 0x80, //point position, angle, scale
        EseEntry = 0x0100,
        EmiEntry = 0x0200,
        LitEntry = 0x0400,
        EffEntry = 0x0800, //position angle
        EffTable9 = 0x1000, //position only

        //combos
        Combo_Item_TriggerZone = Item | TriggerZone,
        Combo_QuadCustom_TriggerZone = QuadCustom | TriggerZone,
        Combo_Item_QuadCustom_TriggerZone = Item | QuadCustom | TriggerZone,
        Combo_Item_QuadCustom = Item | QuadCustom,

        ComboAll = Enemy | Etcmodel | Item | TriggerZone | ExtraSpecialWarpLadderGrappleGun | ExtraSpecialAshley 
                 | QuadCustom | EseEntry | EmiEntry | LitEntry | EffEntry | EffTable9
    }
}
