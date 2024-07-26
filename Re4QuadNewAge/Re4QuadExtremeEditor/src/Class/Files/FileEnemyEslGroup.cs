using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System.Drawing;
using OpenTK;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    /// <summary>
    /// Classe que representa o arquivo .ESL (Inimigos);
    /// </summary>
    public class FileEnemyEslGroup
    {
        /// <summary>
        /// <para>aqui contem o conteudo de todos os inimigos do arquivo;</para>
        /// <para>id da linha, sequencia de 32 bytes (para ambas as versões);</para>
        /// </summary>
        public Dictionary<ushort, byte[]> Lines { get; private set; }
        /// <summary>
        /// aqui contem o resto do arquivo, a parte não usada
        /// </summary>
        public byte[] EndFile;

        public FileEnemyEslGroup()
        {
            Lines = new Dictionary<ushort, byte[]>();
            EndFile = new byte[0];
            Methods = new EnemyMethods();

            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            Methods.ReturnByteFromPosition = ReturnByteFromPosition;
            Methods.SetByteFromPosition = SetByteFromPosition;

            Methods.ReturnOffset0x00Enable = ReturnOffset0x00Enable;
            Methods.SetOffset0x00Enable = SetOffset0x00Enable;

            Methods.ReturnOffset0x01EnemyGroup = ReturnOffset0x01EnemyGroup;
            Methods.SetOffset0x01EnemyGroup = SetOffset0x01EnemyGroup;
            Methods.ReturnOffset0x02EnemySubID = ReturnOffset0x02EnemySubID;
            Methods.SetOffset0x02EnemySubID = SetOffset0x02EnemySubID;

            Methods.ReturnEnemyID = ReturnEnemyID;
            Methods.SetEnemyID = SetEnemyID;

            Methods.ReturnLife = ReturnLife;
            Methods.SetLife = SetLife;

            Methods.ReturnPositionX = ReturnPositionX;
            Methods.ReturnPositionY = ReturnPositionY;
            Methods.ReturnPositionZ = ReturnPositionZ;
            Methods.SetPositionX = SetPositionX;
            Methods.SetPositionY = SetPositionY;
            Methods.SetPositionZ = SetPositionZ;

            Methods.ReturnRotationX = ReturnRotationX;
            Methods.ReturnRotationY = ReturnRotationY;
            Methods.ReturnRotationZ = ReturnRotationZ;
            Methods.SetRotationX = SetRotationX;
            Methods.SetRotationY = SetRotationY;
            Methods.SetRotationZ = SetRotationZ;

            Methods.ReturnRoomID = ReturnRoomID;
            Methods.SetRoomID = SetRoomID;

            DisplayMethods = new NodeDisplayMethods();
            DisplayMethods.GetNodeText = GetNodeText;
            DisplayMethods.GetNodeColor = GetNodeColor;

            MoveMethods = new NodeMoveMethods();
            MoveMethods.GetObjPostion_ToCamera = GetPosition;
            MoveMethods.GetObjAngleY_ToCamera = GetObjAngleY_ToCamera;
            MoveMethods.GetObjPostion_ToMove_General = GetObjPostion_ToMove_General;
            MoveMethods.SetObjPostion_ToMove_General = SetObjPostion_ToMove_General;
            MoveMethods.GetObjRotationAngles_ToMove = GetObjRotationAngles_ToMove;
            MoveMethods.SetObjRotationAngles_ToMove = SetObjRotationAngles_ToMove;
            MoveMethods.GetObjScale_ToMove = Utils.GetObjScale_ToMove_Null;
            MoveMethods.SetObjScale_ToMove = Utils.SetObjScale_ToMove_Null;
            MoveMethods.GetTriggerZoneCategory = Utils.GetTriggerZoneCategory_Null;

            MethodsForGL = new EnemyMethodsForGL();
            MethodsForGL.GetEnemyModelID = ReturnEnemyID;
            MethodsForGL.GetPosition = GetPosition;
            MethodsForGL.GetRotation = GetRotation;
            MethodsForGL.GetEnemyRoom = ReturnRoomID;
            MethodsForGL.GetEnableState = ReturnOffset0x00Enable;

        }


        /// <summary>
        /// Classe com os metodos que serão passados para classe EnemyProperty;
        /// </summary>
        public EnemyMethods Methods { get; }

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
        public EnemyMethodsForGL MethodsForGL { get; }

        // metodos:

        #region metodos para os Nodes Display

        // texto do treeNode
        public string GetNodeText(ushort ID)
        {
            if (Lines.ContainsKey(ID) && Globals.TreeNodeRenderHexValues)
            {
                return "[" + ID.ToString("X2") + "] " + BitConverter.ToString(Lines[ID]).Replace("-", "_");
            }
            else if (Lines.ContainsKey(ID))
            {
                byte enable = ReturnOffset0x00Enable(ID);
                string r = "[" + ID.ToString("X2") + "] " + "0x" + enable.ToString("X2") + ": ";

                if (enable == 0)
                {
                    r += Lang.GetAttributeText(aLang.ListBoxDisable);
                }
                else if (enable == 1)
                {
                    r += Lang.GetAttributeText(aLang.ListBoxEnable);
                }
                else
                {
                    r += Lang.GetAttributeText(aLang.ListBoxAnotherValue);
                }

                r += " - 0x";

                ushort v = Methods.ReturnEnemyID(ID);
                string sv = v.ToString("X4");
                string svff = sv[0].ToString() + sv[1].ToString() + "FF";
                ushort vff = ushort.Parse(svff, System.Globalization.NumberStyles.HexNumber);

                r += sv + ": ";

                if (DataBase.EnemiesIDs.List.ContainsKey(v) && !(sv[2] == 'F' && sv[3] == 'F'))
                {
                    r += DataBase.EnemiesIDs.List[v].Name;
                }
                else if (DataBase.EnemiesIDs.List.ContainsKey(vff) && vff != 0xFFFF)
                {
                    r += DataBase.EnemiesIDs.List[vff].Name;
                }
                else
                {
                    r += Lang.GetAttributeText(aLang.ListBoxUnknownEnemy);
                }

                r += " - R" + ReturnRoomID(ID).ToString("X3");

                string associated = DataBase.Extras.AssociatedSpecialEventObjName(RefInteractionType.Enemy, ID);
                if (associated != null)
                {
                    r += " - Associated = " + associated;
                }

                return r;
            }
            else 
            {
                return "ESL Error Internal Line ID " + ID;
            }
            
        }

        public Color GetNodeColor(ushort ID) 
        {
            if (!Globals.RenderEnemyESL)
            {
                return Globals.NodeColorHided;
            }
            else if (!Globals.RenderDisabledEnemy && Lines.ContainsKey(ID) && ReturnOffset0x00Enable(ID) == 00)
            {
                return Globals.NodeColorHided;
            }
            if (!Globals.RenderDontShowOnlyDefinedRoom && Lines.ContainsKey(ID) && ReturnRoomID(ID) != Globals.RenderEnemyFromDefinedRoom)
            {
                return Globals.NodeColorHided;
            }
            return Globals.NodeColorEntry;
        }

        #endregion


        #region metodos para os MoveMethods
        private float GetObjAngleY_ToCamera(ushort ID)
        {
            return Utils.EnemyAngleToRad(ReturnRotationY(ID));
        }

        private Vector3[] GetObjPostion_ToMove_General(ushort ID) 
        {
            Vector3[] pos = new Vector3[1];
            pos[0] = new Vector3(ReturnPositionX(ID) * 10f, ReturnPositionY(ID) * 10f, ReturnPositionZ(ID) * 10f);
            return pos;
        }

        private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value) 
        {
            if (value != null && value.Length >= 1)
            {
                float X = value[0].X / 10f;
                float Y = value[0].Y / 10f;
                float Z = value[0].Z / 10f;
                if (X > short.MaxValue) { X = short.MaxValue; }
                if (X < short.MinValue) { X = short.MinValue; }
                if (Y > short.MaxValue) { Y = short.MaxValue; }
                if (Y < short.MinValue) { Y = short.MinValue; }
                if (Z > short.MaxValue) { Z = short.MaxValue; }
                if (Z < short.MinValue) { Z = short.MinValue; }
                short sX = (short)X;
                short sY = (short)Y;
                short sZ = (short)Z;
                SetPositionX(ID, sX);
                SetPositionY(ID, sY);
                SetPositionZ(ID, sZ);
            }
        }


        private Vector3[] GetObjRotationAngles_ToMove(ushort ID) 
        {
            Vector3[] v = new Vector3[1];
            v[0] = new Vector3(Utils.EnemyAngleToRad(ReturnRotationX(ID)), Utils.EnemyAngleToRad(ReturnRotationY(ID)), Utils.EnemyAngleToRad(ReturnRotationZ(ID)));
            return v;
        }

        private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value) 
        {
            if (value != null && value.Length >= 1)
            {
                short X = Utils.RadToEnemyAngle(Utils.RadAngle1Scale(value[0].X));
                short Y = Utils.RadToEnemyAngle(Utils.RadAngle1Scale(value[0].Y));
                short Z = Utils.RadToEnemyAngle(Utils.RadAngle1Scale(value[0].Z));
                SetRotationX(ID, X);
                SetRotationY(ID, Y);
                SetRotationZ(ID, Z);
            }
        }

        #endregion


        #region metodos das propriedades
        private byte ReturnOffset0x00Enable(ushort ID) 
        {
            return Lines[ID][0x00];
        }
        private void SetOffset0x00Enable(ushort ID, byte value) 
        {
            Lines[ID][0x00] = value;
        }


        private byte ReturnOffset0x01EnemyGroup(ushort ID)
        {
            return Lines[ID][0x01];
        }
        private void SetOffset0x01EnemyGroup(ushort ID, byte value)
        {
            Lines[ID][0x01] = value;
        }


        private byte ReturnOffset0x02EnemySubID(ushort ID)
        {
            return Lines[ID][0x02];
        }
        private void SetOffset0x02EnemySubID(ushort ID, byte value)
        {
            Lines[ID][0x02] = value;
        }


        private ushort ReturnEnemyID(ushort ID)
        {
            byte[] b = new byte[2];
            b[1] = Lines[ID][0x01];
            b[0] = Lines[ID][0x02];
            return BitConverter.ToUInt16(b, 0);
        }
        private void SetEnemyID(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x01] = b[1];
            Lines[ID][0x02] = b[0];
        }


        private short ReturnLife(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0X08);
        }
        private void SetLife(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x08] = b[0];
            Lines[ID][0x09] = b[1];
        }


        private short ReturnPositionX(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x0C);
        }
        private void SetPositionX(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x0C] = b[0];
            Lines[ID][0x0D] = b[1];
        }

        private short ReturnPositionY(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x0E);
        }
        private void SetPositionY(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x0E] = b[0];
            Lines[ID][0x0F] = b[1];
        }


        private short ReturnPositionZ(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x10);
        }
        private void SetPositionZ(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x10] = b[0];
            Lines[ID][0x11] = b[1];
        }


        private short ReturnRotationX(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x12);
        }
        private void SetRotationX(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x12] = b[0];
            Lines[ID][0x13] = b[1];
        }

        private short ReturnRotationY(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x14);
        }
        private void SetRotationY(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x14] = b[0];
            Lines[ID][0x15] = b[1];
        }

        private short ReturnRotationZ(ushort ID)
        {
            return BitConverter.ToInt16(Lines[ID], 0x16);
        }
        private void SetRotationZ(ushort ID, short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x16] = b[0];
            Lines[ID][0x17] = b[1];
        }


        private ushort ReturnRoomID(ushort ID)
        {
            return BitConverter.ToUInt16(Lines[ID], 0X18);
        }
        private void SetRoomID(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][0x18] = b[0];
            Lines[ID][0x19] = b[1];
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
            value.CopyTo(Lines[ID], 0);
        }

        #endregion


        #region metodos para o GL

        Vector3 GetPosition(ushort ID) 
        {
            return new Vector3(ReturnPositionX(ID) / 10f, ReturnPositionY(ID) / 10f, ReturnPositionZ(ID) / 10f);      
        }

        Matrix4 GetRotation(ushort ID)
        {
            //ordem correta: XYZ
            return Matrix4.CreateRotationX(Utils.EnemyAngleToRad(ReturnRotationX(ID))) * Matrix4.CreateRotationY(Utils.EnemyAngleToRad(ReturnRotationY(ID))) * Matrix4.CreateRotationZ(Utils.EnemyAngleToRad(ReturnRotationZ(ID)));
        }

        #endregion
    }
}
