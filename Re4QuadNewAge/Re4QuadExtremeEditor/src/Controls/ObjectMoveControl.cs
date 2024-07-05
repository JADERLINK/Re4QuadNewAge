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
            List<System.Windows.Forms.TreeNode> SelectedNodes = DataBase.SelectedNodes.Values.ToList();

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
                            if (Special.PropertyMethods.GetSpecialType(obj.ObjLineRef) == SpecialType.T03_Items)
                            {
                                combos |= MoveObjCombos.Item;
                            }
                            else
                            {
                                combos |= MoveObjCombos.SpecialTriggerZone;
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
                    }
                }
                combos -= MoveObjCombos.Null;
            }
            //Console.WriteLine("combos: "+ combos);

            // conjunto de ifs dos combos
            comboBoxMoveMode_IsChangeable = false;
            comboBoxMoveMode.Items.Clear();
            if (combos == MoveObjCombos.Null)
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
            else if (combos == MoveObjCombos.Item)
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
            }
            else if (combos == MoveObjCombos.SpecialTriggerZone)
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
            else if (combos == MoveObjCombos.ComboSpecialTriggerZoneAll)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123None, Lang.GetText(eLang.MoveMode_SpecialObj_Position)));
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
            else if (combos == MoveObjCombos.ComboEnemyEtcmodel
                  || combos == MoveObjCombos.ComboEnemyItem
                  || combos == MoveObjCombos.ComboEtcmodelItem
                  || combos == MoveObjCombos.ComboEnemyEtcmodelItem)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123RotationObjXYZ, Lang.GetText(eLang.MoveMode_Obj_PositionAndRotationAll)));
            }
            else if (combos == MoveObjCombos.ComboExtraSpecialAll)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123None, Lang.GetText(eLang.MoveMode_Obj_Position)));
            }
            else if (combos != MoveObjCombos.Null)
            {
                comboBoxMoveMode.Items.Add(new MoveObjTypeObjForListBox(MoveObjType.SquareMoveObjXZ_VerticalMoveObjY_Horizontal123None, Lang.GetText(eLang.MoveMode_Obj_Position)));
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
            checkBoxKeepOnGround.Enabled = enableAll;
            checkBoxLockMoveSquareHorizontal.Enabled = enableAll;
            checkBoxLockMoveSquareVertical.Enabled = enableAll;
            checkBoxMoveRelativeCam.Enabled = enableAll;
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
            Class.CustomDelegates.ActivateMethod UpdatePropertyGrid)
        {
            this.camera = camera;
            this.UpdateGL = UpdateGL;
            this.UpdateCameraMatrix = UpdateCameraMatrix;
            this.UpdatePropertyGrid = UpdatePropertyGrid;
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
                    EnableSquare = (obj.ID.HasFlag(MoveObjType._SquareMoveObjXZ) || obj.ID.HasFlag(MoveObjType._SquareMoveTriggerZone) || obj.ID.HasFlag(MoveObjType._SquareMoveAshleyZone));
                    EnableVertical = (obj.ID.HasFlag(MoveObjType._VerticalMoveObjY) || obj.ID.HasFlag(MoveObjType._VerticalScaleObjAll) || obj.ID.HasFlag(MoveObjType._VerticalMoveTriggerZoneY));
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
                        var temp = obj.GetObjPostion_ToMove_General();
                        if (temp.Length >= 1)
                        {
                            temp[0].Y = DataBase.SelectedRoom.DropToGround(temp[0]);
                        }
                        obj.SetObjPostion_ToMove_General(temp);
                    }
                }
                if (camera.isOrbitCamera())
                {
                    camera.UpdateCameraOrbitOnChangeValue();
                    UpdateCameraMatrix();
                }
                UpdateGL();
                UpdatePropertyGrid();
            }
        }

        private void checkBoxKeepOnGround_CheckedChanged(object sender, EventArgs e)
        {
            MoveObj.KeepOnGround = checkBoxKeepOnGround.Checked;
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

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveObjXZ))
                        {
                            MoveObj.MoveObjPositionXYZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveTriggerZone))
                        {
                            SpecialZoneCategory category = SpecialZoneCategory.Disable;
                            if (obj.Parent is SpecialNodeGroup group)
                            {
                                category = group.PropertyMethods.GetSpecialZoneCategory(obj.ObjLineRef);
                            }
                            MoveObj.MoveTriggerZonePositionXZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert, MoveObjTypeSelected, category);
                        }
                        else if (MoveObjTypeSelected.HasFlag(MoveObjType._SquareMoveAshleyZone))
                        {
                            MoveObj.MoveTriggerZonePositionXZ(obj, e, moveObj_lastMouseXY, oldPos, camera, dir, move_Invert, MoveObjTypeSelected, SpecialZoneCategory.Category01);
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

                        if (MoveObjTypeSelected.HasFlag(MoveObjType._VerticalMoveObjY))
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
                            SpecialZoneCategory category = SpecialZoneCategory.Disable;
                            if (obj.Parent is SpecialNodeGroup group)
                            {
                                category = group.PropertyMethods.GetSpecialZoneCategory(obj.ObjLineRef);
                            }
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
            }
        }



        // // /// /// // /// /// /// // //

        public void StartUpdateTranslation() 
        {
            labelObjSpeed.Text = Lang.GetText(eLang.labelObjSpeed) + " 100%";
            buttonDropToGround.Text = Lang.GetText(eLang.buttonDropToGround);
            checkBoxKeepOnGround.Text = Lang.GetText(eLang.checkBoxKeepOnGround);
            checkBoxLockMoveSquareHorizontal.Text = Lang.GetText(eLang.checkBoxLockMoveSquareHorizontal);
            checkBoxLockMoveSquareVertical.Text = Lang.GetText(eLang.checkBoxLockMoveSquareVertical);
            checkBoxMoveRelativeCam.Text = Lang.GetText(eLang.checkBoxMoveRelativeCam);
        }
    }
}
