using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public class NewAge_EFF_Methods : BaseMethods
    {
        public ReturnByteArray ReturnLine;
        public SetByteArray SetLine;

        public SetUshort SetEntryOrderID;
        public ReturnUshort GetEntryOrderID;
    }

    public class NewAge_EFF_EffectGroup_Methods : BaseMethods
    {
        public Func<GroupType> GetGrouptype;
        
        public ReturnByteArray ReturnLine;
        public SetByteArray SetLine;

        public SetUshort SetEntryOrderID;
        public ReturnUshort GetEntryOrderID;

        public ReturnInt GetEntryCountInGroup;

        public ReturnFloat ReturnAngleX;
        public SetFloat SetAngleX;

        public ReturnFloat ReturnAngleY;
        public SetFloat SetAngleY;

        public ReturnFloat ReturnAngleZ;
        public SetFloat SetAngleZ;

        public ReturnFloat ReturnPositionX;
        public SetFloat SetPositionX;

        public ReturnFloat ReturnPositionY;
        public SetFloat SetPositionY;

        public ReturnFloat ReturnPositionZ;
        public SetFloat SetPositionZ;

        public ReturnUint ReturnAngleX_Hex;
        public SetUint SetAngleX_Hex;

        public ReturnUint ReturnAngleY_Hex;
        public SetUint SetAngleY_Hex;

        public ReturnUint ReturnAngleZ_Hex;
        public SetUint SetAngleZ_Hex;

        public ReturnUint ReturnPositionX_Hex;
        public SetUint SetPositionX_Hex;

        public ReturnUint ReturnPositionY_Hex;
        public SetUint SetPositionY_Hex;

        public ReturnUint ReturnPositionZ_Hex;
        public SetUint SetPositionZ_Hex;

        public ReturnUint ReturnPositionW_Hex;
        public SetUint SetPositionW_Hex;
    }

    public class NewAge_EFF_EffectEntry_Methods : BaseMethods
    {
        public ReturnByteArray ReturnLine;
        public SetByteArray SetLine;

        public SetUshort SetEntryOrderID;
        public ReturnUshort GetEntryOrderID;

        public SetUshort SetGroupOrderID;
        public ReturnUshort GetGroupOrderID;

        public SetByte SetTableID;
        public ReturnByte GetTableID;

        public ReturnFloat ReturnAngleX;
        public SetFloat SetAngleX;

        public ReturnFloat ReturnAngleY;
        public SetFloat SetAngleY;

        public ReturnFloat ReturnAngleZ;
        public SetFloat SetAngleZ;

        public ReturnFloat ReturnPositionX;
        public SetFloat SetPositionX;

        public ReturnFloat ReturnPositionY;
        public SetFloat SetPositionY;

        public ReturnFloat ReturnPositionZ;
        public SetFloat SetPositionZ;

        public ReturnUint ReturnAngleX_Hex;
        public SetUint SetAngleX_Hex;

        public ReturnUint ReturnAngleY_Hex;
        public SetUint SetAngleY_Hex;

        public ReturnUint ReturnAngleZ_Hex;
        public SetUint SetAngleZ_Hex;

        public ReturnUint ReturnPositionX_Hex;
        public SetUint SetPositionX_Hex;

        public ReturnUint ReturnPositionY_Hex;
        public SetUint SetPositionY_Hex;

        public ReturnUint ReturnPositionZ_Hex;
        public SetUint SetPositionZ_Hex;

        public ReturnUint ReturnPositionW_Hex;
        public SetUint SetPositionW_Hex;

    }

    public class NewAge_EFF_Table9Entry_Methods : BaseMethods
    {
        public ReturnByteArray ReturnLine;
        public SetByteArray SetLine;

        public SetUshort SetEntryOrderID;
        public ReturnUshort GetEntryOrderID;

        public SetUshort SetGroupOrderID;
        public ReturnUshort GetGroupOrderID;

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

        public ReturnUint ReturnPositionW_Hex;
        public SetUint SetPositionW_Hex;
    }

    public class NewAge_EFF_EffectGroup_Methods_MethodsForGL
    {
        public ReturnVector3 GetPosition;

        public ReturnMatrix4 GetAngle;
    }

    public class NewAge_EFF_EffectEntry_Methods_MethodsForGL
    {
        public ReturnVector3 GetPosition;

        public ReturnMatrix4 GetAngle;

        public ReturnUshort GetGroupOrderID;

        public ReturnEffectEntryTableID GetTableID;
    }

    public class NewAge_EFF_Table9Entry_Methods_MethodsForGL
    {
        public ReturnVector3 GetPosition;

        public ReturnUshort GetGroupOrderID;
    }

}
