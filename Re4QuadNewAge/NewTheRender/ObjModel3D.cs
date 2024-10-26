using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.JSON;
using Re4QuadExtremeEditor.src;
using Re4QuadExtremeEditor.src.Class.Shaders;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using ViewerBase;

namespace NewAgeTheRender
{
    public class ObjModel3D
    {
        public static bool RenderTextures = true;
        public static bool RenderWireframe = false;
        public static bool RenderNormals = false;
        public static bool RenderOnlyFrontFace = false;
        public static bool RenderVertexColor = true;
        public static bool RenderAlphaChannel = true;
        public static bool LoadTextureLinear = true;

        private BoundingBoxLimit boundingBox;
        private Dictionary<string, PreFix> preFixs;
        private string[] ObjNameOrder;
        private Dictionary<string, SimpleTexRef> meshTexRef;
        private ModelGroup modelGroup;
        private ModelNodeGroup mng;
        private TexturePackNodeGroup tpng;

        public ObjModel3D(ModelGroup modelGroup, ModelNodeGroup mng, TexturePackNodeGroup tpng)
        {
            preFixs = new Dictionary<string, PreFix>();
            meshTexRef = new Dictionary<string, SimpleTexRef>();
            ObjNameOrder = new string[0];
            boundingBox = new BoundingBoxLimit(Vector3.Zero, Vector3.Zero);

            this.modelGroup = modelGroup;
            this.mng = mng;
            this.tpng = tpng;
        }

        /// <summary>
        /// carrega obj model
        /// </summary>
        /// <param name="jsonFile">caminho completo para o arquivo json</param>
        public string LoadModelObject(string jsonFile)
        {
            ObjectModel objModel = null;
            try
            {
                objModel = ObjectModelFile.ParseFromFile(jsonFile);
            }
            catch (Exception)
            {
            }

            if (objModel is ObjectModel2007 objmodel2007)
            {
                Load2007(objmodel2007);
            }
            else if (objModel is ObjectModelUhd objmodelUhd)
            {
                LoadUHD(objmodelUhd);
            }
            else if (objModel is ObjectModelPs2 objmodelPs2)
            {
                LoadPS2(objmodelPs2);
            }

            CalculateBoundingBoxLimit();

            return ObjNameOrder.Length > 0 ? objModel?.JsonFileName : null;
        }

        private void Load2007(ObjectModel2007 objmodel2007)
        {
            string BaseDirectory = "";
            if (DataBase.DirectoryDic.ContainsKey(objmodel2007.PathKey))
            {
                BaseDirectory = DataBase.DirectoryDic[objmodel2007.PathKey];
            }

            List<string> pmdOrder = new List<string>();

            foreach (var sub in objmodel2007.List)
            {
                string modelFullPath = BaseDirectory + sub.PmdFile;
                FileInfo fileInfo = new FileInfo(modelFullPath);
                string FileID = fileInfo.Name.ToUpperInvariant();

                pmdOrder.Add(FileID);

                if (File.Exists(modelFullPath))
                {
                    RE4_2007_MODEL_VIEWER.src.LoadPmdModel loadPmdModel = new RE4_2007_MODEL_VIEWER.src.LoadPmdModel(modelGroup, mng, tpng);
                    loadPmdModel.LoadPMD(modelFullPath);
                    if (!preFixs.ContainsKey(FileID))
                    {
                        preFixs.Add(FileID, sub.PreFix);
                    }
                }             
            }

            List<string> ObjNameOrderList = new List<string>();

            foreach (var FileID in pmdOrder)
            {
                if (modelGroup.Objects.ContainsKey(FileID))
                {
                    ObjNameOrderList.Add(FileID);

                    foreach (var MeshName in modelGroup.Objects[FileID].MeshNames)
                    {
                        meshTexRef.Add(MeshName, new SimpleTexRef(MeshName, modelGroup));
                    }
                }
            }

            ObjNameOrder = ObjNameOrderList.ToArray();
            GC.Collect();
        }

