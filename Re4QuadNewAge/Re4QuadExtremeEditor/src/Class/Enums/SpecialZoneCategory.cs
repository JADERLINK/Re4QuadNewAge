using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum SpecialZoneCategory : byte
    {
        Disable = 0x00, // Disable TriggerZone
        Category01 = 0x01, // TriggerZone Use 4 points
        Category02 = 0x02, // TriggerZone Use 1 point and Scale
        AnotherValue = 0xFF // Another Value: Disable TriggerZone
    }
}
