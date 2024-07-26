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
    /// arquivo customizado do quad
    /// </summary>
    public class FileQuadCustomGroup : BaseTriggerZoneGroup
    {
        /// <summary>
        /// <para>para simplificar cada entry do meu arquivo é um byte array</para>
        /// <para>Segue a baixo a difinição de cada campo</para>
        /// <para>offset[0x00] a offset[0x2F] byte[48] triggerZone</para>
        /// <para>offset[0x30] a offset[0x3B] byte[12] position X Y Z</para>
        /// <para>offset[0x3C] a offset[0x47] byte[12] angle X Y Z</para>
        /// <para>offset[0x48] a offset[0x53] byte[12] scale X Y Z</para>
        /// <para>offset[0x54] a offset[0x57] uint valor Id do modelo a ser renderizado</para>
        /// <para>offset[0x58] a offset[0x5A] byte[3] color RGB</para>
        /// <para>offset[0x5B] byte[1] status, sendo:</para>
        /// <para>00: point desabilitado não renderiza</para>
        /// <para>01: abilitado renderiza modelo padrão (Arrow)</para>
        /// <para>02: abilitado renderiza modelo personalizado</para>
        /// </summary>
        public Dictionary<ushort, byte[]> Lines { get; private set; }
        /// <summary>
        /// Para cada linha é posivel escrever um texto
        /// </summary>
        public Dictionary<ushort, string> Texts { get; private set; }
        /// <summary>
        /// Id para ser usado para adicionar novas linhas;
        /// </summary>
        public ushort IdForNewLine = 0;

        public FileQuadCustomGroup()
        {
            Lines = new Dictionary<ushort, byte[]>();
            Texts = new Dictionary<ushort, string>();
            IdForNewLine = 0;

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
            MoveMethods.GetTriggerZoneCategory = GetSpecialZoneCategory;

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;

            Methods = new QuadCustomMethods();
            SetBaseMethods(Methods);
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            //hex
            Methods.ReturnScaleX_Hex = ReturnScaleX_Hex;
            Methods.ReturnScaleY_Hex = ReturnScaleY_Hex;
            Methods.ReturnScaleZ_Hex = ReturnScaleZ_Hex;

            Methods.ReturnAngleX_Hex = ReturnAngleX_Hex;
            Methods.ReturnAngleY_Hex = ReturnAngleY_Hex;
            Methods.ReturnAngleZ_Hex = ReturnAngleZ_Hex;

            Methods.ReturnPositionX_Hex = ReturnPositionX_Hex;
            Methods.ReturnPositionY_Hex = ReturnPositionY_Hex;
            Methods.ReturnPositionZ_Hex = ReturnPositionZ_Hex;

            Methods.SetScaleX_Hex = SetScaleX_Hex;
            Methods.SetScaleY_Hex = SetScaleY_Hex;
            Methods.SetScaleZ_Hex = SetScaleZ_Hex;

            Methods.SetAngleX_Hex = SetAngleX_Hex;
            Methods.SetAngleY_Hex = SetAngleY_Hex;
            Methods.SetAngleZ_Hex = SetAngleZ_Hex;

            Methods.SetPositionX_Hex = SetPositionX_Hex;
            Methods.SetPositionY_Hex = SetPositionY_Hex;
            Methods.SetPositionZ_Hex = SetPositionZ_Hex;

            // floats
            Methods.ReturnScaleX = ReturnScaleX;
            Methods.ReturnScaleY = ReturnScaleY;
            Methods.ReturnScaleZ = ReturnScaleZ;

            Methods.ReturnAngleX = ReturnAngleX;
            Methods.ReturnAngleY = ReturnAngleY;
            Methods.ReturnAngleZ = ReturnAngleZ;

            Methods.ReturnPositionX = ReturnPositionX;
            Methods.ReturnPositionY = ReturnPositionY;
            Methods.ReturnPositionZ = ReturnPositionZ;

            Methods.SetScaleX = SetScaleX;
            Methods.SetScaleY = SetScaleY;
            Methods.SetScaleZ = SetScaleZ;

            Methods.SetAngleX = SetAngleX;
            Methods.SetAngleY = SetAngleY;
            Methods.SetAngleZ = SetAngleZ;

            Methods.SetPositionX = SetPositionX;
            Methods.SetPositionY = SetPositionY;
            Methods.SetPositionZ = SetPositionZ;

            //outros
            Methods.ReturnPointStatus = ReturnPointStatus;
            Methods.SetPointStatus = SetPointStatus;
            Methods.GetQuadCustomPointStatus = GetQuadCustomPointStatus;
            Methods.SetPointModelID = SetPointModelID;
            Methods.ReturnPointModelID = ReturnPointModelID;
            Methods.ReturnColorRGB = ReturnColorRGB;
            Methods.SetColorRGB = SetColorRGB;
            Methods.ReturnObjectName = ReturnObjectName;
            Methods.SetObjectName = SetObjectName;

            MethodsForGL = new QuadCustomMethodsForGL();
            MethodsForGL.GetQuadCustomPointStatus = GetQuadCustomPointStatus;
            MethodsForGL.GetPointModelID = ReturnPointModelID;
            MethodsForGL.GetScale = GetScale;
            MethodsForGL.GetAngle = GetAngle;
            MethodsForGL.GetPosition = GetPosition;
            MethodsForGL.GetCustomColor = GetCustomColor;

            SetTriggerZoneMethods(Methods);
            SetTriggerZoneMethodsForGL(MethodsForGL);
        }

        /// <summary>
        /// Classe com os metodos que serão passados para classe QuadCustomProperty;
        /// </summary>
        public QuadCustomMethods Methods { get; }

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
        public QuadCustomMethodsForGL MethodsForGL { get; }

        /// <summary>
        /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
        /// </summary>
        public NodeChangeAmountMethods ChangeAmountMethods { get; }

        //metodos:
        #region metodos para os Nodes

        // texto do treeNode
        public string GetNodeText(ushort ID)
        {
            if (Texts.ContainsKey(ID))
            {
                return "Quad Custom: " + Texts[ID];
            }
            return "Error Getting Name " + ID;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderFileQuadCustom)
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


            byte[] content = new byte[Consts.QuadCustomLineArrayLength];

            content[0x00] = 0x01; // fixed 0x01
            content[0x01] = 0x01; // triggerZone Category

            // trigger Zone height2, float 1000
            content[0x0A] = 0x7A;
            content[0x0B] = 0x44;

            // trigger Zone more scale, float 750
            content[0x0D] = 0x80;
            content[0x0E] = 0x3B;
            content[0x0F] = 0x44;

            //C47A0000 = -1000
            //447A0000 = +1000

            //TZC0.X  +
            content[0x11] = 0x80;
            content[0x12] = 0x3B;
            content[0x13] = 0x44;
            //TZC0.Z  +
            content[0x15] = 0x80;
            content[0x16] = 0x3B;
            content[0x17] = 0x44;

            //TZC1.X  +
            content[0x19] = 0x80;
            content[0x1A] = 0x3B;
            content[0x1B] = 0xC4;
            //TZC1.Z  -
            content[0x1D] = 0x80;
            content[0x1E] = 0x3B;
            content[0x1F] = 0x44;

            //TZC2.X  -
            content[0x21] = 0x80;
            content[0x22] = 0x3B;
            content[0x23] = 0xC4;
            //TZC2.Z  -
            content[0x25] = 0x80;
            content[0x26] = 0x3B;
            content[0x27] = 0xC4;

            //TZC3.X  -
            content[0x29] = 0x80;
            content[0x2A] = 0x3B;
            content[0x2B] = 0x44;
            //TZC3.Z  +
            content[0x2D] = 0x80;
            content[0x2E] = 0x3B;
            content[0x2F] = 0xC4;

            //3F800000 = 1.0

            //scaleX
            content[0x4A] = 0x80;
            content[0x4B] = 0x3F;

            //scaleY
            content[0x4E] = 0x80;
            content[0x4F] = 0x3F;

            //scaleZ
            content[0x52] = 0x80;
            content[0x53] = 0x3F;

            // custom color
            content[0x58] = (byte)(Globals.GL_ColorQuadCustom.X * 255);
            content[0x59] = (byte)(Globals.GL_ColorQuadCustom.Y * 255);
            content[0x5A] = (byte)(Globals.GL_ColorQuadCustom.Z * 255);

            //enable (pointer)
            content[0x5B] = 0x01;

            Lines.Add(newID, content);
            Texts.Add(newID, "Unnamed Object " + newID);
            return newID;
        }

        private void RemoveLineID(ushort ID)
        {
            Lines.Remove(ID);
            Texts.Remove(ID);
        }

        #endregion

        #region metodos para o GL

        private Vector3 GetScale(ushort ID)
        {
            return new Vector3(ReturnScaleX(ID), ReturnScaleY(ID), ReturnScaleZ(ID));
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

        private Vector4 GetCustomColor(ushort ID)
        {
            return new Vector4(Lines[ID][0x58] / 255f, Lines[ID][0x59] / 255f, Lines[ID][0x5A] / 255f, 1f);
        }

        #endregion


        #region property position angle scale

        private int DynamicOffset(int origin) 
        {
            return origin + 0x30;
        }


        private uint ReturnScaleX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x18));
        }

        private void SetScaleX_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x18)] = b[0];
            Lines[ID][DynamicOffset(0x19)] = b[1];
            Lines[ID][DynamicOffset(0x1A)] = b[2];
            Lines[ID][DynamicOffset(0x1B)] = b[3];
        }

        private uint ReturnScaleY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x1C));
        }

        private void SetScaleY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x1C)] = b[0];
            Lines[ID][DynamicOffset(0x1D)] = b[1];
            Lines[ID][DynamicOffset(0x1E)] = b[2];
            Lines[ID][DynamicOffset(0x1F)] = b[3];
        }

        private uint ReturnScaleZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x20));
        }

        private void SetScaleZ_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x20)] = b[0];
            Lines[ID][DynamicOffset(0x21)] = b[1];
            Lines[ID][DynamicOffset(0x22)] = b[2];
            Lines[ID][DynamicOffset(0x23)] = b[3];
        }

        private uint ReturnAngleX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x0C));
        }

        private void SetAngleX_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x0C)] = b[0];
            Lines[ID][DynamicOffset(0x0D)] = b[1];
            Lines[ID][DynamicOffset(0x0E)] = b[2];
            Lines[ID][DynamicOffset(0x0F)] = b[3];
        }

        private uint ReturnAngleY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x10));
        }

        private void SetAngleY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x10)] = b[0];
            Lines[ID][DynamicOffset(0x11)] = b[1];
            Lines[ID][DynamicOffset(0x12)] = b[2];
            Lines[ID][DynamicOffset(0x13)] = b[3];
        }

        private uint ReturnAngleZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x14));
        }

        private void SetAngleZ_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x14)] = b[0];
            Lines[ID][DynamicOffset(0x15)] = b[1];
            Lines[ID][DynamicOffset(0x16)] = b[2];
            Lines[ID][DynamicOffset(0x17)] = b[3];
        }


        private uint ReturnPositionX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x00));
        }

        private void SetPositionX_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x00)] = b[0];
            Lines[ID][DynamicOffset(0x01)] = b[1];
            Lines[ID][DynamicOffset(0x02)] = b[2];
            Lines[ID][DynamicOffset(0x03)] = b[3];
        }

        private uint ReturnPositionY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x04));
        }

        private void SetPositionY_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x04)] = b[0];
            Lines[ID][DynamicOffset(0x05)] = b[1];
            Lines[ID][DynamicOffset(0x06)] = b[2];
            Lines[ID][DynamicOffset(0x07)] = b[3];
        }

        private uint ReturnPositionZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], DynamicOffset(0x08));
        }

        private void SetPositionZ_Hex(ushort ID, uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][DynamicOffset(0x08)] = b[0];
            Lines[ID][DynamicOffset(0x09)] = b[1];
            Lines[ID][DynamicOffset(0x0A)] = b[2];
            Lines[ID][DynamicOffset(0x0B)] = b[3];
        }

        // floats

        private float ReturnScaleX(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleX_Hex(ID)), 0);
        }

        private float ReturnScaleY(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleY_Hex(ID)), 0);
        }

        private float ReturnScaleZ(ushort ID)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(ReturnScaleZ_Hex(ID)), 0);
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

        #region property outros

        private string ReturnObjectName(ushort ID)
        {
            if (Texts.ContainsKey(ID))
            {
                return Texts[ID];
            }
            return "Unnamed Object " + ID;
        }

        private void SetObjectName(ushort ID, string Text) 
        {
            if (Texts.ContainsKey(ID))
            {
                Texts[ID] = Text;
            }
        }

        private uint ReturnPointModelID(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], 0x54);
        }

        private void SetPointModelID(ushort ID, uint value) 
        {
            BitConverter.GetBytes(value).CopyTo(Lines[ID], 0x54);
        }

        private byte ReturnPointStatus(ushort ID) 
        {
            return Lines[ID][0x5B];
        }

        private void SetPointStatus(ushort ID, byte value) 
        {
            Lines[ID][0x5B] = value;
        }

        private QuadCustomPointStatus GetQuadCustomPointStatus(ushort ID)
        {
            byte status = Lines[ID][0x5B];
            if (status < 3)
            {
                return (QuadCustomPointStatus)status;
            }
            return QuadCustomPointStatus.AnotherValue;
        }


        private byte[] ReturnColorRGB(ushort ID)
        {
            byte[] rgb = new byte[3];
            rgb[0] = Lines[ID][0x58];
            rgb[1] = Lines[ID][0x59];
            rgb[2] = Lines[ID][0x5A];
            return rgb;
        }

        private void SetColorRGB(ushort ID, byte[] value) 
        {
            Lines[ID][0x58] = value[0];
            Lines[ID][0x59] = value[1];
            Lines[ID][0x5A] = value[2];
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

        #region Triggerzone dependencias

        protected override byte[] GetInternalLine(ushort ID)
        {
            return Lines[ID];
        }

        protected override int GetTriggerZoneStartIndex()
        {
            return 0x00; // o primeiro campo é a Triggerzone
        }

        #endregion


        #region move metodos

        private Vector3 GetObjPostion_ToCamera(ushort ID)
        {
            Vector3 position = Vector3.Zero;

            QuadCustomPointStatus status = GetQuadCustomPointStatus(ID);
            if (status == QuadCustomPointStatus.ArrowPoint01 || status == QuadCustomPointStatus.CustomModel02)
            {
                position = GetPosition(ID);
                Utils.ToCameraCheckValue(ref position);
            }
            else
            {
                position = GetTriggerZonePos_ToCamera(ID);
            }

            return position;
        }

        private float GetObjAngleY_ToCamera(ushort ID)
        {
            float AngleY = 0f;
            QuadCustomPointStatus status = GetQuadCustomPointStatus(ID);
            if (status == QuadCustomPointStatus.ArrowPoint01 || status == QuadCustomPointStatus.CustomModel02)
            {
                AngleY = ReturnAngleY(ID);
                if (float.IsNaN(AngleY) || float.IsInfinity(AngleY)) { AngleY = 0; }
            }
            return AngleY;
        }


        private Vector3[] GetObjPostion_ToMove_General(ushort ID)
        {
            Vector3[] pos = new Vector3[7];
            QuadCustomPointStatus status = GetQuadCustomPointStatus(ID);
            if (status == QuadCustomPointStatus.ArrowPoint01 || status == QuadCustomPointStatus.CustomModel02)
            {
                pos[0] = new Vector3(ReturnPositionX(ID), ReturnPositionY(ID), ReturnPositionZ(ID));
            }
            else
            {
                pos[0] = Vector3.Zero;
            }

            GetTriggerZonePos_ToMove_General(ID, ref pos);

            Utils.ToMoveCheckLimits(ref pos);
            return pos;
        }


        private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value)
        {
            if (value != null && value.Length >= 6)
            {
                QuadCustomPointStatus status = GetQuadCustomPointStatus(ID);
                if (status == QuadCustomPointStatus.ArrowPoint01 || status == QuadCustomPointStatus.CustomModel02)
                {
                    SetPositionX(ID, value[0].X);
                    SetPositionY(ID, value[0].Y);
                    SetPositionZ(ID, value[0].Z);
                }

                SetTriggerZonePos_ToMove_General(ID, value);
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

    }
}