        private void LoadUHD(ObjectModelUhd objmodelUhd)
        {
            bool IsPS4NS = objmodelUhd.Type == ObjectModel.EType.PS4NS;

            string BaseDirectory = "";
            if (DataBase.DirectoryDic.ContainsKey(objmodelUhd.PathKey))
            {
                BaseDirectory = DataBase.DirectoryDic[objmodelUhd.PathKey];
            }

            List<(string FileID_BIN, string FileID_TPL)> binTplOrder = new List<(string FileID_BIN, string FileID_TPL)>();

            foreach (var sub in objmodelUhd.List)
            {
                string modelFullPath = BaseDirectory + sub.UhdBinFile;
                FileInfo fileInfoBIN = new FileInfo(modelFullPath);
                string FileID_BIN = fileInfoBIN.Name.ToUpperInvariant();

                string tplFullPath = BaseDirectory + sub.UhdTplFile;
                FileInfo fileInfoTPL = new FileInfo(tplFullPath);
                string FileID_TPL = fileInfoTPL.Name.ToUpperInvariant();

                binTplOrder.Add((FileID_BIN, FileID_TPL));

                if (File.Exists(modelFullPath))
                {
                    LoadUhdPs4Ns.src.LoadUhdBinModel loadUhdBinModel = new LoadUhdPs4Ns.src.LoadUhdBinModel(modelGroup, mng, IsPS4NS);
                    loadUhdBinModel.LoadUhdBIN(modelFullPath);
                    if (!preFixs.ContainsKey(FileID_BIN))
                    {
                        preFixs.Add(FileID_BIN, sub.PreFix);
                    }
                }

                if (File.Exists(tplFullPath))
                {
                    LoadUhdPs4Ns.src.LoadUhdTpl loadUhdTpl = new LoadUhdPs4Ns.src.LoadUhdTpl(modelGroup, mng, IsPS4NS);
                    loadUhdTpl.LoadUhdTPL(tplFullPath);
                }

            }

            //associa o TPL com o BIN
            foreach (var item in binTplOrder)
            {
                if (modelGroup.MatLinkerDic.ContainsKey(item.FileID_BIN))
                {
                    modelGroup.MatLinkerDic[item.FileID_BIN].MatTexGroupName = item.FileID_TPL;
                }  
            }

            List<string> ObjNameOrderList = new List<string>();

            foreach (var item in binTplOrder)
            {
                if (modelGroup.Objects.ContainsKey(item.FileID_BIN))
                {
                    ObjNameOrderList.Add(item.FileID_BIN);

                    foreach (var MeshName in modelGroup.Objects[item.FileID_BIN].MeshNames)
                    {
                        meshTexRef.Add(MeshName, new SimpleTexRef(MeshName, modelGroup));
                    }
                }
            }

            ObjNameOrder = ObjNameOrderList.ToArray();

            //texturas
            HashSet<string> packIds = new HashSet<string>();
            Dictionary<string, List<int>> packId_texId = new Dictionary<string, List<int>>();

            foreach (var item in binTplOrder)
            {
                if (modelGroup.MatTexGroupDic.ContainsKey(item.FileID_TPL))
                {
                    foreach (var i2 in modelGroup.MatTexGroupDic[item.FileID_TPL].MatTexDic.Values)
                    {
                        var split = i2.TextureName.Split('/');
                        string packID = split[0];
                        packIds.Add(packID);

                        int texId = int.Parse(split[1],System.Globalization.NumberStyles.Integer , System.Globalization.CultureInfo.InvariantCulture);
                        if (packId_texId.ContainsKey(packID))
                        {
                            packId_texId[packID].Add(texId);
                        }
                        else 
                        {
                            packId_texId.Add(packID,new List<int> { texId });
                        }
                    }
                }
            }

            string PackBaseDirectory = "";
            if (DataBase.DirectoryDic.ContainsKey(objmodelUhd.PackPathKey))
            {
                PackBaseDirectory = DataBase.DirectoryDic[objmodelUhd.PackPathKey];
            }

            if (IsPS4NS == false) // UHD
            {
                RE4_UHD_MODEL_VIEWER.src.LoadUhdPackCustomQuad loadUhdPack = new RE4_UHD_MODEL_VIEWER.src.LoadUhdPackCustomQuad(modelGroup, tpng);
                foreach (var item in packIds)
                {
                    var Packyz2Filepath = Path.Combine(PackBaseDirectory, objmodelUhd.PackFolder, item + ".pack.yz2");
                    var PackFilepath = Path.Combine(PackBaseDirectory, objmodelUhd.PackFolder, item + ".pack");

                    if (File.Exists(Packyz2Filepath))
                    {
                        loadUhdPack.LoadPack(Packyz2Filepath, packId_texId[item].ToArray());
                    }
                    else if (File.Exists(PackFilepath))
                    {
                        loadUhdPack.LoadPack(PackFilepath, packId_texId[item].ToArray());
                    }
                }
            }
            else // PS4NS
            {
                RE4_PS4NS_MODEL_VIEWER.src.LoadPs4NsPackCustomQuad loadPack = new RE4_PS4NS_MODEL_VIEWER.src.LoadPs4NsPackCustomQuad(modelGroup, tpng);
                foreach (var item in packIds)
                {
                    var PackFilepath = Path.Combine(PackBaseDirectory, objmodelUhd.PackFolder, item + ".pack");
                    if (File.Exists(PackFilepath))
                    {
                        loadPack.LoadPack(PackFilepath, packId_texId[item].ToArray());
                    }
                }
            }
            GC.Collect();
        }

