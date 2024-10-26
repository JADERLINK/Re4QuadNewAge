using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Scarlet.IO
{
    internal enum VerifyResult { NoMagicNumber, VerifyOkay, WrongMagicNumber }

    internal class IdentificationMatch
    {
        public Type Type { get; private set; }
        public uint Weight { get; private set; }

        public IdentificationMatch(Type type, uint weight)
        {
            Type = type;
            Weight = weight;
        }
    }

    public abstract class FileFormat
    {
        bool isLoaded;

        public bool IsLoaded { get { return isLoaded; } }

        protected FileFormat()
        {
            isLoaded = false;
        }

        // content deleted

        public void Open(string filename)
        {
            Open(filename, EndianBinaryReader.NativeEndianness);
        }

        public void Open(string filename, Endian endianness)
        {
            using (EndianBinaryReader reader = new EndianBinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), endianness))
            {
                Open(reader);
            }
        }

        public void Open(Stream stream)
        {
            Open(stream, EndianBinaryReader.NativeEndianness);
        }

        public void Open(Stream stream, Endian endianness)
        {
            using (EndianBinaryReader reader = new EndianBinaryReader(stream, endianness))
            {
                Open(reader);
            }
        }

        public void Open(EndianBinaryReader reader)
        {
            VerifyResult verifyResult = VerifyMagicNumber(reader, this.GetType());
            if (verifyResult != VerifyResult.WrongMagicNumber)
            {
                OnOpen(reader);
                isLoaded = true;
            }
            else
            { 
                throw new Exception("Invalid magic number");
            }
        }

        protected abstract void OnOpen(EndianBinaryReader reader);

        public virtual string GetFormatDescription()
        {
            return null;
        }

        internal static VerifyResult VerifyMagicNumber(EndianBinaryReader reader, Type type)
        {
            VerifyResult result = VerifyResult.NoMagicNumber;
            long lastPosition = reader.BaseStream.Position;

            foreach (MagicNumberAttribute magicNumberAttrib in type.GetCustomAttributes(typeof(MagicNumberAttribute), false))
            {
                reader.BaseStream.Seek(lastPosition + magicNumberAttrib.Position, SeekOrigin.Begin);
                if (reader.ReadBytes(magicNumberAttrib.MagicNumber.Length).SequenceEqual(magicNumberAttrib.MagicNumber))
                {
                    result = VerifyResult.VerifyOkay;
                    break;
                }
                else
                {
                    result = VerifyResult.WrongMagicNumber;
                }
            }

            reader.BaseStream.Position = lastPosition;
            return result;
        }

        // TODO: better place to move to? some IO helper class?
        internal static void CopyStream(Stream input, Stream output, int bytes)
        {
            byte[] buffer = new byte[32768];
            int read;
            while (bytes > 0 && (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        internal static string ReadNullTermString(Stream input)
        {
            StringBuilder builder = new StringBuilder();
            char read;
            while ((read = (char)input.ReadByte()) != 0) builder.Append(read);
            return builder.ToString();
        }
    }
}
