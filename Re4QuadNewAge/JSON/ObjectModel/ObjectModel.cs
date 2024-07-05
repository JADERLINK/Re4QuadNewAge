using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;

namespace Re4QuadExtremeEditor.src.JSON
{
    public abstract class ObjectModel
    {
        public enum EType
        {
            NULL = 0,
            V2007 = 1,
            PS2 = 2,
            UHD = 3
        }

        public string JsonFileName { get; internal set; }
        public EType Type { get; internal set; }
        public string PathKey { get; internal set; }

        public ObjectModel(string JsonFileName, EType Type, string PathKey)
        {
            this.JsonFileName = JsonFileName;
            this.Type = Type;
            this.PathKey = PathKey;
        }

        public override bool Equals(object obj)
        {
            return obj is ObjectModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

    public class PmdSub
    {
        public string PmdFile { get; internal set; }
        public PreFix PreFix { get; internal set; }

        public PmdSub(string PmdFile, PreFix PreFix)
        {
            this.PmdFile = PmdFile;
            this.PreFix = PreFix;
        }
    }

    public class ObjectModel2007 : ObjectModel
    {
        public PmdSub[] List { get; }

        public ObjectModel2007(string JsonFileName, EType Type, string PathKey, PmdSub[] List) : base(JsonFileName, Type, PathKey)
        {
            this.List = List;
        }

        public override bool Equals(object obj)
        {
            return obj is ObjectModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

    public class Ps2Sub
    {
        public string Ps2BinFile { get; internal set; }
        public string Ps2TplFile { get; internal set; }
        public PreFix PreFix { get; internal set; }

        public Ps2Sub(string Ps2BinFile, string Ps2TplFile, PreFix PreFix)
        {
            this.Ps2BinFile = Ps2BinFile;
            this.Ps2TplFile = Ps2TplFile;
            this.PreFix = PreFix;
        }
    }

    public class ObjectModelPs2 : ObjectModel
    {
        public Ps2Sub[] List { get; }

        public ObjectModelPs2(string JsonFileName, EType Type, string PathKey, Ps2Sub[] List) : base(JsonFileName, Type, PathKey)
        {
            this.List = List;
        }

        public override bool Equals(object obj)
        {
            return obj is ObjectModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }

    public class UhdSub
    {
        public string UhdBinFile { get; internal set; }
        public string UhdTplFile { get; internal set; }
        public PreFix PreFix { get; internal set; }

        public UhdSub(string UhdBinFile, string UhdTplFile, PreFix PreFix)
        {
            this.UhdBinFile = UhdBinFile;
            this.UhdTplFile = UhdTplFile;
            this.PreFix = PreFix;
        }
    }

    public class ObjectModelUhd : ObjectModel
    {
        public string PackPathKey { get; internal set; }
        public string PackFolder { get; internal set; }
        public UhdSub[] List { get; }

        public ObjectModelUhd(string JsonFileName, EType Type, string PathKey, string PackPathKey, string PackFolder, UhdSub[] List) : base(JsonFileName, Type, PathKey)
        {
            this.PackPathKey = PackPathKey;
            this.PackFolder = PackFolder;
            this.List = List;
        }
        public override bool Equals(object obj)
        {
            return obj is ObjectModel o && o.JsonFileName == JsonFileName;
        }

        public override int GetHashCode()
        {
            return JsonFileName.GetHashCode();
        }
    }
}
