using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using System.IO;
using OpenTK;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using RE4_PS2_BIN_TOOL.ALL;
using RE4_PS2_BIN_TOOL.EXTRACT;

namespace RE4_PS2_MODEL_VIEWER.src
{
    public class LoadPs2BinModel
    {
        public Action<TreatedModel> ExternalAddTreatedModel;

        private ModelGroup modelGroup;
        private ModelNodeGroup mng;

        public LoadPs2BinModel(ModelGroup modelGroup, ModelNodeGroup mng)
        {
            this.modelGroup = modelGroup;
            this.mng = mng;
        }

        public void LoadPs2BIN(string binPath)
        {
            FileInfo fileInfo = new FileInfo(binPath);
            string FileID = fileInfo.Name.ToUpperInvariant();

            if (modelGroup.Objects.ContainsKey(FileID))
            {
                var node = mng.Nodes.Find(FileID, false).FirstOrDefault();
                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                node?.Remove();
            }

            if (!modelGroup.Objects.ContainsKey(FileID))
            {
                PS2BIN ps2bin = null;
                Stream stream = null;

                try
                {
                    stream = fileInfo.OpenRead();
                    ps2bin = BINdecoder.Decode(stream, 0, out _);
                }
                catch (Exception)
                {
                }
                finally 
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }

                if (ps2bin != null)
                {

                    ResponsibilityContainer RContainer = new ResponsibilityContainer();
                    OBJ_Representation obj_representation = new OBJ_Representation(FileID);
                    ModelResponsibility modelResponsibility = new ModelResponsibility(modelGroup, obj_representation);
                    RContainer.Add(modelResponsibility);
                    MaterialGroup_Representation materialGroup_Representation = new MaterialGroup_Representation(FileID);
                    MaterialGroupResponsibility materialGroupResponsibility = new MaterialGroupResponsibility(modelGroup, materialGroup_Representation);
                    RContainer.Add(materialGroupResponsibility);

                    //--------
                    MatLinker matLinker = new MatLinker(FileID, FileID.Replace(".BIN", ".TPL"));
                    MaterialGroup materialGroup = new MaterialGroup(FileID);

                    TreatedModel treatedModel = new TreatedModel(FileID);
                    treatedModel.BIN_ID = -1;

                    //adicina as vertices
                    PopulateTreatedModel(ref treatedModel, ref ps2bin, FileID);

                    //materialGroup
                    PopulateMaterialGroup(ref materialGroup, ref ps2bin);

                    // calculo Boundary do objeto.
                    BoundaryCalculation.TreatedModel(ref treatedModel);

                    //armazena o modelo em local externo
                    ExternalAddTreatedModel?.Invoke(treatedModel);

                    //------------------
                    // adicionar o modelo para renderização
                    modelGroup.AddModel(treatedModel);
                    modelGroup.AddMaterialGroup(materialGroup);
                    modelGroup.MatLinkerDic[FileID] = matLinker;

                    //-------------------------

                    //node
                    NodeModel nodeModel = new NodeModel();
                    nodeModel.Name = FileID;
                    nodeModel.SetFileID(FileID);
                    nodeModel.SetBlockModelHiding(false);
                    nodeModel.Text = FileID;
                    nodeModel.Responsibility = RContainer;
                    mng.Nodes.Add(nodeModel);
                }

            }

        }


