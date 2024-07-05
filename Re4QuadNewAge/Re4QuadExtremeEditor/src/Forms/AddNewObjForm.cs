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

        public AddNewObjForm()
        {
            InitializeComponent();

            KeyPreview = true;
            if (DataBase.FileETS != null && DataBase.FileETS.Lines.Count < 10000)
            {
                comboBoxType.Items.Add(new GroupTypeObjForListBox(GroupType.ETS, Lang.GetText(eLang.AddNewETS)));
            }
            if (DataBase.FileITA != null && DataBase.FileITA.Lines.Count < 10000)
            {
                comboBoxType.Items.Add(new GroupTypeObjForListBox(GroupType.ITA, Lang.GetText(eLang.AddNewITA)));
            }
            if (DataBase.FileAEV != null && DataBase.FileAEV.Lines.Count < 10000)
            {
                comboBoxType.Items.Add(new GroupTypeObjForListBox(GroupType.AEV, Lang.GetText(eLang.AddNewAEV)));
            }

            if (comboBoxType.Items.Count == 0)
            {
                comboBoxType.Items.Add(new GroupTypeObjForListBox(GroupType.NULL, Lang.GetText(eLang.AddNewNull)));
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
            if (comboBoxType.SelectedItem is GroupTypeObjForListBox gt)
            {
                if (gt.ID == GroupType.ETS)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (int i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileETS.Lines.Count < 10000)
                        {
                            ushort NewId = DataBase.NodeETS.ChangeAmountMethods.AddNewLineID();
                            Object3D o = new Object3D();
                            o.Name = NewId.ToString();
                            o.Text = "";
                            o.Group = GroupType.ETS;
                            o.ObjLineRef = NewId;
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

                if (gt.ID == GroupType.ITA)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileITA.Lines.Count < 10000)
                        {
                            ushort NewId = DataBase.NodeITA.ChangeAmountMethods.AddNewLineID();
                            Object3D o = new Object3D();
                            o.Name = NewId.ToString();
                            o.Text = "";
                            o.Group = GroupType.ITA;
                            o.ObjLineRef = NewId;
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeITA.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeITA.Expand();
                }

                if (gt.ID == GroupType.AEV)
                {
                    List<Object3D> nodes = new List<Object3D>();
                    for (ushort i = 0; i < numericUpDownAmount.Value; i++)
                    {
                        if (DataBase.FileAEV.Lines.Count < 10000)
                        {
                            ushort NewId = DataBase.NodeAEV.ChangeAmountMethods.AddNewLineID();
                            Object3D o = new Object3D();
                            o.Name = NewId.ToString();
                            o.Text = "";
                            o.Group = GroupType.AEV;
                            o.ObjLineRef = NewId;
                            nodes.Add(o);
                        }
                        else
                        {
                            break;
                        }
                    }
                    DataBase.NodeAEV.Nodes.AddRange(nodes.ToArray());
                    DataBase.NodeAEV.Expand();
                }
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
            buttonCancel.Text = Lang.GetText(eLang.buttonCancel);
            buttonOK.Text = Lang.GetText(eLang.buttonOK);
            labelAmountInfo.Text = Lang.GetText(eLang.labelAmountInfo);
            labelTypeInfo.Text = Lang.GetText(eLang.labelTypeInfo);
        }
    }
}
