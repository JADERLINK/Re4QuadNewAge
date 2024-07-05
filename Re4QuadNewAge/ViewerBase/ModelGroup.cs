using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ViewerBase
{
    public class ModelGroup
    {
        // todos os objetos
        public Dictionary<string, RenderedObjModel> Objects { get; private set; }

        //todas as meshs
        public Dictionary<string, GLMeshPart> MeshParts { get; private set; }

        //todos os groups de Material
        public Dictionary<string, MaterialGroup> MaterialGroupDic { get; private set; }

        //todos os groups de MatTex
        public Dictionary<string, MatTexGroup> MatTexGroupDic { get; private set; }

        // todos os linkers
        public Dictionary<string, MatLinker> MatLinkerDic { get; private set; }

        //todas as texturas
        public Dictionary<string, TextureRef> TextureRefDic { get; private set; }

        // arquivo Smd do re4
        public SmdGroup SmdGroup { get; private set; }

        // arquivo Smx do re4
        public SmxGroup SmxGroup { get; private set; }

        // lista com o nomes dos bin que são usado nos scenario
        // BIN_ID, nome do modelo
        public Dictionary<int, string> ScenarioBinList { get; private set; }

        public ModelGroup()
        {
            Objects = new Dictionary<string, RenderedObjModel>();
            MeshParts = new Dictionary<string, GLMeshPart>();
            MaterialGroupDic = new Dictionary<string, MaterialGroup>();
            MatTexGroupDic = new Dictionary<string, MatTexGroup>();
            MatLinkerDic = new Dictionary<string, MatLinker>();
            TextureRefDic = new Dictionary<string, TextureRef>();
            SmdGroup = null;
            SmxGroup = null;
            ScenarioBinList = new Dictionary<int, string>();
        }

        public void AddModel(TreatedModel model)
        {
            if (!Objects.ContainsKey(model.ModelID))
            {
                RenderedObjModel renderedModel = new RenderedObjModel();
                renderedModel.BIN_ID = model.BIN_ID;
                renderedModel.ModelID = model.ModelID;
                renderedModel.CenterBoundary = model.CenterBoundary;
                renderedModel.MaxBoundary = model.MaxBoundary;
                renderedModel.MinBoundary = model.MinBoundary;
                renderedModel.MeshNames = new List<string>();

                for (int i = 0; i < model.Meshes.Count; i++)
                {
                    GLMeshPart meshPart = new GLMeshPart(model.Meshes[i]);
                    string key = model.Meshes[i].RefModelID + "||" + model.Meshes[i].MeshID;
                    renderedModel.MeshNames.Add(key);
                    MeshParts.Add(key, meshPart);
                }

                Objects.Add(model.ModelID, renderedModel);

                MatLinker matLinker = new MatLinker();
                matLinker.ModelID = model.ModelID;
                matLinker.MatTexGroupName = model.ModelID;
                MatLinkerDic.Add(model.ModelID, matLinker);

                if (!ScenarioBinList.ContainsKey(model.BIN_ID) && model.BIN_ID != -1)
                {
                    ScenarioBinList.Add(model.BIN_ID, model.ModelID);
                }
            }
        }

        public void RemoveModel(OBJ_Representation repr) 
        {
            if (Objects.ContainsKey(repr.ModelID))
            {
                var obj = Objects[repr.ModelID];
                for (int i = 0; i < obj.MeshNames.Count; i++)
                {
                    string key = obj.MeshNames[i];
                    if (MeshParts.ContainsKey(key))
                    {
                        MeshParts[key].Unload();
                        MeshParts.Remove(key);
                    }
                }

                if (ScenarioBinList.ContainsKey(obj.BIN_ID))
                {
                    ScenarioBinList.Remove(obj.BIN_ID);
                }

                Objects.Remove(repr.ModelID);
            }

            if (MatLinkerDic.ContainsKey(repr.ModelID))
            {
                MatLinkerDic.Remove(repr.ModelID);
            }
         
        }

        public void AddMaterialGroup(MaterialGroup group)
        {
            if (!MaterialGroupDic.ContainsKey(group.MaterialGroupName))
            {
                MaterialGroupDic.Add(group.MaterialGroupName, group);
            }
        }

        public void RemoveMaterialGroup(MaterialGroup_Representation repr)
        {
            if (MaterialGroupDic.ContainsKey(repr.MaterialGroupName))
            {
                MaterialGroupDic.Remove(repr.MaterialGroupName);
            }
        }

        public void AddMatTexGroup(MatTexGroup group) 
        {
            if (!MatTexGroupDic.ContainsKey(group.MatTexGroupName))
            {
                MatTexGroupDic.Add(group.MatTexGroupName, group);
            }
        }

        public void RemoveMatTexGroup(MatTexGroup_Representation repr) 
        {
            if (MatTexGroupDic.ContainsKey(repr.MatTexGroupName))
            {
                MatTexGroupDic.Remove(repr.MatTexGroupName);
            }
        }

        public void AddTextureRef(Dictionary<string, Bitmap> textures) 
        {
            foreach (var tex in textures)
            {
                if (!TextureRefDic.ContainsKey(tex.Key))
                {
                    TextureRef textureRef = new TextureRef(tex.Value);
                    TextureRefDic.Add(tex.Key, textureRef);
                }
            }
        }

        public void RemoveTextureRef(TEX_Representation repr) 
        {
            foreach (var texName in repr.TextureList)
            {
                if (TextureRefDic.ContainsKey(texName))
                {
                    TextureRefDic[texName].Unload();
                    TextureRefDic.Remove(texName);
                }
            }
        
        }

        public void AddSmdGroup(SmdGroup SmdGroup) 
        {
            this.SmdGroup = SmdGroup;
        }

        public void RemoveSmdGroup() 
        {
            this.SmdGroup = null;
        }

        public void AddSmxGroup(SmxGroup SmxGroup)
        {
            this.SmxGroup = SmxGroup;
        }

        public void RemoveSmxGroup()
        {
            this.SmxGroup = null;
        }

    }
}
