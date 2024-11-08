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
    public class NewAge_LIT_Groups_NodeGroup : TreeNodeGroup, INodeChangeAmount
    {
        public NewAge_LIT_Groups_NodeGroup() : base() { }
        public NewAge_LIT_Groups_NodeGroup(string text) : base(text) { }
        public NewAge_LIT_Groups_NodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }

        public NewAge_LIT_Group_Methods PropertyMethods { get; set; }

        public NewAge_LIT_Group_MethodsForGL MethodsForGL { get; set; }
    }
}