using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    public enum SpecialType : byte
    {
        T00_GeneralPurpose = 0x00,
        T01_WarpDoor = 0x01,
        T02_CutSceneEvents = 0x02,
        T03_Items = 0x03,
        T04_GroupedEnemyTrigger = 0x04,
        T05_Message = 0x05,
        T06_Unused = 0x06,
        T07_Unused = 0x07,
        T08_TypeWriter = 0x08,
        T09_Unused = 0x09,
        T0A_DamagesThePlayer = 0x0A,
        T0B_FalseCollision = 0x0B,
        T0C_Unused = 0x0C,
        T0D_Unknown = 0x0D,
        T0E_Crouch = 0x0E,
        T0F_Unused = 0x0F,
        T10_FixedLadderClimbUp = 0x10,
        T11_ItemDependentEvents = 0x11,
        T12_AshleyHideCommand = 0x12,
        T13_LocalTeleportation = 0x13,
        T14_UsedForElevators = 0x14,
        T15_AdaGrappleGun = 0x15,
        // Valor do tipo de linha no AEV e ITA não definido
        UnspecifiedType = 0XFF,
    }
}
