using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.Interfaces;

namespace Re4QuadExtremeEditor.src.Class.TreeNodeObj
{
    public class ExtraNodeGroup : TreeNodeGroup
    {
        public ExtraNodeGroup() : base() { }
        public ExtraNodeGroup(string text) : base(text) { }
        public ExtraNodeGroup(string text, TreeNode[] children) : base(text, children) { }
    }
}
