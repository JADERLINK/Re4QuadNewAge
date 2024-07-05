using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using System.IO;
using OpenTK;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using RE4_PS2_SCENARIO_SMD_TOOL.SCENARIO;
using RE4_PS2_BIN_TOOL.ALL;
using RE4_PS2_BIN_TOOL.EXTRACT;
using TPL_PS2_EXTRACT;
using System.Drawing;

namespace RE4_PS2_MODEL_VIEWER.src
{
    public class LoadPs2ScenarioSMD
    {
        public Action<TreatedModel> ExternalAddTreatedModel;

        private ModelGroup modelGroup;
        private ScenarioNodeGroup sng;

        public LoadPs2ScenarioSMD(ModelGroup modelGroup, ScenarioNodeGroup sng)
        {
            this.modelGroup = modelGroup;
            this.sng = sng;
        }

        public void LoadScenario(string SmdPath)
        {
            FileInfo fileInfo = new FileInfo(SmdPath);

            Dictionary<int, PS2BIN> uhdBinDic = null;
            int binAmount = 0;
            SMDLine[] SmdLines = null;
            TplExtras extras = new TplExtras();

            try
            {
                Ps2ScenarioExtract extract = new Ps2ScenarioExtract();
                extract.ToFileTpl += extras.ToFileTpl;
                SmdLines = extract.Extract(fileInfo, out uhdBinDic, out _, out binAmount);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error:");
            }

            if (SmdLines != null && SmdLines.Length != 0)
            {
                string Nodekey = "SCENARIOSMD";

                var node = sng.Nodes.Find(Nodekey, false).FirstOrDefault();
                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                node?.Remove();

                string smdName = fileInfo.Name.ToUpperInvariant();
                SmdGroup smdGroup = new SmdGroup(smdName);

                for (int i = 0; i < SmdLines.Length; i++)
                {
                    SmdEntry entry = new SmdEntry();
                    entry.SMD_ID = i;
                    entry.BIN_ID = SmdLines[i].BinID;
                    entry.SMX_ID = SmdLines[i].SmxID;
                    PreFix fix = new PreFix();
                    fix.Angle = new Vector3(SmdLines[i].angleX, SmdLines[i].angleY, SmdLines[i].angleZ);
                    fix.Position = new Vector3(SmdLines[i].positionX / 100.0f, SmdLines[i].positionY / 100.0f, SmdLines[i].positionZ / 100.0f);
                    fix.Scale = new Vector3(SmdLines[i].scaleX, SmdLines[i].scaleY, SmdLines[i].scaleZ);
                    entry.Fix = fix;
                    smdGroup.SmdEntries.Add(i, entry);
                }

                modelGroup.AddSmdGroup(smdGroup);

                ResponsibilityContainer RContainer = new ResponsibilityContainer();
                SMD_Representation smd_representation = new SMD_Representation(smdName);
                ScenarioSmdResponsibility smdResponsibility = new ScenarioSmdResponsibility(modelGroup, smd_representation);
                RContainer.Add(smdResponsibility);

                // TPL
                if (extras.TplCount != 0)
                {
                    MatTexGroup_Representation mat_representation = new MatTexGroup_Representation(TplExtras.ScenarioTPL);
                    MatTexGroupResponsibility matTexResponsibility = new MatTexGroupResponsibility(modelGroup, mat_representation);
                    RContainer.Add(matTexResponsibility);

                    MatTexGroup matTexGroup = new MatTexGroup(TplExtras.ScenarioTPL);
                    LoadPs2Tpl.PopulateMatTexGroup(ref matTexGroup, extras.TplCount, TplExtras.ScenarioTPL);
                    modelGroup.AddMatTexGroup(matTexGroup);

                    // texturas
                    foreach (var texId in extras.textureDic.Keys)
                    {
                        TEX_Representation tex_representation = new TEX_Representation(texId, new List<string>() { texId });
                        TextureGroupResponsibility textureGroupResponsibility = new TextureGroupResponsibility(modelGroup, tex_representation);
                        RContainer.Add(textureGroupResponsibility);
                    }

                    // GL das texturas
                    modelGroup.AddTextureRef(extras.textureDic);
                } 

                //== bins
                if (uhdBinDic != null)
                {
                    foreach (var item in uhdBinDic)
                    {
                        var binkey = Nodekey + "_" + item.Key.ToString();

                        OBJ_Representation obj_representation = new OBJ_Representation(binkey);
                        ModelResponsibility modelResponsibility = new ModelResponsibility(modelGroup, obj_representation);
                        RContainer.Add(modelResponsibility);
                        MaterialGroup_Representation materialGroup_Representation = new MaterialGroup_Representation(binkey);
                        MaterialGroupResponsibility materialGroupResponsibility = new MaterialGroupResponsibility(modelGroup, materialGroup_Representation);
                        RContainer.Add(materialGroupResponsibility);

                        MatLinker matLinker = new MatLinker(binkey, TplExtras.ScenarioTPL);
                        MaterialGroup materialGroup = new MaterialGroup(binkey);

                        TreatedModel treatedModel = new TreatedModel(binkey);
                        treatedModel.BIN_ID = item.Key;

                        var uhdBin = item.Value;

                        //adicina as vertices
                        LoadPs2BinModel.PopulateTreatedModel(ref treatedModel, ref uhdBin, binkey);

                        //materialGroup
                        LoadPs2BinModel.PopulateMaterialGroup(ref materialGroup, ref uhdBin);

                        // calculo Boundary do objeto.
                        BoundaryCalculation.TreatedModel(ref treatedModel);

                        //armazena o modelo em local externo
                        ExternalAddTreatedModel?.Invoke(treatedModel);

                        //------------------
                        // adicionar o modelo para renderização
                        modelGroup.AddModel(treatedModel);
                        modelGroup.AddMaterialGroup(materialGroup);
                        modelGroup.MatLinkerDic[binkey] = matLinker;
                    }
                }

                NodeScenario nodeScenario = new NodeScenario();
                nodeScenario.Name = Nodekey;
                nodeScenario.Text = smdName + " | Scenario SMD";
                nodeScenario.Responsibility = RContainer;
                sng.Nodes.Add(nodeScenario);
            }

        }
    
    
        private class TplExtras
        {
            public int TplCount { get; private set; }
            public Dictionary<string, Bitmap> textureDic { get; private set; }

            public const string ScenarioTPL = "SCENARIOTPL";

            public TplExtras() 
            {
              textureDic = new Dictionary<string, Bitmap>();
            }

            public void ToFileTpl(Stream fileStream, long tplOffset, long endOffset)
            {
                TplImageHeader[] Tihs = null;
                TplExtractHeaders Teh = null;
                BinaryReader br = null;

                try
                {
                    br = new BinaryReader(fileStream);
                    Teh = new TplExtractHeaders(br, tplOffset);
                    var Header = Teh.MainReader();
                    Tihs = Teh.Extract(Header.TplCount, Header.StartOffset);
                    TplCount = (int)Header.TplCount;
                }
                catch (Exception)
                {
                }

                if (Teh != null && Tihs != null && br != null)
                {

                    //texturas
                    Dictionary<string, Bitmap> itextureDic = new Dictionary<string, Bitmap>();
                    LoadPs2Tpl.GetImages(ref itextureDic, ref Tihs, ref br, tplOffset, ScenarioTPL, false);
                    textureDic = itextureDic;
                }

            }

        }    
    }

   


}
