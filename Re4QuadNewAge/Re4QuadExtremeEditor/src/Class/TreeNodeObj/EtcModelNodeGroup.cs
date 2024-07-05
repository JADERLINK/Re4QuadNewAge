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
    public class EtcModelNodeGroup : TreeNodeGroup, INodeChangeAmount
    {
        public EtcModelNodeGroup() : base() { }
        public EtcModelNodeGroup(string text) : base(text) { }
        public EtcModelNodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public EtcModelMethods PropertyMethods { get; set; }

        public EtcModelMethodsForGL MethodsForGL { get; set; }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }
    }
}
