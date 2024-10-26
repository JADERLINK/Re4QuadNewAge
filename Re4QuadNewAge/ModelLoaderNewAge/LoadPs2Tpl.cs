using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using System.IO;
using OpenTK;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using TPL_PS2_EXTRACT;
using System.Drawing;

namespace RE4_PS2_MODEL_VIEWER.src
{
    public class LoadPs2Tpl
    {
        private ModelGroup modelGroup;
        private ModelNodeGroup mng;

        public LoadPs2Tpl(ModelGroup modelGroup, ModelNodeGroup mng)
        {
            this.modelGroup = modelGroup;
            this.mng = mng;
        }

        public void LoadPs2TPL(string tplPath)
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
                long mainOffset = 0;

                TplImageHeader[] Tihs = null;
                TplExtractHeaders Teh = null;
                BinaryReader br = null;

                try
                {
                    br = new BinaryReader(fileInfo.OpenRead());
                    Teh = new TplExtractHeaders(br, mainOffset);
                    var Header = Teh.MainReader();
                    Tihs = Teh.Extract(Header.TplCount, Header.StartOffset);
                }
                catch (Exception)
                {
                }

                if (Teh != null && Tihs != null && br != null)
                {
                    ResponsibilityContainer RContainer = new ResponsibilityContainer();
                    MatTexGroup_Representation matTexGroup_Representation = new MatTexGroup_Representation(FileID);
                    MatTexGroupResponsibility matTexGroupResponsibility = new MatTexGroupResponsibility(modelGroup, matTexGroup_Representation);
                    RContainer.Add(matTexGroupResponsibility);

                    //MatTexGroup
                    MatTexGroup matTexGroup = new MatTexGroup(FileID);
                    PopulateMatTexGroup(ref matTexGroup, Tihs.Length, FileID);
                    modelGroup.AddMatTexGroup(matTexGroup);

                    //texturas
                    Dictionary<string, Bitmap> textureDic = new Dictionary<string, Bitmap>();
                    GetImages(ref textureDic, ref Tihs, ref br, mainOffset, FileID, true);
                    br.Close();

                    // texturas
                    foreach (var texId in textureDic.Keys)
                    {
                        TEX_Representation tex_representation = new TEX_Representation(texId, new List<string>() { texId });
                        TextureGroupResponsibility textureGroupResponsibility = new TextureGroupResponsibility(modelGroup, tex_representation);
                        RContainer.Add(textureGroupResponsibility);
                    }

                    // GL das texturas
                    modelGroup.AddTextureRef(textureDic);

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

        public static void PopulateMatTexGroup(ref MatTexGroup matTexGroup, int Length, string FileID)
        {
            for (int i = 0; i < Length; i++)
            {
                MatTex matTex = new MatTex(i.ToString());
                matTex.TextureName = FileID + "/" + i.ToString("D4");
                matTexGroup.MatTexDic.Add(matTex.MatTexName, matTex);
            }
        }

        public static void GetImages(ref Dictionary<string, Bitmap> textureDic, ref TplImageHeader[] Tihs, ref BinaryReader br, long MainOffset, string FileID, bool rotateInterlace1and3) 
        {
            try
            {
                bool flipY = false;
                TplImage tplImage = new TplImage(ref br, flipY, rotateInterlace1and3, MainOffset);
                //images
                for (int i = 0; i < Tihs.Length; i++)
                {
                    Bitmap bitmap = null;
                    bool AsBitmap = false;

                    try
                    {
                        AsBitmap = tplImage.GetImage(
                            Tihs[i].Width, 
                            Tihs[i].Height, 
                            Tihs[i].BitDepth, 
                            Tihs[i].Interlace, 
                            Tihs[i].IndexesOffset,
                            Tihs[i].PaletteOffset,
                            out bitmap);
                    }
                    catch (Exception)
                    {
                    }

                    if (AsBitmap && bitmap != null)
                    {
                        // recriar um novo Bitmap resolve possiveis bugs de acontecer.
                        Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        Graphics Edit = Graphics.FromImage(newBitmap);
                        Edit.DrawImage(bitmap, new Point(0,0));
                        Edit.Save();
                        textureDic.Add(FileID + "/" + i.ToString("D4"), newBitmap);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
