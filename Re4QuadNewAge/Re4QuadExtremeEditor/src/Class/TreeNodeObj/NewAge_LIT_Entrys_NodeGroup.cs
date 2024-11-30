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
    public class NewAge_LIT_Entrys_NodeGroup : TreeNodeGroup, INodeChangeAmount, IChangeAmountIndexFix
    {
        public NewAge_LIT_Entrys_NodeGroup() : base() { }
        public NewAge_LIT_Entrys_NodeGroup(string text) : base(text) { }
        public NewAge_LIT_Entrys_NodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }

        public NewAge_LIT_Entry_Methods PropertyMethods { get; set; }

        public NewAge_LIT_Entry_MethodsForGL MethodsForGL { get; set; }

        private NodeChangeAmountCallbackMethods _ChangeAmountCallbackMethods = null;

        public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods
        { get
            {
                if (_ChangeAmountCallbackMethods == null)
                {
                    _ChangeAmountCallbackMethods = new NodeChangeAmountCallbackMethods() { OnDeleteNode = OnDeleteNode, OnMoveNode = OnMoveNode };
                }
                return _ChangeAmountCallbackMethods;
            }
        }

        public void OnDeleteNode()
        {
            OnNodeChange();
        }

        public void OnMoveNode()
        {
            OnNodeChange();
        }

        private void OnNodeChange()
        {
            if (PropertyMethods != null)
            {
                // GroupID, cont
                Dictionary<ushort, int> CountGroups = new Dictionary<ushort, int>();

                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (Nodes[i] is Object3D obj)
                    {
                        ushort ObjLineRef = obj.ObjLineRef;
                        ushort Group = PropertyMethods.GetGroupOrderID(ObjLineRef);

                        if (CountGroups.ContainsKey(Group))
                        {
                            int newValue = CountGroups[Group];
                            CountGroups[Group]++;
                            PropertyMethods.SetEntryOrderID(ObjLineRef, (ushort)newValue);
                        }
                        else
                        {
                            CountGroups.Add(Group, 1);
                            PropertyMethods.SetEntryOrderID(ObjLineRef, 0);
                        }
                    }
                }
            }
        }

    }
}
