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
        Null = 0,
        DisableMoveObject = 1,
        Enemy = 2,
        Etcmodel = 4,
        Item = 8, // Item position angle
        TriggerZone = 16, //only TriggerZone (aev, ita, sar, ear, fse, QuadCustom)
        ExtraSpecialWarpLadderGrappleGun = 32,
        ExtraSpecialAshley = 64,
        QuadCustom = 128, //point position, angle, scale
        EseEntry = 256,
        EmiEntry = 512,
        LitEntry = 1024,

        //combos
        Combo_Item_TriggerZone = Item | TriggerZone,
        Combo_QuadCustom_TriggerZone = QuadCustom | TriggerZone,
        Combo_Item_QuadCustom_TriggerZone = Item | QuadCustom | TriggerZone,
        Combo_Item_QuadCustom = Item | QuadCustom,

        ComboAll = Enemy | Etcmodel | Item | TriggerZone | ExtraSpecialWarpLadderGrappleGun | ExtraSpecialAshley | QuadCustom | EseEntry | EmiEntry | LitEntry
    }
}
