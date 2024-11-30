using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using SimpleEndianBinaryIO;

namespace EFF_SPLIT
{
    internal class Join
    {
        public delegate void CustomTable(EndianBinaryWriter bw, bool IsPS2);

        public CustomTable WriteTable05;
        public CustomTable WriteTable10;

        private TablesGroup tables = null;

        public Join(TablesGroup tables) 
        { 
            this.tables = tables;
            WriteTable05 = CustomTableEmpty;
            WriteTable10 = CustomTableEmpty;
        }

        public void Create_EFF_File(Stream stream, Endianness endianness) 
        {
            var bw = new EndianBinaryWriter(stream, endianness);
            bw.Write((uint)0x0B);
            bw.Write(new byte[0x2C]);
            bw.Write(new byte[0x10]);
    
            uint offsetTable00 = (uint)bw.BaseStream.Position;
            WriteTableIndex(bw, tables.Table00, false);

            uint offsetTable01 = (uint)bw.BaseStream.Position;
            WriteTableIndex(bw, tables.Table01, false);

            uint offsetTable02 = (uint)bw.BaseStream.Position;
            WriteTableIndex(bw, tables.Table02, false);

            uint offsetTable03 = (uint)bw.BaseStream.Position;
            WriteTableIndex(bw, tables.Table03, false);

            uint offsetTable04 = (uint)bw.BaseStream.Position;
            WriteTableIndex(bw, tables.Table04, false);

            uint offsetTable05 = (uint)bw.BaseStream.Position;
            WriteTable05(bw, false);

            uint offsetTable06 = (uint)bw.BaseStream.Position;
            WriteTable06(bw, tables.Table06, endianness, false);

            uint offsetTable07 = (uint)bw.BaseStream.Position;
            Write_Effect_Type(bw, tables.Table07_Effect_0_Type, endianness, false);

            uint offsetTable08 = (uint)bw.BaseStream.Position;
            Write_Effect_Type(bw, tables.Table08_Effect_1_Type, endianness, false);

            uint offsetTable09 = (uint)bw.BaseStream.Position;
            WriteTable09(bw, tables.Table09, endianness, false);

            uint offsetTable10 = (uint)bw.BaseStream.Position;
            WriteTable10(bw, false);

            bw.BaseStream.Position = 4;
            bw.Write(offsetTable00);
            bw.Write(offsetTable01);
            bw.Write(offsetTable02);
            bw.Write(offsetTable03);
            bw.Write(offsetTable04);
            bw.Write(offsetTable05);
            bw.Write(offsetTable06);
            bw.Write(offsetTable07);
            bw.Write(offsetTable08);
            bw.Write(offsetTable09);
            bw.Write(offsetTable10);
        }

        private void WriteTableIndex(EndianBinaryWriter bw, TableIndex Table, bool IsPS2) 
        {
            if (Table != null && Table.Entries.Length != 0)
            {
                bw.Write((uint)Table.Entries.Length);
                for (int i = 0; i < Table.Entries.Length; i++)
                {
                    bw.Write(Table.Entries[i].Value);
                }
                AddPadding(bw, IsPS2 == false);
            }
            else if (IsPS2)
            {
                bw.Write(new byte[0x10]);
            }
            else 
            {
                bw.Write(new byte[0x20]);
            }
        }

        private void WriteTable06(EndianBinaryWriter bw, TableIndex Table06, Endianness endianness, bool IsPS2) 
        {
            if (Table06 != null && Table06.Entries.Length != 0)
            {
                uint entryByteLength = IsPS2 ? 16u : 32u;

                uint Length = (uint)Table06.Entries.Length;
                uint calc = 4 + (Length * 4);
                uint _line = calc / 16;
                uint rest = calc % 16;
                _line += rest != 0 ? 1u : 0u;
                calc = _line * 16;
                calc += Length * entryByteLength;
                byte[] res = new byte[calc];
                EndianBinaryWriter ms = new EndianBinaryWriter(new MemoryStream(res), endianness);

                ms.Write(Length);
                uint offsetToOffset = 4;
                uint offset = _line * 16;

                for (int i = 0; i < Table06.Entries.Length; i++)
                {
                    ms.BaseStream.Position = offsetToOffset;
                    ms.Write(offset);
                    ms.BaseStream.Position = offset;
                    if (IsPS2)
                    {
                        ms.Write(Table06.Entries[i].Value.Take(16).ToArray());
                    }
                    else
                    {
                        ms.Write(Table06.Entries[i].Value);
                    }
                    offsetToOffset += 4;
                    offset = (uint)ms.BaseStream.Position;
                }

                ms.Close();
                bw.Write(res);
            }
            else if (IsPS2)
            {
                bw.Write(new byte[0x10]);
            }
            else
            {
                bw.Write(new byte[0x20]);
            }
        }

