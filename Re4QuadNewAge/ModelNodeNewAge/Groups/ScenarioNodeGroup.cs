using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public class ScenarioNodeGroup : TreeNodeGroup
    {
        public ScenarioNodeGroup() : base() { }
        public ScenarioNodeGroup(string text) : base(text) { }

        public override GroupType GetGroup()
        {
            return GroupType.Scenario;
        }
    }
}
