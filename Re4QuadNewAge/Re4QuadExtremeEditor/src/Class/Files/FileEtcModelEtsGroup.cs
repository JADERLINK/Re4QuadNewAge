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
    /// Classe que representa o arquivo .ETS (EtcModel);
    /// </summary>
    public class FileEtcModelEtsGroup
    {
        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }
        /// <summary>
        /// <para>aqui contem o começo do arquivo 16 bytes fixos;</para>
        /// <para>sendo que os primeiros 2 bytes define a quantidade de "linhas" no arquivo;</para>
        /// </summary>
        public byte[] StartFile;
        /// <summary>
        /// <para>aqui contem o conteudo de todos os ETS do arquivo;</para>
        /// <para>id da linha, sequencia de 64 bytes para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 40 bytes para re4 uhd;</para>
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
        /// ETS_ID, lineID; lista com os ids ets usado para para associação com os itens
        /// </summary>
        public Dictionary<ushort, List<ushort>> ETS_ID_List;


        public FileEtcModelEtsGroup(Re4Version version) 
        {
            GetRe4Version = version;
            StartFile = new byte[16];
            Lines = new Dictionary<ushort, byte[]>();
            EndFile = new byte[0];
            ETS_ID_List = new Dictionary<ushort, List<ushort>>();

            Methods = new EtcModelMethods();
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            Methods.ReturnByteFromPosition = ReturnByteFromPosition;
            Methods.SetByteFromPosition = SetByteFromPosition;

            Methods.ReturnRe4Version = ReturnRe4Version;

            Methods.ReturnEtcModelID = ReturnEtcModelID;
            Methods.SetEtcModelID = SetEtcModelID;

            Methods.ReturnETS_ID = ReturnETS_ID;
            Methods.SetETS_ID = SetETS_ID;

            Methods.ReturnUnknown_TTS_X_Hex = ReturnScaleX_Hex;
            Methods.ReturnUnknown_TTS_Y_Hex = ReturnScaleY_Hex;
            Methods.ReturnUnknown_TTS_Z_Hex = ReturnScaleZ_Hex;
            Methods.ReturnUnknown_TTS_W_Hex = ReturnScaleW_Hex;

            Methods.ReturnAngleX_Hex = ReturnAngleX_Hex;
            Methods.ReturnAngleY_Hex = ReturnAngleY_Hex;
            Methods.ReturnAngleZ_Hex = ReturnAngleZ_Hex;
            Methods.ReturnAngleW_Hex = ReturnAngleW_Hex;

            Methods.ReturnPositionX_Hex = ReturnPositionX_Hex;
            Methods.ReturnPositionY_Hex = ReturnPositionY_Hex;
            Methods.ReturnPositionZ_Hex = ReturnPositionZ_Hex;
            Methods.ReturnPositionW_Hex = ReturnPositionW_Hex;

            Methods.SetUnknown_TTS_X_Hex = SetScaleX_Hex;
            Methods.SetUnknown_TTS_Y_Hex = SetScaleY_Hex;
            Methods.SetUnknown_TTS_Z_Hex = SetScaleZ_Hex;
            Methods.SetUnknown_TTS_W_Hex = SetScaleW_Hex;

            Methods.SetAngleX_Hex = SetAngleX_Hex;
            Methods.SetAngleY_Hex = SetAngleY_Hex;
            Methods.SetAngleZ_Hex = SetAngleZ_Hex;
            Methods.SetAngleW_Hex = SetAngleW_Hex;

            Methods.SetPositionX_Hex = SetPositionX_Hex;
            Methods.SetPositionY_Hex = SetPositionY_Hex;
            Methods.SetPositionZ_Hex = SetPositionZ_Hex;
            Methods.SetPositionW_Hex = SetPositionW_Hex;

            // floats
            Methods.ReturnUnknown_TTS_X = ReturnScaleX;
            Methods.ReturnUnknown_TTS_Y = ReturnScaleY;
            Methods.ReturnUnknown_TTS_Z = ReturnScaleZ;
            Methods.ReturnUnknown_TTS_W = ReturnScaleW;

            Methods.ReturnAngleX = ReturnAngleX;
            Methods.ReturnAngleY = ReturnAngleY;
            Methods.ReturnAngleZ = ReturnAngleZ;
            Methods.ReturnAngleW = ReturnAngleW;

            Methods.ReturnPositionX = ReturnPositionX;
            Methods.ReturnPositionY = ReturnPositionY;
            Methods.ReturnPositionZ = ReturnPositionZ;
            Methods.ReturnPositionW = ReturnPositionW;

            Methods.SetUnknown_TTS_X = SetScaleX;
            Methods.SetUnknown_TTS_Y = SetScaleY;
            Methods.SetUnknown_TTS_Z = SetScaleZ;
            Methods.SetUnknown_TTS_W = SetScaleW;

            Methods.SetAngleX = SetAngleX;
            Methods.SetAngleY = SetAngleY;
            Methods.SetAngleZ = SetAngleZ;
            Methods.SetAngleW = SetAngleW;

            Methods.SetPositionX = SetPositionX;
            Methods.SetPositionY = SetPositionY;
            Methods.SetPositionZ = SetPositionZ;
            Methods.SetPositionW = SetPositionW;

            //byte arry
            Methods.ReturnUnknown_TTJ = ReturnUnknown_TTJ;
            Methods.SetUnknown_TTJ = SetUnknown_TTJ;

            Methods.ReturnUnknown_TTH = ReturnUnknown_TTH;
            Methods.SetUnknown_TTH = SetUnknown_TTH;

            Methods.ReturnUnknown_TTG = ReturnUnknown_TTG;
            Methods.SetUnknown_TTG = SetUnknown_TTG;


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
            MoveMethods.GetObjScale_ToMove = GetObjScale_ToMove;
            MoveMethods.SetObjScale_ToMove = SetObjScale_ToMove;
            MoveMethods.GetTriggerZoneCategory = Utils.GetTriggerZoneCategory_Null;

            MethodsForGL = new EtcModelMethodsForGL();
            MethodsForGL.GetEtcModelID = ReturnEtcModelID;
            MethodsForGL.GetScale = GetScale;
            MethodsForGL.GetAngle = GetAngle;
            MethodsForGL.GetPosition = GetPosition;

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;
        }

        /// <summary>
        /// Classe com os metodos que serão passados para classe EtcModelProperty;
        /// </summary>
        public EtcModelMethods Methods { get; }

        /// <summary>
        /// classe com os metodos responsaveis pelo oque sera exibido no node;
        /// </summary>
        public NodeDisplayMethods DisplayMethods { get; }

        /// <summary>
        /// Classe com os metodos para o GL
        /// </summary>
        public EtcModelMethodsForGL MethodsForGL { get; }

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
            if (Lines.ContainsKey(ID) && Globals.TreeNodeRenderHexValues)
            {
                return BitConverter.ToString(Lines[ID]).Replace("-", "_");
            }
            else if (Lines.ContainsKey(ID))
            {
                ushort ETS_ID = ReturnETS_ID(ID);
                string r = "[" + ETS_ID.ToString("X4") + "] 0x";

                ushort v = Methods.ReturnEtcModelID(ID);
                string sv = v.ToString("X4");
                r += sv + ": ";

                if (DataBase.EtcModelIDs.List.ContainsKey(v) && v != 0xFFFF)
                {
                    r += DataBase.EtcModelIDs.List[v].Name;
                }
                else
                {
                    r += Lang.GetAttributeText(aLang.ListBoxUnknownEtcModel);
                }

                string associated = DataBase.Extras.AssociatedSpecialEventObjName(RefInteractionType.EtcModel, ETS_ID);
                if (associated != null)
                {
                    r += " - Associated = " + associated;
                }

                return r;
            }
            else 
            {
                return "ETS Error Internal Line ID " + ID;
            }
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderEtcmodelETS)
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

            ushort newETS_ID = 0;
            if (ETS_ID_List.Count > 0)
            {
                newETS_ID = ETS_ID_List.Max(o => o.Key);
            }
            if (newETS_ID != ushort.MaxValue)
            {
                newETS_ID++;
            }
            byte[] newETS_ID_Bytes = BitConverter.GetBytes(newETS_ID);

            byte[] content = null;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                content = new byte[64];
                content[0x30] = 0x01; // crate

                content[0x32] = newETS_ID_Bytes[0];
                content[0x33] = newETS_ID_Bytes[1];

                content[0x02] = 0x80; //scale 1.0f
                content[0x03] = 0x3F;

                content[0x06] = 0x80; //scale 1.0f
                content[0x07] = 0x3F;

                content[0x0A] = 0x80; //scale 1.0f
                content[0x0B] = 0x3F;
            }
            else if (GetRe4Version == Re4Version.UHD)
            {
                content = new byte[40];
                content[0x00] = 0x01; // crate

                content[0x02] = newETS_ID_Bytes[0];
                content[0x03] = newETS_ID_Bytes[1];

                content[0x06] = 0x80; //scale 1.0f
                content[0x07] = 0x3F;

                content[0x0A] = 0x80; //scale 1.0f
                content[0x0B] = 0x3F;

                content[0x0E] = 0x80; //scale 1.0f
                content[0x0F] = 0x3F;
            }

            AddNewETS_ID_List(newID, newETS_ID);
            Lines.Add(newID, content);
            return newID;
        }

        private void RemoveLineID(ushort ID) 
        {
           RemoveETS_ID_List(ID, ReturnETS_ID(ID));
           Lines.Remove(ID);
        }


        #endregion


        #region metodos para ToMove

        private Vector3 GetObjPostion_ToCamera(ushort ID)
        {
            Vector3 position = GetPosition(ID);
            if (float.IsNaN(position.X) || float.IsInfinity(position.X)) { position.X = 0; }
            if (float.IsNaN(position.Y) || float.IsInfinity(position.Y)) { position.Y = 0; }
            if (float.IsNaN(position.Z) || float.IsInfinity(position.Z)) { position.Z = 0; }
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
            v[0] = new Vector3(ReturnAngleX(ID), ReturnAngleY(ID), ReturnAngleZ(ID));
            Utils.ToMoveCheckLimits(ref v);
            return v;
        }

        private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value)
        {
            if (value != null && value.Length >= 1)
            {
                SetAngleX(ID, value[0].X);
                SetAngleY(ID, value[0].Y);
                SetAngleZ(ID, value[0].Z);
            }
        }

        private Vector3[] GetObjScale_ToMove(ushort ID) 
        {
            Vector3[] v = new Vector3[1];
            v[0] = new Vector3(ReturnScaleX(ID), ReturnScaleY(ID), ReturnScaleZ(ID));
            Utils.ToMoveCheckLimits(ref v);
            return v;
        }

        private void SetObjScale_ToMove(ushort ID, Vector3[] value) 
        {
            if (value != null && value.Length >= 1)
            {
                SetScaleX(ID, value[0].X);
                SetScaleY(ID, value[0].Y);
                SetScaleZ(ID, value[0].Z);
            }
        }


        #endregion


        #region metodos das propriedades

        private Re4Version ReturnRe4Version() 
        {
            return GetRe4Version;
        }


        private byte ReturnByteFromPosition(ushort ID, int FromPostion)
        {
            return Lines[ID][FromPostion];
        }

        private void SetByteFromPosition(ushort ID, int FromPostion, byte value)
        {
            Lines[ID][FromPostion] = value;
        }

        private byte[] ReturnLine(ushort ID)
        {
            return (byte[])Lines[ID].Clone();
        }

        private void SetLine(ushort ID, byte[] value)
        {
            ushort OldEtsID = ReturnETS_ID(ID);

            value.CopyTo(Lines[ID], 0);

            UpdateETS_ID_List(ID, OldEtsID, ReturnETS_ID(ID));
        }


        private ushort ReturnEtcModelID(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                byte[] b = new byte[2];
                b[0] = Lines[ID][0x00];
                b[1] = Lines[ID][0x01];
                return BitConverter.ToUInt16(b, 0);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[2];
                b[0] = Lines[ID][0x30];
                b[1] = Lines[ID][0x31];
                return BitConverter.ToUInt16(b, 0);
            }
            else { return 0; }
        }

        private void SetEtcModelID(ushort ID, ushort value) 
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x00] = b[0];
                Lines[ID][0x01] = b[1];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x30] = b[0];
                Lines[ID][0x31] = b[1];
            }
        }

        private ushort ReturnETS_ID(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                byte[] b = new byte[2];
                b[0] = Lines[ID][0x02];
                b[1] = Lines[ID][0x03];
                return BitConverter.ToUInt16(b, 0);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[2];
                b[0] = Lines[ID][0x32];
                b[1] = Lines[ID][0x33];
                return BitConverter.ToUInt16(b, 0);
            }
            else { return 0; }
        }

        private void SetETS_ID(ushort ID, ushort value)
        {
            ushort OldEtsID = ReturnETS_ID(ID);
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x02] = b[0];
                Lines[ID][0x03] = b[1];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x32] = b[0];
                Lines[ID][0x33] = b[1];
            }
            UpdateETS_ID_List(ID, OldEtsID, value);
        }


        private uint ReturnScaleX_Hex(ushort ID) 
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

        private void SetScaleX_Hex(ushort ID, uint value) 
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

        private uint ReturnScaleY_Hex(ushort ID)
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

        private void SetScaleY_Hex(ushort ID, uint value)
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

        private uint ReturnScaleZ_Hex(ushort ID)
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

        private void SetScaleZ_Hex(ushort ID, uint value)
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

        private uint ReturnScaleW_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x0C);
            }
            else { return 0; }
        }

        private void SetScaleW_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x0C] = b[0];
                Lines[ID][0x0D] = b[1];
                Lines[ID][0x0E] = b[2];
                Lines[ID][0x0F] = b[3];
            }
        }

        private uint ReturnAngleX_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x10);
            }
            else { return 0; }
        }

        private void SetAngleX_Hex(ushort ID, uint value)
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

        private uint ReturnAngleY_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x14);
            }
            else { return 0; }
        }

        private void SetAngleY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x14] = b[0];
                Lines[ID][0x15] = b[1];
                Lines[ID][0x16] = b[2];
                Lines[ID][0x17] = b[3];
            }
        }

        private uint ReturnAngleZ_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x18);
            }
            else { return 0; }
        }

        private void SetAngleZ_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD || GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x18] = b[0];
                Lines[ID][0x19] = b[1];
                Lines[ID][0x1A] = b[2];
                Lines[ID][0x1B] = b[3];
            }
        }

        private uint ReturnAngleW_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x1C);
            }
            else { return 0; }
        }

        private void SetAngleW_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x1C] = b[0];
                Lines[ID][0x1D] = b[1];
                Lines[ID][0x1E] = b[2];
                Lines[ID][0x1F] = b[3];
            }
        }

        private uint ReturnPositionX_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x1C);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x20);
            }
            else { return 0; }
        }

        private void SetPositionX_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x1C] = b[0];
                Lines[ID][0x1D] = b[1];
                Lines[ID][0x1E] = b[2];
                Lines[ID][0x1F] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x20] = b[0];
                Lines[ID][0x21] = b[1];
                Lines[ID][0x22] = b[2];
                Lines[ID][0x23] = b[3];
            }
        }

        private uint ReturnPositionY_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x20);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x24);
            }
            else { return 0; }
        }

        private void SetPositionY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x20] = b[0];
                Lines[ID][0x21] = b[1];
                Lines[ID][0x22] = b[2];
                Lines[ID][0x23] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x24] = b[0];
                Lines[ID][0x25] = b[1];
                Lines[ID][0x26] = b[2];
                Lines[ID][0x27] = b[3];
            }
        }

        private uint ReturnPositionZ_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.UHD)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x24);
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x28);
            }
            else { return 0; }
        }

        private void SetPositionZ_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.UHD)
            {
                Lines[ID][0x24] = b[0];
                Lines[ID][0x25] = b[1];
                Lines[ID][0x26] = b[2];
                Lines[ID][0x27] = b[3];
            }
            else if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x28] = b[0];
                Lines[ID][0x29] = b[1];
                Lines[ID][0x2A] = b[2];
                Lines[ID][0x2B] = b[3];
            }
        }

        private uint ReturnPositionW_Hex(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                return BitConverter.ToUInt32(Lines[ID], 0x2C);
            }
            else { return 0; }
        }

        private void SetPositionW_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x2C] = b[0];
                Lines[ID][0x2D] = b[1];
                Lines[ID][0x2E] = b[2];
                Lines[ID][0x2F] = b[3];
            }
        }

        // floats

        private float ReturnScaleX(ushort ID) 
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleX_Hex(ID)),0);
        }

        private float ReturnScaleY(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleY_Hex(ID)), 0);
        }

        private float ReturnScaleZ(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleZ_Hex(ID)), 0);
        }

        private float ReturnScaleW(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleW_Hex(ID)), 0);
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

        private float ReturnAngleW(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnAngleW_Hex(ID)), 0);
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

        private float ReturnPositionW(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnPositionW_Hex(ID)), 0);
        }

        // sets

        private void SetScaleX(ushort ID, float value) 
        {
            SetScaleX_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }

        private void SetScaleY(ushort ID, float value)
        {
            SetScaleY_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }

        private void SetScaleZ(ushort ID, float value)
        {
            SetScaleZ_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }

        private void SetScaleW(ushort ID, float value)
        {
            SetScaleW_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
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

        private void SetAngleW(ushort ID, float value)
        {
            SetAngleW_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
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

        private void SetPositionW(ushort ID, float value)
        {
            SetPositionW_Hex(ID, BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
        }


        // end bytes arry, only classic

        private byte[] ReturnUnknown_TTJ(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x34];
                b[1] = Lines[ID][0x35];
                b[2] = Lines[ID][0x36];
                b[3] = Lines[ID][0x37];
                return b;
            }
            else { return new byte[4]; }
        }

        private void SetUnknown_TTJ(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x34] = value[0];
                Lines[ID][0x35] = value[1];
                Lines[ID][0x36] = value[2];
                Lines[ID][0x37] = value[3];
            }
        }

        private byte[] ReturnUnknown_TTH(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x38];
                b[1] = Lines[ID][0x39];
                b[2] = Lines[ID][0x3A];
                b[3] = Lines[ID][0x3B];
                return b;
            }
            else { return new byte[4]; }
        }

        private void SetUnknown_TTH(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x38] = value[0];
                Lines[ID][0x39] = value[1];
                Lines[ID][0x3A] = value[2];
                Lines[ID][0x3B] = value[3];
            }
        }

        private byte[] ReturnUnknown_TTG(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x3C];
                b[1] = Lines[ID][0x3D];
                b[2] = Lines[ID][0x3E];
                b[3] = Lines[ID][0x3F];
                return b;
            }
            else { return new byte[4]; }
        }

        private void SetUnknown_TTG(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x3C] = value[0];
                Lines[ID][0x3D] = value[1];
                Lines[ID][0x3E] = value[2];
                Lines[ID][0x3F] = value[3];
            }
        }


        #endregion


        #region metodos para o GL

        private Vector3 GetScale(ushort ID)
        {
            if (!Globals.RenderEtcmodelUsingScale)
            {
                return Vector3.One;
            }
            return new Vector3(ReturnScaleX(ID), ReturnScaleY(ID), ReturnScaleZ(ID)); // O JOGO NÃO RECONHECEU A ESCALA
        }


        private Vector3 GetPosition(ushort ID)
        {
            return new Vector3(ReturnPositionX(ID) / 100f, ReturnPositionY(ID) / 100f, ReturnPositionZ(ID) / 100f);
        }

        private Matrix4 GetAngle(ushort ID)
        {
            //ordem correta: XYZ
            return Matrix4.CreateRotationX(ReturnAngleX(ID)) * Matrix4.CreateRotationY(ReturnAngleY(ID)) * Matrix4.CreateRotationZ(ReturnAngleZ(ID)); // OK
        }


        #endregion

        // outros metodos
        #region ETS_ID_List 
        public void CreateNewETS_ID_List()
        {
            ETS_ID_List.Clear();
            foreach (var lineID in Lines.Keys)
            {
                if (!ETS_ID_List.ContainsKey(ReturnETS_ID(lineID)))
                {
                    var List = new List<ushort>();
                    List.Add(lineID);
                    ETS_ID_List.Add(ReturnETS_ID(lineID), List);
                }
                else 
                {
                    ETS_ID_List[ReturnETS_ID(lineID)].Add(lineID);
                }
            }
        }

        private void UpdateETS_ID_List(ushort LineID, ushort OldETS_ID, ushort NewETS_ID)
        {
            if (ETS_ID_List.ContainsKey(OldETS_ID) && ETS_ID_List[OldETS_ID].Contains(LineID))
            {
                ETS_ID_List[OldETS_ID].Remove(LineID);
                if (ETS_ID_List[OldETS_ID].Count == 0)
                {
                    ETS_ID_List.Remove(OldETS_ID);
                }          
            }
            if (!ETS_ID_List.ContainsKey(NewETS_ID))
            {
                var List = new List<ushort>();
                List.Add(LineID);
                ETS_ID_List.Add(NewETS_ID, List);
            }
            else
            {
                ETS_ID_List[NewETS_ID].Add(LineID);
            }

        }

        private void RemoveETS_ID_List(ushort LineID, ushort OldETS_ID)
        {
            if (ETS_ID_List.ContainsKey(OldETS_ID) && ETS_ID_List[OldETS_ID].Contains(LineID))
            {
                ETS_ID_List[OldETS_ID].Remove(LineID);
                if (ETS_ID_List[OldETS_ID].Count == 0)
                {
                    ETS_ID_List.Remove(OldETS_ID);
                }
            }

        }

        private void AddNewETS_ID_List(ushort LineID, ushort NewETS_ID)
        {
            if (!ETS_ID_List.ContainsKey(NewETS_ID))
            {
                var List = new List<ushort>();
                List.Add(LineID);
                ETS_ID_List.Add(NewETS_ID, List);
            }
            else
            {
                ETS_ID_List[NewETS_ID].Add(LineID);
            }

        }

        #endregion
    }
}
