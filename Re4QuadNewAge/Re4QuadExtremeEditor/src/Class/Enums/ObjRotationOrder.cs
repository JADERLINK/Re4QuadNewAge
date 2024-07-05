using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum ObjRotationOrder : int
    {
        RotationXYZ = 0,
        RotationXZY = 1,
        RotationYXZ = 2,
        RotationYZX = 3,
        RotationZYX = 4,
        RotationZXY = 5,
        RotationXY = 6,
        RotationXZ = 7,
        RotationYX = 8,
        RotationYZ = 9,
        RotationZX = 10,
        RotationZY = 11,
        RotationX = 12,
        RotationY = 13,
        RotationZ = 14,
        NoRotation = -1,
    }
}
