using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEndianBinaryIO;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System.Drawing;
using OpenTK;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    public class File_EFFBLOB_Group
    {
        /// <summary>
        /// Endian do arquivo
        /// </summary>
        public Endianness Endian { get; private set; }

        /// <summary>
        /// Table_0_TPL_Texture_IDs, (byte[8] Line, ushort OrderID)
        /// </summary>
        public EffTable0 _Table0 { get; private set; }

        /// <summary>
        /// Table_1_Ref_Effect_0, (byte[8] Line, ushort OrderID)
        /// </summary>
        public EffTable1 _Table1 { get; private set; }

        /// <summary>
        /// Table_2_EAR_Links, (byte[8] Line, ushort OrderID)
        /// </summary>
        public EffTable2 _Table2 { get; private set; }

        /// <summary>
        /// Table_3_Effect_Path_IDs, (byte[8] Line, ushort OrderID)
        /// </summary>
        public EffTable3 _Table3 { get; private set; }

        /// <summary>
        /// Table_4_BIN_Model_IDs, (byte[8] Line, ushort OrderID)
        /// </summary>
        public EffTable4 _Table4 { get; private set; }

        /// <summary>
        /// Table_6_TextureData, (byte[32] Line, ushort OrderID)
        /// </summary>
        public EffTable6 _Table6 { get; private set; }

        /// <summary>
        /// Table_7_Effect_0_Group, byte[48 -2] //não tem os bytes do campo quantidade;
        /// </summary>
        public EffTableEffectGroup _Table7_Effect0_Group { get; private set; }

        /// <summary>
        /// Table_8_Effect_1_Group, byte[48 -2] //não tem os bytes do campo quantidade;
        /// </summary>
        public EffTableEffectGroup _Table8_Effect1_Group { get; private set; }

        /// <summary>
        /// entries dos groups das tables 7 e 8
        /// <para>Ordem das entrys</para>
        /// <para>internalID, (byte[300] Line, ushort EntryOrder{Indice}, ushort GroupID{Indice}, byte tableID{7 ou 8})</para>
        /// </summary>
        public EffTableEffectEntry _TableEffectEntry { get; private set; }

        /// <summary>
        /// Table_9_Paths_Entry, byte[40], listagem com as entry
        /// <para> Table_9_Paths_Order_Group (groups, e ordem das entry) </para>
        /// <para> ushort internal_Id_da_Table_9_Paths_Entry, (ushort Order_index, ushort GroupID_ordem_dos_grupos) </para>
        /// </summary>
        public EffTable9 _Table9 { get; private set; }

        public Dictionary<EffectEntryTableID, EffTableEffectGroup> _TableEffectGroups { get; private set; }

        public File_EFFBLOB_Group(Endianness Endian)
        {
            this.Endian = Endian;
            _Table0 = new EffTable0(Endian, this);
            _Table1 = new EffTable1(Endian, this);
            _Table2 = new EffTable2(Endian, this);
            _Table3 = new EffTable3(Endian, this);
            _Table4 = new EffTable4(Endian, this);
            _Table6 = new EffTable6(Endian, this);
            _Table9 = new EffTable9(Endian, this);
            _Table7_Effect0_Group = new EffTableEffectGroup(Endian, this, EffectEntryTableID.Table7);
            _Table8_Effect1_Group = new EffTableEffectGroup(Endian, this, EffectEntryTableID.Table8);
            _TableEffectEntry = new EffTableEffectEntry(Endian, this);

            _TableEffectGroups = new Dictionary<EffectEntryTableID, EffTableEffectGroup>
            {
                { EffectEntryTableID.Table7, _Table7_Effect0_Group },
                { EffectEntryTableID.Table8, _Table8_Effect1_Group }
            };
        }

        public abstract class EffTableBase : BaseGroup 
        {
            public Dictionary<ushort, (byte[] Line, ushort OrderID)> TableLines { get; protected set; }
            public ushort IdForNewLine = 0;

            /// <summary>
            /// A classe pai dessa;
            /// </summary>
            protected File_EFFBLOB_Group Parent { get; private set; }

            /// <summary>
            /// Endian do arquivo;
            /// </summary>
            public Endianness Endian { get; private set; }

            public EffTableBase(Endianness endian, File_EFFBLOB_Group parent)
            {
                TableLines = new Dictionary<ushort, (byte[] Line, ushort OrderID)>();

                Parent = parent;
                Endian = endian;

                DisplayMethods = new NodeDisplayMethods();
                DisplayMethods.GetNodeText = GetNodeText;
                DisplayMethods.GetNodeColor = GetNodeColor;

                ChangeAmountMethods = new NodeChangeAmountMethods();
                ChangeAmountMethods.AddNewLineID = AddNewLineID;
                ChangeAmountMethods.RemoveLineID = RemoveLineID;

                Methods = new NewAge_EFF_Methods();
                SetBaseMethods(Methods);
                Methods.ReturnLine = ReturnLine;
                Methods.SetLine = SetLine;
                Methods.SetEntryOrderID = SetEntryOrderID;
                Methods.GetEntryOrderID = GetEntryOrderID;
            }

            /// <summary>
            /// classe com os metodos responsaveis pelo oque sera exibido no node;
            /// </summary>
            public NodeDisplayMethods DisplayMethods { get; }


            /// <summary>
            /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
            /// </summary>
            public NodeChangeAmountMethods ChangeAmountMethods { get; }

            /// <summary>
            /// Classe com os metodos que serão passados para classe Property;
            /// </summary>
            public NewAge_EFF_Methods Methods { get; }

            protected override Endianness GetEndianness()
            {
                return Endian;
            }

            protected override byte[] GetInternalLine(ushort ID)
            {
                return TableLines[ID].Line;
            }

            protected virtual bool LinesContainsKey(ushort ID)
            {
                return TableLines.ContainsKey(ID);
            }

            protected virtual void SetEntryOrderID(ushort InternalID, ushort EntryOrderID)
            {
                var o = TableLines[InternalID];
                o.OrderID = EntryOrderID;
                TableLines[InternalID] = o;
            }

            protected virtual ushort GetEntryOrderID(ushort InternalID)
            {
                return TableLines[InternalID].OrderID;
            }

            protected virtual byte[] ReturnLine(ushort ID)
            {
                return (byte[])GetInternalLine(ID).Clone();
            }

            protected virtual void SetLine(ushort ID, byte[] value)
            {
                value.CopyTo(GetInternalLine(ID), 0);
            }

            protected ushort GetNewValidEntryOrderID()
            {
                var elements = TableLines.Values.Select(x => x.OrderID).ToArray();
                return elements.Length == 0 ? (ushort)0 : (ushort)(elements.Max() + 1);
            }

            protected abstract void RemoveLineID(ushort ID);
            protected abstract ushort AddNewLineID(byte initType);
            protected abstract Color GetNodeColor(ushort ID);
            protected abstract string GetNodeText(ushort ID);
        }

        public class EffTable0 : EffTableBase
        {
            public EffTable0(Endianness endian, File_EFFBLOB_Group parent) : base(endian, parent) { }

            protected override string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        var val1 = EndianBitConverter.ToInt16(TableLines[ID].Line, 0, Endian);
                        string r = "EFF Table0 - InID: " + ID + " OrderID: " + GetEntryOrderID(ID) + "  [0x" + val1.ToString("X2") + "]";
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            protected override Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            protected override void RemoveLineID(ushort ID)
            {
                TableLines.Remove(ID);
            }

            protected override ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = TableLines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[8];

                TableLines.Add(newID, (content, newEntryOrderID));
                return newID;
            }

        }

        public class EffTable1 : EffTableBase
        {
            public EffTable1(Endianness endian, File_EFFBLOB_Group parent) : base(endian, parent) { }

            protected override string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        var val1 = EndianBitConverter.ToInt16(TableLines[ID].Line, 0, Endian);
                        string r = "EFF Table1 - InID: " + ID + " OrderID: " + GetEntryOrderID(ID) + "  [0x" + val1.ToString("X2") + "]";
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            protected override Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            protected override void RemoveLineID(ushort ID)
            {
                TableLines.Remove(ID);
            }

            protected override ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = TableLines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[8];

                TableLines.Add(newID, (content, newEntryOrderID));
                return newID;
            }

        }

        public class EffTable2 : EffTableBase
        {
            public EffTable2(Endianness endian, File_EFFBLOB_Group parent) : base(endian, parent) { }

            protected override string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        var val1 = EndianBitConverter.ToInt16(TableLines[ID].Line, 0, Endian);
                        var val2 = EndianBitConverter.ToInt16(TableLines[ID].Line, 2, Endian);
                        string r = "EFF Table2 - InID: " + ID + " OrderID: " + GetEntryOrderID(ID) +
                            "  [0x" + val1.ToString("X2") + "][0x" + val2.ToString("X2") + "]";
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            protected override Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            protected override void RemoveLineID(ushort ID)
            {
                TableLines.Remove(ID);
            }

            protected override ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = TableLines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[8];

                TableLines.Add(newID, (content, newEntryOrderID));
                return newID;
            }

        }

        public class EffTable3 : EffTableBase
        {
            public EffTable3(Endianness endian, File_EFFBLOB_Group parent) : base(endian, parent) { }

            protected override string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        var val1 = EndianBitConverter.ToInt16(TableLines[ID].Line, 0, Endian);
                        string r = "EFF Table3 - InID: " + ID + " OrderID: " + GetEntryOrderID(ID) + "  [0x" + val1.ToString("X2") + "]";
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            protected override Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            protected override void RemoveLineID(ushort ID)
            {
                TableLines.Remove(ID);
            }

            protected override ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = TableLines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[8];

                TableLines.Add(newID, (content, newEntryOrderID));
                return newID;
            }

        }

        public class EffTable4 : EffTableBase
        {
            public EffTable4(Endianness endian, File_EFFBLOB_Group parent) : base(endian, parent) { }

            protected override string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        var val1 = EndianBitConverter.ToInt16(TableLines[ID].Line, 0, Endian);
                        string r = "EFF Table4 - InID: " + ID + " OrderID: " + GetEntryOrderID(ID) + "  [0x" + val1.ToString("X2") + "]";
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            protected override Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            protected override void RemoveLineID(ushort ID)
            {
                TableLines.Remove(ID);
            }

            protected override ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = TableLines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[8];

                TableLines.Add(newID, (content, newEntryOrderID));
                return newID;
            }

        }

        public class EffTable6 : EffTableBase
        {
            public EffTable6(Endianness endian, File_EFFBLOB_Group parent) : base(endian, parent) { }

            protected override string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        string r = "EFF Table6 - InID: " + ID + " OrderID: " + GetEntryOrderID(ID);
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            protected override Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            protected override void RemoveLineID(ushort ID)
            {
                TableLines.Remove(ID);
            }

            protected override ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = TableLines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[32];

                TableLines.Add(newID, (content, newEntryOrderID));
                return newID;
            }

        }

        public class EffTable9 : BaseGroup
        {
            public Dictionary<ushort, (byte[] Line, ushort EntryOrderID, ushort GroupOrderID)> Table9Lines { get; protected set; }
            public ushort IdForNewLine = 0;

            /// <summary>
            /// A classe pai dessa;
            /// </summary>
            protected File_EFFBLOB_Group Parent { get; private set; }

            /// <summary>
            /// Endian do arquivo;
            /// </summary>
            public Endianness Endian { get; private set; }

            public EffTable9(Endianness endian, File_EFFBLOB_Group parent)
            {
                Table9Lines = new Dictionary<ushort, (byte[] Line, ushort EntryOrderID, ushort GroupOrderID)>();

                Parent = parent;
                Endian = endian;

                DisplayMethods = new NodeDisplayMethods();
                DisplayMethods.GetNodeText = GetNodeText;
                DisplayMethods.GetNodeColor = GetNodeColor;

                ChangeAmountMethods = new NodeChangeAmountMethods();
                ChangeAmountMethods.AddNewLineID = AddNewLineID;
                ChangeAmountMethods.RemoveLineID = RemoveLineID;

                Methods = new NewAge_EFF_Table9Entry_Methods();
                SetBaseMethods(Methods);
                Methods.ReturnLine = ReturnLine;
                Methods.SetLine = SetLine;
                Methods.SetEntryOrderID = SetEntryOrderID;
                Methods.GetEntryOrderID = GetEntryOrderID;
                Methods.SetGroupOrderID = SetGroupOrderID;
                Methods.GetGroupOrderID = GetGroupOrderID;

                Methods.ReturnPositionX_Hex = ReturnPositionX_Hex;
                Methods.ReturnPositionY_Hex = ReturnPositionY_Hex;
                Methods.ReturnPositionZ_Hex = ReturnPositionZ_Hex;
                Methods.SetPositionX_Hex = SetPositionX_Hex;
                Methods.SetPositionY_Hex = SetPositionY_Hex;
                Methods.SetPositionZ_Hex = SetPositionZ_Hex;
                Methods.ReturnPositionX = ReturnPositionX;
                Methods.ReturnPositionY = ReturnPositionY;
                Methods.ReturnPositionZ = ReturnPositionZ;
                Methods.SetPositionX = SetPositionX;
                Methods.SetPositionY = SetPositionY;
                Methods.SetPositionZ = SetPositionZ;

                MethodsForGL = new NewAge_EFF_Table9Entry_Methods_MethodsForGL();
                MethodsForGL.GetPosition = GetPosition;
                MethodsForGL.GetGroupOrderID = GetGroupOrderID;

                MoveMethods = new NodeMoveMethods();
                MoveMethods.GetObjPostion_ToCamera = GetObjPostion_ToCamera;
                MoveMethods.GetObjAngleY_ToCamera = Utils.GetObjAngleY_ToCamera_Null;
                MoveMethods.GetObjPostion_ToMove_General = GetObjPostion_ToMove_General;
                MoveMethods.SetObjPostion_ToMove_General = SetObjPostion_ToMove_General;
                MoveMethods.GetObjRotationAngles_ToMove = Utils.GetObjRotationAngles_ToMove_Null;
                MoveMethods.SetObjRotationAngles_ToMove = Utils.SetObjRotationAngles_ToMove_Null;
                MoveMethods.GetObjScale_ToMove = Utils.GetObjScale_ToMove_Null;
                MoveMethods.SetObjScale_ToMove = Utils.SetObjScale_ToMove_Null;
                MoveMethods.GetTriggerZoneCategory = Utils.GetTriggerZoneCategory_Null;
            }

            /// <summary>
            /// classe com os metodos responsaveis pelo oque sera exibido no node;
            /// </summary>
            public NodeDisplayMethods DisplayMethods { get; }

            /// <summary>
            /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
            /// </summary>
            public NodeChangeAmountMethods ChangeAmountMethods { get; }

            /// <summary>
            /// Classe com os metodos que serão passados para classe Property;
            /// </summary>
            public NewAge_EFF_Table9Entry_Methods Methods { get; }

            /// <summary>
            ///  classe com os metodos responsaveis pela movimentação dos objetos e da camera
            /// </summary>
            public NodeMoveMethods MoveMethods { get; }

            /// <summary>
            /// classe com os metodos usado para arrumar os index (ordem) das entrys
            /// </summary>
            public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods { get; set; }

            /// <summary>
            /// MethodsForGL
            /// </summary>
            public NewAge_EFF_Table9Entry_Methods_MethodsForGL MethodsForGL { get; }

            #region parte principal

            protected override Endianness GetEndianness()
            {
                return Endian;
            }

            protected override byte[] GetInternalLine(ushort ID)
            {
                return Table9Lines[ID].Line;
            }

            protected bool LinesContainsKey(ushort ID)
            {
                return Table9Lines.ContainsKey(ID);
            }

            protected void SetEntryOrderID(ushort InternalID, ushort EntryOrderID)
            {
                var o = Table9Lines[InternalID];
                o.EntryOrderID = EntryOrderID;
                Table9Lines[InternalID] = o;
            }

            protected ushort GetEntryOrderID(ushort InternalID)
            {
                return Table9Lines[InternalID].EntryOrderID;
            }

            protected ushort GetGroupOrderID(ushort InternalID)
            {
                return Table9Lines[InternalID].GroupOrderID;
            }

            protected void SetGroupOrderID(ushort InternalID, ushort GroupOrderID)
            {
                if (GroupOrderID < Consts.AmountLimitEFF_Table9_Group)
                {
                    var o = Table9Lines[InternalID];
                    o.GroupOrderID = GroupOrderID;
                    Table9Lines[InternalID] = o;
                    ChangeAmountCallbackMethods.OnMoveNode();
                }
            }

            protected byte[] ReturnLine(ushort ID)
            {
                return (byte[])GetInternalLine(ID).Clone();
            }

            protected void SetLine(ushort ID, byte[] value)
            {
                value.CopyTo(GetInternalLine(ID), 0);
            }

            private ushort GetNewValidEntryOrderID(ushort GroupOrderID)
            {
                var elements = Table9Lines.Values.Where(x => x.GroupOrderID == GroupOrderID).Select(x => x.EntryOrderID).ToArray();
                return elements.Length == 0 ? (ushort)0 : (ushort)(elements.Max() + 1);
            }

            private string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        string r = "EFF Table9 - InID: " + ID + "  OrderID: " + GetEntryOrderID(ID) + "  GroupID: " + GetGroupOrderID(ID);
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            private Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                else if (!Globals.EFF_RenderTable9)
                {
                    return Globals.NodeColorHided;
                }
                else if (!(Globals.EFF_ShowOnlySelectedGroup == false || (Globals.EFF_ShowOnlySelectedGroup && Globals.EFF_SelectedGroup == GetGroupOrderID(ID))))
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            private void RemoveLineID(ushort ID)
            {
                Table9Lines.Remove(ID);
            }

            private ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = Table9Lines.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID(0);

                byte[] content = new byte[40];

                Table9Lines.Add(newID, (content, newEntryOrderID, 0));
                return newID;
            }

            #endregion

            #region propriedades

            private uint ReturnPositionX_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table9Lines[ID].Line, 0x00, Endian);
            }

            private void SetPositionX_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table9Lines[ID].Line, 0x00);
            }

            private uint ReturnPositionY_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table9Lines[ID].Line, 0x04, Endian);
            }

            private void SetPositionY_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table9Lines[ID].Line, 0x04);
            }

            private uint ReturnPositionZ_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table9Lines[ID].Line, 0x08, Endian);
            }

            private void SetPositionZ_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table9Lines[ID].Line, 0x08);
            }

            private float ReturnPositionX(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionX_Hex(ID)), 0);
            }

            private float ReturnPositionY(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionY_Hex(ID)), 0);
            }

            private float ReturnPositionZ(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionZ_Hex(ID)), 0);
            }

            private void SetPositionX(ushort ID, float value)
            {
                SetPositionX_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetPositionY(ushort ID, float value)
            {
                SetPositionY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetPositionZ(ushort ID, float value)
            {
                SetPositionZ_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            #endregion

            #region parte MethodsForGL

            private Vector3 GetPosition(ushort ID)
            {
                return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
            }

            #endregion

            #region metodos move

            private Vector3 GetObjPostion_ToCamera(ushort ID)
            {
                Vector3 position = GetPosition(ID);
                Utils.ToCameraCheckValue(ref position);
                return position;
            }

            private Vector3[] GetObjPostion_ToMove_General(ushort ID)
            {
                Vector3[] pos = new Vector3[1];
                pos[0] = new Vector3(ReturnPositionX(ID), ReturnPositionY(ID), ReturnPositionZ(ID));
                Utils.ToMoveCheckLimits(ref pos);
                return pos;
            }


            private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value)
            {
                if (value != null && value.Length >= 1)
                {
                    SetPositionX(ID, value[0].X);
                    SetPositionY(ID, value[0].Y);
                    SetPositionZ(ID, value[0].Z);
                }
            }
            #endregion
        }

        public class EffTableEffectGroup : BaseGroup
        {
            public Dictionary<ushort, (byte[] Line, ushort OrderID)> Table_Effect_Group { get; private set; }
            public ushort IdForNewLine = 0;

            /// <summary>
            /// verifica qual é a tabela;
            /// </summary>
            private EffectEntryTableID EffectEntryTableID { get; }

            /// <summary>
            /// a classe pai dessa;
            /// </summary>
            private File_EFFBLOB_Group Parent { get; }

            /// <summary>
            /// Endian do arquivo;
            /// </summary>
            public Endianness Endian { get; private set; }

            public EffTableEffectGroup(Endianness endian, File_EFFBLOB_Group parent, EffectEntryTableID effectEntryTableID)
            {
                Table_Effect_Group = new Dictionary<ushort, (byte[] Line, ushort OrderID)>();

                Parent = parent;
                Endian = endian;
                EffectEntryTableID = effectEntryTableID;

                DisplayMethods = new NodeDisplayMethods();
                DisplayMethods.GetNodeText = GetNodeText;
                DisplayMethods.GetNodeColor = GetNodeColor;

                ChangeAmountMethods = new NodeChangeAmountMethods();
                ChangeAmountMethods.AddNewLineID = AddNewLineID;
                ChangeAmountMethods.RemoveLineID = RemoveLineID;

                Methods = new NewAge_EFF_EffectGroup_Methods();
                SetBaseMethods(Methods);
                Methods.ReturnLine = ReturnLine;
                Methods.SetLine = SetLine;
                Methods.SetEntryOrderID = SetEntryOrderID;
                Methods.GetEntryOrderID = GetEntryOrderID;
                Methods.GetGrouptype = GetGrouptype;
                Methods.GetEntryCountInGroup = GetEntryCountInGroup;

                Methods.ReturnAngleX_Hex = ReturnAngleX_Hex;
                Methods.ReturnAngleY_Hex = ReturnAngleY_Hex;
                Methods.ReturnAngleZ_Hex = ReturnAngleZ_Hex;
                Methods.ReturnPositionX_Hex = ReturnPositionX_Hex;
                Methods.ReturnPositionY_Hex = ReturnPositionY_Hex;
                Methods.ReturnPositionZ_Hex = ReturnPositionZ_Hex;
                Methods.SetAngleX_Hex = SetAngleX_Hex;
                Methods.SetAngleY_Hex = SetAngleY_Hex;
                Methods.SetAngleZ_Hex = SetAngleZ_Hex;
                Methods.SetPositionX_Hex = SetPositionX_Hex;
                Methods.SetPositionY_Hex = SetPositionY_Hex;
                Methods.SetPositionZ_Hex = SetPositionZ_Hex;
                Methods.ReturnAngleX = ReturnAngleX;
                Methods.ReturnAngleY = ReturnAngleY;
                Methods.ReturnAngleZ = ReturnAngleZ;
                Methods.ReturnPositionX = ReturnPositionX;
                Methods.ReturnPositionY = ReturnPositionY;
                Methods.ReturnPositionZ = ReturnPositionZ;
                Methods.SetAngleX = SetAngleX;
                Methods.SetAngleY = SetAngleY;
                Methods.SetAngleZ = SetAngleZ;
                Methods.SetPositionX = SetPositionX;
                Methods.SetPositionY = SetPositionY;
                Methods.SetPositionZ = SetPositionZ;

                MethodsForGL = new NewAge_EFF_EffectGroup_Methods_MethodsForGL();
                MethodsForGL.GetPosition = GetPosition;
                MethodsForGL.GetAngle = GetAngle;

                MoveMethods = new NodeMoveMethods();
                MoveMethods.GetObjPostion_ToCamera = GetObjPostion_ToCamera;
                MoveMethods.GetObjAngleY_ToCamera = GetObjAngleY_ToCamera;
                MoveMethods.GetObjPostion_ToMove_General = GetObjPostion_ToMove_General;
                MoveMethods.SetObjPostion_ToMove_General = SetObjPostion_ToMove_General;
                MoveMethods.GetObjRotationAngles_ToMove = GetObjRotationAngles_ToMove;
                MoveMethods.SetObjRotationAngles_ToMove = SetObjRotationAngles_ToMove;
                MoveMethods.GetObjScale_ToMove = Utils.GetObjScale_ToMove_Null;
                MoveMethods.SetObjScale_ToMove = Utils.SetObjScale_ToMove_Null;
                MoveMethods.GetTriggerZoneCategory = Utils.GetTriggerZoneCategory_Null;
            }

            /// <summary>
            /// classe com os metodos responsaveis pelo oque sera exibido no node;
            /// </summary>
            public NodeDisplayMethods DisplayMethods { get; }

            /// <summary>
            ///  classe com os metodos responsaveis pela movimentação dos objetos e da camera
            /// </summary>
            public NodeMoveMethods MoveMethods { get; }

            /// <summary>
            /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
            /// </summary>
            public NodeChangeAmountMethods ChangeAmountMethods { get; }

            /// <summary>
            /// Classe com os metodos que serão passados para classe Property;
            /// </summary>
            public NewAge_EFF_EffectGroup_Methods Methods { get; }

            /// <summary>
            /// MethodsForGL
            /// </summary>
            public NewAge_EFF_EffectGroup_Methods_MethodsForGL MethodsForGL { get; }

            #region parte principal

            protected override Endianness GetEndianness()
            {
                return Endian;
            }

            private GroupType GetGrouptype()
            {
                if (EffectEntryTableID == EffectEntryTableID.Table7)
                {
                    return GroupType.EFF_Table7_Effect_0;
                }
                else if (EffectEntryTableID == EffectEntryTableID.Table8)
                {
                    return GroupType.EFF_Table8_Effect_1;
                }
                else
                {
                    return GroupType.NULL;
                }
            }

            private int GetEntryCountInGroup(ushort InternalID)
            {
                return Parent._TableEffectEntry.GetEntryCountInGroup(GetEntryOrderID(InternalID), EffectEntryTableID);
            }

            protected void SetEntryOrderID(ushort InternalID, ushort EntryOrderID)
            {
                var o = Table_Effect_Group[InternalID];
                o.OrderID = EntryOrderID;
                Table_Effect_Group[InternalID] = o;
            }

            protected ushort GetEntryOrderID(ushort InternalID)
            {
                return Table_Effect_Group[InternalID].OrderID;
            }

            protected override byte[] GetInternalLine(ushort ID)
            {
               return Table_Effect_Group[ID].Line;
            }

            protected bool LinesContainsKey(ushort ID)
            {
                return Table_Effect_Group.ContainsKey(ID);
            }

            protected byte[] ReturnLine(ushort ID)
            {
                return (byte[])GetInternalLine(ID).Clone();
            }

            protected void SetLine(ushort ID, byte[] value)
            {
                value.CopyTo(GetInternalLine(ID), 0);
            }

            protected ushort GetNewValidEntryOrderID()
            {
                var elements = Table_Effect_Group.Values.Select(x => x.OrderID).ToArray();
                return elements.Length == 0 ? (ushort)0 : (ushort)(elements.Max() + 1);
            }

            public bool GetExistGroupID(ushort GroupOrderID)
            {
                return Table_Effect_Group.Where(x => x.Value.OrderID == GroupOrderID).Count() != 0;
            }

            public ushort Get_InternalID_From_GroupOrderID(ushort GroupOrderID) 
            {
                var where = Table_Effect_Group.Where(x => x.Value.OrderID == GroupOrderID);
                return where.Count() != 0 ? where.FirstOrDefault().Key : (ushort)0xFFFF;
            }

            private string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        string table = "Error";
                        if (EffectEntryTableID == EffectEntryTableID.Table7)
                        {
                            table = "Table7";
                        }
                        else if (EffectEntryTableID == EffectEntryTableID.Table8)
                        {
                            table = "Table8";
                        }

                        string r = $"EFF {table} - InID: {ID} OrderID: {GetEntryOrderID(ID)}";
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            private Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                else if (!Globals.EFF_RenderTable7 && EffectEntryTableID == EffectEntryTableID.Table7)
                {
                    return Globals.NodeColorHided;
                }
                else if (!Globals.EFF_RenderTable8 && EffectEntryTableID == EffectEntryTableID.Table8)
                {
                    return Globals.NodeColorHided;
                }
                else if (!(Globals.EFF_ShowOnlySelectedGroup == false || (Globals.EFF_ShowOnlySelectedGroup && Globals.EFF_SelectedGroup == GetEntryOrderID(ID))))
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            private void RemoveLineID(ushort ID)
            {
                Table_Effect_Group.Remove(ID);
            }

            private ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = Table_Effect_Group.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID();

                byte[] content = new byte[46];
                content[0x08] = 0xFE;
                content[0x22] = 0x10;

                Table_Effect_Group.Add(newID, (content, newEntryOrderID));
                return newID;
            }

            #endregion

            #region propriedades

            private uint ReturnPositionX_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table_Effect_Group[ID].Line, 0x0C -2, Endian);
            }

            private void SetPositionX_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table_Effect_Group[ID].Line, 0x0C -2);
            }

            private uint ReturnPositionY_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table_Effect_Group[ID].Line, 0x10 -2, Endian);
            }

            private void SetPositionY_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table_Effect_Group[ID].Line, 0x10 -2);
            }

            private uint ReturnPositionZ_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table_Effect_Group[ID].Line, 0x14 -2, Endian);
            }

            private void SetPositionZ_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table_Effect_Group[ID].Line, 0x14 -2);
            }

            private float ReturnPositionX(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionX_Hex(ID)), 0);
            }

            private float ReturnPositionY(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionY_Hex(ID)), 0);
            }

            private float ReturnPositionZ(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionZ_Hex(ID)), 0);
            }

            private void SetPositionX(ushort ID, float value)
            {
                SetPositionX_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetPositionY(ushort ID, float value)
            {
                SetPositionY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetPositionZ(ushort ID, float value)
            {
                SetPositionZ_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private uint ReturnAngleX_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table_Effect_Group[ID].Line, 0x18 -2, Endian);
            }

            private void SetAngleX_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table_Effect_Group[ID].Line, 0x18 -2);
            }

            private uint ReturnAngleY_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table_Effect_Group[ID].Line, 0x1C -2, Endian);
            }

            private void SetAngleY_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table_Effect_Group[ID].Line, 0x1C -2);
            }

            private uint ReturnAngleZ_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(Table_Effect_Group[ID].Line, 0x20 -2, Endian);
            }

            private void SetAngleZ_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(Table_Effect_Group[ID].Line, 0x20 -2);
            }

            private float ReturnAngleX(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleX_Hex(ID)), 0);
            }

            private float ReturnAngleY(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleY_Hex(ID)), 0);
            }

            private float ReturnAngleZ(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleZ_Hex(ID)), 0);
            }

            private void SetAngleX(ushort ID, float value)
            {
                SetAngleX_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetAngleY(ushort ID, float value)
            {
                SetAngleY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetAngleZ(ushort ID, float value)
            {
                SetAngleZ_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            #endregion

            #region parte MethodsForGL
            private Matrix4 GetAngle(ushort ID)
            {
                return Matrix4.CreateRotationX(MathHelper.DegreesToRadians(ReturnAngleX(ID))) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(ReturnAngleY(ID))) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(ReturnAngleZ(ID)));
            }

            private Vector3 GetPosition(ushort ID)
            {
                return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
            }

            #endregion

            #region metodos move

            private Vector3 GetObjPostion_ToCamera(ushort ID)
            {
                Vector3 position = GetPosition(ID);
                Utils.ToCameraCheckValue(ref position);
                return position;
            }

            private float GetObjAngleY_ToCamera(ushort ID)
            {
                float AngleY = MathHelper.DegreesToRadians(ReturnAngleY(ID));
                if (float.IsNaN(AngleY) || float.IsInfinity(AngleY)) { AngleY = 0; }
                return AngleY;
            }


            private Vector3[] GetObjPostion_ToMove_General(ushort ID)
            {
                Vector3[] pos = new Vector3[1];
                pos[0] = new Vector3(ReturnPositionX(ID), ReturnPositionY(ID), ReturnPositionZ(ID));
                Utils.ToMoveCheckLimits(ref pos);
                return pos;
            }


            private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value)
            {
                if (value != null && value.Length >= 1)
                {
                    SetPositionX(ID, value[0].X);
                    SetPositionY(ID, value[0].Y);
                    SetPositionZ(ID, value[0].Z);
                }
            }

            private Vector3[] GetObjRotationAngles_ToMove(ushort ID)
            {
                Vector3[] v = new Vector3[1];
                v[0] = new Vector3(MathHelper.DegreesToRadians(ReturnAngleX(ID)), MathHelper.DegreesToRadians(ReturnAngleY(ID)), MathHelper.DegreesToRadians(ReturnAngleZ(ID)));
                Utils.ToMoveCheckLimits(ref v);
                return v;
            }

            private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value)
            {
                if (value != null && value.Length >= 1)
                {
                    SetAngleX(ID, MathHelper.Clamp(MathHelper.RadiansToDegrees(value[0].X), -360, 360));
                    SetAngleY(ID, MathHelper.Clamp(MathHelper.RadiansToDegrees(value[0].Y), -360, 360));
                    SetAngleZ(ID, MathHelper.Clamp(MathHelper.RadiansToDegrees(value[0].Z), -360, 360));
                }
            }

            #endregion
        }

        public class EffTableEffectEntry : BaseGroup
        {
            public Dictionary<ushort, (byte[] Line, ushort EntryOrderID, ushort GroupOrderID, EffectEntryTableID TableID)> EffectEntry { get; private set; }
            public ushort IdForNewLine = 0;

            /// <summary>
            /// a classe pai dessa;
            /// </summary>
            private File_EFFBLOB_Group Parent { get; }

            /// <summary>
            /// Endian do arquivo;
            /// </summary>
            public Endianness Endian { get; private set; }

            public EffTableEffectEntry(Endianness endian, File_EFFBLOB_Group parent)
            {
                EffectEntry = new Dictionary<ushort, (byte[] Line, ushort EntryOrderID, ushort GroupOrderID, EffectEntryTableID TableID)>();
                Parent = parent;
                Endian = endian;

                DisplayMethods = new NodeDisplayMethods();
                DisplayMethods.GetNodeText = GetNodeText;
                DisplayMethods.GetNodeColor = GetNodeColor;

                ChangeAmountMethods = new NodeChangeAmountMethods();
                ChangeAmountMethods.AddNewLineID = AddNewLineID;
                ChangeAmountMethods.RemoveLineID = RemoveLineID;

                Methods = new NewAge_EFF_EffectEntry_Methods();
                SetBaseMethods(Methods);
                Methods.ReturnLine = ReturnLine;
                Methods.SetLine = SetLine;
                Methods.SetEntryOrderID = SetEntryOrderID;
                Methods.GetEntryOrderID = GetEntryOrderID;
                Methods.SetGroupOrderID = SetGroupOrderID;
                Methods.GetGroupOrderID = GetGroupOrderID;
                Methods.SetTableID = SetTableID;
                Methods.GetTableID = GetTableID;

                Methods.ReturnAngleX_Hex = ReturnAngleX_Hex;
                Methods.ReturnAngleY_Hex = ReturnAngleY_Hex;
                Methods.ReturnAngleZ_Hex = ReturnAngleZ_Hex;
                Methods.ReturnPositionX_Hex = ReturnPositionX_Hex;
                Methods.ReturnPositionY_Hex = ReturnPositionY_Hex;
                Methods.ReturnPositionZ_Hex = ReturnPositionZ_Hex;
                Methods.SetAngleX_Hex = SetAngleX_Hex;
                Methods.SetAngleY_Hex = SetAngleY_Hex;
                Methods.SetAngleZ_Hex = SetAngleZ_Hex;
                Methods.SetPositionX_Hex = SetPositionX_Hex;
                Methods.SetPositionY_Hex = SetPositionY_Hex;
                Methods.SetPositionZ_Hex = SetPositionZ_Hex;
                Methods.ReturnAngleX = ReturnAngleX;
                Methods.ReturnAngleY = ReturnAngleY;
                Methods.ReturnAngleZ = ReturnAngleZ;
                Methods.ReturnPositionX = ReturnPositionX;
                Methods.ReturnPositionY = ReturnPositionY;
                Methods.ReturnPositionZ = ReturnPositionZ;
                Methods.SetAngleX = SetAngleX;
                Methods.SetAngleY = SetAngleY;
                Methods.SetAngleZ = SetAngleZ;
                Methods.SetPositionX = SetPositionX;
                Methods.SetPositionY = SetPositionY;
                Methods.SetPositionZ = SetPositionZ;

                MethodsForGL = new NewAge_EFF_EffectEntry_Methods_MethodsForGL();
                MethodsForGL.GetPosition = GetPosition;
                MethodsForGL.GetAngle = GetAngle;
                MethodsForGL.GetGroupOrderID = GetGroupOrderID;
                MethodsForGL.GetTableID = GetEffectEntryTableID;

                MoveMethods = new NodeMoveMethods();
                MoveMethods.GetObjPostion_ToCamera = GetObjPostion_ToCamera;
                MoveMethods.GetObjAngleY_ToCamera = GetObjAngleY_ToCamera;
                MoveMethods.GetObjPostion_ToMove_General = GetObjPostion_ToMove_General;
                MoveMethods.SetObjPostion_ToMove_General = SetObjPostion_ToMove_General;
                MoveMethods.GetObjRotationAngles_ToMove = GetObjRotationAngles_ToMove;
                MoveMethods.SetObjRotationAngles_ToMove = SetObjRotationAngles_ToMove;
                MoveMethods.GetObjScale_ToMove = Utils.GetObjScale_ToMove_Null;
                MoveMethods.SetObjScale_ToMove = Utils.SetObjScale_ToMove_Null;
                MoveMethods.GetTriggerZoneCategory = Utils.GetTriggerZoneCategory_Null;
            }

            /// <summary>
            /// classe com os metodos responsaveis pelo oque sera exibido no node;
            /// </summary>
            public NodeDisplayMethods DisplayMethods { get; }

            /// <summary>
            /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
            /// </summary>
            public NodeChangeAmountMethods ChangeAmountMethods { get; }

            /// <summary>
            /// Classe com os metodos que serão passados para classe Property;
            /// </summary>
            public NewAge_EFF_EffectEntry_Methods Methods { get; }

            /// <summary>
            /// MethodsForGL
            /// </summary>
            public NewAge_EFF_EffectEntry_Methods_MethodsForGL MethodsForGL { get; }

            /// <summary>
            ///  classe com os metodos responsaveis pela movimentação dos objetos e da camera
            /// </summary>
            public NodeMoveMethods MoveMethods { get; }

            /// <summary>
            /// classe com os metodos usado para arrumar os index (ordem) das entrys
            /// </summary>
            public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods { get; set; }

            #region parte principal

            protected override Endianness GetEndianness()
            {
                return Endian;
            }

            protected override byte[] GetInternalLine(ushort ID)
            {
                return EffectEntry[ID].Line;
            }

            private bool LinesContainsKey(ushort ID)
            {
                return EffectEntry.ContainsKey(ID);
            }

            protected void SetEntryOrderID(ushort InternalID, ushort EntryOrderID)
            {
                var o = EffectEntry[InternalID];
                o.EntryOrderID = EntryOrderID;
                EffectEntry[InternalID] = o;
            }

            protected ushort GetEntryOrderID(ushort InternalID)
            {
                return EffectEntry[InternalID].EntryOrderID;
            }

            protected ushort GetGroupOrderID(ushort InternalID)
            {
                return EffectEntry[InternalID].GroupOrderID;
            }

            protected void SetGroupOrderID(ushort InternalID, ushort GroupOrderID)
            {
                if (GroupOrderID < Consts.AmountLimitEFF_Table7and8)
                {
                    var o = EffectEntry[InternalID];
                    o.GroupOrderID = GroupOrderID;
                    EffectEntry[InternalID] = o;
                    ChangeAmountCallbackMethods.OnMoveNode();
                }
            }

            private byte GetTableID(ushort ID)
            {
                return (byte)EffectEntry[ID].TableID;
            }

            private void SetTableID(ushort ID, byte value)
            {
                if (value == 8 || value == 7)
                {
                    var o = EffectEntry[ID];
                    o.TableID = (EffectEntryTableID)value;
                    EffectEntry[ID] = o;
                    ChangeAmountCallbackMethods.OnMoveNode();
                }
            }

            private ushort GetNewValidEntryOrderID(ushort GroupOrderID, EffectEntryTableID table)
            {
                var elements = EffectEntry.Values.Where(x => x.GroupOrderID == GroupOrderID && x.TableID == table).Select(x => x.EntryOrderID).ToArray();
                return elements.Length == 0 ? (ushort)0 : (ushort)(elements.Max() + 1);
            }

            private EffectEntryTableID GetEffectEntryTableID(ushort ID) 
            {
                return EffectEntry[ID].TableID;
            }

            private byte[] ReturnLine(ushort ID)
            {
                return (byte[])GetInternalLine(ID).Clone();
            }

            private void SetLine(ushort ID, byte[] value)
            {
                value.CopyTo(GetInternalLine(ID), 0);
            }

            private bool GetExistGroupID(ushort InternalID)
            {
                EffectEntryTableID tableID = GetEffectEntryTableID(InternalID);
                if (Parent._TableEffectGroups.ContainsKey(tableID))
                {
                    return Parent._TableEffectGroups[tableID].GetExistGroupID(GetGroupOrderID(InternalID));
                }
                return false;
            }

            public int GetEntryCountInGroup(ushort GroupOrderID, EffectEntryTableID TableID)
            {
                return EffectEntry.Values.Where(x => x.GroupOrderID == GroupOrderID && x.TableID == TableID).Count();
            }

            private string GetNodeText(ushort ID)
            {
                if (LinesContainsKey(ID))
                {
                    if (Globals.TreeNodeRenderHexValues)
                    {
                        return BitConverter.ToString(GetInternalLine(ID)).Replace("-", "_");
                    }
                    else
                    {
                        string r = "EFF EffectEntry - InID: " + ID + "  OrderID: " + GetEntryOrderID(ID) + "  GroupID: " + GetGroupOrderID(ID);
                        if (GetEffectEntryTableID(ID) == EffectEntryTableID.Table7)
                        {
                            r += "  Table7 Effect0";
                        }
                        else if (GetEffectEntryTableID(ID) == EffectEntryTableID.Table8)
                        {
                            r += "  Table8 Effect1";
                        }
                        else 
                        {
                            r += "  ERROR";
                        }

                        byte EspID = GetInternalLine(ID)[1];
                        byte TexID = GetInternalLine(ID)[2];

                        r += "  EspID: 0x" + EspID.ToString("X2") + "  TexID: 0x" + TexID.ToString("X2");  

                        if (!GetExistGroupID(ID))
                        {
                            r += "  This group does not exist!";
                        }
                        return r;
                    }
                }

                return "Error Internal Line ID " + ID;
            }

            private Color GetNodeColor(ushort ID)
            {
                if (!Globals.RenderFileEFFBLOB)
                {
                    return Globals.NodeColorHided;
                }
                else if (!Globals.EFF_RenderTable7 && GetEffectEntryTableID(ID) == EffectEntryTableID.Table7)
                {
                    return Globals.NodeColorHided;
                }
                else if (!Globals.EFF_RenderTable8 && GetEffectEntryTableID(ID) == EffectEntryTableID.Table8)
                {
                    return Globals.NodeColorHided;
                }
                else if (!(Globals.EFF_ShowOnlySelectedGroup == false || (Globals.EFF_ShowOnlySelectedGroup && Globals.EFF_SelectedGroup == GetGroupOrderID(ID))))
                {
                    return Globals.NodeColorHided;
                }
                return Globals.NodeColorEntry;
            }

            private void RemoveLineID(ushort ID)
            {
                EffectEntry.Remove(ID);
            }

            private ushort AddNewLineID(byte initType)
            {
                ushort newID = IdForNewLine;
                if (IdForNewLine == ushort.MaxValue)
                {
                    var Ushots = Utils.AllUshots();
                    var Useds = EffectEntry.Keys.ToList();
                    Ushots.RemoveAll(x => Useds.Contains(x));
                    newID = Ushots[0];
                }
                else
                {
                    IdForNewLine++;
                }

                ushort newEntryOrderID = GetNewValidEntryOrderID(0, (EffectEntryTableID)initType);

                byte[] content = new byte[300];

                EffectEntry.Add(newID, (content, newEntryOrderID, 0, (EffectEntryTableID)initType));
                return newID;
            }

            #endregion

            #region propriedades

            private uint ReturnPositionX_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(EffectEntry[ID].Line, 0x0C, Endian);
            }

            private void SetPositionX_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(EffectEntry[ID].Line, 0x0C);
            }

            private uint ReturnPositionY_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(EffectEntry[ID].Line, 0x10, Endian);
            }

            private void SetPositionY_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(EffectEntry[ID].Line, 0x10);
            }

            private uint ReturnPositionZ_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(EffectEntry[ID].Line, 0x14, Endian);
            }

            private void SetPositionZ_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(EffectEntry[ID].Line, 0x14);
            }

            private float ReturnPositionX(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionX_Hex(ID)), 0);
            }

            private float ReturnPositionY(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionY_Hex(ID)), 0);
            }

            private float ReturnPositionZ(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionZ_Hex(ID)), 0);
            }

            private void SetPositionX(ushort ID, float value)
            {
                SetPositionX_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetPositionY(ushort ID, float value)
            {
                SetPositionY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetPositionZ(ushort ID, float value)
            {
                SetPositionZ_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private uint ReturnAngleX_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(EffectEntry[ID].Line, 0x58, Endian);
            }

            private void SetAngleX_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(EffectEntry[ID].Line, 0x58);
            }

            private uint ReturnAngleY_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(EffectEntry[ID].Line, 0x5C, Endian);
            }

            private void SetAngleY_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(EffectEntry[ID].Line, 0x5C);
            }

            private uint ReturnAngleZ_Hex(ushort ID)
            {
                return EndianBitConverter.ToUInt32(EffectEntry[ID].Line, 0x60, Endian);
            }

            private void SetAngleZ_Hex(ushort ID, uint value)
            {
                EndianBitConverter.GetBytes(value, Endian).CopyTo(EffectEntry[ID].Line, 0x60);
            }

            private float ReturnAngleX(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleX_Hex(ID)), 0);
            }

            private float ReturnAngleY(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleY_Hex(ID)), 0);
            }

            private float ReturnAngleZ(ushort ID)
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleZ_Hex(ID)), 0);
            }

            private void SetAngleX(ushort ID, float value)
            {
                SetAngleX_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetAngleY(ushort ID, float value)
            {
                SetAngleY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            private void SetAngleZ(ushort ID, float value)
            {
                SetAngleZ_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
            }

            #endregion

            #region parte MethodsForGL
            private Matrix4 GetAngle(ushort ID)
            {
                return Matrix4.CreateRotationX(MathHelper.DegreesToRadians(ReturnAngleX(ID))) * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(ReturnAngleY(ID))) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(ReturnAngleZ(ID)));
            }

            private Vector3 GetPosition(ushort ID)
            {
                return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
            }

            #endregion

            #region metodos move

            private Vector3 GetObjPostion_ToCamera(ushort InternalID)
            {
                EffectEntryTableID tableID = GetEffectEntryTableID(InternalID);
                EffTableEffectGroup tableGroup = null;
                if (Parent._TableEffectGroups.ContainsKey(tableID))
                {
                    tableGroup = Parent._TableEffectGroups[tableID];
                }

                Vector3 GroupPos = Vector3.Zero;
                Matrix4 GroupRot = Matrix4.Identity;
                if (GetExistGroupID(InternalID) && tableGroup != null && Globals.EFF_Use_Group_Position)
                {
                    GroupPos = tableGroup.MethodsForGL.GetPosition(tableGroup.Get_InternalID_From_GroupOrderID(GetGroupOrderID(InternalID)));
                    GroupRot = tableGroup.MethodsForGL.GetAngle(tableGroup.Get_InternalID_From_GroupOrderID(GetGroupOrderID(InternalID)));
                }

                Vector4 position = new Vector4(GetPosition(InternalID));
                position *= GroupRot;
                position += new Vector4(GroupPos);
                Vector3 pos = position.Xyz;

                Utils.ToCameraCheckValue(ref pos);
                return pos;
            }

            private float GetObjAngleY_ToCamera(ushort ID)
            {
                float AngleY = MathHelper.DegreesToRadians(ReturnAngleY(ID));
                if (float.IsNaN(AngleY) || float.IsInfinity(AngleY)) { AngleY = 0; }
                return AngleY;
            }


            private Vector3[] GetObjPostion_ToMove_General(ushort ID)
            {
                Vector3[] pos = new Vector3[1];
                pos[0] = new Vector3(ReturnPositionX(ID), ReturnPositionY(ID), ReturnPositionZ(ID));
                Utils.ToMoveCheckLimits(ref pos);
                return pos;
            }


            private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value)
            {
                if (value != null && value.Length >= 1)
                {
                    SetPositionX(ID, value[0].X);
                    SetPositionY(ID, value[0].Y);
                    SetPositionZ(ID, value[0].Z);
                }
            }

            private Vector3[] GetObjRotationAngles_ToMove(ushort ID)
            {
                Vector3[] v = new Vector3[1];
                v[0] = new Vector3(MathHelper.DegreesToRadians(ReturnAngleX(ID)), MathHelper.DegreesToRadians(ReturnAngleY(ID)), MathHelper.DegreesToRadians(ReturnAngleZ(ID)));
                Utils.ToMoveCheckLimits(ref v);
                return v;
            }

            private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value)
            {
                if (value != null && value.Length >= 1)
                {
                    SetAngleX(ID, MathHelper.Clamp(MathHelper.RadiansToDegrees(value[0].X), -360, 360));
                    SetAngleY(ID, MathHelper.Clamp(MathHelper.RadiansToDegrees(value[0].Y), -360, 360));
                    SetAngleZ(ID, MathHelper.Clamp(MathHelper.RadiansToDegrees(value[0].Z), -360, 360));
                }
            }

            #endregion
        }
    }
}
