using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System.Drawing;
using OpenTK;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    /// <summary>
    /// Classe que representa o arquivo .LIT (Light);
    /// </summary>
    public class File_LIT_Group
    {
        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }

        /// <summary>
        /// <para>aqui contem o começo do arquivo, 2 bytes desconhecidos;</para>
        /// </summary>
        public byte[] HeaderFile;

        /// <summary>
        /// classe com o conteudo da parte dos LightGroups
        /// </summary>
        public File_LIT_LightGroups LightGroups { get; private set; }

        /// <summary>
        /// classe com o conteudo da parte dos LightEntrys
        /// </summary>
        public File_LIT_LightEntrys LightEntrys { get; private set; }

        public File_LIT_Group(Re4Version version) 
        {
            GetRe4Version = version;
            HeaderFile = new byte[2];
            LightGroups = new File_LIT_LightGroups(version, this);
            LightEntrys = new File_LIT_LightEntrys(version, this);
        }

    }

    public class File_LIT_LightGroups : BaseGroup
    {
        /// <summary>
        /// a classe pai dessa
        /// </summary>
        private File_LIT_Group Parent { get; }

        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }

        /// <summary>
        /// <para>aqui contem o conteudo de todos as LightGroups do arquivo;</para>
        /// <para>id da linha, sequencia de 100 bytes (.LIT LightGroup) para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 260 bytes (.LIT LightGroup) para re4 uhd;</para>
        /// </summary>
        public Dictionary<ushort, byte[]> Lines { get; private set; }
        
        /// <summary>
        /// define a ordem no arquivo final
        /// </summary>
        public Dictionary<ushort, ushort> InternalID_GroupOrderID { get; private set; }

        /// <summary>
        /// Id para ser usado para adicionar novas linhas;
        /// </summary>
        public ushort IdForNewLine = 0;

        public File_LIT_LightGroups(Re4Version version, File_LIT_Group parent)
        {
            Parent = parent;
            GetRe4Version = version;
            Lines = new Dictionary<ushort, byte[]>();
            InternalID_GroupOrderID = new Dictionary<ushort, ushort>();

            DisplayMethods = new NodeDisplayMethods();
            DisplayMethods.GetNodeText = GetNodeText;
            DisplayMethods.GetNodeColor = GetNodeColor;

            MoveMethods = new NodeMoveMethods();
            MoveMethods.GetObjPostion_ToCamera = Utils.GetObjPostion_ToCamera_Null;
            MoveMethods.GetObjAngleY_ToCamera = Utils.GetObjAngleY_ToCamera_Null;
            MoveMethods.GetObjPostion_ToMove_General = Utils.GetObjPostion_ToMove_General_Null;
            MoveMethods.SetObjPostion_ToMove_General = Utils.SetObjPostion_ToMove_General_Null;
            MoveMethods.GetObjRotationAngles_ToMove = Utils.GetObjRotationAngles_ToMove_Null;
            MoveMethods.SetObjRotationAngles_ToMove = Utils.SetObjRotationAngles_ToMove_Null;
            MoveMethods.GetObjScale_ToMove = Utils.GetObjScale_ToMove_Null;
            MoveMethods.SetObjScale_ToMove = Utils.SetObjScale_ToMove_Null;
            MoveMethods.GetTriggerZoneCategory = Utils.GetTriggerZoneCategory_Null;

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;

            Methods = new NewAge_LIT_Group_Methods();
            SetBaseMethods(Methods);
            Methods.ReturnRe4Version = ReturnRe4Version;
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            Methods.SetGroupOrderID = SetGroupOrderID;
            Methods.GetGroupOrderID = GetGroupOrderID;
            Methods.GetEntryCountInGroup = GetEntryCountInGroup;
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
        /// Classe com os metodos que serão passados para classe NewAge_LIT_Group_Property;
        /// </summary>
        public NewAge_LIT_Group_Methods Methods { get; }

        /// <summary>
        /// Classe com os metodos para o GL
        /// </summary>
        public NewAge_LIT_Group_MethodsForGL MethodsForGL { get; }

        #region extra

        private int GetEntryCountInGroup(ushort InternalID)
        {
            return Parent.LightEntrys.GetEntryCountInGroup(GetGroupOrderID(InternalID));
        }

        public bool GetExistGroupID(ushort GroupOrderID) 
        {
            return InternalID_GroupOrderID.Values.Contains(GroupOrderID);
        }

        #endregion

        #region DisplayMethods
        public string GetNodeText(ushort ID)
        {
            if (Lines.ContainsKey(ID))
            {
                if (Globals.TreeNodeRenderHexValues)
                {
                    return BitConverter.ToString(Lines[ID]).Replace("-", "_");
                }
                else
                {
                    string r = "LIT LightGroup - InID: " + ID + "  GroupOrderID: " + InternalID_GroupOrderID[ID];
                    return r;
                }
            }

            return "LIT LightGroup Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileLIT)
            {
                return Globals.NodeColorHided;
            }
            else if ( ! (Globals.LIT_ShowOnlySelectedGroup == false || (Globals.LIT_ShowOnlySelectedGroup && Globals.LIT_SelectedGroup == GetGroupOrderID(ID))))
            {
                return Globals.NodeColorHided;
            }
            return Globals.NodeColorEntry;
        }
        #endregion

        #region ChangeAmountMethods

        private ushort AddNewLineID(byte initType)
        {
            ushort newID = IdForNewLine;
            if (IdForNewLine == ushort.MaxValue)
            {
                var Ushots = Utils.AllUshots();
                var Useds = Lines.Keys.ToList();
                Ushots.RemoveAll(x => Useds.Contains(x));
                newID = Ushots[0];
            }
            else
            {
                IdForNewLine++;
            }

            ushort newGroupOrderID = GetNewValidGroupOrderID();

            byte[] content = new byte[0];
            if (GetRe4Version == Re4Version.UHD)
            {
                content = new byte[260];
               
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                content = new byte[100];
            }

            Lines.Add(newID, content);
            InternalID_GroupOrderID.Add(newID, newGroupOrderID);
            return newID;
        }

        private void RemoveLineID(ushort ID)
        {
            Lines.Remove(ID);
            InternalID_GroupOrderID.Remove(ID);
        }


        private ushort GetNewValidGroupOrderID() 
        {
            ushort res = 0;
            while (true)
            {
                if (!InternalID_GroupOrderID.Values.Contains(res))
                {
                    return res;
                }
                else 
                {
                    res++;
                }
            }
        }

        #endregion

        #region metodos das propriedades

        protected override byte[] GetInternalLine(ushort ID)
        {
            return Lines[ID];
        }

        private Re4Version ReturnRe4Version()
        {
            return GetRe4Version;
        }

        private byte[] ReturnLine(ushort ID)
        {
            return (byte[])Lines[ID].Clone();
        }

        private void SetLine(ushort ID, byte[] value)
        {
            value.CopyTo(Lines[ID], 0);
        }

        #endregion

        #region GroupOrderID

        private void SetGroupOrderID(ushort InternalID, ushort GroupOrderID)
        {
            if (GroupOrderID < Consts.AmountLimitLIT_Groups && !InternalID_GroupOrderID.Values.Contains(GroupOrderID))
            {
                InternalID_GroupOrderID[InternalID] = GroupOrderID;
            }
        }

        private ushort GetGroupOrderID(ushort InternalID)
        {
            return InternalID_GroupOrderID[InternalID];
        }

        #endregion
    }


    public class File_LIT_LightEntrys : BaseGroup
    {
        /// <summary>
        /// a classe pai dessa
        /// </summary>
        private File_LIT_Group Parent { get; }

        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }

        /// <summary>
        /// <para>aqui contem o conteudo de todos as LightEntrys do arquivo;</para>
        /// <para>id da linha, sequencia de 112 bytes (.LIT LightEntry) para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 300 bytes (.LIT LightEntry) para re4 uhd;</para>
        /// </summary>
        public Dictionary<ushort, byte[]> Lines { get; private set; }
        /// <summary>
        /// define a ordem e de qual grupo pertence
        /// </summary>
        public Dictionary<ushort, (ushort EntryOrderID, ushort GroupOrderID)> GroupConnection { get; private set; }

        /// <summary>
        /// Id para ser usado para adicionar novas linhas;
        /// </summary>
        public ushort IdForNewLine = 0;


        public File_LIT_LightEntrys(Re4Version version, File_LIT_Group parent) 
        {
            Parent = parent;
            GetRe4Version = version;
            Lines = new Dictionary<ushort, byte[]>();
            GroupConnection = new Dictionary<ushort, (ushort EntryOrderID, ushort GroupOrderID)>();

            DisplayMethods = new NodeDisplayMethods();
            DisplayMethods.GetNodeText = GetNodeText;
            DisplayMethods.GetNodeColor = GetNodeColor;

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

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;

            Methods = new NewAge_LIT_Entry_Methods();
            SetBaseMethods(Methods);
            Methods.ReturnRe4Version = ReturnRe4Version;
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

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
            Methods.ReturnRangeRadius_Hex = ReturnRangeRadius_Hex;
            Methods.SetRangeRadius_Hex = SetRangeRadius_Hex;
            Methods.ReturnRangeRadius = ReturnRangeRadius;
            Methods.SetRangeRadius = SetRangeRadius;
            Methods.ReturnIntensity = ReturnIntensity;
            Methods.SetIntensity = SetIntensity;
            Methods.ReturnIntensity_Hex = ReturnIntensity_Hex;
            Methods.SetIntensity_Hex = SetIntensity_Hex;

            Methods.ReturnColorRGB = ReturnColorRGB;
            Methods.SetColorRGB = SetColorRGB;
            Methods.ReturnColorAlfa = ReturnColorAlfa;
            Methods.SetColorAlfa = SetColorAlfa;

            Methods.SetEntryOrderID = SetEntryOrderID;
            Methods.SetGroupOrderID = SetGroupOrderID;
            Methods.GetGroupOrderID = GetGroupOrderID;
            Methods.GetEntryOrderID = GetEntryOrderID;

            MethodsForGL = new NewAge_LIT_Entry_MethodsForGL();
            MethodsForGL.GetPosition = GetPosition;
            MethodsForGL.GetRangeRadius = GetRangeRadius;
            MethodsForGL.GetGroupOrderID = GetGroupOrderID;
            MethodsForGL.GetLightColor = GetLightColor;
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
        /// Classe com os metodos que serão passados para classe NewAge_LIT_Entry_Property;
        /// </summary>
        public NewAge_LIT_Entry_Methods Methods { get; }

        /// <summary>
        /// Classe com os metodos para o GL
        /// </summary>
        public NewAge_LIT_Entry_MethodsForGL MethodsForGL { get; }

        /// <summary>
        /// 
        /// </summary>
        public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods { get; set; }

        #region extra

        public int GetEntryCountInGroup(ushort GroupOrderID)
        {
            return GroupConnection.Values.Where(x => x.GroupOrderID == GroupOrderID).Count();
        }

        private bool GetExistGroupID(ushort InternalID) 
        {
            return Parent.LightGroups.GetExistGroupID(GetGroupOrderID(InternalID));
        }

        #endregion

        #region DisplayMethods
        public string GetNodeText(ushort ID)
        {
            if (Lines.ContainsKey(ID))
            {
                if (Globals.TreeNodeRenderHexValues)
                {
                    return BitConverter.ToString(Lines[ID]).Replace("-", "_");
                }
                else
                {
                    string r = "LIT LightEntry - InID: " + ID + "  EntryOrderID: " + GroupConnection[ID].EntryOrderID + "  Group: " + GroupConnection[ID].GroupOrderID
                        + "  [" + BitConverter.ToString(Lines[ID].Take(4).ToArray()) + "]";
                    if (!GetExistGroupID(ID))
                    {
                        r += "  This group does not exist!";
                    }
                    return r;
                }
            }

            return "LIT LightEntry Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileLIT)
            {
                return Globals.NodeColorHided;
            }
            else if ( ! (Globals.LIT_ShowOnlySelectedGroup == false || (Globals.LIT_ShowOnlySelectedGroup && Globals.LIT_SelectedGroup == GetGroupOrderID(ID)) ))
            {
                return Globals.NodeColorHided;
            }
            return Globals.NodeColorEntry;
        }
        #endregion


        #region ChangeAmountMethods

        private ushort AddNewLineID(byte initType)
        {
            ushort newID = IdForNewLine;
            if (IdForNewLine == ushort.MaxValue)
            {
                var Ushots = Utils.AllUshots();
                var Useds = Lines.Keys.ToList();
                Ushots.RemoveAll(x => Useds.Contains(x));
                newID = Ushots[0];
            }
            else
            {
                IdForNewLine++;
            }

            ushort newEntryOrderID = GetNewValidEntryOrderID(0);

            byte[] content = new byte[0];
            if (GetRe4Version == Re4Version.UHD)
            {
                content = new byte[300];

                content[0x17] = 0x80;

            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                content = new byte[112];
                content[0x0B] = 0x80;
            }
            content[0x00] = 0x07;
            content[0x1F] = 0x03;

            Lines.Add(newID, content);
            GroupConnection.Add(newID, (newEntryOrderID, 0));
            return newID;
        }

        private void RemoveLineID(ushort ID)
        {
            Lines.Remove(ID);
            GroupConnection.Remove(ID);
            ChangeAmountCallbackMethods.OnDeleteNode();
        }

        private ushort GetNewValidEntryOrderID(ushort GroupOrderID) 
        {
            var elements = GroupConnection.Values.Where(x => x.GroupOrderID == GroupOrderID).Select(x => x.EntryOrderID).ToArray();
            return elements.Length == 0 ? (ushort)0 : (ushort)(elements.Max() + 1);
        }

        private void SetEntryOrderID(ushort InternalID, ushort EntryOrderID) 
        {
            var o = GroupConnection[InternalID];
            o.EntryOrderID = EntryOrderID;
            GroupConnection[InternalID] = o;
        }

        private void SetGroupOrderID(ushort InternalID, ushort GroupOrderID) 
        {
            if (GroupOrderID < Consts.AmountLimitLIT_Groups)
            {
                var o = GroupConnection[InternalID];
                o.GroupOrderID = GroupOrderID;
                GroupConnection[InternalID] = o;
                ChangeAmountCallbackMethods.OnMoveNode();
            }
        }

        private ushort GetGroupOrderID(ushort InternalID) 
        {
            return GroupConnection[InternalID].GroupOrderID;
        }

        private ushort GetEntryOrderID(ushort InternalID) 
        {
            return GroupConnection[InternalID].EntryOrderID;
        }

        #endregion


        #region metodos das propriedades

        protected override byte[] GetInternalLine(ushort ID)
        {
            return Lines[ID];
        }

        private Re4Version ReturnRe4Version()
        {
            return GetRe4Version;
        }

        private byte[] ReturnLine(ushort ID)
        {
            return (byte[])Lines[ID].Clone();
        }

        private void SetLine(ushort ID, byte[] value)
        {
            value.CopyTo(Lines[ID], 0);
        }

        #endregion


        #region LIT_Light_Definition_Category

        private uint ReturnPositionX_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x04);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x10);
            }
            else { return 0; }
        }

        private void SetPositionX_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x04] = b[0];
                Lines[ID][0x05] = b[1];
                Lines[ID][0x06] = b[2];
                Lines[ID][0x07] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x10] = b[0];
                Lines[ID][0x11] = b[1];
                Lines[ID][0x12] = b[2];
                Lines[ID][0x13] = b[3];
            }
        }

        private uint ReturnPositionY_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x08);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x14);
            }
            else { return 0; }
        }

        private void SetPositionY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x08] = b[0];
                Lines[ID][0x09] = b[1];
                Lines[ID][0x0A] = b[2];
                Lines[ID][0x0B] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x14] = b[0];
                Lines[ID][0x15] = b[1];
                Lines[ID][0x16] = b[2];
                Lines[ID][0x17] = b[3];
            }
        }

        private uint ReturnPositionZ_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x0C);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x18);
            }
            else { return 0; }
        }

        private void SetPositionZ_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x0C] = b[0];
                Lines[ID][0x0D] = b[1];
                Lines[ID][0x0E] = b[2];
                Lines[ID][0x0F] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x18] = b[0];
                Lines[ID][0x19] = b[1];
                Lines[ID][0x1A] = b[2];
                Lines[ID][0x1B] = b[3];
            }
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

        private uint ReturnRangeRadius_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x10);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x04);
            }
            else { return 0; }
        }

        private void SetRangeRadius_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x10] = b[0];
                Lines[ID][0x11] = b[1];
                Lines[ID][0x12] = b[2];
                Lines[ID][0x13] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x04] = b[0];
                Lines[ID][0x05] = b[1];
                Lines[ID][0x06] = b[2];
                Lines[ID][0x07] = b[3];
            }
        }

        private float ReturnRangeRadius(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnRangeRadius_Hex(ID)), 0);
        }

        private void SetRangeRadius(ushort ID, float value)
        {
            SetRangeRadius_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }


        private uint ReturnIntensity_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x18);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x0C);
            }
            else { return 0; }
        }

        private void SetIntensity_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x18] = b[0];
                Lines[ID][0x19] = b[1];
                Lines[ID][0x1A] = b[2];
                Lines[ID][0x1B] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x0C] = b[0];
                Lines[ID][0x0D] = b[1];
                Lines[ID][0x0E] = b[2];
                Lines[ID][0x0F] = b[3];
            }
        }

        private float ReturnIntensity(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnIntensity_Hex(ID)), 0);
        }

        private void SetIntensity(ushort ID, float value)
        {
            SetIntensity_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }

        private byte[] ReturnColorRGB(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                byte[] rgb = new byte[3];
                rgb[0] = Lines[ID][0x14];
                rgb[1] = Lines[ID][0x15];
                rgb[2] = Lines[ID][0x16];
                return rgb;
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] rgb = new byte[3];
                rgb[0] = Lines[ID][0x08];
                rgb[1] = Lines[ID][0x09];
                rgb[2] = Lines[ID][0x0A];
                return rgb;
            }
            else { return new byte[3]; }
        }

        private void SetColorRGB(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x14] = value[0];
                Lines[ID][0x15] = value[1];
                Lines[ID][0x16] = value[2];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x08] = value[0];
                Lines[ID][0x09] = value[1];
                Lines[ID][0x0A] = value[2];
            }
        }

        private byte ReturnColorAlfa(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return Lines[ID][0x17];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return Lines[ID][0x0B];
            }
            else { return 0; }
        }

        private void SetColorAlfa(ushort ID, byte value)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x17] = value;
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x0B] = value;
            }
        }

        #endregion


        #region metodos para o GL

        private Vector3 GetPosition(ushort ID)
        {
            return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
        }

        private float GetRangeRadius(ushort ID) 
        {
            return ReturnRangeRadius(ID) / 100;
        }

        private Vector4 GetLightColor(ushort ID)
        {
            return new Vector4(ReturnColorRGB(ID)[0] / 255f, ReturnColorRGB(ID)[1] / 255f, ReturnColorRGB(ID)[2] / 255f, 1f);
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

}
