﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.Enums;
using OpenTK;
using System.Windows.Forms;
using System.Drawing;
using NsCamera;

namespace Re4QuadExtremeEditor.src.Class
{
 

    public static class MoveObj
    {
        public static float objSpeedMultiplier = 1.0f;

        public static bool LockMoveSquareVertical = false;
        public static bool LockMoveSquareHorisontal = false;

        public static bool MoveRelativeCamera = false;
        public static bool KeepOnGround = false;

        public static bool TriggerZoneKeepOnGround = false;

        [Flags]
        public enum MoveDirection : byte
        {
            Null = 0,
            X = 1,
            Y = 2,
            Z = 4
        }

        public struct ObjKey
        {
            public ushort ID { get; }
            public GroupType Group { get; }

            public ObjKey(ushort ID, GroupType Group) 
            {
                this.ID = ID;
                this.Group = Group;
            }

            public override bool Equals(object obj)
            {
                return (obj is ObjKey key && key.Group == Group && key.ID == ID);
            }
            public override int GetHashCode()
            {
                return ((byte)Group * 0x10000) + ID;
            }
        }

        public static Dictionary<ObjKey, Vector3[]> GetSavedPosition() 
        {
            Dictionary<ObjKey, Vector3[]> r = new Dictionary<ObjKey, Vector3[]>();
            foreach (var item in DataBase.SelectedNodes.Values)
            {
                if (item is Object3D obj && item.Parent is TreeNodeGroup)
                {
                    r.Add(new ObjKey(obj.ObjLineRef, obj.Group), obj.GetObjPostion_ToMove_General());
                }
            }
            return r;
        }

        public static Dictionary<ObjKey, Vector3[]> GetSavedRotationAngles() 
        {
            Dictionary<ObjKey, Vector3[]> r = new Dictionary<ObjKey, Vector3[]>();
            foreach (var item in DataBase.SelectedNodes.Values)
            {
                if (item is Object3D obj && item.Parent is TreeNodeGroup)
                {
                    r.Add(new ObjKey(obj.ObjLineRef, obj.Group), obj.GetObjRotarionAngles_ToMove());
                }
            }
            return r;
        }

        public static Dictionary<ObjKey, Vector3[]> GetSavedScales()
        {
            Dictionary<ObjKey, Vector3[]> r = new Dictionary<ObjKey, Vector3[]>();
            foreach (var item in DataBase.SelectedNodes.Values)
            {
                if (item is Object3D obj && item.Parent is TreeNodeGroup)
                {
                    r.Add(new ObjKey(obj.ObjLineRef, obj.Group), obj.GetObjScale_ToMove());
                }
            }
            return r;
        }

