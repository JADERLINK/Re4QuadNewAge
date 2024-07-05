using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Class
{
    public class UshortObjForListBox
    {
        public ushort ID { get; }
        public string Description { set; get; }
        public UshortObjForListBox(ushort ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return (obj is UshortObjForListBox o && o.ID == ID);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }

    public class ByteObjForListBox
    {
        public byte ID { get; }
        public string Description { set; get; }
        public ByteObjForListBox(byte ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return (obj is ByteObjForListBox o && o.ID == ID);
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }

    public class BoolObjForListBox
    {
        public bool ID { get; }
        public string Description { set; get; }
        public BoolObjForListBox(bool ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return (obj is BoolObjForListBox o && o.ID == ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }

    public class GroupTypeObjForListBox
    {
        public GroupType ID { get; }
        public string Description { set; get; }
        public GroupTypeObjForListBox(GroupType ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return (obj is GroupTypeObjForListBox o && o.ID == ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }

    public class MoveObjTypeObjForListBox
    {
        public MoveObjType ID { get; }
        public string Description { set; get; }
        public MoveObjTypeObjForListBox(MoveObjType ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return (obj is MoveObjTypeObjForListBox o && o.ID == ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }


}
