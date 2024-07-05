using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ViewerBase;

namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public abstract class NodeItem
    {
        public NodeItem() : base() { }
        public NodeItem(string text) { }

        public ResponsibilityContainer Responsibility;

        public string Name { get; set; } = "";

        public string Text { get; set; } = "";

        public TreeNodeGroup Parent { get; set; } = null;

        public void Remove() 
        {
            if (Parent != null)
            {
                Parent.Nodes.Remove(this);
            }
        }
    }
}
