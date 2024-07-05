using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public abstract class TreeNodeGroup
    {
        public TreeNodeGroup() : base() { Nodes = new MyCollection(this); }
        public TreeNodeGroup(string text) { Nodes = new MyCollection(this); }
        public abstract GroupType GetGroup();

        public MyCollection Nodes { get; protected set; }

        public class MyCollection 
        {
            private TreeNodeGroup thisGroup = null;

            public List<NodeItem> List { get; protected set; }

            public MyCollection(TreeNodeGroup thisGroup) 
            {
                List = new List<NodeItem>();
                this.thisGroup = thisGroup;
            }

            public void Add(NodeItem item) 
            {
                List.Add(item);
                item.Parent = thisGroup;
            }

            public NodeItem[] Find(string key, bool searchAllChildren) 
            {
                return List.FindAll(x =>x.Name == key).ToArray();
            }

            public void Remove(NodeItem item) 
            {
                List.Remove(item);
            }
        }
    }

}
