using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using DynamicTypeDescriptor;

namespace Re4QuadExtremeEditor.src.Class.MyProperty
{
    public abstract class GenericProperty
    {
        private DynamicCustomTypeDescriptor DCTD = null;

        protected void SetThis(object instance)
        {
            DCTD = ProviderInstaller.Install(instance);
            DCTD.PropertySortOrder = CustomSortOrder.AscendingById;
            DCTD.CategorySortOrder = CustomSortOrder.AscendingById;
        }

        public GenericProperty() {}

        #region manipula os atributos

        public void ChangePropertyIsBrowsable(string property, bool show)
        {
            DCTD?.GetProperty(property ?? "")?.SetIsBrowsable(show);
        }

        public void ChangePropertyIsReadOnly(string property, bool isReadOnly) 
        {
            DCTD?.GetProperty(property ?? "")?.SetIsReadOnly(isReadOnly);
        }

        public void ChangePropertyName(string property, string name)
        {
            DCTD?.GetProperty(property ?? "")?.SetDisplayName(name);
        }

        public void ChangeCustomPropertyName(string property, aLang AttributeTextId)
        {
            DCTD?.GetProperty(property ?? "")?.SetDisplayName(Lang.GetAttributeText(AttributeTextId));
        }


        public void ChangePropertyDescription(string property, string description)
        {
            DCTD?.GetProperty(property ?? "")?.SetDescription(description);
        }

        public void ChangeCustomPropertyDescription(string property, aLang AttributeTextId)
        {
            DCTD?.GetProperty(property ?? "")?.SetDescription(Lang.GetAttributeText(AttributeTextId));
        }

        public void ChangePropertyCategory(string property, string category)
        {
            DCTD?.GetProperty(property ?? "")?.SetCategory(category);
        }

        public void ChangeCustomPropertyCategory(string property, aLang AttributeTextId)
        {
            DCTD?.GetProperty(property ?? "")?.SetCategory(Lang.GetAttributeText(AttributeTextId));
        }

        public void ChangePropertyId(string property, int Id)
        {
            var prop = DCTD?.GetProperty(property ?? "");
            if (prop != null)
            {
                prop.PropertyId = Id;
            }       
        }


        public void SetPropertyValue(string property, object value)
        {
            var prop = GetClassType()?.GetProperty(property ?? "");
            prop?.SetValue(this, value);
        }

        public void SetPropertyValueDCTD(string property, object value) 
        {
            var prop = DCTD?.GetProperty(property ?? "");
            prop?.SetValue(prop.m_owner, value);
        }

        public object GetPropertyValue(string property)
        {
            var prop = GetClassType()?.GetProperty(property ?? "");
            return prop?.GetValue(this);
        }

        public object GetPropertyValueDCTD(string property)
        {
            var prop = DCTD?.GetProperty(property ?? "");
            return prop?.GetValue(prop.m_owner);
        }

        public CustomPropertyDescriptor GetPropertyDCTD(string property) 
        {
            return DCTD?.GetProperty(property ?? "");
        }

        public PropertyInfo GetProperty(string property) 
        {
            return GetClassType().GetProperty(property ?? "");
        }

        public abstract Type GetClassType();

        public PropertyDescriptorCollection GetProperties() 
        {
            return DCTD?.GetProperties();
        }

        #endregion
    }
}
