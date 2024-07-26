using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Files;

namespace Re4QuadExtremeEditor.src.Class
{
    /// <summary>
    /// Classe responsavel por toda a manipulação de carregar e salvar arquivos
    /// </summary>
    public static class FileManager
    {
        #region loadFile
        public static void LoadFileESL(FileStream file, FileInfo fileInfo) 
        {
            FileEnemyEslGroup esl = new FileEnemyEslGroup();
            int offset = 0;
            ushort i = 0;
            for (; i < 256; i++)
            {
                byte[] res = new byte[32];
                offset = file.Read(res, 0, 32);
                esl.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < 256)
            {
                for (; i < 256; i++)
                {
                    esl.Lines.Add(i, new byte[32]);
                }
            }

            if (fileInfo.Length > 8192)
            {
                esl.EndFile = new byte[fileInfo.Length - 8192];
                file.Read(esl.EndFile, 0, (int)fileInfo.Length - 8192);
            }
            DataBase.FileESL = null;
            DataBase.FileESL = esl;

            // carregou novo arquivo
            // atualiza node
            DataBase.NodeESL.Nodes.Clear();
            DataBase.NodeESL.MethodsForGL = DataBase.FileESL.MethodsForGL;
            DataBase.NodeESL.PropertyMethods = DataBase.FileESL.Methods;
            DataBase.NodeESL.DisplayMethods = DataBase.FileESL.DisplayMethods;
            DataBase.NodeESL.MoveMethods = DataBase.FileESL.MoveMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < 256; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ESL, iN);
                nodes.Add(o);
            }
            DataBase.NodeESL.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeESL.Expand();
            GC.Collect();
        }

        public static void LoadFileETS_2007_PS2(FileStream file, FileInfo fileInfo) 
        {
            FileEtcModelEtsGroup ets = new FileEtcModelEtsGroup(Re4Version.V2007PS2);
           
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x00);
            ets.StartFile = header;

            if (Amount > Consts.AmountLimitETS)
            {
                Amount = Consts.AmountLimitETS;
                byte[] b = BitConverter.GetBytes(Amount);
                ets.StartFile[0x00] = b[0];
                ets.StartFile[0x01] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[64];
                offset = file.Read(res, 0, 64);
                ets.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ets.Lines.Add(i, new byte[64]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 64))
            {
                ets.EndFile = new byte[fileInfo.Length -(16 + (Amount * 64))];
                file.Read(ets.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 64)));
            }

            ets.IdForNewLine = i;
            ets.CreateNewETS_ID_List();

            DataBase.FileETS = null;
            DataBase.FileETS = ets;

            DataBase.NodeETS.Nodes.Clear();
            DataBase.NodeETS.MethodsForGL = DataBase.FileETS.MethodsForGL;
            DataBase.NodeETS.PropertyMethods = DataBase.FileETS.Methods;
            DataBase.NodeETS.DisplayMethods = DataBase.FileETS.DisplayMethods;
            DataBase.NodeETS.MoveMethods = DataBase.FileETS.MoveMethods;
            DataBase.NodeETS.ChangeAmountMethods = DataBase.FileETS.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ETS, iN);
                nodes.Add(o);
            }
            DataBase.NodeETS.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeETS.Expand();
            GC.Collect();
        }

        public static void LoadFileETS_UHD(FileStream file, FileInfo fileInfo)
        {
            FileEtcModelEtsGroup ets = new FileEtcModelEtsGroup(Re4Version.UHD);
            
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x00);
            ets.StartFile = header;

            if (Amount > Consts.AmountLimitETS)
            {
                Amount = Consts.AmountLimitETS;
                byte[] b = BitConverter.GetBytes(Amount);
                ets.StartFile[0x00] = b[0];
                ets.StartFile[0x01] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[40];
                offset = file.Read(res, 0, 40);
                ets.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ets.Lines.Add(i, new byte[40]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 40))
            {
                ets.EndFile = new byte[fileInfo.Length - (16 + (Amount * 40))];
                file.Read(ets.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 40)));
            }

            ets.IdForNewLine = i;
            ets.CreateNewETS_ID_List();

            DataBase.FileETS = null;
            DataBase.FileETS = ets;

            DataBase.NodeETS.Nodes.Clear();
            DataBase.NodeETS.MethodsForGL = DataBase.FileETS.MethodsForGL;
            DataBase.NodeETS.PropertyMethods = DataBase.FileETS.Methods;
            DataBase.NodeETS.DisplayMethods = DataBase.FileETS.DisplayMethods;
            DataBase.NodeETS.MoveMethods = DataBase.FileETS.MoveMethods;
            DataBase.NodeETS.ChangeAmountMethods = DataBase.FileETS.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ETS, iN);
                nodes.Add(o);
            }
            DataBase.NodeETS.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeETS.Expand();
            GC.Collect();
        }

        public static void LoadFileITA_2007_PS2(FileStream file, FileInfo fileInfo)
        {
            FileSpecialGroup ita = new FileSpecialGroup(Re4Version.V2007PS2, SpecialFileFormat.ITA);
          
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            ita.StartFile = header;

            if (Amount > Consts.AmountLimitITA)
            {
                Amount = Consts.AmountLimitITA;
                byte[] b = BitConverter.GetBytes(Amount);
                ita.StartFile[0x06] = b[0];
                ita.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[176];
                offset = file.Read(res, 0, 176);
                ita.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ita.Lines.Add(i, new byte[176]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 176))
            {
                ita.EndFile = new byte[fileInfo.Length - (16 + (Amount * 176))];
                file.Read(ita.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 176)));
            }

            ita.IdForNewLine = i;
            ita.SetStartIdexContent();

            DataBase.FileITA = null;
            DataBase.FileITA = ita;
            DataBase.Extras.SetStartRefInteractionTypeContent();

            DataBase.Extras.ClearITAs();
            DataBase.NodeITA.Nodes.Clear();
            DataBase.NodeITA.MethodsForGL = DataBase.FileITA.MethodsForGL;
            DataBase.NodeITA.ExtrasMethodsForGL = DataBase.FileITA.ExtrasMethodsForGL;
            DataBase.NodeITA.PropertyMethods = DataBase.FileITA.Methods;
            DataBase.NodeITA.DisplayMethods = DataBase.FileITA.DisplayMethods;
            DataBase.NodeITA.MoveMethods = DataBase.FileITA.MoveMethods;
            DataBase.NodeITA.ChangeAmountMethods = DataBase.FileITA.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ITA, iN);
                nodes.Add(o);
            }
            DataBase.NodeITA.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeITA.Expand();
            DataBase.Extras.AddITAs();
            DataBase.NodeEXTRAS.Expand();
            GC.Collect();
        }

        public static void LoadFileITA_UHD(FileStream file, FileInfo fileInfo)
        {
            FileSpecialGroup ita = new FileSpecialGroup(Re4Version.UHD, SpecialFileFormat.ITA);
          
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            ita.StartFile = header;

            if (Amount > Consts.AmountLimitITA)
            {
                Amount = Consts.AmountLimitITA;
                byte[] b = BitConverter.GetBytes(Amount);
                ita.StartFile[0x06] = b[0];
                ita.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[156];
                offset = file.Read(res, 0, 156);
                ita.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ita.Lines.Add(i, new byte[156]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 156))
            {
                ita.EndFile = new byte[fileInfo.Length - (16 + (Amount * 156))];
                file.Read(ita.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 156)));
            }

            ita.IdForNewLine = i;
            ita.SetStartIdexContent();

            DataBase.FileITA = null;
            DataBase.FileITA = ita;
            DataBase.Extras.SetStartRefInteractionTypeContent();

            DataBase.Extras.ClearITAs();
            DataBase.NodeITA.Nodes.Clear();
            DataBase.NodeITA.MethodsForGL = DataBase.FileITA.MethodsForGL;
            DataBase.NodeITA.ExtrasMethodsForGL = DataBase.FileITA.ExtrasMethodsForGL;
            DataBase.NodeITA.PropertyMethods = DataBase.FileITA.Methods;
            DataBase.NodeITA.DisplayMethods = DataBase.FileITA.DisplayMethods;
            DataBase.NodeITA.MoveMethods = DataBase.FileITA.MoveMethods;
            DataBase.NodeITA.ChangeAmountMethods = DataBase.FileITA.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ITA, iN);
                nodes.Add(o);
            }
            DataBase.NodeITA.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeITA.Expand();
            DataBase.Extras.AddITAs();
            DataBase.NodeEXTRAS.Expand();
            GC.Collect();
        }

        public static void LoadFileAEV_2007_PS2(FileStream file, FileInfo fileInfo)
        {
            FileSpecialGroup aev = new FileSpecialGroup(Re4Version.V2007PS2, SpecialFileFormat.AEV);
            
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            aev.StartFile = header;

            if (Amount > Consts.AmountLimitAEV)
            {
                Amount = Consts.AmountLimitAEV;
                byte[] b = BitConverter.GetBytes(Amount);
                aev.StartFile[0x06] = b[0];
                aev.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[160];
                offset = file.Read(res, 0, 160);
                aev.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    aev.Lines.Add(i, new byte[160]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 160))
            {
                aev.EndFile = new byte[fileInfo.Length - (16 + (Amount * 160))];
                file.Read(aev.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 160)));
            }

            aev.IdForNewLine = i;
            aev.SetStartIdexContent();

            DataBase.FileAEV = null;
            DataBase.FileAEV = aev;
            DataBase.Extras.SetStartRefInteractionTypeContent();

            DataBase.Extras.ClearAll();
            DataBase.NodeAEV.Nodes.Clear();
            DataBase.NodeAEV.MethodsForGL = DataBase.FileAEV.MethodsForGL;
            DataBase.NodeAEV.ExtrasMethodsForGL = DataBase.FileAEV.ExtrasMethodsForGL;
            DataBase.NodeAEV.PropertyMethods = DataBase.FileAEV.Methods;
            DataBase.NodeAEV.DisplayMethods = DataBase.FileAEV.DisplayMethods;
            DataBase.NodeAEV.MoveMethods = DataBase.FileAEV.MoveMethods;
            DataBase.NodeAEV.ChangeAmountMethods = DataBase.FileAEV.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.AEV, iN);
                nodes.Add(o);
            }
            DataBase.NodeAEV.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeAEV.Expand();
            DataBase.Extras.AddAll();
            DataBase.NodeEXTRAS.Expand();
            GC.Collect();
        }

        public static void LoadFileAEV_UHD(FileStream file, FileInfo fileInfo)
        {
            FileSpecialGroup aev = new FileSpecialGroup(Re4Version.UHD, SpecialFileFormat.AEV);
            
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            aev.StartFile = header;

            if (Amount > Consts.AmountLimitAEV)
            {
                Amount = Consts.AmountLimitAEV;
                byte[] b = BitConverter.GetBytes(Amount);
                aev.StartFile[0x06] = b[0];
                aev.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[156];
                offset = file.Read(res, 0, 156);
                aev.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    aev.Lines.Add(i, new byte[156]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 156))
            {
                aev.EndFile = new byte[fileInfo.Length - (16 + (Amount * 156))];
                file.Read(aev.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 156)));
            }

            aev.IdForNewLine = i;
            aev.SetStartIdexContent();

            DataBase.FileAEV = null;
            DataBase.FileAEV = aev;
            DataBase.Extras.SetStartRefInteractionTypeContent();

            DataBase.Extras.ClearAll();
            DataBase.NodeAEV.Nodes.Clear();
            DataBase.NodeAEV.MethodsForGL = DataBase.FileAEV.MethodsForGL;
            DataBase.NodeAEV.ExtrasMethodsForGL = DataBase.FileAEV.ExtrasMethodsForGL;
            DataBase.NodeAEV.PropertyMethods = DataBase.FileAEV.Methods;
            DataBase.NodeAEV.DisplayMethods = DataBase.FileAEV.DisplayMethods;
            DataBase.NodeAEV.MoveMethods = DataBase.FileAEV.MoveMethods;
            DataBase.NodeAEV.ChangeAmountMethods = DataBase.FileAEV.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.AEV, iN);
                nodes.Add(o);
            }
            DataBase.NodeAEV.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeAEV.Expand();
            DataBase.Extras.AddAll();
            DataBase.NodeEXTRAS.Expand();
            GC.Collect();
        }

        public static void LoadFileDSE(FileStream file, FileInfo fileInfo) 
        {
            File_DSE_Group dse = new File_DSE_Group();
            byte[] header = new byte[4];
            int offset = file.Read(header, 0, 4);
            ushort Amount = BitConverter.ToUInt16(header, 0x00);

            if (Amount > Consts.AmountLimitDSE)
            {
                Amount = Consts.AmountLimitDSE;
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[12];
                offset = file.Read(res, 0, 12);
                dse.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    dse.Lines.Add(i, new byte[12]);
                }
            }

            if (fileInfo.Length > 4 + (Amount * 12))
            {
                dse.EndFile = new byte[fileInfo.Length - (4 + (Amount * 12))];
                file.Read(dse.EndFile, 0, (int)fileInfo.Length - (4 + (Amount * 12)));
            }

            dse.IdForNewLine = i;

            DataBase.FileDSE = null;
            DataBase.FileDSE = dse;

            DataBase.NodeDSE.Nodes.Clear();
            DataBase.NodeDSE.PropertyMethods = DataBase.FileDSE.Methods;
            DataBase.NodeDSE.DisplayMethods = DataBase.FileDSE.DisplayMethods;
            DataBase.NodeDSE.MoveMethods = DataBase.FileDSE.MoveMethods;
            DataBase.NodeDSE.ChangeAmountMethods = DataBase.FileDSE.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.DSE, iN);
                nodes.Add(o);
            }
            DataBase.NodeDSE.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeDSE.Expand();
            GC.Collect();
        }

        public static void LoadFileFSE(FileStream file, FileInfo fileInfo)
        {
            File_FSE_Group fse = new File_FSE_Group();
           
            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            fse.StartFile = header;

            if (Amount > Consts.AmountLimitFSE)
            {
                Amount = Consts.AmountLimitFSE;
                byte[] b = BitConverter.GetBytes(Amount);
                fse.StartFile[0x06] = b[0];
                fse.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[132];
                offset = file.Read(res, 0, 132);
                fse.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    fse.Lines.Add(i, new byte[132]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 132))
            {
                fse.EndFile = new byte[fileInfo.Length - (16 + (Amount * 132))];
                file.Read(fse.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 132)));
            }

            fse.IdForNewLine = i;
            fse.SetStartIdexContent();

            DataBase.FileFSE = null;
            DataBase.FileFSE = fse;

            DataBase.NodeFSE.Nodes.Clear();
            DataBase.NodeFSE.MethodsForGL = DataBase.FileFSE.MethodsForGL;
            DataBase.NodeFSE.PropertyMethods = DataBase.FileFSE.Methods;
            DataBase.NodeFSE.DisplayMethods = DataBase.FileFSE.DisplayMethods;
            DataBase.NodeFSE.MoveMethods = DataBase.FileFSE.MoveMethods;
            DataBase.NodeFSE.ChangeAmountMethods = DataBase.FileFSE.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.FSE, iN);
                nodes.Add(o);
            }
            DataBase.NodeFSE.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeFSE.Expand();
            GC.Collect();
        }

        public static void LoadFileSAR(FileStream file, FileInfo fileInfo)
        {
            File_ESAR_Group sar = new File_ESAR_Group(EsarFileFormat.SAR);

            byte[] header = new byte[16];
            int ofsart = file.Read(header, 0, 16);
            ushort Amount = (ushort)BitConverter.ToUInt32(header, 0x00);
            sar.StartFile = header;

            if (Amount > Consts.AmountLimitSAR)
            {
                Amount = Consts.AmountLimitSAR;
                byte[] b = BitConverter.GetBytes(Amount);
                sar.StartFile[0x00] = b[0];
                sar.StartFile[0x01] = b[1];
                sar.StartFile[0x02] = 0x00;
                sar.StartFile[0x03] = 0x00;
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[216];
                ofsart = file.Read(res, 0, 216);
                sar.Lines.Add(i, res);

                if (ofsart > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    sar.Lines.Add(i, new byte[216]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 216))
            {
                sar.EndFile = new byte[fileInfo.Length - (16 + (Amount * 216))];
                file.Read(sar.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 216)));
            }

            sar.IdForNewLine = i;
            sar.SetStartIdexContent();

            DataBase.FileSAR = null;
            DataBase.FileSAR = sar;

            DataBase.NodeSAR.Nodes.Clear();
            DataBase.NodeSAR.MethodsForGL = DataBase.FileSAR.MethodsForGL;
            DataBase.NodeSAR.PropertyMethods = DataBase.FileSAR.Methods;
            DataBase.NodeSAR.DisplayMethods = DataBase.FileSAR.DisplayMethods;
            DataBase.NodeSAR.MoveMethods = DataBase.FileSAR.MoveMethods;
            DataBase.NodeSAR.ChangeAmountMethods = DataBase.FileSAR.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.SAR, iN);
                nodes.Add(o);
            }
            DataBase.NodeSAR.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeSAR.Expand();
            GC.Collect();
        }

        public static void LoadFileEAR(FileStream file, FileInfo fileInfo) 
        {
            File_ESAR_Group ear = new File_ESAR_Group(EsarFileFormat.EAR);

            byte[] header = new byte[16];
            int ofeart = file.Read(header, 0, 16);
            ushort Amount = (ushort)BitConverter.ToUInt32(header, 0x00);
            ear.StartFile = header;

            if (Amount > Consts.AmountLimitEAR)
            {
                Amount = Consts.AmountLimitEAR;
                byte[] b = BitConverter.GetBytes(Amount);
                ear.StartFile[0x00] = b[0];
                ear.StartFile[0x01] = b[1];
                ear.StartFile[0x02] = 0x00;
                ear.StartFile[0x03] = 0x00;
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[152];
                ofeart = file.Read(res, 0, 152);
                ear.Lines.Add(i, res);

                if (ofeart > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ear.Lines.Add(i, new byte[152]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 152))
            {
                ear.EndFile = new byte[fileInfo.Length - (16 + (Amount * 152))];
                file.Read(ear.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 152)));
            }

            ear.IdForNewLine = i;
            ear.SetStartIdexContent();

            DataBase.FileEAR = null;
            DataBase.FileEAR = ear;

            DataBase.NodeEAR.Nodes.Clear();
            DataBase.NodeEAR.MethodsForGL = DataBase.FileEAR.MethodsForGL;
            DataBase.NodeEAR.PropertyMethods = DataBase.FileEAR.Methods;
            DataBase.NodeEAR.DisplayMethods = DataBase.FileEAR.DisplayMethods;
            DataBase.NodeEAR.MoveMethods = DataBase.FileEAR.MoveMethods;
            DataBase.NodeEAR.ChangeAmountMethods = DataBase.FileEAR.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.EAR, iN);
                nodes.Add(o);
            }
            DataBase.NodeEAR.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeEAR.Expand();
            GC.Collect();
        }

        public static void LoadFileEMI_2007_PS2(FileStream file, FileInfo fileInfo)
        {
            File_EMI_Group emi = new File_EMI_Group(Re4Version.V2007PS2);

            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = (ushort)BitConverter.ToUInt32(header, 0x00);
            emi.StartFile = header;

            if (Amount > Consts.AmountLimitEMI)
            {
                Amount = Consts.AmountLimitEMI;
                byte[] b = BitConverter.GetBytes(Amount);
                emi.StartFile[0x00] = b[0];
                emi.StartFile[0x01] = b[1];
                emi.StartFile[0x02] = 0x00;
                emi.StartFile[0x03] = 0x00;
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[64];
                offset = file.Read(res, 0, 64);
                emi.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    emi.Lines.Add(i, new byte[64]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 64))
            {
                emi.EndFile = new byte[fileInfo.Length - (16 + (Amount * 64))];
                file.Read(emi.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 64)));
            }

            emi.IdForNewLine = i;

            DataBase.FileEMI = null;
            DataBase.FileEMI = emi;

            DataBase.NodeEMI.Nodes.Clear();
            DataBase.NodeEMI.MethodsForGL = DataBase.FileEMI.MethodsForGL;
            DataBase.NodeEMI.PropertyMethods = DataBase.FileEMI.Methods;
            DataBase.NodeEMI.DisplayMethods = DataBase.FileEMI.DisplayMethods;
            DataBase.NodeEMI.MoveMethods = DataBase.FileEMI.MoveMethods;
            DataBase.NodeEMI.ChangeAmountMethods = DataBase.FileEMI.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.EMI, iN);
                nodes.Add(o);
            }
            DataBase.NodeEMI.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeEMI.Expand();
            GC.Collect();
        }

        public static void LoadFileEMI_UHD(FileStream file, FileInfo fileInfo)
        {
            File_EMI_Group emi = new File_EMI_Group(Re4Version.UHD);

            byte[] header = new byte[8];
            int offset = file.Read(header, 0, 8);
            ushort Amount = (ushort)BitConverter.ToUInt32(header, 0x00);
            emi.StartFile = header;

            if (Amount > Consts.AmountLimitEMI)
            {
                Amount = Consts.AmountLimitEMI;
                byte[] b = BitConverter.GetBytes(Amount);
                emi.StartFile[0x00] = b[0];
                emi.StartFile[0x01] = b[1];
                emi.StartFile[0x02] = 0x00;
                emi.StartFile[0x03] = 0x00;
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[64];
                offset = file.Read(res, 0, 64);
                emi.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    emi.Lines.Add(i, new byte[64]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 64))
            {
                emi.EndFile = new byte[fileInfo.Length - (16 + (Amount * 64))];
                file.Read(emi.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 64)));
            }

            emi.IdForNewLine = i;

            DataBase.FileEMI = null;
            DataBase.FileEMI = emi;

            DataBase.NodeEMI.Nodes.Clear();
            DataBase.NodeEMI.MethodsForGL = DataBase.FileEMI.MethodsForGL;
            DataBase.NodeEMI.PropertyMethods = DataBase.FileEMI.Methods;
            DataBase.NodeEMI.DisplayMethods = DataBase.FileEMI.DisplayMethods;
            DataBase.NodeEMI.MoveMethods = DataBase.FileEMI.MoveMethods;
            DataBase.NodeEMI.ChangeAmountMethods = DataBase.FileEMI.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.EMI, iN);
                nodes.Add(o);
            }
            DataBase.NodeEMI.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeEMI.Expand();
            GC.Collect();
        }
       
        public static void LoadFileESE_2007_PS2(FileStream file, FileInfo fileInfo)
        {
            File_ESE_Group ese = new File_ESE_Group(Re4Version.V2007PS2);

            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            ese.StartFile = header;

            if (Amount > Consts.AmountLimitESE)
            {
                Amount = Consts.AmountLimitESE;
                byte[] b = BitConverter.GetBytes(Amount);
                ese.StartFile[0x06] = b[0];
                ese.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[48];
                offset = file.Read(res, 0, 48);
                ese.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ese.Lines.Add(i, new byte[48]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 48))
            {
                ese.EndFile = new byte[fileInfo.Length - (16 + (Amount * 48))];
                file.Read(ese.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 48)));
            }

            ese.IdForNewLine = i;
            ese.SetStartIdexContent();

            DataBase.FileESE = null;
            DataBase.FileESE = ese;

            DataBase.NodeESE.Nodes.Clear();
            DataBase.NodeESE.MethodsForGL = DataBase.FileESE.MethodsForGL;
            DataBase.NodeESE.PropertyMethods = DataBase.FileESE.Methods;
            DataBase.NodeESE.DisplayMethods = DataBase.FileESE.DisplayMethods;
            DataBase.NodeESE.MoveMethods = DataBase.FileESE.MoveMethods;
            DataBase.NodeESE.ChangeAmountMethods = DataBase.FileESE.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ESE, iN);
                nodes.Add(o);
            }
            DataBase.NodeESE.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeESE.Expand();
            GC.Collect();
        }

        public static void LoadFileESE_UHD(FileStream file, FileInfo fileInfo) 
        {
            File_ESE_Group ese = new File_ESE_Group(Re4Version.UHD);

            byte[] header = new byte[16];
            int offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0x06);
            ese.StartFile = header;

            if (Amount > Consts.AmountLimitESE)
            {
                Amount = Consts.AmountLimitESE;
                byte[] b = BitConverter.GetBytes(Amount);
                ese.StartFile[0x06] = b[0];
                ese.StartFile[0x07] = b[1];
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[44];
                offset = file.Read(res, 0, 44);
                ese.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    ese.Lines.Add(i, new byte[44]);
                }
            }

            if (fileInfo.Length > 16 + (Amount * 44))
            {
                ese.EndFile = new byte[fileInfo.Length - (16 + (Amount * 44))];
                file.Read(ese.EndFile, 0, (int)fileInfo.Length - (16 + (Amount * 44)));
            }

            ese.IdForNewLine = i;
            ese.SetStartIdexContent();

            DataBase.FileESE = null;
            DataBase.FileESE = ese;

            DataBase.NodeESE.Nodes.Clear();
            DataBase.NodeESE.MethodsForGL = DataBase.FileESE.MethodsForGL;
            DataBase.NodeESE.PropertyMethods = DataBase.FileESE.Methods;
            DataBase.NodeESE.DisplayMethods = DataBase.FileESE.DisplayMethods;
            DataBase.NodeESE.MoveMethods = DataBase.FileESE.MoveMethods;
            DataBase.NodeESE.ChangeAmountMethods = DataBase.FileESE.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ESE, iN);
                nodes.Add(o);
            }
            DataBase.NodeESE.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeESE.Expand();
            GC.Collect();
        }

        public static void LoadFileQuadCustom(FileStream file, FileInfo fileInfo) 
        {
            byte[] header = new byte[32];
            int offset = file.Read(header, 0, 32);
            string sHeader = Encoding.UTF8.GetString(header.Take(16).ToArray());
            string sVersion = Encoding.UTF8.GetString(header.Skip(16).Take(4).ToArray());
            if (!sHeader.Contains("QUADCUSTOM BIN"))
            {
                return;
            }
            if (!sVersion.Contains("V001"))
            {
                return;
            }

            FileQuadCustomGroup quad = new FileQuadCustomGroup();
            ushort Amount = BitConverter.ToUInt16(header, 0x1E);
            ushort LineArrayLength = BitConverter.ToUInt16(header, 0x1C);

            if (Amount > Consts.AmountLimitQuadCustom)
            {
                Amount = Consts.AmountLimitQuadCustom;
            }

            ushort i = 0;
            for (; i < Amount; i++)
            {
                byte[] res = new byte[LineArrayLength];
                offset = file.Read(res, 0, LineArrayLength);
                if (res.Length > Consts.QuadCustomLineArrayLength)
                {
                    res = res.Take(Consts.QuadCustomLineArrayLength).ToArray();
                }
                else if (res.Length < Consts.QuadCustomLineArrayLength)
                {
                    byte[] nres = new byte[Consts.QuadCustomLineArrayLength];
                    res.CopyTo(nres, 0);
                    res = nres;
                }
                quad.Lines.Add(i, res);

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (i < Amount)
            {
                for (; i < Amount; i++)
                {
                    quad.Lines.Add(i, new byte[Consts.QuadCustomLineArrayLength]);
                }
            }


            ushort j = 0;
            for (; j < Amount; j++)
            {
                byte[] bLength = new byte[2];
                offset = file.Read(bLength, 0, 2);
                ushort uLength = BitConverter.ToUInt16(bLength, 0);

                byte[] bText = new byte[uLength];
                offset = file.Read(bText, 0, uLength);

                if (bText.Length > Consts.QuadCustomTextLength)
                {
                    bText = bText.Take(Consts.QuadCustomTextLength).ToArray();
                }
                quad.Texts.Add(j, Encoding.UTF8.GetString(bText));

                if (offset > fileInfo.Length)
                {
                    break;
                }
            }

            if (j < Amount)
            {
                for (; j < Amount; j++)
                {
                    quad.Texts.Add(j, "Unnamed Object " + j);
                }
            }

            quad.IdForNewLine = i;

            DataBase.FileQuadCustom = null;
            DataBase.FileQuadCustom = quad;

            DataBase.NodeQuadCustom.Nodes.Clear();
            DataBase.NodeQuadCustom.MethodsForGL = DataBase.FileQuadCustom.MethodsForGL;
            DataBase.NodeQuadCustom.PropertyMethods = DataBase.FileQuadCustom.Methods;
            DataBase.NodeQuadCustom.DisplayMethods = DataBase.FileQuadCustom.DisplayMethods;
            DataBase.NodeQuadCustom.MoveMethods = DataBase.FileQuadCustom.MoveMethods;
            DataBase.NodeQuadCustom.ChangeAmountMethods = DataBase.FileQuadCustom.ChangeAmountMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < Amount; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.QUAD_CUSTOM, iN);
                nodes.Add(o);
            }
            DataBase.NodeQuadCustom.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeQuadCustom.Expand();
            GC.Collect();
        }

        #endregion

        #region newFile

        public static void NewFileESL()
        {
            FileEnemyEslGroup esl = new FileEnemyEslGroup();
            for (ushort i = 0; i < 256; i++)
            {
                esl.Lines.Add(i, new byte[32]);
            }

            DataBase.FileESL = null;
            DataBase.FileESL = esl;

            // carregou novo arquivo
            // atualiza node
            DataBase.NodeESL.Nodes.Clear();
            DataBase.NodeESL.MethodsForGL = DataBase.FileESL.MethodsForGL;
            DataBase.NodeESL.PropertyMethods = DataBase.FileESL.Methods;
            DataBase.NodeESL.DisplayMethods = DataBase.FileESL.DisplayMethods;
            DataBase.NodeESL.MoveMethods = DataBase.FileESL.MoveMethods;
            List<Object3D> nodes = new List<Object3D>();
            for (ushort iN = 0; iN < 256; iN++)
            {
                Object3D o = Object3D.CreateNewInstance(GroupType.ESL, iN);
                nodes.Add(o);
            }
            DataBase.NodeESL.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeESL.Expand();
            GC.Collect();
        }

        public static void NewFileETS(Re4Version version) 
        {
            FileEtcModelEtsGroup ets = new FileEtcModelEtsGroup(version);
            byte[] header = new byte[16];
            ets.StartFile = header;

            ets.IdForNewLine = 0;
            ets.CreateNewETS_ID_List();

            DataBase.FileETS = null;
            DataBase.FileETS = ets;

            DataBase.NodeETS.Nodes.Clear();
            DataBase.NodeETS.MethodsForGL = DataBase.FileETS.MethodsForGL;
            DataBase.NodeETS.PropertyMethods = DataBase.FileETS.Methods;
            DataBase.NodeETS.DisplayMethods = DataBase.FileETS.DisplayMethods;
            DataBase.NodeETS.MoveMethods = DataBase.FileETS.MoveMethods;
            DataBase.NodeETS.ChangeAmountMethods = DataBase.FileETS.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileITA(Re4Version version)
        {
            FileSpecialGroup ita = new FileSpecialGroup(version, SpecialFileFormat.ITA);
            byte[] header = new byte[16];
            header[0] = 0x49;
            header[1] = 0x54;
            header[2] = 0x41;
            header[3] = 0x00;
            header[4] = 0x05;
            header[5] = 0x01;
            ita.StartFile = header;
            ita.IdForNewLine = 0;
            ita.SetStartIdexContent();

            DataBase.FileITA = null;
            DataBase.FileITA = ita;
            DataBase.Extras.SetStartRefInteractionTypeContent();

            DataBase.Extras.ClearITAs();
            DataBase.NodeITA.Nodes.Clear();
            DataBase.NodeITA.MethodsForGL = DataBase.FileITA.MethodsForGL;
            DataBase.NodeITA.ExtrasMethodsForGL = DataBase.FileITA.ExtrasMethodsForGL;
            DataBase.NodeITA.PropertyMethods = DataBase.FileITA.Methods;
            DataBase.NodeITA.DisplayMethods = DataBase.FileITA.DisplayMethods;
            DataBase.NodeITA.MoveMethods = DataBase.FileITA.MoveMethods;
            DataBase.NodeITA.ChangeAmountMethods = DataBase.FileITA.ChangeAmountMethods;
            DataBase.Extras.AddITAs();
            GC.Collect();
        }

        public static void NewFileAEV(Re4Version version)
        {
            FileSpecialGroup aev = new FileSpecialGroup(version, SpecialFileFormat.AEV);
            byte[] header = new byte[16];
            header[0] = 0x41;
            header[1] = 0x45;
            header[2] = 0x56;
            header[3] = 0x00;
            header[4] = 0x04;
            header[5] = 0x01;
            aev.StartFile = header;
            aev.IdForNewLine = 0;
            aev.SetStartIdexContent();

            DataBase.FileAEV = null;
            DataBase.FileAEV = aev;
            DataBase.Extras.SetStartRefInteractionTypeContent();

            DataBase.Extras.ClearAll();
            DataBase.NodeAEV.Nodes.Clear();
            DataBase.NodeAEV.MethodsForGL = DataBase.FileAEV.MethodsForGL;
            DataBase.NodeAEV.ExtrasMethodsForGL = DataBase.FileAEV.ExtrasMethodsForGL;
            DataBase.NodeAEV.PropertyMethods = DataBase.FileAEV.Methods;
            DataBase.NodeAEV.DisplayMethods = DataBase.FileAEV.DisplayMethods;
            DataBase.NodeAEV.MoveMethods = DataBase.FileAEV.MoveMethods;
            DataBase.NodeAEV.ChangeAmountMethods = DataBase.FileAEV.ChangeAmountMethods;
            DataBase.Extras.AddAll();
            GC.Collect();
        }

        public static void NewFileDSE() 
        {
            File_DSE_Group dse = new File_DSE_Group();
            dse.IdForNewLine = 0;

            DataBase.FileDSE = null;
            DataBase.FileDSE = dse;

            DataBase.NodeDSE.Nodes.Clear();
            DataBase.NodeDSE.PropertyMethods = DataBase.FileDSE.Methods;
            DataBase.NodeDSE.DisplayMethods = DataBase.FileDSE.DisplayMethods;
            DataBase.NodeDSE.MoveMethods = DataBase.FileDSE.MoveMethods;
            DataBase.NodeDSE.ChangeAmountMethods = DataBase.FileDSE.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileFSE()
        {
            File_FSE_Group fse = new File_FSE_Group();

            byte[] header = new byte[16];
            header[0] = 0x46;
            header[1] = 0x53;
            header[2] = 0x45;
            header[3] = 0x00;
            header[4] = 0x03;
            header[5] = 0x01;
            fse.StartFile = header;
            fse.IdForNewLine = 0;
            fse.SetStartIdexContent();

            DataBase.FileFSE = null;
            DataBase.FileFSE = fse;

            DataBase.NodeFSE.Nodes.Clear();
            DataBase.NodeFSE.MethodsForGL = DataBase.FileFSE.MethodsForGL;
            DataBase.NodeFSE.PropertyMethods = DataBase.FileFSE.Methods;
            DataBase.NodeFSE.DisplayMethods = DataBase.FileFSE.DisplayMethods;
            DataBase.NodeFSE.MoveMethods = DataBase.FileFSE.MoveMethods;
            DataBase.NodeFSE.ChangeAmountMethods = DataBase.FileFSE.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileSAR()
        {
            File_ESAR_Group sar = new File_ESAR_Group(EsarFileFormat.SAR);

            byte[] header = new byte[16];
            sar.StartFile = header;

            sar.IdForNewLine = 0;
            sar.SetStartIdexContent();

            DataBase.FileSAR = null;
            DataBase.FileSAR = sar;

            DataBase.NodeSAR.Nodes.Clear();
            DataBase.NodeSAR.MethodsForGL = DataBase.FileSAR.MethodsForGL;
            DataBase.NodeSAR.PropertyMethods = DataBase.FileSAR.Methods;
            DataBase.NodeSAR.DisplayMethods = DataBase.FileSAR.DisplayMethods;
            DataBase.NodeSAR.MoveMethods = DataBase.FileSAR.MoveMethods;
            DataBase.NodeSAR.ChangeAmountMethods = DataBase.FileSAR.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileEAR() 
        {
            File_ESAR_Group ear = new File_ESAR_Group(EsarFileFormat.EAR);

            byte[] header = new byte[16];
            ear.StartFile = header;

            ear.IdForNewLine = 0;
            ear.SetStartIdexContent();

            DataBase.FileEAR = null;
            DataBase.FileEAR = ear;

            DataBase.NodeEAR.Nodes.Clear();
            DataBase.NodeEAR.MethodsForGL = DataBase.FileEAR.MethodsForGL;
            DataBase.NodeEAR.PropertyMethods = DataBase.FileEAR.Methods;
            DataBase.NodeEAR.DisplayMethods = DataBase.FileEAR.DisplayMethods;
            DataBase.NodeEAR.MoveMethods = DataBase.FileEAR.MoveMethods;
            DataBase.NodeEAR.ChangeAmountMethods = DataBase.FileEAR.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileEMI(Re4Version version)
        {
            File_EMI_Group emi = new File_EMI_Group(version);

            byte[] header = new byte[16];
            if (version == Re4Version.UHD)
            {
                header = new byte[8];
            }
            emi.StartFile = header;
            emi.IdForNewLine = 0;

            DataBase.FileEMI = null;
            DataBase.FileEMI = emi;

            DataBase.NodeEMI.Nodes.Clear();
            DataBase.NodeEMI.MethodsForGL = DataBase.FileEMI.MethodsForGL;
            DataBase.NodeEMI.PropertyMethods = DataBase.FileEMI.Methods;
            DataBase.NodeEMI.DisplayMethods = DataBase.FileEMI.DisplayMethods;
            DataBase.NodeEMI.MoveMethods = DataBase.FileEMI.MoveMethods;
            DataBase.NodeEMI.ChangeAmountMethods = DataBase.FileEMI.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileESE(Re4Version version)
        {
            File_ESE_Group ese = new File_ESE_Group(version);
            byte[] header = new byte[16];
            header[0] = 0x45;
            header[1] = 0x53;
            header[2] = 0x45;
            header[3] = 0x00;
            header[4] = 0x00;
            header[5] = 0x01;
            ese.StartFile = header;
            ese.IdForNewLine = 0;
            ese.SetStartIdexContent();

            DataBase.FileESE = null;
            DataBase.FileESE = ese;

            DataBase.NodeESE.Nodes.Clear();
            DataBase.NodeESE.MethodsForGL = DataBase.FileESE.MethodsForGL;
            DataBase.NodeESE.PropertyMethods = DataBase.FileESE.Methods;
            DataBase.NodeESE.DisplayMethods = DataBase.FileESE.DisplayMethods;
            DataBase.NodeESE.MoveMethods = DataBase.FileESE.MoveMethods;
            DataBase.NodeESE.ChangeAmountMethods = DataBase.FileESE.ChangeAmountMethods;
            GC.Collect();
        }

        public static void NewFileQuadCustom() 
        {
            FileQuadCustomGroup quad = new FileQuadCustomGroup();

            DataBase.FileQuadCustom = null;
            DataBase.FileQuadCustom = quad;

            DataBase.NodeQuadCustom.Nodes.Clear();
            DataBase.NodeQuadCustom.MethodsForGL = DataBase.FileQuadCustom.MethodsForGL;
            DataBase.NodeQuadCustom.PropertyMethods = DataBase.FileQuadCustom.Methods;
            DataBase.NodeQuadCustom.DisplayMethods = DataBase.FileQuadCustom.DisplayMethods;
            DataBase.NodeQuadCustom.MoveMethods = DataBase.FileQuadCustom.MoveMethods;
            DataBase.NodeQuadCustom.ChangeAmountMethods = DataBase.FileQuadCustom.ChangeAmountMethods;
            GC.Collect();
        }

        #endregion

        #region Clear

        public static void ClearESL()
        {
            DataBase.NodeESL.Nodes.Clear();
            DataBase.NodeESL.MethodsForGL = null;
            DataBase.NodeESL.PropertyMethods = null;
            DataBase.NodeESL.DisplayMethods = null;
            DataBase.NodeESL.MoveMethods = null;
            DataBase.FileESL = null;
            GC.Collect();
        }

        public static void ClearETS()
        {
            DataBase.NodeETS.Nodes.Clear();
            DataBase.NodeETS.MethodsForGL = null;
            DataBase.NodeETS.PropertyMethods = null;
            DataBase.NodeETS.DisplayMethods = null;
            DataBase.NodeETS.MoveMethods = null;
            DataBase.NodeETS.ChangeAmountMethods = null;
            DataBase.FileETS = null;
            GC.Collect();
        }

        public static void ClearITA()
        {      
            DataBase.Extras.ClearITAs();
            DataBase.NodeITA.Nodes.Clear();
            DataBase.NodeITA.MethodsForGL = null;
            DataBase.NodeITA.ExtrasMethodsForGL = null;
            DataBase.NodeITA.PropertyMethods = null;
            DataBase.NodeITA.DisplayMethods = null;
            DataBase.NodeITA.MoveMethods = null;
            DataBase.NodeITA.ChangeAmountMethods = null;
            DataBase.FileITA = null;
            DataBase.Extras.SetStartRefInteractionTypeContent();
            GC.Collect();
        }

        public static void ClearAEV()
        {
            DataBase.Extras.ClearAll();
            DataBase.NodeAEV.Nodes.Clear();
            DataBase.NodeAEV.MethodsForGL = null;
            DataBase.NodeAEV.ExtrasMethodsForGL = null;
            DataBase.NodeAEV.PropertyMethods = null;
            DataBase.NodeAEV.DisplayMethods = null;
            DataBase.NodeAEV.MoveMethods = null;
            DataBase.NodeAEV.ChangeAmountMethods = null;
            DataBase.FileAEV = null;
            DataBase.Extras.AddAll();
            DataBase.Extras.SetStartRefInteractionTypeContent();
            GC.Collect();
        }

        public static void ClearDSE()
        {
            DataBase.NodeDSE.Nodes.Clear();
            DataBase.NodeDSE.PropertyMethods = null;
            DataBase.NodeDSE.DisplayMethods = null;
            DataBase.NodeDSE.MoveMethods = null;
            DataBase.NodeDSE.ChangeAmountMethods = null;
            DataBase.FileDSE = null;
            GC.Collect();
        }

        public static void ClearFSE()
        {
            DataBase.NodeFSE.Nodes.Clear();
            DataBase.NodeFSE.MethodsForGL = null;
            DataBase.NodeFSE.PropertyMethods = null;
            DataBase.NodeFSE.DisplayMethods = null;
            DataBase.NodeFSE.MoveMethods = null;
            DataBase.NodeFSE.ChangeAmountMethods = null;
            DataBase.FileFSE = null;
            GC.Collect();
        }

        public static void ClearSAR()
        {
            DataBase.NodeSAR.Nodes.Clear();
            DataBase.NodeSAR.MethodsForGL = null;
            DataBase.NodeSAR.PropertyMethods = null;
            DataBase.NodeSAR.DisplayMethods = null;
            DataBase.NodeSAR.MoveMethods = null;
            DataBase.NodeSAR.ChangeAmountMethods = null;
            DataBase.FileSAR = null;
            GC.Collect();
        }

        public static void ClearEAR()
        {
            DataBase.NodeEAR.Nodes.Clear();
            DataBase.NodeEAR.MethodsForGL = null;
            DataBase.NodeEAR.PropertyMethods = null;
            DataBase.NodeEAR.DisplayMethods = null;
            DataBase.NodeEAR.MoveMethods = null;
            DataBase.NodeEAR.ChangeAmountMethods = null;
            DataBase.FileEAR = null;
            GC.Collect();
        }

        public static void ClearEMI()
        {
            DataBase.NodeEMI.Nodes.Clear();
            DataBase.NodeEMI.MethodsForGL = null;
            DataBase.NodeEMI.PropertyMethods = null;
            DataBase.NodeEMI.DisplayMethods = null;
            DataBase.NodeEMI.MoveMethods = null;
            DataBase.NodeEMI.ChangeAmountMethods = null;
            DataBase.FileEMI = null;
            GC.Collect();
        }

        public static void ClearESE()
        {
            DataBase.NodeESE.Nodes.Clear();
            DataBase.NodeESE.MethodsForGL = null;
            DataBase.NodeESE.PropertyMethods = null;
            DataBase.NodeESE.DisplayMethods = null;
            DataBase.NodeESE.MoveMethods = null;
            DataBase.NodeESE.ChangeAmountMethods = null;
            DataBase.FileESE = null;
            GC.Collect();
        }

        public static void ClearQuadCustom()
        {
            DataBase.NodeQuadCustom.Nodes.Clear();
            DataBase.NodeQuadCustom.MethodsForGL = null;
            DataBase.NodeQuadCustom.PropertyMethods = null;
            DataBase.NodeQuadCustom.DisplayMethods = null;
            DataBase.NodeQuadCustom.MoveMethods = null;
            DataBase.NodeQuadCustom.ChangeAmountMethods = null;
            DataBase.FileQuadCustom = null;
            GC.Collect();
        }

        #endregion

        #region save as

        public static void SaveFileESL(FileStream stream) 
        {
            if (DataBase.FileESL != null && DataBase.FileESL.Lines != null)
            {
                for (ushort i = 0; i < 256; i++)
                {
                    byte[] b = DataBase.FileESL.Lines[i];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileESL.EndFile != null && DataBase.FileESL.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileESL.EndFile, 0, DataBase.FileESL.EndFile.Length);
                }
            }
        }

        public static void SaveFileETS(FileStream stream)
        {
            if (DataBase.FileETS != null && DataBase.FileETS.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileETS.Lines.Count);
                DataBase.FileETS.StartFile[0x00] = lenght[0];
                DataBase.FileETS.StartFile[0x01] = lenght[1];

                stream.Write(DataBase.FileETS.StartFile, 0, DataBase.FileETS.StartFile.Length);

                var nodes = DataBase.NodeETS.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileETS.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileETS.EndFile != null && DataBase.FileETS.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileETS.EndFile, 0, DataBase.FileETS.EndFile.Length);
                }
            }
        }

        public static void SaveFileITA(FileStream stream)
        {
            if (DataBase.FileITA != null && DataBase.FileITA.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileITA.Lines.Count);
                DataBase.FileITA.StartFile[0x06] = lenght[0];
                DataBase.FileITA.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileITA.StartFile, 0, DataBase.FileITA.StartFile.Length);

                var nodes = DataBase.NodeITA.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileITA.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileITA.EndFile != null && DataBase.FileITA.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileITA.EndFile, 0, DataBase.FileITA.EndFile.Length);
                }
            }
        }

        public static void SaveFileAEV(FileStream stream)
        {
            if (DataBase.FileAEV != null && DataBase.FileAEV.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileAEV.Lines.Count);
                DataBase.FileAEV.StartFile[0x06] = lenght[0];
                DataBase.FileAEV.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileAEV.StartFile, 0, DataBase.FileAEV.StartFile.Length);

                var nodes = DataBase.NodeAEV.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileAEV.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileAEV.EndFile != null && DataBase.FileAEV.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileAEV.EndFile, 0, DataBase.FileAEV.EndFile.Length);
                }
            }
        }

        public static void SaveFileDSE(FileStream stream)
        {
            if (DataBase.FileDSE != null && DataBase.FileDSE.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes((uint)DataBase.FileDSE.Lines.Count);
               
                stream.Write(lenght, 0, lenght.Length);

                var nodes = DataBase.NodeDSE.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileDSE.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileDSE.EndFile != null && DataBase.FileDSE.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileDSE.EndFile, 0, DataBase.FileDSE.EndFile.Length);
                }
            }
        }

        public static void SaveFileFSE(FileStream stream)
        {
            if (DataBase.FileFSE != null && DataBase.FileFSE.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileFSE.Lines.Count);
                DataBase.FileFSE.StartFile[0x06] = lenght[0];
                DataBase.FileFSE.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileFSE.StartFile, 0, DataBase.FileFSE.StartFile.Length);

                var nodes = DataBase.NodeFSE.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileFSE.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileFSE.EndFile != null && DataBase.FileFSE.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileFSE.EndFile, 0, DataBase.FileFSE.EndFile.Length);
                }
            }
        }

        public static void SaveFileSAR(FileStream stream)
        {
            if (DataBase.FileSAR != null && DataBase.FileSAR.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileSAR.Lines.Count);
                DataBase.FileSAR.StartFile[0x00] = lenght[0];
                DataBase.FileSAR.StartFile[0x01] = lenght[1];
                DataBase.FileSAR.StartFile[0x02] = 0x00;
                DataBase.FileSAR.StartFile[0x03] = 0x00;

                stream.Write(DataBase.FileSAR.StartFile, 0, DataBase.FileSAR.StartFile.Length);

                var nodes = DataBase.NodeSAR.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileSAR.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileSAR.EndFile != null && DataBase.FileSAR.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileSAR.EndFile, 0, DataBase.FileSAR.EndFile.Length);
                }
            }
        }

        public static void SaveFileEAR(FileStream stream)
        {
            if (DataBase.FileEAR != null && DataBase.FileEAR.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileEAR.Lines.Count);
                DataBase.FileEAR.StartFile[0x00] = lenght[0];
                DataBase.FileEAR.StartFile[0x01] = lenght[1];
                DataBase.FileEAR.StartFile[0x02] = 0x00;
                DataBase.FileEAR.StartFile[0x03] = 0x00;

                stream.Write(DataBase.FileEAR.StartFile, 0, DataBase.FileEAR.StartFile.Length);

                var nodes = DataBase.NodeEAR.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileEAR.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileEAR.EndFile != null && DataBase.FileEAR.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileEAR.EndFile, 0, DataBase.FileEAR.EndFile.Length);
                }
            }
        }

        public static void SaveFileEMI(FileStream stream)
        {
            if (DataBase.FileEMI != null && DataBase.FileEMI.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileEMI.Lines.Count);
                DataBase.FileEMI.StartFile[0x00] = lenght[0];
                DataBase.FileEMI.StartFile[0x01] = lenght[1];
                DataBase.FileEMI.StartFile[0x02] = 0x00;
                DataBase.FileEMI.StartFile[0x03] = 0x00;

                stream.Write(DataBase.FileEMI.StartFile, 0, DataBase.FileEMI.StartFile.Length);

                var nodes = DataBase.NodeEMI.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileEMI.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileEMI.EndFile != null && DataBase.FileEMI.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileEMI.EndFile, 0, DataBase.FileEMI.EndFile.Length);
                }
            }
        }

        public static void SaveFileESE(FileStream stream)
        {
            if (DataBase.FileESE != null && DataBase.FileESE.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileESE.Lines.Count);
                DataBase.FileESE.StartFile[0x06] = lenght[0];
                DataBase.FileESE.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileESE.StartFile, 0, DataBase.FileESE.StartFile.Length);

                var nodes = DataBase.NodeESE.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileESE.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileESE.EndFile != null && DataBase.FileESE.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileESE.EndFile, 0, DataBase.FileESE.EndFile.Length);
                }
            }
        }

        public static void SaveFileQuadCustom(FileStream stream) 
        {
            if (DataBase.FileQuadCustom != null && DataBase.FileQuadCustom.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileQuadCustom.Lines.Count);
                byte[] byteArryLenght = BitConverter.GetBytes(Consts.QuadCustomLineArrayLength);

                byte[] header = new byte[32];
                header[0x00] = 0x51;//Q
                header[0x01] = 0x55;//U
                header[0x02] = 0x41;//A
                header[0x03] = 0x44;//D
                header[0x04] = 0x43;//C
                header[0x05] = 0x55;//U
                header[0x06] = 0x53;//S
                header[0x07] = 0x54;//T
                header[0x08] = 0x4F;//O
                header[0x09] = 0x4D;//M
                header[0x0A] = 0x20;// 
                header[0x0B] = 0x42;//B
                header[0x0C] = 0x49;//I
                header[0x0D] = 0x4E;//N
                header[0x0E] = 0x0D;//new line
                header[0x0F] = 0x0A;//new line
                header[0x10] = 0x56;//V
                header[0x11] = 0x30;//0
                header[0x12] = 0x30;//0
                header[0x13] = 0x31;//1
                header[0x14] = 0x00;
                header[0x15] = 0x00;
                header[0x16] = 0x00;
                header[0x17] = 0x00;
                header[0x18] = 0x00;
                header[0x19] = 0x00;
                header[0x1A] = 0x00;
                header[0x1B] = 0x00;
                header[0x1C] = byteArryLenght[0];
                header[0x1D] = byteArryLenght[1];
                header[0x1E] = lenght[0];
                header[0x1F] = lenght[1];

                stream.Write(header, 0, header.Length);

                var nodes = DataBase.NodeQuadCustom.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileQuadCustom.Lines[Order[i]];
                    stream.Write(b, 0, b.Length);
                }

                for (int i = 0; i < Order.Length; i++)
                {
                    string res = DataBase.FileQuadCustom.Texts[Order[i]];
                    if (res.Length > Consts.QuadCustomTextLength)
                    {
                        res.Substring(0, Consts.QuadCustomTextLength);
                    }
                    byte[] b = Encoding.UTF8.GetBytes(res);
                    byte[] length = BitConverter.GetBytes((ushort)b.Length);
                    stream.Write(length, 0, length.Length);
                    stream.Write(b, 0, b.Length);
                }
            }
        }

        #endregion

        #region saveConvert

        public static void SaveConvertFileETS(FileStream stream) 
        {
            if (DataBase.FileETS != null && DataBase.FileETS.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileETS.Lines.Count);
                DataBase.FileETS.StartFile[0x00] = lenght[0];
                DataBase.FileETS.StartFile[0x01] = lenght[1];

                stream.Write(DataBase.FileETS.StartFile, 0, DataBase.FileETS.StartFile.Length);

                var nodes = DataBase.NodeETS.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = ConvertLineETS(DataBase.FileETS.Lines[Order[i]], DataBase.FileETS.GetRe4Version);
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileETS.EndFile != null && DataBase.FileETS.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileETS.EndFile, 0, DataBase.FileETS.EndFile.Length);
                }
            }
        }

        private static byte[] ConvertLineETS(byte[] line, Re4Version from) 
        {
            if (from == Re4Version.V2007PS2) // convert to UHD
            {
                byte[] res = new byte[40]; // UHD lenght;
                //ETCMODEL_ID
                res[0x00] = line[0x30];
                res[0x01] = line[0x31];
                //ETS_ID
                res[0x02] = line[0x32];
                res[0x03] = line[0x33];
                //U_TTS
                line.Take(12).ToArray().CopyTo(res, 0x04);
                //angle
                line.Skip(0x10).Take(12).ToArray().CopyTo(res, 0x10);
                //position
                line.Skip(0x20).Take(12).ToArray().CopyTo(res, 0x1C);

                return res;
            }
            else if (from == Re4Version.UHD) // Convert To Classic
            {
                byte[] res = new byte[64]; //classic lenght
                //ETCMODEL_ID
                res[0x30] = line[0x00];
                res[0x31] = line[0x01];
                //ETS_ID
                res[0x32] = line[0x02];
                res[0x33] = line[0x03];
                //U_TTS
                line.Skip(0x04).Take(12).ToArray().CopyTo(res, 0x00);
                //angle
                line.Skip(0X10).Take(12).ToArray().CopyTo(res, 0x10);
                //position
                line.Skip(0x1C).Take(12).ToArray().CopyTo(res, 0x20);

                return res;
            }
            // from == null
            return line;
        }

        public static void SaveConvertFileITA(FileStream stream)
        {
            if (DataBase.FileITA != null && DataBase.FileITA.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileITA.Lines.Count);
                DataBase.FileITA.StartFile[0x06] = lenght[0];
                DataBase.FileITA.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileITA.StartFile, 0, DataBase.FileITA.StartFile.Length);

                var nodes = DataBase.NodeITA.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = ConvertLineSpecial(DataBase.FileITA.Lines[Order[i]], DataBase.FileITA.GetRe4Version, DataBase.FileITA.GetSpecialFileFormat);
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileITA.EndFile != null && DataBase.FileITA.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileITA.EndFile, 0, DataBase.FileITA.EndFile.Length);
                }
            }
        }

        public static void SaveConvertFileAEV(FileStream stream)
        {
            if (DataBase.FileAEV != null && DataBase.FileAEV.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileAEV.Lines.Count);
                DataBase.FileAEV.StartFile[0x06] = lenght[0];
                DataBase.FileAEV.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileAEV.StartFile, 0, DataBase.FileAEV.StartFile.Length);

                var nodes = DataBase.NodeAEV.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = ConvertLineSpecial(DataBase.FileAEV.Lines[Order[i]], DataBase.FileAEV.GetRe4Version, DataBase.FileAEV.GetSpecialFileFormat);
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileAEV.EndFile != null && DataBase.FileAEV.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileAEV.EndFile, 0, DataBase.FileAEV.EndFile.Length);
                }
            }
        }

        private static byte[] ConvertLineSpecial(byte[] line, Re4Version from, SpecialFileFormat fileFormat) 
        {
            byte[] res = new byte[0];

            Re4Version to = Re4Version.NULL;

            if (from == Re4Version.V2007PS2) // to UHD
            {
                to = Re4Version.UHD;
                res = new byte[156];
              
            }
            else if (from == Re4Version.UHD) // to classic
            {
                to = Re4Version.V2007PS2;
                if (fileFormat == SpecialFileFormat.AEV)
                {
                    res = new byte[160];
                }
                else if (fileFormat == SpecialFileFormat.ITA)
                {
                    res = new byte[176];
                }
            }

            //start line fixo
            line.Take(0x5C).ToArray().CopyTo(res, 0);

            //
            byte specialType = line[0x35];

            switch (specialType)
            {
                case 0x03: //T03_Items
                    if (to == Re4Version.V2007PS2)
                    {
                        line.Skip(0x5C).Take(28).ToArray().CopyTo(res, 0x60);
                        res[0x7C] = 0x3F;
                        res[0x7D] = 0x80;
                        //res[0x7E] = 0x00;
                        //res[0x7F] = 0x00;
                        line.Skip(0x78).Take(28).ToArray().CopyTo(res, 0x84);
                        if (fileFormat == SpecialFileFormat.ITA)
                        {
                            line.Skip(0x94).Take(8).ToArray().CopyTo(res, 0xA0);
                        }
                        if (BitConverter.ToUInt32(res, 0x6C) == 0x0)
                        {
                            res[0x6C] = 0x3F;
                            res[0x6D] = 0x80;
                            res[0x6E] = 0x00;
                            res[0x6F] = 0x00;
                        }
                    }
                    else if (to == Re4Version.UHD)
                    {
                        line.Skip(0x60).Take(28).ToArray().CopyTo(res, 0x5C);
                        line.Skip(0x84).Take(28).ToArray().CopyTo(res, 0x78);
                        if (fileFormat == SpecialFileFormat.ITA)
                        {
                            line.Skip(0xA0).Take(8).ToArray().CopyTo(res, 0x94);
                        }
                        if (BitConverter.ToUInt32(res, 0x68) == 0x803F)
                        {
                            res[0x68] = 0x00;
                            res[0x69] = 0x00;
                            res[0x6A] = 0x00;
                            res[0x6B] = 0x00;

                        }
                    }
                        break;
                case 0x13: //T13_LocalTeleportation 
                case 0x10: //T10_FixedLadderClimbUp
                    if (to == Re4Version.V2007PS2)
                    {
                        line.Skip(0x5C).Take(12).ToArray().CopyTo(res, 0x60);
                        res[0x6C] = 0x3F;
                        res[0x6D] = 0x80;
                        //res[0x6E] = 0x00;
                        //res[0x6F] = 0x00;
                        line.Skip(0x68).Take(48).ToArray().CopyTo(res, 0x70);
                    }
                    else if (to == Re4Version.UHD)
                    {
                        line.Skip(0x60).Take(12).ToArray().CopyTo(res, 0x5C);
                        line.Skip(0x70).Take(48).ToArray().CopyTo(res, 0x68);
                    }
                    break;
                case 0x12: //T12_AshleyHideCommand
                    if (to == Re4Version.V2007PS2)
                    {
                        line.Skip(0x80).Take(12).ToArray().CopyTo(res,0x60);
                        res[0x6C] = 0x3F;
                        res[0x6D] = 0x80;
                        //res[0x6E] = 0x00;
                        //res[0x6F] = 0x00;
                        line.Skip(0x5C).Take(36).ToArray().CopyTo(res, 0x70);
                        line.Skip(0x8C).Take(12).ToArray().CopyTo(res, 0x94);
                    }
                    else if (to == Re4Version.UHD)
                    {
                        line.Skip(0x70).Take(36).ToArray().CopyTo(res, 0x5C);
                        line.Skip(0x60).Take(12).ToArray().CopyTo(res, 0x80);
                        line.Skip(0x94).Take(12).ToArray().CopyTo(res, 0x8C);
                    }
                    break;
                case 0x15: //T15_AdaGrappleGun
                    if (to == Re4Version.V2007PS2)
                    {
                        line.Skip(0x5C).Take(12).ToArray().CopyTo(res,0x60);
                        res[0x6C] = 0x3F;
                        res[0x6D] = 0x80;
                        //res[0x6E] = 0x00;
                        //res[0x6F] = 0x00;
                        line.Skip(0x68).Take(12).ToArray().CopyTo(res, 0x70);
                        res[0x7C] = 0x3F;
                        res[0x7D] = 0x80;
                        //res[0x7E] = 0x00;
                        //res[0x7F] = 0x00;
                        line.Skip(0x74).Take(12).ToArray().CopyTo(res, 0x80);
                        res[0x8C] = 0x3F;
                        res[0x8D] = 0x80;
                        //res[0x8E] = 0x00;
                        //res[0x8F] = 0x00;
                        line.Skip(0x80).Take(16).ToArray().CopyTo(res, 0x90);
                    }
                    else if (to == Re4Version.UHD)
                    {
                        line.Skip(0x60).Take(12).ToArray().CopyTo(res, 0x5C);
                        line.Skip(0x70).Take(12).ToArray().CopyTo(res, 0x68);
                        line.Skip(0x80).Take(12).ToArray().CopyTo(res, 0x74);
                        line.Skip(0x90).Take(16).ToArray().CopyTo(res, 0x80);
                    }
                    break;
                //case 0x00: //T00_GeneralPurpose 
                //case 0x01: //T01_WarpDoor
                //case 0x02: //T02_CutSceneEvents
                //case 0x04: //T04_GroupedEnemyTrigger
                //case 0x05: //T05_Message
                //case 0x06: //T06_Unused
                //case 0x07: //T07_Unused
                //case 0x08: //T08_TypeWriter
                //case 0x09: //T09_Unused
                //case 0x0A: //T0A_DamagesThePlayer
                //case 0x0B: //T0B_FalseCollision
                //case 0x0C: //T0C_Unused
                //case 0x0D: //T0D_Unknown
                //case 0x0E: //T0E_Crouch
                //case 0x0F: //T0F_Unused
                //case 0x11: //T11_ItemDependentEvents
                //case 0x14: //T14_UsedForElevators
                default:
                    if (to == Re4Version.V2007PS2)
                    {
                        line.Skip(0x5C).Take(64).ToArray().CopyTo(res, 0x60);
                    }
                    else if (to == Re4Version.UHD)
                    {
                        line.Skip(0x60).Take(64).ToArray().CopyTo(res, 0x5C);
                    }
                    break;
            }


            if (res.Length != 0)
            {
                return res;
            }
            return line;
        }

        public static void SaveConvertFileEMI(FileStream stream)
        {
            if (DataBase.FileEMI != null && DataBase.FileEMI.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileEMI.Lines.Count);
                byte[] header = new byte[2];

                if (DataBase.FileEMI.GetRe4Version == Re4Version.V2007PS2)
                {
                    // to UHD
                    header = new byte[8];
                }
                else if (DataBase.FileEMI.GetRe4Version == Re4Version.UHD)
                {
                    // to Classic
                    header = new byte[16];
                }

                header[0x00] = lenght[0];
                header[0x01] = lenght[1];

                stream.Write(header, 0, header.Length);

                var nodes = DataBase.NodeEMI.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = ConvertLineEMI(DataBase.FileEMI.Lines[Order[i]], DataBase.FileEMI.GetRe4Version);
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileEMI.EndFile != null && DataBase.FileEMI.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileEMI.EndFile, 0, DataBase.FileEMI.EndFile.Length);
                }
            }

        }

        private static byte[] ConvertLineEMI(byte[] line, Re4Version from) 
        {
            // todos os dois são 64 bytes de tamanho 
            // UHD 0x14 até final, tem nada zempre 00
            // classic 0x18 até final, tem nada zempre 00
            // position 12 bytes UHD 0x04 classic 0x00
            // classic 4 bytes fixo em 0x0C valor 00 00 80 3F
            // index? 4 bytes uhd 0x00 classic 0x14

            byte[] res = new byte[64];

            if (from == Re4Version.V2007PS2) // to UHD
            {
                //to Re4Version.UHD;
                line.Take(12).ToArray().CopyTo(res, 0x04); //position
                line.Skip(0x10).Take(4).ToArray().CopyTo(res, 0x10); //angleY
                line.Skip(0x14).Take(4).ToArray().CopyTo(res, 0x00); //index?
            }
            else if (from == Re4Version.UHD) // to classic
            {
                //to Re4Version.V2007PS2;
                line.Skip(0x04).Take(12).ToArray().CopyTo(res, 0x00); //position
                res[0x0E] = 0x80;
                res[0x0F] = 0x3F;
                line.Skip(0x10).Take(4).ToArray().CopyTo(res, 0x10); //angleY
                line.Skip(0x00).Take(4).ToArray().CopyTo(res, 0x14); //index?
            }

            // a parte zerada, acho que é padding, então estou ignorando.
            return res;
        }

        public static void SaveConvertFileESE(FileStream stream)
        {
            if (DataBase.FileESE != null && DataBase.FileESE.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileESE.Lines.Count);
                DataBase.FileESE.StartFile[0x06] = lenght[0];
                DataBase.FileESE.StartFile[0x07] = lenght[1];

                stream.Write(DataBase.FileESE.StartFile, 0, DataBase.FileESE.StartFile.Length);

                var nodes = DataBase.NodeESE.Nodes.Cast<Object3D>();
                ushort[] Order = (from obj in nodes select obj.ObjLineRef).ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = ConvertLineESE(DataBase.FileESE.Lines[Order[i]], DataBase.FileESE.GetRe4Version);
                    stream.Write(b, 0, b.Length);
                }

                if (DataBase.FileESE.EndFile != null && DataBase.FileESE.EndFile.Length > 0)
                {
                    stream.Write(DataBase.FileESE.EndFile, 0, DataBase.FileESE.EndFile.Length);
                }
            }

        }

        private static byte[] ConvertLineESE(byte[] line, Re4Version from) 
        {
            // position 12 bytes UHD 0x04 classic 0x00
            // classic 4 bytes fixo em 0x0C valor 3F 80 00 00
            // index? 4 bytes uhd 0x00 classic 0x10
            // UHD de 0x10 pra frete vai para 0x14
            // classic de 0x14 pra frente volta para 0x10

            byte[] res = new byte[0];

            if (from == Re4Version.V2007PS2) // to UHD
            {
                //to Re4Version.UHD;
                res = new byte[44];
                line.Take(12).ToArray().CopyTo(res, 0x04); //position
                line.Skip(0x10).Take(4).ToArray().CopyTo(res, 0x00); //index?
                line.Skip(0x14).Take(0x1C).ToArray().CopyTo(res, 0x10); // end
            }
            else if (from == Re4Version.UHD) // to classic
            {
                //to Re4Version.V2007PS2;
                res = new byte[48];
                line.Skip(0x04).Take(12).ToArray().CopyTo(res, 0x00); //position
                res[0x0C] = 0x3F;
                res[0x0D] = 0x80;
                line.Take(4).ToArray().CopyTo(res, 0x10); //index?
                line.Skip(0x10).Take(0x1C).ToArray().CopyTo(res, 0x14);// end
            }
                                        
            if (res.Length != 0)
            {
                return res;
            }
            return line;
        }

        #endregion

    }
}
