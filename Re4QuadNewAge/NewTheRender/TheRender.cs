using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Re4QuadExtremeEditor.src;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Shaders;


namespace NewAgeTheRender
{
    /// <summary>
    /// Classe destinada a renderizar tudo no cenario (No ambiente GL);
    /// </summary>
    public static class TheRender
    {
        private static readonly Vector3 boundNoneEnemy = new Vector3(3f, 4f, 3f);
        private static readonly Vector3 boundNoneEtcModel = new Vector3(3f, 3f, 3f);
        private static readonly Vector3 boundNoneItem = new Vector3(1.5f, 1.5f, 1.5f);
        private static readonly Vector3 boundNoneExtras = new Vector3(2f, 2f, 2f);

        public static void AllRender(ref Matrix4 camMtx, ref Matrix4 ProjMatrix, Vector3 camPos, float objY, bool IsSelectMode = false)
        {
            if (IsSelectMode)
            {
                GL.ClearColor(Color.White);
            }
            else 
            {
                GL.ClearColor(Globals.SkyColor);
            }
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DataShader.ShaderBoundaryBox.Use();
            DataShader.ShaderBoundaryBox.SetMatrix4("view", camMtx);
            DataShader.ShaderBoundaryBox.SetMatrix4("projection", ProjMatrix);

            DataShader.ShaderTriggerZoneBox.Use();
            DataShader.ShaderTriggerZoneBox.SetMatrix4("view", camMtx);
            DataShader.ShaderTriggerZoneBox.SetMatrix4("projection", ProjMatrix);

            DataShader.ShaderTriggerZoneCircle.Use();
            DataShader.ShaderTriggerZoneCircle.SetMatrix4("view", camMtx);
            DataShader.ShaderTriggerZoneCircle.SetMatrix4("projection", ProjMatrix);

            DataShader.ShaderPlaneZone.Use();
            DataShader.ShaderPlaneZone.SetMatrix4("view", camMtx);
            DataShader.ShaderPlaneZone.SetMatrix4("projection", ProjMatrix);

            DataShader.ShaderItemTrigggerRadius.Use();
            DataShader.ShaderItemTrigggerRadius.SetMatrix4("view", camMtx);
            DataShader.ShaderItemTrigggerRadius.SetMatrix4("projection", ProjMatrix);

            if (IsSelectMode == false && Globals.CamGridEnable && Globals.CamGridvalue != 0) //render mode
            {
                DataShader.ShaderGrid.Use();
                DataShader.ShaderGrid.SetMatrix4("view", camMtx);
                DataShader.ShaderGrid.SetMatrix4("projection", ProjMatrix);
                Grid.RenderViewer(objY, Globals.CamGridvalue, Globals.GL_ColorGrid);
            }

            if (IsSelectMode == true && Globals.RenderRoom && DataBase.SelectedRoom != null) // select mode
            {
                DataShader.ShaderRoomSelectMode.Use();
                DataShader.ShaderRoomSelectMode.SetMatrix4("view", camMtx);
                DataShader.ShaderRoomSelectMode.SetMatrix4("projection", ProjMatrix);
                DataBase.SelectedRoom.Render_Solid();
            }

            //select mode box
            if (IsSelectMode)
            {
                RenderEnemyESL(RenderMode.SelectMode);
                RenderExtras(RenderMode.SelectMode);
                RenderITA_TriggerZone(RenderMode.SelectMode);
                RenderAEV_TriggerZone(RenderMode.SelectMode);
                RenderITA_ItemObj(RenderMode.SelectMode);
                RenderAEV_ItemObj(RenderMode.SelectMode);
                RenderEtcModelETS(RenderMode.SelectMode);
            }
            else // box mode
            {
                RenderEnemyESL(RenderMode.BoxMode);
                RenderExtras(RenderMode.BoxMode);
                RenderITA_TriggerZone(RenderMode.BoxMode);
                RenderAEV_TriggerZone(RenderMode.BoxMode);
                RenderITA_ItemObj(RenderMode.BoxMode);
                RenderAEV_ItemObj(RenderMode.BoxMode);
                RenderEtcModelETS(RenderMode.BoxMode);
            }

 
            if (IsSelectMode == false && Globals.RenderRoom && DataBase.SelectedRoom != null)
            {
                DataShader.ShaderRoom.Use();
                DataShader.ShaderRoom.SetMatrix4("view", camMtx);
                DataShader.ShaderRoom.SetMatrix4("projection", ProjMatrix);
                DataShader.ShaderRoom.SetVector3("CameraPosition", camPos);
                DataBase.SelectedRoom.Render();
            }

            if (IsSelectMode == false) // render model
            {
                DataShader.ShaderObjModel.Use();
                DataShader.ShaderObjModel.SetMatrix4("view", camMtx);
                DataShader.ShaderObjModel.SetMatrix4("projection", ProjMatrix);
                DataShader.ShaderObjModel.SetVector3("CameraPosition", camPos);

                ObjModel3D.PreRender();

                RenderExtras(RenderMode.ModelMode);
                RenderEnemyESL(RenderMode.ModelMode);
                RenderITA_ItemObj(RenderMode.ModelMode);
                RenderAEV_ItemObj(RenderMode.ModelMode);
                RenderEtcModelETS(RenderMode.ModelMode);

                ObjModel3D.PosRender();

                //final, transparencia da triggerzone
                RenderPosTriggerZoneBox();
            }

            GL.Finish();
        }

