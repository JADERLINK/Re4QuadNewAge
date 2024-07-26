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
using Re4QuadExtremeEditor.src.Class.MyProperty;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.Enums;
using System.Globalization;

namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class MultiSelectEditorForm : Form
    {

        GenericProperty[] ALL = new GenericProperty[0];

        Dictionary<MultiSelectKey, MultiSelectObj> multiSelectObjs = new Dictionary<MultiSelectKey, MultiSelectObj>();

        bool ComboBoxPropertyListIsChanged_Enable = false;
        bool checkBoxHexadecimalIsChanged_Enable = false;
        bool checkBoxDecimalIsChanged_Enable = false;
        bool checkBoxProgressiveSumIsChanged_Enable = false;
        bool checkBoxCurrentValuePlusValueToAddIsChanged_Enable = false;

        int ByteArryLenght = 0;

        UpdateMethods updateMethods;

        Type selectedType = null;
        /// <summary>
        ///  Nota: o Centeudo dessa classe foi feito de modo provisorio permanente (Então tem a tal da "Gambiarra")
        /// </summary>
        /// <param name="obj"></param>
        public MultiSelectEditorForm(ref MultiSelectObjInfoToProperty objM)
        {
            InitializeComponent();

            KeyPreview = true;

            updateMethods = objM.updateMethods;

            List<GenericProperty> All_List = new List<GenericProperty>(objM.GetCount());

            var Selecteds = objM.GetObject3DList();

            var Object3D_ESL = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ESL).Cast<Object3D>();
            var Object3D_ETS = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ETS).Cast<Object3D>();
            var Object3D_Sepecial = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && (g.Group == GroupType.ITA || g.Group == GroupType.AEV)).Cast<Object3D>();
            var Object3D_DSE = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.DSE).Cast<Object3D>();
            var Object3D_FSE = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.FSE).Cast<Object3D>();
            var Object3D_ESAR = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && (g.Group == GroupType.SAR || g.Group == GroupType.EAR)).Cast<Object3D>();
            var Object3D_ESE = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ESE).Cast<Object3D>();
            var Object3D_EMI = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.EMI).Cast<Object3D>();
            var Object3D_QuadCustom = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.QUAD_CUSTOM).Cast<Object3D>();

            var PropertyList_ESL = (from obj in Object3D_ESL select new EnemyProperty(obj.ObjLineRef, updateMethods, ((EnemyNodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_ETS = (from obj in Object3D_ETS select new EtcModelProperty(obj.ObjLineRef, updateMethods, ((EtcModelNodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_Special = (from obj in Object3D_Sepecial select new SpecialProperty(obj.ObjLineRef, updateMethods, ((SpecialNodeGroup)obj.Parent).PropertyMethods, false, true));
            var PropertyList_DSE = (from obj in Object3D_DSE select new NewAge_DSE_Property(obj.ObjLineRef, updateMethods, ((NewAge_DSE_NodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_FSE = (from obj in Object3D_FSE select new NewAge_FSE_Property(obj.ObjLineRef, updateMethods, ((NewAge_FSE_NodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_ESAR = (from obj in Object3D_ESAR select new NewAge_ESAR_Property(obj.ObjLineRef, updateMethods, ((NewAge_ESAR_NodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_ESE = (from obj in Object3D_ESE select new NewAge_ESE_Property(obj.ObjLineRef, updateMethods, ((NewAge_ESE_NodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_EMI = (from obj in Object3D_EMI select new NewAge_EMI_Property(obj.ObjLineRef, updateMethods, ((NewAge_EMI_NodeGroup)obj.Parent).PropertyMethods, true));
            var PropertyList_QuadCustom = (from obj in Object3D_QuadCustom select new QuadCustomProperty(obj.ObjLineRef, updateMethods, ((QuadCustomNodeGroup)obj.Parent).PropertyMethods, true));
            
            All_List.AddRange(PropertyList_ESL);
            All_List.AddRange(PropertyList_ETS);
            All_List.AddRange(PropertyList_Special);
            All_List.AddRange(PropertyList_DSE);
            All_List.AddRange(PropertyList_FSE);
            All_List.AddRange(PropertyList_ESAR);
            All_List.AddRange(PropertyList_ESE);
            All_List.AddRange(PropertyList_EMI);
            All_List.AddRange(PropertyList_QuadCustom);
            ALL = All_List.ToArray();


            if (PropertyList_ESL.Count() > 0)
            {
                EnemyProperty p = new EnemyProperty(PropertyList_ESL.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_ETS.Count() > 0)
            {
                EtcModelProperty p = new EtcModelProperty(PropertyList_ETS.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_Special.Count() > 0)
            {
                SpecialProperty p = new SpecialProperty(PropertyList_Special.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_DSE.Count() > 0)
            {
                NewAge_DSE_Property p = new NewAge_DSE_Property(PropertyList_DSE.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }


            if (PropertyList_FSE.Count() > 0)
            {
                NewAge_FSE_Property p = new NewAge_FSE_Property(PropertyList_FSE.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_ESAR.Count() > 0)
            {
                NewAge_ESAR_Property p = new NewAge_ESAR_Property(PropertyList_ESAR.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_ESE.Count() > 0)
            {
                NewAge_ESE_Property p = new NewAge_ESE_Property(PropertyList_ESE.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_EMI.Count() > 0)
            {
                NewAge_EMI_Property p = new NewAge_EMI_Property(PropertyList_EMI.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            if (PropertyList_QuadCustom.Count() > 0)
            {
                QuadCustomProperty p = new QuadCustomProperty(PropertyList_QuadCustom.First());
                var prop = p.GetProperties();
                PopulateMultiSelectObjsList(prop);
            }

            comboBoxPropertyList.Items.Add("");
            comboBoxPropertyList.Items.AddRange(multiSelectObjs.Values.ToList().Cast<object>().ToArray());
            comboBoxPropertyList.SelectedIndex = 0;

            //
            buttonSetValue.Enabled = false;
            groupBoxProgressiveSum.Enabled = false;
            numericUpDownDecimal.Enabled = false;
            checkBoxDecimal.Enabled = false;
            checkBoxHexadecimal.Enabled = false;
            checkBoxHexadecimal.Checked = true;
            textBoxHexadecimal.Enabled = false;
            checkBoxProgressiveSum.Enabled = false;
            checkBoxCurrentValuePlusValueToAdd.Enabled = false;
            groupBoxCurrentValuePlusValueToAdd.Enabled = false;

            ComboBoxPropertyListIsChanged_Enable = true;

            if (Lang.LoadedTranslation)
            {
                StartUpdateTranslation();
            }
        }

        private void PopulateMultiSelectObjsList(PropertyDescriptorCollection collection) 
        {
            foreach (DynamicTypeDescriptor.CustomPropertyDescriptor item in collection)
            {
                if (item.Attributes.Contains(new AllowInMultiSelectAttribute()))
                {
                    string Name = item.Name;
                    string DisplayName = item.DisplayName;
                    string Description = item.Description;
                    Type type = item.PropertyType;
                    int ByteLenght = 0;
                    if (typeof(byte[]) == type)
                    {
                        var itemvalue = item.GetValue(item.m_owner);
                        ByteLenght = ((byte[])itemvalue).Length;       
                    }
                    MultiSelectObj obj = new MultiSelectObj(Name, DisplayName, Description, type, ByteLenght);
                    MultiSelectKey key = new MultiSelectKey(Name, type, ByteLenght);

                    if (!multiSelectObjs.ContainsKey(key))
                    {
                        multiSelectObjs.Add(key, obj);
                    }
                }
               
            }
        }

        /// <summary>
        /// Aqui contem más praticas de programação, necessita de melhorias, mas no geral deve funcionar para novas propertys;
        /// </summary>
        private void buttonSetValue_Click(object sender, EventArgs e)
        {
            if (comboBoxPropertyList.SelectedItem is MultiSelectObj obj)
            {
                object value = null;

                string Cont = null;

                bool Ishex = false;

                bool Sum = false;

                string SumCont = null;

                object Sumvalue = null;

                bool CurrentPlusToAdd = false;

                string ToAddCont = null;

                object ToAddvalue = null;

                if (checkBoxDecimal.Checked)
                {
                    Cont = numericUpDownDecimal.Value.ToString(CultureInfo.InvariantCulture.NumberFormat);
                }
                else if (checkBoxHexadecimal.Checked)
                {
                    Cont = textBoxHexadecimal.Text.ToString();
                    Ishex = true;
                }

                if (checkBoxProgressiveSum.Checked)
                {
                    SumCont = numericUpDownSumValue.Value.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    Sum = true;
                }

                if (checkBoxCurrentValuePlusValueToAdd.Checked)
                {
                    ToAddCont = numericUpDownValueToAdd.Value.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    CurrentPlusToAdd = true;
                }


                if (obj.PropertyType == typeof(byte[]))
                {
                    List<byte> bList = new List<byte>();
                    for (int i = 0; i < Cont.Length; i += 2)
                    {
                        string b = Cont[i].ToString() + Cont[i + 1];
                        bList.Add(byte.Parse(b, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat));
                    }
                    value = bList.ToArray();
                }

                else if (obj.PropertyType == typeof(byte))
                {
                    if (Ishex)
                    {
                        value = byte.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else 
                    {
                        try
                        {
                            value = byte.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message,Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                        
                    }

                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = byte.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                        
                    }

                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = byte.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }
                }

                else if (obj.PropertyType == typeof(sbyte))
                {
                    if (Ishex)
                    {
                        value = sbyte.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {
                        try
                        {
                            value = sbyte.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }

                    }

                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = sbyte.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }

                    }

                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = sbyte.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }
                }

                else if (obj.PropertyType == typeof(short))
                {
                    if (Ishex)
                    {
                        value = short.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {
                        try
                        {
                            value = short.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }

                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = short.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }

                    }

                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = short.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }
                }

                else if (obj.PropertyType == typeof(ushort))
                {
                    if (Ishex)
                    {
                        value = ushort.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {
                        try
                        {
                            value = ushort.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }

                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = ushort.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }

                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = ushort.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }
                }

                else if (obj.PropertyType == typeof(int))
                {
                    if (Ishex)
                    {
                        value = int.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {
                        try
                        {
                            value = int.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }

                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = int.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                       
                    }


                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = int.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                        
                    }
                }

                else if (obj.PropertyType == typeof(uint))
                {
                    if (Ishex)
                    {
                        value = uint.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else
                    {
                        try
                        {
                            value = uint.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                       
                    }


                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = uint.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                       
                    }

                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = uint.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                    }
                }

                else if (obj.PropertyType == typeof(float))
                {
                    if (Ishex)
                    {
                        uint temp = uint.Parse(Cont, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                        byte[] temp2 = BitConverter.GetBytes(temp);
                        value = BitConverter.ToSingle(temp2, 0);
                    }
                    else
                    {
                        try
                        {
                            value = float.Parse(Cont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                      
                    }

                    if (Sum)
                    {
                        try
                        {
                            Sumvalue = float.Parse(SumCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                        
                    }

                    if (CurrentPlusToAdd)
                    {
                        try
                        {
                            ToAddvalue = float.Parse(ToAddCont, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception Ex)
                        {
                            MessageBox.Show(Ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle));
                            return;
                        }
                       
                    }
                }


                // aqui
                if (value != null || CurrentPlusToAdd)
                {
                    foreach (var item in ALL)
                    {
                        var prop = item.GetProperty(obj.PropertyName);
                       
                        if (prop != null && prop.PropertyType == obj.PropertyType)
                        {
                            if (prop.PropertyType == typeof(byte[]))
                            {
                                var itemvalue = item.GetPropertyValue(obj.PropertyName);
                                int ByteLenght = ((byte[])itemvalue).Length;
                                if (ByteLenght != obj.ByteLenght)
                                {
                                    continue;
                                }
                            }

                            if (!CurrentPlusToAdd)
                            {
                                item.SetPropertyValue(obj.PropertyName, value);

                                if (Sum)
                                {
                                    value = SumObject(value, Sumvalue);
                                }
                            }
                            else
                            {
                                object oldValue = item.GetPropertyValue(obj.PropertyName);

                                value = SumObject(oldValue, ToAddvalue);
                                item.SetPropertyValue(obj.PropertyName, value);
                            }
                        }
                    }
                }

                updateMethods.UpdateTreeViewObjs();
                updateMethods.UpdatePropertyGrid();
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            
                MessageBox.Show(Lang.GetText(eLang.MultiSelectEditorFinishMessageBoxDialog), Lang.GetText(eLang.MultiSelectEditorFinishMessageBoxTitle));
            }
        }


        object SumObject(object value, object sum) 
        {
            object res = value;

            if (sum.GetType() == typeof(byte))
            {
                byte v = (byte)value;
                byte s = (byte)sum;
                byte r = v;
                try { r = (byte)(v + s); } catch (Exception) { }
                res = r;
            }

            if (sum.GetType() == typeof(sbyte))
            {
                sbyte v = (sbyte)value;
                sbyte s = (sbyte)sum;
                sbyte r = v;
                try { r = (sbyte)(v + s); } catch (Exception) { }
                res = r;
            }

            else if (sum.GetType() == typeof(short))
            {
                short v = (short)value;
                short s = (short)sum;
                short r = v;
                try { r = (short)(v + s); } catch (Exception) { }
                res = r;
            }

            else if(sum.GetType() == typeof(ushort))
            {
                ushort v = (ushort)value;
                ushort s = (ushort)sum;
                ushort r = v;
                try { r = (ushort)(v + s); } catch (Exception) { }
                res = r;
            }

            else if(sum.GetType() == typeof(int))
            {
                int v = (int)value;
                int s = (int)sum;
                int r = v;
                try { r = (int)(v + s); } catch (Exception) { }
                res = r;
            }

            else if(sum.GetType() == typeof(uint))
            {
                uint v = (uint)value;
                uint s = (uint)sum;
                uint r = v;
                try { r = (uint)(v + s); } catch (Exception) { }
                res = r;
            }

            else if(sum.GetType() == typeof(float))
            {
                float v = (float)value;
                float s = (float)sum;
                float r = v;
                try { r = (float)(v + s); } catch (Exception) { }
                res = r;
            }


            return res;
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void comboBoxPropertyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxPropertyListIsChanged_Enable)
            {
                selectedType = null;

                if (comboBoxPropertyList.SelectedIndex == 0)
                {
                    ByteArryLenght = 0;

                    checkBoxHexadecimalIsChanged_Enable = false;
                    checkBoxDecimalIsChanged_Enable = false;
                    checkBoxProgressiveSumIsChanged_Enable = false;
                    checkBoxCurrentValuePlusValueToAddIsChanged_Enable = false;
                    buttonSetValue.Enabled = false;
                    groupBoxProgressiveSum.Enabled = false;
                    numericUpDownDecimal.Enabled = false;
                    checkBoxDecimal.Enabled = false;
                    checkBoxHexadecimal.Enabled = false;
                    textBoxHexadecimal.Enabled = false;
                    checkBoxProgressiveSum.Enabled = false;
                    checkBoxCurrentValuePlusValueToAdd.Enabled = false;
                    groupBoxCurrentValuePlusValueToAdd.Enabled = false;


                    checkBoxDecimal.Checked = false;
                    checkBoxHexadecimal.Checked = true;
                    checkBoxProgressiveSum.Checked = false;
                    numericUpDownDecimal.Value = 0;
                    numericUpDownSumValue.Value = 0;
                    numericUpDownValueToAdd.Value = 0;
                    textBoxHexadecimal.Text = "";
                    textBoxDescription.Text = "";


                    numericUpDownDecimal.Maximum = 0;
                    numericUpDownDecimal.Minimum = 0;
                    numericUpDownDecimal.DecimalPlaces = 0;

                    numericUpDownSumValue.Maximum = 0;
                    numericUpDownSumValue.Minimum = 0;
                    numericUpDownSumValue.DecimalPlaces = 0;

                    numericUpDownValueToAdd.Maximum = 0;
                    numericUpDownValueToAdd.Minimum = 0;
                    numericUpDownValueToAdd.DecimalPlaces = 0;

                }
                else 
                {
                    ByteArryLenght = 0;

                    checkBoxHexadecimalIsChanged_Enable = false;
                    checkBoxDecimalIsChanged_Enable = false;
                    checkBoxProgressiveSumIsChanged_Enable = false;
                    checkBoxCurrentValuePlusValueToAddIsChanged_Enable = false;
                    buttonSetValue.Enabled = false;
                    groupBoxProgressiveSum.Enabled = false;
                    numericUpDownDecimal.Enabled = false;
                    checkBoxDecimal.Enabled = false;
                    checkBoxHexadecimal.Enabled = false;
                    textBoxHexadecimal.Enabled = false;
                    checkBoxProgressiveSum.Enabled = false;
                    checkBoxCurrentValuePlusValueToAdd.Enabled = false;
                    groupBoxCurrentValuePlusValueToAdd.Enabled = false;

                    if (comboBoxPropertyList.SelectedItem is MultiSelectObj obj)
                    {
                        selectedType = obj.PropertyType;

                        textBoxDescription.Text = obj.PropertyDescription;
                        buttonSetValue.Enabled = true;

                        if (obj.PropertyType == typeof(byte[]))
                        {
                            textBoxHexadecimal.Enabled = true;

                            checkBoxProgressiveSum.Checked = false;
                            checkBoxDecimal.Checked = false;
                            checkBoxCurrentValuePlusValueToAdd.Checked = false;
                            checkBoxHexadecimal.Checked = true;
                            checkBoxHexadecimal.Enabled = true;

                            ByteArryLenght = obj.ByteLenght;
                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = 0;
                            numericUpDownDecimal.Minimum = 0;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = 0;
                            numericUpDownSumValue.Minimum = 0;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = 0;
                            numericUpDownValueToAdd.Minimum = 0;
                            numericUpDownValueToAdd.DecimalPlaces = 0;

                        }

                       else if (obj.PropertyType == typeof(byte))
                        {
                            ByteArryLenght = 1;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = byte.MaxValue;
                            numericUpDownDecimal.Minimum = byte.MinValue;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = byte.MaxValue;
                            numericUpDownSumValue.Minimum = byte.MinValue;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = byte.MaxValue;
                            numericUpDownValueToAdd.Minimum = byte.MinValue;
                            numericUpDownValueToAdd.DecimalPlaces = 0;
                        }

                        else if (obj.PropertyType == typeof(sbyte))
                        {
                            ByteArryLenght = 1;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = sbyte.MaxValue;
                            numericUpDownDecimal.Minimum = sbyte.MinValue;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = sbyte.MaxValue;
                            numericUpDownSumValue.Minimum = sbyte.MinValue;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = sbyte.MaxValue;
                            numericUpDownValueToAdd.Minimum = sbyte.MinValue;
                            numericUpDownValueToAdd.DecimalPlaces = 0;
                        }


                        else if (obj.PropertyType == typeof(ushort))
                        {
                            ByteArryLenght = 2;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = ushort.MaxValue;
                            numericUpDownDecimal.Minimum = ushort.MinValue;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = ushort.MaxValue;
                            numericUpDownSumValue.Minimum = ushort.MinValue;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = ushort.MaxValue;
                            numericUpDownValueToAdd.Minimum = ushort.MinValue;
                            numericUpDownValueToAdd.DecimalPlaces = 0;
                        }

                        else if (obj.PropertyType == typeof(short))
                        {
                            ByteArryLenght = 2;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = short.MaxValue;
                            numericUpDownDecimal.Minimum = short.MinValue;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = short.MaxValue;
                            numericUpDownSumValue.Minimum = short.MinValue;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = short.MaxValue;
                            numericUpDownValueToAdd.Minimum = short.MinValue;
                            numericUpDownValueToAdd.DecimalPlaces = 0;
                        }

                        else if (obj.PropertyType == typeof(int))
                        {
                            ByteArryLenght = 4;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = int.MaxValue;
                            numericUpDownDecimal.Minimum = int.MinValue;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = int.MaxValue;
                            numericUpDownSumValue.Minimum = int.MinValue;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = int.MaxValue;
                            numericUpDownValueToAdd.Minimum = int.MinValue;
                            numericUpDownValueToAdd.DecimalPlaces = 0;
                        }

                        else if (obj.PropertyType == typeof(uint))
                        {
                            ByteArryLenght = 4;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = uint.MaxValue;
                            numericUpDownDecimal.Minimum = uint.MinValue;
                            numericUpDownDecimal.DecimalPlaces = 0;

                            numericUpDownSumValue.Maximum = uint.MaxValue;
                            numericUpDownSumValue.Minimum = uint.MinValue;
                            numericUpDownSumValue.DecimalPlaces = 0;

                            numericUpDownValueToAdd.Maximum = uint.MaxValue;
                            numericUpDownValueToAdd.Minimum = uint.MinValue;
                            numericUpDownValueToAdd.DecimalPlaces = 0;
                        }

                        else if (obj.PropertyType == typeof(float))
                        {
                            ByteArryLenght = 4;
                            checkBoxProgressiveSum.Enabled = true;
                            checkBoxDecimal.Enabled = true;
                            checkBoxHexadecimal.Enabled = true;
                            checkBoxCurrentValuePlusValueToAdd.Enabled = true;

                            textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                            if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                            {
                                textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                            }

                            numericUpDownDecimal.Maximum = (decimal)Consts.MyFloatMax;
                            numericUpDownDecimal.Minimum = (decimal) -Consts.MyFloatMax;
                            numericUpDownDecimal.DecimalPlaces = 9;

                            numericUpDownSumValue.Maximum = (decimal)Consts.MyFloatMax;
                            numericUpDownSumValue.Minimum = (decimal)-Consts.MyFloatMax;
                            numericUpDownSumValue.DecimalPlaces = 9;

                            numericUpDownValueToAdd.Maximum = (decimal)Consts.MyFloatMax;
                            numericUpDownValueToAdd.Minimum = (decimal)-Consts.MyFloatMax;
                            numericUpDownValueToAdd.DecimalPlaces = 9;
                        }

                        numericUpDownDecimal.Enabled = checkBoxDecimal.Checked;
                        textBoxHexadecimal.Enabled = checkBoxHexadecimal.Checked;

                        groupBoxProgressiveSum.Enabled = checkBoxProgressiveSum.Checked;

                        groupBoxCurrentValuePlusValueToAdd.Enabled = checkBoxCurrentValuePlusValueToAdd.Checked;

                        if (checkBoxCurrentValuePlusValueToAdd.Checked)
                        {
                            numericUpDownDecimal.Enabled = false;
                            textBoxHexadecimal.Enabled = false;
                            groupBoxProgressiveSum.Enabled = false;

                            checkBoxDecimal.Enabled = false;
                            checkBoxHexadecimal.Enabled = false;
                            checkBoxProgressiveSum.Enabled = false;
                        }

                        if (numericUpDownDecimal.Value > numericUpDownDecimal.Maximum)
                        {
                            numericUpDownDecimal.Value = numericUpDownDecimal.Maximum;
                        }
                        else if (numericUpDownDecimal.Value < numericUpDownDecimal.Minimum)
                        {
                            numericUpDownDecimal.Value = numericUpDownDecimal.Minimum;
                        }

                        checkBoxHexadecimalIsChanged_Enable = true;
                        checkBoxDecimalIsChanged_Enable = true;
                        checkBoxProgressiveSumIsChanged_Enable = true;
                        checkBoxCurrentValuePlusValueToAddIsChanged_Enable = true;

                    }
                }
            }
        }

        private void checkBoxHexadecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHexadecimalIsChanged_Enable)
            {
                checkBoxHexadecimalIsChanged_Enable = false;
                checkBoxHexadecimal.Checked = true;
                checkBoxHexadecimalIsChanged_Enable = true;
                checkBoxDecimalIsChanged_Enable = false;
                checkBoxDecimal.Checked = false;
                checkBoxDecimalIsChanged_Enable = true;

                numericUpDownDecimal.Enabled = false;
                textBoxHexadecimal.Enabled = true;
            }
        }

        private void checkBoxDecimal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDecimalIsChanged_Enable)
            {
                checkBoxDecimalIsChanged_Enable = false;
                checkBoxDecimal.Checked = true;
                checkBoxDecimalIsChanged_Enable = true;
                checkBoxHexadecimalIsChanged_Enable = false;
                checkBoxHexadecimal.Checked = false;
                checkBoxHexadecimalIsChanged_Enable = true;

                numericUpDownDecimal.Enabled = true;
                textBoxHexadecimal.Enabled = false;
            }
        }

        private void checkBoxProgressiveSum_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxProgressiveSumIsChanged_Enable)
            {
                groupBoxProgressiveSum.Enabled = checkBoxProgressiveSum.Checked;
            }
        }

        private void textBoxHexadecimal_TextChanged(object sender, EventArgs e)
        {
            string newtext = "";
            for (int i = 0; i < textBoxHexadecimal.Text.Length; i++)
            {
                char cc = textBoxHexadecimal.Text[i];

                if (char.IsDigit(cc)
                  || cc == 'A'
                  || cc == 'B'
                  || cc == 'C'
                  || cc == 'D'
                  || cc == 'E'
                  || cc == 'F'
                  || cc == 'a'
                  || cc == 'b'
                  || cc == 'c'
                  || cc == 'd'
                  || cc == 'e'
                  || cc == 'f'
                  )
                {
                    newtext += cc;
                }
            }

            textBoxHexadecimal.Text = newtext.PadRight(ByteArryLenght * 2, '0');

            if (textBoxHexadecimal.Text.Length != ByteArryLenght * 2 && ByteArryLenght != 0)
            {
                textBoxHexadecimal.Text = textBoxHexadecimal.Text.PadRight(ByteArryLenght * 2, '0');
                if (textBoxHexadecimal.Text.Length > ByteArryLenght * 2)
                {
                    textBoxHexadecimal.Text = textBoxHexadecimal.Text.Substring(0, ByteArryLenght * 2);
                }
            }

            if (checkBoxHexadecimal.Checked && checkBoxDecimal.Checked == false && checkBoxDecimal.Enabled)
            {
                try
                {
                    if (selectedType == typeof(byte))
                    {
                        numericUpDownDecimal.Value = byte.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else if (selectedType == typeof(sbyte))
                    {
                        numericUpDownDecimal.Value = sbyte.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else if (selectedType == typeof(ushort))
                    {
                        numericUpDownDecimal.Value = ushort.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else if (selectedType == typeof(short))
                    {
                        numericUpDownDecimal.Value = short.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else if (selectedType == typeof(int))
                    {
                        numericUpDownDecimal.Value = int.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else if (selectedType == typeof(uint))
                    {
                        numericUpDownDecimal.Value = uint.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    else if (selectedType == typeof(float))
                    {
                        uint temp = uint.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                        byte[] temp2 = BitConverter.GetBytes(temp);
                        numericUpDownDecimal.Value = (decimal)BitConverter.ToSingle(temp2, 0);
                    }
                }
                catch (Exception)
                {
                }
            }

        }
        private void numericUpDownDecimal_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxDecimal.Checked && checkBoxHexadecimal.Checked == false && checkBoxHexadecimal.Enabled)
            {
                try
                {

                    if (selectedType == typeof(byte))
                    {
                        byte v = (byte)numericUpDownDecimal.Value;
                        textBoxHexadecimal.Text = v.ToString("X2");
                    }
                    else if (selectedType == typeof(sbyte))
                    {
                        sbyte v = (sbyte)numericUpDownDecimal.Value;
                        textBoxHexadecimal.Text = v.ToString("X2");
                    }
                    else if (selectedType == typeof(ushort))
                    {
                        ushort v = (ushort)numericUpDownDecimal.Value;
                        textBoxHexadecimal.Text = v.ToString("X4");
                    }
                    else if (selectedType == typeof(short))
                    {
                        short v = (short)numericUpDownDecimal.Value;
                        textBoxHexadecimal.Text = v.ToString("X4");
                    }
                    else if (selectedType == typeof(uint))
                    {
                        uint v = (uint)numericUpDownDecimal.Value;
                        textBoxHexadecimal.Text = v.ToString("X8");
                    }
                    else if (selectedType == typeof(int))
                    {
                        int v = (int)numericUpDownDecimal.Value;
                        textBoxHexadecimal.Text = v.ToString("X8");
                    }
                    else if (selectedType == typeof(float))
                    {
                        float v = (float)numericUpDownDecimal.Value;
                        byte[] b = BitConverter.GetBytes(v);
                        b = b.Reverse().ToArray();
                        textBoxHexadecimal.Text = BitConverter.ToString(b).Replace("-", "");
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void textBoxHexadecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)
               || e.KeyChar == 'A'
               || e.KeyChar == 'B'
               || e.KeyChar == 'C'
               || e.KeyChar == 'D'
               || e.KeyChar == 'E'
               || e.KeyChar == 'F'
               || e.KeyChar == 'a'
               || e.KeyChar == 'b'
               || e.KeyChar == 'c'
               || e.KeyChar == 'd'
               || e.KeyChar == 'e'
               || e.KeyChar == 'f'
               )
            {
                if (textBoxHexadecimal.SelectionStart < textBoxHexadecimal.TextLength)
                {
                    int CacheSelectionStart = textBoxHexadecimal.SelectionStart;
                    StringBuilder sb = new StringBuilder(textBoxHexadecimal.Text);
                    sb[textBoxHexadecimal.SelectionStart] = e.KeyChar;
                    textBoxHexadecimal.Text = sb.ToString();
                    textBoxHexadecimal.SelectionStart = CacheSelectionStart + 1;
                }
            }

            e.Handled = true;

            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
        }

        private void HexadecimalAndDecimal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && ByteArryLenght == 2)
            {
                List<object> list = new List<object>();
                list.AddRange(ListBoxProperty.EnemiesList.Values);
                list.AddRange(ListBoxProperty.EtcmodelsList.Values);
                list.AddRange(ListBoxProperty.ItemsList.Values);
                list.AddRange(ListBoxProperty.ItemAuraTypeList.Values);

                ushort value = 0;

                try
                {
                    value = ushort.Parse(textBoxHexadecimal.Text, NumberStyles.HexNumber);
                }
                catch (Exception)
                {
                }

                SearchForm search = new SearchForm(list.ToArray(), new UshortObjForListBox(value, ""));
                search.Search += setHexadecimalAndDecimalValue;
                search.ShowDialog();
            }

            if (e.Alt) //e.Shift || 
            {
                e.Handled = true;
            }
        }

        void setHexadecimalAndDecimalValue(object obj) 
        {
            if (obj is UshortObjForListBox box)
            {
                textBoxHexadecimal.Text = box.ID.ToString("X4");

                if (box.ID >= numericUpDownDecimal.Minimum && box.ID <= numericUpDownDecimal.Maximum)
                {
                    numericUpDownDecimal.Value = box.ID;
                }
            }
        }


        private void MultiSelectEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void checkBoxCurrentValuePlusValueToAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCurrentValuePlusValueToAddIsChanged_Enable)
            {
                bool status = checkBoxCurrentValuePlusValueToAdd.Checked;
                groupBoxCurrentValuePlusValueToAdd.Enabled = status;

                checkBoxDecimal.Enabled = !status;
                checkBoxHexadecimal.Enabled = !status;
                checkBoxProgressiveSum.Enabled = !status;

                groupBoxProgressiveSum.Enabled = !status;
                numericUpDownDecimal.Enabled = !status;
                textBoxHexadecimal.Enabled = !status;

                if (status == false)
                {
                    groupBoxProgressiveSum.Enabled = checkBoxProgressiveSum.Checked;
                    numericUpDownDecimal.Enabled = checkBoxDecimal.Checked;
                    textBoxHexadecimal.Enabled = checkBoxHexadecimal.Checked;
                }
            }
        }

        // byte, byte[], short, ushot, uint, float, sbyte

        // /// /// /// / // // // / // / / // / /


        private void StartUpdateTranslation() 
        {
            this.Text = Lang.GetText(eLang.MultiSelectEditor);
            labelValueSumText2.Text = Lang.GetText(eLang.labelValueSumText2);
            labelValueSumText.Text = Lang.GetText(eLang.labelValueSumText);
            labelPropertyDescriptionText.Text = Lang.GetText(eLang.labelPropertyDescriptionText);
            labelChoiseText.Text = Lang.GetText(eLang.labelChoiseText);
            checkBoxProgressiveSum.Text = Lang.GetText(eLang.checkBoxProgressiveSum);
            checkBoxHexadecimal.Text = Lang.GetText(eLang.checkBoxHexadecimal);
            checkBoxDecimal.Text = Lang.GetText(eLang.checkBoxDecimal);
            checkBoxCurrentValuePlusValueToAdd.Text = Lang.GetText(eLang.checkBoxCurrentValuePlusValueToAdd);
            buttonSetValue.Text = Lang.GetText(eLang.buttonSetValue);
            buttonClose.Text = Lang.GetText(eLang.buttonClose);
        }

    }
}
