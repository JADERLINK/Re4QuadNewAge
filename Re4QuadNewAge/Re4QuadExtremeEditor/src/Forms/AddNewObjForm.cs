using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;

namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class AddNewObjForm : Form
    {
        public event Class.CustomDelegates.ActivateMethod OnButtonOk_Click;
        public event Class.CustomDelegates.ActivateMethod TreeViewDisableDrawNode;
        public event Class.CustomDelegates.ActivateMethod TreeViewEnableDrawNode;

        public AddNewObjForm()
        {
            InitializeComponent();

            KeyPreview = true;
            if (DataBase.FileETS != null && DataBase.FileETS.Lines.Count < Consts.AmountLimitETS)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ETS, 0x00, Lang.GetText(eLang.AddNewETS)));
            }
            if (DataBase.FileDSE != null && DataBase.FileDSE.Lines.Count < Consts.AmountLimitDSE)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.DSE, 0x00, Lang.GetText(eLang.AddNewDSE)));
            }
            if (DataBase.FileFSE != null && DataBase.FileFSE.Lines.Count < Consts.AmountLimitFSE)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.FSE, 0x00, Lang.GetText(eLang.AddNewFSE)));
            }
            if (DataBase.FileSAR != null && DataBase.FileSAR.Lines.Count < Consts.AmountLimitSAR)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.SAR, 0x00, Lang.GetText(eLang.AddNewSAR)));
            }
            if (DataBase.FileEAR != null && DataBase.FileEAR.Lines.Count < Consts.AmountLimitEAR)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.EAR, 0x00, Lang.GetText(eLang.AddNewEAR)));
            }
            if (DataBase.FileESE != null && DataBase.FileESE.Lines.Count < Consts.AmountLimitESE)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ESE, 0x00, Lang.GetText(eLang.AddNewESE)));
            }
            if (DataBase.FileEMI != null && DataBase.FileEMI.Lines.Count < Consts.AmountLimitEMI)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.EMI, 0x00, Lang.GetText(eLang.AddNewEMI)));
            }
            if (DataBase.FileQuadCustom != null && DataBase.FileQuadCustom.Lines.Count < Consts.AmountLimitQuadCustom)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.QUAD_CUSTOM, 0x00, Lang.GetText(eLang.AddNewQuadCustom)));
            }
            if (DataBase.FileAEV != null && DataBase.FileAEV.Lines.Count < Consts.AmountLimitAEV)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x00, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType00_GeneralPurpose)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x01, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType01_WarpDoor)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x03, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType03_Items)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x05, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType05_Message)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x08, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType08_TypeWriter)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x0B, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType0B_FalseCollision)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x10, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.AEV, 0x13, Lang.GetText(eLang.AddNewAEV) + " 0x" + Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation)));
            }
            if (DataBase.FileITA != null && DataBase.FileITA.Lines.Count < Consts.AmountLimitITA)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x00, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType00_GeneralPurpose)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x01, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType01_WarpDoor)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x03, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType03_Items)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x05, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType05_Message)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x08, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType08_TypeWriter)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x0B, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType0B_FalseCollision)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x10, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp)));
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.ITA, 0x13, Lang.GetText(eLang.AddNewITA) + " 0x" + Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation)));
            }

            if (comboBoxType.Items.Count == 0)
            {
                comboBoxType.Items.Add(new NewEntryObjForListBox(GroupType.NULL, 0x00, Lang.GetText(eLang.AddNewNull)));
                comboBoxType.Enabled = false;
                numericUpDownAmount.Enabled = false;
                buttonOK.Enabled = false;
            }
            comboBoxType.SelectedIndex = 0;

            if (Lang.LoadedTranslation)
            {
                StartUpdateTranslation();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonCancel.Enabled = false;
            buttonOK.Enabled = false;
            if (comboBoxType.SelectedItem is NewEntryObjForListBox gt)
            {
                TreeViewDisableDrawNode?.Invoke();

                if (gt.ID == GroupType.ETS)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (int i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileETS.Lines.Count + nodes.Count < Consts.AmountLimitETS)
                        {
                            ushort NewId = DataBase.NodeETS.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.ETS, NewId);
                            nodes.Add(o);
                        }
                        else 
                        {
                            break;
                        }
                    }
                    DataBase.NodeETS.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeETS.Expand();
                }

                else if (gt.ID == GroupType.ITA)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileITA.Lines.Count + nodes.Count < Consts.AmountLimitITA)
                        {
                            ushort NewId = DataBase.NodeITA.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.ITA, NewId);
                            nodes.Add(o);

                            DataBase.Extras.UpdateExtraNodes(NewId, DataBase.NodeITA.PropertyMethods.GetSpecialType(NewId), SpecialFileFormat.ITA);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeITA.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeITA.Expand();
                    DataBase.NodeEXTRAS.Expand();
                }

                else if (gt.ID == GroupType.AEV)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileAEV.Lines.Count + nodes.Count < Consts.AmountLimitAEV)
                        {
                            ushort NewId = DataBase.NodeAEV.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.AEV, NewId);
                            nodes.Add(o);

                            DataBase.Extras.UpdateExtraNodes(NewId, DataBase.NodeAEV.PropertyMethods.GetSpecialType(NewId), SpecialFileFormat.AEV);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeAEV.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeAEV.Expand();
                    DataBase.NodeEXTRAS.Expand();
                }

                else if (gt.ID == GroupType.DSE)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (int i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileDSE.Lines.Count + nodes.Count < Consts.AmountLimitDSE)
                        {
                            ushort NewId = DataBase.NodeDSE.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.DSE, NewId);
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeDSE.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeDSE.Expand();
                }

                else if (gt.ID == GroupType.FSE)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileFSE.Lines.Count + nodes.Count < Consts.AmountLimitFSE)
                        {
                            ushort NewId = DataBase.NodeFSE.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.FSE, NewId);
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeFSE.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeFSE.Expand();
                }

                else if (gt.ID == GroupType.SAR)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileSAR.Lines.Count + nodes.Count < Consts.AmountLimitSAR)
                        {
                            ushort NewId = DataBase.NodeSAR.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.SAR, NewId);
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeSAR.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeSAR.Expand();
                }

                else if (gt.ID == GroupType.EAR)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileEAR.Lines.Count + nodes.Count < Consts.AmountLimitEAR)
                        {
                            ushort NewId = DataBase.NodeEAR.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.EAR, NewId);
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeEAR.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeEAR.Expand();
                }

                else if (gt.ID == GroupType.ESE)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileESE.Lines.Count + nodes.Count < Consts.AmountLimitESE)
                        {
                            ushort NewId = DataBase.NodeESE.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.ESE, NewId);
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeESE.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeESE.Expand();
                }

                else if (gt.ID == GroupType.EMI)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileEMI.Lines.Count + nodes.Count < Consts.AmountLimitEMI)
                        {
                            ushort NewId = DataBase.NodeEMI.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.EMI, NewId);
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeEMI.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeEMI.Expand();
                }

                else if (gt.ID == GroupType.QUAD_CUSTOM)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileQuadCustom.Lines.Count + nodes.Count < Consts.AmountLimitQuadCustom)
                        {
                            ushort NewId = DataBase.NodeQuadCustom.ChangeAmountMethods.AddNewLineID(gt.InitType);
                            Object3D o = Object3D.CreateNewInstance(GroupType.QUAD_CUSTOM, NewId);
                            o.NodeFont = Globals.TreeNodeFontText;
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeQuadCustom.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeQuadCustom.Expand();
                }

                TreeViewEnableDrawNode?.Invoke();
            }
            OnButtonOk_Click?.Invoke();
            Close();
        }

        private void AddNewObjForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void StartUpdateTranslation() 
        {
            this.Text = Lang.GetText(eLang.AddNewObjForm);
            buttonCancel.Text = Lang.GetText(eLang.AddNewObjButtonCancel);
            buttonOK.Text = Lang.GetText(eLang.AddNewObjButtonOK);
            labelAmountInfo.Text = Lang.GetText(eLang.labelAmountInfo);
            labelTypeInfo.Text = Lang.GetText(eLang.labelTypeInfo);
        }
    }
}
