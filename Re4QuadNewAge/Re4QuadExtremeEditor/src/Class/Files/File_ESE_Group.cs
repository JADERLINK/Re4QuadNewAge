using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System.Drawing;
using OpenTK;
using SimpleEndianBinaryIO;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    /// <summary>
    /// Classe que representa os arquivos .ESE (Environment Sound Effect);
    /// </summary>
    public class File_ESE_Group : BaseGroup
    {
        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }
        /// <summary>
        /// <para>aqui contem o comeco do arquivo 16 bytes fixos;</para>
        /// <para>offset 0, 1 e 2 formato do arquivo, ascii: ESE;</para>
        /// <para>offset 3 fixo: 0x00;</para>
        /// <para>offset 4 e 5: ESE: 0x0001;</para>
        /// <para>offset 6 e 7: ushort value usado para a quantidade de "linhas";</para>
        /// </summary>
        public byte[] StartFile;
        /// <summary>
        /// <para>aqui contem o conteudo de todos as entrys do arquivo;</para>
        /// <para>id da linha, sequencia de 48 bytes (.ESE) para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 44 bytes (.ESE) para re4 uhd;</para>
        /// </summary>
        public Dictionary<ushort, byte[]> Lines { get; private set; }
        /// <summary>
        /// aqui contem o resto do arquivo, a parte não usada;
        /// </summary>
        public byte[] EndFile;
        /// <summary>
        /// Id para ser usado para adicionar novas linhas;
        /// </summary>
        public ushort IdForNewLine = 0;

        /// <summary>
        /// lista de FirstIndex offset[0x01] UHD e offset[0x11]
        /// <para>FirstIndex, line</para>
        /// </summary>
        private Dictionary<byte, List<ushort>> FirstIndexList;


        public File_ESE_Group(Re4Version version) 
        {
            GetRe4Version = version;
            StartFile = new byte[16];
            Lines = new Dictionary<ushort, byte[]>();
            EndFile = new byte[0];
            FirstIndexList = new Dictionary<byte, List<ushort>>();

            DisplayMethods = new NodeDisplayMethods();
            DisplayMethods.GetNodeText = GetNodeText;
            DisplayMethods.GetNodeColor = GetNodeColor;

            MoveMethods = new NodeMoveMethods();
            MoveMethods.GetObjPostion_ToCamera = GetObjPostion_ToCamera;
            MoveMethods.GetObjAngleY_ToCamera = GetObjAngleY_ToCamera;
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

            Methods = new NewAge_ESE_Methods();
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

            MethodsForGL = new NewAge_ESE_MethodsForGL();
            MethodsForGL.GetPosition = GetPosition;
        }


        /// <summary>
        /// Classe com os metodos que serão passados para classe NewAge_ESE_Property;
        /// </summary>
        public NewAge_ESE_Methods Methods { get; }

        /// <summary>
        /// classe com os metodos responsaveis pelo oque sera exibido no node;
        /// </summary>
        public NodeDisplayMethods DisplayMethods { get; }

        /// <summary>
        ///  classe com os metodos responsaveis pela movimentação dos objetos e da camera
        /// </summary>
        public NodeMoveMethods MoveMethods { get; }

        /// <summary>
        /// Classe com os metodos para o GL
        /// </summary>
        public NewAge_ESE_MethodsForGL MethodsForGL { get; }

        /// <summary>
        /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
        /// </summary>
        public NodeChangeAmountMethods ChangeAmountMethods { get; }

        //metodos:
        #region metodos para os Nodes

        // texto do treeNode
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
                    string r = "ESE Object Internal Line ID " + ID;
                    return r;
                }
            }
         
            return "ESE Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileESE)
            {
                return Globals.NodeColorHided;
            }
            return Globals.NodeColorEntry;
        }

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

            byte newFirstIndex = 0;
            if (FirstIndexList.Count > 0)
            {
                newFirstIndex = FirstIndexList.Max(o => o.Key);
            }
            if (newFirstIndex != byte.MaxValue)
            {
                newFirstIndex++;
            }

            byte[] content = null;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                content = new byte[48];

                content[0x10] = 0x03; //fixed 0x03
                content[0x11] = newFirstIndex;

                content[0x0C] = 0x3F;
                content[0x0D] = 0x80;
            }
            else if (GetRe4Version == Re4Version.UHD)
            {
                content = new byte[44];

                content[0x00] = 0x03; //fixed 0x03
                content[0x01] = newFirstIndex;
            }

            AddNewFirstIndexList(newID, newFirstIndex);
            Lines.Add(newID, content);
            return newID;
        }

        private void RemoveLineID(ushort ID)
        {
            RemoveFirstIndexList(ID, ReturnFirstIndex(ID));
            Lines.Remove(ID);
        }

        #endregion

        #region metodos das propriedades

        protected override byte[] GetInternalLine(ushort ID)
        {
            return Lines[ID];
        }

        protected override Endianness GetEndianness()
        {
            return Endianness.LittleEndian;
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


        private byte ReturnFirstIndex(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                return Lines[ID][0x11];
            }
            else if (GetRe4Version == Re4Version.UHD)
            {
                return Lines[ID][0x01];
            }

            return 0;
        }

        private void SetFirstIndex(ushort ID, byte value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte OldIndex = ReturnFirstIndex(ID);
                Lines[ID][0x11] = value;
                UpdateFirstIndexList(ID, OldIndex, value);
            }
            else if (GetRe4Version == Re4Version.UHD)
            {
                byte OldIndex = ReturnFirstIndex(ID);
                Lines[ID][0x01] = value;
                UpdateFirstIndexList(ID, OldIndex, value);
            }
        
        }

        #endregion

        #region position

        private uint ReturnPositionX_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x04);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x00);
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
                Lines[ID][0x00] = b[0];
                Lines[ID][0x01] = b[1];
                Lines[ID][0x02] = b[2];
                Lines[ID][0x03] = b[3];
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
                return BitConverter.ToUInt32(Lines[ID], 0x04);
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
                Lines[ID][0x04] = b[0];
                Lines[ID][0x05] = b[1];
                Lines[ID][0x06] = b[2];
                Lines[ID][0x07] = b[3];
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
                return BitConverter.ToUInt32(Lines[ID], 0x08);
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
                Lines[ID][0x08] = b[0];
                Lines[ID][0x09] = b[1];
                Lines[ID][0x0A] = b[2];
                Lines[ID][0x0B] = b[3];
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

        #endregion


        #region metodos para o GL

        private Vector3 GetPosition(ushort ID)
        {
            return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
        }

        #endregion


        #region index Manager

        //FirstIndexList

        public void SetStartIdexContent()
        {
            FirstIndexList.Clear();

            ushort[] Keys = Lines.Keys.ToArray();
            for (int i = 0; i < Keys.Length; i++)
            {
                ushort ID = Keys[i];
                byte index = ReturnFirstIndex(ID);

                if (FirstIndexList.ContainsKey(index))
                {
                    FirstIndexList[index].Add(ID);
                }
                else
                {
                    List<ushort> internalLines = new List<ushort>();
                    internalLines.Add(ID);
                    FirstIndexList.Add(index, internalLines);
                }

            }

        }

        private void UpdateFirstIndexList(ushort LineID, byte OldIndex, byte NewIndex)
        {
            if (FirstIndexList.ContainsKey(OldIndex) && FirstIndexList[OldIndex].Contains(LineID))
            {
                FirstIndexList[OldIndex].Remove(LineID);
                if (FirstIndexList[OldIndex].Count == 0)
                {
                    FirstIndexList.Remove(OldIndex);
                }
            }
            if (!FirstIndexList.ContainsKey(NewIndex))
            {
                var List = new List<ushort>();
                List.Add(LineID);
                FirstIndexList.Add(NewIndex, List);
            }
            else
            {
                FirstIndexList[NewIndex].Add(LineID);
            }
        }

        private void RemoveFirstIndexList(ushort LineID, byte OldIndex)
        {
            if (FirstIndexList.ContainsKey(OldIndex) && FirstIndexList[OldIndex].Contains(LineID))
            {
                FirstIndexList[OldIndex].Remove(LineID);
                if (FirstIndexList[OldIndex].Count == 0)
                {
                    FirstIndexList.Remove(OldIndex);
                }
            }

        }

        private void AddNewFirstIndexList(ushort LineID, byte NewIndex)
        {
            if (!FirstIndexList.ContainsKey(NewIndex))
            {
                var List = new List<ushort>();
                List.Add(LineID);
                FirstIndexList.Add(NewIndex, List);
            }
            else
            {
                FirstIndexList[NewIndex].Add(LineID);
            }

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
            return 0;
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
