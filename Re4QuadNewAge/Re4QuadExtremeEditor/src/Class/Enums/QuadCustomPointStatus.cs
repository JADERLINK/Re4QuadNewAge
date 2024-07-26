using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum QuadCustomPointStatus : byte
    {
        Disable = 0x00, // Disable Point
        ArrowPoint01 = 0x01, // ArrowPoint modelo padrão
        CustomModel02 = 0x02, // CustomModel modelo customizado
        AnotherValue = 0xFF // Another Value: Disable Point
    }
}
