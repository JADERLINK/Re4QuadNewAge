using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SimpleEndianBinaryIO;

namespace EFF_SPLIT
{
    internal class Separate
    {
        public static TableIndex TableIndexEntry(EndianBinaryReader br, long StartOffset, out long EndOfffset) 
        {
            if (StartOffset != 0)
            {
                br.BaseStream.Position = StartOffset;
                uint Amount = br.ReadUInt32();

                TableIndex table = new TableIndex(Amount);
                for (int i = 0; i < Amount; i++)
                {
                    TableEntry entry = new TableEntry();
                    entry.Value = br.ReadBytes(0x08);
                    table.Entries[i] = entry;
                }
                EndOfffset = br.BaseStream.Position;
                return table;
            }
            else
            {
                EndOfffset = 0;
                return new TableIndex(0);
            }
        }

        public static TableIndex Table06(EndianBinaryReader br, long StartOffset, out long EndOffset, bool IsExtendedEntry)
        {
            if (StartOffset != 0)
            {
                br.BaseStream.Position = StartOffset;
                uint Amount = br.ReadUInt32();

                uint[] offsetArray = new uint[Amount];

                for (int i = 0; i < Amount; i++)
                {
                    offsetArray[i] = br.ReadUInt32();
                }

                TableIndex table = new TableIndex(Amount);
                for (int i = 0; i < Amount; i++)
                {
                    br.BaseStream.Position = StartOffset + offsetArray[i];
                    TableEntry entry = new TableEntry();
                    byte[] Arr = new byte[32];
                    if (IsExtendedEntry)
                    {
                        var temp = br.ReadBytes(32);
                        temp.CopyTo(Arr, 0);
                    }
                    else 
                    {
                        var temp = br.ReadBytes(16);
                        temp.CopyTo(Arr, 0);
                    }
                    entry.Value = Arr;
                    table.Entries[i] = entry;
                }
                EndOffset = br.BaseStream.Position;
                return table;
            }
            else
            {
                EndOffset = 0;
                return new TableIndex(0);
            }
        }

        public static Table09 Table09(EndianBinaryReader br, long StartOffset, out long EndOffset)
        {
            if (StartOffset != 0)
            {
                br.BaseStream.Position = StartOffset;
                uint Amount1 = br.ReadUInt32();

                uint[] offsetArray = new uint[Amount1];

                for (int i = 0; i < Amount1; i++)
                {
                    offsetArray[i] = br.ReadUInt32();
                }

                Table09 table9 = new Table09(Amount1);
                for (int i = 0; i < Amount1; i++)
                {
                    br.BaseStream.Position = StartOffset + offsetArray[i];
                    ushort Amount2 = br.ReadUInt16();
                    ushort _padding = br.ReadUInt16();

                    TableIndex table0 = new TableIndex(Amount2);
                    for (int j = 0; j < Amount2; j++)
                    {
                        TableEntry entry = new TableEntry();
                        entry.Value = br.ReadBytes(40);
                        table0.Entries[j] = entry;
                    }
                    table9.Entries[i] = table0;

                }
                EndOffset = br.BaseStream.Position;
                return table9;
            }
            else
            {
                EndOffset = 0;
                return new Table09(0);
            }
        }

        public static TableEffectType Effect_Type(EndianBinaryReader br, long StartOffset, out long EndOffset, Endianness endianness) 
        {
            if (StartOffset != 0)
            {
                br.BaseStream.Position = StartOffset;
                uint Amount1 = br.ReadUInt32();

                uint[] offsetArray = new uint[Amount1];

                for (int i = 0; i < Amount1; i++)
                {
                    offsetArray[i] = br.ReadUInt32();
                }
                TableEffectType table = new TableEffectType(Amount1);

                for (int i = 0; i < Amount1; i++)
                {
                    br.BaseStream.Position = offsetArray[i] + StartOffset;
                    byte[] header = br.ReadBytes(48);
                    ushort amount2 = EndianBitConverter.ToUInt16(header, 0, endianness);
                    EffectGroup group = new EffectGroup(amount2);
                    group.Header = header;

                    for (int j = 0; j < amount2; j++)
                    {
                        byte[] entry = br.ReadBytes(300);
                        EffectEntry effectEntry = new EffectEntry();
                        effectEntry.Value = entry;
                        group.Entries[j] = effectEntry;
                    }
                    table.Groups[i] = group;
                }

                EndOffset = br.BaseStream.Position;
                return table;
            }
            else
            {
                EndOffset = 0;
                return new TableEffectType(0);
            }
        }
    }

    public class TablesGroup 
    {
        public TableIndex Table00 = null;
        public TableIndex Table01 = null;
        public TableIndex Table02 = null;
        public TableIndex Table03 = null;
        public TableIndex Table04 = null;
        public TableIndex Table06 = null;
        public Table09 Table09 = null;
        public TableEffectType Table07_Effect_0_Type = null;
        public TableEffectType Table08_Effect_1_Type = null;
    }

    public class TableIndex
    {
        public TableEntry[] Entries;

        public TableIndex(uint Amount)
        {
            Entries = new TableEntry[Amount];
        }
    }

    public struct TableEntry 
    {
        public byte[] Value;
    }

    public class Table09 
    {
        public TableIndex[] Entries;

        public Table09(uint Amount)
        {
            Entries = new TableIndex[Amount];
        }
    }

    public class TableEffectType
    {
        public EffectGroup[] Groups;

        public TableEffectType(uint Amount)
        {
            Groups = new EffectGroup[Amount];
        }
    }

    public class EffectGroup 
    {
        public byte[] Header; //48 bytes contando com a parte da quantidade
        public EffectEntry[] Entries;

        public EffectGroup(uint Amount) 
        {
            Entries = new EffectEntry[Amount];
        }
    }

    public struct EffectEntry 
    {
        public byte[] Value; // 300 bytes
    }
}
