using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_SMX_TOOL
{
    public static class SMXextract
    {
        public static List<byte[]> extract(Stream file)
        {
            List<byte[]> res = new List<byte[]>();

            BinaryReader br = new BinaryReader(file);

            byte magic = br.ReadByte();

            if (magic != 0x10)
            {
                throw new InvalidDataException("Incorrect file the magic must be 0x10");
            }

            byte amount = br.ReadByte();

            int allLenght = (amount * 144) + 16;
            if (file.Length < allLenght)
            {
                throw new InvalidDataException("The file size is smaller than the amount of SMXentry reported.");
            }

            br.BaseStream.Position = 0x10;

            for (int i = 0; i < amount; i++)
            {
                byte[] cont = br.ReadBytes(144);
                res.Add(cont);
            }

            return res;
        }

        public static List<SMX> ToSmx(List<byte[]> lines, bool isPs2) 
        {
            List <SMX> res = new List<SMX>();
            for (int i = 0; i < lines.Count; i++)
            {
                SMX smx = new SMX();
                smx.UseSMXID = lines[i][0x00];
                smx.Mode = lines[i][0x01];
                smx.OpacityHierarchy = lines[i][0x02];
                smx.FaceCulling = lines[i][0x03];
                smx.LightSwitch = BitConverter.ToUInt32(lines[i], 0x04);
                smx.AlphaHierarchy = lines[i][0x08];
                smx.UnknownX09 = lines[i][0x09];
                smx.UnknownX0A = lines[i][0x0A];
                smx.UnknownX0B = lines[i][0x0B];
                smx.ColorRGB = new byte[3] { lines[i][0x0C], lines[i][0x0D], lines[i][0x0E] }; // 3 bytes
                smx.ColorAlpha = lines[i][0x0F];

                //----------
                //mode 0x00       
                smx.UnknownU10 = BitConverter.ToUInt32(lines[i], 0x10);
                smx.UnknownU14 = BitConverter.ToUInt32(lines[i], 0x14);
                smx.UnknownU18 = BitConverter.ToUInt32(lines[i], 0x18);
                smx.UnknownU1C = BitConverter.ToUInt32(lines[i], 0x1C);
                smx.UnknownU20 = BitConverter.ToUInt32(lines[i], 0x20);
                smx.UnknownU24 = BitConverter.ToUInt32(lines[i], 0x24);
                smx.UnknownU28 = BitConverter.ToUInt32(lines[i], 0x28);
                smx.UnknownU2C = BitConverter.ToUInt32(lines[i], 0x2C);
                smx.UnknownU30 = BitConverter.ToUInt32(lines[i], 0x30);
                smx.UnknownU34 = BitConverter.ToUInt32(lines[i], 0x34);
                smx.UnknownU38 = BitConverter.ToUInt32(lines[i], 0x38);
                smx.UnknownU3C = BitConverter.ToUInt32(lines[i], 0x3C);
                smx.UnknownU40 = BitConverter.ToUInt32(lines[i], 0x40);
                smx.UnknownU44 = BitConverter.ToUInt32(lines[i], 0x44);
                smx.UnknownU48 = BitConverter.ToUInt32(lines[i], 0x48);
                smx.UnknownU4C = BitConverter.ToUInt32(lines[i], 0x4C);
                smx.UnknownU50 = BitConverter.ToUInt32(lines[i], 0x50);
                smx.UnknownU54 = BitConverter.ToUInt32(lines[i], 0x54);
                smx.UnknownU58 = BitConverter.ToUInt32(lines[i], 0x58);
                smx.UnknownU5C = BitConverter.ToUInt32(lines[i], 0x5C);
                smx.UnknownU60 = BitConverter.ToUInt32(lines[i], 0x60);
                smx.UnknownU64 = BitConverter.ToUInt32(lines[i], 0x64);
                smx.UnknownU68 = BitConverter.ToUInt32(lines[i], 0x68);
                smx.UnknownU6C = BitConverter.ToUInt32(lines[i], 0x6C);
                smx.UnknownU70 = BitConverter.ToUInt32(lines[i], 0x70);
                smx.UnknownU74 = BitConverter.ToUInt32(lines[i], 0x74);
                smx.UnknownU78 = BitConverter.ToUInt32(lines[i], 0x78);
                smx.UnknownU7C = BitConverter.ToUInt32(lines[i], 0x7C);
                smx.UnknownU80 = BitConverter.ToUInt32(lines[i], 0x80);
                smx.UnknownU84 = BitConverter.ToUInt32(lines[i], 0x84);
                //TextureMovement
                smx.TextureMovement_X = BitConverter.ToSingle(lines[i], 0x88);
                smx.TextureMovement_Y = BitConverter.ToSingle(lines[i], 0x8C);

                //----------
                //mode 0x02
                smx.Swing0 = BitConverter.ToSingle(lines[i], 0x10);
                smx.Swing1 = BitConverter.ToSingle(lines[i], 0x14);
                smx.Swing2 = BitConverter.ToSingle(lines[i], 0x18);
                smx.Swing3 = BitConverter.ToSingle(lines[i], 0x1C);
                smx.Swing4 = BitConverter.ToSingle(lines[i], 0x20);
                smx.Swing5 = BitConverter.ToSingle(lines[i], 0x24);
                smx.Swing6 = BitConverter.ToSingle(lines[i], 0x28);
                smx.Swing7 = BitConverter.ToSingle(lines[i], 0x2C);
                smx.Swing8 = BitConverter.ToSingle(lines[i], 0x30);
                smx.Swing9 = BitConverter.ToSingle(lines[i], 0x34);
                smx.SwingA = BitConverter.ToSingle(lines[i], 0x38);
                smx.SwingB = BitConverter.ToSingle(lines[i], 0x3C);
                smx.SwingC = BitConverter.ToSingle(lines[i], 0x40);

                //---------
                //mode 0x01
                smx.RotationSpeed_X = BitConverter.ToSingle(lines[i], 0x10);
                smx.RotationSpeed_Y = BitConverter.ToSingle(lines[i], 0x14);
                smx.RotationSpeed_Z = BitConverter.ToSingle(lines[i], 0x18);
                if (isPs2)
                {
                    smx.RotationSpeed_W = BitConverter.ToSingle(lines[i], 0x1C);
                    smx.Unknown_GTU = BitConverter.ToUInt32(lines[i], 0x20);
                    smx.Unknown_GTV = 0;
                }
                else 
                {
                    smx.RotationSpeed_W = 1.0f;
                    smx.Unknown_GTU = BitConverter.ToUInt32(lines[i], 0x1C);
                    smx.Unknown_GTV = BitConverter.ToUInt32(lines[i], 0x20);
                }

                res.Add(smx);
            }

            return res;
        }

    }
}
