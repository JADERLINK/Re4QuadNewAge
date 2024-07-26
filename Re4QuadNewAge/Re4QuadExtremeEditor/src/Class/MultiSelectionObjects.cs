using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.MyProperty;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System.ComponentModel;
using System.Drawing.Design;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Interfaces;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection;

namespace Re4QuadExtremeEditor.src.Class
{
    public class MultiSelectObjInfoToProperty
    {
        public string Text { get; private set; }

        private List<MultiSelectEntryObj> EntryColetions;

        public UpdateMethods updateMethods { get; private set; }

        public MultiSelectObjInfoToProperty(string text, UpdateMethods updateMethods)
        {
            EntryColetions = new List<MultiSelectEntryObj>();
            this.updateMethods = updateMethods;
            Text = text;
        }

        public void Add(MultiSelectEntryObj Entry)
        {
            EntryColetions.Add(Entry);
        }

        public void AddRange(IEnumerable<MultiSelectEntryObj> list) 
        {
            EntryColetions.AddRange(list);
        }

        public List<Object3D> GetObject3DList() 
        {
            return (from o in EntryColetions select o.GetObject3D()).ToList();
        }

        public int GetCount() 
        {
           return EntryColetions.Count;
        }

        public override string ToString()
        {
            return Text;
        }

    }


    public struct MultiSelectObj
    {
        public string PropertyName { get; }
        public string PropertyDisplayName { get; }
        public string PropertyDescription { get; }
        public Type PropertyType { get; }
        public int ByteLenght { get; }
        public MultiSelectObj(string PropertyName, string PropertyDisplayName, string PropertyDescription, Type PropertyType, int ByteLenght) 
        {
            this.PropertyName = PropertyName;
            this.PropertyDisplayName = PropertyDisplayName;
            this.PropertyDescription = PropertyDescription;
            this.PropertyType = PropertyType;
            this.ByteLenght = ByteLenght;
        }

        public override string ToString()
        {
            return PropertyDisplayName;// + "      " + PropertyName + "       " + PropertyType.FullName + "      " + ByteLenght;
        }

        public override bool Equals(object obj)
        {
            return obj is MultiSelectObj o && o.PropertyName == PropertyName;
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

    }

    public struct MultiSelectKey 
    {
        public string PropertyName { get; }
        public Type PropertyType { get; }
        public int ByteLenght { get; }
        public MultiSelectKey(string PropertyName, Type PropertyType, int ByteLenght)
        {
            this.PropertyName = PropertyName;
            this.PropertyType = PropertyType;
            this.ByteLenght = ByteLenght;
        }

        public override string ToString()
        {
            return PropertyName;
        }

        public override bool Equals(object obj)
        {
            return obj is MultiSelectKey o && o.PropertyName == PropertyName && o.PropertyType == PropertyType && o.ByteLenght == ByteLenght;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + PropertyName.GetHashCode();
                hash = hash * 23 + PropertyType.GetHashCode();
                hash = hash * 23 + ByteLenght.GetHashCode();
                return hash;
            }
        }

    }


    //[TypeConverter(typeof(GenericConverter))]
    public class MultiSelectEntryObj : IDisplay
    {
        private Object3D object3D = null;

        public MultiSelectEntryObj(Object3D object3D)
        {
            this.object3D = object3D;
        }

        public Object3D GetObject3D() 
        {
            return object3D;
        }

        [Browsable(false)]
        public string Text_Name => object3D?.Parent?.Text ?? "";

        [Browsable(false)]
        public string Text_Value => object3D?.AltText ?? "";

        [Browsable(false)]
        public string Text_Description => Lang.GetAttributeText(aLang.NewAge_InternalLineIDDisplayName) + ": " + (object3D?.ObjLineRef.ToString() ?? "");

        public override string ToString()
        {
            return Text_Value;
        }

        public override bool Equals(object obj)
        {
            return obj is MultiSelectEntryObj o && object3D.Equals(o.object3D);
        }

        public override int GetHashCode()
        {
            return object3D.GetHashCode();
        }

        //[CustomCategory(aLang.NewAge_InternalLineIDCategory)]
        //[CustomDisplayName(aLang.NewAge_InternalLineIDDisplayName)]
        //[CustomDescription(aLang.NewAge_InternalLineIDDescription)]
        //[DefaultValue(null)]
        //[ReadOnly(true)]
        //[Browsable(true)]
        //[DynamicTypeDescriptor.Id(0, 0)]
        //public string InternalLineID { get => object3D?.ObjLineRef.ToString() ?? ""; }
    }
}
