using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;

namespace Re4QuadExtremeEditor.src.Class.TreeNodeObj
{
    public abstract class TreeNodeGroup : TreeNode
    {
        public TreeNodeGroup() : base() { }
        public TreeNodeGroup(string text) : base(text) { }
        public TreeNodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public GroupType Group { get; set; }

        public NodeDisplayMethods DisplayMethods { get; set; }

        public NodeMoveMethods MoveMethods { get; set; }

    }
}
