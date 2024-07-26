using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class OptionsForm : Form
    {
        public event Class.CustomDelegates.ActivateMethod OnOKButtonClick;

        private int FrationalAmount = 9;

        private bool EnableRadioButtons = false;

        public OptionsForm()
        {
            InitializeComponent();
            KeyPreview = true;
            Size = MinimumSize;

            //aba 1
            textBoxDiretoryXFILE.Text = Globals.DirectoryXFILE;
            textBoxDirectory2007RE4.Text = Globals.Directory2007RE4;
            textBoxDirectoryPS2RE4.Text = Globals.DirectoryPS2RE4;
            textBoxDirectoryUHDRE4.Text = Globals.DirectoryUHDRE4;
            textBoxDirectoryCustom1.Text = Globals.DirectoryCustom1;
            textBoxDirectoryCustom2.Text = Globals.DirectoryCustom2;
            textBoxDirectoryCustom3.Text = Globals.DirectoryCustom3;

            //aba 2
            var EnemiesLists = GetEnemiesListJson();
            var EtcModelsLists = GetEtcModelsListJson();
            var ItemsLists = GetItemsListJson();
            var QuadCustomLists = GetQuadCustomListJson();
            comboBoxEnemies.Items.AddRange(EnemiesLists);
            comboBoxEtcModels.Items.AddRange(EtcModelsLists);
            comboBoxItems.Items.AddRange(ItemsLists);
            comboBoxQuadCustom.Items.AddRange(QuadCustomLists);

            comboBoxEnemies.SelectedIndex = 0;
            comboBoxEtcModels.SelectedIndex = 0;
            comboBoxItems.SelectedIndex = 0;
            comboBoxQuadCustom.SelectedIndex = 0;

            JSON.ObjectInfoList selectedEnemiesList = EnemiesLists.Where(x => x.JsonFileName == Globals.FileDiretoryEnemiesList).FirstOrDefault();
            if (selectedEnemiesList != null)
            {
                int index = comboBoxEnemies.Items.IndexOf(selectedEnemiesList);
                if (index > -1)
                {
                    comboBoxEnemies.SelectedIndex = index;
                }
            }

            JSON.ObjectInfoList selectedEtcModelsList = EtcModelsLists.Where(x => x.JsonFileName == Globals.FileDiretoryEtcModelsList).FirstOrDefault();
            if(selectedEtcModelsList != null)
            {
                int index = comboBoxEtcModels.Items.IndexOf(selectedEtcModelsList);
                if (index > -1)
                {
                    comboBoxEtcModels.SelectedIndex = index;
                }
            }

            JSON.ObjectInfoList selectedItemsList = ItemsLists.Where(x => x.JsonFileName == Globals.FileDiretoryItemsList).FirstOrDefault();
            if (selectedItemsList != null)
            {
                int index = comboBoxItems.Items.IndexOf(selectedItemsList);
                if (index > -1)
                {
                    comboBoxItems.SelectedIndex = index;
                }
            }

            JSON.QuadCustomInfoList selectedQuadCustomList = QuadCustomLists.Where(x => x.JsonFileName == Globals.FileDiretoryQuadCustomList).FirstOrDefault();
            if (selectedQuadCustomList != null)
            {
                int index = comboBoxQuadCustom.Items.IndexOf(selectedQuadCustomList);
                if (index > -1)
                {
                    comboBoxQuadCustom.SelectedIndex = index;
                }
            }


            //aba 3
            panelSkyColor.BackColor = Globals.SkyColor;

            comboBoxLanguage.Items.Add(Lang.GetText(eLang.OptionsUseInternalLanguage));
            comboBoxLanguage.Items.AddRange(GetLangList());
            int langIndex = comboBoxLanguage.Items.IndexOf(new JSON.LangObjForList("", Globals.BackupConfigs.LangJsonFile));
            if (langIndex == -1 || Globals.BackupConfigs.LoadLangTranslation == false)
            {
                langIndex = 0;
            }
            comboBoxLanguage.SelectedIndex = langIndex;

            comboBoxItemRotationOrder.Items.AddRange(Utils.ItemRotationOrderForListBox());

            ConfigFrationalSymbol frationalSymbol = Globals.FrationalSymbol;
            switch (frationalSymbol)
            {
                case ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputComma:
                    radioButtonAcceptsCommaAndPeriod.Checked = true;
                    radioButtonOutputComma.Checked = true;
                    break;
                case ConfigFrationalSymbol.OnlyAcceptComma:
                    radioButtonOnlyAcceptComma.Checked = true;
                    radioButtonOutputComma.Checked = true;
                    groupBoxOutputFractionalSymbol.Enabled = false;
                    break;
                case ConfigFrationalSymbol.OnlyAcceptPeriod:
                    radioButtonOnlyAcceptPeriod.Checked = true;
                    radioButtonOutputPeriod.Checked = true;
                    groupBoxOutputFractionalSymbol.Enabled = false;
                    break;
             case ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputPeriod:
                default:
                    radioButtonAcceptsCommaAndPeriod.Checked = true;
                    radioButtonOutputPeriod.Checked = true;
                    break;
            }

            FrationalAmount = Globals.FrationalAmount;
            labelFrationalAmount.Text = FrationalAmount.ToString();

            checkBoxDisableItemRotations.Checked = Globals.ItemDisableRotationAll;
            checkBoxIgnoreRotationForZeroXYZ.Checked = Globals.ItemDisableRotationIfXorYorZequalZero;
            checkBoxIgnoreRotationForZisNotGreaterThanZero.Checked = Globals.ItemDisableRotationIfZisNotGreaterThanZero;
            numericUpDownDivider.Value = (decimal)Globals.ItemRotationCalculationDivider;
            numericUpDownMultiplier.Value = (decimal)Globals.ItemRotationCalculationMultiplier;
            comboBoxItemRotationOrder.SelectedIndex = (int)Globals.ItemRotationOrder;

            checkBoxUseDarkerGrayTheme.Checked = Globals.BackupConfigs.UseDarkerGrayTheme;

            EnableRadioButtons = true;

            if (Lang.LoadedTranslation)
            {
                StartUpdateTranslation();
            }
        }

        private void OptionsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string FixDirectory(string dir) 
        {
            return dir != null && dir.Length > 0 ? (dir + (dir.Last() != '\\' ? "\\" : "")) : "";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
          
            Globals.DirectoryXFILE = FixDirectory(textBoxDiretoryXFILE.Text);
            Globals.Directory2007RE4 = FixDirectory(textBoxDirectory2007RE4.Text);
            Globals.DirectoryPS2RE4 = FixDirectory(textBoxDirectoryPS2RE4.Text);
            Globals.DirectoryUHDRE4 = FixDirectory(textBoxDirectoryUHDRE4.Text);
            Globals.DirectoryCustom1 = FixDirectory(textBoxDirectoryCustom1.Text);
            Globals.DirectoryCustom2 = FixDirectory(textBoxDirectoryCustom2.Text);
            Globals.DirectoryCustom3 = FixDirectory(textBoxDirectoryCustom3.Text);

            Globals.FileDiretoryEnemiesList = ((JSON.ObjectInfoList)comboBoxEnemies.SelectedItem).JsonFileName;
            Globals.FileDiretoryEtcModelsList = ((JSON.ObjectInfoList)comboBoxEtcModels.SelectedItem).JsonFileName;
            Globals.FileDiretoryItemsList = ((JSON.ObjectInfoList)comboBoxItems.SelectedItem).JsonFileName;
            Globals.FileDiretoryQuadCustomList = ((JSON.QuadCustomInfoList)comboBoxQuadCustom.SelectedItem).JsonFileName;

            Utils.StartReloadDirectoryDic();

            Globals.SkyColor = panelSkyColor.BackColor;

            if (radioButtonAcceptsCommaAndPeriod.Checked && radioButtonOutputComma.Checked)
            {
                Globals.FrationalSymbol = ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputComma;
            }
            else if (radioButtonAcceptsCommaAndPeriod.Checked && radioButtonOutputPeriod.Checked)
            {
                Globals.FrationalSymbol = ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputPeriod;
            }
            else if (radioButtonOnlyAcceptComma.Checked)
            {
                Globals.FrationalSymbol = ConfigFrationalSymbol.OnlyAcceptComma;
            }
            else if (radioButtonOnlyAcceptPeriod.Checked)
            {
                Globals.FrationalSymbol = ConfigFrationalSymbol.OnlyAcceptPeriod;
            }

            Globals.FrationalAmount = FrationalAmount;

            Globals.ItemDisableRotationAll = checkBoxDisableItemRotations.Checked;
            Globals.ItemDisableRotationIfXorYorZequalZero = checkBoxIgnoreRotationForZeroXYZ.Checked;
            Globals.ItemDisableRotationIfZisNotGreaterThanZero = checkBoxIgnoreRotationForZisNotGreaterThanZero.Checked;
            Globals.ItemRotationCalculationDivider = (float)numericUpDownDivider.Value;
            Globals.ItemRotationCalculationMultiplier = (float)numericUpDownMultiplier.Value;
            Globals.ItemRotationOrder = (ObjRotationOrder)comboBoxItemRotationOrder.SelectedIndex;


            Globals.BackupConfigs.DirectoryXFILE = Globals.DirectoryXFILE;
            Globals.BackupConfigs.Directory2007RE4 = Globals.Directory2007RE4;
            Globals.BackupConfigs.DirectoryPS2RE4 = Globals.DirectoryPS2RE4;
            Globals.BackupConfigs.DirectoryUHDRE4 = Globals.DirectoryUHDRE4;
            Globals.BackupConfigs.DirectoryCustom1 = Globals.DirectoryCustom1;
            Globals.BackupConfigs.DirectoryCustom2 = Globals.DirectoryCustom2;
            Globals.BackupConfigs.DirectoryCustom3 = Globals.DirectoryCustom3;

            Globals.BackupConfigs.FileDiretoryEnemiesList = Globals.FileDiretoryEnemiesList;
            Globals.BackupConfigs.FileDiretoryEtcModelsList = Globals.FileDiretoryEtcModelsList;
            Globals.BackupConfigs.FileDiretoryItemsList = Globals.FileDiretoryItemsList;
            Globals.BackupConfigs.FileDiretoryQuadCustomList = Globals.FileDiretoryQuadCustomList;

            Globals.BackupConfigs.SkyColor = Globals.SkyColor;
            Globals.BackupConfigs.FrationalAmount = Globals.FrationalAmount;
            Globals.BackupConfigs.FrationalSymbol = Globals.FrationalSymbol;
            Globals.BackupConfigs.ItemDisableRotationAll = Globals.ItemDisableRotationAll;
            Globals.BackupConfigs.ItemDisableRotationIfXorYorZequalZero = Globals.ItemDisableRotationIfXorYorZequalZero;
            Globals.BackupConfigs.ItemDisableRotationIfZisNotGreaterThanZero = Globals.ItemDisableRotationIfZisNotGreaterThanZero;
            Globals.BackupConfigs.ItemRotationCalculationDivider = Globals.ItemRotationCalculationDivider;
            Globals.BackupConfigs.ItemRotationCalculationMultiplier = Globals.ItemRotationCalculationMultiplier;
            Globals.BackupConfigs.ItemRotationOrder = Globals.ItemRotationOrder;

            Globals.BackupConfigs.UseDarkerGrayTheme = checkBoxUseDarkerGrayTheme.Checked;

            if (comboBoxLanguage.SelectedIndex <= 0)
            {
                Globals.BackupConfigs.LoadLangTranslation = false;
                Globals.BackupConfigs.LangJsonFile = "";
            }
            else
            {
                var langSelected = (JSON.LangObjForList)comboBoxLanguage.SelectedItem;
                Globals.BackupConfigs.LoadLangTranslation = true;
                Globals.BackupConfigs.LangJsonFile = langSelected.LangJsonFileName;
            }

            JSON.ConfigsFile.writeConfigsFile(Consts.ConfigsFileDirectory, Globals.BackupConfigs);

            tabControlConfigs.Enabled = false;
            checkBoxForceReloadModels.Enabled = false;
            buttonCancel.Enabled = false;
            buttonOK.Enabled = false;

            bool ForceReload = checkBoxForceReloadModels.Checked;
            // aviso de demora, e recarrega modelos/jsons
            if (ForceReload)
            {
                MessageBox.Show(Lang.GetText(eLang.OptionsFormWarningLoadModelsMessageBoxDialog), Lang.GetText(eLang.OptionsFormWarningLoadModelsMessageBoxTitle));
     
                Utils.ReloadJsonFiles();
                Utils.ReloadModels();
            }

            OnOKButtonClick?.Invoke();
            Close();

        }

        private void panelSkyColor_Click(object sender, EventArgs e)
        {
            colorDialogColors.Color = panelSkyColor.BackColor;
            if (colorDialogColors.ShowDialog() == DialogResult.OK)
            {
                panelSkyColor.BackColor = colorDialogColors.Color;
            }
        }

        private void radioButtonAcceptsCommaAndPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableRadioButtons && radioButtonAcceptsCommaAndPeriod.Checked)
            {
                groupBoxOutputFractionalSymbol.Enabled = true;
            }
        }

        private void radioButtonOnlyAcceptComma_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableRadioButtons && radioButtonOnlyAcceptComma.Checked)
            {
                groupBoxOutputFractionalSymbol.Enabled = false;
                radioButtonOutputPeriod.Checked = false;
                radioButtonOutputComma.Checked = true;
            }
        }

        private void radioButtonOnlyAcceptPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableRadioButtons && radioButtonOnlyAcceptPeriod.Checked)
            {
                groupBoxOutputFractionalSymbol.Enabled = false;
                radioButtonOutputComma.Checked = false;
                radioButtonOutputPeriod.Checked = true;
            }
        }

        private void buttonFrationalMinus_Click(object sender, EventArgs e)
        {
            if (FrationalAmount > 4)
            {
                FrationalAmount -= 1;
                labelFrationalAmount.Text = FrationalAmount.ToString();
            }
        }

        private void buttonFrationalPlus_Click(object sender, EventArgs e)
        {
            if (FrationalAmount < 9)
            {
                FrationalAmount += 1;
                labelFrationalAmount.Text = FrationalAmount.ToString();
            }
        }

        //diretorios selecionar
        private void buttonDiretoryXFILE_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " XFILE";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDiretoryXFILE.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDiretoryXFILE.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDiretoryXFILE.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private void buttonDirectory2007RE4_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " RE4 2007";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDirectory2007RE4.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDirectory2007RE4.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectory2007RE4.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private void buttonDirectoryPS2RE4_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " RE4 PS2";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDirectoryPS2RE4.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDirectoryPS2RE4.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectoryPS2RE4.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private void buttonDirectoryUHDRE4_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " RE4 UHD";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDirectoryUHDRE4.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDirectoryUHDRE4.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectoryUHDRE4.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private void buttonDirectoryCustom1_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " Custom1";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDirectoryCustom1.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDirectoryCustom1.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectoryCustom1.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private void buttonDirectoryCustom2_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " Custom2";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDirectoryCustom2.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDirectoryCustom2.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectoryCustom2.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private void buttonDirectoryCustom3_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDiretory.Description = Lang.GetText(eLang.OptionsFormSelectDiretory) + " Custom3";
            folderBrowserDialogDiretory.SelectedPath = "";
            if (Directory.Exists(textBoxDirectoryCustom3.Text))
            {
                folderBrowserDialogDiretory.SelectedPath = textBoxDirectoryCustom3.Text;
            }
            if (folderBrowserDialogDiretory.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectoryCustom3.Text = folderBrowserDialogDiretory.SelectedPath;
            }
        }

        private JSON.LangObjForList[] GetLangList() 
        {
            List<JSON.LangObjForList> list = new List<JSON.LangObjForList>();

            string directory = Path.Combine(AppContext.BaseDirectory, Consts.LangDirectory);

            string[] Files = new string[0];

            if (Directory.Exists(directory))
            {
                Files = Directory.GetFiles(directory, "*.json");
            }

            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    var file = JSON.LangFile.ParseFromFileForList(Files[i]);
                    if (file != null && !list.Contains(file))
                    {
                        list.Add(file);
                    }
                }
                catch (Exception)
                {
                }
            }

            return list.ToArray();
        }

        private JSON.ObjectInfoList[] GetEnemiesListJson() 
        {
            List<JSON.ObjectInfoList> lists = new List<JSON.ObjectInfoList>();

            string directory = Path.Combine(AppContext.BaseDirectory, Consts.EnemiesDirectory);

            string[] Files = new string[0];

            if (Directory.Exists(directory))
            {
                Files = Directory.GetFiles(directory, "*.json");
            }

            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    var file = JSON.ObjectInfoListFile.ParseFromFileForOptions(Files[i], Consts.NameEnemiesList);
                    if (file != null && !lists.Contains(file))
                    {
                        lists.Add(file);
                    }
                }
                catch (Exception)
                {
                }
            }

            JSON.ObjectInfoList _default = new JSON.ObjectInfoList(Consts.DefaultEnemiesListFileDirectory, "Default List", "null", new Dictionary<ushort, JSON.ObjectInfo>());
            if (_default != null && !lists.Contains(_default))
            {
                lists.Add(_default);
            }
    
            return lists.ToArray();
        }

        private JSON.ObjectInfoList[] GetEtcModelsListJson()
        {
            List<JSON.ObjectInfoList> lists = new List<JSON.ObjectInfoList>();

            string directory = Path.Combine(AppContext.BaseDirectory, Consts.EtcModelsDirectory);

            string[] Files = new string[0];

            if (Directory.Exists(directory))
            {
                Files = Directory.GetFiles(directory, "*.json");
            }

            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    var file = JSON.ObjectInfoListFile.ParseFromFileForOptions(Files[i], Consts.NameEtcModelsList);
                    if (file != null && !lists.Contains(file))
                    {
                        lists.Add(file);
                    }
                }
                catch (Exception)
                {
                }
            }

            JSON.ObjectInfoList _default = new JSON.ObjectInfoList(Consts.DefaultEtcModelsListFileDirectory, "Default List", "null", new Dictionary<ushort, JSON.ObjectInfo>());
            if (_default != null && !lists.Contains(_default))
            {
                lists.Add(_default);
            }

            return lists.ToArray();
        }

        private JSON.ObjectInfoList[] GetItemsListJson()
        {
            List<JSON.ObjectInfoList> lists = new List<JSON.ObjectInfoList>();

            string directory = Path.Combine(AppContext.BaseDirectory, Consts.ItemsDirectory);

            string[] Files = new string[0];

            if (Directory.Exists(directory))
            {
                Files = Directory.GetFiles(directory, "*.json");
            }

            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    var file = JSON.ObjectInfoListFile.ParseFromFileForOptions(Files[i], Consts.NameItemsList);
                    if (file != null && !lists.Contains(file))
                    {
                        lists.Add(file);
                    }
                }
                catch (Exception)
                {
                }
            }

            JSON.ObjectInfoList _default = new JSON.ObjectInfoList(Consts.DefaultItemsListFileDirectory, "Default List", "null", new Dictionary<ushort, JSON.ObjectInfo>());
            if (_default != null && !lists.Contains(_default))
            {
                lists.Add(_default);
            }

            return lists.ToArray();
        }

        private JSON.QuadCustomInfoList[] GetQuadCustomListJson()
        {
            List<JSON.QuadCustomInfoList> lists = new List<JSON.QuadCustomInfoList>();

            string directory = Path.Combine(AppContext.BaseDirectory, Consts.QuadCustomDirectory);

            string[] Files = new string[0];

            if (Directory.Exists(directory))
            {
                Files = Directory.GetFiles(directory, "*.json");
            }

            for (int i = 0; i < Files.Length; i++)
            {
                try
                {
                    var file = JSON.QuadCustomInfoListFile.ParseFromFileForOptions(Files[i]);
                    if (file != null && !lists.Contains(file))
                    {
                        lists.Add(file);
                    }
                }
                catch (Exception)
                {
                }
            }

            JSON.QuadCustomInfoList _default = new JSON.QuadCustomInfoList(Consts.DefaultQuadCustomModelsListFileDirectory, "Default List", "null", new Dictionary<uint, JSON.QuadCustomInfo>());
            if (_default != null && !lists.Contains(_default))
            {
                lists.Add(_default);
            }

            return lists.ToArray();
        }


        private void StartUpdateTranslation() 
        {
            this.Text = Lang.GetText(eLang.OptionsForm);
            buttonCancel.Text = Lang.GetText(eLang.Options_buttonCancel);
            buttonOK.Text = Lang.GetText(eLang.Options_buttonOK);
            checkBoxDisableItemRotations.Text = Lang.GetText(eLang.checkBoxDisableItemRotations);
            checkBoxForceReloadModels.Text = Lang.GetText(eLang.checkBoxForceReloadModels);
            checkBoxIgnoreRotationForZeroXYZ.Text = Lang.GetText(eLang.checkBoxIgnoreRotationForZeroXYZ);
            checkBoxIgnoreRotationForZisNotGreaterThanZero.Text = Lang.GetText(eLang.checkBoxIgnoreRotationForZisNotGreaterThanZero);
            groupBoxColors.Text = Lang.GetText(eLang.groupBoxColors);
            groupBoxDirectory.Text = Lang.GetText(eLang.groupBoxDirectory);
            groupBoxFloatStyle.Text = Lang.GetText(eLang.groupBoxFloatStyle);
            groupBoxFractionalPart.Text = Lang.GetText(eLang.groupBoxFractionalPart);
            groupBoxInputFractionalSymbol.Text = Lang.GetText(eLang.groupBoxInputFractionalSymbol);
            groupBoxItemRotations.Text = Lang.GetText(eLang.groupBoxItemRotations);
            groupBoxLanguage.Text = Lang.GetText(eLang.groupBoxLanguage);
            groupBoxOutputFractionalSymbol.Text = Lang.GetText(eLang.groupBoxOutputFractionalSymbol);
            labelDivider.Text = Lang.GetText(eLang.labelDivider);
            labelItemExtraCalculation.Text = Lang.GetText(eLang.labelItemExtraCalculation);
            labelitemRotationOrderText.Text = Lang.GetText(eLang.labelitemRotationOrderText);
            labelLanguageWarning.Text = Lang.GetText(eLang.labelLanguageWarning);
            labelMultiplier.Text = Lang.GetText(eLang.labelMultiplier);
            labelSkyColor.Text = Lang.GetText(eLang.labelSkyColor);
            radioButtonAcceptsCommaAndPeriod.Text = Lang.GetText(eLang.radioButtonAcceptsCommaAndPeriod);
            radioButtonOnlyAcceptComma.Text = Lang.GetText(eLang.radioButtonOnlyAcceptComma);
            radioButtonOnlyAcceptPeriod.Text = Lang.GetText(eLang.radioButtonOnlyAcceptPeriod);
            radioButtonOutputComma.Text = Lang.GetText(eLang.radioButtonOutputComma);
            radioButtonOutputPeriod.Text = Lang.GetText(eLang.radioButtonOutputPeriod);
            tabPageDiretory.Text = Lang.GetText(eLang.tabPageDiretory);
            tabPageOthers.Text = Lang.GetText(eLang.tabPageOthers);
            tabPageLists.Text = Lang.GetText(eLang.tabPageLists);
            groupBoxLists.Text = Lang.GetText(eLang.groupBoxLists);
            labelEnemies.Text = Lang.GetText(eLang.labelEnemies);
            labelEtcModels.Text = Lang.GetText(eLang.labelEtcModels);
            labelItems.Text = Lang.GetText(eLang.labelItems);
            labelQuadCustom.Text = Lang.GetText(eLang.labelQuadCustom);
            groupBoxTheme.Text = Lang.GetText(eLang.groupBoxTheme);
            labelThemeWarning.Text = Lang.GetText(eLang.labelThemeWarning);
            checkBoxUseDarkerGrayTheme.Text = Lang.GetText(eLang.checkBoxUseDarkerGrayTheme);


            labelDirectoryXFILE.Text = "XFILE " + Lang.GetText(eLang.labelOptionsDirectory);
            labelDirectory2007RE4.Text = "RE4 2007 " + Lang.GetText(eLang.labelOptionsDirectory);
            labelDirectoryPS2RE4.Text = "RE4 PS2 " + Lang.GetText(eLang.labelOptionsDirectory);
            labelDirectoryUHDRE4.Text = "RE4 UHD " + Lang.GetText(eLang.labelOptionsDirectory);
            labelDirectoryCustom1.Text = "Custom1 " + Lang.GetText(eLang.labelOptionsDirectory);
            labelDirectoryCustom2.Text = "Custom2 " + Lang.GetText(eLang.labelOptionsDirectory);
            labelDirectoryCustom3.Text = "Custom3 " + Lang.GetText(eLang.labelOptionsDirectory);
        }

  
    }
}
