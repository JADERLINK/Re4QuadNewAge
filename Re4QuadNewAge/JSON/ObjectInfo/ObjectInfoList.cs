using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.JSON
{
    public class ObjectInfoList
    {
        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string JsonFileName { get; }

        /// <summary>
        /// Descrição dessa listagem
        /// </summary>
        public string ListName { get; }

        /// <summary>
        /// Nome da pasta em que estão os arquivos json
        /// </summary>
        public string Folder { get; }

        /// <summary>
        /// Dic com a Descrição para o objeto
        /// </summary>
        public Dictionary<ushort, ObjectInfo> List { get; }

        public ObjectInfoList(string JsonFileName, string ListName, string Folder, Dictionary<ushort, ObjectInfo> List)
        {
            this.JsonFileName = JsonFileName;
            this.ListName = ListName;
            this.Folder = Folder;
            this.List = List;
        }

        public override string ToString()
        {
            return ListName + " {" + JsonFileName + "}";
        }

        public override bool Equals(object obj)
        {
            return (obj is ObjectInfoList r && r.JsonFileName == this.JsonFileName);
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }

    }
}
