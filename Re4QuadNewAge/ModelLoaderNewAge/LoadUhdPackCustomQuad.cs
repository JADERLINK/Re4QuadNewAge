using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using TGASharpLib;
using System.IO;
using System.Drawing;
using OpenTK;
using JADERLINK_MODEL_VIEWER.src.Nodes;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RE4_UHD_MODEL_VIEWER.src
{
    public class LoadUhdPackCustomQuad
    {
        private ModelGroup modelGroup;
        private TexturePackNodeGroup tpng;

        public LoadUhdPackCustomQuad(ModelGroup modelGroup, TexturePackNodeGroup tpng)
        {
            this.modelGroup = modelGroup;
            this.tpng = tpng;
        }

        public void LoadPack(string packPath, int[] texIds)
        {

            FileInfo fileInfo = new FileInfo(packPath);
            string FileID = fileInfo.Name.ToUpperInvariant();

            try
            {
                var pack = new BinaryReader(fileInfo.OpenRead());

                uint PackID = pack.ReadUInt32();
                uint Amount = pack.ReadUInt32();

                List<uint> offsets = new List<uint>();

                for (int i = 0; i < Amount; i++)
                {
                    uint offset = pack.ReadUInt32();
                    offsets.Add(offset);
                }

                for (int i = 0; i < offsets.Count; i++)
                {
                    if (offsets[i] != 0 && texIds.Contains(i))
                    {
                        Bitmap bitmap = null;

                        pack.BaseStream.Position = offsets[i];
                        uint fileLength = pack.ReadUInt32();
                        uint ff_ff_ff_ff = pack.ReadUInt32();
                        uint PackID_ = pack.ReadUInt32();
                        uint Type = pack.ReadUInt32();

                        string Extension = "NULL";
                        string texkey = PackID.ToString("X8") + "/" + i.ToString("D4");

                        byte[] imagebytes = new byte[fileLength];
                        pack.BaseStream.Read(imagebytes, 0, (int)fileLength);

                        uint imagemagic = BitConverter.ToUInt32(imagebytes, 0);
                        if (imagemagic == 0x20534444)
                        {
                            Extension = "DDS";

                            try
                            {
                                bitmap = DDSReaderSharp.ToBitmap(imagebytes);
                            }
                            catch (Exception)
                            {
                            }
                          
                        }
                        else
                        {
                            Extension = "TGA";
                            try
                            {
                                TGASharpLib.TGA nTGA = new TGASharpLib.TGA(imagebytes);
                                bitmap = nTGA.ToBitmap(true);
                              
                            }
                            catch (Exception)
                            {
                            }
                        }


                        if (bitmap != null)
                        {
                            if (modelGroup.TextureRefDic.ContainsKey(texkey))
                            {
                                var node = tpng.Nodes.Find(texkey, false).FirstOrDefault();
                                ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                                node?.Remove();
                            }

                            ResponsibilityContainer texContainer = new ResponsibilityContainer();
                            TEX_Representation tex_representation = new TEX_Representation(texkey, new List<string>() { texkey });
                            TextureGroupResponsibility textureGroupResponsibility = new TextureGroupResponsibility(modelGroup, tex_representation);
                            texContainer.Add(textureGroupResponsibility);

                            NodeTexture nodeTexture = new NodeTexture();
                            nodeTexture.Name = texkey;
                            nodeTexture.Text = texkey + "." + Extension;
                            nodeTexture.Responsibility = texContainer;
                            tpng.Nodes.Add(nodeTexture);

                            // GL da textura
                            modelGroup.AddTextureRef(new Dictionary<string, Bitmap> { { texkey, bitmap } });
                        }
                      
                    }
                }

                pack.Close();
            }
            catch (Exception)
            {

            }

        }
    }
}