        private void LoadPS2(ObjectModelPs2 objmodelPs2)
        {
            string BaseDirectory = "";
            if (DataBase.DirectoryDic.ContainsKey(objmodelPs2.PathKey))
            {
                BaseDirectory = DataBase.DirectoryDic[objmodelPs2.PathKey];
            }

            List<(string FileID_BIN, string FileID_TPL)> binTplOrder = new List<(string FileID_BIN, string FileID_TPL)>();

            foreach (var sub in objmodelPs2.List)
            {
                string modelFullPath = BaseDirectory + sub.Ps2BinFile;
                FileInfo fileInfoBIN = new FileInfo(modelFullPath);
                string FileID_BIN = fileInfoBIN.Name.ToUpperInvariant();

                string tplFullPath = BaseDirectory + sub.Ps2TplFile;
                FileInfo fileInfoTPL = new FileInfo(tplFullPath);
                string FileID_TPL = fileInfoTPL.Name.ToUpperInvariant();

                binTplOrder.Add((FileID_BIN, FileID_TPL));

                if (File.Exists(modelFullPath))
                {
                    RE4_PS2_MODEL_VIEWER.src.LoadPs2BinModel loadUhdBinModel = new RE4_PS2_MODEL_VIEWER.src.LoadPs2BinModel(modelGroup, mng);
                    loadUhdBinModel.LoadPs2BIN(modelFullPath);
                    if (!preFixs.ContainsKey(FileID_BIN))
                    {
                        preFixs.Add(FileID_BIN, sub.PreFix);
                    }
                }

                if (File.Exists(tplFullPath))
                {
                    RE4_PS2_MODEL_VIEWER.src.LoadPs2Tpl loadUhdTpl = new RE4_PS2_MODEL_VIEWER.src.LoadPs2Tpl(modelGroup, mng);
                    loadUhdTpl.LoadPs2TPL(tplFullPath);

                }
            }

            //associa o TPL com o BIN
            foreach (var item in binTplOrder)
            {
                if (modelGroup.MatLinkerDic.ContainsKey(item.FileID_BIN))
                {
                    modelGroup.MatLinkerDic[item.FileID_BIN].MatTexGroupName = item.FileID_TPL;
                }
            }


            List<string> ObjNameOrderList = new List<string>();

            foreach (var item in binTplOrder)
            {
                if (modelGroup.Objects.ContainsKey(item.FileID_BIN))
                {
                    ObjNameOrderList.Add(item.FileID_BIN);

                    foreach (var MeshName in modelGroup.Objects[item.FileID_BIN].MeshNames)
                    {
                        meshTexRef.Add(MeshName, new SimpleTexRef(MeshName, modelGroup));
                    }
                }
            }


            ObjNameOrder = ObjNameOrderList.ToArray();
            GC.Collect();
        }

        public static void PreRender() 
        {
            if (RenderWireframe)
            {
                GL.Disable(EnableCap.CullFace);
                GL.CullFace(CullFaceMode.FrontAndBack);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }
            else
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

                if (RenderOnlyFrontFace)
                {
                    GL.Enable(EnableCap.CullFace);
                    GL.CullFace(CullFaceMode.Front);
                }
                else
                {
                    GL.CullFace(CullFaceMode.FrontAndBack);
                    GL.Disable(EnableCap.CullFace);
                }
            }

            GL.FrontFace(FrontFaceDirection.Cw);

            DataShader.ShaderObjModel.Use();

            if (RenderNormals)
            {
                DataShader.ShaderObjModel.SetInt("EnableNormals", 1);
            }
            else
            {
                DataShader.ShaderObjModel.SetInt("EnableNormals", 0);
            }

