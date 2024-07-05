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

namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class SearchForm : Form
    {
        public event Re4QuadExtremeEditor.src.Class.CustomDelegates.ReturnSearch Search;

        object[] List = new object[0];
        object selected = null;

        public SearchForm(object[] List, object SelectedObj = null)
        {
            InitializeComponent();

            this.MouseWheel += SearchForm_MouseWheel;

            KeyPreview = true;

            textBoxSearch.KeyDown += TextBoxSearch_KeyDown;

            listBoxCont.Items.AddRange(List);
            this.List = List;

            if (listBoxCont.Items.Contains(SelectedObj))
            {
                listBoxCont.SelectedIndex = listBoxCont.Items.IndexOf(SelectedObj);
                selected = SelectedObj;
            }
            else
            {
                if (listBoxCont.Items.Count != 0)
                {
                    listBoxCont.SelectedIndex = 0;
                }
            }

            checkBoxFilterMode.SuspendLayout();
            checkBoxFilterMode.Checked = Globals.SearchFilterMode;
            checkBoxFilterMode.ResumeLayout();

            if (Lang.LoadedTranslation)
            {
                StartUpdateTranslation();
            }
        }

        private void TextBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (listBoxCont.SelectedIndex != 0)
                {
                    listBoxCont.SelectedIndex -= 1;
                }
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                if (listBoxCont.SelectedIndex != listBoxCont.Items.Count - 1)
                {
                    listBoxCont.SelectedIndex += 1;
                }
                e.Handled = true;
            }

        }

        private void SearchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listBoxCont.SelectedIndex > -1)
            {
                Search?.Invoke(listBoxCont.SelectedItem);
                Close();
            }
        }
        private void listBoxCont_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxCont.SelectedIndex > -1)
            {
                Search?.Invoke(listBoxCont.SelectedItem);
                Close();
            }
         
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text != "")
            {
                if (Globals.SearchFilterMode)
                {
                    string ToSearch = textBoxSearch.Text.ToUpper();

                    var res = (from obj in List
                               where obj.ToString().ToUpper().Contains(ToSearch)
                               select obj).ToArray();

                    listBoxCont.SuspendLayout();
                    listBoxCont.Items.Clear();
                    listBoxCont.Items.AddRange(res);
                    listBoxCont.ResumeLayout();
                }

                for (int i = 0; i < listBoxCont.Items.Count; i++)
                {
                    string cont = listBoxCont.Items[i].ToString().ToUpper();
                    if (cont.Contains(textBoxSearch.Text.ToUpper()))
                    {
                        listBoxCont.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                if (Globals.SearchFilterMode)
                {
                    listBoxCont.SuspendLayout();
                    listBoxCont.Items.Clear();
                    listBoxCont.Items.AddRange(List);

                    if (listBoxCont.Items.Contains(selected))
                    {
                        listBoxCont.SelectedIndex = listBoxCont.Items.IndexOf(selected);
                    }
                    else
                    {
                        if (listBoxCont.Items.Count != 0)
                        {
                            listBoxCont.SelectedIndex = 0;
                        }
                    }

                    listBoxCont.ResumeLayout();
                }
             
            }

        }

        private void listBoxCont_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxSearch.Focus();
        }

        private void SearchForm_MouseWheel(object sender, MouseEventArgs e)
        {
            int current = listBoxCont.TopIndex;
            listBoxCont.TopIndex = current + ((e.Delta * -1) / 60);
        }

        private void checkBoxFilterMode_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSearch.Focus();
            Globals.SearchFilterMode = checkBoxFilterMode.Checked;
            if (Globals.SearchFilterMode)
            {
                if (textBoxSearch.Text != "")
                {

                    string ToSearch = textBoxSearch.Text.ToUpper();

                    var res = (from obj in List
                               where obj.ToString().ToUpper().Contains(ToSearch)
                               select obj).ToArray();

                    listBoxCont.SuspendLayout();
                    listBoxCont.Items.Clear();
                    listBoxCont.Items.AddRange(res);
                    listBoxCont.ResumeLayout();

                    for (int i = 0; i < listBoxCont.Items.Count; i++)
                    {
                        string cont = listBoxCont.Items[i].ToString().ToUpper();
                        if (cont.Contains(textBoxSearch.Text.ToUpper()))
                        {
                            listBoxCont.SelectedIndex = i;
                            break;
                        }
                    }
                }

            }
            else
            {
                listBoxCont.SuspendLayout();
                listBoxCont.Items.Clear();
                listBoxCont.Items.AddRange(List);

                if (listBoxCont.Items.Contains(selected))
                {
                    listBoxCont.SelectedIndex = listBoxCont.Items.IndexOf(selected);
                }
                else
                {
                    if (listBoxCont.Items.Count != 0)
                    {
                        listBoxCont.SelectedIndex = 0;
                    }
                }
                listBoxCont.ResumeLayout();
            }
        }

        private void listBoxCont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCont.SelectedIndex > -1)
            {
                selected = listBoxCont.SelectedItem;
            }
            else 
            {
                selected = null;
            }
        }

        void StartUpdateTranslation() 
        {
            this.Text = Lang.GetText(eLang.SearchForm);
            checkBoxFilterMode.Text = Lang.GetText(eLang.checkBoxFilterMode);
        }
    }
}
