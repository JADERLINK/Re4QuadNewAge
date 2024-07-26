using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.Enums;
using System.Drawing;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using OpenTK;

namespace Re4QuadExtremeEditor.src.Class.Files
{  /// <summary>
   /// Classe que contem o conteudo extra
   /// </summary>
    public class ExtraGroup
    {
        /// <summary>
        /// listas de associações, dos objetos extras
        /// </summary>
        public Dictionary<ushort, AssociationObj> AssociationList { get; private set; }

        /// <summary>
        /// Id para ser usado para adicionar novos links de objetos;
        /// </summary>
        private ushort IdForNewAssociation = 0;

        /// <summary>
        /// lista de associação dos eventos/itens com os inimigos/etcModel
        /// </summary>
        private Dictionary<RefInteractionTypeKey,List<RefInteractionTypeValue>> RefInteractionTypeList;


        public ExtraGroup()
        {
            AssociationList = new Dictionary<ushort, AssociationObj>();
            RefInteractionTypeList = new Dictionary<RefInteractionTypeKey, List<RefInteractionTypeValue>>();

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
        }

        /// <summary>
        /// classe com os metodos responsaveis pelo oque sera exibido no node;
        /// </summary>
        public NodeDisplayMethods DisplayMethods { get; }

        /// <summary>
        ///  classe com os metodos responsaveis pela movimentação dos objetos e da camera
        /// </summary>
        public NodeMoveMethods MoveMethods { get; }

        #region DisplayMethods
        public string GetNodeText(ushort ID)
        {
            if (AssociationList.ContainsKey(ID))
            {
                ushort TrueId = AssociationList[ID].LineID;
                byte SubId = AssociationList[ID].SubObjID;

                if (AssociationList[ID].FileFormat == SpecialFileFormat.AEV)
                {
                    return "Extra: " + TextSubPart(DataBase.NodeAEV.PropertyMethods, TrueId, SubId);
                }
                else if (AssociationList[ID].FileFormat == SpecialFileFormat.ITA)
                {
                    return "Extra: " + TextSubPart(DataBase.NodeITA.PropertyMethods, TrueId, SubId) + " (From ITA)";
                }
            }
            return "Extras Error Association Code " + ID;
        }

