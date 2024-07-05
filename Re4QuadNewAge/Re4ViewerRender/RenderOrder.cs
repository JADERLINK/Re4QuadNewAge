using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using ViewerBase;

namespace Re4ViewerRender
{
    public class RenderOrder
    {
        public List<OrderObj> MeshOrder { get; private set; }

        public RenderOrder() 
        {
            MeshOrder = new List<OrderObj>();
        }

        public void ToOrder(ref ModelGroup modelGroup, List<string> NodeOrder) 
        {
            MeshOrder.Clear();

            //lista de objetos a serem renderizados
            List<OrderObj> temp = new List<OrderObj>();

            var allMeshName = modelGroup.MeshParts.Keys.ToList();

            //smd
            List<(string ObjName, SmdEntry smd, SmxEntry smx)> smdOrder = new List<(string ObjName, SmdEntry smd, SmxEntry smx)>();
            List<(string ObjName, SmdEntry smd, SmxEntry smx)> smdSpecial = new List<(string ObjName, SmdEntry smd, SmxEntry smx)>();
            List<(string ObjName, SmdEntry smd, SmxEntry smx)> smdTemp = new List<(string ObjName, SmdEntry smd, SmxEntry smx)>();

            if (modelGroup.SmdGroup != null)
            {
                foreach (var smdPair in modelGroup.SmdGroup.SmdEntries)
                {
                    SmdEntry smdEntry = smdPair.Value;
                    SmxEntry smxEntry = new SmxEntry();
                    smxEntry.OpacityHierarchy = 0x00;
                    smxEntry.AlphaHierarchy = 0x00;
                    smxEntry.SmxColor = Vector4.One;
                    smxEntry.SMX_ID = smdEntry.SMX_ID;
                    smxEntry.FaceCulling = SmxFaceCulling.OnlyFront;

                    if (modelGroup.SmxGroup != null && modelGroup.SmxGroup.SmxEntries.ContainsKey(smdEntry.SMX_ID))
                    {
                        smxEntry = modelGroup.SmxGroup.SmxEntries[smdEntry.SMX_ID];
                    }

                    int BIN_ID = smdEntry.BIN_ID;
                    if (modelGroup.ScenarioBinList.ContainsKey(BIN_ID))
                    {
                        var modelID = modelGroup.ScenarioBinList[BIN_ID];

                        if (smxEntry.AlphaHierarchy >= 0x40)
                        {
                            smdSpecial.Add((modelID, smdEntry, smxEntry));
                        }
                        else 
                        {
                            smdOrder.Add((modelID, smdEntry, smxEntry));
                        }
                    }

                }

                smdSpecial = smdSpecial.OrderByDescending(x => x.smd.SMX_ID).OrderByDescending(x => x.smx.AlphaHierarchy).OrderBy(x => x.smx.OpacityHierarchy).ToList();
                smdOrder = smdOrder.OrderByDescending(x => x.smd.SMX_ID).OrderByDescending(x => x.smd.SMD_ID).OrderBy(x => x.smx.OpacityHierarchy).ToList();
                smdTemp.AddRange(smdSpecial);// so renderiza se estiver atras de uma textura com transparencia, necessita de mais estudos.
                smdTemp.AddRange(smdOrder);


                foreach (var item in smdTemp)
                {
                    var model = modelGroup.Objects[item.ObjName];

                    for (int iM = 0; iM < model.MeshNames.Count; iM++)
                    {
                        string meshName = model.MeshNames[iM];
                        temp.Add(new OrderObj(meshName, item.smd, item.smx));

                        allMeshName.Remove(meshName);
                    }
                }

                // remove os que sobraram
                foreach (var item in modelGroup.ScenarioBinList)
                {
                    var model = modelGroup.Objects[item.Value];

                    for (int iM = 0; iM < model.MeshNames.Count; iM++)
                    {
                        string meshName = model.MeshNames[iM];
                        allMeshName.Remove(meshName);
                    }
                }
            
            }


            PreFix none = new PreFix();
            none.Position = Vector3.Zero;
            none.Angle = Vector3.Zero;
            none.Scale = Vector3.One;

            SmdEntry smdEmpty = new SmdEntry();
            smdEmpty.Fix = none;
            smdEmpty.SMD_ID = int.MaxValue;
            smdEmpty.SMX_ID = int.MaxValue;
            smdEmpty.BIN_ID = int.MaxValue;
            SmxEntry smxEmpty = new SmxEntry();
            smxEmpty.OpacityHierarchy = int.MaxValue;
            smxEmpty.AlphaHierarchy = int.MaxValue;
            smxEmpty.SmxColor = Vector4.One;
            smxEmpty.SMX_ID = int.MaxValue;
            smxEmpty.FaceCulling = SmxFaceCulling.OnlyFront;

            foreach (var FileID in NodeOrder)
            {
                if (modelGroup.Objects.ContainsKey(FileID))
                {
                    foreach (var meshName in modelGroup.Objects[FileID].MeshNames)
                    {
                        if (allMeshName.Contains(meshName))
                        {
                            temp.Add(new OrderObj(meshName, smdEmpty, smxEmpty));
                            allMeshName.Remove(meshName);
                        }


                    }
 
                }
            }

            //ordenação
            MeshOrder = temp;


            //test
            //foreach (var item in temp)
            //{
            //    Console.WriteLine(item.mesh + " " + item.smdEntry.SMD_ID + " " + item.smdEntry.SMX_ID + " " + item.smxEntry.SMX_ID + " " + item.smxEntry.AlphaHierarchy + " " + item.smxEntry.OpacityHierarchy);
            //}
            //Console.WriteLine("----------------------------");
        }

    }

    public struct OrderObj
    {
        public string mesh;
        public SmxEntry smxEntry;
        public SmdEntry smdEntry;

        public OrderObj(string mesh, SmdEntry smdEntry, SmxEntry smxEntry) 
        {
            this.smdEntry = smdEntry;
            this.smxEntry = smxEntry;
            this.mesh = mesh;
        }
    }

}
