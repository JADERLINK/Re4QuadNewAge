using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewerBase;
using System.IO;
using System.Drawing;
using JADERLINK_MODEL_VIEWER.src.Nodes;

namespace RE4_PS4NS_MODEL_VIEWER.src
{
    public class LoadPs4NsPackCustomQuad
    {
        private ModelGroup modelGroup;
        private TexturePackNodeGroup tpng;

        public LoadPs4NsPackCustomQuad(ModelGroup modelGroup, TexturePackNodeGroup tpng)
        {
            this.modelGroup = modelGroup;
            this.tpng = tpng;
        }

        public void LoadPack(string packPath, int[] texIds)
        {
            BinaryReader pack = null;

            try
            {
                FileInfo fileInfo = new FileInfo(packPath);
                pack = new BinaryReader(fileInfo.OpenRead());

                uint PackID = pack.ReadUInt32();
                uint Amount = pack.ReadUInt32();

                List<long> offsets = new List<long>();

                for (int i = 0; i < Amount; i++)
                {
                    long offset = pack.ReadInt64();
                    offsets.Add(offset);
                }

                for (int i = 0; i < offsets.Count; i++)
                {
                    string texkey = PackID.ToString("X8") + "/" + i.ToString("D4");
                    
                    if (offsets[i] != 0 && texIds.Contains(i))
                    {
                        if (modelGroup.TextureRefDic.ContainsKey(texkey))
                        {
                            var node = tpng.Nodes.Find(texkey, false).FirstOrDefault();
                            ((NodeItem)node)?.Responsibility.ReleaseResponsibilities();
                            node?.Remove();
                        }

                        Bitmap bitmap = null;

                        pack.BaseStream.Position = offsets[i];
                        uint fileLength = pack.ReadUInt32();
                        uint ff_ff_ff_ff = pack.ReadUInt32();
                        uint PackID_ = pack.ReadUInt32();
                        uint Type = pack.ReadUInt32();

                        string Extension = "NULL";
                        
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
                        else if (imagemagic == 0x20464E47)
                        {
                            Extension = "GNF";

                            try
                            {
                                using (MemoryStream ms = new MemoryStream(imagebytes)) 
                                {
                                    Scarlet.IO.ImageFormats.GNF gnf = new Scarlet.IO.ImageFormats.GNF();
                                    gnf.Open(ms);
                                    bitmap = gnf.GetBitmap();
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else if (imagemagic == 0x00020000 || imagemagic == 0x000A0000)
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
            }
            catch (Exception)
            {
            }
            finally 
            {
                if (pack != null)
                {
                    pack.Close();
                }
            }

        }
    }
}