        public static void MoveObjPositionXYZ(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, Camera camera, MoveDirection moveDirection, bool invert)
        {
            if (savedPos != null && savedPos.Length >= 1)
            {
                int MousePosX = e.X - oldMouseXY.X;
                int MousePosY = oldMouseXY.Y - e.Y; //invertido
                if (invert)
                {
                    MousePosX = -MousePosX;
                    MousePosY = -MousePosY;
                }

                float sensitivity = 10f * objSpeedMultiplier;

                Vector3 pos = savedPos[0];

                if (moveDirection == MoveDirection.X)
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity));
                    }
                    else
                    {
                        pos.X = savedPos[0].X + (MousePosX * sensitivity);
                    }
                }
                else if (moveDirection == MoveDirection.Y)
                {
                    pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                }
                else if (moveDirection == MoveDirection.Z)
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjFront * (MousePosY * sensitivity));
                    }
                    else
                    {
                        pos.Z = savedPos[0].Z + (MousePosY * sensitivity);
                    }
                }
                else if (moveDirection == (MoveDirection.X | MoveDirection.Y))
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity));
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                    }
                    else
                    {
                        pos.X = savedPos[0].X + (MousePosX * sensitivity);
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                    }
                }
                else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity)) + (camera.MoveObjFront * (MousePosY * sensitivity));
                    }
                    else 
                    {
                        pos.X = savedPos[0].X + (MousePosX * sensitivity);
                        pos.Z = savedPos[0].Z + (MousePosY * sensitivity);
                    }
                }
                else if (moveDirection == (MoveDirection.Y | MoveDirection.Z))
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity));
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                    }
                    else
                    {
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                        pos.Z = savedPos[0].Z + (MousePosX * sensitivity);
                    }
                }

                Vector3[] PosArr = (Vector3[])savedPos.Clone();
                PosArr[0] = pos;

                if (KeepOnGround && !(moveDirection == MoveDirection.Y) && DataBase.SelectedRoom != null)
                {
                    PosArr[0].Y = DataBase.SelectedRoom.DropToGround(PosArr[0]);
                }

                obj.SetObjPostion_ToMove_General(PosArr);

            }

        }

        public static void MoveTriggerZonePlusObjPositionXYZ(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, Camera camera, MoveDirection moveDirection, bool invert) 
        {
            if (savedPos != null && savedPos.Length >= 1)
            {
                int MousePosX = e.X - oldMouseXY.X;
                int MousePosY = oldMouseXY.Y - e.Y; //invertido
                if (invert)
                {
                    MousePosX = -MousePosX;
                    MousePosY = -MousePosY;
                }

                float sensitivity = 10f * objSpeedMultiplier;

                Vector3 pos = savedPos[0];

                if (moveDirection == MoveDirection.X)
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity));
                    }
                    else
                    {
                        pos.X = savedPos[0].X + (MousePosX * sensitivity);
                    }
                }
                else if (moveDirection == MoveDirection.Y)
                {
                    pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                }
                else if (moveDirection == MoveDirection.Z)
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjFront * (MousePosY * sensitivity));
                    }
                    else
                    {
                        pos.Z = savedPos[0].Z + (MousePosY * sensitivity);
                    }
                }
                else if (moveDirection == (MoveDirection.X | MoveDirection.Y))
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity));
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                    }
                    else
                    {
                        pos.X = savedPos[0].X + (MousePosX * sensitivity);
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                    }
                }
                else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity)) + (camera.MoveObjFront * (MousePosY * sensitivity));
                    }
                    else
                    {
                        pos.X = savedPos[0].X + (MousePosX * sensitivity);
                        pos.Z = savedPos[0].Z + (MousePosY * sensitivity);
                    }
                }
                else if (moveDirection == (MoveDirection.Y | MoveDirection.Z))
                {
                    if (MoveRelativeCamera)
                    {
                        pos = savedPos[0] + (camera.MoveObjRight * (MousePosX * sensitivity));
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                    }
                    else
                    {
                        pos.Y = savedPos[0].Y + (MousePosY * sensitivity);
                        pos.Z = savedPos[0].Z + (MousePosX * sensitivity);
                    }
                }

                Vector3[] PosArr = (Vector3[])savedPos.Clone();
                PosArr[0] = pos;

                if (KeepOnGround && !(moveDirection == MoveDirection.Y) && DataBase.SelectedRoom != null)
                {
                    PosArr[0].Y = DataBase.SelectedRoom.DropToGround(PosArr[0]);
                }

                if (savedPos.Length >= 7 && (obj.GetTriggerZoneCategory() == TriggerZoneCategory.Category01 || obj.GetTriggerZoneCategory() == TriggerZoneCategory.Category02))
                {
                    PosArr[1] = MoveZonePosition(savedPos[1], MousePosX, MousePosY, camera, moveDirection);
                    PosArr[2] = MoveZonePosition(savedPos[2], MousePosX, MousePosY, camera, moveDirection);
                    PosArr[3] = MoveZonePosition(savedPos[3], MousePosX, MousePosY, camera, moveDirection);
                    PosArr[4] = MoveZonePosition(savedPos[4], MousePosX, MousePosY, camera, moveDirection);
                    PosArr[6] = MoveZonePosition(savedPos[6], MousePosX, MousePosY, camera, moveDirection);

                    if (moveDirection.HasFlag(MoveDirection.Y))
                    {
                        Vector3 PosY = savedPos[5];
                        PosY.Y += sensitivity * MousePosY;
                        PosArr[5] = PosY;
                    }

                    if (TriggerZoneKeepOnGround && !(moveDirection == MoveDirection.Y) && DataBase.SelectedRoom != null)
                    {
                        PosArr[5].Y = DataBase.SelectedRoom.DropToGround(new Vector3(PosArr[6].X, PosArr[5].Y, PosArr[6].Z));
                    }
                }

                obj.SetObjPostion_ToMove_General(PosArr);
            }

        }

        public static void MoveObjRotationAnglesXYZ(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, MoveDirection moveDirection, bool invert) 
        {
            if (savedPos != null && savedPos.Length >= 1)
            {
                int MousePosX = e.X - oldMouseXY.X;
                if (invert)
                {
                    MousePosX = -MousePosX;
                }

                float sensitivity = 0.005f * objSpeedMultiplier;

                Vector3 angle = savedPos[0];

                if (moveDirection == MoveDirection.X)
                {
                    angle.X += sensitivity * MousePosX;
                }
                else if (moveDirection == MoveDirection.Y)
                {
                    angle.Y += sensitivity * MousePosX;
                }
                else if (moveDirection == MoveDirection.Z)
                {
                    angle.Z += sensitivity * MousePosX;
                }

                Vector3[] Arr = (Vector3[])savedPos.Clone();
                Arr[0] = angle;
                obj.SetObjRotarionAngles_ToMove(Arr);
            }
        }

        public static void MoveObjScaleXYZ(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, MoveDirection moveDirection, bool invert)
        {
            if (savedPos != null && savedPos.Length >= 1)
            {
                int MousePosX = e.X - oldMouseXY.X;
                int MousePosY = oldMouseXY.Y - e.Y; //invertido
                if (invert)
                {
                    MousePosX = -MousePosX;
                    MousePosY = -MousePosY;
                }

                float sensitivity = 0.001f * objSpeedMultiplier;

                Vector3 scale = savedPos[0];

                if (moveDirection == MoveDirection.X)
                {
                    scale.X += sensitivity * MousePosX;
                }
                else if (moveDirection == MoveDirection.Y)
                {
                    scale.Y += sensitivity * MousePosX;
                }
                else if (moveDirection == MoveDirection.Z)
                {
                    scale.Z += sensitivity * MousePosX;
                }
                else if (moveDirection.HasFlag(MoveDirection.X | MoveDirection.Y | MoveDirection.Z))
                {
                    scale.X += sensitivity * MousePosY;
                    scale.Y += sensitivity * MousePosY;
                    scale.Z += sensitivity * MousePosY;
                }

                Vector3[] Arr = (Vector3[])savedPos.Clone();
                Arr[0] = scale;
                obj.SetObjScale_ToMove(Arr);

            }
        }

        // triggerZone
        public static void MoveTriggerZonePositionXZ(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, Camera camera, MoveDirection moveDirection, bool invert, MoveObjType MoveObjTypeSelected, TriggerZoneCategory category)
        {
            if (savedPos != null && savedPos.Length >= 7)
            {
                int MousePosX = e.X - oldMouseXY.X;
                int MousePosY = oldMouseXY.Y - e.Y; // invertido
                if (invert)
                {
                    MousePosX = -MousePosX;
                    MousePosY = -MousePosY;
                }

                Vector3[] PosArr = (Vector3[])savedPos.Clone();

                if (category == TriggerZoneCategory.Category01)
                {
                    if (MoveObjTypeSelected.HasFlag(MoveObjType.__AllPointsXZ))
                    {
                        PosArr[1] = MoveZonePosition(savedPos[1], MousePosX, MousePosY, camera, moveDirection);
                        PosArr[2] = MoveZonePosition(savedPos[2], MousePosX, MousePosY, camera, moveDirection);
                        PosArr[3] = MoveZonePosition(savedPos[3], MousePosX, MousePosY, camera, moveDirection);
                        PosArr[4] = MoveZonePosition(savedPos[4], MousePosX, MousePosY, camera, moveDirection);
                        PosArr[6] = MoveZonePosition(savedPos[6], MousePosX, MousePosY, camera, moveDirection);
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__Point0XZ))
                    {
                        PosArr[1] = MoveZonePosition(savedPos[1], MousePosX, MousePosY, camera, moveDirection);
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__Point1XZ))
                    {
                        PosArr[2] = MoveZonePosition(savedPos[2], MousePosX, MousePosY, camera, moveDirection);
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__Point2XZ))
                    {
                        PosArr[3] = MoveZonePosition(savedPos[3], MousePosX, MousePosY, camera, moveDirection);
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__Point3XZ))
                    {
                        PosArr[4] = MoveZonePosition(savedPos[4], MousePosX, MousePosY, camera, moveDirection);
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__WallPoint01and12XZ))
                    {
                        
                        if (moveDirection.HasFlag(MoveDirection.X) && !moveDirection.HasFlag(MoveDirection.Z))
                        {
                            MoveZoneWall(ref PosArr[1], ref PosArr[2], MousePosX);
                        }
                        else if (moveDirection.HasFlag(MoveDirection.Z) && !moveDirection.HasFlag(MoveDirection.X))
                        {
                            MoveZoneWall(ref PosArr[2], ref PosArr[3], MousePosY);
                        }
                        else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
                        {
                            MoveZoneWallScale(ref PosArr[1], ref PosArr[2], MousePosX, savedPos[1], savedPos[2]);
                            MoveZoneWallScale(ref PosArr[2], ref PosArr[3], MousePosY, savedPos[2], savedPos[3]);
                        }
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__WallPoint12and23XZ))
                    {
                        
                        if (moveDirection.HasFlag(MoveDirection.X) && !moveDirection.HasFlag(MoveDirection.Z))
                        {
                            MoveZoneWall(ref PosArr[2], ref PosArr[3], MousePosX);
                        }
                        else if (moveDirection.HasFlag(MoveDirection.Z) && !moveDirection.HasFlag(MoveDirection.X))
                        {
                            MoveZoneWall(ref PosArr[3], ref PosArr[4], MousePosY);
                        }
                        else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
                        {
                            MoveZoneWallScale(ref PosArr[2], ref PosArr[3], MousePosX, savedPos[2], savedPos[3]);
                            MoveZoneWallScale(ref PosArr[3], ref PosArr[4], MousePosY, savedPos[3], savedPos[4]);
                        }
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__Wallpoint23and30XZ))
                    {
                        
                        if (moveDirection.HasFlag(MoveDirection.X) && !moveDirection.HasFlag(MoveDirection.Z))
                        {
                            MoveZoneWall(ref PosArr[3], ref PosArr[4], MousePosX);
                        }
                        else if (moveDirection.HasFlag(MoveDirection.Z) && !moveDirection.HasFlag(MoveDirection.X))
                        {
                            MoveZoneWall(ref PosArr[4], ref PosArr[1], MousePosY);
                        }
                        else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
                        {
                            MoveZoneWallScale(ref PosArr[3], ref PosArr[4], MousePosX, savedPos[3], savedPos[4]);
                            MoveZoneWallScale(ref PosArr[4], ref PosArr[1], MousePosY, savedPos[4], savedPos[1]);
                        }
                    }
                    else if (MoveObjTypeSelected.HasFlag(MoveObjType.__WallPoint30and01XZ))
                    {
                        
                        if (moveDirection.HasFlag(MoveDirection.X) && !moveDirection.HasFlag(MoveDirection.Z))
                        {
                            MoveZoneWall(ref PosArr[4], ref PosArr[1], MousePosX);
                        }
                        else if (moveDirection.HasFlag(MoveDirection.Z) && !moveDirection.HasFlag(MoveDirection.X))
                        {
                            MoveZoneWall(ref PosArr[1], ref PosArr[2], MousePosY);
                        }
                        else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
                        {
                            MoveZoneWallScale(ref PosArr[4], ref PosArr[1], MousePosX, savedPos[4], savedPos[1]);
                            MoveZoneWallScale(ref PosArr[1], ref PosArr[2], MousePosY, savedPos[1], savedPos[2]);
                        }
                    }

                    for (int i = 1; i <= 4; i++)
                    {
                        if (float.IsNaN(PosArr[i].X) || float.IsNaN(PosArr[i].Z) || float.IsInfinity(PosArr[i].X) || float.IsInfinity(PosArr[i].Z))
                        {
                            PosArr[i] = savedPos[i];
                        }
                    }
                   
                }
                else if (category == TriggerZoneCategory.Category02)
                {
                   PosArr[1] = MoveZonePosition(savedPos[1], MousePosX, MousePosY, camera, moveDirection);
                   PosArr[6] = MoveZonePosition(savedPos[6], MousePosX, MousePosY, camera, moveDirection);
                }

                if (TriggerZoneKeepOnGround && !(moveDirection == MoveDirection.Y) && DataBase.SelectedRoom != null
                    && ((MoveObjTypeSelected.HasFlag(MoveObjType.__AllPointsXZ) && category == TriggerZoneCategory.Category01) || (category == TriggerZoneCategory.Category02)))
                {
                    PosArr[5].Y = DataBase.SelectedRoom.DropToGround(new Vector3(PosArr[6].X, PosArr[5].Y, PosArr[6].Z));
                }

                obj.SetObjPostion_ToMove_General(PosArr);
            }
        }

        private static Vector3 MoveZonePosition(Vector3 savedPos, int MousePosX, int MousePosY, Camera camera, MoveDirection moveDirection) 
        {
            Vector3 vec = savedPos;
            float sensitivity = 10f * objSpeedMultiplier;

            if (moveDirection == MoveDirection.X)
            {
                if (MoveRelativeCamera)
                {
                    vec = savedPos + (camera.MoveObjRight * (MousePosX * sensitivity));
                }
                else
                {
                    vec.X = savedPos.X + (MousePosX * sensitivity);
                }
            }
            else if (moveDirection == MoveDirection.Z)
            {
                if (MoveRelativeCamera)
                {
                    vec = savedPos + (camera.MoveObjFront * (MousePosY * sensitivity));
                }
                else
                {
                    vec.Z = savedPos.Z + (MousePosY * sensitivity);
                }
            }
            else if (moveDirection == (MoveDirection.X | MoveDirection.Z))
            {
                if (MoveRelativeCamera)
                {
                    vec = savedPos + (camera.MoveObjRight * (MousePosX * sensitivity)) + (camera.MoveObjFront * (MousePosY * sensitivity));
                }
                else
                {
                    vec.X = savedPos.X + (MousePosX * sensitivity);
                    vec.Z = savedPos.Z + (MousePosY * sensitivity);
                }
            }
            return vec;
        }

        private static void MoveZoneWall(ref Vector3 pointA, ref Vector3 pointB, int MousePos) 
        {
            float sensitivity = 10f * objSpeedMultiplier;
            Vector3 direction = Vector3.Normalize((pointA - pointB) / 100);
            float yaw = (float)Math.Atan2(direction.Z, direction.X);
            Vector3 side = Vector3.Zero;
            side.X = (float)Math.Cos(yaw);
            side.Z = (float)Math.Sin(yaw);
            side = Vector3.Normalize(side);
            Vector3 front  = Vector3.Normalize(Vector3.Cross(side, Vector3.UnitY));
            pointA += front * sensitivity * MousePos;
            pointB += front * sensitivity * MousePos;
        }

        private static void MoveZoneWallScale(ref Vector3 pointA, ref Vector3 pointB, int MousePos, Vector3 oldPointA, Vector3 oldPointB)
        {
            float sensitivity = 10f * objSpeedMultiplier;
            Vector3 direction = Vector3.Normalize((oldPointA - oldPointB) / 100);
            float yaw = (float)Math.Atan2(direction.Z, direction.X);
            Vector3 side = Vector3.Zero;
            side.X = (float)Math.Cos(yaw);
            side.Z = (float)Math.Sin(yaw);
            side = Vector3.Normalize(side);
            Vector3 front = Vector3.Normalize(Vector3.Cross(side, Vector3.UnitY));
            pointA += front * sensitivity * MousePos;
            pointB += front * sensitivity * MousePos;
        }

        public static void MoveTriggerZonePositionY(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, bool invert)
        {
            if (savedPos != null && savedPos.Length >= 6)
            {
                int MousePosY = oldMouseXY.Y - e.Y; // pode ser invertido
                if (invert)
                {
                    MousePosY = -MousePosY;
                }

                float sensitivity = 10f * objSpeedMultiplier;

                Vector3 PosY = savedPos[5];
                PosY.Y += sensitivity * MousePosY;

                Vector3[] Arr = (Vector3[])savedPos.Clone();
                Arr[5] = PosY;

                obj.SetObjPostion_ToMove_General(Arr);
            }
        }

        public static void MoveTriggerZoneHeight(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, bool invert)
        {
            if (savedPos != null && savedPos.Length >= 6)
            {
                int MousePosX = e.X - oldMouseXY.X;
                if (invert)
                {
                    MousePosX = -MousePosX;
                }

                float sensitivity = 5f * objSpeedMultiplier;

                Vector3 Height = savedPos[5];
                Height.Z += sensitivity * MousePosX;

                Vector3[] Arr = (Vector3[])savedPos.Clone();
                Arr[5] = Height;
                obj.SetObjPostion_ToMove_General(Arr);
            }
        }

        public static void MoveZoneRotate(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, bool invert) // triggerZone And AshleyZone
        {
            if (savedPos != null && savedPos.Length >= 7)
            {
                int MousePosX = e.X - oldMouseXY.X;
                if (invert)
                {
                    MousePosX = -MousePosX;
                }

                float sensitivity = 0.002f * objSpeedMultiplier;

                Vector3[] Arr = (Vector3[])savedPos.Clone();

                Matrix4 rotation = Matrix4.CreateRotationY(sensitivity * MousePosX);

                Arr[1] = (new Vector4(savedPos[1] - savedPos[6], 1) * rotation).Xyz + savedPos[6];
                Arr[2] = (new Vector4(savedPos[2] - savedPos[6], 1) * rotation).Xyz + savedPos[6];
                Arr[3] = (new Vector4(savedPos[3] - savedPos[6], 1) * rotation).Xyz + savedPos[6];
                Arr[4] = (new Vector4(savedPos[4] - savedPos[6], 1) * rotation).Xyz + savedPos[6];

                obj.SetObjPostion_ToMove_General(Arr);

            }
        }

        public static void MoveTriggerZoneScale(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, bool invert, TriggerZoneCategory category)
        {
            if (savedPos != null && savedPos.Length >= 6)
            {
                int MousePosX = e.X - oldMouseXY.X;
                if (invert)
                {
                    MousePosX = -MousePosX;
                }
                Vector3[] Arr = (Vector3[])savedPos.Clone();

                if (category == TriggerZoneCategory.Category01)
                {
                    MoveZoneWallScale(ref Arr[1], ref Arr[2], MousePosX, savedPos[1], savedPos[2]);
                    MoveZoneWallScale(ref Arr[2], ref Arr[3], MousePosX, savedPos[2], savedPos[3]);
                    MoveZoneWallScale(ref Arr[3], ref Arr[4], MousePosX, savedPos[3], savedPos[4]);
                    MoveZoneWallScale(ref Arr[4], ref Arr[1], MousePosX, savedPos[4], savedPos[1]);

                    for (int i = 1; i <= 4; i++)
                    {
                        if (float.IsNaN(Arr[i].X) || float.IsNaN(Arr[i].Z) || float.IsInfinity(Arr[i].X) || float.IsInfinity(Arr[i].Z))
                        {
                            Arr[i] = savedPos[i];
                        }
                    }
                }
                else if (category == TriggerZoneCategory.Category02)
                {
                    float sensitivity = 5f * objSpeedMultiplier;
                    Vector3 Scale = savedPos[5];
                    Scale.X += sensitivity * MousePosX;
                    Arr[5] = Scale;
                }

                obj.SetObjPostion_ToMove_General(Arr);

            }

        }

        public static void MoveAshleyZoneScale(Object3D obj, MouseEventArgs e, Point oldMouseXY, Vector3[] savedPos, bool invert)
        {
            if (savedPos != null && savedPos.Length >= 5)
            {
                int MousePosX = e.X - oldMouseXY.X;
                if (invert)
                {
                    MousePosX = -MousePosX;
                }
                Vector3[] Arr = (Vector3[])savedPos.Clone();
                MoveZoneWallScale(ref Arr[1], ref Arr[2], MousePosX, savedPos[1], savedPos[2]);
                MoveZoneWallScale(ref Arr[2], ref Arr[3], MousePosX, savedPos[2], savedPos[3]);
                MoveZoneWallScale(ref Arr[3], ref Arr[4], MousePosX, savedPos[3], savedPos[4]);
                MoveZoneWallScale(ref Arr[4], ref Arr[1], MousePosX, savedPos[4], savedPos[0]);
                for (int i = 1; i <= 4; i++)
                {
                    if (float.IsNaN(Arr[i].X) || float.IsNaN(Arr[i].Z) || float.IsInfinity(Arr[i].X) || float.IsInfinity(Arr[i].Z))
                    {
                        Arr[i] = savedPos[i];
                    }
                }
                obj.SetObjPostion_ToMove_General(Arr);
            }
        }


    }
}
