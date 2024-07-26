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
    /// Classe que representa os arquivos .FSE (Floor Sound Effect);
    /// </summary>
    public class File_FSE_Group : BaseTriggerZoneGroup
    {
        /// <summary>
        /// <para>aqui contem o comeco do arquivo 16 bytes fixos;</para>
        /// <para>offset 0, 1 e 2 formato do arquivo, ascii: FSE;</para>
        /// <para>offset 3 fixo: 0x00;</para>
        /// <para>offset 4 e 5: FSE: 0x0301</para>
        /// <para>offset 6 e 7: ushort value usado para a quantidade de "linhas";</para>
        /// <para>offset 8 desconhecido, pode ser 0x00 ou 0x01;</para>
        /// </summary>
        public byte[] StartFile;
        /// <summary>
        /// <para>aqui contem o conteudo de todos os FSE do arquivo;</para>
        /// <para>id da linha, sequencia de 132 bytes para re4 ambas as versões;</para>
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
        /// lista de FirstIndex offset[0x02]
        /// <para>FirstIndex, line</para>
        /// </summary>
        private Dictionary<byte, List<ushort>> FirstIndexList;

        public File_FSE_Group() 
        {
            Lines = new Dictionary<ushort, byte[]>();
            StartFile = new byte[16];
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

            Methods = new NewAge_FSE_Methods();
            SetBaseMethods(Methods);
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            MethodsForGL = new NewAge_FSE_MethodsForGL();

            SetTriggerZoneMethods(Methods);
            SetTriggerZoneMethodsForGL(MethodsForGL);
        }

        /// <summary>
        /// Classe com os metodos que serão passados para classe NewAge_FSE_Property;
        /// </summary>
        public NewAge_FSE_Methods Methods { get; }

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
        public NewAge_FSE_MethodsForGL MethodsForGL { get; }

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
                    string r = "FSE Object Internal Line ID " + ID;
                    return r;
                }
            }
           
            return "FSE Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileFSE)
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

            byte[] content = new byte[132];

            content[0x00] = 0x03; //fixed 0x03
            content[0x02] = newFirstIndex;

            content[0x14] = 0x01; // fixed 0x01
            content[0x15] = 0x01; // triggerZone Category

            // trigger Zone height2, float 1000
            content[0x1E] = 0x7A;
            content[0x1F] = 0x44;

            // trigger Zone more scale, float 750
            content[0x21] = 0x80;
            content[0x22] = 0x3B;
            content[0x23] = 0x44;

            //C47A0000 = -1000
            //447A0000 = +1000

            //TZC0.X  +
            content[0x25] = 0x80;
            content[0x26] = 0x3B;
            content[0x27] = 0x44;
            //TZC0.Z  +
            content[0x29] = 0x80;
            content[0x2A] = 0x3B;
            content[0x2B] = 0x44;

            //TZC1.X  +
            content[0x2D] = 0x80;
            content[0x2E] = 0x3B;
            content[0x2F] = 0xC4;
            //TZC1.Z  -
            content[0x31] = 0x80;
            content[0x32] = 0x3B;
            content[0x33] = 0x44;

            //TZC2.X  -
            content[0x35] = 0x80;
            content[0x36] = 0x3B;
            content[0x37] = 0xC4;
            //TZC2.Z  -
            content[0x39] = 0x80;
            content[0x3A] = 0x3B;
            content[0x3B] = 0xC4;

            //TZC3.X  -
            content[0x3D] = 0x80;
            content[0x3E] = 0x3B;
            content[0x3F] = 0x44;
            //TZC3.Z  +
            content[0x41] = 0x80;
            content[0x42] = 0x3B;
            content[0x43] = 0xC4;

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
            return Lines[ID][0x02];
        }

        private void SetFirstIndex(ushort ID, byte value)
        {
            byte OldIndex = ReturnFirstIndex(ID);
            Lines[ID][0x02] = value;
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
            return 0x14;
        }
    }
}
