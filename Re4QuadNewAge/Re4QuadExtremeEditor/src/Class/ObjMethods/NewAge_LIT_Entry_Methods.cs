using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public class NewAge_LIT_Entry_Methods : BaseMethods
    {
        public ReturnRe4Version ReturnRe4Version;

        public ReturnByteArray ReturnLine;
        public SetByteArray SetLine;

        public ReturnFloat ReturnPositionX;
        public SetFloat SetPositionX;

        public ReturnFloat ReturnPositionY;
        public SetFloat SetPositionY;

        public ReturnFloat ReturnPositionZ;
        public SetFloat SetPositionZ;

        public ReturnUint ReturnPositionX_Hex;
        public SetUint SetPositionX_Hex;

        public ReturnUint ReturnPositionY_Hex;
        public SetUint SetPositionY_Hex;

        public ReturnUint ReturnPositionZ_Hex;
        public SetUint SetPositionZ_Hex;

        public ReturnFloat ReturnRangeRadius;
        public SetFloat SetRangeRadius;

        public ReturnUint ReturnRangeRadius_Hex;
        public SetUint SetRangeRadius_Hex;

        public ReturnFloat ReturnIntensity;
        public SetFloat SetIntensity;

        public ReturnUint ReturnIntensity_Hex;
        public SetUint SetIntensity_Hex;

        public ReturnByteArray ReturnColorRGB;
        public SetByteArray SetColorRGB;

        public ReturnByte ReturnColorAlfa;
        public SetByte SetColorAlfa;

        public SetUshort SetEntryOrderID;
        public ReturnUshort GetEntryOrderID;

        public SetUshort SetGroupOrderID;
        public ReturnUshort GetGroupOrderID;
    }
}
