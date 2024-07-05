using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Re4QuadExtremeEditor.src.Forms;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor
{
    public class MultiSelectUITypeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //MultiSelectObjInfo
            if (value is MultiSelectObjInfoToProperty obj)
            {
                MultiSelectEditorForm editor = new MultiSelectEditorForm(ref obj);
                editor.ShowDialog();
            }

            return base.EditValue(context, provider, value);
        }


        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}