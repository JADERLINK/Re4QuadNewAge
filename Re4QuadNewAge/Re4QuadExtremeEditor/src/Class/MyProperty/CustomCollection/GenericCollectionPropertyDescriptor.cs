using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection // from https://www.codeproject.com/Articles/4448/Customized-Display-of-Collection-Data-in-a-Propert

{   /// <summary>
	/// Summary description for CollectionPropertyDescriptor.
	/// </summary>
	public class GenericCollectionPropertyDescriptor : PropertyDescriptor
	{
		private GenericCollection collection = null;
		private int index = -1;

		public GenericCollectionPropertyDescriptor(GenericCollection coll, int idx) :
			base("#" + idx.ToString(), null)
		{
			this.collection = coll;
			this.index = idx;
		}

		public override AttributeCollection Attributes
		{
			get
			{
				return new AttributeCollection(null);
			}
		}

		public override bool CanResetValue(object component)
		{
			return true;
		}

		public override Type ComponentType
		{
			get
			{
				return this.collection.GetType();
			}
		}

		public override string DisplayName
		{
			get
			{
                if (this.collection[index] is Interfaces.IDisplay diplay)
                {
					return diplay.Text_Name;
				}
				return this.collection?[index]?.ToString() ?? "";
			}
		}

		public override string Description
		{
			get
			{
				if (this.collection[index] is Interfaces.IDisplay diplay)
				{
					return diplay.Text_Description;
				}
				return ""; // a descrição do objeto
			}
		}

		public override object GetValue(object component)
		{
			return this.collection[index];
		}

		public override bool IsReadOnly
		{
			get { return false; }
		}

		public override string Name
		{
			get { return "#" + index.ToString(); }
		}

		public override Type PropertyType
		{
			get { return this.collection[index].GetType(); }
		}

		public override void ResetValue(object component)
		{
		}

		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}

		public override void SetValue(object component, object value)
		{
			// this.collection[index] = value;
		}
	}
}