        private void WriteTable09(EndianBinaryWriter bw, Table09 table09, Endianness endianness, bool IsPS2) 
        { 
            if (table09 != null && table09.Entries.Length != 0)
            {
                uint Length = (uint)table09.Entries.Length;
                uint calc = 4 + (Length * 4);
                uint _line = calc / 16;
                uint rest = calc % 16;
                _line += rest != 0 ? 1u : 0u;
                calc = _line * 16;

                for (int i = 0; i < Length; i++)
                {
                    ushort Length2 = (ushort)table09.Entries[i].Entries.Length;
                    calc += 4u + (Length2 * 40u);
                    uint _line2 = calc / 16;
                    uint rest2 = calc % 16;
                    _line2 += rest2 != 0 ? 1u : 0u;
                    calc = _line2 * 16;
                }
               
                byte[] res = new byte[calc];
                EndianBinaryWriter ms = new EndianBinaryWriter(new MemoryStream(res), endianness);

                ms.Write(Length);
                uint offsetToOffset = 4;
                uint offset = _line * 16;

                for (int i = 0; i < table09.Entries.Length; i++)
                {
                    ushort Length2 = (ushort)table09.Entries[i].Entries.Length;

                    ms.BaseStream.Position = offsetToOffset;
                    ms.Write(offset);
                    ms.BaseStream.Position = offset;

                    ms.Write(Length2);
                    ms.Write((ushort)0);

                    for (int j = 0; j < Length2; j++)
                    {
                        ms.Write(table09.Entries[i].Entries[j].Value);
                    }

                    AddPadding(ms, false);

                    offsetToOffset += 4;
                    offset = (uint)ms.BaseStream.Position;
                }

                ms.Close();
                bw.Write(res);

            }
            else if (IsPS2)
            {
                bw.Write(new byte[0x10]);
            }
            else
            {
                bw.Write(new byte[0x20]);
            }
        }

        private void Write_Effect_Type(EndianBinaryWriter bw, TableEffectType table, Endianness endianness, bool IsPS2) 
        {
            if (table != null && table.Groups.Length != 0) 
            {
                uint Length = (uint)table.Groups.Length;
                uint calc = 4 + (Length * 4);
                uint _line = calc / 16;
                uint rest = calc % 16;
                _line += rest != 0 ? 1u : 0u;
                calc = _line * 16;

                for (int i = 0; i < Length; i++)
                {
                    ushort Length2 = (ushort)table.Groups[i].Entries.Length;
                    calc += 48u + (Length2 * 300u);
                    uint _line2 = calc / 16;
                    uint rest2 = calc % 16;
                    _line2 += rest2 != 0 ? 1u : 0u;
                    calc = _line2 * 16;
                }

                byte[] res = new byte[calc];
                EndianBinaryWriter ms = new EndianBinaryWriter(new MemoryStream(res), endianness);

                ms.Write(Length);
                uint offsetToOffset = 4;
                uint offset = _line * 16;

                for (int i = 0; i < table.Groups.Length; i++)
                {
                    ms.BaseStream.Position = offsetToOffset;
                    ms.Write(offset);
                    ms.BaseStream.Position = offset;

                    ms.Write(table.Groups[i].Header);

                    for (int j = 0; j < table.Groups[i].Entries.Length; j++)
                    {
                        ms.Write(table.Groups[i].Entries[j].Value);
                    }

                    AddPadding(ms, false);

                    offsetToOffset += 4;
                    offset = (uint)ms.BaseStream.Position;
                }

                ms.Close();
                bw.Write(res);

            }
            else if (IsPS2)
            {
                bw.Write(new byte[0x10]);
            }
            else
            {
                bw.Write(new byte[0x20]);
            }
        }


        private void AddPadding(EndianBinaryWriter bw, bool IsExtendedPadding) 
        {
            long _base = IsExtendedPadding ? 32 : 16;

            long current = bw.BaseStream.Position;
            long lines = current / _base;
            long rest = current % _base;
            lines += rest != 0 ? 1 : 0;
            long total = lines * _base;
            long dif = total - current;
            bw.Write(new byte[dif]);
        }

        private void CustomTableEmpty(EndianBinaryWriter bw, bool IsPS2) 
        {
            if (IsPS2)
            {
                bw.Write(new byte[0x10]);
            }
            else
            {
                bw.Write(new byte[0x20]);
            }
        }
    }
}