        public static void PopulateTreatedModel(ref TreatedModel treatedModel, ref PS2BIN ps2bin, string FileID)
        {

            for (int t = 0; t < ps2bin.Nodes.Length; t++) // cada node é um material
            {
                Dictionary<Boundary, Vector3> boundary = new Dictionary<Boundary, Vector3>();

                List<float> verticesList = new List<float>();
                List<uint> indicesList = new List<uint>();

                uint IndexCounter = 0;

                for (int s = 0; s < ps2bin.Nodes[t].Segments.Length; s++)
                {
                    for (int l = 0; l < ps2bin.Nodes[t].Segments[s].vertexLines.Length; l++)
                    {
                        VertexLine vertexLine = ps2bin.Nodes[t].Segments[s].vertexLines[l];

                        var point = new Vector4(
                             ((float)vertexLine.VerticeX * ps2bin.Nodes[t].Segments[s].ConversionFactorValue / 100f),
                             ((float)vertexLine.VerticeY * ps2bin.Nodes[t].Segments[s].ConversionFactorValue / 100f),
                             ((float)vertexLine.VerticeZ * ps2bin.Nodes[t].Segments[s].ConversionFactorValue / 100f), 
                            1);

                        var color = new Vector4(1, 1, 1, 1);

                        if (ps2bin.binType == BinType.ScenarioWithColors)
                        {
                            color = new Vector4(
                                ((float)vertexLine.NormalX / 128f),
                                ((float)vertexLine.NormalY / 128f),
                                ((float)vertexLine.NormalZ / 128f),
                                ((float)vertexLine.UnknownB / 128f)
                                );
                        }

                        var texture = new Vector2(
                            (vertexLine.TextureU / 255f),
                            (vertexLine.TextureV / 255f));

                        var normal = new Vector3(0, 0, 0);

                        if (ps2bin.binType != BinType.ScenarioWithColors)
                        {
                            float nx = vertexLine.NormalX;
                            float ny = vertexLine.NormalY;
                            float nz = vertexLine.NormalZ;

                            float NORMAL_FIX = (float)Math.Sqrt((nx * nx) + (ny * ny) + (nz * nz));
                            NORMAL_FIX = (NORMAL_FIX == 0) ? 1 : NORMAL_FIX;
                            nx /= NORMAL_FIX;
                            ny /= NORMAL_FIX;
                            nz /= NORMAL_FIX;

                            normal = new Vector3(nx, ny, nz);
                        }


                        verticesList.Add(point.X);
                        verticesList.Add(point.Y);
                        verticesList.Add(point.Z);

                        verticesList.Add(normal.X);
                        verticesList.Add(normal.Y);
                        verticesList.Add(normal.Z);

                        verticesList.Add(texture.X);
                        verticesList.Add(texture.Y);

                        verticesList.Add(color.X);
                        verticesList.Add(color.Y);
                        verticesList.Add(color.Z);
                        verticesList.Add(color.W);

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

                    bool invFace = false;
                    int counter = 0;
                    while (counter < ps2bin.Nodes[t].Segments[s].vertexLines.Length)
                    {
                        uint a = (IndexCounter - 2);
                        uint b = (IndexCounter - 1);
                        uint c = (IndexCounter);

                        if ((counter - 2) > -1 &&
                           (ps2bin.Nodes[t].Segments[s].vertexLines[counter].IndexComplement == 0)
                           )
                        {
                            if (invFace)
                            {
                                indicesList.Add(c);
                                indicesList.Add(b);
                                indicesList.Add(a);

                                invFace = false;
                            }
                            else
                            {
                                indicesList.Add(a);
                                indicesList.Add(b);
                                indicesList.Add(c);

                                invFace = true;
                            }


                        }
                        else
                        {
                            invFace = false;
                        }

                        counter++;
                        IndexCounter++;
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

                int IndexesLength = indicesList.Count;

                int tempIndexDiv = IndexesLength / (16 / sizeof(uint));
                int tempIndexRest = IndexesLength % (16 / sizeof(uint));
                tempIndexDiv += tempIndexRest != 0 ? 1 : 0;
                int tempIndexLength = tempIndexDiv * (16 / sizeof(uint));
                indicesList.AddRange(new uint[tempIndexLength - IndexesLength]);

                MeshPart mesh = new MeshPart();
                mesh.Vertex = verticesList.ToArray();
                mesh.Indexes = indicesList.ToArray();
                mesh.IndexesLength = IndexesLength;
                mesh.MinBoundary = bmin;
                mesh.MaxBoundary = bmax;
                mesh.CenterBoundary = new Vector3((bmax.X + bmin.X) / 2, (bmax.Y + bmin.Y) / 2, (bmax.Z + bmin.Z) / 2);
                mesh.RefModelID = FileID;
                mesh.MeshID = t.ToString();
                mesh.MaterialRef = t.ToString();
                treatedModel.Meshes.Add(mesh);

            }
        }

        public static void PopulateMaterialGroup(ref MaterialGroup materialGroup, ref PS2BIN ps2bin)
        {
            //material
            for (int i = 0; i < ps2bin.materials.Length; i++)
            {
                var ps2mat = new MaterialPart(ps2bin.materials[i].materialLine);

                ViewerBase.Material material = new ViewerBase.Material(i.ToString());
                material.DiffuseMatTex = ps2mat.diffuse_map.ToString();
                material.AsAlphaTex = true;
                material.AlphaMatTex = ps2mat.diffuse_map.ToString();
                material.MatColor = Vector4.One;
                materialGroup.MaterialsDic.Add(material.MaterialName, material);
            }

        }

    }
}
