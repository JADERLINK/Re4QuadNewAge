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
    public class QuadCustomNodeGroup : TreeNodeGroup, INodeChangeAmount
    {
        public QuadCustomNodeGroup() : base() { }
        public QuadCustomNodeGroup(string text) : base(text) { }
        public QuadCustomNodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public QuadCustomMethods PropertyMethods { get; set; }

        public QuadCustomMethodsForGL MethodsForGL { get; set; }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }
    }
}