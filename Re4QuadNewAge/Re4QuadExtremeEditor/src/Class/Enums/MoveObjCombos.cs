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
        Enemy = 1,
        Etcmodel = 2,
        Item = 4,
        SpecialTriggerZone = 8,
        ExtraSpecialWarpLadderGrappleGun = 16,
        ExtraSpecialAshley = 32,

        ComboEnemyEtcmodel = Enemy | Etcmodel,
        ComboEnemyItem = Enemy | Item,
        ComboEtcmodelItem = Etcmodel | Item,
        ComboEnemyEtcmodelItem = Enemy | Etcmodel | Item,
        ComboSpecialTriggerZoneAll = Item | SpecialTriggerZone,
        ComboExtraSpecialAll = ExtraSpecialWarpLadderGrappleGun | ExtraSpecialAshley,
        ComboAll = Enemy | Etcmodel | Item | SpecialTriggerZone | ExtraSpecialWarpLadderGrappleGun | ExtraSpecialAshley
    }
}
