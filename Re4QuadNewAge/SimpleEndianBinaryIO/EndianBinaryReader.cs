using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleEndianBinaryIO
{
    public class EndianBinaryReader : BinaryReader
    {
        public Endianness Endianness { get; private set; }

        public bool IsNativeEndianness { get { return EndianBitConverter.NativeEndianness == Endianness; } }

        public EndianBinaryReader(Stream stream, Endianness endianness) : base(stream)
        {
            Endianness = endianness;
        }

        public EndianBinaryReader(Stream stream, Encoding encoding , Endianness endianness) : base(stream, encoding)
        {
            Endianness = endianness;
        }

        public EndianBinaryReader(Stream stream, Encoding encoding, bool leaveOpen, Endianness endianness) : base(stream, encoding, leaveOpen)
        {
            Endianness = endianness;
        }

        public override Stream BaseStream => base.BaseStream;

        public long Position { get => BaseStream.Position; set { BaseStream.Position = value; } }
        
        public long Length { get => BaseStream.Length; }
        
        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public long Seek(int offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void Close()
        {
            base.Close();
        }

        protected override void FillBuffer(int numBytes)
        {
            base.FillBuffer(numBytes);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string ReadString()
        {
            return base.ReadString();
        }

        public override int PeekChar()
        {
            return base.PeekChar();
        }

        public override int Read()
        {
            return base.Read();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            return base.Read(buffer, index, count);
        }

        public override int Read(byte[] buffer, int index, int count)
        {
            return base.Read(buffer, index, count);
        }

        public override bool ReadBoolean()
        {
            return base.ReadBoolean();
        }

        public override byte ReadByte()
        {
            return base.ReadByte();
        }

        public override sbyte ReadSByte()
        {
            return base.ReadSByte();
        }

        public override byte[] ReadBytes(int count)
        {
            return base.ReadBytes(count);
        }

        public override char ReadChar()
        {
            return base.ReadChar();
        }

        public override char[] ReadChars(int count)
        {
            return base.ReadChars(count);
        }

        public override decimal ReadDecimal()
        {
            return base.ReadDecimal();
        }

        public override float ReadSingle()
        {
            return ReadSingle(Endianness);
        }

        public float ReadSingle(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadSingle();
            }
            else
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(EndianBitConverter.Reverse(base.ReadUInt32())), 0);
            }
        }

        public override double ReadDouble()
        {
            return ReadDouble(Endianness);
        }

        public double ReadDouble(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadDouble();
            }
            else
            {
                return BitConverter.ToDouble(BitConverter.GetBytes(EndianBitConverter.Reverse(base.ReadUInt64())), 0);
            }
        }

        public override short ReadInt16()
        {
            return ReadInt16(Endianness);
        }

        public short ReadInt16(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadInt16();
            }
            else
            {
                return EndianBitConverter.Reverse(base.ReadInt16());
            }
        }

        public override ushort ReadUInt16()
        {
            return ReadUInt16(Endianness);
        }

        public ushort ReadUInt16(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadUInt16();
            }
            else
            {
                return EndianBitConverter.Reverse(base.ReadUInt16());
            }
        }

        public override int ReadInt32()
        {
            return ReadInt32(Endianness);
        }

        public int ReadInt32(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadInt32();
            }
            else
            {
                return EndianBitConverter.Reverse(base.ReadInt32());
            }
        }

        public override uint ReadUInt32()
        {
            return ReadUInt32(Endianness);
        }

        public uint ReadUInt32(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadUInt32();
            }
            else
            {
                return EndianBitConverter.Reverse(base.ReadUInt32());
            }
        }

        public override long ReadInt64()
        {
            return ReadInt64(Endianness);
        }

        public long ReadInt64(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadInt64();
            }
            else
            {
                return EndianBitConverter.Reverse(base.ReadInt64());
            }
        }

        public override ulong ReadUInt64()
        {
            return ReadUInt64(Endianness);
        }

        public ulong ReadUInt64(Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                return base.ReadUInt64();
            }
            else
            {
                return EndianBitConverter.Reverse(base.ReadUInt64());
            }
        }
 
    }
}
