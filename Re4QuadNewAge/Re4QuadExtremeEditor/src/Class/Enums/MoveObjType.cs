using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.Enums
{
    [Flags]
    public enum MoveObjType : uint
    {
        Null = 0,
        // Square
        _SquareNone = 1,
        _SquareMoveObjXZ = 32,
        _SquareMoveTriggerZone = 64,
        _SquareMoveAshleyZone = 32768,
        // 524288,
        // addons:
        __AllPointsXZ = 65536,
        __Point0XZ = 128,
        __Point1XZ = 256,
        __Point2XZ = 512,
        __Point3XZ = 1024,
        __WallPoint01and12XZ = 4096, //SquareMoveTriggerZone
        __WallPoint12and23XZ = 2048, //SquareMoveTriggerZone
        __Wallpoint23and30XZ = 16384, //SquareMoveTriggerZone
        __WallPoint30and01XZ = 8192, //SquareMoveTriggerZone  
        // Vertical
        _VerticalNone = 2,
        _VerticalMoveObjY = 1048576,
        _VerticalScaleObjAll = 2097152,
        _VerticalMoveTriggerZoneY = 4194304,
        // Horizontal1
        _Horizontal1None = 4,
        _Horizontal1RotationObjX = 8388608,
        _Horizontal1ScaleObjX = 33554432,
        _Horizontal1ChangeTriggerZoneHeight = 134217728,
        // Horizontal2
        _Horizontal2None = 8,
        _Horizontal2RotationObjY = 16777216,
        _Horizontal2ScaleObjY = 67108864,
        _Horizontal2RotationZoneY = 268435456,
        // Horizontal3
        _Horizontal3None = 16,
        _Horizontal3RotationObjZ = 131072,
        _Horizontal3ScaleObjZ = 67108864,
        _Horizontal3TriggerZoneScaleAll = 536870912,
        _Horizontal3AshleyZoneScaleAll = 262144,

        //combos
        SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ =
            _SquareMoveObjXZ | _VerticalMoveObjY | _Horizontal1RotationObjX | _Horizontal2RotationObjY | _Horizontal3RotationObjZ,

        SquareNone_VerticalScaleObjAll_Horizontal123ScaleObjXYZ =
            _SquareNone | _VerticalScaleObjAll | _Horizontal1ScaleObjX | _Horizontal2ScaleObjY | _Horizontal3ScaleObjZ,

        SquareMoveTriggerZoneAllPointsXZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal2RotationZoneY_Horizontal3ScaleAll =
            _SquareMoveTriggerZone | __AllPointsXZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2RotationZoneY | _Horizontal3TriggerZoneScaleAll,

        SquareMoveTriggerZonePoint0XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __Point0XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZonePoint1XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __Point1XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZonePoint2XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __Point2XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZonePoint3XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __Point3XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZoneWallPoint01and12XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __WallPoint01and12XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZoneWallPoint12and23XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __WallPoint12and23XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZoneWallpoint23and30XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __Wallpoint23and30XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveTriggerZoneWallPoint30and01XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None =
            _SquareMoveTriggerZone | __WallPoint30and01XZ | _VerticalMoveTriggerZoneY | _Horizontal1ChangeTriggerZoneHeight | _Horizontal2None | _Horizontal3None,

        SquareMoveObjXZ_VerticalMoveObjY_Horizontal1None_Horizontal2RotationObjY_Horizontal3None =
            _SquareMoveObjXZ | _VerticalMoveObjY | _Horizontal1None | _Horizontal2RotationObjY | _Horizontal3None,

        SquareMoveObjXZ_VerticalMoveObjY_Horizontal123None =
            _SquareMoveObjXZ | _VerticalMoveObjY | _Horizontal1None | _Horizontal2None | _Horizontal3None,

        SquareMoveAshleyAllPointsXZ_VerticalNone_Horizontal1None_Horizontal2RotationZoneY_Horizontal3ScaleAll =
            _SquareMoveAshleyZone | __AllPointsXZ | _VerticalNone | _Horizontal1None | _Horizontal2RotationZoneY | _Horizontal3AshleyZoneScaleAll,

        SquareMoveAshleyPoint0XZ_VerticalNone_Horizontal123None =
            _SquareMoveAshleyZone | __Point0XZ | _VerticalNone | _Horizontal1None | _Horizontal2None | _Horizontal3None,

        SquareMoveAshleyPoint1XZ_VerticalNone_Horizontal123None =
            _SquareMoveAshleyZone | __Point1XZ | _VerticalNone | _Horizontal1None | _Horizontal2None | _Horizontal3None,

        SquareMoveAshleyPoint2XZ_VerticalNone_Horizontal123None =
            _SquareMoveAshleyZone | __Point2XZ | _VerticalNone | _Horizontal1None | _Horizontal2None | _Horizontal3None,

        SquareMoveAshleyPoint3XZ_VerticalNone_Horizontal123None =
            _SquareMoveAshleyZone | __Point3XZ | _VerticalNone | _Horizontal1None | _Horizontal2None | _Horizontal3None
    }
}
