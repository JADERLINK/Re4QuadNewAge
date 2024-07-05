using System;
using System.Collections.Generic;
using System.Text;
using ViewerBase;
using System.Windows.Forms;

namespace JADERLINK_MODEL_VIEWER.src.Nodes
{
    public class ModelNodeLinker
    {
        private ModelGroup modelGroup;
        private ModelNodeGroup mng;

        public ModelNodeLinker(ModelGroup modelGroup, ModelNodeGroup mng)
        {
            this.modelGroup = modelGroup;
            this.mng = mng;
        }


        public void GetNodeLinker()
        {
            Dictionary<string, List<string>> invDic = new Dictionary<string, List<string>>();

            List<string> temp = new List<string>();

            foreach (NodeItem item in mng.Nodes.List)
            {
                if (item is NodeModel i)
                {
                    temp.Add(i.FileID);
                }
                else if (item is NodeMatRef j) 
                {
                    invDic.Add(j.FileID, temp);
                    temp = new List<string>();
                }
            }

            foreach (var item in invDic)
            {
                foreach (var imodel in item.Value)
                {
                    modelGroup.MatLinkerDic[imodel].MatTexGroupName = item.Key;
                }
            }
        }

    }
}
