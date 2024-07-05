using System;
using System.Collections.Generic;
using System.Text;

namespace ViewerBase
{
    // representar o OBJETO arquivo com modelo obj/smd/pmd/bin
    // representar sub material, MTL/TPL
    // representar grupo de texturas
    //-------------

    public class OBJ_Representation // representa o modelo/arquivo
    {
        public string ModelID { get; private set; }

        public OBJ_Representation(string ModelID) 
        {
            this.ModelID = ModelID;
        }

    }

    public class MaterialGroup_Representation // representa MaterialGroup (MTL, materials dos objetos)
    {
        public string MaterialGroupName { get; private set; }

        public MaterialGroup_Representation(string MaterialGroupName)
        {
            this.MaterialGroupName = MaterialGroupName;
        }
    }

    public class MatTexGroup_Representation // representa MatTexGroup (TPL)
    {
        public string MatTexGroupName { get; private set; }

        public MatTexGroup_Representation(string MatTexGroupName)
        {
            this.MatTexGroupName = MatTexGroupName;
        }
    }

    public class TEX_Representation // representa o grupo de texturas
    {
        public string TextureGroupName { get; private set; }
        public List<string> TextureList { get; private set; }

        public TEX_Representation(string TextureGroupName, List<string> TextureList)
        {
            this.TextureGroupName = TextureGroupName;
            this.TextureList = TextureList;
        }
    }

    public class SMD_Representation //representa um arquivo SMD do re4
    {
        public string SmdName { get; private set; }

        public SMD_Representation(string SmdName)
        {
            this.SmdName = SmdName;
        }
    }

    public class SMX_Representation //representa um arquivo SMX do re4
    {
        public string SmxName { get; private set; }

        public SMX_Representation(string SmxName)
        {
            this.SmxName = SmxName;
        }
    }

}
