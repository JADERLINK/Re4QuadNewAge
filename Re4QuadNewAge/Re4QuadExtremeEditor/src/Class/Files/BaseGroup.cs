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
    /// implementação uteis
    /// </summary>
    public abstract class BaseGroup
    {
        /// <summary>
        /// tem que retorna a referencia
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected abstract byte[] GetInternalLine(ushort ID);

        protected void SetBaseMethods(BaseMethods Methods) 
        {
            Methods.ReturnByteFromPosition = ReturnByteFromPosition;
            Methods.SetByteFromPosition = SetByteFromPosition;
            Methods.SetSbyteFromPosition = SetSbyteFromPosition;
            Methods.ReturnSbyteFromPosition = ReturnSbyteFromPosition;
            Methods.ReturnShortFromPosition = ReturnShortFromPosition;
            Methods.SetShortFromPosition = SetShortFromPosition;
            Methods.ReturnUshortFromPosition = ReturnUshortFromPosition;
            Methods.SetUshortFromPosition = SetUshortFromPosition;
            Methods.ReturnIntFromPosition = ReturnIntFromPosition;
            Methods.SetIntFromPosition = SetIntFromPosition;
            Methods.ReturnUintFromPosition = ReturnUintFromPosition;
            Methods.SetUintFromPosition = SetUintFromPosition;
            Methods.ReturnFloatFromPosition = ReturnFloatFromPosition;
            Methods.SetFloatFromPosition = SetFloatFromPosition;
            Methods.ReturnByteArrayFromPosition = ReturnByteArrayFromPosition;
            Methods.SetByteArrayFromPosition = SetByteArrayFromPosition;
        }

        private byte ReturnByteFromPosition(ushort ID, int FromPosition)
        {
            return GetInternalLine(ID)[FromPosition];
        }

        private void SetByteFromPosition(ushort ID, int FromPosition, byte value)
        {
            GetInternalLine(ID)[FromPosition] = value;
        }

        private sbyte ReturnSbyteFromPosition(ushort ID, int FromPosition)
        {
            return (sbyte)GetInternalLine(ID)[FromPosition];
        }

        private void SetSbyteFromPosition(ushort ID, int FromPosition, sbyte value)
        {
            GetInternalLine(ID)[FromPosition] = (byte)value;
        }

        private short ReturnShortFromPosition(ushort ID, int FromPosition)
        {
            return BitConverter.ToInt16(GetInternalLine(ID), FromPosition);
        }

        private void SetShortFromPosition(ushort ID, int FromPosition, short value)
        {
            BitConverter.GetBytes(value).CopyTo(GetInternalLine(ID), FromPosition);
        }

        private ushort ReturnUshortFromPosition(ushort ID, int FromPosition)
        {
            return BitConverter.ToUInt16(GetInternalLine(ID), FromPosition);
        }

        private void SetUshortFromPosition(ushort ID, int FromPosition, ushort value)
        {
            BitConverter.GetBytes(value).CopyTo(GetInternalLine(ID), FromPosition);
        }


        private int ReturnIntFromPosition(ushort ID, int FromPosition)
        {
            return BitConverter.ToInt32(GetInternalLine(ID), FromPosition);
        }

        private void SetIntFromPosition(ushort ID, int FromPosition, int value)
        {
            BitConverter.GetBytes(value).CopyTo(GetInternalLine(ID), FromPosition);
        }

        private uint ReturnUintFromPosition(ushort ID, int FromPosition)
        {
            return BitConverter.ToUInt32(GetInternalLine(ID), FromPosition);
        }

        private void SetUintFromPosition(ushort ID, int FromPosition, uint value)
        {
            BitConverter.GetBytes(value).CopyTo(GetInternalLine(ID), FromPosition);
        }

        private float ReturnFloatFromPosition(ushort ID, int FromPosition)
        {
            return BitConverter.ToSingle(GetInternalLine(ID), FromPosition);
        }

        private void SetFloatFromPosition(ushort ID, int FromPosition, float value)
        {
            BitConverter.GetBytes(value).CopyTo(GetInternalLine(ID), FromPosition);
        }

        private byte[] ReturnByteArrayFromPosition(ushort ID, int FromPosition, int Count)
        {
            return GetInternalLine(ID).Skip(FromPosition).Take(Count).ToArray();
        }

        private void SetByteArrayFromPosition(ushort ID, int FromPosition, byte[] value)
        {
            value.CopyTo(GetInternalLine(ID), FromPosition);
        }
    }
}
