using OpenTK;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Re4QuadExtremeEditor.src.Class.Files
{
    /// <summary>
    /// Classe que representa os arquivos .ITA (Items) e .AEV (Eventos);
    /// </summary>
    public class FileSpecialGroup : BaseTriggerZoneGroup
    {
        /// <summary>
        /// de qual versão do re4 que é o arquivo;
        /// </summary>
        public Re4Version GetRe4Version { get; }     
        /// <summary>
        /// define se é da versão ps4/ns, nota esta adapdado para o formato do uhd (gambiarra)
        /// </summary>
        public bool IsPs4Ns_Adapted { get; }

        /// <summary>
        /// especifica se é ITA ou AEV;
        /// </summary>
        public SpecialFileFormat GetSpecialFileFormat { get; }
        /// <summary>
        /// <para>aqui contem o comeco do arquivo 16 bytes fixos;</para>
        /// <para>offset 0, 1 e 2 formato do arquivo, ascii: AEV ou ITA;</para>
        /// <para>offset 3 fixo: 0x00;</para>
        /// <para>offset 4 e 5: AEV: 0x0401 e ITA: 0x0501;</para>
        /// <para>offset 6 e 7: ushort value usado para a quantidade de "linhas";</para>
        /// </summary>
        public byte[] StartFile;
        /// <summary>
        /// <para>aqui contem o conteudo de todos os "Speciais" do arquivo;</para>
        /// <para>id da linha, sequencia de 176 bytes (.ITA) para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 156 bytes (.ITA) para re4 uhd;</para>
        /// <para>id da linha, sequencia de 160 bytes (.AEV) para re4 2007ps2;</para>
        /// <para>id da linha, sequencia de 156 bytes (.AEV) para re4 uhd;</para>
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
        /// lista de FirstIndex offset[0x36]
        /// <para>Specialindex, line</para>
        /// </summary>
        private Dictionary<byte, List<ushort>> FirstIndexList;

        /// <summary>
        /// lista de SecundIndex, somente para o tipo item
        /// <para>SecundIndex, Line</para>
        /// </summary>
        private Dictionary<ushort, List<ushort>> SecundIndexList;

        public FileSpecialGroup(Re4Version version, SpecialFileFormat fileFormat, bool IsPs4Ns_Adapted = false)
        {
            this.IsPs4Ns_Adapted = IsPs4Ns_Adapted;
            GetRe4Version = version;
            GetSpecialFileFormat = fileFormat;
            StartFile = new byte[16];
            Lines = new Dictionary<ushort, byte[]>();
            EndFile = new byte[0];

            FirstIndexList = new Dictionary<byte, List<ushort>>();
            SecundIndexList = new Dictionary<ushort, List<ushort>>();

            Methods = new SpecialMethods();
            SetBaseMethods(Methods);
            Methods.ReturnRe4Version = ReturnRe4Version;
            Methods.GetSpecialFileFormat = GetSpecialFileFormatMethod;

            Methods.GetSpecialType = GetSpecialType;
            Methods.GetRefInteractionType = GetRefInteractionType;
            
            Methods.ReturnLine = ReturnLine;
            Methods.SetLine = SetLine;

            Methods.ReturnSpecialType = ReturnSpecialType;
            Methods.SetSpecialType = SetSpecialType;
            Methods.ReturnSpecialIndex = ReturnSpecialIndex;
            Methods.SetSpecialIndex = SetSpecialIndex;

            #region PART1
            Methods.ReturnUnknown_GG = ReturnUnknown_GG;
            Methods.SetUnknown_GG = SetUnknown_GG;
          
            // triggerZone entre esse campos

            Methods.ReturnUnknown_KG = ReturnUnknown_KG;
            Methods.SetUnknown_KG = SetUnknown_KG;
            Methods.ReturnUnknown_KJ = ReturnUnknown_KJ;
            Methods.SetUnknown_KJ = SetUnknown_KJ;
            Methods.ReturnUnknown_LI = ReturnUnknown_LI;
            Methods.SetUnknown_LI = SetUnknown_LI;
            Methods.ReturnUnknown_LO = ReturnUnknown_LO;
            Methods.SetUnknown_LO = SetUnknown_LO;
            Methods.ReturnUnknown_LU = ReturnUnknown_LU;
            Methods.SetUnknown_LU = SetUnknown_LU;
            Methods.ReturnUnknown_LH = ReturnUnknown_LH;
            Methods.SetUnknown_LH = SetUnknown_LH;
            Methods.ReturnUnknown_MI = ReturnUnknown_MI;
            Methods.SetUnknown_MI = SetUnknown_MI;
            Methods.ReturnUnknown_MO = ReturnUnknown_MO;
            Methods.SetUnknown_MO = SetUnknown_MO;
            Methods.ReturnUnknown_MU = ReturnUnknown_MU;
            Methods.SetUnknown_MU = SetUnknown_MU;
            Methods.ReturnUnknown_NI = ReturnUnknown_NI;
            Methods.SetUnknown_NI = SetUnknown_NI;
            Methods.ReturnUnknown_NO = ReturnUnknown_NO;
            Methods.SetUnknown_NO = SetUnknown_NO;
            Methods.ReturnUnknown_NS = ReturnUnknown_NS;
            Methods.SetUnknown_NS = SetUnknown_NS;
            Methods.ReturnRefInteractionType = ReturnRefInteractionType;
            Methods.SetRefInteractionType = SetRefInteractionType;
            Methods.ReturnRefInteractionIndex = ReturnRefInteractionIndex;
            Methods.SetRefInteractionIndex = SetRefInteractionIndex;
            Methods.ReturnUnknown_NT = ReturnUnknown_NT;
            Methods.SetUnknown_NT = SetUnknown_NT;
            Methods.ReturnUnknown_NU = ReturnUnknown_NU;
            Methods.SetUnknown_NU = SetUnknown_NU;
            Methods.ReturnPromptMessage = ReturnPromptMessage;
            Methods.SetPromptMessage = SetPromptMessage;
            Methods.ReturnUnknown_PI = ReturnUnknown_PI;
            Methods.SetUnknown_PI = SetUnknown_PI;
            Methods.ReturnUnknown_PO = ReturnUnknown_PO;
            Methods.SetUnknown_PO = SetUnknown_PO;
            Methods.ReturnUnknown_PU = ReturnUnknown_PU;
            Methods.SetUnknown_PU = SetUnknown_PU;
            Methods.ReturnUnknown_PK = ReturnUnknown_PK;
            Methods.SetUnknown_PK = SetUnknown_PK;
            Methods.ReturnMessageColor = ReturnMessageColor;
            Methods.SetMessageColor = SetMessageColor;
            Methods.ReturnUnknown_QI = ReturnUnknown_QI;
            Methods.SetUnknown_QI = SetUnknown_QI;
            Methods.ReturnUnknown_QO = ReturnUnknown_QO;
            Methods.SetUnknown_QO = SetUnknown_QO;

            //CLASSIC
            Methods.ReturnUnknown_QU = ReturnUnknown_QU;
            Methods.SetUnknown_QU = SetUnknown_QU;
            #endregion

            #region part2 and itens
            // itens (ITA)


            Methods.ReturnObjPointW = ReturnObjPointW;
            Methods.SetObjPointW = SetObjPointW;

            Methods.ReturnObjPointW_onlyClassic = ReturnObjPointW_onlyClassic;
            Methods.SetObjPointW_onlyClassic = SetObjPointW_onlyClassic;


            Methods.ReturnObjPointX_Hex = ReturnObjPositionX_Hex;
            Methods.ReturnObjPointY_Hex = ReturnObjPositionY_Hex;
            Methods.ReturnObjPointZ_Hex = ReturnObjPositionZ_Hex;         
            Methods.ReturnUnknown_RI_X_Hex = ReturnUnknown_RI_X_Hex;
            Methods.ReturnUnknown_RI_Y_Hex = ReturnUnknown_RI_Y_Hex;
            Methods.ReturnUnknown_RI_Z_Hex = ReturnUnknown_RI_Z_Hex;
            Methods.ReturnItemTriggerRadius_Hex = ReturnItemTriggerRadius_Hex;
            Methods.ReturnItemAngleX_Hex = ReturnItemAngleX_Hex;
            Methods.ReturnItemAngleY_Hex = ReturnItemAngleY_Hex;
            Methods.ReturnItemAngleZ_Hex = ReturnItemAngleZ_Hex;
            Methods.SetObjPointX_Hex = SetObjPositionX_Hex;
            Methods.SetObjPointY_Hex = SetObjPositionY_Hex;
            Methods.SetObjPointZ_Hex = SetObjPositionZ_Hex;
            Methods.SetUnknown_RI_X_Hex = SetUnknown_RI_X_Hex;
            Methods.SetUnknown_RI_Y_Hex = SetUnknown_RI_Y_Hex;
            Methods.SetUnknown_RI_Z_Hex = SetUnknown_RI_Z_Hex;
            Methods.SetItemTriggerRadius_Hex = SetItemTriggerRadius_Hex;
            Methods.SetItemAngleX_Hex = SetItemAngleX_Hex;
            Methods.SetItemAngleY_Hex = SetItemAngleY_Hex;
            Methods.SetItemAngleZ_Hex = SetItemAngleZ_Hex;
            Methods.ReturnObjPointX = ReturnObjPositionX;
            Methods.ReturnObjPointY = ReturnObjPositionY;
            Methods.ReturnObjPointZ = ReturnObjPositionZ;
            Methods.ReturnUnknown_RI_X = ReturnUnknown_RI_X;
            Methods.ReturnUnknown_RI_Y = ReturnUnknown_RI_Y;
            Methods.ReturnUnknown_RI_Z = ReturnUnknown_RI_Z;
            Methods.ReturnItemTriggerRadius = ReturnItemTriggerRadius;
            Methods.ReturnItemAngleX = ReturnItemAngleX;
            Methods.ReturnItemAngleY = ReturnItemAngleY;
            Methods.ReturnItemAngleZ = ReturnItemAngleZ;
            Methods.SetObjPointX = SetObjPositionX;
            Methods.SetObjPointY = SetObjPositionY;
            Methods.SetObjPointZ = SetObjPositionZ;
            Methods.SetUnknown_RI_X = SetUnknown_RI_X;
            Methods.SetUnknown_RI_Y = SetUnknown_RI_Y;
            Methods.SetUnknown_RI_Z = SetUnknown_RI_Z;   
            Methods.SetItemTriggerRadius = SetItemTriggerRadius;
            Methods.SetItemAngleX = SetItemAngleX;
            Methods.SetItemAngleY = SetItemAngleY;
            Methods.SetItemAngleZ = SetItemAngleZ;

            // itens (ITA)

   
            Methods.ReturnUnknown_RI_W = ReturnUnknown_RI_W; 
            Methods.SetUnknown_RI_W = SetUnknown_RI_W;
            Methods.ReturnItemAngleW = ReturnItemAngleW;
            Methods.SetItemAngleW = SetItemAngleW;

            Methods.ReturnUnknown_RO = ReturnUnknown_RO;
            Methods.SetUnknown_RO = SetUnknown_RO;
            Methods.ReturnItemNumber = ReturnItemNumber;
            Methods.SetItemNumber = SetItemNumber;
            Methods.ReturnUnknown_RU = ReturnUnknown_RU;
            Methods.SetUnknown_RU = SetUnknown_RU;
            Methods.ReturnItemAmount = ReturnItemAmount;
            Methods.SetItemAmount = SetItemAmount;
            Methods.ReturnSecundIndex = ReturnSecundIndex;
            Methods.SetSecundIndex = SetSecundIndex;
            Methods.ReturnItemAuraType = ReturnItemAuraType;
            Methods.SetItemAuraType = SetItemAuraType;

            Methods.ReturnUnknown_QM = ReturnUnknown_QM;
            Methods.SetUnknown_QM = SetUnknown_QM;
            Methods.ReturnUnknown_QL = ReturnUnknown_QL;
            Methods.SetUnknown_QL = SetUnknown_QL;
            Methods.ReturnUnknown_QR = ReturnUnknown_QR;
            Methods.SetUnknown_QR = SetUnknown_QR;
            Methods.ReturnUnknown_QH = ReturnUnknown_QH;
            Methods.SetUnknown_QH = SetUnknown_QH;
            Methods.ReturnUnknown_QG = ReturnUnknown_QG;
            Methods.SetUnknown_QG = SetUnknown_QG;



            //ITA classic
            Methods.ReturnUnknown_VS = ReturnUnknown_VS;
            Methods.SetUnknown_VS = SetUnknown_VS;
            Methods.ReturnUnknown_VT = ReturnUnknown_VT;
            Methods.SetUnknown_VT = SetUnknown_VT;
            Methods.ReturnUnknown_VI = ReturnUnknown_VI;
            Methods.SetUnknown_VI = SetUnknown_VI;
            Methods.ReturnUnknown_VO = ReturnUnknown_VO;
            Methods.SetUnknown_VO = SetUnknown_VO;

            #endregion

            #region part2, Unknown/geral types

            //special Unknown/geral types
            Methods.ReturnUnknown_HH = ReturnUnknown_HH;
            Methods.SetUnknown_HH = SetUnknown_HH;
            Methods.ReturnUnknown_HK = ReturnUnknown_HK;
            Methods.SetUnknown_HK = SetUnknown_HK;
            Methods.ReturnUnknown_HL = ReturnUnknown_HL;
            Methods.SetUnknown_HL = SetUnknown_HL;
            Methods.ReturnUnknown_HM = ReturnUnknown_HM;
            Methods.SetUnknown_HM = SetUnknown_HM;
            Methods.ReturnUnknown_HN = ReturnUnknown_HN;
            Methods.SetUnknown_HN = SetUnknown_HN;
            Methods.ReturnUnknown_HR = ReturnUnknown_HR;
            Methods.SetUnknown_HR = SetUnknown_HR;
            Methods.ReturnUnknown_RH = ReturnUnknown_RH;
            Methods.SetUnknown_RH = SetUnknown_RH;
            Methods.ReturnUnknown_RJ = ReturnUnknown_RJ;
            Methods.SetUnknown_RJ = SetUnknown_RJ;
            Methods.ReturnUnknown_RK = ReturnUnknown_RK;
            Methods.SetUnknown_RK = SetUnknown_RK;
            Methods.ReturnUnknown_RL = ReturnUnknown_RL;
            Methods.SetUnknown_RL = SetUnknown_RL;
            Methods.ReturnUnknown_RM = ReturnUnknown_RM;
            Methods.SetUnknown_RM = SetUnknown_RM;
            Methods.ReturnUnknown_RN = ReturnUnknown_RN;
            Methods.SetUnknown_RN = SetUnknown_RN;
            Methods.ReturnUnknown_RP = ReturnUnknown_RP;
            Methods.SetUnknown_RP = SetUnknown_RP;
            Methods.ReturnUnknown_RQ = ReturnUnknown_RQ;
            Methods.SetUnknown_RQ = SetUnknown_RQ;
            Methods.ReturnUnknown_TG = ReturnUnknown_TG;
            Methods.SetUnknown_TG = SetUnknown_TG;
            Methods.ReturnUnknown_TH = ReturnUnknown_TH;
            Methods.SetUnknown_TH = SetUnknown_TH;
            Methods.ReturnUnknown_TJ = ReturnUnknown_TJ;
            Methods.SetUnknown_TJ = SetUnknown_TJ;
            Methods.ReturnUnknown_TK = ReturnUnknown_TK;
            Methods.SetUnknown_TK = SetUnknown_TK;
            Methods.ReturnUnknown_TL = ReturnUnknown_TL;
            Methods.SetUnknown_TL = SetUnknown_TL;
            Methods.ReturnUnknown_TM = ReturnUnknown_TM;
            Methods.SetUnknown_TM = SetUnknown_TM;
            Methods.ReturnUnknown_TN = ReturnUnknown_TN;
            Methods.SetUnknown_TN = SetUnknown_TN;
            Methods.ReturnUnknown_TP = ReturnUnknown_TP;
            Methods.SetUnknown_TP = SetUnknown_TP;
            Methods.ReturnUnknown_TQ = ReturnUnknown_TQ;
            Methods.SetUnknown_TQ = SetUnknown_TQ;

            // para os outros specials

            Methods.ReturnNeededItemNumber = ReturnNeededItemNumber;
            Methods.SetNeededItemNumber = SetNeededItemNumber;
            Methods.ReturnEnemyGroup = ReturnUnknown_HK_Ushort;
            Methods.SetEnemyGroup = SetUnknown_HK_Ushort;
            Methods.ReturnRoomMessage = ReturnUnknown_HK_Ushort;
            Methods.SetRoomMessage = SetUnknown_HK_Ushort;
            Methods.ReturnMessageCutSceneID = ReturnMessageCutSceneID;
            Methods.SetMessageCutSceneID = SetMessageCutSceneID;
            Methods.ReturnMessageID = ReturnMessageID;
            Methods.SetMessageID = SetMessageID;
            Methods.ReturnActivationType = ReturnActivationType;
            Methods.SetActivationType = SetActivationType;
            Methods.ReturnDamageType = ReturnDamageType;
            Methods.SetDamageType = SetDamageType;
            Methods.ReturnBlockingType = ReturnBlockingType;
            Methods.SetBlockingType = SetBlockingType;
            Methods.ReturnUnknown_SJ = ReturnUnknown_SJ;
            Methods.SetUnknown_SJ = SetUnknown_SJ;
            Methods.ReturnDamageAmount = ReturnDamageAmount;
            Methods.SetDamageAmount = SetDamageAmount;

            // warp door
            Methods.ReturnDestinationFacingAngle = ReturnDestinationFacingAngle;
            Methods.SetDestinationFacingAngle = SetDestinationFacingAngle;
            Methods.ReturnDestinationFacingAngle_Hex = ReturnDestinationFacingAngle_Hex;
            Methods.SetDestinationFacingAngle_Hex = SetDestinationFacingAngle_Hex;

            Methods.ReturnDestinationRoom = ReturnDestinationRoom;
            Methods.SetDestinationRoom = SetDestinationRoom;

            Methods.ReturnLockedDoorType = ReturnLockedDoorType;
            Methods.SetLockedDoorType = SetLockedDoorType;
            Methods.ReturnLockedDoorIndex = ReturnLockedDoorIndex;
            Methods.SetLockedDoorIndex = SetLockedDoorIndex;



            // localTeleportation
            Methods.ReturnTeleportationFacingAngle = ReturnLocationAndLadderFacingAngle; // basicamente aqui é os mesmos offsets
            Methods.SetTeleportationFacingAngle = SetLocationAndLadderFacingAngle;
            Methods.ReturnTeleportationFacingAngle_Hex = ReturnLocationAndLadderFacingAngle_Hex;
            Methods.SetTeleportationFacingAngle_Hex = SetLocationAndLadderFacingAngle_Hex;
            #endregion

            #region LadderClimbUp
            //LadderClimbUp
            Methods.ReturnLadderFacingAngle = ReturnLocationAndLadderFacingAngle;
            Methods.SetLadderFacingAngle = SetLocationAndLadderFacingAngle;
            Methods.ReturnLadderFacingAngle_Hex = ReturnLocationAndLadderFacingAngle_Hex;
            Methods.SetLadderFacingAngle_Hex = SetLocationAndLadderFacingAngle_Hex;
            Methods.ReturnLadderStepCount = ReturnLadderStepCount;
            Methods.SetLadderStepCount = SetLadderStepCount;
            Methods.ReturnLadderParameter0 = ReturnLadderParameter0;
            Methods.SetLadderParameter0 = SetLadderParameter0;
            Methods.ReturnLadderParameter1 = ReturnLadderParameter1;
            Methods.SetLadderParameter1 = SetLadderParameter1;
            Methods.ReturnLadderParameter2 = ReturnLadderParameter2;
            Methods.SetLadderParameter2 = SetLadderParameter2;
            Methods.ReturnLadderParameter3 = ReturnLadderParameter3;
            Methods.SetLadderParameter3 = SetLadderParameter3;
            Methods.ReturnUnknown_SG = ReturnUnknown_SG;
            Methods.SetUnknown_SG = SetUnknown_SG;
            Methods.ReturnUnknown_SH = ReturnUnknown_SH;
            Methods.SetUnknown_SH = SetUnknown_SH;
            #endregion

            #region GrappleGun
            //GrappleGun
            Methods.ReturnGrappleGunEndPointX = ReturnGrappleGunEndPointX;
            Methods.SetGrappleGunEndPointX = SetGrappleGunEndPointX;
            Methods.ReturnGrappleGunEndPointY = ReturnGrappleGunEndPointY;
            Methods.SetGrappleGunEndPointY = SetGrappleGunEndPointY;
            Methods.ReturnGrappleGunEndPointZ = ReturnGrappleGunEndPointZ;
            Methods.SetGrappleGunEndPointZ = SetGrappleGunEndPointZ;
            Methods.ReturnGrappleGunEndPointW = ReturnUnknown_RI_W;
            Methods.SetGrappleGunEndPointW = SetUnknown_RI_W;
            Methods.ReturnGrappleGunThirdPointX = ReturnGrappleGunThirdPointX;
            Methods.SetGrappleGunThirdPointX = SetGrappleGunThirdPointX;
            Methods.ReturnGrappleGunThirdPointY = ReturnGrappleGunThirdPointY;
            Methods.SetGrappleGunThirdPointY = SetGrappleGunThirdPointY;
            Methods.ReturnGrappleGunThirdPointZ = ReturnGrappleGunThirdPointZ;
            Methods.SetGrappleGunThirdPointZ = SetGrappleGunThirdPointZ;
            Methods.ReturnGrappleGunThirdPointW = ReturnGrappleGunThirdPointW;
            Methods.SetGrappleGunThirdPointW = SetGrappleGunThirdPointW;
            Methods.ReturnGrappleGunFacingAngle = ReturnGrappleGunFacingAngle;
            Methods.SetGrappleGunFacingAngle = SetGrappleGunFacingAngle;
            Methods.ReturnGrappleGunEndPointX_Hex = ReturnGrappleGunEndPointX_Hex;
            Methods.SetGrappleGunEndPointX_Hex = SetGrappleGunEndPointX_Hex;
            Methods.ReturnGrappleGunEndPointY_Hex = ReturnGrappleGunEndPointY_Hex;
            Methods.SetGrappleGunEndPointY_Hex = SetGrappleGunEndPointY_Hex;
            Methods.ReturnGrappleGunEndPointZ_Hex = ReturnGrappleGunEndPointZ_Hex;
            Methods.SetGrappleGunEndPointZ_Hex = SetGrappleGunEndPointZ_Hex;
            Methods.ReturnGrappleGunThirdPointX_Hex = ReturnGrappleGunThirdPointX_Hex;
            Methods.SetGrappleGunThirdPointX_Hex = SetGrappleGunThirdPointX_Hex;
            Methods.ReturnGrappleGunThirdPointY_Hex = ReturnGrappleGunThirdPointY_Hex;
            Methods.SetGrappleGunThirdPointY_Hex = SetGrappleGunThirdPointY_Hex;
            Methods.ReturnGrappleGunThirdPointZ_Hex = ReturnGrappleGunThirdPointZ_Hex;
            Methods.SetGrappleGunThirdPointZ_Hex = SetGrappleGunThirdPointZ_Hex;
            Methods.ReturnGrappleGunFacingAngle_Hex = ReturnGrappleGunFacingAngle_Hex;
            Methods.SetGrappleGunFacingAngle_Hex = SetGrappleGunFacingAngle_Hex;
            Methods.ReturnGrappleGunParameter0 = ReturnGrappleGunParameter0;
            Methods.SetGrappleGunParameter0 = SetGrappleGunParameter0;
            Methods.ReturnGrappleGunParameter1 = ReturnGrappleGunParameter1;
            Methods.SetGrappleGunParameter1 = SetGrappleGunParameter1;
            Methods.ReturnGrappleGunParameter2 = ReturnGrappleGunParameter2;
            Methods.SetGrappleGunParameter2 = SetGrappleGunParameter2;
            Methods.ReturnGrappleGunParameter3 = ReturnGrappleGunParameter3;
            Methods.SetGrappleGunParameter3 = SetGrappleGunParameter3;
            Methods.ReturnUnknown_SK = ReturnUnknown_SK;
            Methods.SetUnknown_SK = SetUnknown_SK;
            Methods.ReturnUnknown_SL = ReturnUnknown_SL;
            Methods.SetUnknown_SL = SetUnknown_SL;
            #endregion

            #region AshleyHiding
            //AshleyHiding
            Methods.ReturnAshleyHidingPointX = ReturnAshleyHidingPointX;
            Methods.SetAshleyHidingPointX = SetAshleyHidingPointX;
            Methods.ReturnAshleyHidingPointY = ReturnAshleyHidingPointY;
            Methods.SetAshleyHidingPointY = SetAshleyHidingPointY;
            Methods.ReturnAshleyHidingPointZ = ReturnAshleyHidingPointZ;
            Methods.SetAshleyHidingPointZ = SetAshleyHidingPointZ;
            Methods.ReturnAshleyHidingZoneCorner0_X = ReturnAshleyHidingZoneCorner0_X;
            Methods.SetAshleyHidingZoneCorner0_X = SetAshleyHidingZoneCorner0_X;
            Methods.ReturnAshleyHidingZoneCorner0_Z = ReturnAshleyHidingZoneCorner0_Z;
            Methods.SetAshleyHidingZoneCorner0_Z = SetAshleyHidingZoneCorner0_Z;
            Methods.ReturnAshleyHidingZoneCorner1_X = ReturnAshleyHidingZoneCorner1_X;
            Methods.SetAshleyHidingZoneCorner1_X = SetAshleyHidingZoneCorner1_X;
            Methods.ReturnAshleyHidingZoneCorner1_Z = ReturnAshleyHidingZoneCorner1_Z;
            Methods.SetAshleyHidingZoneCorner1_Z = SetAshleyHidingZoneCorner1_Z;
            Methods.ReturnAshleyHidingZoneCorner2_X = ReturnAshleyHidingZoneCorner2_X;
            Methods.SetAshleyHidingZoneCorner2_X = SetAshleyHidingZoneCorner2_X;
            Methods.ReturnAshleyHidingZoneCorner2_Z = ReturnAshleyHidingZoneCorner2_Z;
            Methods.SetAshleyHidingZoneCorner2_Z = SetAshleyHidingZoneCorner2_Z;
            Methods.ReturnAshleyHidingZoneCorner3_X = ReturnAshleyHidingZoneCorner3_X;
            Methods.SetAshleyHidingZoneCorner3_X = SetAshleyHidingZoneCorner3_X;
            Methods.ReturnAshleyHidingZoneCorner3_Z = ReturnAshleyHidingZoneCorner3_Z;
            Methods.SetAshleyHidingZoneCorner3_Z = SetAshleyHidingZoneCorner3_Z;
            Methods.ReturnAshleyHidingPointX_Hex = ReturnAshleyHidingPointX_Hex;
            Methods.SetAshleyHidingPointX_Hex = SetAshleyHidingPointX_Hex;
            Methods.ReturnAshleyHidingPointY_Hex = ReturnAshleyHidingPointY_Hex;
            Methods.SetAshleyHidingPointY_Hex = SetAshleyHidingPointY_Hex;
            Methods.ReturnAshleyHidingPointZ_Hex = ReturnAshleyHidingPointZ_Hex;
            Methods.SetAshleyHidingPointZ_Hex = SetAshleyHidingPointZ_Hex;
            Methods.ReturnAshleyHidingZoneCorner0_X_Hex = ReturnAshleyHidingZoneCorner0_X_Hex;
            Methods.SetAshleyHidingZoneCorner0_X_Hex = SetAshleyHidingZoneCorner0_X_Hex;
            Methods.ReturnAshleyHidingZoneCorner0_Z_Hex = ReturnAshleyHidingZoneCorner0_Z_Hex;
            Methods.SetAshleyHidingZoneCorner0_Z_Hex = SetAshleyHidingZoneCorner0_Z_Hex;
            Methods.ReturnAshleyHidingZoneCorner1_X_Hex = ReturnAshleyHidingZoneCorner1_X_Hex;
            Methods.SetAshleyHidingZoneCorner1_X_Hex = SetAshleyHidingZoneCorner1_X_Hex;
            Methods.ReturnAshleyHidingZoneCorner1_Z_Hex = ReturnAshleyHidingZoneCorner1_Z_Hex;
            Methods.SetAshleyHidingZoneCorner1_Z_Hex = SetAshleyHidingZoneCorner1_Z_Hex;
            Methods.ReturnAshleyHidingZoneCorner2_X_Hex = ReturnAshleyHidingZoneCorner2_X_Hex;
            Methods.SetAshleyHidingZoneCorner2_X_Hex = SetAshleyHidingZoneCorner2_X_Hex;
            Methods.ReturnAshleyHidingZoneCorner2_Z_Hex = ReturnAshleyHidingZoneCorner2_Z_Hex;
            Methods.SetAshleyHidingZoneCorner2_Z_Hex = SetAshleyHidingZoneCorner2_Z_Hex;
            Methods.ReturnAshleyHidingZoneCorner3_X_Hex = ReturnAshleyHidingZoneCorner3_X_Hex;
            Methods.SetAshleyHidingZoneCorner3_X_Hex = SetAshleyHidingZoneCorner3_X_Hex;
            Methods.ReturnAshleyHidingZoneCorner3_Z_Hex = ReturnAshleyHidingZoneCorner3_Z_Hex;
            Methods.SetAshleyHidingZoneCorner3_Z_Hex = SetAshleyHidingZoneCorner3_Z_Hex;
            Methods.ReturnUnknown_SM = ReturnUnknown_SM;
            Methods.SetUnknown_SM = SetUnknown_SM;
            Methods.ReturnUnknown_SN = ReturnUnknown_SN;
            Methods.SetUnknown_SN = SetUnknown_SN;
            Methods.ReturnUnknown_SP = ReturnUnknown_SP;
            Methods.SetUnknown_SP = SetUnknown_SP;
            Methods.ReturnUnknown_SQ = ReturnUnknown_SQ;
            Methods.SetUnknown_SQ = SetUnknown_SQ;
            Methods.ReturnUnknown_SR = ReturnUnknown_SR;
            Methods.SetUnknown_SR = SetUnknown_SR;
            Methods.ReturnUnknown_SS = ReturnUnknown_SS;
            Methods.SetUnknown_SS = SetUnknown_SS;
            #endregion

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
            MoveMethods.GetTriggerZoneCategory = GetSpecialZoneCategory;

            MethodsForGL = new SpecialMethodsForGL();
            MethodsForGL.GetSpecialType = GetSpecialType;
            MethodsForGL.GetItemPosition = GetItemPosition;
            MethodsForGL.GetItemRotation = GetItemRotation;
            MethodsForGL.GetItemModelID = ReturnItemNumber;
            MethodsForGL.GetItemTrigggerRadius = GetItemTriggerRadiusToRender;
           
            ExtrasMethodsForGL = new ExtrasMethodsForGL();
            ExtrasMethodsForGL.GetSpecialType = GetSpecialType;
            ExtrasMethodsForGL.GetFirstPosition = GetFirstPosition;
            ExtrasMethodsForGL.GetWarpRotation = GetWarpRotation;
            ExtrasMethodsForGL.GetLocationAndLadderRotation = GetLocationAndLadderRotation;
            ExtrasMethodsForGL.GetLadderStepCount = ReturnLadderStepCount;
            ExtrasMethodsForGL.GetAshleyPoint = GetAshleyPoint;
            ExtrasMethodsForGL.GetAshleyHidingZoneCorner = GetAshleyHidingZoneCorner;
            ExtrasMethodsForGL.GetAshleyHidingZoneCornerMatrix4 = GetAshleyHidingZoneCornerMatrix4;
            ExtrasMethodsForGL.GetGrappleGunEndPosition = GetGrappleGunEndPosition;
            ExtrasMethodsForGL.GetGrappleGunThirdPosition = GetGrappleGunThirdPosition;
            ExtrasMethodsForGL.GetGrappleGunFacingAngleRotation = GetGrappleGunFacingAngleRotation;
            ExtrasMethodsForGL.GetGrappleGunParameter3 = ReturnGrappleGunParameter3;

            ChangeAmountMethods = new NodeChangeAmountMethods();
            ChangeAmountMethods.AddNewLineID = AddNewLineID;
            ChangeAmountMethods.RemoveLineID = RemoveLineID;

            SetTriggerZoneMethods(Methods);
            SetTriggerZoneMethodsForGL(MethodsForGL);
        }


        /// <summary>
        /// Classe com os metodos que serão passados para classe Specialproperty;
        /// </summary>
        public SpecialMethods Methods { get; }

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
        public SpecialMethodsForGL MethodsForGL { get; }

        /// <summary>
        /// outros metodos para rederizar os objetos faltantes
        /// </summary>
        public ExtrasMethodsForGL ExtrasMethodsForGL { get; }

        /// <summary>
        /// Classe com os metodos responsaveis para adicinar e remover linhas/lines
        /// </summary>
        public NodeChangeAmountMethods ChangeAmountMethods { get; }


        #region FixOffset

        private int FixOffset(int OffsetUHD)
        {
            int newOffset = OffsetUHD;
            if (GetRe4Version == Re4Version.V2007PS2 && OffsetUHD >= 0x5C)
            {
                newOffset += 0x4;
            }
            return newOffset;
        }

        //Item
        private int ItemFixOffset(int OffsetUHD)
        {
            int newOffset = OffsetUHD;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                if (OffsetUHD >= 0x5C)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x78)
                {
                    newOffset += 0x8; // são so 8, pois ja são somados 4 bytes acima
                }
            }
            return newOffset;
        }

        //LadderClimbUp
        private int LadderFixOffset(int OffsetUHD)
        {
            int newOffset = OffsetUHD;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                if (OffsetUHD >= 0x5C)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x68) // move os 4 bytes do descolamento de StartPoint.W
                {
                    newOffset += 0x4;
                }
                /*if (OffsetUHD >= 0x78) // aqui fica na possição original
                {
                    newOffset -= 0x4;
                }*/
            }
            return newOffset;
        }

        //AshleyHiding
        private int AshleyFixOffset(int OffsetUHD)
        {
            int newOffset = OffsetUHD;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                if (OffsetUHD >= 0x5C)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x8C)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x5C && OffsetUHD <= 0x7F)
                {
                    newOffset += 0x10;
                }           
                if (OffsetUHD >= 0x80 && OffsetUHD <= 0x8B)
                {
                    newOffset = OffsetUHD -= 0x20;
                }
            }
            return newOffset;
        }

        //GrappleGun
        private int GrappleGunFixOffset(int OffsetUHD)
        {
            int newOffset = OffsetUHD;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                if (OffsetUHD >= 0x5C)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x68)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x74)
                {
                    newOffset += 0x4;
                }
                if (OffsetUHD >= 0x80)
                {
                    newOffset += 0x4;
                }
            }
            return newOffset;
        }

        #endregion


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
                string r = "[" + ReturnSpecialIndex(ID).ToString("X2") + "] ";

                switch (GetSpecialType(ID))
                {
                    case SpecialType.T03_Items:
                        r += "[" + ReturnSecundIndex(ID).ToString("X4") + "] 0x";
                        ushort itemId = ReturnItemNumber(ID);
                        r += itemId.ToString("X4") + ": ";
                        if (DataBase.ItemsIDs.List.ContainsKey(itemId))
                        {
                            r += DataBase.ItemsIDs.List[itemId].Name;
                        }
                        else
                        {
                            r += Lang.GetAttributeText(aLang.ListBoxUnknownItem);
                        }
                        ushort amount = ReturnItemAmount(ID);

                        r += " (Amount: ";

                        if (amount == 0)
                        {
                            r += "Default";
                        }
                        else
                        {
                            r += amount;
                        }

                        r += ") {Aura Type: 0x";

                        ushort aura = ReturnItemAuraType(ID);
                        r += aura.ToString("X2") + "}";

                        // continuar
                        break;
                    case SpecialType.T00_GeneralPurpose:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType00_GeneralPurpose);
                        break;
                    case SpecialType.T01_WarpDoor:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType01_WarpDoor) + " [R" + ReturnDestinationRoom(ID).ToString("X3") + "]";
                        byte lockedDoorType = ReturnLockedDoorType(ID);
                        if (lockedDoorType == 0x01)
                        {
                            r += " {Is Locked Door (0x" + ReturnLockedDoorIndex(ID).ToString("X2") + ")}";
                        }
                        else if (lockedDoorType == 0x02)
                        {
                            r += " {Is UnLocked Door (0x" + ReturnLockedDoorIndex(ID).ToString("X2") + ")}";
                        }
                        break;
                    case SpecialType.T02_CutSceneEvents:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType02_CutSceneEvents);
                        break;
                    case SpecialType.T04_GroupedEnemyTrigger:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType04_GroupedEnemyTrigger) + " [" + ReturnUnknown_HK_Ushort(ID).ToString("X4") + "]";
                        break;
                    case SpecialType.T05_Message:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType05_Message)
                            + " [" + ReturnUnknown_HK_Ushort(ID).ToString("X4") + "] [" +
                            ReturnMessageCutSceneID(ID).ToString("X4") + "] [" +
                            ReturnMessageID(ID).ToString("X4") + "]";
                        break;
                    case SpecialType.T08_TypeWriter:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType08_TypeWriter);
                        break;
                    case SpecialType.T0A_DamagesThePlayer:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType0A_DamagesThePlayer) +
                          "; Activation Type: 0x" + ReturnActivationType(ID).ToString("X2") +
                          " Damage Type: 0x" + ReturnDamageType(ID).ToString("X2") +
                          " Blocking Type: 0x" + ReturnBlockingType(ID).ToString("X2") +
                          " Damage Amount: " + ReturnDamageAmount(ID);
                        break;
                    case SpecialType.T0B_FalseCollision:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType0B_FalseCollision);
                        break;
                    case SpecialType.T0D_FieldInfo:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType0D_FieldInfo);
                        break;
                    case SpecialType.T0E_Crouch:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType0E_Crouch);
                        break;
                    case SpecialType.T10_FixedLadderClimbUp:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp) +
                            " (Step Count: " + ReturnLadderStepCount(ID)
                            + ") Parameters: [" + ReturnLadderParameter0(ID).ToString("X2") + "] [" +
                            ReturnLadderParameter1(ID).ToString("X2") + "] [" +
                            ReturnLadderParameter2(ID).ToString("X2") + "] [" +
                            ReturnLadderParameter3(ID).ToString("X2") + "]";
                        break;
                    case SpecialType.T11_ItemDependentEvents:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType11_ItemDependentEvents) + "; 0x";
                        ushort needitemNumber = ReturnNeededItemNumber(ID);
                        r += needitemNumber.ToString("X4") + ": ";
                        if (DataBase.ItemsIDs.List.ContainsKey(needitemNumber))
                        {
                            r += DataBase.ItemsIDs.List[needitemNumber].Name;
                        }
                        else
                        {
                            r += Lang.GetAttributeText(aLang.ListBoxUnknownItem);
                        }
                        break;
                    case SpecialType.T12_AshleyHideCommand:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType12_AshleyHideCommand) + " [" + ReturnUnknown_SP(ID).ToString("X2") + "]";
                        break;
                    case SpecialType.T13_LocalTeleportation:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation);
                        break;
                    case SpecialType.T14_UsedForElevators:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType14_UsedForElevators);
                        break;
                    case SpecialType.T15_AdaGrappleGun:
                        r += "0x" + Lang.GetAttributeText(aLang.SpecialType15_AdaGrappleGun)
                             + "; Parameters: [" + ReturnGrappleGunParameter0(ID).ToString("X2") + "] [" +
                            ReturnGrappleGunParameter1(ID).ToString("X2") + "] [" +
                            ReturnGrappleGunParameter2(ID).ToString("X2") + "] [" +
                            ReturnGrappleGunParameter3(ID).ToString("X2") + "]"; ;
                        break;
                    case SpecialType.T06_Unused:
                    case SpecialType.T07_Unused:
                    case SpecialType.T09_Unused:
                    case SpecialType.T0C_Unused:
                    case SpecialType.T0F_Unused:
                    case SpecialType.UnspecifiedType:
                    default:
                        r += "0x" + ReturnSpecialType(ID).ToString("X2") + ": " + Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType);
                        break;
                }

                if (GetRefInteractionType(ID) == RefInteractionType.Enemy)
                {
                    r += " - Associated Enemy ID: 0x" + ReturnRefInteractionIndex(ID).ToString("X2");
                }
                else if (GetRefInteractionType(ID) == RefInteractionType.EtcModel)
                {
                    r += " - Associated Ets ID: 0x" + ReturnRefInteractionIndex(ID).ToString("X2");
                }

                return r;
            }
            else 
            {
                return "Special Error Internal Line ID " + ID;
            }
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderItemsITA && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                return Globals.NodeColorHided;
            }
            else if (!Globals.RenderEventsAEV && GetSpecialFileFormat == SpecialFileFormat.AEV)
            {
                return Globals.NodeColorHided;
            }
            else if (!Globals.RenderSpecialTriggerZone && Lines.ContainsKey(ID) && GetSpecialType(ID) != SpecialType.T03_Items)
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

            ushort newSecundIndex = 0;
            if (SecundIndexList.Count > 0)
            {
                newSecundIndex = SecundIndexList.Max(o => o.Key);
            }
            if (newSecundIndex != ushort.MaxValue)
            {
                newSecundIndex++;
            }
            byte[] newSecundIndex_Bytes = BitConverter.GetBytes(newSecundIndex);

            byte[] content = null;
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                if (GetSpecialFileFormat == SpecialFileFormat.AEV)
                {
                    content = new byte[160];
                }
                else if (GetSpecialFileFormat == SpecialFileFormat.ITA)
                {
                    content = new byte[176];
                }

                content[0x35] = initType; //type

                if (initType == 0x03)
                {
                    content[0x6C] = 0x3F;
                    content[0x6D] = 0x80;

                    content[0x7C] = 0x3F;
                    content[0x7D] = 0x80;

                    content[0x8A] = newSecundIndex_Bytes[0];
                    content[0x8B] = newSecundIndex_Bytes[1];
                    AddNewSecundIndexList(newID, newSecundIndex);
                }
                else if (initType == 0x10)
                {
                    content[0x6C] = 0x3F;
                    content[0x6D] = 0x80;

                    content[0x74] = 0x02; // tamanho
                    content[0x76] = 0x01; // fixo
                }
                
            }
            else if (GetRe4Version == Re4Version.UHD)
            {
                content = new byte[156];

                content[0x35] = initType; //type

                if (initType == 0x03)
                {
                    content[0x7E] = newSecundIndex_Bytes[0];
                    content[0x7F] = newSecundIndex_Bytes[1];
                    AddNewSecundIndexList(newID, newSecundIndex);
                }
                else if (initType == 0x10)
                {
                    content[0x6C] = 0x02; // tamanho
                    content[0x6E] = 0x01; // fixo
                }
            }

            if (content != null)
            {
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

                //U_KG
                content[0x34] = 0x03; // fixed 0x03
                
                
                content[0x36] = newFirstIndex; // index

                //U_KJ
                content[0x37] = 0x01;
                //U_LI
                content[0x38] = 0x02;
                //U_LO
                content[0x39] = 0x01;

                //U_NO
                content[0x44] = 0x02;

                //U_NU
                content[0x49] = 0x2D;
            }

            AddNewFirstIndexList(newID, newFirstIndex);
            Lines.Add(newID, content);
            return newID;
        }

        private void RemoveLineID(ushort ID)
        {
            DataBase.Extras.RefInteractionTypeListRemove(GetRefInteractionType(ID), ReturnRefInteractionIndex(ID), GetSpecialFileFormat, ID);
            RemoveFirstIndexList(ID, ReturnSpecialIndex(ID));
            if (GetSpecialType(ID) == SpecialType.T03_Items)
            {
                RemoveSecundIndexList(ID, ReturnSecundIndex(ID));
            }
            Lines.Remove(ID);
        }


        #endregion


        #region move metodos

        private Vector3 GetObjPostion_ToCamera(ushort ID)
        {
            Vector3 position = Vector3.Zero;
            if (GetSpecialType(ID) == SpecialType.T03_Items)
            {
                position = GetItemPosition(ID);
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
            if (GetSpecialType(ID) == SpecialType.T03_Items)
            {
                AngleY = ReturnItemAngleY(ID);
                if (float.IsNaN(AngleY) || float.IsInfinity(AngleY)) { AngleY = 0; }
            }
            return AngleY;
        }


        private Vector3[] GetObjPostion_ToMove_General(ushort ID) 
        {
            Vector3[] pos = new Vector3[7];
            if (GetSpecialType(ID) == SpecialType.T03_Items)
            {
                pos[0] = new Vector3(ReturnObjPositionX(ID), ReturnObjPositionY(ID), ReturnObjPositionZ(ID));
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
                if (GetSpecialType(ID) == SpecialType.T03_Items)
                {
                    SetObjPositionX(ID, value[0].X);
                    SetObjPositionY(ID, value[0].Y);
                    SetObjPositionZ(ID, value[0].Z);
                }

                SetTriggerZonePos_ToMove_General(ID, value);
            }
        
        }


        private Vector3[] GetObjRotationAngles_ToMove(ushort ID) 
        {
            Vector3[] v = new Vector3[1];
            if (GetSpecialType(ID) == SpecialType.T03_Items)
            {
                float AngleZ = 0;
                if (!(GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.AEV))
                {
                    AngleZ = ReturnItemAngleZ(ID);
                }
                v[0] = new Vector3(ReturnItemAngleX(ID), ReturnItemAngleY(ID), AngleZ);
            }
            else
            {
                v[0] = Vector3.Zero;
            }
            Utils.ToMoveCheckLimits(ref v);
            return v;
        }

        private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value)
        {
            if (value != null && value.Length >= 1)
            {
                if (GetSpecialType(ID) == SpecialType.T03_Items)
                {
                    SetItemAngleX(ID, value[0].X);
                    SetItemAngleY(ID, value[0].Y);
                    if (!(GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.AEV))
                    {
                        SetItemAngleZ(ID, value[0].Z);
                    }
                }
            }
        }

        #endregion


        #region metodos das propriedades // geral

        private Re4Version ReturnRe4Version()
        {
            return GetRe4Version;
        }

        private SpecialFileFormat GetSpecialFileFormatMethod()
        {
            return GetSpecialFileFormat;
        }

        private byte[] ReturnLine(ushort ID)
        {
            return (byte[])Lines[ID].Clone();
        }

        private void SetLine(ushort ID, byte[] value)
        {
            byte oldType = ReturnSpecialType(ID);
            byte OldIndex = ReturnSpecialIndex(ID);
            ushort oldsecondIndex = ReturnSecundIndex(ID);

            RefInteractionType oldRefInteractionType = GetRefInteractionType(ID);
            ushort oldRefInteractionIndex = ReturnRefInteractionIndex(ID);

            //
            value.CopyTo(Lines[ID], 0);
            //

            if (oldType == 0x03)
            {
                RemoveSecundIndexList(ID, oldsecondIndex);
            }
            if (ReturnSpecialType(ID) == 0x03)
            {
                AddNewSecundIndexList(ID, ReturnSecundIndex(ID));
            }

            UpdateFirstIndexList(ID, OldIndex, ReturnSpecialIndex(ID));

            //
            DataBase.Extras.RefInteractionTypeListRemove(oldRefInteractionType, oldRefInteractionIndex, GetSpecialFileFormat, ID);
            DataBase.Extras.RefInteractionTypeListAdd(GetRefInteractionType(ID), ReturnRefInteractionIndex(ID), GetSpecialFileFormat, ID);

        }

        private SpecialType GetSpecialType(ushort ID)
        {
            byte Type = Lines[ID][0x35];
            if (Type < 0x16)
            {
                return (SpecialType)Type;
            }
            return SpecialType.UnspecifiedType;
        }

        private byte ReturnSpecialType(ushort ID)
        {
            return Lines[ID][0x35];
        }

        private void SetSpecialType(ushort ID, byte value)
        {
            byte oldType = Lines[ID][0x35];
            Lines[ID][0x35] = value;

            if (oldType == 0x03)
            {
                RemoveSecundIndexList(ID, ReturnSecundIndex(ID));
            }
            if (value == 0x03)
            {
                AddNewSecundIndexList(ID, ReturnSecundIndex(ID));
            }
        }

        private byte ReturnSpecialIndex(ushort ID)
        {
            return Lines[ID][0x36];
        }

        private void SetSpecialIndex(ushort ID, byte value)
        {
            byte OldIndex = ReturnSpecialIndex(ID);
            Lines[ID][0x36] = value;
            UpdateFirstIndexList(ID, OldIndex, value);
        }



        #endregion


        #region metodos das propriedades // parte 1 geral

        private byte[] ReturnUnknown_GG(ushort ID)
        {
            byte[] b = new byte[4];
            b[0] = Lines[ID][0x00];
            b[1] = Lines[ID][0x01];
            b[2] = Lines[ID][0x02];
            b[3] = Lines[ID][0x03];
            return b;
        }

        private void SetUnknown_GG(ushort ID, byte[] value)
        {
            Lines[ID][0x00] = value[0];
            Lines[ID][0x01] = value[1];
            Lines[ID][0x02] = value[2];
            Lines[ID][0x03] = value[3];
        }

        #endregion


        #region Geral depois de triggerZone

        // depois de triggerZone

        private byte ReturnUnknown_KG(ushort ID)
        {
            return Lines[ID][0x34];
        }

        private void SetUnknown_KG(ushort ID, byte value)
        {
            Lines[ID][0x34] = value;
        }


        private byte ReturnUnknown_KJ(ushort ID)
        {
            return Lines[ID][0x37];
        }

        private void SetUnknown_KJ(ushort ID, byte value)
        {
            Lines[ID][0x37] = value;
        }

        private byte ReturnUnknown_LI(ushort ID)
        {
            return Lines[ID][0x38];
        }

        private void SetUnknown_LI(ushort ID, byte value)
        {
            Lines[ID][0x38] = value;
        }

        private byte ReturnUnknown_LO(ushort ID)
        {
            return Lines[ID][0x39];
        }

        private void SetUnknown_LO(ushort ID, byte value)
        {
            Lines[ID][0x39] = value;
        }

        private byte ReturnUnknown_LU(ushort ID)
        {
            return Lines[ID][0x3A];
        }

        private void SetUnknown_LU(ushort ID, byte value)
        {
            Lines[ID][0x3A] = value;
        }

        private byte ReturnUnknown_LH(ushort ID)
        {
            return Lines[ID][0x3B];
        }

        private void SetUnknown_LH(ushort ID, byte value)
        {
            Lines[ID][0x3B] = value;
        }

        private byte[] ReturnUnknown_MI(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][0x3C];
            b[1] = Lines[ID][0x3D];
            return b;
        }

        private void SetUnknown_MI(ushort ID, byte[] value)
        {
            Lines[ID][0x3D] = value[0];
            Lines[ID][0x3C] = value[1];
        }


        private byte[] ReturnUnknown_MO(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][0x3E];
            b[1] = Lines[ID][0x3F];
            return b;
        }

        private void SetUnknown_MO(ushort ID, byte[] value)
        {
            Lines[ID][0x3E] = value[0];
            Lines[ID][0x3F] = value[1];
        }

        private byte[] ReturnUnknown_MU(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][0x40];
            b[1] = Lines[ID][0x41];
            return b;
        }

        private void SetUnknown_MU(ushort ID, byte[] value)
        {
            Lines[ID][0x40] = value[0];
            Lines[ID][0x41] = value[1];
        }

        private byte[] ReturnUnknown_NI(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][0x42];
            b[1] = Lines[ID][0x43];
            return b;
        }

        private void SetUnknown_NI(ushort ID, byte[] value)
        {
            Lines[ID][0x42] = value[0];
            Lines[ID][0x43] = value[1];
        }

        private byte ReturnUnknown_NO(ushort ID)
        {
            return Lines[ID][0x44];
        }

        private void SetUnknown_NO(ushort ID, byte value)
        {
            Lines[ID][0x44] = value;
        }

        private byte ReturnUnknown_NS(ushort ID)
        {
            return Lines[ID][0x45];
        }

        private void SetUnknown_NS(ushort ID, byte value)
        {
            Lines[ID][0x45] = value;
        }

        private byte ReturnRefInteractionType(ushort ID)
        {
            return Lines[ID][0x46];
        }

        private RefInteractionType GetRefInteractionType(ushort ID) 
        {
            byte refInteractionType = Lines[ID][0x46];
            if (refInteractionType < 3)
            {
                return (RefInteractionType)refInteractionType;
            }
            return RefInteractionType.AnotherValue;
        }

        private void SetRefInteractionType(ushort ID, byte value)
        {
            RefInteractionType oldvalue = GetRefInteractionType(ID);
            Lines[ID][0x46] = value;
            RefInteractionType newvalue = GetRefInteractionType(ID);
            DataBase.Extras.UpdateRefInteractionTypeList(GetSpecialFileFormat, ID, oldvalue, newvalue, ReturnRefInteractionIndex(ID));
        }

        private byte ReturnRefInteractionIndex(ushort ID)
        {
            return Lines[ID][0x47];
        }

        private void SetRefInteractionIndex(ushort ID, byte value)
        {
            RefInteractionType refInteractionType = GetRefInteractionType(ID);
            ushort oldRefInteractionIndex = ReturnRefInteractionIndex(ID);
            Lines[ID][0x47] = value;
            DataBase.Extras.UpdateRefInteractionTypeList(GetSpecialFileFormat, ID, refInteractionType, oldRefInteractionIndex, value);
        }


        private byte ReturnUnknown_NT(ushort ID)
        {
            return Lines[ID][0x48];
        }

        private void SetUnknown_NT(ushort ID, byte value)
        {
            Lines[ID][0x48] = value;
        }


        private byte ReturnUnknown_NU(ushort ID)
        {
            return Lines[ID][0x49];
        }

        private void SetUnknown_NU(ushort ID, byte value)
        {
            Lines[ID][0x49] = value;
        }


        private byte ReturnPromptMessage(ushort ID)
        {
            return Lines[ID][0x4A];
        }

        private void SetPromptMessage(ushort ID, byte value)
        {
            Lines[ID][0x4A] = value;
        }


        private byte ReturnUnknown_PI(ushort ID)
        {
            return Lines[ID][0x4B];
        }

        private void SetUnknown_PI(ushort ID, byte value)
        {
            Lines[ID][0x4B] = value;
        }


        private byte[] ReturnUnknown_PO(ushort ID)
        {
            byte[] b = new byte[4];
            b[0] = Lines[ID][0x4C];
            b[1] = Lines[ID][0x4D];
            b[2] = Lines[ID][0x4E];
            b[3] = Lines[ID][0x4F];
            return b;
        }

        private void SetUnknown_PO(ushort ID, byte[] value)
        {
            Lines[ID][0x4C] = value[0];
            Lines[ID][0x4D] = value[1];
            Lines[ID][0x4E] = value[2];
            Lines[ID][0x4F] = value[3];
        }

        private byte[] ReturnUnknown_PU(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][0x50];
            b[1] = Lines[ID][0x51];
            return b;
        }

        private void SetUnknown_PU(ushort ID, byte[] value)
        {
            Lines[ID][0x50] = value[0];
            Lines[ID][0x51] = value[1];
        }

        private byte ReturnUnknown_PK(ushort ID)
        {
            return Lines[ID][0x52];
        }

        private void SetUnknown_PK(ushort ID, byte value)
        {
            Lines[ID][0x52] = value;
        }

        private byte ReturnMessageColor(ushort ID)
        {
            return Lines[ID][0x53];
        }

        private void SetMessageColor(ushort ID, byte value)
        {
            Lines[ID][0x53] = value;
        }


        private byte[] ReturnUnknown_QI(ushort ID)
        {
            byte[] b = new byte[4];
            b[0] = Lines[ID][0x54];
            b[1] = Lines[ID][0x55];
            b[2] = Lines[ID][0x56];
            b[3] = Lines[ID][0x57];
            return b;
        }

        private void SetUnknown_QI(ushort ID, byte[] value)
        {
            Lines[ID][0x54] = value[0];
            Lines[ID][0x55] = value[1];
            Lines[ID][0x56] = value[2];
            Lines[ID][0x57] = value[3];
        }

        private byte[] ReturnUnknown_QO(ushort ID)
        {
            byte[] b = new byte[4];
            b[0] = Lines[ID][0x58];
            b[1] = Lines[ID][0x59];
            b[2] = Lines[ID][0x5A];
            b[3] = Lines[ID][0x5B];
            return b;
        }

        private void SetUnknown_QO(ushort ID, byte[] value)
        {
            Lines[ID][0x58] = value[0];
            Lines[ID][0x59] = value[1];
            Lines[ID][0x5A] = value[2];
            Lines[ID][0x5B] = value[3];
        }

        // Somente para o Classic
        private byte[] ReturnUnknown_QU(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x5C];
                b[1] = Lines[ID][0x5D];
                b[2] = Lines[ID][0x5E];
                b[3] = Lines[ID][0x5F];
                return b;
            }
            return new byte[4];
        }

        // Somente para o Classic
        private void SetUnknown_QU(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x5C] = value[0];
                Lines[ID][0x5D] = value[1];
                Lines[ID][0x5E] = value[2];
                Lines[ID][0x5F] = value[3];
            }
        }


        #endregion


        // special type 0x03 e classic ITA Propertys
        #region Itens Type 0x3 // Floats

        private uint ReturnObjPositionX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x5C));
        }

        private uint ReturnObjPositionY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x60));
        }

        private uint ReturnObjPositionZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x64));
        }

        private uint ReturnUnknown_RI_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x6C));
        }

        private uint ReturnUnknown_RI_Y_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x70));
        }

        private uint ReturnUnknown_RI_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x74));
        }

        private uint ReturnItemTriggerRadius_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], ItemFixOffset(0x88));
        }

        private uint ReturnItemAngleX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], ItemFixOffset(0x8C));
        }

        private uint ReturnItemAngleY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], ItemFixOffset(0x90));
        }

        private uint ReturnItemAngleZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], ItemFixOffset(0x94));
        }

        private void SetObjPositionX_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x5C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetObjPositionY_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x60);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetObjPositionZ_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x64);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetUnknown_RI_X_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x6C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetUnknown_RI_Y_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x70);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetUnknown_RI_Z_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x74);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemTriggerRadius_Hex(ushort ID, uint value)
        {
            int offset = ItemFixOffset(0x88);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemAngleX_Hex(ushort ID, uint value)
        {
            int offset = ItemFixOffset(0x8C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemAngleY_Hex(ushort ID, uint value)
        {
            int offset = ItemFixOffset(0x90);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemAngleZ_Hex(ushort ID, uint value)
        {
            int offset = ItemFixOffset(0x94);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        //float

        private float ReturnObjPositionX(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x5C));
        }

        private float ReturnObjPositionY(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x60));
        }

        private float ReturnObjPositionZ(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x64));
        }


        private float ReturnUnknown_RI_X(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x6C));
        }

        private float ReturnUnknown_RI_Y(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x70));
        }

        private float ReturnUnknown_RI_Z(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x74));
        }

        private float ReturnItemTriggerRadius(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], ItemFixOffset(0x88));
        }

        private float ReturnItemAngleX(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], ItemFixOffset(0x8C));
        }

        private float ReturnItemAngleY(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], ItemFixOffset(0x90));
        }

        private float ReturnItemAngleZ(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], ItemFixOffset(0x94));
        }

        private void SetObjPositionX(ushort ID, float value)
        {
            int offset = FixOffset(0x5C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetObjPositionY(ushort ID, float value)
        {
            int offset = FixOffset(0x60);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetObjPositionZ(ushort ID, float value)
        {
            int offset = FixOffset(0x64);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetUnknown_RI_X(ushort ID, float value)
        {
            int offset = FixOffset(0x6C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetUnknown_RI_Y(ushort ID, float value)
        {
            int offset = FixOffset(0x70);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetUnknown_RI_Z(ushort ID, float value)
        {
            int offset = FixOffset(0x74);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemTriggerRadius(ushort ID, float value)
        {
            int offset = ItemFixOffset(0x88);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemAngleX(ushort ID, float value)
        {
            int offset = ItemFixOffset(0x8C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemAngleY(ushort ID, float value)
        {
            int offset = ItemFixOffset(0x90);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetItemAngleZ(ushort ID, float value)
        {
            int offset = ItemFixOffset(0x94);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        #endregion


        #region itens Type 0x3 outros

        private byte[] ReturnObjPointW(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = ItemFixOffset(0x68);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        private void SetObjPointW(ushort ID, byte[] value)
        {
            int offset = ItemFixOffset(0x68);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }


        private byte[] ReturnUnknown_RI_W(ushort ID) // Somente para o Classic
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x7C];
                b[1] = Lines[ID][0x7D];
                b[2] = Lines[ID][0x7E];
                b[3] = Lines[ID][0x7F];
                return b;
            }
            return new byte[4];
        }

        private void SetUnknown_RI_W(ushort ID, byte[] value) // Somente para o Classic
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x7C] = value[0];
                Lines[ID][0x7D] = value[1];
                Lines[ID][0x7E] = value[2];
                Lines[ID][0x7F] = value[3];
            }
        }

        private byte[] ReturnUnknown_RO(ushort ID) // Somente para o Classic
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x80];
                b[1] = Lines[ID][0x81];
                b[2] = Lines[ID][0x82];
                b[3] = Lines[ID][0x83];
                return b;
            }
            return new byte[4];
        }

        private void SetUnknown_RO(ushort ID, byte[] value) // Somente para o Classic
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x80] = value[0];
                Lines[ID][0x81] = value[1];
                Lines[ID][0x82] = value[2];
                Lines[ID][0x83] = value[3];
            }
        }

        private ushort ReturnItemNumber(ushort ID)
        {
            return BitConverter.ToUInt16(Lines[ID], ItemFixOffset(0x78));
        }

        private void SetItemNumber(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][ItemFixOffset(0x78)] = b[0];
            Lines[ID][ItemFixOffset(0x79)] = b[1];
        }

        private byte[] ReturnUnknown_RU(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][ItemFixOffset(0x7A)];
            b[1] = Lines[ID][ItemFixOffset(0x7B)];
            return b;
        }

        private void SetUnknown_RU(ushort ID, byte[] value)
        {
            Lines[ID][ItemFixOffset(0x7A)] = value[0];
            Lines[ID][ItemFixOffset(0x7B)] = value[1];
        }

        private ushort ReturnItemAmount(ushort ID)
        {
            return BitConverter.ToUInt16(Lines[ID], ItemFixOffset(0x7C));
        }

        private void SetItemAmount(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][ItemFixOffset(0x7C)] = b[0];
            Lines[ID][ItemFixOffset(0x7D)] = b[1];
        }

        private ushort ReturnSecundIndex(ushort ID) // secundIndex
        {
            return BitConverter.ToUInt16(Lines[ID], ItemFixOffset(0x7E));
        }

        private void SetSecundIndex(ushort ID, ushort value) // secundIndex
        {
            ushort oldIndex = ReturnSecundIndex(ID);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][ItemFixOffset(0x7E)] = b[0];
            Lines[ID][ItemFixOffset(0x7F)] = b[1];
            UpdateSecundIndexList(ID, oldIndex, value);
        }

        private ushort ReturnItemAuraType(ushort ID)
        {
            return BitConverter.ToUInt16(Lines[ID], ItemFixOffset(0x80));
        }

        private void SetItemAuraType(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][ItemFixOffset(0x80)] = b[0];
            Lines[ID][ItemFixOffset(0x81)] = b[1];
        }

        private byte ReturnUnknown_QM(ushort ID) 
        {
            return Lines[ID][ItemFixOffset(0x82)];
        }

        private void SetUnknown_QM(ushort ID, byte value) 
        {
            Lines[ID][ItemFixOffset(0x82)] = value;
        }


        private byte ReturnUnknown_QL(ushort ID)
        {
            return Lines[ID][ItemFixOffset(0x83)];
        }

        private void SetUnknown_QL(ushort ID, byte value)
        {
            Lines[ID][ItemFixOffset(0x83)] = value;
        }

        private byte ReturnUnknown_QR(ushort ID)
        {
            return Lines[ID][ItemFixOffset(0x84)];
        }

        private void SetUnknown_QR(ushort ID, byte value)
        {
            Lines[ID][ItemFixOffset(0x84)] = value;
        }

        private byte ReturnUnknown_QH(ushort ID)
        {
            return Lines[ID][ItemFixOffset(0x85)];
        }

        private void SetUnknown_QH(ushort ID, byte value)
        {
            Lines[ID][ItemFixOffset(0x85)] = value;
        }


        private byte[] ReturnUnknown_QG(ushort ID)
        {
            byte[] b = new byte[2];
            b[0] = Lines[ID][ItemFixOffset(0x86)];
            b[1] = Lines[ID][ItemFixOffset(0x87)];
            return b;
        }

        private void SetUnknown_QG(ushort ID, byte[] value)
        {
            Lines[ID][ItemFixOffset(0x86)] = value[0];
            Lines[ID][ItemFixOffset(0x87)] = value[1];
        }


        private byte[] ReturnItemAngleW(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = ItemFixOffset(0x98);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        private void SetItemAngleW(ushort ID, byte[] value)
        {
            int offset = ItemFixOffset(0x98);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        #endregion


        #region somente para o Classic ITA

        // Somente para o Classic ITA
        private byte[] ReturnUnknown_VS(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0xA0];
                b[1] = Lines[ID][0xA1];
                b[2] = Lines[ID][0xA2];
                b[3] = Lines[ID][0xA3];
                return b;
            }
            return new byte[4];
        }

        // Somente para o Classic  ITA
        private void SetUnknown_VS(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                Lines[ID][0xA0] = value[0];
                Lines[ID][0xA1] = value[1];
                Lines[ID][0xA2] = value[2];
                Lines[ID][0xA3] = value[3];
            }
        }

        // Somente para o Classic ITA
        private byte[] ReturnUnknown_VT(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0xA4];
                b[1] = Lines[ID][0xA5];
                b[2] = Lines[ID][0xA6];
                b[3] = Lines[ID][0xA7];
                return b;
            }
            return new byte[4];
        }

        // Somente para o Classic  ITA
        private void SetUnknown_VT(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                Lines[ID][0xA4] = value[0];
                Lines[ID][0xA5] = value[1];
                Lines[ID][0xA6] = value[2];
                Lines[ID][0xA7] = value[3];
            }
        }

        // Somente para o Classic ITA
        private byte[] ReturnUnknown_VI(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0xA8];
                b[1] = Lines[ID][0xA9];
                b[2] = Lines[ID][0xAA];
                b[3] = Lines[ID][0xAB];
                return b;
            }
            return new byte[4];
        }

        // Somente para o Classic  ITA
        private void SetUnknown_VI(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                Lines[ID][0xA8] = value[0];
                Lines[ID][0xA9] = value[1];
                Lines[ID][0xAA] = value[2];
                Lines[ID][0xAB] = value[3];
            }
        }

        // Somente para o Classic ITA
        private byte[] ReturnUnknown_VO(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0xAC];
                b[1] = Lines[ID][0xAD];
                b[2] = Lines[ID][0xAE];
                b[3] = Lines[ID][0xAF];
                return b;
            }
            return new byte[4];
        }

        // Somente para o Classic  ITA
        private void SetUnknown_VO(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.ITA)
            {
                Lines[ID][0xAC] = value[0];
                Lines[ID][0xAD] = value[1];
                Lines[ID][0xAE] = value[2];
                Lines[ID][0xAF] = value[3];
            }
        }

        #endregion

        // outros special types

        #region Geral Specials / Unknown Special Type

        private byte[] ReturnUnknown_HH(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x5C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_HH(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x5C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_HK(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x5E);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_HK(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x5E);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_HL(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x60);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_HL(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x60);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_HM(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x62);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_HM(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x62);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_HN(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x64);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_HN(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x64);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_HR(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x66);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_HR(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x66);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_RH(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x68);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RH(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x68);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_RJ(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x6A);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RJ(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x6A);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_RK(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x6C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RK(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x6C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_RL(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x6E);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RL(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x6E);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_RM(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x70);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RM(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x70);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }


        private byte[] ReturnUnknown_RN(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x72);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RN(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x72);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_RP(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x74);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RP(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x74);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_RQ(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = FixOffset(0x76);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_RQ(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x76);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        private byte[] ReturnUnknown_TG(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x78);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TG(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x78);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TH(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x7C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TH(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x7C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TJ(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x80);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TJ(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x80);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TK(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x84);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TK(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x84);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TL(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x88);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TL(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x88);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TM(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x8C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TM(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x8C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TN(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x90);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TN(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x90);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TP(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x94);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TP(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x94);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        private byte[] ReturnUnknown_TQ(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = FixOffset(0x98);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }
        private void SetUnknown_TQ(ushort ID, byte[] value)
        {
            int offset = FixOffset(0x98);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }

        #endregion


        // referente aos types

        #region TYPE 0x04, 0x05, 0x0A and 0x11

        private ushort ReturnNeededItemNumber(ushort ID) // U_HH, type 0x11
        {
            return BitConverter.ToUInt16(Lines[ID], FixOffset(0x5C));
        }

        private void SetNeededItemNumber(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][FixOffset(0x5C)] = b[0];
            Lines[ID][FixOffset(0x5D)] = b[1];
        }

        private ushort ReturnUnknown_HK_Ushort(ushort ID) // EnemyGroup, RoomMessage
        {
            return BitConverter.ToUInt16(Lines[ID], FixOffset(0x5E));
        }

        private void SetUnknown_HK_Ushort(ushort ID, ushort value) // EnemyGroup, RoomMessage
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][FixOffset(0x5E)] = b[0];
            Lines[ID][FixOffset(0x5F)] = b[1];
        }

        private ushort ReturnMessageCutSceneID(ushort ID) // U_HL, type 0x05
        {
            return BitConverter.ToUInt16(Lines[ID], FixOffset(0x60));
        }

        private void SetMessageCutSceneID(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][FixOffset(0x60)] = b[0];
            Lines[ID][FixOffset(0x61)] = b[1];
        }

        private ushort ReturnMessageID(ushort ID) // U_HM, type 0x05
        {
            return BitConverter.ToUInt16(Lines[ID], FixOffset(0x62));
        }

        private void SetMessageID(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][FixOffset(0x62)] = b[0];
            Lines[ID][FixOffset(0x63)] = b[1];
        }

        private ushort ReturnDamageAmount(ushort ID) // U_HN, type 0x0A
        {
            return BitConverter.ToUInt16(Lines[ID], FixOffset(0x64));
        }

        private void SetDamageAmount(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][FixOffset(0x64)] = b[0];
            Lines[ID][FixOffset(0x65)] = b[1];
        }

        private byte ReturnActivationType(ushort ID) // U_HL[0], type 0x0A
        {
            return Lines[ID][FixOffset(0x60)];
        }

        private void SetActivationType(ushort ID, byte value)
        {
            Lines[ID][FixOffset(0x60)] = value;
        }

        private byte ReturnDamageType(ushort ID) // U_HL[1], type 0x0A
        {
            return Lines[ID][FixOffset(0x61)];
        }

        private void SetDamageType(ushort ID, byte value)
        {
            Lines[ID][FixOffset(0x61)] = value;
        }


        private byte ReturnBlockingType(ushort ID) // U_HM[0], type 0x0A
        {
            return Lines[ID][FixOffset(0x62)];
        }

        private void SetBlockingType(ushort ID, byte value)
        {
            Lines[ID][FixOffset(0x62)] = value;
        }

        private byte ReturnUnknown_SJ(ushort ID) // U_HM[1], type 0x0A
        {
            return Lines[ID][FixOffset(0x63)];
        }

        private void SetUnknown_SJ(ushort ID, byte value)
        {
            Lines[ID][FixOffset(0x63)] = value;
        }

        #endregion


        #region Type 0x01
        private float ReturnDestinationFacingAngle(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], FixOffset(0x68));
        }
        private void SetDestinationFacingAngle(ushort ID, float value)
        {
            int offset = FixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnDestinationFacingAngle_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], FixOffset(0x68));
        }
        private void SetDestinationFacingAngle_Hex(ushort ID, uint value)
        {
            int offset = FixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }


        // aqui o id da rom, não esta invertido, então eu inverto para ficar certo no ushort 
        private ushort ReturnDestinationRoom(ushort ID) // U_RK, type 0x01
        {
            byte[] b = new byte[] { Lines[ID][FixOffset(0x6D)], Lines[ID][FixOffset(0x6C)] };
            return BitConverter.ToUInt16(b, 0);
        }

        // lembrando da estrada invertida de byte
        private void SetDestinationRoom(ushort ID, ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][FixOffset(0x6D)] = b[0];
            Lines[ID][FixOffset(0x6C)] = b[1];
        }

        //LockedDoorType
        private byte ReturnLockedDoorType(ushort ID)
        {
            return Lines[ID][FixOffset(0x6E)];
        }
        private void SetLockedDoorType(ushort ID, byte value)
        {
            Lines[ID][FixOffset(0x6E)] = value;
        }

        //LockedDoorIndex
        private byte ReturnLockedDoorIndex(ushort ID)
        {
            return Lines[ID][FixOffset(0x6F)];
        }
        private void SetLockedDoorIndex(ushort ID, byte value)
        {
            Lines[ID][FixOffset(0x6F)] = value;
        }


        #endregion


        #region for Type 0x10, 0x12, 0x15, StartPointW/HidingPointW

        private byte[] ReturnObjPointW_onlyClassic(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x6C];
                b[1] = Lines[ID][0x6D];
                b[2] = Lines[ID][0x6E];
                b[3] = Lines[ID][0x6F];
                return b;
            }
            return new byte[4];
        }


        private void SetObjPointW_onlyClassic(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x6C] = value[0];
                Lines[ID][0x6D] = value[1];
                Lines[ID][0x6E] = value[2];
                Lines[ID][0x6F] = value[3];
            }
        }

        #endregion


        #region Type 0x10 LadderClimbUp

        private uint ReturnLocationAndLadderFacingAngle_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], LadderFixOffset(0x68));
        }
        private void SetLocationAndLadderFacingAngle_Hex(ushort ID, uint value)
        {
            int offset = LadderFixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnLocationAndLadderFacingAngle(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], LadderFixOffset(0x68));
        }
        private void SetLocationAndLadderFacingAngle(ushort ID, float value)
        {
            int offset = LadderFixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private sbyte ReturnLadderStepCount(ushort ID)
        {
            return (sbyte)Lines[ID][LadderFixOffset(0x6C)];
        }
        private void SetLadderStepCount(ushort ID, sbyte value)
        {
            Lines[ID][LadderFixOffset(0x6C)] = (byte)value;
        }

        private byte ReturnLadderParameter0(ushort ID)
        {
            return Lines[ID][LadderFixOffset(0x6D)];
        }
        private void SetLadderParameter0(ushort ID, byte value)
        {
            Lines[ID][LadderFixOffset(0x6D)] = value;
        }


        private byte ReturnLadderParameter1(ushort ID)
        {
            return Lines[ID][LadderFixOffset(0x6E)];
        }
        private void SetLadderParameter1(ushort ID, byte value)
        {
            Lines[ID][LadderFixOffset(0x6E)] = value;
        }

        private byte ReturnLadderParameter2(ushort ID)
        {
            return Lines[ID][LadderFixOffset(0x6F)];
        }
        private void SetLadderParameter2(ushort ID, byte value)
        {
            Lines[ID][LadderFixOffset(0x6F)] = value;
        }

        private byte ReturnLadderParameter3(ushort ID)
        {
            return Lines[ID][LadderFixOffset(0x70)];
        }
        private void SetLadderParameter3(ushort ID, byte value)
        {
            Lines[ID][LadderFixOffset(0x70)] = value;
        }

        private byte ReturnUnknown_SG(ushort ID)
        {
            return Lines[ID][LadderFixOffset(0x71)];
        }
        private void SetUnknown_SG(ushort ID, byte value)
        {
            Lines[ID][LadderFixOffset(0x71)] = value;
        }

        private byte[] ReturnUnknown_SH(ushort ID)
        {
            int offset = LadderFixOffset(0x72);
            byte[] b = new byte[2];
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }
        private void SetUnknown_SH(ushort ID, byte[] value)
        {
            int offset = LadderFixOffset(0x72);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }

        #endregion


        #region Type 0x12 AshleyHiding

        private float ReturnAshleyHidingZoneCorner0_X(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x60));
        }
        private void SetAshleyHidingZoneCorner0_X(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x60);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner0_Z(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x64));
        }
        private void SetAshleyHidingZoneCorner0_Z(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x64);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner1_X(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x68));
        }
        private void SetAshleyHidingZoneCorner1_X(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner1_Z(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x6C));
        }
        private void SetAshleyHidingZoneCorner1_Z(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x6C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner2_X(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x70));
        }
        private void SetAshleyHidingZoneCorner2_X(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x70);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner2_Z(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x74));
        }
        private void SetAshleyHidingZoneCorner2_Z(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x74);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner3_X(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x78));
        }
        private void SetAshleyHidingZoneCorner3_X(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x78);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingZoneCorner3_Z(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x7C));
        }
        private void SetAshleyHidingZoneCorner3_Z(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x7C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }


        private float ReturnAshleyHidingPointX(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x80));
        }
        private void SetAshleyHidingPointX(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x80);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnAshleyHidingPointY(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x84));
        }
        private void SetAshleyHidingPointY(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x84);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }


        private float ReturnAshleyHidingPointZ(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], AshleyFixOffset(0x88));
        }
        private void SetAshleyHidingPointZ(ushort ID, float value)
        {
            int offset = AshleyFixOffset(0x88);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner0_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x60));
        }
        private void SetAshleyHidingZoneCorner0_X_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x60);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner0_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x64));
        }
        private void SetAshleyHidingZoneCorner0_Z_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x64);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner1_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x68));
        }
        private void SetAshleyHidingZoneCorner1_X_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner1_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x6C));
        }
        private void SetAshleyHidingZoneCorner1_Z_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x6C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner2_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x70));
        }
        private void SetAshleyHidingZoneCorner2_X_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x70);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner2_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x74));
        }
        private void SetAshleyHidingZoneCorner2_Z_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x74);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner3_X_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x78));
        }
        private void SetAshleyHidingZoneCorner3_X_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x78);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingZoneCorner3_Z_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x7C));
        }
        private void SetAshleyHidingZoneCorner3_Z_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x7C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }


        private uint ReturnAshleyHidingPointX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x80));
        }
        private void SetAshleyHidingPointX_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x80);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnAshleyHidingPointY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x84));
        }
        private void SetAshleyHidingPointY_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x84);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }


        private uint ReturnAshleyHidingPointZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], AshleyFixOffset(0x88));
        }
        private void SetAshleyHidingPointZ_Hex(ushort ID, uint value)
        {
            int offset = AshleyFixOffset(0x88);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }


        private void SetUnknown_SM(ushort ID, byte[] value)
        {
            int offset = AshleyFixOffset(0x5C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }
        private byte[] ReturnUnknown_SM(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = AshleyFixOffset(0x5C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        private void SetUnknown_SN(ushort ID, byte[] value)
        {
            int offset = AshleyFixOffset(0x8C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }
        private byte[] ReturnUnknown_SN(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = AshleyFixOffset(0x8C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        private void SetUnknown_SS(ushort ID, byte[] value)
        {
            int offset = AshleyFixOffset(0x94);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }
        private byte[] ReturnUnknown_SS(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = AshleyFixOffset(0x94);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        private void SetUnknown_SR(ushort ID, byte[] value)
        {
            int offset = AshleyFixOffset(0x92);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
        }
        private byte[] ReturnUnknown_SR(ushort ID)
        {
            byte[] b = new byte[2];
            int offset = AshleyFixOffset(0x92);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            return b;
        }

        private byte ReturnUnknown_SP(ushort ID)
        {
            return Lines[ID][AshleyFixOffset(0x90)];
        }
        private void SetUnknown_SP(ushort ID, byte value)
        {
            Lines[ID][AshleyFixOffset(0x90)] = value;
        }

        private byte ReturnUnknown_SQ(ushort ID)
        {
            return Lines[ID][AshleyFixOffset(0x91)];
        }
        private void SetUnknown_SQ(ushort ID, byte value)
        {
            Lines[ID][AshleyFixOffset(0x91)] = value;
        }

        #endregion


        #region Type 0x15 GrappleGun

        // INFO
        //GrappleGunEndPoint.W = U_RI.W

        private float ReturnGrappleGunEndPointX(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x68));
        }

        private float ReturnGrappleGunEndPointY(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x6C));
        }

        private float ReturnGrappleGunEndPointZ(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x70));
        }

        private void SetGrappleGunEndPointX(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunEndPointY(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x6C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunEndPointZ(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x70);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnGrappleGunEndPointX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x68));
        }

        private uint ReturnGrappleGunEndPointY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x6C));
        }

        private uint ReturnGrappleGunEndPointZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x70));
        }

        private void SetGrappleGunEndPointX_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x68);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunEndPointY_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x6C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunEndPointZ_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x70);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private float ReturnGrappleGunThirdPointX(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x74));
        }

        private float ReturnGrappleGunThirdPointY(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x78));
        }

        private float ReturnGrappleGunThirdPointZ(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x7C));
        }

        private void SetGrappleGunThirdPointX(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x74);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunThirdPointY(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x78);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunThirdPointZ(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x7C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private uint ReturnGrappleGunThirdPointX_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x74));
        }

        private uint ReturnGrappleGunThirdPointY_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x78));
        }

        private uint ReturnGrappleGunThirdPointZ_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x7C));
        }

        private void SetGrappleGunThirdPointX_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x74);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunThirdPointY_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x78);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        private void SetGrappleGunThirdPointZ_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x7C);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        //GrappleGunFacingAngle
        private float ReturnGrappleGunFacingAngle(ushort ID)
        {
            return BitConverter.ToSingle(Lines[ID], GrappleGunFixOffset(0x80));
        }
        private void SetGrappleGunFacingAngle(ushort ID, float value)
        {
            int offset = GrappleGunFixOffset(0x80);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }
        private uint ReturnGrappleGunFacingAngle_Hex(ushort ID)
        {
            return BitConverter.ToUInt32(Lines[ID], GrappleGunFixOffset(0x80));
        }
        private void SetGrappleGunFacingAngle_Hex(ushort ID, uint value)
        {
            int offset = GrappleGunFixOffset(0x80);
            byte[] b = BitConverter.GetBytes(value);
            Lines[ID][offset] = b[0];
            Lines[ID][offset + 1] = b[1];
            Lines[ID][offset + 2] = b[2];
            Lines[ID][offset + 3] = b[3];
        }

        //GrappleGunThirdPointW
        private byte[] ReturnGrappleGunThirdPointW(ushort ID)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                byte[] b = new byte[4];
                b[0] = Lines[ID][0x8C];
                b[1] = Lines[ID][0x8D];
                b[2] = Lines[ID][0x8E];
                b[3] = Lines[ID][0x8F];
                return b;
            }
            return new byte[4];
        }
        private void SetGrappleGunThirdPointW(ushort ID, byte[] value)
        {
            if (GetRe4Version == Re4Version.V2007PS2)
            {
                Lines[ID][0x8C] = value[0];
                Lines[ID][0x8D] = value[1];
                Lines[ID][0x8E] = value[2];
                Lines[ID][0x8F] = value[3];
            }        
        }

        private byte ReturnGrappleGunParameter0(ushort ID)
        {
            return Lines[ID][GrappleGunFixOffset(0x84)];
        }
        private void SetGrappleGunParameter0(ushort ID, byte value)
        {
            Lines[ID][GrappleGunFixOffset(0x84)] = value;
        }

        private byte ReturnGrappleGunParameter1(ushort ID)
        {
            return Lines[ID][GrappleGunFixOffset(0x85)];
        }
        private void SetGrappleGunParameter1(ushort ID, byte value)
        {
            Lines[ID][GrappleGunFixOffset(0x85)] = value;
        }


        private byte ReturnGrappleGunParameter2(ushort ID)
        {
            return Lines[ID][GrappleGunFixOffset(0x86)];
        }
        private void SetGrappleGunParameter2(ushort ID, byte value)
        {
            Lines[ID][GrappleGunFixOffset(0x86)] = value;
        }


        private byte ReturnGrappleGunParameter3(ushort ID)
        {
            return Lines[ID][GrappleGunFixOffset(0x87)];
        }
        private void SetGrappleGunParameter3(ushort ID, byte value)
        {
            Lines[ID][GrappleGunFixOffset(0x87)] = value;
        }

        private void SetUnknown_SK(ushort ID, byte[] value)
        {
            int offset = GrappleGunFixOffset(0x88);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }
        private byte[] ReturnUnknown_SK(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = GrappleGunFixOffset(0x88);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        private void SetUnknown_SL(ushort ID, byte[] value)
        {
            int offset = GrappleGunFixOffset(0x8C);
            Lines[ID][offset] = value[0];
            Lines[ID][offset + 1] = value[1];
            Lines[ID][offset + 2] = value[2];
            Lines[ID][offset + 3] = value[3];
        }
        private byte[] ReturnUnknown_SL(ushort ID)
        {
            byte[] b = new byte[4];
            int offset = GrappleGunFixOffset(0x8C);
            b[0] = Lines[ID][offset];
            b[1] = Lines[ID][offset + 1];
            b[2] = Lines[ID][offset + 2];
            b[3] = Lines[ID][offset + 3];
            return b;
        }

        #endregion


        #region MethodsForGL

        public float GetItemTriggerRadiusToRender(ushort ID) 
        {
            return ReturnItemTriggerRadius(ID) / 100f;
        }


        private Vector3 GetItemPosition(ushort ID) 
        {
            if (Globals.RenderItemPositionAtAssociatedObjectLocation)
            {
                RefInteractionType refInteractionType = GetRefInteractionType(ID);
                ushort refInteractionIndex = ReturnRefInteractionIndex(ID);
                if (refInteractionType == RefInteractionType.Enemy && DataBase.FileESL != null && DataBase.FileESL.Lines.ContainsKey(refInteractionIndex)) // associado a inimigo
                {
                    return DataBase.FileESL.MethodsForGL.GetPosition(refInteractionIndex);
                }
                else if (refInteractionType == RefInteractionType.EtcModel && DataBase.FileETS != null && DataBase.FileETS.ETS_ID_List.ContainsKey(refInteractionIndex)
                    && DataBase.FileETS.ETS_ID_List[refInteractionIndex].Count >= 1 && DataBase.FileETS.Lines.ContainsKey(DataBase.FileETS.ETS_ID_List[refInteractionIndex][0])) // associado a EtcModel
                {
                    return DataBase.FileETS.MethodsForGL.GetPosition(DataBase.FileETS.ETS_ID_List[refInteractionIndex][0]);
                }
            }
            return new Vector3(ReturnObjPositionX(ID) / 100f, ReturnObjPositionY(ID) / 100f, ReturnObjPositionZ(ID) / 100f);
        }

        private Matrix4 GetItemRotation(ushort ID)
        {
            if (Globals.ItemDisableRotationAll)
            {
                return Matrix4.Identity;
            }

            float AngleX = ReturnItemAngleX(ID);
            float AngleY = ReturnItemAngleY(ID);
            float AngleZ = 0;

            if (!(GetRe4Version == Re4Version.V2007PS2 && GetSpecialFileFormat == SpecialFileFormat.AEV))
            {
                AngleZ = ReturnItemAngleZ(ID);
            }

           
            if (Globals.ItemDisableRotationIfXorYorZequalZero && (AngleZ == 0 || AngleX == 0 || AngleY == 0))  // senão tive valor nos tres campos no jogo não rotaciona
            {
                return Matrix4.Identity;
            }
            
            if (Globals.ItemDisableRotationIfZisNotGreaterThanZero && (AngleZ <= 0))  // se o valor for 0 ou menor (negativo) não rotaciona
            {
                return Matrix4.Identity;
            }


            Matrix4 X = Matrix4.CreateRotationX(ItemAngleConverter(AngleX));
            Matrix4 Y = Matrix4.CreateRotationY(ItemAngleConverter(AngleY));
            Matrix4 Z = Matrix4.CreateRotationZ(ItemAngleConverter(AngleZ));

            switch (Globals.ItemRotationOrder)
            {
                case ObjRotationOrder.RotationXYZ:
                    return X * Y * Z;
                case ObjRotationOrder.RotationXZY:
                    return X * Z * Y;
                case ObjRotationOrder.RotationYXZ:
                    return Y * X * Z;
                case ObjRotationOrder.RotationYZX:
                    return Y * Z * X;
                case ObjRotationOrder.RotationZYX:
                    return Z * Y * X;
                case ObjRotationOrder.RotationZXY:
                    return Z * X * Y;
                case ObjRotationOrder.RotationXY:
                    return X * Y;
                case ObjRotationOrder.RotationXZ:
                    return X * Z;
                case ObjRotationOrder.RotationYX:
                    return Y * X;
                case ObjRotationOrder.RotationYZ:
                    return Y * Z;
                case ObjRotationOrder.RotationZX:
                    return Z * X;
                case ObjRotationOrder.RotationZY:
                    return Z * Y;
                case ObjRotationOrder.RotationX:
                    return X;
                case ObjRotationOrder.RotationY:
                    return Y;
                case ObjRotationOrder.RotationZ:
                    return Z;
                default:
                    return Matrix4.Identity;
            }
        }


        private static float ItemAngleConverter(float ItemAngle)
        {
            return (Globals.ItemRotationCalculationMultiplier * ItemAngle) / Globals.ItemRotationCalculationDivider;
        }


        #endregion


        #region ExtraMethodsForGL

        private Vector3 GetFirstPosition(ushort ID)
        {
            return new Vector3(ReturnObjPositionX(ID) / 100f, ReturnObjPositionY(ID) / 100f, ReturnObjPositionZ(ID) / 100f);
        }

        private Matrix4 GetWarpRotation(ushort ID)
        {
            return Matrix4.CreateRotationY(ReturnDestinationFacingAngle(ID));
        }

        private Matrix4 GetLocationAndLadderRotation(ushort ID)
        {
            return Matrix4.CreateRotationY(ReturnLocationAndLadderFacingAngle(ID));
        }


        private Vector3 GetAshleyPoint(ushort ID)
        {
            return new Vector3(ReturnAshleyHidingPointX(ID) / 100f, ReturnAshleyHidingPointY(ID) / 100f, ReturnAshleyHidingPointZ(ID) / 100f);
        }

        /// <summary>
        /// <para>ordem dos vector2</para>
        /// <para>[0] point0</para>
        /// <para>[1] point1</para>
        /// <para>[2] point2</para>
        /// <para>[3] point3</para>
        /// <para>[4] ReturnAshleyHidingPointY, ReturnAshleyHidingPointY</para>
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private Vector2[] GetAshleyHidingZoneCorner(ushort ID)
        {
            Vector2[] v = new Vector2[5];
            v[0] = new Vector2(ReturnAshleyHidingZoneCorner0_X(ID) / 100f, ReturnAshleyHidingZoneCorner0_Z(ID) / 100f);
            v[1] = new Vector2(ReturnAshleyHidingZoneCorner1_X(ID) / 100f, ReturnAshleyHidingZoneCorner1_Z(ID) / 100f);
            v[2] = new Vector2(ReturnAshleyHidingZoneCorner2_X(ID) / 100f, ReturnAshleyHidingZoneCorner2_Z(ID) / 100f);
            v[3] = new Vector2(ReturnAshleyHidingZoneCorner3_X(ID) / 100f, ReturnAshleyHidingZoneCorner3_Z(ID) / 100f);
            v[4] = new Vector2(ReturnAshleyHidingPointY(ID) / 100f, ReturnAshleyHidingPointY(ID) / 100f);
            return v;
        }

        private Matrix4 GetAshleyHidingZoneCornerMatrix4(ushort ID) 
        {
            float Y = ReturnAshleyHidingPointY(ID) / 100f;
            return new Matrix4(
                ReturnAshleyHidingZoneCorner0_X(ID) / 100f, Y, ReturnAshleyHidingZoneCorner0_Z(ID) / 100f, 0f,
                ReturnAshleyHidingZoneCorner1_X(ID) / 100f, Y, ReturnAshleyHidingZoneCorner1_Z(ID) / 100f, 0f,
                ReturnAshleyHidingZoneCorner2_X(ID) / 100f, Y, ReturnAshleyHidingZoneCorner2_Z(ID) / 100f, 0f,
                ReturnAshleyHidingZoneCorner3_X(ID) / 100f, Y, ReturnAshleyHidingZoneCorner3_Z(ID) / 100f, 0f
                );
        }

        private Vector3 GetGrappleGunEndPosition(ushort ID)
        {
            return new Vector3(ReturnGrappleGunEndPointX(ID) / 100f, ReturnGrappleGunEndPointY(ID) / 100f, ReturnGrappleGunEndPointZ(ID) / 100f);
        }


        private Vector3 GetGrappleGunThirdPosition(ushort ID)
        {
            return new Vector3(ReturnGrappleGunThirdPointX(ID) / 100f, ReturnGrappleGunThirdPointY(ID) / 100f, ReturnGrappleGunThirdPointZ(ID) / 100f);
        }

        private Matrix4 GetGrappleGunFacingAngleRotation(ushort ID)
        {
            return Matrix4.CreateRotationY(ReturnGrappleGunFacingAngle(ID));
        }

        #endregion


        #region index Manager

        //FirtIndexList
        //SecundIndexList

        public void SetStartIdexContent() 
        {
            FirstIndexList.Clear();
            SecundIndexList.Clear();

            ushort[] Keys = Lines.Keys.ToArray();
            for (int i = 0; i < Keys.Length; i++)
            {
                ushort ID = Keys[i];
                byte index = ReturnSpecialIndex(ID);

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

                var specialType = GetSpecialType(ID);
                if (specialType == SpecialType.T03_Items)
                {
                    ushort secondIndex = ReturnSpecialIndex(ID);

                    if (SecundIndexList.ContainsKey(secondIndex))
                    {
                        SecundIndexList[secondIndex].Add(ID);
                    }
                    else
                    {
                        List<ushort> internalLines = new List<ushort>();
                        internalLines.Add(ID);
                        SecundIndexList.Add(secondIndex, internalLines);
                    }

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

        private void UpdateSecundIndexList(ushort LineID, ushort OldIndex, ushort NewIndex)
        {
            if (SecundIndexList.ContainsKey(OldIndex) && SecundIndexList[OldIndex].Contains(LineID))
            {
                SecundIndexList[OldIndex].Remove(LineID);
                if (SecundIndexList[OldIndex].Count == 0)
                {
                    SecundIndexList.Remove(OldIndex);
                }
            }
            if (!SecundIndexList.ContainsKey(NewIndex))
            {
                var List = new List<ushort>();
                List.Add(LineID);
                SecundIndexList.Add(NewIndex, List);
            }
            else
            {
                SecundIndexList[NewIndex].Add(LineID);
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

        private void RemoveSecundIndexList(ushort LineID, ushort OldIndex)
        {
            if (SecundIndexList.ContainsKey(OldIndex) && SecundIndexList[OldIndex].Contains(LineID))
            {
                SecundIndexList[OldIndex].Remove(LineID);
                if (SecundIndexList[OldIndex].Count == 0)
                {
                    SecundIndexList.Remove(OldIndex);
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

        private void AddNewSecundIndexList(ushort LineID, ushort NewIndex)
        {
            if (!SecundIndexList.ContainsKey(NewIndex))
            {
                var List = new List<ushort>();
                List.Add(LineID);
                SecundIndexList.Add(NewIndex, List);
            }
            else
            {
                SecundIndexList[NewIndex].Add(LineID);
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

