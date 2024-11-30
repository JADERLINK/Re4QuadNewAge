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
    /// Classe que representa os arquivos .EMI (Interaction Point);
    /// </summary>
    public class File_EMI_Group : BaseGroup
    {
        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }

        /// <summary>
        /// <para>aqui contem o começo do arquivo, 16 bytes para 2007PS2 e 8 bytes para UHD;</para>
        /// <para>offset 0x00 uint value usado para a quantidade de "linhas"</para>
        /// </summary>
        public byte[] StartFile;
        /// <summary>
        /// <para>aqui contem o conteudo de todos as entrys do arquivo;</para>
        /// <para>id da linha, sequencia de 64 bytes (.EMI) para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 64 bytes (.EMI) para re4 uhd;</para>
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

        public File_EMI_Group(Re4Version version) 
        {
            GetRe4Version = version;
            StartFile = new byte[16]; // nota 2007Ps2 é 16 bytes e UHD é 8 bytes
            Lines = new Dictionary<ushort, byte[]>();
            EndFile = new byte[0];


            DisplayMethods = new NodeDisplayMethods();
            DisplayMethods.GetNodeText = GetNodeText;
            DisplayMethods.GetNodeColor = GetNodeColor;

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

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;

            Methods = new NewAge_EMI_Methods();
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
            Methods.ReturnAngleY = ReturnAngleY;
            Methods.ReturnAngleY_Hex = ReturnAngleY_Hex;
            Methods.SetAngleY = SetAngleY;
            Methods.SetAngleY_Hex = SetAngleY_Hex;

            MethodsForGL = new NewAge_EMI_MethodsForGL();
            MethodsForGL.GetPosition = GetPosition;
            MethodsForGL.GetAngle = GetAngle;
        }

        /// <summary>
        /// Classe com os metodos que serão passados para classe NewAge_EMI_Property;
        /// </summary>
        public NewAge_EMI_Methods Methods { get; }

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
        public NewAge_EMI_MethodsForGL MethodsForGL { get; }

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
                    string r = "EMI Object Internal Line ID " + ID;
                    return r;
                }
            }
           
            return "EMI Error Internal Line ID " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileEMI)
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

            byte[] content = new byte[64];

            if (GetRe4Version == Re4Version.V2007PS2)
            {
                content[0x0E] = 0x80;
                content[0x0F] = 0x3F;
            }

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

        #region positionXYZ, angleY

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


        private uint ReturnAngleY_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x10);
            }
            else { return 0; }
        }

        private void SetAngleY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x10] = b[0];
                Lines[ID][0x11] = b[1];
                Lines[ID][0x12] = b[2];
                Lines[ID][0x13] = b[3];
            }
        }

        private float ReturnAngleY(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleY_Hex(ID)), 0);
        }

        private void SetAngleY(ushort ID, float value)
        {
            SetAngleY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }


        #endregion


        #region metodos para o GL

        private Vector3 GetPosition(ushort ID)
        {
            return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
        }

        private Matrix4 GetAngle(ushort ID)
        {
            return Matrix4.CreateRotationY(ReturnAngleY(ID));
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
            float AngleY = ReturnAngleY(ID);
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
            v[0] = new Vector3(0, ReturnAngleY(ID), 0);
            Utils.ToMoveCheckLimits(ref v);
            return v;
        }

        private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value)
        {
            if (value != null && value.Length >= 1)
            {
                SetAngleY(ID, value[0].Y);
            }
        }


        #endregion
    }
}
