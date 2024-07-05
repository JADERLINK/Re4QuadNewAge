using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RE4_UHD_BIN_TOOL.ALL;
using RE4_UHD_BIN_TOOL.EXTRACT;

namespace RE4_UHD_SCENARIO_SMD_TOOL.SCENARIO
{
    public class UhdSmdExtract
    {
        //Action<Stream fileStream, long binOffset, long endOffset, int binID>
        public event Action<Stream, long, long, int> ToFileBin;

        //Action<Stream fileStream, long tplOffset, long endOffset>
        public event Action<Stream, long, long> ToFileTpl;

        public SMDLine[] Extract(Stream fileStream, out Dictionary<int, UhdBIN> uhdBinDic, out UhdTPL uhdTpl, out SmdMagic smdMagic, ref int binAmount)
        {
            BinaryReader br = new BinaryReader(fileStream);

            ushort Magic = br.ReadUInt16();
            
            // se não for um desses não é um .SMD valido;
            // nota: tem o magic 0x00000, porem esse crasha o jogo
            if (!(Magic == 0x0040 || Magic == 0x0140 || Magic == 0x0031 || Magic == 0x0020))
            {
                throw new ArgumentException("Invalid smd file!!!");
            }

            ushort SmdLinesAmount = br.ReadUInt16();

            uint offsetBin = br.ReadUInt32(); // para offset dos bins (são relativos)
            uint offsetTpl = br.ReadUInt32(); // tpl offset relativo (aponta para um offset (relativo), dele que vai pro tpl)
            uint offsetNone = br.ReadUInt32(); // no gc é o fim do arquivo, no de pc fica sendo o primeiro bin.

            smdMagic = new SmdMagic();
            smdMagic.magic = Magic;

            // no magic 0x0140, tem parametros adicionais
            if (Magic == 0x0140)
            {
                uint unkAmount = br.ReadUInt32();
                smdMagic.extraParameters = new uint[unkAmount];
                for (int i = 0; i < unkAmount; i++)
                {
                    uint unkValue = br.ReadUInt32();
                    smdMagic.extraParameters[i] = unkValue;
                }
            }
            else 
            {
                smdMagic.extraParameters = new uint[0];
            }


            // 72 bytes por linha smd

            int BinRealCount = binAmount;

            SMDLine[] SMDLines = new SMDLine[SmdLinesAmount];

            for (int i = 0; i < SmdLinesAmount; i++)
            {
                SMDLine smdLine = new SMDLine();

                smdLine.positionX = br.ReadSingle();
                smdLine.positionY = br.ReadSingle();
                smdLine.positionZ = br.ReadSingle();
                smdLine.angleX = br.ReadSingle();
                smdLine.angleY = br.ReadSingle();
                smdLine.angleZ = br.ReadSingle();
                smdLine.scaleX = br.ReadSingle();
                smdLine.scaleY = br.ReadSingle();
                smdLine.scaleZ = br.ReadSingle();
                smdLine.BinID = br.ReadUInt16();
                smdLine.FixedFF = br.ReadByte();
                smdLine.SmxID = br.ReadByte();
                smdLine.unused1 = br.ReadUInt32();
                smdLine.unused2 = br.ReadUInt32();
                smdLine.unused3 = br.ReadUInt32();
                smdLine.unused4 = br.ReadUInt32();
                smdLine.unused5 = br.ReadUInt32();
                smdLine.unused6 = br.ReadUInt32();
                smdLine.unused7 = br.ReadUInt32();
                smdLine.objectStatus = br.ReadUInt32();
               
                SMDLines[i] = smdLine;

                if (BinRealCount <= smdLine.BinID)
                {
                    BinRealCount = smdLine.BinID + 1;
                }
            }

            //------------------------------
            //BIN offset

            br.BaseStream.Position = offsetBin;

            List<uint> binOffsets = new List<uint>();

            for (int i = 0; i < BinRealCount && br.BaseStream.Position < br.BaseStream.Length; i++)
            {
                uint lastoffset = br.ReadUInt32();
                if (lastoffset == 0)
                {
                    break;
                }
                binOffsets.Add(lastoffset + offsetBin);

                if (binAmount <= i)
                {
                    binAmount = i + 1;
                }
            }

            //get bin

            Dictionary<int, UhdBIN> UhdBINs = new Dictionary<int, UhdBIN>();

            for (int i = 0; i < binOffsets.Count; i++)
            {
                long endOffset = binOffsets[i];
                try
                {
                    var uhdBin = UhdBinDecoder.Decoder(fileStream, binOffsets[i], out endOffset);
                    UhdBINs.Add(i, uhdBin);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error on Read Bin in Smd: " + i.ToString("D3") + Environment.NewLine + ex.ToString());
                }

                ToFileBin?.Invoke(fileStream, binOffsets[i], endOffset, i);

            }

            uhdBinDic = UhdBINs;

            //------------------------------
            //TPL offset

            br.BaseStream.Position = offsetTpl;

            uint tplOffset = br.ReadUInt32();
            tplOffset = tplOffset + offsetTpl;

            long tplEndOffset = tplOffset;
            try
            {
                uhdTpl = UhdTplDecoder.Decoder(fileStream, tplOffset, out tplEndOffset);
            }
            catch (Exception ex)
            {
                uhdTpl = null;
                Console.WriteLine("Error on Read: Tpl in Smd"  + Environment.NewLine + ex.ToString());
            }

            ToFileTpl?.Invoke(fileStream, tplOffset, tplEndOffset);

            return SMDLines;
        }

    }

    public class SmdMagic
    {
        // "magic" é o inicio do arquivo .smd, os dois primeiro bytes
        // na verdade, o magic é um byte, e o segundo byte é uma flag
        // mais estou colocando os dois junto para simplificar.
        public ushort magic = 0x0040;
        // tem somente no magic 0x0140 do r100.udas/r100_004.SMD
        // esses valores são a quantidade de "SMDLine", dos Smd que estão dentro dos arquivos .dat
        public uint[] extraParameters = new uint[0];
    }

    public class SMDLine // SmdEntry
    {
        public float positionX;
        public float positionY;
        public float positionZ;
        public float angleX;
        public float angleY;
        public float angleZ;
        public float scaleX;
        public float scaleY;
        public float scaleZ;

        public ushort BinID;
        public byte FixedFF;
        public byte SmxID;
        public uint unused1;
        public uint unused2;
        public uint unused3;
        public uint unused4;
        public uint unused5;
        public uint unused6;
        public uint unused7;
        public uint objectStatus;

        public SMDLine Clone() 
        {
            SMDLine newLine = new SMDLine();
            newLine.positionX = positionX;
            newLine.positionY = positionY;
            newLine.positionZ = positionZ;
            newLine.angleX = angleX;
            newLine.angleY = angleY;
            newLine.angleZ = angleZ;
            newLine.scaleX = scaleX;
            newLine.scaleY = scaleY;
            newLine.scaleZ = scaleZ;

            newLine.BinID = BinID;
            newLine.FixedFF = FixedFF;
            newLine.SmxID = SmxID;
            newLine.unused1 = unused1;
            newLine.unused2 = unused2;
            newLine.unused3 = unused3;
            newLine.unused4 = unused4;
            newLine.unused5 = unused5;
            newLine.unused6 = unused6;
            newLine.unused7 = unused7;
            newLine.objectStatus = objectStatus;

            return newLine;
        }

    }

}