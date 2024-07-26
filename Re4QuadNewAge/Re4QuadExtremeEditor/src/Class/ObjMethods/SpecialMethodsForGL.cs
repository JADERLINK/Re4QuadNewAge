using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public class SpecialMethodsForGL : BaseTriggerZoneMethodsForGL
    {
        public ReturnSpecialType GetSpecialType;
        
        public ReturnVector3 GetItemPosition;

        public ReturnMatrix4 GetItemRotation;

        public ReturnUshort GetItemModelID;

        public ReturnFloat GetItemTrigggerRadius;
    }
}
