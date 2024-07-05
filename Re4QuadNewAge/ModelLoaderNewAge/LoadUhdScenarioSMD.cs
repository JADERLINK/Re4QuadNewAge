using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using TGASharpLib;
using System.IO;
using OpenTK;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using RE4_UHD_SCENARIO_SMD_TOOL.SCENARIO;
using RE4_UHD_BIN_TOOL.ALL;
using RE4_UHD_BIN_TOOL.EXTRACT;

namespace RE4_UHD_MODEL_VIEWER.src
{
    public class LoadUhdScenarioSMD
    {
        public Action<TreatedModel> ExternalAddTreatedModel;

        private ModelGroup modelGroup;
        private ScenarioNodeGroup sng;

        public LoadUhdScenarioSMD(ModelGroup modelGroup, ScenarioNodeGroup sng)
        {
            this.modelGroup = modelGroup;
            this.sng = sng;
        }

        public void LoadScenario(string SmdPath)
        {
            FileInfo fileInfo = new FileInfo(SmdPath);

            Dictionary<int, UhdBIN> uhdBinDic = null;
            UhdTPL uhdTpl = null;
            SmdMagic smdMagic = null;
            int binAmount = 0;
            SMDLine[] SmdLines = null;

            try
            {
                UhdSmdExtract extract = new UhdSmdExtract();
                SmdLines = extract.Extract(fileInfo.OpenRead(), out uhdBinDic, out uhdTpl, out smdMagic, ref binAmount);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error:");
            }

            if (SmdLines != null && SmdLines.Length != 0)
            {
                string Nodekey = "SCENARIOSMD";

                string ScenarioTPL = "SCENARIOTPL";

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

                if (uhdTpl != null)
                {
                    MatTexGroup_Representation mat_representation = new MatTexGroup_Representation(ScenarioTPL);
                    MatTexGroupResponsibility matTexResponsibility = new MatTexGroupResponsibility(modelGroup, mat_representation);
                    RContainer.Add(matTexResponsibility);

                    MatTexGroup matTexGroup = new MatTexGroup(ScenarioTPL);

                    LoadUhdTpl.PopulateMatTexGroup(ref matTexGroup, ref uhdTpl);

                    modelGroup.AddMatTexGroup(matTexGroup);
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

                        MatLinker matLinker = new MatLinker(binkey, ScenarioTPL);
                        MaterialGroup materialGroup = new MaterialGroup(binkey);

                        TreatedModel treatedModel = new TreatedModel(binkey);
                        treatedModel.BIN_ID = item.Key;

                        var uhdBin = item.Value;

                        //adicina as vertices
                        LoadUhdBinModel.PopulateTreatedModel(ref treatedModel, ref uhdBin, binkey);

                        //materialGroup
                        LoadUhdBinModel.PopulateMaterialGroup(ref materialGroup, ref uhdBin);

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
    
    
    }
}
