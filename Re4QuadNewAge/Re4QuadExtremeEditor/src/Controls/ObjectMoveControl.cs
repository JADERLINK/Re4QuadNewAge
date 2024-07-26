using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class;
using OpenTK;
using NsCamera;

namespace Re4QuadExtremeEditor.src.Controls
{
    public partial class ObjectMoveControl : UserControl
    {
        private Camera camera;

        private Class.CustomDelegates.ActivateMethod UpdateGL;
        private Class.CustomDelegates.ActivateMethod UpdateCameraMatrix;
        private Class.CustomDelegates.ActivateMethod UpdatePropertyGrid;
        private Class.CustomDelegates.ActivateMethod UpdateTreeViewObjs;

        bool comboBoxMoveMode_IsChangeable = false;
        bool checkBoxLockMoveSquareHorizontal_IsChangeable = true;
        bool checkBoxLockMoveSquareVertical_IsChangeable = true;


        bool EnableSquare = false;
        bool EnableVertical = false;
        bool EnableHorisontal1 = false;
        bool EnableHorisontal2 = false;
        bool EnableHorisontal3 = false;

        MoveObjType MoveObjTypeSelected = MoveObjType.Null;

        public void UpdateSelection()
        {
            List<TreeNode> SelectedNodes = DataBase.SelectedNodes.Values.ToList();

            MoveObjCombos combos = MoveObjCombos.Null;
            if (SelectedNodes.Count > 0)
            {
                for (int i = 0; i < SelectedNodes.Count; i++)
                {
                    if (SelectedNodes[i] is Object3D obj)
                    {
                        var parent = obj.Parent;
                        if (parent is EnemyNodeGroup Enemy)
                        {
                            combos |= MoveObjCombos.Enemy;
                        }
                        else if (parent is EtcModelNodeGroup EtcModel)
                        {
                            combos |= MoveObjCombos.Etcmodel;
                        }
                        else if (parent is SpecialNodeGroup Special)
                        {
                            TriggerZoneCategory triggerZoneCategory = Special.PropertyMethods.GetTriggerZoneCategory(obj.ObjLineRef);
                            if (triggerZoneCategory == TriggerZoneCategory.Category01 || triggerZoneCategory == TriggerZoneCategory.Category02)
                            {
                                combos |= MoveObjCombos.TriggerZone;
                            }

                            if (Special.PropertyMethods.GetSpecialType(obj.ObjLineRef) == SpecialType.T03_Items)
                            {
                                combos |= MoveObjCombos.Item;
                            }
                        }
                        else if (parent is ExtraNodeGroup Extra)
                        {
                            var Association = DataBase.Extras.AssociationList[obj.ObjLineRef];
                            if (Association.FileFormat == SpecialFileFormat.AEV)
                            {
                                var SpecialType = DataBase.FileAEV.Methods.GetSpecialType(Association.LineID);
                                if (SpecialType == SpecialType.T12_AshleyHideCommand)
                                {
                                    combos |= MoveObjCombos.ExtraSpecialAshley;
                                }
                                else
                                {
                                    combos |= MoveObjCombos.ExtraSpecialWarpLadderGrappleGun;
                                }
                            }
                            else if (Association.FileFormat == SpecialFileFormat.ITA)
                            {
                                var SpecialType = DataBase.FileITA.Methods.GetSpecialType(Association.LineID);
                                if (SpecialType == SpecialType.T12_AshleyHideCommand)
                                {
                                    combos |= MoveObjCombos.ExtraSpecialAshley;
                                }
                                else
                                {
                                    combos |= MoveObjCombos.ExtraSpecialWarpLadderGrappleGun;
                                }
                            }
                        }
                        else if (parent is NewAge_DSE_NodeGroup) 
                        {
                            combos |= MoveObjCombos.DisableMoveObject;
                        }
                        else if (parent is NewAge_FSE_NodeGroup fse)
                        {
                            TriggerZoneCategory triggerZoneCategory = fse.PropertyMethods.GetTriggerZoneCategory(obj.ObjLineRef);
                            if (triggerZoneCategory == TriggerZoneCategory.Category01 || triggerZoneCategory == TriggerZoneCategory.Category02)
                            {
                                combos |= MoveObjCombos.TriggerZone;
                            }
                        }
                        else if (parent is NewAge_ESAR_NodeGroup esar)
                        {
                            TriggerZoneCategory triggerZoneCategory = esar.PropertyMethods.GetTriggerZoneCategory(obj.ObjLineRef);
                            if (triggerZoneCategory == TriggerZoneCategory.Category01 || triggerZoneCategory == TriggerZoneCategory.Category02)
                            {
                                combos |= MoveObjCombos.TriggerZone;
                            }
                        }
                        else if (parent is NewAge_ESE_NodeGroup)
                        {
                            combos |= MoveObjCombos.EseEntry;
                        }
                        else if (parent is NewAge_EMI_NodeGroup)
                        {
                            combos |= MoveObjCombos.EmiEntry;
                        }
                        else if (parent is QuadCustomNodeGroup quad)
                        {
                            TriggerZoneCategory triggerZoneCategory = quad.PropertyMethods.GetTriggerZoneCategory(obj.ObjLineRef);
                            if (triggerZoneCategory == TriggerZoneCategory.Category01 || triggerZoneCategory == TriggerZoneCategory.Category02)
                            {
                                combos |= MoveObjCombos.TriggerZone;
                            }

                            QuadCustomPointStatus status = quad.PropertyMethods.GetQuadCustomPointStatus(obj.ObjLineRef);
                            if (status == QuadCustomPointStatus.ArrowPoint01 || status == QuadCustomPointStatus.CustomModel02)
                            {
                                combos |= MoveObjCombos.QuadCustom;
                            }
                        }
                    }
                }
                combos -= MoveObjCombos.Null;
                if (combos.HasFlag(MoveObjCombos.DisableMoveObject))
                {
                    combos = MoveObjCombos.DisableMoveObject;
                }
            }
            //Console.WriteLine("combos: "+ combos);

            // conjunto de ifs dos combos
            comboBoxMoveMode_IsChangeable = false;
            comboBoxMoveMode.Items.Clear();
            if (combos == MoveObjCombos.Null || combos == MoveObjCombos.DisableMoveObject)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.Null, ""));
            }
            else if (combos == MoveObjCombos.Enemy)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Enemy_PositionAndRotationAll)));
            }
            else if (combos == MoveObjCombos.Etcmodel)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_EtcModel_PositionAndRotationAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareNone_VerticalScaleObjAll_Horizontal123ScaleObjXYZ, Lang.GetText(eLang.MoveMode_EtcModel_Scale)));
            }
            else if (combos == MoveObjCombos.EseEntry)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123None, Lang.GetText(eLang.MoveMode_EseEntry_PositionPoint)));
            }
            else if (combos == MoveObjCombos.EmiEntry)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal1None_Horizontal2RotationObjY_Horizontal3None, Lang.GetText(eLang.MoveMode_EmiEntry_PositionAndAnglePoint)));
            }
            else if (combos == MoveObjCombos.ExtraSpecialWarpLadderGrappleGun)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal1None_Horizontal2RotationObjY_Horizontal3None, Lang.GetText(eLang.MoveMode_Obj_PositionAndRotationY)));
            }
            else if (combos == MoveObjCombos.ExtraSpecialAshley)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123None, Lang.GetText(eLang.MoveMode_Ashley_Position)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveAshleyAllPointsXZ_VerticalNone_Horizontal1None_Horizontal2RotationZoneY_Horizontal3ScaleAll, Lang.GetText(eLang.MoveMode_AshleyZone_MoveAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveAshleyPoint0XZ_VerticalNone_Horizontal123None, Lang.GetText(eLang.MoveMode_AshleyZone_Point0)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveAshleyPoint1XZ_VerticalNone_Horizontal123None, Lang.GetText(eLang.MoveMode_AshleyZone_Point1)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveAshleyPoint2XZ_VerticalNone_Horizontal123None, Lang.GetText(eLang.MoveMode_AshleyZone_Point2)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveAshleyPoint3XZ_VerticalNone_Horizontal123None, Lang.GetText(eLang.MoveMode_AshleyZone_Point3)));
            }
            else if (combos == MoveObjCombos.Item)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Item_PositionAndRotationAll)));
            }
            else if (combos == MoveObjCombos.QuadCustom) 
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_QuadCustomPoint_PositionAndRotationAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareNone_VerticalScaleObjAll_Horizontal123ScaleObjXYZ, Lang.GetText(eLang.MoveMode_QuadCustomPoint_Scale)));
            }
            else if (combos == MoveObjCombos.TriggerZone)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneAllPointsXZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal2RotationZoneY_Horizontal3ScaleAll, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint0XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point0)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint1XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point1)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint2XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point2)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint3XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point3)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint01and12XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall01)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint12and23XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall12)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallpoint23and30XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall23)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint30and01XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall30)));
            }
            else if (combos == MoveObjCombos.Combo_Item_TriggerZone)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Item_PositionAndRotationAll)));
                
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneAllPointsXZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal2RotationZoneY_Horizontal3ScaleAll, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint0XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point0)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint1XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point1)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint2XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point2)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint3XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point3)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint01and12XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall01)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint12and23XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall12)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallpoint23and30XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall23)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint30and01XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall30)));
                
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.AllMoveXYZ_Horizontal123None, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll_Obj_Position)));
            }
            else if (combos == MoveObjCombos.Combo_QuadCustom_TriggerZone)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_QuadCustomPoint_PositionAndRotationAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareNone_VerticalScaleObjAll_Horizontal123ScaleObjXYZ, Lang.GetText(eLang.MoveMode_QuadCustomPoint_Scale)));
                
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneAllPointsXZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal2RotationZoneY_Horizontal3ScaleAll, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint0XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point0)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint1XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point1)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint2XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point2)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint3XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point3)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint01and12XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall01)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint12and23XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall12)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallpoint23and30XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall23)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint30and01XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall30)));
               
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.AllMoveXYZ_Horizontal123None, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll_Obj_Position)));
            }
            else if (combos == MoveObjCombos.Combo_Item_QuadCustom_TriggerZone)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Obj_PositionAndRotationAll)));
                
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneAllPointsXZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal2RotationZoneY_Horizontal3ScaleAll, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint0XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point0)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint1XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point1)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint2XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point2)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZonePoint3XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Point3)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint01and12XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall01)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint12and23XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall12)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallpoint23and30XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall23)));
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveTriggerZoneWallPoint30and01XZ_VerticalMoveTriggerZoneY_Horizontal1ChangeTriggerZoneHeight_Horizontal23None, Lang.GetText(eLang.MoveMode_TriggerZone_Wall30)));
                
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.AllMoveXYZ_Horizontal123None, Lang.GetText(eLang.MoveMode_TriggerZone_MoveAll_Obj_Position)));
            }
            else if (combos == MoveObjCombos.Combo_Item_QuadCustom)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Obj_PositionAndRotationAll)));
            }

       
            else if ((
                combos.HasFlag(MoveObjCombos.Enemy) 
                || combos.HasFlag(MoveObjCombos.Etcmodel)
                || combos.HasFlag(MoveObjCombos.QuadCustom)
                || combos.HasFlag(MoveObjCombos.Item)
                ) && !(
                combos.HasFlag(MoveObjCombos.TriggerZone)
                || combos.HasFlag(MoveObjCombos.EmiEntry)
                || combos.HasFlag(MoveObjCombos.EseEntry)
                || combos.HasFlag(MoveObjCombos.ExtraSpecialAshley)
                || combos.HasFlag(MoveObjCombos.ExtraSpecialWarpLadderGrappleGun)
                ))
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Obj_PositionAndRotationAll)));
            }
            else if ((
              combos.HasFlag(MoveObjCombos.Enemy)
              || combos.HasFlag(MoveObjCombos.Etcmodel)
              || combos.HasFlag(MoveObjCombos.QuadCustom)
              || combos.HasFlag(MoveObjCombos.Item)
              || combos.HasFlag(MoveObjCombos.EmiEntry)
              || combos.HasFlag(MoveObjCombos.ExtraSpecialWarpLadderGrappleGun)
              ) && !(
              combos.HasFlag(MoveObjCombos.TriggerZone)
              || combos.HasFlag(MoveObjCombos.EseEntry)
              || combos.HasFlag(MoveObjCombos.ExtraSpecialAshley)            
              ))
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal1None_Horizontal2RotationObjY_Horizontal3None, Lang.GetText(eLang.MoveMode_Obj_PositionAndRotationY)));
            }

            else if (combos != MoveObjCombos.Null)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.AllMoveXYZ_Horizontal123None, Lang.GetText(eLang.MoveMode_Obj_Position)));
            }
            else  // anti bug
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.Null, ""));
            }

            comboBoxMoveMode_IsChangeable = true;
            if (comboBoxMoveMode.Items.Contains(new MoveObjTypeObjForListBox(MoveObjTypeSelected, null)))
            {
                comboBoxMoveMode.SelectedIndex = comboBoxMoveMode.Items.IndexOf(new MoveObjTypeObjForListBox(MoveObjTypeSelected, null));
            }
            else
            {
                comboBoxMoveMode.SelectedIndex = 0;
            }

        }


        void EnableAll(bool enableAll)
        {
            this.Enabled = enableAll;
            comboBoxMoveMode.Enabled = enableAll;
            buttonDropToGround.Enabled = enableAll;
            checkBoxObjKeepOnGround.Enabled = enableAll;
            checkBoxLockMoveSquareHorizontal.Enabled = enableAll;
            checkBoxLockMoveSquareVertical.Enabled = enableAll;
            checkBoxMoveRelativeCam.Enabled = enableAll;
            checkBoxTriggerZoneKeepOnGround.Enabled = enableAll;
            trackBarMoveSpeed.Enabled = enableAll;

            moveObjHorizontal1.Enabled = EnableHorisontal1 && enableAll;
            moveObjHorizontal2.Enabled = EnableHorisontal2 && enableAll;
            moveObjHorizontal3.Enabled = EnableHorisontal3 && enableAll;
            moveObjVertical.Enabled = EnableVertical && enableAll;
            moveObjSquare.Enabled = EnableSquare && enableAll;
        }

        void UpdatePictureBoxImages()
        {
            if (EnableHorisontal1)
            {
                moveObjHorizontal1.BackgroundImage = Properties.Resources.HorizontalYelow;
            }
            else
            {
                moveObjHorizontal1.BackgroundImage = Properties.Resources.HorizontalDisable;
            }
            if (EnableHorisontal2)
            {
                moveObjHorizontal2.BackgroundImage = Properties.Resources.HorizontalYelow;
            }
            else
            {
                moveObjHorizontal2.BackgroundImage = Properties.Resources.HorizontalDisable;
            }
            if (EnableHorisontal3)
            {
                moveObjHorizontal3.BackgroundImage = Properties.Resources.HorizontalYelow;
            }
            else
            {
                moveObjHorizontal3.BackgroundImage = Properties.Resources.HorizontalDisable;
            }

            if (EnableVertical)
            {
                moveObjVertical.BackgroundImage = Properties.Resources.VerticalRed;
            }
            else
            {
                moveObjVertical.BackgroundImage = Properties.Resources.VerticalDisable;
            }

            if (EnableSquare)
            {
                if (MoveObj.LockMoveSquareVertical)
                {
                    moveObjSquare.BackgroundImage = Properties.Resources.SquareRedLookVertical;
                }
                else if (MoveObj.LockMoveSquareHorisontal)
                {
                    moveObjSquare.BackgroundImage = Properties.Resources.SquareRedLookHorisontal;
                }
                else
                {
                    moveObjSquare.BackgroundImage = Properties.Resources.SquareRed;
                }
            }
            else
            {
                moveObjSquare.BackgroundImage = Properties.Resources.SquareDisable;
            }

        }

        public ObjectMoveControl(ref Camera camera,
            Class.CustomDelegates.ActivateMethod UpdateGL,
            Class.CustomDelegates.ActivateMethod UpdateCameraMatrix,
            Class.CustomDelegates.ActivateMethod UpdatePropertyGrid,
            Class.CustomDelegates.ActivateMethod UpdateTreeViewObjs)
        {
            this.camera = camera;
            this.UpdateGL = UpdateGL;
            this.UpdateCameraMatrix = UpdateCameraMatrix;
            this.UpdatePropertyGrid = UpdatePropertyGrid;
            this.UpdateTreeViewObjs = UpdateTreeViewObjs;
            InitializeComponent();
            EnableAll(false);
            UpdatePictureBoxImages();
            comboBoxMoveMode.MouseWheel += ComboBoxMoveMode_MouseWheel;
            trackBarMoveSpeed.MouseWheel += TrackBarMoveSpeed_MouseWheel;
            comboBoxMoveMode_IsChangeable = false;
            comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.Null, ""));
            comboBoxMoveMode.SelectedIndex = 0;
        }

        private void TrackBarMoveSpeed_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            if (e.X >= 0 && e.Y >= 0 && e.X < trackBarMoveSpeed.Width && e.Y < trackBarMoveSpeed.Height)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void ComboBoxMoveMode_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = !((ComboBox)sender).DroppedDown;
        }


        private void ObjectMoveControl_Resize(object sender, EventArgs e)
        {
            int width = this.Width - comboBoxMoveMode.Location.X;
            if (width > 800)
            {
                width = 800;
            }
            comboBoxMoveMode.Size = new Size(width, comboBoxMoveMode.Size.Height);
        }


        private void trackBarMoveSpeed_Scroll(object sender, EventArgs e)
        {
            float newValue;
            if (trackBarMoveSpeed.Value > 50)
            { newValue = 100.0f + ((trackBarMoveSpeed.Value - 50) * 8f); }
            else
            { newValue = (trackBarMoveSpeed.Value / 50.0f) * 100f; }
            if (newValue < 1f)
            { newValue = 1f; }
            else if (newValue > 96f && newValue < 114f)
            { newValue = 100f; }

            labelObjSpeed.Text = Lang.GetText(eLang.labelObjSpeed) + " " + ((int)newValue).ToString().PadLeft(3) + "%";
            MoveObj.objSpeedMultiplier = newValue / 100.0f;
        }

        private void comboBoxMoveMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMoveMode_IsChangeable)
            {
                if (comboBoxMoveMode.SelectedItem is MoveObjTypeObjForListBox obj && obj.ID != MoveObjType.Null)
                {
                    MoveObjTypeSelected = obj.ID;
                    EnableSquare = (obj.ID.HasFlag(MoveObjType._SquareMoveObjXZ) || obj.ID.HasFlag(MoveObjType._SquareMoveTriggerZone) || obj.ID.HasFlag(MoveObjType._SquareMoveAshleyZone) || obj.ID.HasFlag(MoveObjType._AllMoveXYZ));
                    EnableVertical = (obj.ID.HasFlag(MoveObjType._VerticalMoveObjY) || obj.ID.HasFlag(MoveObjType._VerticalScaleObjAll) || obj.ID.HasFlag(MoveObjType._VerticalMoveTriggerZoneY) || obj.ID.HasFlag(MoveObjType._AllMoveXYZ));
                    EnableHorisontal1 = (obj.ID.HasFlag(MoveObjType._Horizontal1RotationObjX) || obj.ID.HasFlag(MoveObjType._Horizontal1ScaleObjX) || obj.ID.HasFlag(MoveObjType._Horizontal1ChangeTriggerZoneHeight));
                    EnableHorisontal2 = (obj.ID.HasFlag(MoveObjType._Horizontal2RotationObjY) || obj.ID.HasFlag(MoveObjType._Horizontal2ScaleObjY) || obj.ID.HasFlag(MoveObjType._Horizontal2RotationZoneY));
                    EnableHorisontal3 = (obj.ID.HasFlag(MoveObjType._Horizontal3RotationObjZ) || obj.ID.HasFlag(MoveObjType._Horizontal3ScaleObjZ) || obj.ID.HasFlag(MoveObjType._Horizontal3TriggerZoneScaleAll) || obj.ID.HasFlag(MoveObjType._Horizontal3AshleyZoneScaleAll));
                    EnableAll(true);
                    UpdatePictureBoxImages();
                }
                else
                {
                    MoveObjTypeSelected = MoveObjType.Null;
                    EnableSquare = false;
                    EnableVertical = false;
                    EnableHorisontal1 = false;
                    EnableHorisontal2 = false;
                    EnableHorisontal3 = false;
                    EnableAll(false);
                    UpdatePictureBoxImages();
                }
            }
        }

        private void buttonDropToGround_Click(object sender, EventArgs e)
        {
            if (DataBase.SelectedRoom != null)
            {
                foreach (TreeNode item in DataBase.SelectedNodes.Values)
                {
                    if (item.Parent != null && item is Object3D obj)
                    {
                        var PosArr = obj.GetObjPostion_ToMove_General();
                        if (PosArr.Length >= 1)
                        {
                            if (MoveObjTypeSelected.HasFlag(MoveObjType._AllMoveXYZ)
                                || MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveObjXZ))
                            {
                                PosArr[0].Y = DataBase.SelectedRoom.DropToGround(PosArr[0]);
                            }
                        }

                        if (PosArr.Length >= 7)
                        {
                            if (MoveObjTypeSelected.HasFlag(MoveObjType._AllMoveXYZ))
                            {
                                PosArr[5].Y = DataBase.SelectedRoom.DropToGround(new Vector3(PosArr[6].X, PosArr[5].Y, PosArr[6].Z));

                            }
                            else if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveTriggerZone))
                            {
                                TriggerZoneCategory category = obj.GetTriggerZoneCategory();
                                if ( ((MoveObjTypeSelected.HasFlag(MoveObjType.__AllPointsXZ) && category == TriggerZoneCategory.Category01)
                                    || (category == TriggerZoneCategory.Category02)))
                                {
                                    PosArr[5].Y = DataBase.SelectedRoom.DropToGround(new Vector3(PosArr[6].X, PosArr[5].Y, PosArr[6].Z));
                                }
                            }
                        }

                        obj.SetObjPostion_ToMove_General(PosArr);
                    }
                }
                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
                if (Globals.TreeNodeRenderHexValues)
                {
                    UpdateTreeViewObjs();
                }
            }
        }

        private void checkBoxKeepOnGround_CheckedChanged(object sender, EventArgs e)
        {
            MoveObj.KeepOnGround = checkBoxObjKeepOnGround.Checked;
        }

        private void checkBoxTriggerZoneKeepOnGround_CheckedChanged(object sender, EventArgs e)
        {
            MoveObj.TriggerZoneKeepOnGround = checkBoxTriggerZoneKeepOnGround.Checked;
        }

        private void checkBoxMoveRelativeCam_CheckedChanged(object sender, EventArgs e)
        {
            MoveObj.MoveRelativeCamera = checkBoxMoveRelativeCam.Checked;
        }

        private void checkBoxLockMoveSquareHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLockMoveSquareHorizontal_IsChangeable)
            {
                checkBoxLockMoveSquareVertical_IsChangeable = false;
                checkBoxLockMoveSquareVertical.Checked = false;
                MoveObj.LockMoveSquareHorisontal = checkBoxLockMoveSquareHorizontal.Checked;
                MoveObj.LockMoveSquareVertical = false;
                UpdatePictureBoxImages();
                checkBoxLockMoveSquareVertical_IsChangeable = true;
            }
        }

        private void checkBoxLockMoveSquareVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLockMoveSquareVertical_IsChangeable)
            {
                checkBoxLockMoveSquareHorizontal_IsChangeable = false;
                checkBoxLockMoveSquareHorizontal.Checked = false;
                MoveObj.LockMoveSquareVertical = checkBoxLockMoveSquareVertical.Checked;
                MoveObj.LockMoveSquareHorisontal = false;
                UpdatePictureBoxImages();
                checkBoxLockMoveSquareHorizontal_IsChangeable = true;
            }
        }



        // // // // // // // // // // // // // // // // // // // // // //
        bool moveObjSquare_mouseDown = false;
        bool moveObjVertical_mouseDown = false;
        bool moveObjHorisontal1_mouseDown = false;
        bool moveObjHorisontal2_mouseDown = false;
        bool moveObjHorisontal3_mouseDown = false;

        bool move_Invert = false;

        Point moveObj_lastMouseXY = new Point(0, 0);
        Dictionary<MoveObj.ObjKey, Vector3[]> SavedPosition;



        private void moveObjSquare_MouseLeave(object sender, EventArgs e)
        {
            moveObjSquare_mouseDown = false;
        }

        private void moveObjSquare_MouseDown(object sender, MouseEventArgs e)
        {
            moveObjSquare_mouseDown = true;
            moveObj_lastMouseXY.X = e.X;
            moveObj_lastMouseXY.Y = e.Y;
            SavedPosition = MoveObj.GetSavedPosition();
            if (e.Button == MouseButtons.Right)
            {
                move_Invert = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                move_Invert = false;
            }
        }

        private void moveObjSquare_MouseUp(object sender, MouseEventArgs e)
        {
            moveObjSquare_mouseDown = false;
        }

        private void moveObjSquare_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnableSquare && moveObjSquare_mouseDown)
            {
                foreach (TreeNode item in DataBase.SelectedNodes.Values)
                {
                    if (item is Object3D obj && item.Parent is TreeNodeGroup)
                    {
                        Vector3[] oldPos = null;
                        var key = new MoveObj.ObjKey(obj.ObjLineRef, obj.Group);
                        if (SavedPosition.ContainsKey(key))
                        {
                            oldPos = SavedPosition[key];
                        }

                        MoveObj.MoveDirection dir = MoveObj.MoveDirection.Null;
                        if (MoveObj.LockMoveSquareHorisontal)
                        {
                            dir = MoveObj.MoveDirection.Z;
                        }
                        else if (MoveObj.LockMoveSquareVertical)
                        {
                            dir = MoveObj.MoveDirection.X;
                        }
                        else
                        {
                            dir = MoveObj.MoveDirection.X | MoveObj.MoveDirection.Z;
                        }

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._AllMoveXYZ))
                        {
                            MoveObj.MoveTriggerZonePlusObjPositionXYZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveObjXZ))
                        {
                            MoveObj.MoveObjPositionXYZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveTriggerZone))
                        {
                            TriggerZoneCategory category = obj.GetTriggerZoneCategory();
                            MoveObj.MoveTriggerZonePositionXZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert, MoveObjTypeSelected, category);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveAshleyZone))
                        {
                            MoveObj.MoveTriggerZonePositionXZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert, MoveObjTypeSelected, TriggerZoneCategory.Category01);
                        }

                    }
                }


                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
                if (Globals.TreeNodeRenderHexValues)
                {
                    UpdateTreeViewObjs();
                }
            }

        }



        private void moveObjVertical_MouseLeave(object sender, EventArgs e)
        {
            moveObjVertical_mouseDown = false;
        }

        private void moveObjVertical_MouseDown(object sender, MouseEventArgs e)
        {
            moveObjVertical_mouseDown = true;
            moveObj_lastMouseXY.X = e.X;
            moveObj_lastMouseXY.Y = e.Y;
            if (MoveObjTypeSelected.HasFlag(MoveObjType._VerticalScaleObjAll))
            {
                SavedPosition = MoveObj.GetSavedScales();
            }
            else 
            {
                SavedPosition = MoveObj.GetSavedPosition();
            }
           
            if (e.Button == MouseButtons.Right)
            {
                move_Invert = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                move_Invert = false;
            }
        }

        private void moveObjVertical_MouseUp(object sender, MouseEventArgs e)
        {
            moveObjVertical_mouseDown = false;
        }

        private void moveObjVertical_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnableVertical && moveObjVertical_mouseDown)
            {
                foreach (TreeNode item in DataBase.SelectedNodes.Values)
                {
                    if (item is Object3D obj && item.Parent is TreeNodeGroup)
                    {
                        Vector3[] oldPos = null;
                        var key = new MoveObj.ObjKey(obj.ObjLineRef, obj.Group);
                        if (SavedPosition.ContainsKey(key))
                        {
                            oldPos = SavedPosition[key];
                        }

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._AllMoveXYZ))
                        {
                            MoveObj.MoveTriggerZonePlusObjPositionXYZ(obj, e, moveObj_lastMouseXY, oldPos, camera, MoveObj.MoveDirection.Y, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._VerticalMoveObjY))
                        {
                            MoveObj.MoveObjPositionXYZ(obj, e, moveObj_lastMouseXY, oldPos, camera, MoveObj.MoveDirection.Y, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._VerticalMoveTriggerZoneY))
                        {
                            MoveObj.MoveTriggerZonePositionY(obj, e, moveObj_lastMouseXY, oldPos, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._VerticalScaleObjAll))
                        {
                            MoveObj.MoveObjScaleXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.X | MoveObj.MoveDirection.Y | MoveObj.MoveDirection.Z, move_Invert);
                        }

                    }

                }

                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
                if (Globals.TreeNodeRenderHexValues)
                {
                    UpdateTreeViewObjs();
                }
            }

        }



        private void moveObjHorizontal1_MouseLeave(object sender, EventArgs e)
        {
            moveObjHorisontal1_mouseDown = false;
        }

        private void moveObjHorizontal1_MouseDown(object sender, MouseEventArgs e)
        {
            moveObjHorisontal1_mouseDown = true;
            moveObj_lastMouseXY.X = e.X;
            moveObj_lastMouseXY.Y = e.Y;
            if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal1RotationObjX))
            {
                SavedPosition = MoveObj.GetSavedRotationAngles();
            }
            else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal1ScaleObjX))
            {
                SavedPosition = MoveObj.GetSavedScales();
            }
            else
            {
                SavedPosition = MoveObj.GetSavedPosition();
            }
            if (e.Button == MouseButtons.Right)
            {
                move_Invert = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                move_Invert = false;
            }
        }

        private void moveObjHorizontal1_MouseUp(object sender, MouseEventArgs e)
        {
            moveObjHorisontal1_mouseDown = false;
        }

        private void moveObjHorizontal1_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnableHorisontal1 && moveObjHorisontal1_mouseDown)
            {
                foreach (TreeNode item in DataBase.SelectedNodes.Values)
                {
                    if (item is Object3D obj && item.Parent is TreeNodeGroup)
                    {
                        Vector3[] oldPos = null;
                        var key = new MoveObj.ObjKey(obj.ObjLineRef, obj.Group);
                        if (SavedPosition.ContainsKey(key))
                        {
                            oldPos = SavedPosition[key];
                        }

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal1RotationObjX))
                        {
                            MoveObj.MoveObjRotationAnglesXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.X, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal1ScaleObjX))
                        {
                            MoveObj.MoveObjScaleXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.X, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal1ChangeTriggerZoneHeight))
                        {
                            MoveObj.MoveTriggerZoneHeight(obj, e, moveObj_lastMouseXY, oldPos, move_Invert);
                        }

                    }
                }

                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
                if (Globals.TreeNodeRenderHexValues)
                {
                    UpdateTreeViewObjs();
                }
            }
        }



        private void moveObjHorizontal2_MouseLeave(object sender, EventArgs e)
        {
            moveObjHorisontal2_mouseDown = false;
        }

        private void moveObjHorizontal2_MouseDown(object sender, MouseEventArgs e)
        {
            moveObjHorisontal2_mouseDown = true;
            moveObj_lastMouseXY.X = e.X;
            moveObj_lastMouseXY.Y = e.Y;
            if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal2RotationObjY))
            {
                SavedPosition = MoveObj.GetSavedRotationAngles();
            }
            else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal2ScaleObjY))
            {
                SavedPosition = MoveObj.GetSavedScales();
            }
            else
            {
                SavedPosition = MoveObj.GetSavedPosition();
            }
            if (e.Button == MouseButtons.Right)
            {
                move_Invert = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                move_Invert = false;
            }
        }

        private void moveObjHorizontal2_MouseUp(object sender, MouseEventArgs e)
        {
            moveObjHorisontal2_mouseDown = false;
        }

        private void moveObjHorizontal2_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnableHorisontal2 && moveObjHorisontal2_mouseDown)
            {
                foreach (TreeNode item in DataBase.SelectedNodes.Values)
                {
                    if (item is Object3D obj && item.Parent is TreeNodeGroup)
                    {
                        Vector3[] oldPos = null;
                        var key = new MoveObj.ObjKey(obj.ObjLineRef, obj.Group);
                        if (SavedPosition.ContainsKey(key))
                        {
                            oldPos = SavedPosition[key];
                        }

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal2RotationObjY))
                        {
                            MoveObj.MoveObjRotationAnglesXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.Y, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal2ScaleObjY))
                        {
                            MoveObj.MoveObjScaleXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.Y, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal2RotationZoneY))
                        {
                            MoveObj.MoveZoneRotate(obj, e, moveObj_lastMouseXY, oldPos, move_Invert);
                        }
                    }
                }

                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
                if (Globals.TreeNodeRenderHexValues)
                {
                    UpdateTreeViewObjs();
                }
            }
        }




        private void moveObjHorizontal3_MouseLeave(object sender, EventArgs e)
        {
            moveObjHorisontal3_mouseDown = false;
        }

        private void moveObjHorizontal3_MouseDown(object sender, MouseEventArgs e)
        {
            moveObjHorisontal3_mouseDown = true;
            moveObj_lastMouseXY.X = e.X;
            moveObj_lastMouseXY.Y = e.Y;
            if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal3RotationObjZ))
            {
                SavedPosition = MoveObj.GetSavedRotationAngles();
            }
            else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal3ScaleObjZ))
            {
                SavedPosition = MoveObj.GetSavedScales();
            }
            else
            {
                SavedPosition = MoveObj.GetSavedPosition();
            }
            if (e.Button == MouseButtons.Right)
            {
                move_Invert = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                move_Invert = false;
            }
        }

        private void moveObjHorizontal3_MouseUp(object sender, MouseEventArgs e)
        {
            moveObjHorisontal3_mouseDown = false;
        }

        private void moveObjHorizontal3_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnableHorisontal3 && moveObjHorisontal3_mouseDown)
            {
                foreach (TreeNode item in DataBase.SelectedNodes.Values)
                {
                    if (item is Object3D obj && item.Parent is TreeNodeGroup)
                    {
                        Vector3[] oldPos = null;
                        var key = new MoveObj.ObjKey(obj.ObjLineRef, obj.Group);
                        if (SavedPosition.ContainsKey(key))
                        {
                            oldPos = SavedPosition[key];
                        }

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal3RotationObjZ))
                        {
                            MoveObj.MoveObjRotationAnglesXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.Z, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal3ScaleObjZ))
                        {
                            MoveObj.MoveObjScaleXYZ(obj, e, moveObj_lastMouseXY, oldPos, MoveObj.MoveDirection.Z, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal3TriggerZoneScaleAll))
                        {
                            TriggerZoneCategory category = obj.GetTriggerZoneCategory();
                            MoveObj.MoveTriggerZoneScale(obj, e, moveObj_lastMouseXY, oldPos, move_Invert, category);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._Horizontal3AshleyZoneScaleAll))
                        {
                            MoveObj.MoveAshleyZoneScale(obj, e, moveObj_lastMouseXY, oldPos, move_Invert);
                        }
                    }
                }

                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
                if (Globals.TreeNodeRenderHexValues)
                {
                    UpdateTreeViewObjs();
                }
            }
        }



        // // /// /// // /// /// /// // //

        public void StartUpdateTranslation() 
        {
            labelObjSpeed.Text = Lang.GetText(eLang.labelObjSpeed) + " 100%";
            buttonDropToGround.Text = Lang.GetText(eLang.buttonDropToGround);
            checkBoxObjKeepOnGround.Text = Lang.GetText(eLang.checkBoxObjKeepOnGround);
            checkBoxTriggerZoneKeepOnGround.Text = Lang.GetText(eLang.checkBoxTriggerZoneKeepOnGround);
            checkBoxLockMoveSquareHorizontal.Text = Lang.GetText(eLang.checkBoxLockMoveSquareHorizontal);
            checkBoxLockMoveSquareVertical.Text = Lang.GetText(eLang.checkBoxLockMoveSquareVertical);
            checkBoxMoveRelativeCam.Text = Lang.GetText(eLang.checkBoxMoveRelativeCam);
        }

    }
}
