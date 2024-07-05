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
using JADERLINK_MODEL_VIEWER.src.Nodes;

namespace RE4_2007_MODEL_VIEWER.src
{
    public class LoadPmdModel
    {
        public Action<TreatedModel> ExternalAddTreatedModel;

        private ModelGroup modelGroup;
        private System.Text.RegularExpressions.Regex regex;
        private ModelNodeGroup mng;
        private TexturePackNodeGroup tpng;

        public LoadPmdModel(ModelGroup modelGroup, ModelNodeGroup mng, TexturePackNodeGroup tpng) 
        {
            this.modelGroup = modelGroup;
            this.mng = mng;
            this.tpng = tpng;
            string pattern = "^(R)([0-9|A-F]{1,3})(_)([0-9]{1,3})(.PMD).*$";
            regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.CultureInvariant);
        }

        public void LoadPMD(string PmdPath)
        {
            FileInfo fileInfo = new FileInfo(PmdPath);
            string PmdDiretory = fileInfo.DirectoryName + "\\";
            string FileID = fileInfo.Name.ToUpperInvariant();
            string FileName = FileID;

            bool isScenarioPmd = regex.IsMatch(FileID);
            int BIN_ID = -1;

            if (isScenarioPmd)
            {
                try
                {
                    string nun = FileID.Split('_').Last().Split('.').First();
                    BIN_ID = int.Parse(nun, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                }
            }

            if (BIN_ID > -1)
            {
                FileID = "SCENARIO_PMD_" + BIN_ID.ToString("D3");
            }

            if (modelGroup.Objects.ContainsKey(FileID))
            {
                var node = mng.Nodes.Find(FileID, false).FirstOrDefault();
                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                node?.Remove();
            }

            if (!modelGroup.Objects.ContainsKey(FileID))
            {

                PMD pmd = null;

                try
                {
                    pmd = PmdDecoder.GetPMD(fileInfo.FullName);
                }
                catch (Exception)
                {
                }

                if (pmd != null)
                {
                    ResponsibilityContainer RContainer = new ResponsibilityContainer();
                    OBJ_Representation obj_representation = new OBJ_Representation(FileID);
                    ModelResponsibility modelResponsibility = new ModelResponsibility(modelGroup, obj_representation);
                    RContainer.Add(modelResponsibility);
                    MatTexGroup_Representation matTexGroup_Representation = new MatTexGroup_Representation(FileID);
                    MatTexGroupResponsibility matTexGroupResponsibility = new MatTexGroupResponsibility(modelGroup, matTexGroup_Representation);
                    RContainer.Add(matTexGroupResponsibility);
                    MaterialGroup_Representation materialGroup_Representation = new MaterialGroup_Representation(FileID);
                    MaterialGroupResponsibility materialGroupResponsibility = new MaterialGroupResponsibility(modelGroup, materialGroup_Representation);
                    RContainer.Add(materialGroupResponsibility);

                    //--------
                    MatLinker matLinker = new MatLinker(FileID, FileID);
                    MatTexGroup matTexGroup = new MatTexGroup(FileID);
                    MaterialGroup materialGroup = new MaterialGroup(FileID);

                    TreatedModel treatedModel = new TreatedModel(FileID);
                    treatedModel.BIN_ID = BIN_ID;

                    PopulateTreatedModel(ref treatedModel, ref pmd, FileID, isScenarioPmd);

                    PopulateMaterialGroup(ref materialGroup, ref matTexGroup, ref pmd);

                    BoundaryCalculation.TreatedModel(ref treatedModel);

                    //armazena o modelo em local externo
                    ExternalAddTreatedModel?.Invoke(treatedModel);

                    //------------------
                    // adicionar o modelo para renderização
                    modelGroup.AddModel(treatedModel);
                    modelGroup.AddMatTexGroup(matTexGroup);
                    modelGroup.AddMaterialGroup(materialGroup);
                    modelGroup.MatLinkerDic[FileID] = matLinker;

                    //-----------------
                    //faz parte das texturas
                    LoadTGA loadTGA = new LoadTGA(modelGroup, tpng);
                    List<string> TexPathList = new List<string>();
                    foreach (var tex in matTexGroup.MatTexDic)
                    {
                        string TexPath = PmdDiretory + tex.Value.TextureName;
                        if (!TexPathList.Contains(TexPath))
                        {
                            TexPathList.Add(TexPath);
                        }
                    }
                    loadTGA.LoadTga(TexPathList.ToArray());
                    //-------------------------

                    //node
                    NodeModel nodeModel = new NodeModel();
                    nodeModel.Name = FileID;
                    nodeModel.SetFileID(FileID);
                    nodeModel.SetBlockModelHiding(treatedModel.BIN_ID != -1);
                    nodeModel.Text = FileName + (treatedModel.BIN_ID != -1 ? " | ID: " + treatedModel.BIN_ID.ToString("D3") : "");
                    nodeModel.Responsibility = RContainer;
                    mng.Nodes.Add(nodeModel);
                }

            }

        }

        private static void PopulateTreatedModel(ref TreatedModel treatedModel, ref PMD pmd, string FileID, bool isScenarioPmd) 
        {
            //mesh
            for (int iN = 0; iN < pmd.Nodes.Length; iN++)
            {
                for (int iM = 0; iM < pmd.Nodes[iN].Meshs.Length; iM++)
                {
                    if (pmd.Nodes[iN].Meshs[iM].Orders.Length > 0 && pmd.Nodes[iN].Meshs[iM].Vertexs.Length > 0)
                    {
                        int IndexesLength = pmd.Nodes[iN].Meshs[iM].Orders.Length;

                        Dictionary<Boundary, Vector3> boundary = new Dictionary<Boundary, Vector3>();

                        int tempIndexDiv = pmd.Nodes[iN].Meshs[iM].Orders.Length / (16 / sizeof(uint));
                        int tempIndexRest = pmd.Nodes[iN].Meshs[iM].Orders.Length % (16 / sizeof(uint));
                        tempIndexDiv += tempIndexRest != 0 ? 1 : 0;
                        int tempIndexLength = tempIndexDiv * (16 / sizeof(uint));

                        float[] vertices = new float[pmd.Nodes[iN].Meshs[iM].Vertexs.Length * 12];
                        uint[] indices = new uint[tempIndexLength];

                        for (int i = 0; i < IndexesLength; i++)
                        {
                            indices[i] = pmd.Nodes[iN].Meshs[iM].Orders[i];
                        }

                        int vOffset = 0;
                        for (int iv = 0; iv < pmd.Nodes[iN].Meshs[iM].Vertexs.Length; iv++)
                        {
                            var point = new Vector4(pmd.Nodes[iN].Meshs[iM].Vertexs[iv].x, pmd.Nodes[iN].Meshs[iM].Vertexs[iv].y, pmd.Nodes[iN].Meshs[iM].Vertexs[iv].z, 1);

                            if (isScenarioPmd)
                            {
                                point.X *= pmd.SkeletonBoneData[0][0];
                                point.Y *= pmd.SkeletonBoneData[0][1];
                                point.Z *= pmd.SkeletonBoneData[0][2];
                            }

                            vertices[vOffset + 0] = point.X;
                            vertices[vOffset + 1] = point.Y;
                            vertices[vOffset + 2] = point.Z;

                            vertices[vOffset + 3] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].nx;
                            vertices[vOffset + 4] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].ny;
                            vertices[vOffset + 5] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].nz;

                            vertices[vOffset + 6] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].tu;
                            vertices[vOffset + 7] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].tv;

                            vertices[vOffset + 8] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].r;
                            vertices[vOffset + 9] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].g;
                            vertices[vOffset + 10] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].b;
                            vertices[vOffset + 11] = pmd.Nodes[iN].Meshs[iM].Vertexs[iv].a;

                            vOffset += 12;

                            //seta para caso não tenha
                            if (!boundary.ContainsKey(Boundary.max))
                            {
                                boundary.Add(Boundary.max, point.Xyz);
                            }

                            if (!boundary.ContainsKey(Boundary.min))
                            {
                                boundary.Add(Boundary.min, point.Xyz);
                            }

                            // MAX
                            if (boundary[Boundary.max].X < point.X)
                            {
                                var p = boundary[Boundary.max];
                                p.X = point.X;
                                boundary[Boundary.max] = p;
                            }

                            if (boundary[Boundary.max].Y < point.Y)
                            {
                                var p = boundary[Boundary.max];
                                p.Y = point.Y;
                                boundary[Boundary.max] = p;
                            }

                            if (boundary[Boundary.max].Z < point.Z)
                            {
                                var p = boundary[Boundary.max];
                                p.Z = point.Z;
                                boundary[Boundary.max] = p;
                            }

                            //MIN
                            if (boundary[Boundary.min].X > point.X)
                            {
                                var p = boundary[Boundary.min];
                                p.X = point.X;
                                boundary[Boundary.min] = p;
                            }

                            if (boundary[Boundary.min].Y > point.Y)
                            {
                                var p = boundary[Boundary.min];
                                p.Y = point.Y;
                                boundary[Boundary.min] = p;
                            }

                            if (boundary[Boundary.min].Z > point.Z)
                            {
                                var p = boundary[Boundary.min];
                                p.Z = point.Z;
                                boundary[Boundary.min] = p;
                            }
                        }

                        Vector3 bmin = Vector3.Zero;
                        Vector3 bmax = Vector3.Zero;

                        if (boundary.ContainsKey(Boundary.max))
                        {
                            bmax = boundary[Boundary.max];
                        }

                        if (boundary.ContainsKey(Boundary.min))
                        {
                            bmin = boundary[Boundary.min];
                        }

                        MeshPart mesh = new MeshPart();
                        mesh.Vertex = vertices;
                        mesh.Indexes = indices;
                        mesh.IndexesLength = IndexesLength;
                        mesh.MinBoundary = bmin;
                        mesh.MaxBoundary = bmax;
                        mesh.CenterBoundary = new Vector3((bmax.X + bmin.X) / 2, (bmax.Y + bmin.Y) / 2, (bmax.Z + bmin.Z) / 2);
                        mesh.RefModelID = FileID;
                        mesh.MeshID = (iN + "||" + iM);
                        mesh.MaterialRef = pmd.Nodes[iN].Meshs[iM].TextureIndex.ToString();
                        treatedModel.Meshes.Add(mesh);
                    }
                }
            }

        }

        private static void PopulateMaterialGroup(ref MaterialGroup materialGroup, ref MatTexGroup matTexGroup, ref PMD pmd) 
        {
            //material
            for (int i = 0; i < pmd.Materials.Length; i++)
            {
                Material material = new Material(i.ToString());
                material.DiffuseMatTex = pmd.Materials[i].TextureName.ToLowerInvariant();
                material.AsAlphaTex = true;
                material.AlphaMatTex = pmd.Materials[i].TextureName.ToLowerInvariant();
                material.MatColor = new Vector4(pmd.Materials[i].TextureData[4], pmd.Materials[i].TextureData[5], pmd.Materials[i].TextureData[6], pmd.Materials[i].TextureData[7]);
                materialGroup.MaterialsDic.Add(material.MaterialName, material);

                MatTex matTex = new MatTex(material.DiffuseMatTex);
                matTex.TextureName = pmd.Materials[i].TextureName.ToLowerInvariant();

                if (!matTexGroup.MatTexDic.ContainsKey(matTex.MatTexName))
                {
                    matTexGroup.MatTexDic.Add(matTex.MatTexName, matTex);
                }
            }
        }

    }
}
