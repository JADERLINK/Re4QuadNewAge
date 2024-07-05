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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.ESL;
                o.ObjLineRef = iN;
                nodes.Add(o);
            }
            DataBase.NodeESL.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeESL.Expand();
            GC.Collect();
        }

        public static void LoadFileETS_Classic(FileStream file, FileInfo fileInfo) 
        {
            FileEtcModelEtsGroup ets = new FileEtcModelEtsGroup(Re4Version.Classic);
            int offset = 0;
            byte[] header = new byte[16];
            offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0);
            ets.StartFile = header;

            if (Amount > 10000)
            {
                Amount = 10000;
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.ETS;
                o.ObjLineRef = iN;
                nodes.Add(o);
            }
            DataBase.NodeETS.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeETS.Expand();
            GC.Collect();
        }

        public static void LoadFileETS_UHD(FileStream file, FileInfo fileInfo)
        {
            FileEtcModelEtsGroup ets = new FileEtcModelEtsGroup(Re4Version.UHD);
            int offset = 0;
            byte[] header = new byte[16];
            offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 0);
            ets.StartFile = header;

            if (Amount > 10000)
            {
                Amount = 10000;
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.ETS;
                o.ObjLineRef = iN;
                nodes.Add(o);
            }
            DataBase.NodeETS.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeETS.Expand();
            GC.Collect();
        }

        public static void LoadFileITA_Classic(FileStream file, FileInfo fileInfo)
        {
            FileSpecialGroup ita = new FileSpecialGroup(Re4Version.Classic, SpecialFileFormat.ITA);
            int offset = 0;
            byte[] header = new byte[16];
            offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 6);
            ita.StartFile = header;

            if (Amount > 10000)
            {
                Amount = 10000;
                byte[] b = BitConverter.GetBytes(Amount);
                ita.StartFile[0x00] = b[0];
                ita.StartFile[0x01] = b[1];
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.ITA;
                o.ObjLineRef = iN;
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
            int offset = 0;
            byte[] header = new byte[16];
            offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 6);
            ita.StartFile = header;

            if (Amount > 10000)
            {
                Amount = 10000;
                byte[] b = BitConverter.GetBytes(Amount);
                ita.StartFile[0x00] = b[0];
                ita.StartFile[0x01] = b[1];
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.ITA;
                o.ObjLineRef = iN;
                nodes.Add(o);
            }
            DataBase.NodeITA.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeITA.Expand();
            DataBase.Extras.AddITAs();
            DataBase.NodeEXTRAS.Expand();
            GC.Collect();
        }

        public static void LoadFileAEV_Classic(FileStream file, FileInfo fileInfo)
        {
            FileSpecialGroup aev = new FileSpecialGroup(Re4Version.Classic, SpecialFileFormat.AEV);
            int offset = 0;
            byte[] header = new byte[16];
            offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 6);
            aev.StartFile = header;

            if (Amount > 10000)
            {
                Amount = 10000;
                byte[] b = BitConverter.GetBytes(Amount);
                aev.StartFile[0x00] = b[0];
                aev.StartFile[0x01] = b[1];
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.AEV;
                o.ObjLineRef = iN;
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
            int offset = 0;
            byte[] header = new byte[16];
            offset = file.Read(header, 0, 16);
            ushort Amount = BitConverter.ToUInt16(header, 6);
            aev.StartFile = header;

            if (Amount > 10000)
            {
                Amount = 10000;
                byte[] b = BitConverter.GetBytes(Amount);
                aev.StartFile[0x00] = b[0];
                aev.StartFile[0x01] = b[1];
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.AEV;
                o.ObjLineRef = iN;
                nodes.Add(o);
            }
            DataBase.NodeAEV.Nodes.AddRange(nodes.ToArray());
            DataBase.NodeAEV.Expand();
            DataBase.Extras.AddAll();
            DataBase.NodeEXTRAS.Expand();
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
                Object3D o = new Object3D();
                o.Name = iN.ToString();
                o.Text = "";
                o.Group = GroupType.ESL;
                o.ObjLineRef = iN;
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


        #endregion

        #region save as

        public static void SaveFileESL(FileStream stream) 
        {
            if (DataBase.FileESL != null && DataBase.FileESL.Lines != null)
            {
                ushort[] Order = DataBase.FileESL.Lines.Keys.ToArray();

                for (int i = 0; i < Order.Length; i++)
                {
                    byte[] b = DataBase.FileESL.Lines[Order[i]];
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
                DataBase.FileETS.StartFile[0] = lenght[0];
                DataBase.FileETS.StartFile[1] = lenght[1];

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
                DataBase.FileITA.StartFile[6] = lenght[0];
                DataBase.FileITA.StartFile[7] = lenght[1];

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
                DataBase.FileAEV.StartFile[6] = lenght[0];
                DataBase.FileAEV.StartFile[7] = lenght[1];

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

        #endregion

        #region saveConvert

        public static void SaveConvertFileETS(FileStream stream) 
        {
            if (DataBase.FileETS != null && DataBase.FileETS.Lines != null)
            {
                byte[] lenght = BitConverter.GetBytes(DataBase.FileETS.Lines.Count);
                DataBase.FileETS.StartFile[0] = lenght[0];
                DataBase.FileETS.StartFile[1] = lenght[1];

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
            if (from == Re4Version.Classic) // convert to UHD
            {
                byte[] res = new byte[40]; // UHD lenght;
                //ETCMODEL_ID
                res[0x00] = line[0x30];
                res[0x01] = line[0x31];
                //ETS_ID
                res[0x02] = line[0x32];
                res[0x03] = line[0x33];
                //U_TTS
                line.Take(12).ToArray().CopyTo(res, 0x4);
                //angle
                line.Skip(0X10).Take(12).ToArray().CopyTo(res, 0x10);
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
                line.Skip(0x4).Take(12).ToArray().CopyTo(res, 0);
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
                DataBase.FileITA.StartFile[6] = lenght[0];
                DataBase.FileITA.StartFile[7] = lenght[1];

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
                DataBase.FileAEV.StartFile[6] = lenght[0];
                DataBase.FileAEV.StartFile[7] = lenght[1];

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

            Re4Version to = Re4Version.Null;

            if (from == Re4Version.Classic) // to UHD
            {
                to = Re4Version.UHD;
                res = new byte[156];
              
            }
            else if (from == Re4Version.UHD) // to classic
            {
                to = Re4Version.Classic;
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
                    if (to == Re4Version.Classic)
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
                    if (to == Re4Version.Classic)
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
                    if (to == Re4Version.Classic)
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
                    if (to == Re4Version.Classic)
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
                    if (to == Re4Version.Classic)
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


        #endregion

    }
}
