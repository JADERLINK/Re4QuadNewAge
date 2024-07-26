using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public class QuadCustomMethodsForGL : BaseTriggerZoneMethodsForGL
    {
        public ReturnVector4 GetCustomColor;

        public ReturnVector3 GetPosition;

        public ReturnVector3 GetScale;

        public ReturnMatrix4 GetAngle;

        public ReturnUint GetPointModelID;

        public ReturnQuadCustomPointStatus GetQuadCustomPointStatus;
    }
}
