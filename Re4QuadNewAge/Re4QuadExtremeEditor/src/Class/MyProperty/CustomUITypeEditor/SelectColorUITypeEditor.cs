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
    public class SelectColorUITypeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object copy = value;
            if (copy is byte[] obj && obj.Length == 3)
            {
                ColorDialog colorDialogRGB = new ColorDialog();
                colorDialogRGB.AnyColor = true;
                colorDialogRGB.FullOpen = true;
                colorDialogRGB.SolidColorOnly = true;
                colorDialogRGB.Color = Color.FromArgb(255, obj[0], obj[1], obj[2]);
                colorDialogRGB.ShowDialog();
                byte[] rgb = new byte[3];
                rgb[0] = colorDialogRGB.Color.R;
                rgb[1] = colorDialogRGB.Color.G;
                rgb[2] = colorDialogRGB.Color.B;
                copy = rgb;
            }
            return copy; //base.EditValue(context, provider, value);
        }


        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}