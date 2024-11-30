using OpenTK;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SimpleEndianBinaryIO;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    /// <summary>
    /// implementação uteis
    /// </summary>
    public abstract class BaseGroup
    {
        /// <summary>
        /// tem que retornar a referencia
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected abstract byte[] GetInternalLine(ushort ID);
        /// <summary>
        /// return Endianness
        /// </summary>
        /// <returns></returns>
        protected abstract Endianness GetEndianness();

        protected void SetBaseMethods(BaseMethods Methods) 
        {
            Methods.GetEndianness = GetEndianness;
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

        protected byte ReturnByteFromPosition(ushort ID, int FromPosition)
        {
            return GetInternalLine(ID)[FromPosition];
        }

        protected void SetByteFromPosition(ushort ID, int FromPosition, byte value)
        {
            GetInternalLine(ID)[FromPosition] = value;
        }

        protected sbyte ReturnSbyteFromPosition(ushort ID, int FromPosition)
        {
            return (sbyte)GetInternalLine(ID)[FromPosition];
        }

        protected void SetSbyteFromPosition(ushort ID, int FromPosition, sbyte value)
        {
            GetInternalLine(ID)[FromPosition] = (byte)value;
        }

        protected short ReturnShortFromPosition(ushort ID, int FromPosition)
        {
            return EndianBitConverter.ToInt16(GetInternalLine(ID), FromPosition, GetEndianness());
        }

        protected void SetShortFromPosition(ushort ID, int FromPosition, short value)
        {
            EndianBitConverter.GetBytes(value, GetEndianness()).CopyTo(GetInternalLine(ID), FromPosition);
        }

        protected ushort ReturnUshortFromPosition(ushort ID, int FromPosition)
        {
            return EndianBitConverter.ToUInt16(GetInternalLine(ID), FromPosition, GetEndianness());
        }

        protected void SetUshortFromPosition(ushort ID, int FromPosition, ushort value)
        {
            EndianBitConverter.GetBytes(value, GetEndianness()).CopyTo(GetInternalLine(ID), FromPosition);
        }

        protected int ReturnIntFromPosition(ushort ID, int FromPosition)
        {
            return EndianBitConverter.ToInt32(GetInternalLine(ID), FromPosition, GetEndianness());
        }

        protected void SetIntFromPosition(ushort ID, int FromPosition, int value)
        {
            EndianBitConverter.GetBytes(value, GetEndianness()).CopyTo(GetInternalLine(ID), FromPosition);
        }

        protected uint ReturnUintFromPosition(ushort ID, int FromPosition)
        {
            return EndianBitConverter.ToUInt32(GetInternalLine(ID), FromPosition, GetEndianness());
        }

        protected void SetUintFromPosition(ushort ID, int FromPosition, uint value)
        {
            EndianBitConverter.GetBytes(value, GetEndianness()).CopyTo(GetInternalLine(ID), FromPosition);
        }

        protected float ReturnFloatFromPosition(ushort ID, int FromPosition)
        {
            return EndianBitConverter.ToSingle(GetInternalLine(ID), FromPosition, GetEndianness());
        }

        protected void SetFloatFromPosition(ushort ID, int FromPosition, float value)
        {
            EndianBitConverter.GetBytes(value, GetEndianness()).CopyTo(GetInternalLine(ID), FromPosition);
        }

        protected byte[] ReturnByteArrayFromPosition(ushort ID, int FromPosition, int Count)
        {
            return GetInternalLine(ID).Skip(FromPosition).Take(Count).ToArray();
        }

        protected void SetByteArrayFromPosition(ushort ID, int FromPosition, byte[] value)
        {
            value.CopyTo(GetInternalLine(ID), FromPosition);
        }
    }
}
