using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.JSON
{
    public class RoomListObj
    {
        public string JsonFileName { get; internal set; }
        public string Folder { get; internal set; }
        public string Description { get; internal set; }

        public RoomListObj(string JsonFileName, string Folder, string Description) 
        {
            this.JsonFileName = JsonFileName;
            this.Folder = Folder;
            this.Description = Description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomListObj r && r.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }
}
