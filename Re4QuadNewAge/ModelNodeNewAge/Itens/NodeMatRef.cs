using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace JADERLINK_MODEL_VIEWER.src.Nodes
{

    public class NodeMatRef : NodeItem
    {
        public NodeMatRef() : base() { }
        public NodeMatRef(string text) : base(text) { }

        public string FileID { get; private set; } = null;

        public void SetFileID(string FileID)
        {
            this.FileID = FileID;
        }

    }
}
