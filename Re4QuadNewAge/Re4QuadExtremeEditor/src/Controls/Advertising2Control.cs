using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re4QuadExtremeEditor.src.Controls
{
    public partial class Advertising2Control : UserControl
    {
        public Advertising2Control()
        {
            InitializeComponent();
        }

        public void SetDarkerGrayTheme()
        {
            labelYoutube.ForeColor = Color.DarkGray;
        }
    }
}
