using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Class.Interfaces
{
   public interface IInternalID
    {
        ushort GetInternalID();

        GroupType GetGroupType();

    }
}
