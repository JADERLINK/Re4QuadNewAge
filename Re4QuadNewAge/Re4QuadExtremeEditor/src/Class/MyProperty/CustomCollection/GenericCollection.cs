using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using DynamicTypeDescriptor;
using Re4QuadExtremeEditor.src.Class.Interfaces;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection // from https://www.codeproject.com/Articles/4448/Customized-Display-of-Collection-Data-in-a-Propert
{
	public class GenericCollection : ICustomTypeDescriptor
	{
		public GenericCollection() 
		{
		}

		#region collection impl

		private List<object> List = new List<object>();

		/// <summary>
		/// Adds an object to the collection
		/// </summary>
		public void Add(object obj)
		{
			this.List.Add(obj);
		}

		public void AddRange(IEnumerable<object> collection) 
		{
			this.List.AddRange(collection);
		}


		/// <summary>
		/// Removes an object from the collection
		/// </summary>
		public void Remove(object obj)
		{
			this.List.Remove(obj);
		}

		/// <summary>
		/// Returns an object at index position.
		/// </summary>
		public object this[int index]
		{
			get
			{
				return (object)this.List[index];
			}
		}

		public void OrderList() 
		{
			var list = (from obj in List.Cast<object>().ToArray()
					 orderby obj.ToString()
					 select obj).ToArray();
			List.Clear();
			List.AddRange(list);
		}

		public int Count { get => List.Count; }

        #endregion


        // Implementation of interface ICustomTypeDescriptor 
        #region ICustomTypeDescriptor impl 

        public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}


		/// <summary>
		/// Called to get the properties of this type. Returns properties with certain
		/// attributes. this restriction is not implemented here.
		/// </summary>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		/// <summary>
		/// Called to get the properties of this type.
		/// </summary>
		/// <returns></returns>
		public PropertyDescriptorCollection GetProperties()
		{
			// Create a collection object to hold property descriptors
			PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

			// Iterate the list of object
			for (int i = 0; i < this.List.Count; i++)
			{
				// Create a property descriptor for the  item and add to the property descriptor collection
				GenericCollectionPropertyDescriptor pd = new GenericCollectionPropertyDescriptor(this, i);
				pds.Add(pd);
			}
			// return the property descriptor collection
			return pds;
		}

		#endregion
	}

}
