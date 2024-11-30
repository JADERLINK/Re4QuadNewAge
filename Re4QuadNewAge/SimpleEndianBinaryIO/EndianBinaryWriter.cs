using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleEndianBinaryIO
{
    public class EndianBinaryWriter : BinaryWriter
    {
        public Endianness Endianness { get; private set; }

        public bool IsNativeEndianness { get { return EndianBitConverter.NativeEndianness == Endianness; } }

        public EndianBinaryWriter(Stream output, Endianness endianness) : base(output)
        {
            Endianness = endianness;
        }

        public EndianBinaryWriter(Stream output, Encoding encoding, Endianness endianness) : base(output, encoding)
        {
            Endianness = endianness;
        }

        public EndianBinaryWriter(Stream output, Encoding encoding, bool leaveOpen, Endianness endianness) : base(output, encoding, leaveOpen)
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

        public override long Seek(int offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Flush()
        {
            base.Flush();
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

        public override void Write(bool value)
        {
            base.Write(value);
        }

        public override void Write(byte value)
        {
            base.Write(value);
        }

        public override void Write(sbyte value)
        {
            base.Write(value);
        }

        public override void Write(byte[] buffer)
        {
            base.Write(buffer);
        }

        public override void Write(byte[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
        }

        public override void Write(char ch)
        {
            base.Write(ch);
        }

        public override void Write(char[] chars)
        {
            base.Write(chars);
        }

        public override void Write(char[] chars, int index, int count)
        {
            base.Write(chars, index, count);
        }

        public override void Write(decimal value)
        {
            base.Write(value);
        }

        public override void Write(string value)
        {
            base.Write(value);
        }
        public override void Write(float value)
        {
            Write(value, Endianness);
        }

        public void Write(float value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(BitConverter.ToUInt32(BitConverter.GetBytes(value), 0)));
            }
        }

        public override void Write(double value)
        {
            Write(value, Endianness);
        }

        public void Write(double value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(BitConverter.ToUInt64(BitConverter.GetBytes(value), 0)));
            }
        }

        public override void Write(short value)
        {
            Write(value, Endianness);
        }

        public void Write(short value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(value));
            }
        }

        public override void Write(ushort value)
        {
            Write(value, Endianness);
        }

        public void Write(ushort value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(value));
            }
        }

        public override void Write(int value)
        {
            Write(value, Endianness);
        }
        public void Write(int value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(value));
            }
        }

        public override void Write(uint value)
        {
            Write(value, Endianness);
        }

        public void Write(uint value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(value));
            }
        }

        public override void Write(long value)
        {
            Write(value, Endianness);
        }

        public void Write(long value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(value));
            }
        }

        public override void Write(ulong value)
        {
            Write(value, Endianness);
        }

        public void Write(ulong value, Endianness endianness)
        {
            if (endianness == EndianBitConverter.NativeEndianness)
            {
                base.Write(value);
            }
            else
            {
                base.Write(EndianBitConverter.Reverse(value));
            }
        }
    }
}
