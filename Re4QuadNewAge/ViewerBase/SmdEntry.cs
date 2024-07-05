using System;
using System.Collections.Generic;
using System.Text;

namespace ViewerBase
{
    public class SmdEntry // referente smd entry
    {
        public int SMD_ID { get; set; }
        public int SMX_ID { get; set; }
        public PreFix Fix { get; set; }
        public int BIN_ID { get; set; }
    }

    public class SmdGroup
    {
        public string SmdName { get; private set; }
        public Dictionary<int, SmdEntry> SmdEntries { get; set; }

        public SmdGroup(string SmdName) 
        {
            this.SmdName = SmdName;
            SmdEntries = new Dictionary<int, SmdEntry>();
        }
    }

}
