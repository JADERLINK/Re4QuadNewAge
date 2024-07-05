using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using OpenTK;

namespace ViewerBase
{
    public class SmxEntry // referente smx entry
    {
        public int SMX_ID { get; set; }
        public int OpacityHierarchy { get; set; }
        public int AlphaHierarchy { get; set; }
        public Vector4 SmxColor { get; set; }

        public SmxFaceCulling FaceCulling { get; set; }

        public SmxEntry() 
        {
            SMX_ID = -1;
            OpacityHierarchy = 0;
            AlphaHierarchy = 0;
            SmxColor = Vector4.One;
            FaceCulling = SmxFaceCulling.OnlyFront;
        }
    }

    public enum SmxFaceCulling 
    {
        OnlyFront = 0,
        OnlyBack = 1,
        FrontAndBack = 2
    }

    public class SmxGroup
    {
        public string SmxName { get; private set; }
        public Dictionary<int, SmxEntry> SmxEntries { get; set; }

        public SmxGroup(string SmdName)
        {
            this.SmxName = SmdName;
            SmxEntries = new Dictionary<int, SmxEntry>();
        }
    }
}
