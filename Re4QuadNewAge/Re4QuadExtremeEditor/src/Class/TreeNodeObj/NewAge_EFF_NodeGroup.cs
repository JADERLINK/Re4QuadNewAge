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
    public class NewAge_EFF_NodeGroup : TreeNodeGroup, INodeChangeAmount, IChangeAmountIndexFix
    {
        public NewAge_EFF_NodeGroup() : base() { }
        public NewAge_EFF_NodeGroup(string text) : base(text) { }
        public NewAge_EFF_NodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public NewAge_EFF_Methods PropertyMethods { get; set; }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }

        private NodeChangeAmountCallbackMethods _ChangeAmountCallbackMethods = null;
        public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods
        {
            get
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
                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (Nodes[i] is Object3D obj)
                    {
                        ushort ObjLineRef = obj.ObjLineRef;
                        PropertyMethods.SetEntryOrderID(ObjLineRef, (ushort)i);
                    }
                }
            }
        }
    }

    public class NewAge_EFF_EffectGroup_NodeGroup : TreeNodeGroup, INodeChangeAmount, IChangeAmountIndexFix
    {
        public NewAge_EFF_EffectGroup_NodeGroup() : base() { }
        public NewAge_EFF_EffectGroup_NodeGroup(string text) : base(text) { }
        public NewAge_EFF_EffectGroup_NodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public NewAge_EFF_EffectGroup_Methods PropertyMethods { get; set; }

        public NewAge_EFF_EffectGroup_Methods_MethodsForGL MethodsForGL { get; set; }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }

        private NodeChangeAmountCallbackMethods _ChangeAmountCallbackMethods = null;
        public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods
        {
            get
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
                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (Nodes[i] is Object3D obj)
                    {
                        ushort ObjLineRef = obj.ObjLineRef;
                        PropertyMethods.SetEntryOrderID(ObjLineRef, (ushort)i);
                    }
                }
            }
        }
    }

    public class NewAge_EFF_EffectEntry_NodeGroup : TreeNodeGroup, INodeChangeAmount, IChangeAmountIndexFix
    {
        public NewAge_EFF_EffectEntry_NodeGroup() : base() { }
        public NewAge_EFF_EffectEntry_NodeGroup(string text) : base(text) { }
        public NewAge_EFF_EffectEntry_NodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public NewAge_EFF_EffectEntry_Methods PropertyMethods { get; set; }

        public NewAge_EFF_EffectEntry_Methods_MethodsForGL MethodsForGL { get; set; }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }

        private NodeChangeAmountCallbackMethods _ChangeAmountCallbackMethods = null;
        public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods
        {
            get
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
                Dictionary<(byte table, ushort group), int> CountGroups = new Dictionary<(byte table, ushort group), int>();

                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (Nodes[i] is Object3D obj)
                    {
                        ushort ObjLineRef = obj.ObjLineRef;
                        ushort Group = PropertyMethods.GetGroupOrderID(ObjLineRef);
                        byte Table = PropertyMethods.GetTableID(ObjLineRef);

                        if (CountGroups.ContainsKey((Table,Group)))
                        {
                            int newValue = CountGroups[(Table, Group)];
                            CountGroups[(Table, Group)]++;
                            PropertyMethods.SetEntryOrderID(ObjLineRef, (ushort)newValue);
                        }
                        else
                        {
                            CountGroups.Add((Table, Group), 1);
                            PropertyMethods.SetEntryOrderID(ObjLineRef, 0);
                        }
                    }
                }
            }
        }
    }

    public class NewAge_EFF_Table9Entry_NodeGroup : TreeNodeGroup, INodeChangeAmount, IChangeAmountIndexFix
    {
        public NewAge_EFF_Table9Entry_NodeGroup() : base() { }
        public NewAge_EFF_Table9Entry_NodeGroup(string text) : base(text) { }
        public NewAge_EFF_Table9Entry_NodeGroup(string text, TreeNode[] children) : base(text, children) { }

        public NewAge_EFF_Table9Entry_Methods PropertyMethods { get; set; }

        public NewAge_EFF_Table9Entry_Methods_MethodsForGL MethodsForGL { get; set; }

        public NodeChangeAmountMethods ChangeAmountMethods { get; set; }

        private NodeChangeAmountCallbackMethods _ChangeAmountCallbackMethods = null;
        public NodeChangeAmountCallbackMethods ChangeAmountCallbackMethods
        {
            get
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
