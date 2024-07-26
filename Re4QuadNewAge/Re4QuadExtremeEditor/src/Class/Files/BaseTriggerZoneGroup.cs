using OpenTK;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    /// <summary>
    /// implementação universal da TriggerZone
    /// </summary>
    public abstract class BaseTriggerZoneGroup : BaseGroup
    {
        /// <summary>
        /// inicio da TiggerZone na line
        /// </summary>
        /// <returns></returns>
        protected abstract int GetTriggerZoneStartIndex();

        protected void SetTriggerZoneMethods(BaseTriggerZoneMethods Methods)
        {
            Methods.GetTriggerZoneCategory = GetSpecialZoneCategory;
            Methods.ReturnUnknown_GH = ReturnUnknown_GH;
            Methods.SetUnknown_GH = SetUnknown_GH;
            Methods.ReturnCategory = ReturnCategory;
            Methods.SetCategory = SetCategory;
            Methods.ReturnUnknown_GK = ReturnUnknown_GK;
            Methods.SetUnknown_GK = SetUnknown_GK;
            Methods.ReturnTriggerZoneCorner0_X = ReturnTriggerZoneCorner0_X;
            Methods.ReturnTriggerZoneCorner0_Z = ReturnTriggerZoneCorner0_Z;
            Methods.ReturnTriggerZoneCorner1_X = ReturnTriggerZoneCorner1_X;
            Methods.ReturnTriggerZoneCorner1_Z = ReturnTriggerZoneCorner1_Z;
            Methods.ReturnTriggerZoneCorner2_X = ReturnTriggerZoneCorner2_X;
            Methods.ReturnTriggerZoneCorner2_Z = ReturnTriggerZoneCorner2_Z;
            Methods.ReturnTriggerZoneCorner3_X = ReturnTriggerZoneCorner3_X;
            Methods.ReturnTriggerZoneCorner3_Z = ReturnTriggerZoneCorner3_Z;
            Methods.ReturnTriggerZoneTrueY = ReturnTriggerZoneTrueY;
            Methods.ReturnTriggerZoneMoreHeight = ReturnTriggerZoneMoreHeight;
            Methods.ReturnTriggerZoneCircleRadius = ReturnTriggerZoneCircleRadius;
            Methods.ReturnTriggerZoneCorner0_X_Hex = ReturnTriggerZoneCorner0_X_Hex;
            Methods.ReturnTriggerZoneCorner0_Z_Hex = ReturnTriggerZoneCorner0_Z_Hex;
            Methods.ReturnTriggerZoneCorner1_X_Hex = ReturnTriggerZoneCorner1_X_Hex;
            Methods.ReturnTriggerZoneCorner1_Z_Hex = ReturnTriggerZoneCorner1_Z_Hex;
            Methods.ReturnTriggerZoneCorner2_X_Hex = ReturnTriggerZoneCorner2_X_Hex;
            Methods.ReturnTriggerZoneCorner2_Z_Hex = ReturnTriggerZoneCorner2_Z_Hex;
            Methods.ReturnTriggerZoneCorner3_X_Hex = ReturnTriggerZoneCorner3_X_Hex;
            Methods.ReturnTriggerZoneCorner3_Z_Hex = ReturnTriggerZoneCorner3_Z_Hex;
            Methods.ReturnTriggerZoneTrueY_Hex = ReturnTriggerZoneTrueY_Hex;
            Methods.ReturnTriggerZoneMoreHeight_Hex = ReturnTriggerZoneMoreHeight_Hex;
            Methods.ReturnTriggerZoneCircleRadius_Hex = ReturnTriggerZoneCircleRadius_Hex;
            Methods.SetTriggerZoneCorner0_X = SetTriggerZoneCorner0_X;
            Methods.SetTriggerZoneCorner0_Z = SetTriggerZoneCorner0_Z;
            Methods.SetTriggerZoneCorner1_X = SetTriggerZoneCorner1_X;
            Methods.SetTriggerZoneCorner1_Z = SetTriggerZoneCorner1_Z;
            Methods.SetTriggerZoneCorner2_X = SetTriggerZoneCorner2_X;
            Methods.SetTriggerZoneCorner2_Z = SetTriggerZoneCorner2_Z;
            Methods.SetTriggerZoneCorner3_X = SetTriggerZoneCorner3_X;
            Methods.SetTriggerZoneCorner3_Z = SetTriggerZoneCorner3_Z;
            Methods.SetTriggerZoneTrueY = SetTriggerZoneTrueY;
            Methods.SetTriggerZoneMoreHeight = SetTriggerZoneMoreHeight;
            Methods.SetTriggerZoneCircleRadius = SetTriggerZoneCircleRadius;
            Methods.SetTriggerZoneCorner0_X_Hex = SetTriggerZoneCorner0_X_Hex;
            Methods.SetTriggerZoneCorner0_Z_Hex = SetTriggerZoneCorner0_Z_Hex;
            Methods.SetTriggerZoneCorner1_X_Hex = SetTriggerZoneCorner1_X_Hex;
            Methods.SetTriggerZoneCorner1_Z_Hex = SetTriggerZoneCorner1_Z_Hex;
            Methods.SetTriggerZoneCorner2_X_Hex = SetTriggerZoneCorner2_X_Hex;
            Methods.SetTriggerZoneCorner2_Z_Hex = SetTriggerZoneCorner2_Z_Hex;
            Methods.SetTriggerZoneCorner3_X_Hex = SetTriggerZoneCorner3_X_Hex;
            Methods.SetTriggerZoneCorner3_Z_Hex = SetTriggerZoneCorner3_Z_Hex;
            Methods.SetTriggerZoneTrueY_Hex = SetTriggerZoneTrueY_Hex;
            Methods.SetTriggerZoneMoreHeight_Hex = SetTriggerZoneMoreHeight_Hex;
            Methods.SetTriggerZoneCircleRadius_Hex = SetTriggerZoneCircleRadius_Hex;
        }

        protected void SetTriggerZoneMethodsForGL(BaseTriggerZoneMethodsForGL MethodsForGL) 
        {
            MethodsForGL.GetTriggerZoneMatrix4 = GetTriggerZoneMatrix4;
            MethodsForGL.GetTriggerZone = GetTriggerZone;
            MethodsForGL.GetCircleTriggerZone = GetCircleTriggerZone;
            MethodsForGL.GetZoneCategory = GetSpecialZoneCategory;
        }

        #region MoveMethods 

        protected Vector3 GetTriggerZonePos_ToCamera(ushort ID) 
        {
            Vector3 position = Vector3.Zero;
            var TriggerZone = GetTriggerZone(ID);
            float Xmin = TriggerZone[0].X;
            float Zmin = TriggerZone[0].Y;
            float Xmax = TriggerZone[0].X;
            float Zmax = TriggerZone[0].Y;
            for (int i = 1; i <= 3; i++)
            {
                if (TriggerZone[i].X < Xmin)
                {
                    Xmin = TriggerZone[i].X;
                }
                if (TriggerZone[i].Y < Zmin)
                {
                    Zmin = TriggerZone[i].Y;
                }
                if (TriggerZone[i].X > Xmax)
                {
                    Xmax = TriggerZone[i].X;
                }
                if (TriggerZone[i].Y > Zmax)
                {
                    Zmax = TriggerZone[i].Y;
                }
            }

            position.X = Xmin + ((Xmax - Xmin) / 2);
            position.Z = Zmin + ((Zmax - Zmin) / 2);

            float Ymin = TriggerZone[4].X;
            float Ymax = TriggerZone[4].Y;
            if (TriggerZone[4].Y < Ymin)
            {
                Ymin = TriggerZone[4].Y;
                Ymax = TriggerZone[4].X;
            }
            position.Y = Ymin + ((Ymax - Ymin) / 2);

            Utils.ToCameraCheckValue(ref position);
            return position;

        }

        protected void GetTriggerZonePos_ToMove_General(ushort ID, ref Vector3[] pos) 
        {
            pos[5] = new Vector3(ReturnTriggerZoneCircleRadius(ID), ReturnTriggerZoneTrueY(ID), ReturnTriggerZoneMoreHeight(ID));

            TriggerZoneCategory category = GetSpecialZoneCategory(ID);

            if (category == TriggerZoneCategory.Category01)
            {
                pos[1] = new Vector3(ReturnTriggerZoneCorner0_X(ID), 0, ReturnTriggerZoneCorner0_Z(ID));
                pos[2] = new Vector3(ReturnTriggerZoneCorner1_X(ID), 0, ReturnTriggerZoneCorner1_Z(ID));
                pos[3] = new Vector3(ReturnTriggerZoneCorner2_X(ID), 0, ReturnTriggerZoneCorner2_Z(ID));
                pos[4] = new Vector3(ReturnTriggerZoneCorner3_X(ID), 0, ReturnTriggerZoneCorner3_Z(ID));
            }
            else if (category == TriggerZoneCategory.Category02)
            {
                pos[1] = new Vector3(ReturnTriggerZoneCorner0_X(ID), 0, ReturnTriggerZoneCorner0_Z(ID));
                pos[2] = pos[1];
                pos[3] = pos[1];
                pos[4] = pos[1];
            }
            else
            {
                pos[1] = Vector3.Zero;
                pos[2] = Vector3.Zero;
                pos[3] = Vector3.Zero;
                pos[4] = Vector3.Zero;
            }

            // center
            float Xmin = pos[1].X;
            float Zmin = pos[1].Z;
            float Xmax = pos[1].X;
            float Zmax = pos[1].Z;
            for (int i = 2; i <= 4; i++)
            {
                if (pos[i].X < Xmin)
                {
                    Xmin = pos[i].X;
                }
                if (pos[i].Z < Zmin)
                {
                    Zmin = pos[i].Z;
                }
                if (pos[i].X > Xmax)
                {
                    Xmax = pos[i].X;
                }
                if (pos[i].Z > Zmax)
                {
                    Zmax = pos[i].Z;
                }
            }
            Vector3 center = Vector3.Zero;
            center.X = Xmin + ((Xmax - Xmin) / 2);
            center.Z = Zmin + ((Zmax - Zmin) / 2);
            pos[6] = center;
        }


        protected void SetTriggerZonePos_ToMove_General(ushort ID, Vector3[] value) 
        {
            if (value != null && value.Length >= 6)
            {
                TriggerZoneCategory category = GetSpecialZoneCategory(ID);

                if (category == TriggerZoneCategory.Category01 || category == TriggerZoneCategory.Category02)
                {
                    SetTriggerZoneCircleRadius(ID, value[5].X);
                    SetTriggerZoneTrueY(ID, value[5].Y);
                    SetTriggerZoneMoreHeight(ID, value[5].Z);

                    SetTriggerZoneCorner0_X(ID, value[1].X);
                    SetTriggerZoneCorner0_Z(ID, value[1].Z);

                    if (category == TriggerZoneCategory.Category01)
                    {
                        SetTriggerZoneCorner1_X(ID, value[2].X);
                        SetTriggerZoneCorner1_Z(ID, value[2].Z);
                        SetTriggerZoneCorner2_X(ID, value[3].X);
                        SetTriggerZoneCorner2_Z(ID, value[3].Z);
                        SetTriggerZoneCorner3_X(ID, value[4].X);
                        SetTriggerZoneCorner3_Z(ID, value[4].Z);
                    }
                }
            }
        }

        #endregion


        #region TriggerZone
        protected byte ReturnUnknown_GH(ushort ID)
        {
            return GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x00];
        }

        protected void SetUnknown_GH(ushort ID, byte value)
        {
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x00] = value;
        }

        protected byte ReturnCategory(ushort ID)
        {
            return GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x01];
        }

        protected TriggerZoneCategory GetSpecialZoneCategory(ushort ID)
        {
            byte Category = GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x01];
            if (Category < 3)
            {
                return (TriggerZoneCategory)Category;
            }
            return TriggerZoneCategory.AnotherValue;
        }


        protected void SetCategory(ushort ID, byte value)
        {
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x01] = value;
        }


        protected byte[] ReturnUnknown_GK(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x02];
            b[1] = GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x03];
            return b;
        }

        protected void SetUnknown_GK(ushort ID, byte[] value)
        {
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x02] = value[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x03] = value[1];
        }


        //TriggerZone Return uint
        protected uint ReturnTriggerZoneTrueY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x04);
        }

        protected uint ReturnTriggerZoneMoreHeight_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x08);
        }

        protected uint ReturnTriggerZoneCircleRadius_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x0C);
        }

        protected uint ReturnTriggerZoneCorner0_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x10);
        }

        protected uint ReturnTriggerZoneCorner0_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x14);
        }

        protected uint ReturnTriggerZoneCorner1_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x18);
        }

        protected uint ReturnTriggerZoneCorner1_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x1C);
        }

        protected uint ReturnTriggerZoneCorner2_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x20);
        }

        protected uint ReturnTriggerZoneCorner2_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x24);
        }

        protected uint ReturnTriggerZoneCorner3_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x28);
        }

        protected uint ReturnTriggerZoneCorner3_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x2C);
        }

        //TriggerZone Set uint
        protected void SetTriggerZoneTrueY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x04] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x05] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x06] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x07] = b[3];
        }

        protected void SetTriggerZoneMoreHeight_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x08] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x09] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0A] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0B] = b[3];
        }

        protected void SetTriggerZoneCircleRadius_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0C] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0D] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0E] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0F] = b[3];
        }

        protected void SetTriggerZoneCorner0_X_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x10] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x11] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x12] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x13] = b[3];
        }

        protected void SetTriggerZoneCorner0_Z_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x14] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x15] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x16] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x17] = b[3];
        }

        protected void SetTriggerZoneCorner1_X_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x18] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x19] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1A] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1B] = b[3];
        }

        protected void SetTriggerZoneCorner1_Z_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1C] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1D] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1E] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1F] = b[3];
        }

        protected void SetTriggerZoneCorner2_X_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x20] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x21] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x22] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x23] = b[3];
        }

        protected void SetTriggerZoneCorner2_Z_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x24] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x25] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x26] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x27] = b[3];
        }

        protected void SetTriggerZoneCorner3_X_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x28] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x29] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2A] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2B] = b[3];
        }

        protected void SetTriggerZoneCorner3_Z_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2C] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2D] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2E] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2F] = b[3];
        }

        //TriggerZone Return float
        protected float ReturnTriggerZoneTrueY(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x04);
        }

        protected float ReturnTriggerZoneMoreHeight(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x08);
        }

        protected float ReturnTriggerZoneCircleRadius(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x0C);
        }

        protected float ReturnTriggerZoneCorner0_X(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x10);
        }

        protected float ReturnTriggerZoneCorner0_Z(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x14);
        }

        protected float ReturnTriggerZoneCorner1_X(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x18);
        }

        protected float ReturnTriggerZoneCorner1_Z(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x1C);
        }

        protected float ReturnTriggerZoneCorner2_X(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x20);
        }

        protected float ReturnTriggerZoneCorner2_Z(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x24);
        }

        protected float ReturnTriggerZoneCorner3_X(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x28);
        }

        protected float ReturnTriggerZoneCorner3_Z(ushort ID)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), GetTriggerZoneStartIndex() + 0x2C);
        }

        //TriggerZone Set float
        protected void SetTriggerZoneTrueY(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x04] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x05] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x06] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x07] = b[3];
        }

        protected void SetTriggerZoneMoreHeight(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x08] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x09] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0A] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0B] = b[3];
        }

        protected void SetTriggerZoneCircleRadius(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0C] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0D] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0E] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x0F] = b[3];
        }

        protected void SetTriggerZoneCorner0_X(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x10] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x11] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x12] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x13] = b[3];
        }

        protected void SetTriggerZoneCorner0_Z(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x14] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x15] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x16] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x17] = b[3];
        }

        protected void SetTriggerZoneCorner1_X(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x18] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x19] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1A] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1B] = b[3];
        }

        protected void SetTriggerZoneCorner1_Z(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1C] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1D] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1E] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x1F] = b[3];
        }

        protected void SetTriggerZoneCorner2_X(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x20] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x21] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x22] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x23] = b[3];
        }

        protected void SetTriggerZoneCorner2_Z(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x24] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x25] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x26] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x27] = b[3];
        }

        protected void SetTriggerZoneCorner3_X(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x28] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x29] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2A] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2B] = b[3];
        }

        protected void SetTriggerZoneCorner3_Z(ushort ID, float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2C] = b[0];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2D] = b[1];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2E] = b[2];
            GetInternalLine(ID)[GetTriggerZoneStartIndex() + 0x2F] = b[3];
        }

        #endregion

        #region MethodsForGL

        /// <summary>
        /// <para>ordem dos valores na matrix</para>
        /// <para>[0] x = Corner0.x, y = TrueY, z = Corner0.z, w = ?</para>
        /// <para>[1] x = Corner1.x, y = MoreHeight, z = Corner1.z, w = ?</para>
        /// <para>[2] x = Corner2.x, y = CircleRadius, z = Corner2.z, w = ?</para>
        /// <para>[3] x = Corner3.x, y = MoreHeight + TrueY, z = Corner3.z, w = ?</para>
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected Matrix4 GetTriggerZoneMatrix4(ushort ID)
        {
            return new Matrix4(
                ReturnTriggerZoneCorner0_X(ID) / 100f,
                ReturnTriggerZoneTrueY(ID) / 100f,
                ReturnTriggerZoneCorner0_Z(ID) / 100f,
                0f,
                ReturnTriggerZoneCorner1_X(ID) / 100f,
                ReturnTriggerZoneMoreHeight(ID) / 100f,
                ReturnTriggerZoneCorner1_Z(ID) / 100f,
                0f,
                ReturnTriggerZoneCorner2_X(ID) / 100f,
                ReturnTriggerZoneCircleRadius(ID) / 100f,
                ReturnTriggerZoneCorner2_Z(ID) / 100f,
                0f,
                ReturnTriggerZoneCorner3_X(ID) / 100f,
                (ReturnTriggerZoneMoreHeight(ID) + ReturnTriggerZoneTrueY(ID)) / 100f,
                ReturnTriggerZoneCorner3_Z(ID) / 100f,
                0f);
        }

        /// <summary>
        /// <para>ordem dos vector2</para>
        /// <para>[0] point0</para>
        /// <para>[1] point1</para>
        /// <para>[2] point2</para>
        /// <para>[3] point3</para>
        /// <para>[4] X = ReturnTriggerZoneTrueY, Y = ReturnTriggerZoneTrueY + ReturnTriggerZoneMoreHeight</para>
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected Vector2[] GetTriggerZone(ushort ID)
        {
            Vector2[] v = new Vector2[5];
            TriggerZoneCategory category = GetSpecialZoneCategory(ID);
            if (category == TriggerZoneCategory.Category01)
            {
                v[0] = new Vector2(ReturnTriggerZoneCorner0_X(ID) / 100f, ReturnTriggerZoneCorner0_Z(ID) / 100f);
                v[1] = new Vector2(ReturnTriggerZoneCorner1_X(ID) / 100f, ReturnTriggerZoneCorner1_Z(ID) / 100f);
                v[2] = new Vector2(ReturnTriggerZoneCorner2_X(ID) / 100f, ReturnTriggerZoneCorner2_Z(ID) / 100f);
                v[3] = new Vector2(ReturnTriggerZoneCorner3_X(ID) / 100f, ReturnTriggerZoneCorner3_Z(ID) / 100f);
                v[4] = new Vector2(ReturnTriggerZoneTrueY(ID) / 100f, (ReturnTriggerZoneMoreHeight(ID) + ReturnTriggerZoneTrueY(ID)) / 100f);
            }
            else if (category == TriggerZoneCategory.Category02)
            {
                Vector2 temp = new Vector2(ReturnTriggerZoneCorner0_X(ID) / 100f, ReturnTriggerZoneCorner0_Z(ID) / 100f);
                float Dist = (ReturnTriggerZoneCircleRadius(ID) / 100f);
                v[0] = new Vector2(temp.X - Dist, temp.Y - Dist);
                v[1] = new Vector2(temp.X - Dist, temp.Y + Dist);
                v[2] = new Vector2(temp.X + Dist, temp.Y + Dist);
                v[3] = new Vector2(temp.X + Dist, temp.Y - Dist);
                v[4] = new Vector2(ReturnTriggerZoneTrueY(ID) / 100f, (ReturnTriggerZoneMoreHeight(ID) + ReturnTriggerZoneTrueY(ID)) / 100f);
            }
            else
            {
                v[0] = Vector2.Zero;
                v[1] = Vector2.Zero;
                v[2] = Vector2.Zero;
                v[3] = Vector2.Zero;
                v[4] = Vector2.Zero;
            }
            return v;
        }

        /// <summary>
        /// <para>ordem dos vector2</para>
        /// <para>[0] Center Point</para>
        /// <para>[1] X = ReturnTriggerZoneTrueY, Y = ReturnTriggerZoneTrueY + ReturnTriggerZoneMoreHeight</para>
        /// <para>[2] X = ReturnTriggerZoneCircleRadius, Y = 0</para>
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected Vector2[] GetCircleTriggerZone(ushort ID)
        {
            Vector2[] v = new Vector2[3];
            TriggerZoneCategory category = GetSpecialZoneCategory(ID);
            if (category == TriggerZoneCategory.Category02)
            {
                v[0] = new Vector2(ReturnTriggerZoneCorner0_X(ID) / 100f, ReturnTriggerZoneCorner0_Z(ID) / 100f);
                v[1] = new Vector2(ReturnTriggerZoneTrueY(ID) / 100f, (ReturnTriggerZoneMoreHeight(ID) + ReturnTriggerZoneTrueY(ID)) / 100f);
                v[2] = new Vector2(ReturnTriggerZoneCircleRadius(ID) / 100f, 0);
            }
            else
            {
                v[0] = Vector2.Zero;
                v[1] = Vector2.Zero;
                v[2] = Vector2.Zero;
            }
            return v;
        }

        #endregion

    }
}
