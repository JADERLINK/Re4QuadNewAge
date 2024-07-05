using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using System.IO;
using OpenTK;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using RE4_UHD_BIN_TOOL.EXTRACT;
using RE4_UHD_BIN_TOOL.ALL;

namespace RE4_UHD_MODEL_VIEWER.src
{
    public class LoadUhdTpl
    {
        private ModelGroup modelGroup;
        private ModelNodeGroup mng;

        public LoadUhdTpl(ModelGroup modelGroup, ModelNodeGroup mng)
        {
            this.modelGroup = modelGroup;
            this.mng = mng;
        }

        public void LoadUhdTPL(string tplPath)
        {
            FileInfo fileInfo = new FileInfo(tplPath);
            string FileID = fileInfo.Name.ToUpperInvariant();

            if (modelGroup.MatTexGroupDic.ContainsKey(FileID))
            {
                var node = mng.Nodes.Find(FileID, false).FirstOrDefault();
                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                node?.Remove();
            }

            if (!modelGroup.MatTexGroupDic.ContainsKey(FileID))
            {
                UhdTPL uhdTPL = null;

                try
                {
                    Stream tplFile = fileInfo.OpenRead();
                    uhdTPL = UhdTplDecoder.Decoder(tplFile, 0, out _);
                    tplFile.Close();
                }
                catch (Exception)
                {
                }

                if (uhdTPL != null)
                {
                    ResponsibilityContainer RContainer = new ResponsibilityContainer();
                    MatTexGroup_Representation matTexGroup_Representation = new MatTexGroup_Representation(FileID);
                    MatTexGroupResponsibility matTexGroupResponsibility = new MatTexGroupResponsibility(modelGroup, matTexGroup_Representation);
                    RContainer.Add(matTexGroupResponsibility);

                    MatTexGroup matTexGroup = new MatTexGroup(FileID);

                    PopulateMatTexGroup(ref matTexGroup, ref uhdTPL);

                    modelGroup.AddMatTexGroup(matTexGroup);

                    //node
                    NodeMatRef nodeMatRef = new NodeMatRef();
                    nodeMatRef.Name = FileID;
                    nodeMatRef.SetFileID(FileID);
                    nodeMatRef.Text = FileID;
                    nodeMatRef.Responsibility = RContainer;
                    mng.Nodes.Add(nodeMatRef);
                }


            }



        }

        public static void PopulateMatTexGroup(ref MatTexGroup matTexGroup, ref UhdTPL uhdTPL) 
        {
            for (int i = 0; i < uhdTPL.TplArray.Length; i++)
            {
                MatTex matTex = new MatTex(i.ToString());
                matTex.TextureName = uhdTPL.TplArray[i].PackID.ToString("X8") + "/" + uhdTPL.TplArray[i].TextureID.ToString("D4");
                matTexGroup.MatTexDic.Add(matTex.MatTexName, matTex);
            }

        }

    }
}
