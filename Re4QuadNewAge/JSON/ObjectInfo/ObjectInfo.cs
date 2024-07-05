using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.JSON
{
    public class ObjectInfo
    {
        /// <summary>
        /// ID do objeto no jogo
        /// </summary>
        public ushort HexID { get; }

        /// <summary>
        /// Key do modelo para ser procurado na lista de modelos
        /// </summary>
        public string ObjectModel { get; }

        /// <summary>
        /// Nome para o objeto
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Descrição para o objeto
        /// </summary>
        public string Description { get; }

        public ObjectInfo(ushort HexID, string ObjectModel, string Name, string Description)
        {
            this.HexID = HexID;
            this.ObjectModel = ObjectModel;
            this.Name = Name;
            this.Description = Description;
        }

        public override string ToString()
        {
            return HexID.ToString("X4") + ": " + Name;
        }

        public override bool Equals(object obj)
        {
            return (obj is ObjectInfo r && r.HexID == this.HexID);
        }

        public override int GetHashCode()
        {
            return HexID.GetHashCode();
        }

    }
}
