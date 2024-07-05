using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewerBase
{
    public class ResponsibilityContainer
    {
        private List<IResponsibility> responsibilityList;

        public ResponsibilityContainer() 
        {
            responsibilityList = new List<IResponsibility>();
        }

        public void Add(IResponsibility responsibility) 
        {
            responsibilityList.Add(responsibility);
        }

        public string[] MyResponsibilityList()
        {
            return (from r in responsibilityList
                   select r.MyResponsibility()).ToArray();
        }

        public void ReleaseResponsibilities()
        {
            foreach (var item in responsibilityList)
            {
                item.ReleaseResponsibility();
            }
            responsibilityList.Clear();
        }

        ~ResponsibilityContainer() 
        {
            ReleaseResponsibilities();
        }
    }

    public interface IResponsibility
    {
        void ReleaseResponsibility();
        string MyResponsibility();
    }

    public class ModelResponsibility : IResponsibility
    {
        private ModelGroup modelGroup;

        private OBJ_Representation representation;

        public ModelResponsibility(ModelGroup modelGroup, OBJ_Representation representation) 
        {
            this.modelGroup = modelGroup;
            this.representation = representation;
        }
        public string MyResponsibility()
        {
           return representation.ModelID;
        }

        public void ReleaseResponsibility()
        {
            modelGroup.RemoveModel(representation);
        }
    }

    public class MatTexGroupResponsibility : IResponsibility 
    {
        private ModelGroup modelGroup;

        private MatTexGroup_Representation representation;

        public MatTexGroupResponsibility(ModelGroup modelGroup, MatTexGroup_Representation representation)
        {
            this.modelGroup = modelGroup;
            this.representation = representation;
        }
        public string MyResponsibility()
        {
            return representation.MatTexGroupName;
        }

        public void ReleaseResponsibility()
        {
            modelGroup.RemoveMatTexGroup(representation);
        }
    }

    public class MaterialGroupResponsibility : IResponsibility
    {
        private ModelGroup modelGroup;

        private MaterialGroup_Representation representation;

        public MaterialGroupResponsibility(ModelGroup modelGroup, MaterialGroup_Representation representation)
        {
            this.modelGroup = modelGroup;
            this.representation = representation;
        }
        public string MyResponsibility()
        {
            return representation.MaterialGroupName;
        }

        public void ReleaseResponsibility()
        {
            modelGroup.RemoveMaterialGroup(representation);
        }
    }

    public class TextureGroupResponsibility : IResponsibility
    {
        private ModelGroup modelGroup;

        private TEX_Representation representation;

        public TextureGroupResponsibility(ModelGroup modelGroup, TEX_Representation representation)
        {
            this.modelGroup = modelGroup;
            this.representation = representation;
        }
        public string MyResponsibility()
        {
            return representation.TextureGroupName;
        }

        public void ReleaseResponsibility()
        {
            modelGroup.RemoveTextureRef(representation);
        }
    }

    public class ScenarioSmdResponsibility : IResponsibility
    {
        private ModelGroup modelGroup;

        private SMD_Representation representation;

        public ScenarioSmdResponsibility(ModelGroup modelGroup, SMD_Representation representation)
        {
            this.modelGroup = modelGroup;
            this.representation = representation;
        }
        public string MyResponsibility()
        {
            return representation.SmdName;
        }

        public void ReleaseResponsibility()
        {
            modelGroup.RemoveSmdGroup();
        }
    }

    public class SmxResponsibility : IResponsibility
    {
        private ModelGroup modelGroup;

        private SMX_Representation representation;

        public SmxResponsibility(ModelGroup modelGroup, SMX_Representation representation)
        {
            this.modelGroup = modelGroup;
            this.representation = representation;
        }
        public string MyResponsibility()
        {
            return representation.SmxName;
        }

        public void ReleaseResponsibility()
        {
            modelGroup.RemoveSmxGroup();
        }
    }


}
