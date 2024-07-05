using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using System.IO;
using RE4_SMX_TOOL;
using JADERLINK_MODEL_VIEWER.src.Nodes;

namespace JADERLINK_MODEL_VIEWER.src
{
    class LoadSMX
    {
        private ModelGroup modelGroup;
        private ScenarioNodeGroup sng;

        public LoadSMX(ModelGroup modelGroup, ScenarioNodeGroup sng)
        {
            this.modelGroup = modelGroup;
            this.sng = sng;
        }

        public void LoadSmx(string SmxPath, bool isPS2)
        {
            List<SMX> smxList = new List<SMX>();
            FileInfo fileInfo = new FileInfo(SmxPath);

            try
            {                
                var stream = fileInfo.OpenRead();
                var lines = SMXextract.extract(stream);
                stream.Close();
                smxList = SMXextract.ToSmx(lines, isPS2);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error:");
            }

            if (smxList.Count != 0)
            {
                string Nodekey = "SMXFILE";

                var node = sng.Nodes.Find(Nodekey, false).FirstOrDefault();
                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                node?.Remove();

                string smxName = fileInfo.Name.ToUpperInvariant();
                SmxGroup smxGroup = new SmxGroup(smxName);

                foreach (var smx in smxList)
                {
                    SmxEntry entry = new SmxEntry();
                    entry.SMX_ID = smx.UseSMXID;
                    entry.OpacityHierarchy = smx.OpacityHierarchy;
                    entry.AlphaHierarchy = smx.AlphaHierarchy;
                    entry.SmxColor = new OpenTK.Vector4(smx.ColorRGB[0] / 255f, smx.ColorRGB[1] / 255f, smx.ColorRGB[2] / 255f, 1f);
                    if (entry.SmxColor.X == 0 && entry.SmxColor.Y == 0 && entry.SmxColor.Z == 0)
                    {
                        entry.SmxColor = new OpenTK.Vector4(1f, 1f, 1f, 1f);
                    }

                    entry.FaceCulling = SmxFaceCulling.OnlyFront;
                    if (smx.FaceCulling == 0x01)
                    {
                        entry.FaceCulling = SmxFaceCulling.OnlyBack;
                    }
                    else if (smx.FaceCulling == 0x02)
                    {
                        entry.FaceCulling = SmxFaceCulling.FrontAndBack;
                    }

                    if (!smxGroup.SmxEntries.ContainsKey(smx.UseSMXID))
                    {
                        smxGroup.SmxEntries.Add(smx.UseSMXID, entry);
                    }
                }

                modelGroup.AddSmxGroup(smxGroup);

                ResponsibilityContainer RContainer = new ResponsibilityContainer();
                SMX_Representation smx_representation = new SMX_Representation(smxName);
                SmxResponsibility smxResponsibility = new SmxResponsibility(modelGroup, smx_representation);
                RContainer.Add(smxResponsibility);

                NodeScenario nodeScenario = new NodeScenario();
                nodeScenario.Name = Nodekey;
                nodeScenario.Text = smxName;
                nodeScenario.Responsibility = RContainer;
                sng.Nodes.Add(nodeScenario);
            }


        }
    }
}
