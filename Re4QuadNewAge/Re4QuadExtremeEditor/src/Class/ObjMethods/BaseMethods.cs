using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public abstract class BaseMethods
    {
        public Func<SimpleEndianBinaryIO.Endianness> GetEndianness;

        public ReturnByteFromPosition ReturnByteFromPosition;
        public SetByteFromPosition SetByteFromPosition;

        public ReturnSbyteFromPosition ReturnSbyteFromPosition;
        public SetSbyteFromPosition SetSbyteFromPosition;

        public ReturnUshortFromPosition ReturnUshortFromPosition;
        public SetUshortFromPosition SetUshortFromPosition;

        public ReturnShortFromPosition ReturnShortFromPosition;
        public SetShortFromPosition SetShortFromPosition;

        public ReturnIntFromPosition ReturnIntFromPosition;
        public SetIntFromPosition SetIntFromPosition;

        public ReturnUintFromPosition ReturnUintFromPosition;
        public SetUintFromPosition SetUintFromPosition;

        public ReturnFloatFromPosition ReturnFloatFromPosition;
        public SetFloatFromPosition SetFloatFromPosition;

        public ReturnByteArrayFromPosition ReturnByteArrayFromPosition;
        public SetByteArrayFromPosition SetByteArrayFromPosition;
    }
}
