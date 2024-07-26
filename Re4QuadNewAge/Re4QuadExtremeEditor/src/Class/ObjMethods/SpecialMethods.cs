using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public class SpecialMethods : BaseTriggerZoneMethods
    {

        #region geral metodos
        public ReturnRe4Version ReturnRe4Version;
        public ReturnSpecialFileFormat GetSpecialFileFormat;

        public ReturnSpecialType GetSpecialType; //[0x35]
        public ReturnRefInteractionType GetRefInteractionType; //[0x46]

        public ReturnByteArray ReturnLine;
        public SetByteArray SetLine;

        #endregion

        // referente aos bytes

        #region para todos os especiais
        // byte 0x35 for all
        public ReturnByte ReturnSpecialType; //[0x35]
        public SetByte SetSpecialType;

        public ReturnByte ReturnSpecialIndex; //[0x36]
        public SetByte SetSpecialIndex;

        // inicio

        public ReturnByteArray ReturnUnknown_GG; //[0x00][0x01][0x02][0x03]
        public SetByteArray SetUnknown_GG;

        public ReturnByte ReturnUnknown_KG; //[0x34]
        public SetByte SetUnknown_KG;

        public ReturnByte ReturnUnknown_KJ; //[0x37]
        public SetByte SetUnknown_KJ;

        public ReturnByte ReturnUnknown_LI; //[0x38]
        public SetByte SetUnknown_LI;

        public ReturnByte ReturnUnknown_LO; //[0x39]
        public SetByte SetUnknown_LO;

        public ReturnByte ReturnUnknown_LU; //[0x3A]
        public SetByte SetUnknown_LU;

        public ReturnByte ReturnUnknown_LH; //[0x3B]
        public SetByte SetUnknown_LH;

        public ReturnByteArray ReturnUnknown_MI; //[0x3C][0x3D]
        public SetByteArray SetUnknown_MI;

        public ReturnByteArray ReturnUnknown_MO; //[0x3E][0x3F]
        public SetByteArray SetUnknown_MO;

        public ReturnByteArray ReturnUnknown_MU; //[0x40][0x41]
        public SetByteArray SetUnknown_MU;

        public ReturnByteArray ReturnUnknown_NI; //[0x42][0x43]
        public SetByteArray SetUnknown_NI;

        public ReturnByte ReturnUnknown_NO; //[0x44]
        public SetByte SetUnknown_NO;

        public ReturnByte ReturnUnknown_NS; //[0x45]
        public SetByte SetUnknown_NS;

        public ReturnByte ReturnRefInteractionType; //[0x46]
        public SetByte SetRefInteractionType;

        public ReturnByte ReturnRefInteractionIndex; //[0x47]
        public SetByte SetRefInteractionIndex;

        public ReturnByte ReturnUnknown_NT; //[0x48]
        public SetByte SetUnknown_NT;

        public ReturnByte ReturnUnknown_NU; //[0x49]
        public SetByte SetUnknown_NU;

        public ReturnByte ReturnPromptMessage; //[0x4A]
        public SetByte SetPromptMessage;

        public ReturnByte ReturnUnknown_PI; //[0x4B]
        public SetByte SetUnknown_PI;

        public ReturnByteArray ReturnUnknown_PO; //[0x4C][0x4D][0x4E][0x4F]
        public SetByteArray SetUnknown_PO;

        public ReturnByteArray ReturnUnknown_PU; //[0x50][0x51]
        public SetByteArray SetUnknown_PU;

        public ReturnByte ReturnUnknown_PK; //[0x52]
        public SetByte SetUnknown_PK;

        public ReturnByte ReturnMessageColor; //[0x53]
        public SetByte SetMessageColor;

        public ReturnByteArray ReturnUnknown_QI; //[0x54][0x55][0x56][0x57]
        public SetByteArray SetUnknown_QI;

        public ReturnByteArray ReturnUnknown_QO; //[0x58][0x59][0x5A][0x5B]
        public SetByteArray SetUnknown_QO;

        public ReturnByteArray ReturnUnknown_QU; //existe somente na versão classic do jogo
        public SetByteArray SetUnknown_QU;
        #endregion

        // a partir daqui não tem mais como referenciar os offsets, pois cada versão tem uma quantidade diferente de bytes

        #region Geral Specials / Unknown Special Type

        public ReturnByteArray ReturnUnknown_HH; // 2 bytes
        public SetByteArray SetUnknown_HH;

        public ReturnByteArray ReturnUnknown_HK; // 2 bytes
        public SetByteArray SetUnknown_HK;

        public ReturnByteArray ReturnUnknown_HL; // 2 bytes
        public SetByteArray SetUnknown_HL;

        public ReturnByteArray ReturnUnknown_HM; // 2 bytes
        public SetByteArray SetUnknown_HM;

        public ReturnByteArray ReturnUnknown_HN; // 2 bytes
        public SetByteArray SetUnknown_HN;

        public ReturnByteArray ReturnUnknown_HR; // 2 bytes
        public SetByteArray SetUnknown_HR;

        public ReturnByteArray ReturnUnknown_RH; // 2 bytes
        public SetByteArray SetUnknown_RH;

        public ReturnByteArray ReturnUnknown_RJ; // 2 bytes
        public SetByteArray SetUnknown_RJ;

        public ReturnByteArray ReturnUnknown_RK; // 2 bytes
        public SetByteArray SetUnknown_RK;

        public ReturnByteArray ReturnUnknown_RL; // 2 bytes
        public SetByteArray SetUnknown_RL;

        public ReturnByteArray ReturnUnknown_RM; // 2 bytes
        public SetByteArray SetUnknown_RM;

        public ReturnByteArray ReturnUnknown_RN; // 2 bytes
        public SetByteArray SetUnknown_RN;

        public ReturnByteArray ReturnUnknown_RP; // 2 bytes
        public SetByteArray SetUnknown_RP;

        public ReturnByteArray ReturnUnknown_RQ; // 2 bytes
        public SetByteArray SetUnknown_RQ;

        public ReturnByteArray ReturnUnknown_TG; // 4 bytes
        public SetByteArray SetUnknown_TG;

        public ReturnByteArray ReturnUnknown_TH; // 4 bytes
        public SetByteArray SetUnknown_TH;

        public ReturnByteArray ReturnUnknown_TJ; // 4 bytes
        public SetByteArray SetUnknown_TJ;

        public ReturnByteArray ReturnUnknown_TK; // 4 bytes
        public SetByteArray SetUnknown_TK;

        public ReturnByteArray ReturnUnknown_TL; // 4 bytes
        public SetByteArray SetUnknown_TL;

        public ReturnByteArray ReturnUnknown_TM; // 4 bytes
        public SetByteArray SetUnknown_TM;

        public ReturnByteArray ReturnUnknown_TN; // 4 bytes
        public SetByteArray SetUnknown_TN;

        public ReturnByteArray ReturnUnknown_TP; // 4 bytes
        public SetByteArray SetUnknown_TP;

        public ReturnByteArray ReturnUnknown_TQ; // 4 bytes
        public SetByteArray SetUnknown_TQ;
        #endregion


        #region existe somente no arquivo ITA do classic

        public ReturnByteArray ReturnUnknown_VS; // 4 bytes, existe somente no arquivo ITA do classic, NÃO USADO NO TYPE 0x03
        public SetByteArray SetUnknown_VS;

        public ReturnByteArray ReturnUnknown_VT; // 4 bytes, existe somente no arquivo ITA do classic, NÃO USADO NO TYPE 0x03
        public SetByteArray SetUnknown_VT;

        public ReturnByteArray ReturnUnknown_VI; // 4 bytes, existe somente no arquivo ITA do classic
        public SetByteArray SetUnknown_VI;

        public ReturnByteArray ReturnUnknown_VO; // 4 bytes, existe somente no arquivo ITA do classic
        public SetByteArray SetUnknown_VO;
        #endregion


        // outros Specials

        #region  ObjPoint for Type 0x03, 0x10, 0x12, 0x13, 0x15


        // in float

        public ReturnFloat ReturnObjPointX; // ITEN_POSITION, WARP_DESTINATION_POINT, LADDER_CLINB_UP_START_POINT, ADA_GRAPLE_GUN_START_POINT
        public SetFloat SetObjPointX;

        public ReturnFloat ReturnObjPointY;
        public SetFloat SetObjPointY;

        public ReturnFloat ReturnObjPointZ;
        public SetFloat SetObjPointZ;

        // in Hex

        public ReturnUint ReturnObjPointX_Hex; // ITEN_POSITION, WARP_DESTINATION_POINT, LADDER_CLINB_UP_START_POINT, ADA_GRAPLE_GUN_START_POINT
        public SetUint SetObjPointX_Hex;

        public ReturnUint ReturnObjPointY_Hex;
        public SetUint SetObjPointY_Hex;

        public ReturnUint ReturnObjPointZ_Hex;
        public SetUint SetObjPointZ_Hex;

        #endregion


        #region ObjPoint.W
        public ReturnByteArray ReturnObjPointW; // 4 bytes
        public SetByteArray SetObjPointW;

        public ReturnByteArray ReturnObjPointW_onlyClassic; // 4 bytes, only Classic
        public SetByteArray SetObjPointW_onlyClassic;

        #endregion


        #region type 0x03 itens
        //U_RI

        public ReturnFloat ReturnUnknown_RI_X;
        public SetFloat SetUnknown_RI_X;

        public ReturnFloat ReturnUnknown_RI_Y;
        public SetFloat SetUnknown_RI_Y;

        public ReturnFloat ReturnUnknown_RI_Z;
        public SetFloat SetUnknown_RI_Z;


        //in hex
        public ReturnUint ReturnUnknown_RI_X_Hex;
        public SetUint SetUnknown_RI_X_Hex;

        public ReturnUint ReturnUnknown_RI_Y_Hex;
        public SetUint SetUnknown_RI_Y_Hex;

        public ReturnUint ReturnUnknown_RI_Z_Hex;
        public SetUint SetUnknown_RI_Z_Hex;

        // parte final Itens

        public ReturnByteArray ReturnUnknown_RI_W; // 4 bytes, existe somente no classic, usado somente no special type 0x03
        public SetByteArray SetUnknown_RI_W;

        public ReturnByteArray ReturnUnknown_RO; // 4 bytes, existe somente no classic, usado somente no special type 0x03
        public SetByteArray SetUnknown_RO;

        public ReturnUshort ReturnItemNumber;
        public SetUshort SetItemNumber;

        public ReturnByteArray ReturnUnknown_RU; // 2 bytes
        public SetByteArray SetUnknown_RU;

        public ReturnUshort ReturnItemAmount;
        public SetUshort SetItemAmount;

        public ReturnUshort ReturnSecundIndex;
        public SetUshort SetSecundIndex;

        public ReturnUshort ReturnItemAuraType;
        public SetUshort SetItemAuraType;

        public ReturnByte ReturnUnknown_QM;
        public SetByte SetUnknown_QM;

        public ReturnByte ReturnUnknown_QL;
        public SetByte SetUnknown_QL;

        public ReturnByte ReturnUnknown_QR;
        public SetByte SetUnknown_QR;

        public ReturnByte ReturnUnknown_QH;
        public SetByte SetUnknown_QH;

        public ReturnByteArray ReturnUnknown_QG; // 2 bytes
        public SetByteArray SetUnknown_QG;


        //float
        public ReturnFloat ReturnItemTriggerRadius;
        public SetFloat SetItemTriggerRadius;

        public ReturnFloat ReturnItemAngleX;
        public SetFloat SetItemAngleX;

        public ReturnFloat ReturnItemAngleY;
        public SetFloat SetItemAngleY;

        public ReturnFloat ReturnItemAngleZ;
        public SetFloat SetItemAngleZ;

        //hex
        public ReturnUint ReturnItemTriggerRadius_Hex;
        public SetUint SetItemTriggerRadius_Hex;

        public ReturnUint ReturnItemAngleX_Hex;
        public SetUint SetItemAngleX_Hex;

        public ReturnUint ReturnItemAngleY_Hex;
        public SetUint SetItemAngleY_Hex;

        public ReturnUint ReturnItemAngleZ_Hex;
        public SetUint SetItemAngleZ_Hex;


        public ReturnByteArray ReturnItemAngleW; // 4 bytes
        public SetByteArray SetItemAngleW;
        #endregion


        #region TYPE 0x04, 0x05, 0x0A and 0x11

        //NeededItemNumber
        public ReturnUshort ReturnNeededItemNumber; // U_HH, type 0x11
        public SetUshort SetNeededItemNumber;

        //EnemyGroup
        public ReturnUshort ReturnEnemyGroup; // U_HK, type 0x04
        public SetUshort SetEnemyGroup;

        //RoomMessage
        public ReturnUshort ReturnRoomMessage; // U_HK, type 0x05
        public SetUshort SetRoomMessage;

        //MessageCutSceneID
        public ReturnUshort ReturnMessageCutSceneID; // U_HL, type 0x05
        public SetUshort SetMessageCutSceneID;

        //MessageID
        public ReturnUshort ReturnMessageID; // U_HM, type 0x05
        public SetUshort SetMessageID;

        //ActivationType
        public ReturnByte ReturnActivationType; // U_HL[0], type 0x0A
        public SetByte SetActivationType;

        //DamageType
        public ReturnByte ReturnDamageType; // U_HL[1], type 0x0A
        public SetByte SetDamageType;

        //BlockingType
        public ReturnByte ReturnBlockingType; // U_HM[0], type 0x0A
        public SetByte SetBlockingType;

        //Unknown_SJ
        public ReturnByte ReturnUnknown_SJ; // U_HM[1], type 0x0A
        public SetByte SetUnknown_SJ;

        //DamageAmount
        public ReturnUshort ReturnDamageAmount; // U_HN, type 0x0A
        public SetUshort SetDamageAmount;


        #endregion


        #region Type 0x01 warp door

        //info:
        //DestinationPoint.X = ObjPointX;
        //DestinationPoint.Y = ObjPointY;
        //DestinationPoint.Z = ObjPointZ;

        public ReturnFloat ReturnDestinationFacingAngle; //WARP_DESTINATION_FACING_ANGLE
        public SetFloat SetDestinationFacingAngle;

        public ReturnUint ReturnDestinationFacingAngle_Hex; //WARP_DESTINATION_FACING_ANGLE
        public SetUint SetDestinationFacingAngle_Hex;

        //DestinationRoom
        public ReturnUshort ReturnDestinationRoom; // U_RK, type 0x01
        public SetUshort SetDestinationRoom;

        //LockedDoorType
        public ReturnByte ReturnLockedDoorType; //U_RL[0], type 0x01
        public SetByte SetLockedDoorType;
        //LockedDoorIndex
        public ReturnByte ReturnLockedDoorIndex; //U_RL[1], type 0x01
        public SetByte SetLockedDoorIndex;

        #endregion

        #region Type 0x13

        public ReturnFloat ReturnTeleportationFacingAngle; //LOCALTELEPORTATION_FACING_ANGLE
        public SetFloat SetTeleportationFacingAngle;

        public ReturnUint ReturnTeleportationFacingAngle_Hex; //LOCALTELEPORTATION_FACING_ANGLE
        public SetUint SetTeleportationFacingAngle_Hex;

        #endregion

        #region Type 0x10 LadderClimbUp

        //info:
        //LadderStartPoint.X = ObjPointX;
        //LadderStartPoint.Y = ObjPointY;
        //LadderStartPoint.Z = ObjPointZ;
        //LadderStartPoint.W = ObjPointW;

        public ReturnFloat ReturnLadderFacingAngle; // LADDER_CLINB_UP_START_POINT_FACING_ANGLE
        public SetFloat SetLadderFacingAngle;

        public ReturnUint ReturnLadderFacingAngle_Hex; // LADDER_CLINB_UP_START_POINT_FACING_ANGLE
        public SetUint SetLadderFacingAngle_Hex;

        // vai de -128 a 127, negativos dece, positivos sobe, sendo -1, 0 e 1, não funcionam
        public ReturnSbyte ReturnLadderStepCount; // quantiade de degraus
        public SetSbyte SetLadderStepCount;

        public ReturnByte ReturnLadderParameter0;
        public SetByte SetLadderParameter0;

        public ReturnByte ReturnLadderParameter1;
        public SetByte SetLadderParameter1;

        public ReturnByte ReturnLadderParameter2;
        public SetByte SetLadderParameter2;

        public ReturnByte ReturnLadderParameter3;
        public SetByte SetLadderParameter3;

        public ReturnByte ReturnUnknown_SG;
        public SetByte SetUnknown_SG;

        public ReturnByteArray ReturnUnknown_SH; // 2 bytes
        public SetByteArray SetUnknown_SH;

        #endregion


        #region Type 0x12 AshleyHiding

        //info:
        //AshleyHidingPoint.W = ObjPointW;

        public ReturnFloat ReturnAshleyHidingPointX;
        public SetFloat SetAshleyHidingPointX;

        public ReturnFloat ReturnAshleyHidingPointY;
        public SetFloat SetAshleyHidingPointY;

        public ReturnFloat ReturnAshleyHidingPointZ;
        public SetFloat SetAshleyHidingPointZ;

        public ReturnFloat ReturnAshleyHidingZoneCorner0_X;
        public SetFloat SetAshleyHidingZoneCorner0_X;

        public ReturnFloat ReturnAshleyHidingZoneCorner0_Z;
        public SetFloat SetAshleyHidingZoneCorner0_Z;

        public ReturnFloat ReturnAshleyHidingZoneCorner1_X;
        public SetFloat SetAshleyHidingZoneCorner1_X;

        public ReturnFloat ReturnAshleyHidingZoneCorner1_Z;
        public SetFloat SetAshleyHidingZoneCorner1_Z;

        public ReturnFloat ReturnAshleyHidingZoneCorner2_X;
        public SetFloat SetAshleyHidingZoneCorner2_X;

        public ReturnFloat ReturnAshleyHidingZoneCorner2_Z;
        public SetFloat SetAshleyHidingZoneCorner2_Z;

        public ReturnFloat ReturnAshleyHidingZoneCorner3_X;
        public SetFloat SetAshleyHidingZoneCorner3_X;

        public ReturnFloat ReturnAshleyHidingZoneCorner3_Z;
        public SetFloat SetAshleyHidingZoneCorner3_Z;

        // in Hex
        public ReturnUint ReturnAshleyHidingPointX_Hex;
        public SetUint SetAshleyHidingPointX_Hex;

        public ReturnUint ReturnAshleyHidingPointY_Hex;
        public SetUint SetAshleyHidingPointY_Hex;

        public ReturnUint ReturnAshleyHidingPointZ_Hex;
        public SetUint SetAshleyHidingPointZ_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner0_X_Hex;
        public SetUint SetAshleyHidingZoneCorner0_X_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner0_Z_Hex;
        public SetUint SetAshleyHidingZoneCorner0_Z_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner1_X_Hex;
        public SetUint SetAshleyHidingZoneCorner1_X_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner1_Z_Hex;
        public SetUint SetAshleyHidingZoneCorner1_Z_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner2_X_Hex;
        public SetUint SetAshleyHidingZoneCorner2_X_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner2_Z_Hex;
        public SetUint SetAshleyHidingZoneCorner2_Z_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner3_X_Hex;
        public SetUint SetAshleyHidingZoneCorner3_X_Hex;

        public ReturnUint ReturnAshleyHidingZoneCorner3_Z_Hex;
        public SetUint SetAshleyHidingZoneCorner3_Z_Hex;

        public ReturnByteArray ReturnUnknown_SM; // 4 bytes
        public SetByteArray SetUnknown_SM;

        public ReturnByteArray ReturnUnknown_SN; // 4 bytes
        public SetByteArray SetUnknown_SN;

        public ReturnByte ReturnUnknown_SP;
        public SetByte SetUnknown_SP;

        public ReturnByte ReturnUnknown_SQ;
        public SetByte SetUnknown_SQ;

        public ReturnByteArray ReturnUnknown_SR; // 2 bytes
        public SetByteArray SetUnknown_SR;

        public ReturnByteArray ReturnUnknown_SS; // 4 bytes
        public SetByteArray SetUnknown_SS;

        #endregion


        #region Type 0x15 GrappleGun

        //info:
        //GrappleGunStartPoint.X = ObjPointX;
        //GrappleGunStartPoint.Y = ObjPointY;
        //GrappleGunStartPoint.Z = ObjPointZ;
        //GrappleGunStartPoint.W = ObjPointW;


        // Ada Grapple Gun
        public ReturnFloat ReturnGrappleGunEndPointX;
        public SetFloat SetGrappleGunEndPointX;

        public ReturnFloat ReturnGrappleGunEndPointY;
        public SetFloat SetGrappleGunEndPointY;

        public ReturnFloat ReturnGrappleGunEndPointZ;
        public SetFloat SetGrappleGunEndPointZ;

        public ReturnByteArray ReturnGrappleGunEndPointW; // 4 bytes, only classic
        public SetByteArray SetGrappleGunEndPointW;

        public ReturnFloat ReturnGrappleGunThirdPointX;
        public SetFloat SetGrappleGunThirdPointX;

        public ReturnFloat ReturnGrappleGunThirdPointY;
        public SetFloat SetGrappleGunThirdPointY;

        public ReturnFloat ReturnGrappleGunThirdPointZ;
        public SetFloat SetGrappleGunThirdPointZ;

        public ReturnByteArray ReturnGrappleGunThirdPointW; // 4 bytes, only classic
        public SetByteArray SetGrappleGunThirdPointW;

        public ReturnFloat ReturnGrappleGunFacingAngle;
        public SetFloat SetGrappleGunFacingAngle;


        // in Hex
        public ReturnUint ReturnGrappleGunEndPointX_Hex;
        public SetUint SetGrappleGunEndPointX_Hex;

        public ReturnUint ReturnGrappleGunEndPointY_Hex;
        public SetUint SetGrappleGunEndPointY_Hex;

        public ReturnUint ReturnGrappleGunEndPointZ_Hex;
        public SetUint SetGrappleGunEndPointZ_Hex;

        public ReturnUint ReturnGrappleGunThirdPointX_Hex;
        public SetUint SetGrappleGunThirdPointX_Hex;

        public ReturnUint ReturnGrappleGunThirdPointY_Hex;
        public SetUint SetGrappleGunThirdPointY_Hex;

        public ReturnUint ReturnGrappleGunThirdPointZ_Hex;
        public SetUint SetGrappleGunThirdPointZ_Hex;

        public ReturnUint ReturnGrappleGunFacingAngle_Hex;
        public SetUint SetGrappleGunFacingAngle_Hex;


        public ReturnByte ReturnGrappleGunParameter0;
        public SetByte SetGrappleGunParameter0;

        public ReturnByte ReturnGrappleGunParameter1;
        public SetByte SetGrappleGunParameter1;

        public ReturnByte ReturnGrappleGunParameter2;
        public SetByte SetGrappleGunParameter2;

        public ReturnByte ReturnGrappleGunParameter3;
        public SetByte SetGrappleGunParameter3;

        public ReturnByteArray ReturnUnknown_SK; // 4 bytes
        public SetByteArray SetUnknown_SK;

        public ReturnByteArray ReturnUnknown_SL; // 4 bytes
        public SetByteArray SetUnknown_SL;

        #endregion


    }
}
