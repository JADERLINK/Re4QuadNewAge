using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public class TexturePackNodeGroup : TreeNodeGroup
    {
        public TexturePackNodeGroup() : base() { }
        public TexturePackNodeGroup(string text) : base(text) { }

        public override GroupType GetGroup()
        {
            return GroupType.TexturePack;
        }
    }
}