        private string TextSubPart(SpecialMethods methods , ushort ID, byte SubId)
        {
            string r = "[" + methods.ReturnSpecialIndex(ID).ToString("X2") + "] ";

            switch (methods.GetSpecialType(ID))
            {
                case SpecialType.T01_WarpDoor:
                    r += "0x" + Lang.GetAttributeText(aLang.SpecialType01_WarpDoor) + " [R" + methods.ReturnDestinationRoom(ID).ToString("X3") + "]";
                    break;
                case SpecialType.T10_FixedLadderClimbUp:
                    r += "0x" + Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp) +
                        " (Step Count: " + methods.ReturnLadderStepCount(ID)
                        + ") Parameters: [" + methods.ReturnLadderParameter0(ID).ToString("X2") + "] [" +
                        methods.ReturnLadderParameter1(ID).ToString("X2") + "] [" +
                        methods.ReturnLadderParameter2(ID).ToString("X2") + "] [" +
                        methods.ReturnLadderParameter3(ID).ToString("X2") + "]";
                    break;
                case SpecialType.T12_AshleyHideCommand:
                    r += "0x" + Lang.GetAttributeText(aLang.SpecialType12_AshleyHideCommand) + " [" + methods.ReturnUnknown_SP(ID).ToString("X2") + "]";
                    break;
                case SpecialType.T13_LocalTeleportation:
                    r += "0x" + Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation);
                    break;
                case SpecialType.T15_AdaGrappleGun:
                    r += "0x" + Lang.GetAttributeText(aLang.SpecialType15_AdaGrappleGun)
                         + "; Parameters: [" + methods.ReturnGrappleGunParameter0(ID).ToString("X2") + "] [" +
                        methods.ReturnGrappleGunParameter1(ID).ToString("X2") + "] [" +
                        methods.ReturnGrappleGunParameter2(ID).ToString("X2") + "] [" +
                        methods.ReturnGrappleGunParameter3(ID).ToString("X2") + "] ";
                    if (SubId == 0)
                    {
                        r += "(Firt Position)";
                    }
                    else if (SubId == 1)
                    {
                        r += "(Second Position)";
                    }
                    else if (SubId == 2) 
                    {
                        r += "(Third Position)";
                    }
                    break;
                default:
                    r += "0x" + methods.ReturnSpecialType(ID).ToString("X2") + ": " + Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType);
                    break;
            }

            return r;
        }

        public Color GetNodeColor(ushort ID)
        {
            if (!Globals.RenderExtraObjs)
            {
                return Globals.NodeColorHided;
            }
            else if (!Globals.RenderExtraWarpDoor && AssociationList.ContainsKey(ID) && AssociationList[ID].FileFormat == SpecialFileFormat.AEV
                 && DataBase.FileAEV != null && DataBase.FileAEV.Lines.ContainsKey(AssociationList[ID].LineID) &&
                 DataBase.FileAEV.Methods.GetSpecialType(AssociationList[ID].LineID) == SpecialType.T01_WarpDoor)
            {
                return Globals.NodeColorHided;
            }
            else if (!Globals.RenderExtraWarpDoor && AssociationList.ContainsKey(ID) && AssociationList[ID].FileFormat == SpecialFileFormat.ITA
                 && DataBase.FileITA != null && DataBase.FileITA.Lines.ContainsKey(AssociationList[ID].LineID) &&
                 DataBase.FileITA.Methods.GetSpecialType(AssociationList[ID].LineID) == SpecialType.T01_WarpDoor)
            {
                return Globals.NodeColorHided;
            }

            else if (Globals.HideExtraExceptWarpDoor && AssociationList.ContainsKey(ID) && AssociationList[ID].FileFormat == SpecialFileFormat.AEV
                 && DataBase.FileAEV != null && DataBase.FileAEV.Lines.ContainsKey(AssociationList[ID].LineID) &&
                 DataBase.FileAEV.Methods.GetSpecialType(AssociationList[ID].LineID) != SpecialType.T01_WarpDoor)
            {
                return Globals.NodeColorHided;
            }
            else if (Globals.HideExtraExceptWarpDoor && AssociationList.ContainsKey(ID) && AssociationList[ID].FileFormat == SpecialFileFormat.ITA
                 && DataBase.FileITA != null && DataBase.FileITA.Lines.ContainsKey(AssociationList[ID].LineID) &&
                 DataBase.FileITA.Methods.GetSpecialType(AssociationList[ID].LineID) != SpecialType.T01_WarpDoor)
            {
                return Globals.NodeColorHided;
            }

            return Globals.NodeColorEntry;
        }

        #endregion


        #region MoveMethods
        private Vector3 GetObjPostion_ToCamera(ushort ID) 
        {
            ushort TrueId = AssociationList[ID].LineID;
            byte SubId = AssociationList[ID].SubObjID;
            if (AssociationList[ID].FileFormat == SpecialFileFormat.AEV)
            {
                return GetObjPostion_ToCamera_SubPart(DataBase.NodeAEV.ExtrasMethodsForGL, TrueId, SubId);
            }
            else if (AssociationList[ID].FileFormat == SpecialFileFormat.ITA)
            {
                return GetObjPostion_ToCamera_SubPart(DataBase.NodeITA.ExtrasMethodsForGL, TrueId, SubId);
            }
            return Vector3.Zero;
        }

        private Vector3 GetObjPostion_ToCamera_SubPart(ExtrasMethodsForGL methods, ushort ID, byte SubId) 
        {
            Vector3 position = Vector3.Zero;
            switch (methods.GetSpecialType(ID))
            {
                case SpecialType.T01_WarpDoor:
                case SpecialType.T13_LocalTeleportation:
                case SpecialType.T10_FixedLadderClimbUp:
                    position = methods.GetFirstPosition(ID);
                    break;
                case SpecialType.T12_AshleyHideCommand:
                    position = methods.GetAshleyPoint(ID);
                    break;
                case SpecialType.T15_AdaGrappleGun:
                    if (SubId == 0)
                    {
                        position = methods.GetFirstPosition(ID);
                    }
                    else if (SubId == 1)
                    {
                        position = methods.GetGrappleGunEndPosition(ID);
                    }
                    else if (SubId == 2)
                    {
                        position = methods.GetGrappleGunThirdPosition(ID);
                    }
                    break;
            }
            if (float.IsNaN(position.X) || float.IsInfinity(position.X)) { position.X = 0; }
            if (float.IsNaN(position.Y) || float.IsInfinity(position.Y)) { position.Y = 0; }
            if (float.IsNaN(position.Z) || float.IsInfinity(position.Z)) { position.Z = 0; }
            return position;
        }

        private float GetObjAngleY_ToCamera(ushort ID) 
        {
            return 0;
        }

        private Vector3[] GetObjPostion_ToMove_General(ushort ID) 
        {
            ushort TrueId = AssociationList[ID].LineID;
            byte SubId = AssociationList[ID].SubObjID;
            if (AssociationList[ID].FileFormat == SpecialFileFormat.AEV)
            {
                return GetObjPostion_ToMove_General_SubPart(DataBase.NodeAEV.PropertyMethods, TrueId, SubId);
            }
            else if (AssociationList[ID].FileFormat == SpecialFileFormat.ITA)
            {
                return GetObjPostion_ToMove_General_SubPart(DataBase.NodeITA.PropertyMethods, TrueId, SubId);
            }
            return null;       
        }

        private Vector3[] GetObjPostion_ToMove_General_SubPart(SpecialMethods methods, ushort ID, byte SubId) 
        {
            Vector3[] pos = new Vector3[1];
            pos[0] = Vector3.Zero;
            switch (methods.GetSpecialType(ID))
            {
                case SpecialType.T01_WarpDoor:
                case SpecialType.T13_LocalTeleportation:
                case SpecialType.T10_FixedLadderClimbUp:
                    pos[0] = new Vector3(methods.ReturnObjPointX(ID), methods.ReturnObjPointY(ID), methods.ReturnObjPointZ(ID));
                    break;
                case SpecialType.T12_AshleyHideCommand:
                    pos = new Vector3[7];
                    pos[0] = new Vector3(methods.ReturnAshleyHidingPointX(ID), methods.ReturnAshleyHidingPointY(ID), methods.ReturnAshleyHidingPointZ(ID));
                    pos[1] = new Vector3(methods.ReturnAshleyHidingZoneCorner0_X(ID), 0, methods.ReturnAshleyHidingZoneCorner0_Z(ID));
                    pos[2] = new Vector3(methods.ReturnAshleyHidingZoneCorner1_X(ID), 0, methods.ReturnAshleyHidingZoneCorner1_Z(ID));
                    pos[3] = new Vector3(methods.ReturnAshleyHidingZoneCorner2_X(ID), 0, methods.ReturnAshleyHidingZoneCorner2_Z(ID));
                    pos[4] = new Vector3(methods.ReturnAshleyHidingZoneCorner3_X(ID), 0, methods.ReturnAshleyHidingZoneCorner3_Z(ID));
                    
                    //none
                    pos[5] = Vector3.Zero;
                    // center
                    float Xmin = pos[1].X;
                    float Zmin = pos[1].Z;
                    float Xmax = pos[1].X;
                    float Zmax = pos[1].Z;
                    for (int i = 2; i <= 4; i++)
                    {
                        if (pos[i].X < Xmin)
                        {
                            Xmin = pos[i].X;
                        }
                        if (pos[i].Z < Zmin)
                        {
                            Zmin = pos[i].Z;
                        }
                        if (pos[i].X > Xmax)
                        {
                            Xmax = pos[i].X;
                        }
                        if (pos[i].Z > Zmax)
                        {
                            Zmax = pos[i].Z;
                        }
                    }
                    Vector3 center = Vector3.Zero;
                    center.X = Xmin + ((Xmax - Xmin) / 2);
                    center.Z = Zmin + ((Zmax - Zmin) / 2);
                    pos[6] = center;

                    break;
                case SpecialType.T15_AdaGrappleGun:
                    if (SubId == 0)
                    {
                        pos[0] = new Vector3(methods.ReturnObjPointX(ID), methods.ReturnObjPointY(ID), methods.ReturnObjPointZ(ID));
                    }
                    else if (SubId == 1)
                    {
                        pos[0] = new Vector3(methods.ReturnGrappleGunEndPointX(ID), methods.ReturnGrappleGunEndPointY(ID), methods.ReturnGrappleGunEndPointZ(ID));
                    }
                    else if (SubId == 2)
                    {
                        pos[0] = new Vector3(methods.ReturnGrappleGunThirdPointX(ID), methods.ReturnGrappleGunThirdPointY(ID), methods.ReturnGrappleGunThirdPointZ(ID));
                    }
                    break;
            }
            Utils.ToMoveCheckLimits(ref pos);
            return pos;
        }

        private void SetObjPostion_ToMove_General(ushort ID, Vector3[] value) 
        {
            ushort TrueId = AssociationList[ID].LineID;
            byte SubId = AssociationList[ID].SubObjID;
            if (AssociationList[ID].FileFormat == SpecialFileFormat.AEV)
            {
                SetObjPostion_ToMove_General_SubPart(DataBase.NodeAEV.PropertyMethods, TrueId, SubId, value);
            }
            else if (AssociationList[ID].FileFormat == SpecialFileFormat.ITA)
            {
                SetObjPostion_ToMove_General_SubPart(DataBase.NodeITA.PropertyMethods, TrueId, SubId,  value);
            }
        }

        private void SetObjPostion_ToMove_General_SubPart(SpecialMethods methods, ushort ID, byte SubId, Vector3[] value)
        {
            if (value != null && value.Length >= 1)
            {
                switch (methods.GetSpecialType(ID))
                {
                    case SpecialType.T01_WarpDoor:
                    case SpecialType.T13_LocalTeleportation:
                    case SpecialType.T10_FixedLadderClimbUp:
                        methods.SetObjPointX(ID, value[0].X);
                        methods.SetObjPointY(ID, value[0].Y);
                        methods.SetObjPointZ(ID, value[0].Z);
                        break;
                    case SpecialType.T12_AshleyHideCommand:
                        methods.SetAshleyHidingPointX(ID, value[0].X);
                        methods.SetAshleyHidingPointY(ID, value[0].Y);
                        methods.SetAshleyHidingPointZ(ID, value[0].Z);
                        if (value.Length >= 5)
                        {
                            methods.SetAshleyHidingZoneCorner0_X(ID, value[1].X);
                            methods.SetAshleyHidingZoneCorner0_Z(ID, value[1].Z);
                            methods.SetAshleyHidingZoneCorner1_X(ID, value[2].X);
                            methods.SetAshleyHidingZoneCorner1_Z(ID, value[2].Z);
                            methods.SetAshleyHidingZoneCorner2_X(ID, value[3].X);
                            methods.SetAshleyHidingZoneCorner2_Z(ID, value[3].Z);
                            methods.SetAshleyHidingZoneCorner3_X(ID, value[4].X);
                            methods.SetAshleyHidingZoneCorner3_Z(ID, value[4].Z);
                        }
                        break;
                    case SpecialType.T15_AdaGrappleGun:
                        if (SubId == 0)
                        {
                            methods.SetObjPointX(ID, value[0].X);
                            methods.SetObjPointY(ID, value[0].Y);
                            methods.SetObjPointZ(ID, value[0].Z);
                        }
                        else if (SubId == 1)
                        {
                            methods.SetGrappleGunEndPointX(ID, value[0].X);
                            methods.SetGrappleGunEndPointY(ID, value[0].Y);
                            methods.SetGrappleGunEndPointZ(ID, value[0].Z);
                        }
                        else if (SubId == 2)
                        {
                            methods.SetGrappleGunThirdPointX(ID, value[0].X);
                            methods.SetGrappleGunThirdPointY(ID, value[0].Y);
                            methods.SetGrappleGunThirdPointZ(ID, value[0].Z);
                        }
                        break;
                }
            }

        }

        private Vector3[] GetObjRotationAngles_ToMove(ushort ID) 
        {
            ushort TrueId = AssociationList[ID].LineID;
            if (AssociationList[ID].FileFormat == SpecialFileFormat.AEV)
            {
                return GetObjRotarionAngles_ToMove_SubPart(DataBase.NodeAEV.PropertyMethods, TrueId);
            }
            else if (AssociationList[ID].FileFormat == SpecialFileFormat.ITA)
            {
                return GetObjRotarionAngles_ToMove_SubPart(DataBase.NodeITA.PropertyMethods, TrueId);
            }
            return null;
        }

        private Vector3[] GetObjRotarionAngles_ToMove_SubPart(SpecialMethods methods, ushort ID) 
        {
            Vector3[] angle = new Vector3[1];
            angle[0] = Vector3.Zero;
            switch (methods.GetSpecialType(ID))
            {
                case SpecialType.T01_WarpDoor:
                    angle[0] = new Vector3(0, methods.ReturnDestinationFacingAngle(ID), 0);
                    break;
                case SpecialType.T13_LocalTeleportation:
                    angle[0] = new Vector3(0, methods.ReturnTeleportationFacingAngle(ID), 0);
                    break;
                case SpecialType.T10_FixedLadderClimbUp:
                    angle[0] = new Vector3(0, methods.ReturnLadderFacingAngle(ID), 0);
                    break;
                case SpecialType.T15_AdaGrappleGun:
                    angle[0] = new Vector3(0, methods.ReturnGrappleGunFacingAngle(ID), 0);
                    break;
            }
            Utils.ToMoveCheckLimits(ref angle);
            return angle;
        }

        private void SetObjRotationAngles_ToMove(ushort ID, Vector3[] value) 
        {
            ushort TrueId = AssociationList[ID].LineID;
            if (AssociationList[ID].FileFormat == SpecialFileFormat.AEV)
            {
                SetObjRotarionAngles_ToMove_SubPart(DataBase.NodeAEV.PropertyMethods, TrueId, value);
            }
            else if (AssociationList[ID].FileFormat == SpecialFileFormat.ITA)
            {
                SetObjRotarionAngles_ToMove_SubPart(DataBase.NodeITA.PropertyMethods, TrueId, value);
            }
        }

        private void SetObjRotarionAngles_ToMove_SubPart(SpecialMethods methods, ushort ID, Vector3[] value) 
        {
            if (value != null && value.Length >= 1)
            {
                switch (methods.GetSpecialType(ID))
                {
                    case SpecialType.T01_WarpDoor:
                        methods.SetDestinationFacingAngle(ID, value[0].Y);
                        break;
                    case SpecialType.T13_LocalTeleportation:
                        methods.SetTeleportationFacingAngle(ID, value[0].Y);
                        break;
                    case SpecialType.T10_FixedLadderClimbUp:
                        methods.SetLadderFacingAngle(ID, value[0].Y);
                        break;
                    case SpecialType.T15_AdaGrappleGun:
                        methods.SetGrappleGunFacingAngle(ID, value[0].Y);
                        break;
                }
            }
        }
        #endregion


        #region // extra objs action

        public void ClearAll()
        {
            DataBase.NodeEXTRAS.Nodes.Clear();
            AssociationList.Clear();
            IdForNewAssociation = 0;
        }

        public void ClearITAs() 
        {
            var ITAs =
               (from obj in AssociationList
                where obj.Value.FileFormat == SpecialFileFormat.ITA
                select obj).ToDictionary(k => k.Key, v => v.Value);

            foreach (var item in ITAs)
            {
                DataBase.NodeEXTRAS.Nodes.RemoveByKey(item.Key.ToString());
                AssociationList.Remove(item.Key);
            }

        }

        public void RemoveObj(ushort ExtraID)
        {
            DataBase.NodeEXTRAS.Nodes.RemoveByKey(ExtraID.ToString());
            AssociationList.Remove(ExtraID);
        }

        public void RemoveObj(ushort LineID, SpecialFileFormat FileFormat)
        {
            var r =
                 (from obj in AssociationList
                  where obj.Value.LineID == LineID && obj.Value.FileFormat == FileFormat
                  select obj).ToDictionary(k => k.Key, v => v.Value);

            foreach (var item in r)
            {
                DataBase.NodeEXTRAS.Nodes.RemoveByKey(item.Key.ToString());
                AssociationList.Remove(item.Key);
            }
        }

        public void AddAll() 
        {
            foreach (Object3D item in DataBase.NodeAEV.Nodes)
            {
                AddAllSubPart(item, SpecialFileFormat.AEV);
            }

            foreach (Object3D item in DataBase.NodeITA.Nodes)
            {
                AddAllSubPart(item, SpecialFileFormat.ITA);
            }
        }

        public void AddITAs() 
        {
            foreach (Object3D item in DataBase.NodeITA.Nodes)
            {
                AddAllSubPart(item, SpecialFileFormat.ITA);
            }
        }

        private void AddAllSubPart(Object3D item, SpecialFileFormat FileFormat) 
        {
            ushort ID = item.ObjLineRef;
            //SpecialFileType FileType = ((SpecialNodeGroup)item.Parent).PropertyMethods.GetSpecialFileType();
            SpecialType type = ((SpecialNodeGroup)item.Parent).PropertyMethods.GetSpecialType(ID);
            if (type == SpecialType.T01_WarpDoor 
             || type == SpecialType.T13_LocalTeleportation
             || type == SpecialType.T10_FixedLadderClimbUp
             || type == SpecialType.T12_AshleyHideCommand)
            {
                AssociationObj n = new AssociationObj(FileFormat, ID, 0);
                AssociationList.Add(IdForNewAssociation, n);
                DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
               IdForNewAssociation++;
            }
            else if (type == SpecialType.T15_AdaGrappleGun)
            {
                AssociationObj n0 = new AssociationObj(FileFormat, ID, 0);
                AssociationObj n1 = new AssociationObj(FileFormat, ID, 1);
                AssociationObj n2 = new AssociationObj(FileFormat, ID, 2);

                AssociationList.Add(IdForNewAssociation, n0);
                DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
                IdForNewAssociation++;

                AssociationList.Add(IdForNewAssociation, n1);
                DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
                IdForNewAssociation++;

                AssociationList.Add(IdForNewAssociation, n2);
                DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
                IdForNewAssociation++;
            }
        }

        private Object3D AddExtraNode(ushort ExtraID) 
        {
            Object3D o = Object3D.CreateNewInstance(GroupType.EXTRAS, ExtraID);
            o.NodeFont = Globals.TreeNodeFontText;
            return o;
        }

        public void AddNewObj(ushort LineID, SpecialFileFormat FileFormat) 
        {
            AssociationObj n = new AssociationObj(FileFormat, LineID, 0);
            AssociationList.Add(IdForNewAssociation, n);
            DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
            IdForNewAssociation++;

        }

        public void AddNewObjGrappleGun(ushort LineID, SpecialFileFormat FileFormat) 
        {
            AssociationObj n0 = new AssociationObj(FileFormat, LineID, 0);
            AssociationObj n1 = new AssociationObj(FileFormat, LineID, 1);
            AssociationObj n2 = new AssociationObj(FileFormat, LineID, 2);

            AssociationList.Add(IdForNewAssociation, n0);
            DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
            IdForNewAssociation++;

            AssociationList.Add(IdForNewAssociation, n1);
            DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
            IdForNewAssociation++;

            AssociationList.Add(IdForNewAssociation, n2);
            DataBase.NodeEXTRAS.Nodes.Add(AddExtraNode(IdForNewAssociation));
            IdForNewAssociation++;
        }

        public AssociationObj GetAssociationObj(ushort ExtraID) 
        {
            if (AssociationList.ContainsKey(ExtraID))
            {
                return AssociationList[ExtraID];
            }
            return new AssociationObj();
        }

        public ushort[] GetExtraIDs(ushort ID, SpecialFileFormat FileFormat)
        {
            return (from obj in AssociationList
                    where obj.Value.LineID == ID && obj.Value.FileFormat == FileFormat
                    select obj.Key).ToArray();
        }

        public void UpdateExtraNodes(ushort LineID, SpecialType NewSpecialType, SpecialFileFormat FileFormat)
        {
            RemoveObj(LineID, FileFormat);
            if (NewSpecialType == SpecialType.T01_WarpDoor
             || NewSpecialType == SpecialType.T13_LocalTeleportation
             || NewSpecialType == SpecialType.T10_FixedLadderClimbUp
             || NewSpecialType == SpecialType.T12_AshleyHideCommand)
            {
                AddNewObj(LineID, FileFormat);
            }
            else if (NewSpecialType == SpecialType.T15_AdaGrappleGun)
            {
                AddNewObjGrappleGun(LineID, FileFormat);
            }
        }

        #endregion


        #region RefInteractionType manager

        public void SetStartRefInteractionTypeContent()
        {
            RefInteractionTypeList.Clear();

            if (DataBase.FileAEV != null)
            {
                foreach (ushort lineID in DataBase.FileAEV.Lines.Keys)
                {
                    RefInteractionType objType = DataBase.FileAEV.Methods.GetRefInteractionType(lineID);
                    ushort objID = DataBase.FileAEV.Methods.ReturnRefInteractionIndex(lineID);

                    if (objType == RefInteractionType.Enemy || objType == RefInteractionType.EtcModel)
                    {
                        RefInteractionTypeListAdd(objType, objID, SpecialFileFormat.AEV, lineID);
                    }
                }
            }

            if (DataBase.FileITA != null)
            {
                foreach (ushort lineID in DataBase.FileITA.Lines.Keys)
                {
                    RefInteractionType objType = DataBase.FileITA.Methods.GetRefInteractionType(lineID);
                    ushort objID = DataBase.FileITA.Methods.ReturnRefInteractionIndex(lineID);

                    if (objType == RefInteractionType.Enemy || objType == RefInteractionType.EtcModel)
                    {
                        RefInteractionTypeListAdd(objType, objID, SpecialFileFormat.ITA, lineID);
                    }
                }

            }

        }

        public void RefInteractionTypeListAdd(RefInteractionType objType, ushort objID, SpecialFileFormat fileFormat, ushort lineID) 
        {
            RefInteractionTypeKey key = new RefInteractionTypeKey(objType, objID);
            RefInteractionTypeValue value = new RefInteractionTypeValue(fileFormat, lineID);

            if (RefInteractionTypeList.ContainsKey(key))
            {
                RefInteractionTypeList[key].Add(value);
            }
            else 
            {
                List<RefInteractionTypeValue> list = new List<RefInteractionTypeValue>();
                list.Add(value);
                RefInteractionTypeList.Add(key, list);
            }
        }

        public void RefInteractionTypeListRemove(RefInteractionType objType, ushort objID, SpecialFileFormat fileFormat, ushort lineID) 
        {
            RefInteractionTypeKey key = new RefInteractionTypeKey(objType, objID);
            RefInteractionTypeValue value = new RefInteractionTypeValue(fileFormat, lineID);
            if (RefInteractionTypeList.ContainsKey(key))
            {
                RefInteractionTypeList[key].RemoveAll(o => o.Equals(value));
                if (RefInteractionTypeList[key].Count == 0)
                {
                    RefInteractionTypeList.Remove(key);
                }
            }
        }

        public void UpdateRefInteractionTypeList(SpecialFileFormat FileFormat, ushort lineID, RefInteractionType oldObjType, RefInteractionType newObjType, ushort objID) 
        {
            RefInteractionTypeListRemove(oldObjType, objID, FileFormat, lineID);
            RefInteractionTypeListAdd(newObjType, objID, FileFormat, lineID);
        }

        public void UpdateRefInteractionTypeList(SpecialFileFormat FileFormat, ushort lineID, RefInteractionType objType, ushort oldObjID, ushort newObjID)
        {
            RefInteractionTypeListRemove(objType, oldObjID, FileFormat, lineID);
            RefInteractionTypeListAdd(objType, newObjID, FileFormat, lineID);
        }


        #endregion


        #region RefInteractionType returns

        public string AssociatedSpecialEventFromSpecialIndex(RefInteractionType objType, ushort objID) 
        {
            RefInteractionTypeValue[] value = GetRefInteractionTypeValue(objType, objID);
            if (value != null && value.Length >= 1)
            {
                SpecialMethods methods = AssociatedSubPartGetSpecialMethods(value[0].FileFormat);
                string r = "0x" + methods.ReturnSpecialIndex(value[0].LineID).ToString("X2");

                for (int i = 1; i < value.Length; i++)
                {
                    SpecialMethods methodsI = AssociatedSubPartGetSpecialMethods(value[i].FileFormat);
                    r += ", 0x" + methods.ReturnSpecialIndex(value[i].LineID).ToString("X2");
                }
                return r;
            }
            return null;
        }


        public string AssociatedSpecialEventFromFile(RefInteractionType objType, ushort objID) 
        {
            RefInteractionTypeValue[] value = GetRefInteractionTypeValue(objType, objID);
            if (value != null && value.Length >= 1)
            {
                string r = value[0].FileFormat.ToString();
                for (int i = 1; i < value.Length; i++)
                {
                    r += ", " + value[i].FileFormat.ToString();
                }
                return r;
            }
            return null;
        }

        public string AssociatedSpecialEventType(RefInteractionType objType, ushort objID)
        {
            RefInteractionTypeValue[] value = GetRefInteractionTypeValue(objType, objID);
            if (value != null && value.Length >= 1)
            {
                string r = "";
                SpecialType? specialType = AssociatedSubPartGetSpecialFileType(value[0].FileFormat, value[0].LineID);

                if (specialType != null && specialType != SpecialType.UnspecifiedType && ListBoxProperty.SpecialTypeList.ContainsKey((SpecialType)specialType))
                {
                    r = ListBoxProperty.SpecialTypeList[(SpecialType)specialType].Description;
                }
                else
                {
                    r = Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType);
                }

                for (int i = 1; i < value.Length; i++)
                {
                    specialType = AssociatedSubPartGetSpecialFileType(value[i].FileFormat, value[i].LineID);

                    if (specialType != null && specialType != SpecialType.UnspecifiedType && ListBoxProperty.SpecialTypeList.ContainsKey((SpecialType)specialType))
                    {
                        r += ", " + ListBoxProperty.SpecialTypeList[(SpecialType)specialType].Description;
                    }
                    else
                    {
                        r += ", " + Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType);
                    }

                }

                return r;
            }
            return null;
        }

        private SpecialType? AssociatedSubPartGetSpecialFileType(SpecialFileFormat fileFormat, ushort lineID) 
        {
            if (fileFormat == SpecialFileFormat.ITA && DataBase.FileITA != null)
            {
                return DataBase.FileITA.Methods.GetSpecialType(lineID);
            }
            else if (fileFormat == SpecialFileFormat.AEV && DataBase.FileAEV != null)
            {
                return DataBase.FileAEV.Methods.GetSpecialType(lineID);
            }
            return null;
        }

        private SpecialMethods AssociatedSubPartGetSpecialMethods(SpecialFileFormat fileFormat) 
        {
            if (fileFormat == SpecialFileFormat.ITA && DataBase.FileITA != null)
            {
                return DataBase.FileITA.Methods;
            }
            else if (fileFormat == SpecialFileFormat.AEV && DataBase.FileAEV != null)
            {
                return DataBase.FileAEV.Methods;
            }
            return null;
        }

        public string AssociatedSpecialEventObjName(RefInteractionType objType, ushort objID) 
        {
            RefInteractionTypeValue[] value = GetRefInteractionTypeValue(objType, objID);
            if (value != null && value.Length >= 1)
            {
                SpecialMethods methods = AssociatedSubPartGetSpecialMethods(value[0].FileFormat);
                string r = AssociatedSpecialEventObjNameSubPart(methods, value[0].LineID);
                for (int i = 1; i < value.Length; i++)
                {
                    SpecialMethods methodsI = AssociatedSubPartGetSpecialMethods(value[i].FileFormat);
                    r += ", " + AssociatedSpecialEventObjNameSubPart(methodsI, value[i].LineID);
                }
                return r;
            }   
            return null;
        }

        private string AssociatedSpecialEventObjNameSubPart(SpecialMethods methods, ushort lineID) 
        {
            SpecialType specialType = methods.GetSpecialType(lineID);
            switch (specialType)
            {
                case SpecialType.T00_GeneralPurpose:
                    return Lang.GetAttributeText(aLang.SpecialType00_GeneralPurpose);
                case SpecialType.T01_WarpDoor:
                    return Lang.GetAttributeText(aLang.SpecialType01_WarpDoor) + " [R" + methods.ReturnDestinationRoom(lineID).ToString("X3") + "]";
                case SpecialType.T02_CutSceneEvents:
                    return Lang.GetAttributeText(aLang.SpecialType02_CutSceneEvents);
                case SpecialType.T03_Items:
                    ushort itemNumber = methods.ReturnItemNumber(lineID);
                    string r03 = "0x" + itemNumber.ToString("X4") + ": ";
                        if (DataBase.ItemsIDs.List.ContainsKey(itemNumber))
                        {
                            r03 += DataBase.ItemsIDs.List[itemNumber].Name;
                        }
                        else
                        {
                            r03 += Lang.GetAttributeText(aLang.ListBoxUnknownItem);
                        }
                    return r03;
                case SpecialType.T04_GroupedEnemyTrigger:
                    return Lang.GetAttributeText(aLang.SpecialType04_GroupedEnemyTrigger) + " [" + methods.ReturnEnemyGroup(lineID).ToString("X4") + "]";
                case SpecialType.T05_Message:
                    return Lang.GetAttributeText(aLang.SpecialType05_Message)
                            + " [" + methods.ReturnRoomMessage(lineID).ToString("X4") + "] [" +
                            methods.ReturnMessageCutSceneID(lineID).ToString("X4") + "] [" +
                            methods.ReturnMessageID(lineID).ToString("X4") + "]";
                case SpecialType.T08_TypeWriter:
                    return Lang.GetAttributeText(aLang.SpecialType08_TypeWriter);
                case SpecialType.T0A_DamagesThePlayer:
                    return Lang.GetAttributeText(aLang.SpecialType0A_DamagesThePlayer);
                case SpecialType.T0B_FalseCollision:
                    return Lang.GetAttributeText(aLang.SpecialType0B_FalseCollision);
                case SpecialType.T0D_FieldInfo:
                    return Lang.GetAttributeText(aLang.SpecialType0D_FieldInfo);
                case SpecialType.T0E_Crouch:
                    return Lang.GetAttributeText(aLang.SpecialType0E_Crouch);
                case SpecialType.T10_FixedLadderClimbUp:
                    return Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp);
                case SpecialType.T11_ItemDependentEvents:
                    ushort ItemDependentEventsID = methods.ReturnNeededItemNumber(lineID);
                    string r11 = Lang.GetAttributeText(aLang.SpecialType11_ItemDependentEvents) + "; 0x" + ItemDependentEventsID.ToString("X4") + ": ";
                    if (DataBase.ItemsIDs.List.ContainsKey(ItemDependentEventsID))
                    {
                        r11 += DataBase.ItemsIDs.List[ItemDependentEventsID].Name;
                    }
                    else
                    {
                        r11 += Lang.GetAttributeText(aLang.ListBoxUnknownItem);
                    }
                    return r11;
                case SpecialType.T12_AshleyHideCommand:
                    return Lang.GetAttributeText(aLang.SpecialType12_AshleyHideCommand);
                case SpecialType.T13_LocalTeleportation:
                    return Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation);
                case SpecialType.T14_UsedForElevators:
                    return Lang.GetAttributeText(aLang.SpecialType14_UsedForElevators);
                case SpecialType.T15_AdaGrappleGun:
                    return Lang.GetAttributeText(aLang.SpecialType15_AdaGrappleGun);
                case SpecialType.T06_Unused:
                case SpecialType.T07_Unused:
                case SpecialType.T09_Unused:
                case SpecialType.T0C_Unused:
                case SpecialType.T0F_Unused:
                case SpecialType.UnspecifiedType:
                    return Lang.GetAttributeText(aLang.SpecialTypeUnspecifiedType);
            }
            return null;
        }


        private RefInteractionTypeValue[] GetRefInteractionTypeValue(RefInteractionType objType, ushort objID) 
        {
            RefInteractionTypeKey key = new RefInteractionTypeKey(objType, objID);
            if (RefInteractionTypeList.ContainsKey(key) && RefInteractionTypeList[key].Count != 0)
            {
                return RefInteractionTypeList[key].ToArray();
            }
            return null;
        }

        #endregion
    }


    public struct AssociationObj
    {
        public ushort LineID { get; }
        public SpecialFileFormat FileFormat { get; }
        public byte SubObjID { get; }

        public AssociationObj(SpecialFileFormat FileFormat, ushort LineID, byte SubObjID = 0) 
        {
            this.FileFormat = FileFormat;
            this.LineID = LineID;
            this.SubObjID = SubObjID;
        }

        public override bool Equals(object obj)
        {
            return (obj is AssociationObj o && o.FileFormat == FileFormat && o.LineID == LineID && o.SubObjID == SubObjID);
        }

        public override int GetHashCode()
        {
            return (FileFormat.ToString()+ "-" + LineID.ToString() + "-" + SubObjID.ToString()).GetHashCode();
        }
    }

    public struct RefInteractionTypeKey 
    {
        public RefInteractionType ObjType { get; }
        public ushort ObjID { get; }

        public RefInteractionTypeKey(RefInteractionType ObjType, ushort ObjID) 
        {
            this.ObjType = ObjType;
            this.ObjID = ObjID;

        }
        public override bool Equals(object obj)
        {
            return obj is RefInteractionTypeKey key && key.ObjID == ObjID && key.ObjType == ObjType;
        }
        public override int GetHashCode()
        {
            return ((byte)ObjType * 0x10000) + ObjID;
        }
    }

    public struct RefInteractionTypeValue
    {
        public SpecialFileFormat FileFormat { get; }
        public ushort LineID { get; }

        public RefInteractionTypeValue(SpecialFileFormat FileFormat, ushort LineID)
        {
            this.FileFormat = FileFormat;
            this.LineID = LineID;

        }
        public override bool Equals(object obj)
        {
            return obj is RefInteractionTypeValue key && key.LineID == LineID && key.FileFormat == FileFormat;
        }
        public override int GetHashCode()
        {
            return ((int)FileFormat * 0x10000) + LineID;
        }

    }
}
