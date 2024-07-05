using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.JSON
{
    public class LangObjForList
    {
        public string LangName { get; }
        public string LangJsonFileName { get; }

        public LangObjForList(string LangName, string LangJsonFileName) 
        {
            this.LangJsonFileName = LangJsonFileName;
            this.LangName = LangName;
        }

        public override bool Equals(object obj)
        {
            return obj is LangObjForList lang && lang.LangJsonFileName == LangJsonFileName;
        }

        public override int GetHashCode()
        {
            return LangJsonFileName.GetHashCode();
        }

        public override string ToString()
        {
            return LangName + " {" + LangJsonFileName + "}";
        }
    }
}