        private static void RenderEnemyESL(RenderMode mode)
        {
            if (Globals.RenderEnemyESL)
            {
                foreach (TreeNode item in DataBase.NodeESL.Nodes)
                {
                    ushort ID = ((Object3D)item).ObjLineRef;
                    ushort EnemiesID = DataBase.NodeESL.MethodsForGL.GetEnemyModelID(ID);
                    ushort EnemyRoom = DataBase.NodeESL.MethodsForGL.GetEnemyRoom(ID);
                    byte EnableState = DataBase.NodeESL.MethodsForGL.GetEnableState(ID);

                    byte[] partColor = BitConverter.GetBytes(ID);
                    Vector4 useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.ESL / 255f, 1f);

                    if ((Globals.RenderDisabledEnemy || EnableState != 0)
                      && (Globals.RenderDontShowOnlyDefinedRoom || EnemyRoom == Globals.RenderEnemyFromDefinedRoom))
                    {
                        if (mode == RenderMode.BoxMode)
                        {
                            useColor = Globals.GL_ColorESL;
                            if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                            {
                                useColor = Globals.GL_ColorSelected;
                            }
                        }

                        RspFix rspFix = new RspFix(
                        Vector3.One, //scale
                        DataBase.NodeESL.MethodsForGL.GetPosition(ID),
                        DataBase.NodeESL.MethodsForGL.GetRotation(ID));

                        if (!DataBase.EnemiesIDs.List.ContainsKey(EnemiesID))
                        {
                            string eId = EnemiesID.ToString("X4");
                            eId = eId[0].ToString() + eId[1].ToString() + "FF";
                            EnemiesID = ushort.Parse(eId, System.Globalization.NumberStyles.HexNumber);
                        }

                        if (DataBase.EnemiesIDs.List.ContainsKey(EnemiesID) && DataBase.EnemiesModels.ContainsKey(DataBase.EnemiesIDs.List[EnemiesID].ObjectModel))
                        {
                            if (mode == RenderMode.ModelMode)
                            {
                                DataBase.EnemiesModels.RenderModel(DataBase.EnemiesIDs.List[EnemiesID].ObjectModel, rspFix);
                            }
                            else if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.BoundingBoxViewer(DataBase.EnemiesModels.GetBoundingBoxLimit(DataBase.EnemiesIDs.List[EnemiesID].ObjectModel), rspFix, useColor);
                            }
                            else if (mode == RenderMode.SelectMode)
                            {
                                RenderAppModel.BoundingBoxToSelect(DataBase.EnemiesModels.GetBoundingBoxLimit(DataBase.EnemiesIDs.List[EnemiesID].ObjectModel), rspFix, useColor);
                            }
                        }
                        else
                        {
                            if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.NoneBoundingBoxViewer(boundNoneEnemy, -boundNoneEnemy, rspFix, useColor);
                            }
                            else if (mode == RenderMode.SelectMode)
                            {
                                RenderAppModel.NoneBoundingBoxToSelect(boundNoneEnemy, -boundNoneEnemy, rspFix, useColor);
                            }

                        }
                    }

                }
            }
        }

        private static void RenderEtcModelETS(RenderMode mode)
        {
            if (Globals.RenderEtcmodelETS)
            {
                foreach (TreeNode item in DataBase.NodeETS.Nodes)
                {
                    ushort ID = ((Object3D)item).ObjLineRef;
                    ushort EtcModelID = DataBase.NodeETS.MethodsForGL.GetEtcModelID(ID);

                    byte[] partColor = BitConverter.GetBytes(ID);
                    Vector4 useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.ETS / 255f, 1f);

                    if (mode == RenderMode.BoxMode)
                    {
                        useColor = Globals.GL_ColorETS;
                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            useColor = Globals.GL_ColorSelected;
                        }
                    }

                    RspFix rspFix = new RspFix(
                        DataBase.NodeETS.MethodsForGL.GetScale(ID),
                        DataBase.NodeETS.MethodsForGL.GetPosition(ID),
                        DataBase.NodeETS.MethodsForGL.GetAngle(ID));

                    if (DataBase.EtcModelIDs.List.ContainsKey(EtcModelID) && DataBase.EtcModels.ContainsKey(DataBase.EtcModelIDs.List[EtcModelID].ObjectModel))
                    {
                        if (mode == RenderMode.ModelMode)
                        {
                            DataBase.EtcModels.RenderModel(DataBase.EtcModelIDs.List[EtcModelID].ObjectModel, rspFix);
                        }
                        else if (mode == RenderMode.BoxMode)
                        {
                            RenderAppModel.BoundingBoxViewer(DataBase.EtcModels.GetBoundingBoxLimit(DataBase.EtcModelIDs.List[EtcModelID].ObjectModel), rspFix, useColor);
                        }
                        else if (mode == RenderMode.SelectMode)
                        {
                            RenderAppModel.BoundingBoxToSelect(DataBase.EtcModels.GetBoundingBoxLimit(DataBase.EtcModelIDs.List[EtcModelID].ObjectModel), rspFix, useColor);
                        }

                    }
                    else
                    {
                        if (mode == RenderMode.BoxMode)
                        {
                            RenderAppModel.NoneBoundingBoxViewer(boundNoneEtcModel, -boundNoneEtcModel, rspFix, useColor);
                        }
                        else if (mode == RenderMode.SelectMode)
                        {
                            RenderAppModel.NoneBoundingBoxToSelect(boundNoneEtcModel, -boundNoneEtcModel, rspFix, useColor);
                        }

                    }

                }
            }
        }


        private static void RenderITA_TriggerZone(RenderMode mode)
        {
            if (Globals.RenderItemsITA)
            {
                foreach (TreeNode item in DataBase.NodeITA.Nodes)
                {
                    RenderSpecial_TriggerZone((Object3D)item, DataBase.NodeITA.MethodsForGL, mode);
                }
            }
        }

        private static void RenderAEV_TriggerZone(RenderMode mode)
        {
            if (Globals.RenderEventsAEV)
            {
                foreach (TreeNode item in DataBase.NodeAEV.Nodes)
                {
                    RenderSpecial_TriggerZone((Object3D)item, DataBase.NodeAEV.MethodsForGL, mode);
                }
            }   
        }

        private static void RenderSpecial_TriggerZone(Object3D item, SpecialMethodsForGL MethodsForGL, RenderMode mode)
        {
            ushort ID = item.ObjLineRef;
            GroupType Group = item.Group;

            byte[] partColor = BitConverter.GetBytes(ID);

            Vector4 mColor = new Vector4(0, 0, 0, 1f);
            Vector4 useColor = new Vector4(0, 0, 0, 1f);

            if (Group == GroupType.ITA)
            {
                useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.ITA / 255f, 1f);
            }
            else if (Group == GroupType.AEV)
            {
                useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.AEV / 255f, 1f);
            }

            if (mode == RenderMode.BoxMode)
            {
                if (Group == GroupType.ITA)
                {
                    mColor = Globals.GL_ColorITA;
                }
                else if (Group == GroupType.AEV)
                {
                    mColor = Globals.GL_ColorAEV;
                }

                if (Globals.UseMoreSpecialColors)
                {
                    mColor = ReturnMoreSpecialColor(MethodsForGL.GetSpecialType(ID), mColor);
                }
            }

            if (MethodsForGL.GetSpecialType(ID) == SpecialType.T03_Items)
            {
                if (Globals.RenderItemTriggerZone)
                {
                    if (mode == RenderMode.BoxMode)
                    {
                        Vector4 TriggerZoneColor = Globals.GL_ColorItemTriggerZone;
                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            TriggerZoneColor = Globals.GL_ColorItemTriggerZoneSelected;
                        }

                        if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category01)
                        {
                            RenderAppModel.TriggerZoneBoxViewer(MethodsForGL.GetTriggerZoneMatrix4(ID), TriggerZoneColor);
                        }
                        else if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category02)
                        {
                            RenderAppModel.TriggerZoneCircleViewer(MethodsForGL.GetTriggerZoneMatrix4(ID), TriggerZoneColor);
                        }

                    }
                    else if (mode == RenderMode.SelectMode)
                    {
                        if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category01)
                        {
                            RenderAppModel.TriggerZoneBoxSolid(MethodsForGL.GetTriggerZoneMatrix4(ID), useColor);
                        }
                        else if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category02)
                        {
                            RenderAppModel.TriggerZoneCircleSolid(MethodsForGL.GetTriggerZoneMatrix4(ID), useColor);
                        }
                    }

                }
            }
            else
            {
                if (Globals.RenderSpecialTriggerZone)
                {
                    if (mode == RenderMode.BoxMode)
                    {
                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode())) 
                        { 
                            mColor = Globals.GL_ColorSelected; 
                        }

                        if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category01)
                        {
                            RenderAppModel.TriggerZoneBoxViewer(MethodsForGL.GetTriggerZoneMatrix4(ID), mColor);
                        }
                        else if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category02)
                        {
                            RenderAppModel.TriggerZoneCircleViewer(MethodsForGL.GetTriggerZoneMatrix4(ID), mColor);
                        }
                    }
                    else if (mode == RenderMode.SelectMode)
                    {
                        if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category01)
                        {
                            RenderAppModel.TriggerZoneBoxSolid(MethodsForGL.GetTriggerZoneMatrix4(ID), useColor);
                        }
                        else if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category02)
                        {
                            RenderAppModel.TriggerZoneCircleSolid(MethodsForGL.GetTriggerZoneMatrix4(ID), useColor);
                        }
                    }
                }
            }
        }


        private static void RenderITA_ItemObj(RenderMode mode)
        {
            if (Globals.RenderItemsITA)
            {
                foreach (TreeNode item in DataBase.NodeITA.Nodes)
                {
                    RenderSpecial_ItemObj((Object3D)item, DataBase.NodeITA.MethodsForGL, mode);
                }
            }         
        }

        private static void RenderAEV_ItemObj(RenderMode mode)
        {
            if (Globals.RenderEventsAEV)
            {
                foreach (TreeNode item in DataBase.NodeAEV.Nodes)
                {
                    RenderSpecial_ItemObj((Object3D)item, DataBase.NodeAEV.MethodsForGL, mode);
                }
            }
        }

        private static void RenderSpecial_ItemObj(Object3D item, SpecialMethodsForGL MethodsForGL, RenderMode mode)
        {
            ushort ID = item.ObjLineRef;
            GroupType Group = item.Group;

            byte[] partColor = BitConverter.GetBytes(ID);

            Vector4 mColor = new Vector4(0, 0, 0, 1f);
            Vector4 useColor = new Vector4(0, 0, 0, 1f);

            if (Group == GroupType.ITA)
            {
                useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.ITA / 255f, 1f);
                mColor = Globals.GL_ColorITA;
            }
            else if (Group == GroupType.AEV)
            {
                useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.AEV / 255f, 1f);
                mColor = Globals.GL_ColorAEV;
            }

            if (MethodsForGL.GetSpecialType(ID) == SpecialType.T03_Items)
            {
                // do objeto item
                if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                {
                    mColor = Globals.GL_ColorSelected;
                }

                RspFix rspFix = new RspFix(Vector3.One, MethodsForGL.GetItemPosition(ID), MethodsForGL.GetItemRotation(ID));

                ushort item_ID = MethodsForGL.GetItemModelID(ID);

                if (DataBase.ItemsIDs.List.ContainsKey(item_ID) && DataBase.ItemsModels.ContainsKey(DataBase.ItemsIDs.List[item_ID].ObjectModel))
                {
                    if (mode == RenderMode.ModelMode)
                    {
                        DataBase.ItemsModels.RenderModel(DataBase.ItemsIDs.List[item_ID].ObjectModel, rspFix);
                    }
                    else if (mode == RenderMode.BoxMode)
                    {
                        RenderAppModel.BoundingBoxViewer(
                            DataBase.ItemsModels.GetBoundingBoxLimit(DataBase.ItemsIDs.List[item_ID].ObjectModel),
                            rspFix, mColor);
                    }
                    else if (mode == RenderMode.SelectMode)
                    {
                        RenderAppModel.BoundingBoxToSelect(
                            DataBase.ItemsModels.GetBoundingBoxLimit(DataBase.ItemsIDs.List[item_ID].ObjectModel),
                            rspFix, useColor);
                    }   
                }
                else
                {
                    if (mode == RenderMode.BoxMode)
                    {
                        RenderAppModel.NoneBoundingBoxViewer(boundNoneItem, -boundNoneItem, rspFix, mColor);
                    }
                    else if (mode == RenderMode.SelectMode)
                    {
                        RenderAppModel.NoneBoundingBoxToSelect(boundNoneItem, -boundNoneItem, rspFix, useColor);
                    }
                }

                if (mode == RenderMode.BoxMode)
                {
                    //RenderItemTriggerRadius
                    float ItemTrigggerRadius = MethodsForGL.GetItemTrigggerRadius(ID);
                    if (Globals.RenderItemTriggerRadius && ItemTrigggerRadius != 0)
                    {
                        Vector4 RadiusColor = Globals.GL_ColorItemTrigggerRadius;
                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            RadiusColor = Globals.GL_ColorItemTrigggerRadiusSelected;
                        }

                        RenderAppModel.ItemTrigggerRadiusViewer(new Vector4(MethodsForGL.GetItemPosition(ID), ItemTrigggerRadius), RadiusColor);
                    }

                }

            }

        }


        private static void RenderExtras(RenderMode mode)
        {
            if (Globals.RenderExtraObjs)
            {
                foreach (TreeNode item in DataBase.NodeEXTRAS.Nodes)
                {
                    ushort ID = ((Object3D)item).ObjLineRef;
                    var association = DataBase.Extras.AssociationList[ID];
                    if (association.FileFormat == SpecialFileFormat.AEV && Globals.RenderEventsAEV)
                    {
                        RenderExtrasSubPart((Object3D)item, DataBase.FileAEV.ExtrasMethodsForGL, association.LineID, association.SubObjID, SpecialFileFormat.AEV, mode);
                    }
                    else if (association.FileFormat == SpecialFileFormat.ITA && Globals.RenderItemsITA)
                    {
                        RenderExtrasSubPart((Object3D)item, DataBase.FileITA.ExtrasMethodsForGL, association.LineID, association.SubObjID, SpecialFileFormat.ITA, mode);
                    }

                }
            }

        }

        private static void RenderExtrasSubPart(Object3D item, ExtrasMethodsForGL MethodsForGL, ushort ID, byte SubId, SpecialFileFormat FileFormat, RenderMode mode)
        {
            SpecialType specialType = MethodsForGL.GetSpecialType(ID);
            ushort ExtraID = item.ObjLineRef;
            byte[] partColor = BitConverter.GetBytes(ExtraID);

            Vector4 useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.EXTRAS / 255f, 1f); // selectmod
            Vector4 mColor = Globals.GL_ColorEXTRAS;

            switch (specialType)
            {
                case SpecialType.T01_WarpDoor:
                    if (Globals.RenderExtraWarpDoor)
                    {
                        if (Globals.UseMoreSpecialColors)
                        {
                            mColor = Globals.GL_MoreColor_T01_DoorWarp;
                        }

                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            mColor = Globals.GL_ColorSelected;
                        }

                        RspFix rspFix = new RspFix(
                        Vector3.One, //scale
                        MethodsForGL.GetFirtPosition(ID),
                        MethodsForGL.GetWarpRotation(ID));

                        if (DataBase.InternalModels.ContainsKey(Consts.ModelKeyWarpPoint))
                        {
                            if (mode == RenderMode.ModelMode)
                            {
                                DataBase.InternalModels.RenderModel(Consts.ModelKeyWarpPoint, rspFix);
                            }
                            else if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.BoundingBoxViewer(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyWarpPoint),
                                    rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode) 
                            {
                                RenderAppModel.BoundingBoxToSelect(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyWarpPoint),
                                    rspFix, useColor);
                            }
                        }
                        else
                        {
                            if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.NoneBoundingBoxViewer(boundNoneExtras, -boundNoneExtras, rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode)
                            {
                                RenderAppModel.NoneBoundingBoxToSelect(boundNoneExtras, -boundNoneExtras, rspFix, useColor);
                            }

                        }
                    }
                    break;
                case SpecialType.T13_LocalTeleportation:
                    if (!Globals.HideExtraExceptWarpDoor)
                    {
                        if (Globals.UseMoreSpecialColors)
                        {
                            mColor = Globals.GL_MoreColor_T13_LocalTeleportation;
                        }

                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            mColor = Globals.GL_ColorSelected;
                        }

                        RspFix rspFix = new RspFix(
                        Vector3.One, //scale
                        MethodsForGL.GetFirtPosition(ID),
                        MethodsForGL.GetLocationAndLadderRotation(ID));

                        if (DataBase.InternalModels.ContainsKey(Consts.ModelKeyLocalTeleportationPoint))
                        {
                            if (mode == RenderMode.ModelMode)
                            {
                                DataBase.InternalModels.RenderModel(Consts.ModelKeyLocalTeleportationPoint, rspFix);
                            }
                            else if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.BoundingBoxViewer(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLocalTeleportationPoint),
                                    rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode) 
                            {
                                RenderAppModel.BoundingBoxToSelect(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLocalTeleportationPoint),
                                    rspFix, useColor);
                            }
   
                        }
                        else
                        {
                            if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.NoneBoundingBoxViewer(boundNoneExtras, -boundNoneExtras, rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode)
                            {
                                RenderAppModel.NoneBoundingBoxToSelect(boundNoneExtras, -boundNoneExtras, rspFix, useColor);
                            }
                           
                        }
                    }
                    break;
                case SpecialType.T10_FixedLadderClimbUp:
                    if (!Globals.HideExtraExceptWarpDoor)
                    {
                        if (Globals.UseMoreSpecialColors)
                        {
                            mColor = Globals.GL_MoreColor_T10_FixedLadderClimbUp;
                        }

                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            mColor = Globals.GL_ColorSelected;
                        }

                        RspFix rspFix = new RspFix(
                        Vector3.One, //scale
                        MethodsForGL.GetFirtPosition(ID),
                        MethodsForGL.GetLocationAndLadderRotation(ID));

                        if (DataBase.InternalModels.ContainsKey(Consts.ModelKeyLadderPoint)
                         && DataBase.InternalModels.ContainsKey(Consts.ModelKeyLadderObj))
                        {
                            //renderiza o X que aparece no chão
                            if (mode == RenderMode.ModelMode)
                            {
                                DataBase.InternalModels.RenderModel(Consts.ModelKeyLadderPoint, rspFix);
                            }
                            else if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.BoundingBoxViewer(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderPoint),
                                    rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode) 
                            {
                                RenderAppModel.BoundingBoxToSelect(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderPoint),
                                    rspFix, useColor);
                            }

                            //renderiza a escada
                            sbyte stepCount = MethodsForGL.GetLadderStepCount(ID);
                            if (stepCount >= 2)
                            {
                                if (mode == RenderMode.ModelMode)
                                {
                                    float maxHeight = DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Y;
                                    DataBase.InternalModels.RenderModel(Consts.ModelKeyLadderObj, rspFix);

                                    for (int i = 1; i < stepCount; i++)
                                    {
                                        Vector3 position = new Vector3(MethodsForGL.GetFirtPosition(ID).X,
                                            MethodsForGL.GetFirtPosition(ID).Y + maxHeight,
                                            MethodsForGL.GetFirtPosition(ID).Z);

                                        RspFix irspFix = new RspFix(
                                        rspFix.Scale, //scale
                                        position,
                                        rspFix.Rotation);

                                        DataBase.InternalModels.RenderModel(Consts.ModelKeyLadderObj, irspFix);

                                        maxHeight += DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Y;
                                    }
                                }

                                Vector3 UpperBoundary = new Vector3(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.X,
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Y * stepCount, //altura correta
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Z);
                                Vector3 LowerBoundary = DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).LowerBoundary;

                                if (mode == RenderMode.BoxMode)
                                {
                                    RenderAppModel.BoundingBoxViewer(new BoundingBoxLimit(LowerBoundary, UpperBoundary), rspFix, mColor);
                                }
                                else if (mode == RenderMode.SelectMode)
                                {
                                    RenderAppModel.BoundingBoxToSelect(new BoundingBoxLimit(LowerBoundary, UpperBoundary), rspFix, useColor);
                                }
                              
                            }
                            else if (stepCount <= -2)
                            {
                                if (mode == RenderMode.ModelMode)
                                {
                                    float minHeight = DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Y;
                                    Vector3 position1 = new Vector3(
                                          MethodsForGL.GetFirtPosition(ID).X,
                                          MethodsForGL.GetFirtPosition(ID).Y - minHeight,
                                          MethodsForGL.GetFirtPosition(ID).Z);


                                    RspFix inrspFix = new RspFix(
                                    rspFix.Scale, //scale
                                    position1,
                                    rspFix.Rotation);

                                    DataBase.InternalModels.RenderModel(Consts.ModelKeyLadderObj, inrspFix);

                                    for (int i = 1; i < -stepCount; i++)
                                    {
                                        minHeight += DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Y;

                                        Vector3 position = new Vector3(
                                            MethodsForGL.GetFirtPosition(ID).X,
                                            MethodsForGL.GetFirtPosition(ID).Y - minHeight,
                                            MethodsForGL.GetFirtPosition(ID).Z);

                                        RspFix irspFix = new RspFix(
                                        rspFix.Scale, //scale
                                        position,
                                        rspFix.Rotation);

                                        DataBase.InternalModels.RenderModel(Consts.ModelKeyLadderObj, irspFix);
                                    }
                                }

                                Vector3 UpperBoundary = new Vector3(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.X,
                               DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).LowerBoundary.Y,
                               DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Z);

                                float _minHeight = DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).UpperBoundary.Y * (-stepCount);
                                Vector3 LowerBoundary = new Vector3(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).LowerBoundary.X,
                                   -_minHeight,
                                   DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderObj).LowerBoundary.Z);

                                if (mode == RenderMode.BoxMode)
                                {
                                    RenderAppModel.BoundingBoxViewer(new BoundingBoxLimit(LowerBoundary, UpperBoundary), rspFix, mColor);
                                }
                                else if (mode == RenderMode.SelectMode) 
                                {
                                    RenderAppModel.BoundingBoxToSelect(new BoundingBoxLimit(LowerBoundary, UpperBoundary), rspFix, useColor);
                                }   
                            }
                            else
                            {
                                if (DataBase.InternalModels.ContainsKey(Consts.ModelKeyLadderError))
                                {
                                    if (mode == RenderMode.ModelMode)
                                    {
                                        DataBase.InternalModels.RenderModel(Consts.ModelKeyLadderError, rspFix);
                                    }
                                    else if (mode == RenderMode.BoxMode)
                                    {
                                        RenderAppModel.BoundingBoxViewer(
                                            DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderError),
                                            rspFix, mColor);
                                    }
                                    else if (mode == RenderMode.SelectMode) 
                                    {
                                        RenderAppModel.BoundingBoxToSelect(
                                            DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyLadderError),
                                            rspFix, useColor);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.NoneBoundingBoxViewer(boundNoneExtras, -boundNoneExtras, rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode)
                            {
                                RenderAppModel.NoneBoundingBoxToSelect(boundNoneExtras, -boundNoneExtras, rspFix, useColor);
                            }
                        }
                    }
                    break;
                case SpecialType.T12_AshleyHideCommand:
                    if (!Globals.HideExtraExceptWarpDoor)
                    {
                        if (Globals.UseMoreSpecialColors)
                        {
                            mColor = Globals.GL_MoreColor_T12_AshleyHideCommand;
                        }

                        if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
                        {
                            mColor = Globals.GL_ColorSelected;
                        }

                        RspFix rspFix = new RspFix(
                        Vector3.One, //scale
                        MethodsForGL.GetAshleyPoint(ID),
                        Matrix4.Identity); //Rotation

                        if (DataBase.InternalModels.ContainsKey(Consts.ModelKeyAshleyPoint))
                        {
                            if (mode == RenderMode.ModelMode)
                            {
                                DataBase.InternalModels.RenderModel(Consts.ModelKeyAshleyPoint, rspFix);
                            }
                            else if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.BoundingBoxViewer(
                                    DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyAshleyPoint),
                                    rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode) 
                            {
                                RenderAppModel.BoundingBoxToSelect(
                                       DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyAshleyPoint),
                                       rspFix, useColor);
                            }
                        }
                        else
                        {
                            if (mode == RenderMode.BoxMode)
                            {
                                RenderAppModel.NoneBoundingBoxViewer(boundNoneExtras, -boundNoneExtras, rspFix, mColor);
                            }
                            else if (mode == RenderMode.SelectMode)
                            {
                                RenderAppModel.NoneBoundingBoxToSelect(boundNoneExtras, -boundNoneExtras, rspFix, useColor);
                            }
                        }

                        // AshleyZone
                        if (mode == RenderMode.BoxMode)
                        {
                            RenderAppModel.PlaneZoneViewer(MethodsForGL.GetAshleyHidingZoneCornerMatrix4(ID), mColor);
                        }
                        else if (mode == RenderMode.SelectMode)
                        {
                            RenderAppModel.PlaneZoneSolid(MethodsForGL.GetAshleyHidingZoneCornerMatrix4(ID), useColor);
                        }
                    }
                    break;
                case SpecialType.T15_AdaGrappleGun:
                    if (!Globals.HideExtraExceptWarpDoor)
                    {
                        if (SubId == 0)
                        {
                            RenderGrappleGun(item, MethodsForGL, ID, SubId, FileFormat, MethodsForGL.GetFirtPosition(ID), mode);
                        }
                        else if (SubId == 1)
                        {
                            RenderGrappleGun(item, MethodsForGL, ID, SubId, FileFormat, MethodsForGL.GetGrappleGunEndPosition(ID), mode);
                        }
                        else if (SubId == 2 && MethodsForGL.GetGrappleGunParameter3(ID) != 0)
                        {
                            RenderGrappleGun(item, MethodsForGL, ID, SubId, FileFormat, MethodsForGL.GetGrappleGunThirdPosition(ID), mode);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private static void RenderGrappleGun(Object3D item, ExtrasMethodsForGL MethodsForGL, ushort ID, byte SubId, SpecialFileFormat FileFormat, Vector3 position, RenderMode mode)
        {
            ushort ExtraID = item.ObjLineRef;
            byte[] partColor = BitConverter.GetBytes(ExtraID);

            Vector4 useColor = new Vector4(partColor[0] / 255f, partColor[1] / 255f, (byte)GroupType.EXTRAS / 255f, 1f);

            Vector4 mColor = Globals.GL_ColorEXTRAS;
            if (Globals.UseMoreSpecialColors)
            {
                mColor = Globals.GL_MoreColor_T15_AdaGrappleGun;
            }

            if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode()))
            {
                mColor = Globals.GL_ColorSelected;
            }

            RspFix rspFix = new RspFix(
                Vector3.One, //scale
                position,
                MethodsForGL.GetGrappleGunFacingAngleRotation(ID));

            if (DataBase.InternalModels.ContainsKey(Consts.ModelKeyGrappleGunPoint))
            {
                if (mode == RenderMode.ModelMode)
                {
                    DataBase.InternalModels.RenderModel(Consts.ModelKeyGrappleGunPoint, rspFix);
                }
                else if (mode == RenderMode.BoxMode)
                {
                    RenderAppModel.BoundingBoxViewer(
                               DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyGrappleGunPoint),
                               rspFix, mColor);
                }
                else if (mode == RenderMode.SelectMode) 
                {
                    RenderAppModel.BoundingBoxToSelect(
                         DataBase.InternalModels.GetBoundingBoxLimit(Consts.ModelKeyGrappleGunPoint),
                         rspFix, useColor);
                }
            }
            else
            {
                if (mode == RenderMode.BoxMode)
                {
                    RenderAppModel.NoneBoundingBoxViewer(boundNoneExtras, -boundNoneExtras, rspFix, mColor);
                }
                else if (mode == RenderMode.SelectMode)
                {
                    RenderAppModel.NoneBoundingBoxToSelect(boundNoneExtras, -boundNoneExtras, rspFix, useColor);
                }
            }
        }


        private static void RenderPosTriggerZoneBox()
        {
            if (Globals.RenderItemsITA)
            {
                foreach (TreeNode item in DataBase.NodeITA.Nodes)
                {
                    RenderPosTriggerZoneBoxSubPart((Object3D)item, DataBase.NodeITA.MethodsForGL);
                }
            }

            if (Globals.RenderEventsAEV)
            {
                foreach (TreeNode item in DataBase.NodeAEV.Nodes)
                {
                    RenderPosTriggerZoneBoxSubPart((Object3D)item, DataBase.NodeAEV.MethodsForGL);
                }
            }
        }

        private static void RenderPosTriggerZoneBoxSubPart(Object3D item, SpecialMethodsForGL MethodsForGL)
        {
            Vector4 mColor = new Vector4(0, 0, 0, 0);

            ushort ID = item.ObjLineRef;
            GroupType Group = item.Group;

            if (Group == GroupType.ITA)
            {
                mColor = Globals.GL_ColorITA;
            }
            else if (Group == GroupType.AEV)
            {
                mColor = Globals.GL_ColorAEV;
            }

            if (Globals.UseMoreSpecialColors)
            {
                mColor = ReturnMoreSpecialColor(MethodsForGL.GetSpecialType(ID), mColor);
            }

            if (MethodsForGL.GetSpecialType(ID) != SpecialType.T03_Items && Globals.RenderSpecialTriggerZone)
            {
                if (DataBase.SelectedNodes.ContainsKey(item.GetHashCode())) { mColor = Globals.GL_ColorSelected; }
                mColor.W = 0.1f;

                if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category01)
                {
                    RenderAppModel.TriggerZoneBoxTransparentSolid(MethodsForGL.GetTriggerZoneMatrix4(ID), mColor);
                }
                else if (MethodsForGL.GetZoneCategory(ID) == SpecialZoneCategory.Category02)
                {
                    RenderAppModel.TriggerZoneCircleTransparentSolid(MethodsForGL.GetTriggerZoneMatrix4(ID), mColor);
                }
            }
        }


        private static Vector4 ReturnMoreSpecialColor(SpecialType specialType, Vector4 color)
        {
            switch (specialType)
            {
                case SpecialType.T00_GeneralPurpose: return Globals.GL_MoreColor_T00_GeneralPurpose;
                case SpecialType.T01_WarpDoor: return Globals.GL_MoreColor_T01_DoorWarp;
                case SpecialType.T02_CutSceneEvents: return Globals.GL_MoreColor_T02_CutSceneEvents;
                case SpecialType.T04_GroupedEnemyTrigger: return Globals.GL_MoreColor_T04_GroupedEnemyTrigger;
                case SpecialType.T05_Message: return Globals.GL_MoreColor_T05_Message;
                case SpecialType.T08_TypeWriter: return Globals.GL_MoreColor_T08_TypeWriter;
                case SpecialType.T0A_DamagesThePlayer: return Globals.GL_MoreColor_T0A_DamagesThePlayer;
                case SpecialType.T0B_FalseCollision: return Globals.GL_MoreColor_T0B_FalseCollision;
                case SpecialType.T0D_Unknown: return Globals.GL_MoreColor_T0D_Unknown;
                case SpecialType.T0E_Crouch: return Globals.GL_MoreColor_T0E_Crouch;
                case SpecialType.T10_FixedLadderClimbUp: return Globals.GL_MoreColor_T10_FixedLadderClimbUp;
                case SpecialType.T11_ItemDependentEvents: return Globals.GL_MoreColor_T11_ItemDependentEvents;
                case SpecialType.T12_AshleyHideCommand: return Globals.GL_MoreColor_T12_AshleyHideCommand;
                case SpecialType.T13_LocalTeleportation: return Globals.GL_MoreColor_T13_LocalTeleportation;
                case SpecialType.T14_UsedForElevators: return Globals.GL_MoreColor_T14_UsedForElevators;
                case SpecialType.T15_AdaGrappleGun: return Globals.GL_MoreColor_T15_AdaGrappleGun;
            }
            return color;
        }

        private enum RenderMode : byte
        {
            SelectMode,
            BoxMode,
            ModelMode
        }
    }
}
