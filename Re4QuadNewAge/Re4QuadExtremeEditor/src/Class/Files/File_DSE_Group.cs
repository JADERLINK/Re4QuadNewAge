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
    /// Classe que representa o arquivo .DSE (Door Sound Environment);
    /// </summary>
    public class File_DSE_Group : BaseGroup
    {
        /// <summary>
        /// <para>aqui contem o conteudo de todos os DSE do arquivo;</para>
        /// <para>id da linha, sequencia de 12 bytes para re4 ambas as versões;</para>
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

        public File_DSE_Group() 
        {
            Lines = new Dictionary<ushort, byte[]>();
            EndFile = new byte[0];

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

            Methods = new NewAge_DSE_Methods();
            SetBaseMethods(Methods);
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;
        }

        /// <summary>
        /// Classe com os metodos que serão passados para classe NewAge_DSE_Property;
        /// </summary>
        public NewAge_DSE_Methods Methods { get; }

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
                    string r = "Room Door r" + ReturnRoomDoor(ID).ToString("X3") + " Sound ID 0x" + ReturnSoundID(ID).ToString("X4");
                    return r;
                }
            }
            return "DSE Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
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

            byte[] content = new byte[12];
            content[4] = 0xFF;
            content[5] = 0xFF;
            content[6] = 0xFF;
            content[7] = 0xFF;
            content[8] = 0xFF;
            content[9] = 0xFF;
            content[10] = 0xFF;
            content[11] = 0xFF;
            Lines.Add(newID, content);
            return newID;
        }

        private void RemoveLineID(ushort ID)
        {
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

        private byte[] ReturnLine(ushort ID)
        {
            return (byte[])Lines[ID].Clone();
        }

        private void SetLine(ushort ID, byte[] value)
        {
            value.CopyTo(Lines[ID], 0);
        }

        private ushort ReturnRoomDoor(ushort ID) 
        {
            return BitConverter.ToUInt16(Lines[ID], 0x00);
        }

        private short ReturnSoundID(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x02);
        }

        #endregion
    }
}
