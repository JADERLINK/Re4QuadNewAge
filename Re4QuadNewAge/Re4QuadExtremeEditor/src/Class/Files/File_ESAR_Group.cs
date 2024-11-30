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
    /// Classe que representa os arquivos .SAR (Controller Lighting Group) e .EAR (Controller Effect Group);
    /// </summary>
    public class File_ESAR_Group : BaseTriggerZoneGroup
    {
        /// <summary>
        /// especifica se é EAR ou SAR;
        /// </summary>
        public EsarFileFormat GetEsarFileFormat { get; }
        /// <summary>
        /// <para>aqui contem o comeco do arquivo 16 bytes fixos;</para>
        /// <para>offset 0x00 uint quantidade de entry;</para>
        /// </summary>
        public byte[] StartFile;
        /// <summary>
        /// <para>aqui contem o conteudo de todos os SAR/EAR do arquivo;</para>
        /// <para>id da linha, sequencia de 216 bytes (.SAR) para re4 ambas as versões;</para>
        /// <para>id da linha, sequencia de 152 bytes (.EAR) para re4 ambas as versões;</para>
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
        /// lista de FirstIndex offset[0x00]
        /// <para>FirstIndex, line</para>
        /// </summary>
        private Dictionary<byte, List<ushort>> FirstIndexList;


        public File_ESAR_Group(EsarFileFormat fileFormat) 
        {
            GetEsarFileFormat = fileFormat;
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
            MoveMethods.GetTriggerZoneCategory = GetSpecialZoneCategory;

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;

            Methods = new NewAge_ESAR_Methods();
            SetBaseMethods(Methods);
            Methods.GetEsarFileFormat = GetEsarFileFormatMethod;
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            MethodsForGL = new NewAge_ESAR_MethodsForGL();

            SetTriggerZoneMethods(Methods);
            SetTriggerZoneMethodsForGL(MethodsForGL);
        }

        /// <summary>
        /// Classe com os metodos que serão passados para classe NewAge_ESAR_Property;
        /// </summary>
        public NewAge_ESAR_Methods Methods { get; }

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
        public NewAge_ESAR_MethodsForGL MethodsForGL { get; }

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
                    if (GetEsarFileFormat == EsarFileFormat.SAR)
                    {
                        return "SAR Object Internal Line ID " + ID;
                    }
                    else if (GetEsarFileFormat == EsarFileFormat.EAR)
                    {
                        return "EAR Object Internal Line ID " + ID;
                    }

                    return "ESAR Object Internal Line ID " + ID;
                }

            }

            return "ESAR Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileEAR && GetEsarFileFormat == EsarFileFormat.EAR)
            {
                return Globals.NodeColorHided;
            }
            else if (!Globals.RenderFileSAR && GetEsarFileFormat == EsarFileFormat.SAR)
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
            if (GetEsarFileFormat == EsarFileFormat.SAR)
            {
                content = new byte[216];
            }
            else if (GetEsarFileFormat == EsarFileFormat.EAR) 
            {
                content = new byte[152];
            }

            if (content != null)
            {
                content[0x00] = newFirstIndex;
                content[0x01] = 0x01; // fixed 0x01

                content[0x04] = 0x01; // fixed 0x01
                content[0x05] = 0x01; // triggerZone Category

                // trigger Zone height2, float 1000
                content[0x0E] = 0x7A;
                content[0x0F] = 0x44;

                // trigger Zone more scale, float 750
                content[0x11] = 0x80;
                content[0x12] = 0x3B;
                content[0x13] = 0x44;

                //C47A0000 = -1000
                //447A0000 = +1000

                //TZC0.X  +
                content[0x15] = 0x80;
                content[0x16] = 0x3B;
                content[0x17] = 0x44;
                //TZC0.Z  +
                content[0x19] = 0x80;
                content[0x1A] = 0x3B;
                content[0x1B] = 0x44;

                //TZC1.X  +
                content[0x1D] = 0x80;
                content[0x1E] = 0x3B;
                content[0x1F] = 0xC4;
                //TZC1.Z  -
                content[0x21] = 0x80;
                content[0x22] = 0x3B;
                content[0x23] = 0x44;

                //TZC2.X  -
                content[0x25] = 0x80;
                content[0x26] = 0x3B;
                content[0x27] = 0xC4;
                //TZC2.Z  -
                content[0x29] = 0x80;
                content[0x2A] = 0x3B;
                content[0x2B] = 0xC4;

                //TZC3.X  -
                content[0x2D] = 0x80;
                content[0x2E] = 0x3B;
                content[0x2F] = 0x44;
                //TZC3.Z  +
                content[0x31] = 0x80;
                content[0x32] = 0x3B;
                content[0x33] = 0xC4;

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

        protected override Endianness GetEndianness()
        {
            return Endianness.LittleEndian;
        }

        private EsarFileFormat GetEsarFileFormatMethod()
        {
            return GetEsarFileFormat;
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
            return Lines[ID][0x00];
        }

        private void SetFirstIndex(ushort ID, byte value)
        {
            byte OldIndex = ReturnFirstIndex(ID);
            Lines[ID][0x00] = value;
            UpdateFirstIndexList(ID, OldIndex, value);
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
            return GetTriggerZonePos_ToCamera(ID);
        }

        private float GetObjAngleY_ToCamera(ushort ID)
        {
            return 0;
        }

        private Vector3[] GetObjPostion_ToMove_General(ushort ID)
        {
            Vector3[] pos = new Vector3[7];
            pos[0] = Vector3.Zero;
            GetTriggerZonePos_ToMove_General(ID, ref pos);
            Utils.ToMoveCheckLimits(ref pos);
            return pos;
        }


        private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value)
        {
            if (value != null && value.Length >= 6)
            {
                SetTriggerZonePos_ToMove_General(ID, value);
            }

        }

        #endregion

        protected override byte[] GetInternalLine(ushort ID)
        {
            return Lines[ID];
        }

        protected override int GetTriggerZoneStartIndex()
        {
            return 0x04;
        }
    }
}
