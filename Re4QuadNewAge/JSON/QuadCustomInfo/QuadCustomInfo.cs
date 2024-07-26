using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.JSON
{
    public class QuadCustomInfo
    {
        /// <summary>
        /// ID do objeto
        /// </summary>
        public uint ID { get; }

        /// <summary>
        /// Key do modelo para ser procurado na lista de modelos
        /// </summary>
        public string ObjectModel { get; }

        /// <summary>
        /// Nome para o objeto
        /// </summary>
        public string Name { get; }

        public QuadCustomInfo(uint ID, string ObjectModel, string Name)
        {
            this.ID = ID;
            this.ObjectModel = ObjectModel;
            this.Name = Name;
        }

        public override string ToString()
        {
            return ID.ToString() + ": " + Name;
        }

        public override bool Equals(object obj)
        {
            return (obj is QuadCustomInfo r && r.ID == this.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

    }
}