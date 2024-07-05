using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_2007_SCENARIO_SMD_EXTRACT
{
    public static class SmdExtract
    {
        public static SMDLine[] Extrator(string Filename, out BinRenderBox[] renderBoxes)
        {
        
            var file = new FileStream(Filename, FileMode.Open);

            byte[] b0x4000 = new byte[2];
            file.Read(b0x4000, 0, 2);

            byte[] listLenght = new byte[2];
            file.Read(listLenght, 0, 2);
            ushort length = BitConverter.ToUInt16(listLenght, 0);

            byte[] offsetbin = new byte[4];
            byte[] offsettpl = new byte[4];
            byte[] none1 = new byte[4];

            file.Read(offsetbin, 0, 4);
            file.Read(offsettpl, 0, 4);
            file.Read(none1, 0, 4);

            uint Uoffsetbin = BitConverter.ToUInt32(offsetbin, 0);
            uint Uoffsettpl = BitConverter.ToUInt32(offsettpl, 0);

            SMDLine[] SMDLines = new SMDLine[length];

            //64 bytes de tamanho
            for (int i = 0; i < length; i++)
            {
                SMDLine smdLine = new SMDLine();

                byte[] line = new byte[64];
                file.Read(line, 0, 64);

                float positionX = BitConverter.ToSingle(line, 0);
                float positionY = BitConverter.ToSingle(line, 4);
                float positionZ = BitConverter.ToSingle(line, 8);
                float positionW = BitConverter.ToSingle(line, 12);
                float angleX = BitConverter.ToSingle(line, 16);
                float angleY = BitConverter.ToSingle(line, 20);
                float angleZ = BitConverter.ToSingle(line, 24);
                float angleW = BitConverter.ToSingle(line, 28);
                float scaleX = BitConverter.ToSingle(line, 32);
                float scaleY = BitConverter.ToSingle(line, 36);
                float scaleZ = BitConverter.ToSingle(line, 40);
                float scaleW = BitConverter.ToSingle(line, 44);

                ushort BinID = BitConverter.ToUInt16(line, 48);
                byte FixedFF = line[50];
                byte SmxID = line[51];
                uint unused1 = BitConverter.ToUInt32(line, 52);
                uint objectStatus = BitConverter.ToUInt32(line, 56);
                uint unused2 = BitConverter.ToUInt32(line, 60);

                smdLine.positionX = positionX;
                smdLine.positionY = positionY;
                smdLine.positionZ = positionZ;
                smdLine.positionW = positionW;

                smdLine.angleX = angleX;
                smdLine.angleY = angleY;
                smdLine.angleZ = angleZ;
                smdLine.angleW = angleW;


                smdLine.scaleX = scaleX;
                smdLine.scaleY = scaleY;
                smdLine.scaleZ = scaleZ;
                smdLine.scaleW = scaleW;

                smdLine.BinID = BinID;
                smdLine.FixedFF = FixedFF;
                smdLine.SmxID = SmxID;
                smdLine.unused1 = unused1;
                smdLine.objectStatus = objectStatus;
                smdLine.unused2 = unused2;

                SMDLines[i] = smdLine;
            }

            List<BinRenderBox> boxes = new List<BinRenderBox>();
            ///-----------------------------------------------------------


            int BinRealCount = 0;

            List<KeyValuePair<int, int>> ListBinPos = new List<KeyValuePair<int, int>>();

            file.Position = Uoffsetbin;

            byte[] BinFirt = new byte[4];
            file.Read(BinFirt, 0, 4);

            uint uBinFirt = BitConverter.ToUInt32(BinFirt, 0);

            int blockSize = (int)uBinFirt - 4;
            byte[] blockWithOffsets = new byte[blockSize];
            file.Read(blockWithOffsets, 0, blockSize);

            int Amount = blockSize / 4;

            ListBinPos.Add(new KeyValuePair<int, int>((int)uBinFirt, 0));

            int tempCount = 0;
            for (int i = 0; i < Amount; i++)
            {
                int offsetBin = BitConverter.ToInt32(blockWithOffsets, tempCount);
                tempCount += 4;
                ListBinPos.Add(new KeyValuePair<int, int>(offsetBin, 0));
                if (offsetBin != 0)
                {
                    ListBinPos[i] = new KeyValuePair<int, int>(ListBinPos[i].Key, (offsetBin - ListBinPos[i].Key));
                }
                else
                {
                    ListBinPos[i] = new KeyValuePair<int, int>(ListBinPos[i].Key, (int)(Uoffsettpl - Uoffsetbin - ListBinPos[i].Key));
                }
            }

            ListBinPos[Amount] = new KeyValuePair<int, int>(ListBinPos[Amount].Key, (int)(Uoffsettpl - Uoffsetbin - ListBinPos[Amount].Key));

            for (int i = 0; i < ListBinPos.Count; i++)
            {
                if (ListBinPos[i].Key != 0)
                {
                    BinRealCount++;

                    file.Position = Uoffsetbin + ListBinPos[i].Key;
                    byte[] arqBin = new byte[ListBinPos[i].Value];
                    file.Read(arqBin, 0, (int)ListBinPos[i].Value);


                    //---
                    BinRenderBox box = new BinRenderBox();
                    box.DrawDistanceNegativeX = BitConverter.ToSingle(arqBin, 0x30);
                    box.DrawDistanceNegativeY = BitConverter.ToSingle(arqBin, 0x34);
                    box.DrawDistanceNegativeZ = BitConverter.ToSingle(arqBin, 0x38);

                    box.DrawDistancePositiveX = BitConverter.ToSingle(arqBin, 0x40);
                    box.DrawDistancePositiveY = BitConverter.ToSingle(arqBin, 0x44);
                    box.DrawDistancePositiveZ = BitConverter.ToSingle(arqBin, 0x48);

                    boxes.Add(box);
                }

            }

            //-----------------------------------------------
            renderBoxes = boxes.ToArray();
            file.Close();
            return SMDLines;
        }



    }

    public class SMDLine
    {
        public float positionX;
        public float positionY;
        public float positionZ;
        public float positionW;
        public float angleX;
        public float angleY;
        public float angleZ;
        public float angleW;
        public float scaleX;
        public float scaleY;
        public float scaleZ;
        public float scaleW;

        public ushort BinID;
        public byte FixedFF;
        public byte SmxID;
        public uint unused1;
        public uint objectStatus;
        public uint unused2;
    }


    public class BinRenderBox 
    {
        public float DrawDistanceNegativeX;
        public float DrawDistanceNegativeY;
        public float DrawDistanceNegativeZ;

        public float DrawDistancePositiveX;
        public float DrawDistancePositiveY;
        public float DrawDistancePositiveZ;

    }

}
