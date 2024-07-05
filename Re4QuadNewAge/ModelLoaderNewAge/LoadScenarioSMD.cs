using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ViewerBase;
using PMD_API;
using TGASharpLib;
using System.IO;
using OpenTK;
using RE4_2007_SCENARIO_SMD_EXTRACT;
using JADERLINK_MODEL_VIEWER.src.Nodes;

namespace RE4_2007_MODEL_VIEWER.src
{
    class LoadScenarioSMD
    {
        private ModelGroup modelGroup;
        private ScenarioNodeGroup sng;

        public LoadScenarioSMD(ModelGroup modelGroup, ScenarioNodeGroup sng)
        {
            this.modelGroup = modelGroup;
            this.sng = sng;
        }

        public void LoadScenario(string SmdPath)
        {
            FileInfo fileInfo = new FileInfo(SmdPath);

            SMDLine[] lines = new SMDLine[0];
            try
            {
                lines = SmdExtract.Extrator(SmdPath, out _);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error:");
            }

            if (lines.Length != 0)
            {
                string Nodekey = "SCENARIOSMD";

                var node = sng.Nodes.Find(Nodekey, false).FirstOrDefault();
                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                node?.Remove();

                string smdName = fileInfo.Name.ToUpperInvariant();
                SmdGroup smdGroup = new SmdGroup(smdName);

                for (int i = 0; i < lines.Length; i++)
                {
                    SmdEntry entry = new SmdEntry();
                    entry.SMD_ID = i;
                    entry.BIN_ID = i;
                    entry.SMX_ID = lines[i].SmxID;
                    PreFix fix = new PreFix();
                    fix.Angle = new Vector3(lines[i].angleX, lines[i].angleY, lines[i].angleZ);
                    fix.Position = new Vector3(lines[i].positionX / 100.0f, lines[i].positionY / 100.0f, lines[i].positionZ / 100.0f);
                    fix.Scale = new Vector3(10.0f);
                    entry.Fix = fix;
                    smdGroup.SmdEntries.Add(i, entry);
                }

                modelGroup.AddSmdGroup(smdGroup);

                ResponsibilityContainer RContainer = new ResponsibilityContainer();
                SMD_Representation smd_representation = new SMD_Representation(smdName);
                ScenarioSmdResponsibility smdResponsibility = new ScenarioSmdResponsibility(modelGroup, smd_representation);
                RContainer.Add(smdResponsibility);

                NodeScenario nodeScenario = new NodeScenario();
                nodeScenario.Name = Nodekey;
                nodeScenario.Text = smdName + " | Scenario SMD";
                nodeScenario.Responsibility = RContainer;
                sng.Nodes.Add(nodeScenario);
            }

        }



    }
}
