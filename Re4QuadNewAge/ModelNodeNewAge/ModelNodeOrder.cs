using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public class ModelNodeOrder
    {
        private ModelNodeGroup mng;
        public List<string> NodeOrder { get; private set; }

        public ModelNodeOrder(ModelNodeGroup mng)
        {
            this.mng = mng;
            NodeOrder = new List<string>();
        }

        public void GetNodeOrder() 
        {
            NodeOrder.Clear();
            foreach (NodeItem item in mng.Nodes.List)
            {
                if (item is NodeModel i)
                {
                    if (i.ModelIsEnable)
                    {
                        NodeOrder.Add(i.FileID);
                    }
                }
            }
          
        }

    }
}
