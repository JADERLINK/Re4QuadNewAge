using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src.JSON
{
    public abstract class RoomModel
    {
        public enum EType
        {
            NULL = 0,
            V2007 = 1,
            PS2 = 2,
            UHD = 3,
            R100UHD = 4,
            PS4NS = 5,
            R100PS4NS = 6
        }

        public string JsonFileName { get; internal set; }
        public ushort HexID { get; internal set; }
        public string Description { get; internal set; }
        public string PathKey { get; internal set; }
        public EType Type { get; internal set; }
        public string SmdFile { get; internal set; }
        public string SmxFile { get; internal set; }

        public RoomModel(string JsonFileName, ushort HexID, string Description, string PathKey, EType Type, string SmdFile, string SmxFile)
        {
            this.JsonFileName = JsonFileName;
            this.HexID = HexID;
            this.Description = Description;
            this.PathKey = PathKey;
            this.Type = Type;
            this.SmdFile = SmdFile;
            this.SmxFile = SmxFile;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

    public class RoomModelPs2 : RoomModel
    {
        public RoomModelPs2(string JsonFileName, ushort HexID, string Description, string PathKey, EType Type, string SmdFile, string SmxFile
            ) : base(JsonFileName, HexID, Description, PathKey, Type, SmdFile, SmxFile) { }

        public override bool Equals(object obj)
        {
            return obj is RoomModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

    public class RoomModel2007 : RoomModel
    {
        public string PmdFolder { get; internal set; }
        public string PmdBaseName { get; internal set; }
        public RoomModel2007(string JsonFileName, ushort HexID, string Description, string PathKey, EType Type, string SmdFile, string SmxFile, string PmdFolder, string PmdBaseName
            ) : base(JsonFileName, HexID, Description, PathKey, Type, SmdFile, SmxFile)
        {
            this.PmdFolder = PmdFolder;
            this.PmdBaseName = PmdBaseName;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

    public class RoomModelUhd : RoomModel
    {
        public string PackFolder { get; internal set; }

        public RoomModelUhd(string JsonFileName, ushort HexID, string Description, string PathKey, EType Type, string SmdFile, string SmxFile, string PackFolder
            ) : base(JsonFileName, HexID, Description, PathKey, Type, SmdFile, SmxFile)
        {
            this.PackFolder = PackFolder;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }


    public class RoomModelR100Uhd : RoomModel
    {
        public string PackFolder { get; internal set; }
        public string SharedSmd { get; internal set; }
        public string[] DatSmd { get; internal set; }

        public RoomModelR100Uhd(string JsonFileName, ushort HexID, string Description, string PathKey, EType Type, string SmdFile, string SmxFile,
            string PackFolder, string SharedSmd, string[] DatSmd) : base(JsonFileName, HexID, Description, PathKey, Type, SmdFile, SmxFile)
        {
            this.PackFolder = PackFolder;
            this.SharedSmd = SharedSmd;
            this.DatSmd = DatSmd;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

}
