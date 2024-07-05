using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor
{
    public class ItemIDGridComboBox : GridComboBox
    {
        protected override void RetrieveDataList(ITypeDescriptorContext context)
        {
            DataList = ListBoxProperty.ItemsList.Values.ToArray();
        }

        protected override object GetDataObjectSelected(ITypeDescriptorContext context)
        {
            return ListBox.SelectedItem;
        }

        protected override void onStart()
        {
        }
    }
}
