using System;
using System.Collections.Generic;
using System.Text;
using Re4QuadExtremeEditor.src.JSON;
using Re4QuadExtremeEditor.src.Class.Shaders;
using Re4QuadExtremeEditor.src;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using ViewerBase;

namespace NewAgeTheRender
{
    public class ModelGroupConteiner
    {
        public string ModelGroupName { get; }

        private Dictionary<string, ObjModel3D> ObjModelDic;
        private ModelGroup modelGroup;
        private ModelNodeGroup mng;
        private TexturePackNodeGroup tpng;

        public ModelGroupConteiner(string ModelGroupName)
        {
            modelGroup = new ModelGroup();
            mng = new ModelNodeGroup();
            tpng = new TexturePackNodeGroup();

            this.ModelGroupName = ModelGroupName;
            ObjModelDic = new Dictionary<string, ObjModel3D>();
        }

        /// <summary>
        /// adiciona obj models
        /// </summary>
        /// <param name="jsonFiles">caminho completo para o arquivo json</param>
        public void AddModelObjects(string[] jsonFiles) 
        {
            foreach (var jsonFile in jsonFiles)
            {
                AddModelObject(jsonFile);
            }
        }

        /// <summary>
        /// adiciona obj model
        /// </summary>
        /// <param name="jsonFiles">caminho completo para o arquivo json</param>
        public void AddModelObject(string jsonFile)
        {
            ObjModel3D objModel3D = new ObjModel3D(modelGroup, mng, tpng);
            string key = objModel3D.LoadModelObject(jsonFile);
            if (key != null && !ObjModelDic.ContainsKey(key))
            {
                ObjModelDic.Add(key, objModel3D);
            }
        }

        public void RenderModel(string ModelKey, RspFix obj) 
        {
            ObjModelDic[ModelKey]?.Render(obj);
        }

        public bool ContainsKey(string ModelKey) 
        {
            return ObjModelDic.ContainsKey(ModelKey);
        }

        public void ClearGL() 
        {
            foreach (NodeItem i in mng.Nodes.List)
            {
                i.Responsibility.ReleaseResponsibilities();
            }

            foreach (NodeItem i in tpng.Nodes.List)
            {
                i.Responsibility.ReleaseResponsibilities();
            }
            ObjModelDic.Clear();
        }

        public BoundingBoxLimit GetBoundingBoxLimit(string ModelKey) 
        {
            if (ObjModelDic.ContainsKey(ModelKey))
            {
                return ObjModelDic[ModelKey].GetBoundingBoxLimit();
            }

            return new BoundingBoxLimit(OpenTK.Vector3.One, -OpenTK.Vector3.One);
        }
    }
}
