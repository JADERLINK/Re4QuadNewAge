using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.MyProperty;
using Re4QuadExtremeEditor.src.Class.ObjMethods;

namespace Re4QuadExtremeEditor.src.Class
{
    public class MultiSelectObjInfoToProperty
    {
        public string Text { get; }

        public List<GenericProperty> propertyColetions;

        public UpdateMethods updateMethods;

        public MultiSelectObjInfoToProperty(string text, UpdateMethods updateMethods)
        {
            propertyColetions = new List<GenericProperty>();
            this.updateMethods = updateMethods;
            Text = text;
        }

        public void Add(GenericProperty propertyColetion)
        {
            propertyColetions.Add(propertyColetion);
        }

        public void AddRange(IEnumerable<GenericProperty> list) 
        {
            propertyColetions.AddRange(list);
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
        public string ClassSource { get; }

        public MultiSelectObj(string PropertyName, string PropertyDisplayName, string PropertyDescription, Type PropertyType, int ByteLenght, string ClassSource) 
        {
            this.PropertyName = PropertyName;
            this.PropertyDisplayName = PropertyDisplayName;
            this.PropertyDescription = PropertyDescription;
            this.PropertyType = PropertyType;
            this.ByteLenght = ByteLenght;
            this.ClassSource = ClassSource;
        }

        public override string ToString()
        {
            return PropertyDisplayName; //+ "      " + PropertyName + "       " + PropertyType.FullName + "      " + ByteLenght;
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


}
