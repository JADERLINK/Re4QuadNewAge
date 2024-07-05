using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsMultiselectTreeView
{
    public interface IFastNode
    {
        int GetHashCode();
        bool Equals(object obj);
        int HashCodeID { get; }
    }
}