            if (RenderVertexColor)
            {
                DataShader.ShaderObjModel.SetInt("EnableVertexColors", 1);
            }
            else
            {
                DataShader.ShaderObjModel.SetInt("EnableVertexColors", 0);
            }

            if (RenderAlphaChannel)
            {
                DataShader.ShaderObjModel.SetInt("EnableAlphaChannel", 1);
            }
            else
            {
                DataShader.ShaderObjModel.SetInt("EnableAlphaChannel", 0);
            }

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.001f);
            GL.Enable(EnableCap.Texture2D);
        }

        public void Render(RspFix obj) 
        {
            DataShader.ShaderObjModel.Use();
            DataShader.ShaderObjModel.SetVector4("matColor", Vector4.One);

            DataShader.WhiteTexture.Use(TextureUnit.Texture0);
            DataShader.WhiteTexture.Use(TextureUnit.Texture1);

            DataShader.ShaderObjModel.SetMatrix4("RspFixRotation", obj.Rotation);
            DataShader.ShaderObjModel.SetVector3("RspFixScale", obj.Scale);
            DataShader.ShaderObjModel.SetVector3("RspFixPosition", obj.Position);

            //lista de modelos
            foreach (var modelID in ObjNameOrder)
            {
                //modelo
                if (preFixs.ContainsKey(modelID))
                {
                    DataShader.ShaderObjModel.SetMatrix4("mRotation", preFixs[modelID].GetRotation());
                    DataShader.ShaderObjModel.SetVector3("mScale", preFixs[modelID].Scale);
                    DataShader.ShaderObjModel.SetVector3("mPosition", preFixs[modelID].Position);
                }
                else
                {
                    DataShader.ShaderObjModel.SetMatrix4("mRotation", Matrix4.Identity);
                    DataShader.ShaderObjModel.SetVector3("mScale", Vector3.One);
                    DataShader.ShaderObjModel.SetVector3("mPosition", Vector3.Zero);
                }

                foreach (var item in modelGroup.Objects[modelID].MeshNames)
                {
                    //chamadas da textura
                    var mesh = modelGroup.MeshParts[item];
                    var material = modelGroup.MaterialGroupDic[modelID].MaterialsDic[mesh.MaterialRef];

                    if (RenderTextures)
                    {
                        DataShader.ShaderObjModel.SetVector4("matColor", material.MatColor);
                        DataShader.WhiteTexture.Use(TextureUnit.Texture0);
                        DataShader.WhiteTexture.Use(TextureUnit.Texture1);
                        meshTexRef[item]?.SetTex();
                    }

                    mesh.Render();
                }
            }

        }

        public static void PosRender() 
        {
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.AlphaTest);
            GL.Disable(EnableCap.Blend);
        }

        private void CalculateBoundingBoxLimit() 
        {
            PreFix nullPreFix = new PreFix();
            nullPreFix.Angle = Vector3.Zero;
            nullPreFix.Position = Vector3.Zero;
            nullPreFix.Scale = Vector3.One;

            Dictionary<Boundary, Vector3> boundary = new Dictionary<Boundary, Vector3>();
            if (ObjNameOrder.Length > 0)
            {
                var first = modelGroup.Objects.Where(x => x.Key == ObjNameOrder[0]).FirstOrDefault();
                var prefix = preFixs.ContainsKey(first.Key) ? preFixs[first.Key] : nullPreFix;
                var max = ((new Vector4(first.Value.MaxBoundary, 1) * prefix.GetRotation()) * new Vector4(prefix.Scale, 1)) + new Vector4(prefix.Position, 0);
                var min = ((new Vector4(first.Value.MinBoundary, 1) * prefix.GetRotation()) * new Vector4(prefix.Scale, 1)) + new Vector4(prefix.Position, 0);

                if (max.X < min.X)
                {
                    float temp = max.X;
                    max.X = min.X;
                    min.X = temp;
                }

                if (max.Y < min.Y)
                {
                    float temp = max.Y;
                    max.Y = min.Y;
                    min.Y = temp;
                }

                if (max.Z < min.Z)
                {
                    float temp = max.Z;
                    max.Z = min.Z;
                    min.Z = temp;
                }

                boundary.Add(Boundary.max, max.Xyz);
                boundary.Add(Boundary.min, min.Xyz);

                foreach (var objName in ObjNameOrder)
                {
                    var second = modelGroup.Objects.Where(x => x.Key == objName).FirstOrDefault();
                    var sprefix = preFixs.ContainsKey(second.Key) ? preFixs[second.Key] : nullPreFix;
                    var smax = ((new Vector4(second.Value.MaxBoundary, 1) * sprefix.GetRotation()) * new Vector4(sprefix.Scale, 1)) + new Vector4(sprefix.Position, 0);
                    var smin = ((new Vector4(second.Value.MinBoundary, 1) * sprefix.GetRotation()) * new Vector4(sprefix.Scale, 1)) + new Vector4(sprefix.Position, 0);

                    if (smax.X < smin.X)
                    {
                        float temp = smax.X;
                        smax.X = smin.X;
                        smin.X = temp;
                    }

                    if (smax.Y < smin.Y)
                    {
                        float temp = smax.Y;
                        smax.Y = smin.Y;
                        smin.Y = temp;
                    }

                    if (smax.Z < smin.Z)
                    {
                        float temp = smax.Z;
                        smax.Z = smin.Z;
                        smin.Z = temp;
                    }

                    //max
                    if (boundary[Boundary.max].X < smax.X)
                    {
                        var v = boundary[Boundary.max];
                        v.X = smax.X;
                        boundary[Boundary.max] = v;
                    }

                    if (boundary[Boundary.max].Y < smax.Y)
                    {
                        var v = boundary[Boundary.max];
                        v.Y = smax.Y;
                        boundary[Boundary.max] = v;
                    }

                    if (boundary[Boundary.max].Z < smax.Z)
                    {
                        var v = boundary[Boundary.max];
                        v.Z = smax.Z;
                        boundary[Boundary.max] = v;
                    }

                    //min
                    if (boundary[Boundary.min].X > smin.X)
                    {
                        var v = boundary[Boundary.min];
                        v.X = smin.X;
                        boundary[Boundary.min] = v;
                    }

                    if (boundary[Boundary.min].Y > smin.Y)
                    {
                        var v = boundary[Boundary.min];
                        v.Y = smin.Y;
                        boundary[Boundary.min] = v;
                    }

                    if (boundary[Boundary.min].Z > smin.Z)
                    {
                        var v = boundary[Boundary.min];
                        v.Z = smin.Z;
                        boundary[Boundary.min] = v;
                    }

                }
            }
            else 
            {
                boundary.Add(Boundary.max, Vector3.One);
                boundary.Add(Boundary.min, -Vector3.One);
            }

            boundingBox = new BoundingBoxLimit(boundary[Boundary.min], boundary[Boundary.max]);
        }

        public BoundingBoxLimit GetBoundingBoxLimit() 
        {
            return boundingBox;
        }


        private class SimpleTexRef
        {
            private string MainTex = null;
            private string AlfaTex = null;
            private ModelGroup modelGroup;

            public SimpleTexRef(string meshName, ModelGroup modelGroup)
            {
                this.modelGroup = modelGroup;
                var mesh = modelGroup.MeshParts[meshName];
                var material = modelGroup.MaterialGroupDic[mesh.RefModelID].MaterialsDic[mesh.MaterialRef];
                var matTexGroupName = modelGroup.MatLinkerDic[mesh.RefModelID].MatTexGroupName;

                if (modelGroup.MatTexGroupDic.ContainsKey(matTexGroupName))
                {
                    var MatTexGroup = modelGroup.MatTexGroupDic[matTexGroupName];
                    if (MatTexGroup.MatTexDic.ContainsKey(material.DiffuseMatTex))
                    {
                        var matTex = MatTexGroup.MatTexDic[material.DiffuseMatTex];
                        MainTex = matTex.TextureName;
                    }

                    if (material.AsAlphaTex)
                    {
                        if (MatTexGroup.MatTexDic.ContainsKey(material.AlphaMatTex))
                        {
                            var matTex = MatTexGroup.MatTexDic[material.AlphaMatTex];
                            AlfaTex = matTex.TextureName;
                        }
                    }

                }

            }

            public void SetTex() 
            {
                if (MainTex != null && modelGroup.TextureRefDic.ContainsKey(MainTex))
                {
                    modelGroup.TextureRefDic[MainTex].Use(TextureUnit.Texture0);
                }

                if (AlfaTex != null && modelGroup.TextureRefDic.ContainsKey(AlfaTex))
                {
                    modelGroup.TextureRefDic[AlfaTex].Use(TextureUnit.Texture1);
                }
            }

        }
    }
}
