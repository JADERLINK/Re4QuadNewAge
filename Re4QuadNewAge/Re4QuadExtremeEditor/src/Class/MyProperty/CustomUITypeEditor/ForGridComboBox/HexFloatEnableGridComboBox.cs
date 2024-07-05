using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor
{
    public class HexFloatEnableGridComboBox : GridComboBox
    {
        protected override void RetrieveDataList(ITypeDescriptorContext context)
        {
            DataList = ListBoxProperty.FloatTypeList.Values.ToArray();
        }

        protected override object GetDataObjectSelected(ITypeDescriptorContext context)
        {
            return ListBox.SelectedItem;
        }

        protected override void onStart()
        {
            ListBox.HorizontalScrollbar = false;
        }

    }
}
