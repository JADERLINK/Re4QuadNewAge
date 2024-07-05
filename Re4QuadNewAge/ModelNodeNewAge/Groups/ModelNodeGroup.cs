using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public class ModelNodeGroup : TreeNodeGroup
    {
        public ModelNodeGroup() : base() { }
        public ModelNodeGroup(string text) : base(text) { }

        public override GroupType GetGroup()
        {
            return GroupType.Model;
        }
    }
}
