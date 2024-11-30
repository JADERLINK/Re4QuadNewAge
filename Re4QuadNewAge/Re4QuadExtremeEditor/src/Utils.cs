using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Re4QuadExtremeEditor.src.JSON;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.Shaders;
using System.IO;
using OpenTK;

namespace Re4QuadExtremeEditor.src
{
    /// <summary>
    /// Metodos uteis para serem usados;
    /// </summary>
    public static class Utils
    {        

        /// <summary>
        /// carrega os modelos 3d dos objetos ao iniciar o programa
        /// </summary>
        public static void StartLoadObjsModels()
        {
            ViewerBase.TextureRef.LoadTextureLinear = NewAgeTheRender.ObjModel3D.LoadTextureLinear;

            DataBase.ItemsModels = new NewAgeTheRender.ModelGroupConteiner(Consts.ItemsModelGroupName);
            var ItemsObjModelJsonPaths = DataBase.ItemsIDs.List
                .Where(x => x.Value.ObjectModel != null && x.Value.ObjectModel.Length > 0)
                .Select(x => Path.Combine(AppContext.BaseDirectory, Consts.ItemsDirectory, DataBase.ItemsIDs.Folder, x.Value.ObjectModel))
                .ToHashSet().ToArray();
            DataBase.ItemsModels.AddModelObjects(ItemsObjModelJsonPaths);


            DataBase.EtcModels = new NewAgeTheRender.ModelGroupConteiner(Consts.EtcModelGroupName);
            var EtcModelsObjModelJsonPaths = DataBase.EtcModelIDs.List
               .Where(x => x.Value.ObjectModel != null && x.Value.ObjectModel.Length > 0)
               .Select(x => Path.Combine(AppContext.BaseDirectory, Consts.EtcModelsDirectory, DataBase.EtcModelIDs.Folder, x.Value.ObjectModel))
               .ToHashSet().ToArray();
            DataBase.EtcModels.AddModelObjects(EtcModelsObjModelJsonPaths);


            DataBase.EnemiesModels = new NewAgeTheRender.ModelGroupConteiner(Consts.EnemiesModelGroupName);
            var EnemiesObjModelJsonPaths = DataBase.EnemiesIDs.List
              .Where(x => x.Value.ObjectModel != null && x.Value.ObjectModel.Length > 0)
              .Select(x => Path.Combine(AppContext.BaseDirectory, Consts.EnemiesDirectory, DataBase.EnemiesIDs.Folder, x.Value.ObjectModel))
              .ToHashSet().ToArray();
            DataBase.EnemiesModels.AddModelObjects(EnemiesObjModelJsonPaths);


            DataBase.QuadCustomModels = new NewAgeTheRender.ModelGroupConteiner(Consts.QuadCustomModelGroupName);
            var QuadCustomObjModelJsonPaths = DataBase.QuadCustomIDs.List
                .Where(x => x.Value.ObjectModel != null && x.Value.ObjectModel.Length > 0)
                .Select(x => Path.Combine(AppContext.BaseDirectory, Consts.QuadCustomDirectory, DataBase.QuadCustomIDs.Folder, x.Value.ObjectModel))
                .ToHashSet().ToArray();
            DataBase.QuadCustomModels.AddModelObjects(QuadCustomObjModelJsonPaths);



            DataBase.InternalModels = new NewAgeTheRender.ModelGroupConteiner(Consts.InternalModelGroupName);
            var InternalObjModelJsonPaths = (new List<string> {
            Consts.ModelKeyAshleyPoint,
            Consts.ModelKeyGrappleGunPoint,
            Consts.ModelKeyLadderError,
            Consts.ModelKeyLadderObj,
            Consts.ModelKeyLadderPoint,
            Consts.ModelKeyLocalTeleportationPoint,
            Consts.ModelKeyWarpPoint,
            Consts.ModelKey_ESE_Point,
            Consts.ModelKey_EMI_Point,
            Consts.ModelKey_LIT_Point,
            Consts.ModelKey_EFF_EntryPoint,
            Consts.ModelKey_EFF_GroupPoint,
            Consts.ModelKey_EFF_Table9,
            Consts.ModelKeyQuadCustomPoint
            })
             .Select(x => Path.Combine(AppContext.BaseDirectory, Consts.InternalModelsDirectory, x)).ToHashSet().ToArray();
            DataBase.InternalModels.AddModelObjects(InternalObjModelJsonPaths);

            //int finish = 0;
        }


        /// <summary>
        /// carrega as lista de ObjInfo ao iniciar o programa
        /// </summary>
        public static void StartLoadObjsInfoLists() 
        {
            DataBase.ItemsIDs = new ObjectInfoList(Consts.NameNull, Consts.NameNull, Consts.NameNull, new Dictionary<ushort, ObjectInfo>());
            DataBase.EtcModelIDs = new ObjectInfoList(Consts.NameNull, Consts.NameNull, Consts.NameNull, new Dictionary<ushort, ObjectInfo>());
            DataBase.EnemiesIDs = new ObjectInfoList(Consts.NameNull, Consts.NameNull, Consts.NameNull, new Dictionary<ushort, ObjectInfo>());
            DataBase.QuadCustomIDs = new QuadCustomInfoList(Consts.NameNull, Consts.NameNull, Consts.NameNull, new Dictionary<uint, QuadCustomInfo>());
            try { DataBase.ItemsIDs = ObjectInfoListFile.ParseFromFile(Path.Combine(AppContext.BaseDirectory, Consts.ItemsDirectory, Globals.FileDiretoryItemsList), Consts.NameItemsList); } catch (Exception){}
            try { DataBase.EtcModelIDs = ObjectInfoListFile.ParseFromFile(Path.Combine(AppContext.BaseDirectory, Consts.EtcModelsDirectory, Globals.FileDiretoryEtcModelsList), Consts.NameEtcModelsList); } catch (Exception){}
            try { DataBase.EnemiesIDs = ObjectInfoListFile.ParseFromFile(Path.Combine(AppContext.BaseDirectory, Consts.EnemiesDirectory, Globals.FileDiretoryEnemiesList), Consts.NameEnemiesList); } catch (Exception){}
            try { DataBase.QuadCustomIDs = QuadCustomInfoListFile.ParseFromFile(Path.Combine(AppContext.BaseDirectory, Consts.QuadCustomDirectory, Globals.FileDiretoryQuadCustomList)); } catch (Exception){}
        }

        /// <summary>
        /// na lista de enimigos os ids vão de 0x00 a 0x4F, depois disso se repete a ordem dos inimigos, porem eles não emitem som
        /// </summary>
        public static void StartEnemyExtraSegmentList() 
        {
            if (Globals.CreateEnemyExtraSegmentList)
            {
                // segund ExtraSegment
                for (ushort i = 0; i < 0x50; i++)
                {
                    ushort originalId = (ushort)(i * 0x100);

                    var list = (from obj in DataBase.EnemiesIDs.List
                                where obj.Key >= originalId && obj.Key <= (originalId + 0xFF)
                                select obj.Key).ToArray();

                    if (list.Length != 0)
                    {
                        foreach (var Key in list)
                        {
                            ushort newId = (ushort)(Key + 0x5000);
                            ObjectInfo obj;
                            if (!DataBase.EnemiesIDs.List.ContainsKey(newId) && DataBase.EnemiesIDs.List.TryGetValue(Key, out obj))
                            {
                                ObjectInfo newObj = new ObjectInfo(newId,
                                    obj.ObjectModel,
                                    obj.Name + " " + Lang.GetText(eLang.EnemyExtraSegmentSegund),
                                    obj.Description + " " + Lang.GetText(eLang.EnemyExtraSegmentSegund) + " " + Lang.GetText(eLang.EnemyExtraSegmentNoSound));
                                DataBase.EnemiesIDs.List.Add(newId, newObj);
                            }

                        }
                    }
                }

                //third ExtraSegment
                for (ushort i = 0; i < 0x50; i++)
                {
                    ushort originalId = (ushort)(i * 0x100);

                    var list = (from obj in DataBase.EnemiesIDs.List
                                where obj.Key >= originalId && obj.Key <= (originalId + 0xFF)
                                select obj.Key).ToArray();

                    if (list.Length != 0)
                    {
                        foreach (var Key in list)
                        {
                            ushort newId = (ushort)(Key + 0xA000);
                            ObjectInfo obj;
                            if (!DataBase.EnemiesIDs.List.ContainsKey(newId) && DataBase.EnemiesIDs.List.TryGetValue(Key, out obj))
                            {
                                ObjectInfo newObj = new ObjectInfo(newId,
                                    obj.ObjectModel,
                                    obj.Name + " " + Lang.GetText(eLang.EnemyExtraSegmentThird),
                                    obj.Description + " " + Lang.GetText(eLang.EnemyExtraSegmentThird) + " " + Lang.GetText(eLang.EnemyExtraSegmentNoSound));
                                DataBase.EnemiesIDs.List.Add(newId, newObj);
                            }

                        }
                    }
                }

            }
        
        }


        /// <summary>
        /// carrega a lista de PromptMessage, em  ListBoxProperty.PromptMessageList
        /// </summary>
        public static void StartLoadPromptMessageList() 
        {
            try
            {
                ListBoxProperty.PromptMessageList = PromptMessageListFile.parsePromptMessageList(Path.Combine(AppContext.BaseDirectory, Consts.PromptMessageListFileDirectory));
            }
            catch (Exception)
            {
                ListBoxProperty.PromptMessageList = new Dictionary<byte, ByteObjForListBox>();
            }
           
        }

        /// <summary>
        /// prenche ListBoxProperty
        /// </summary>
        public static void StartSetListBoxsProperty()
        {
            Dictionary<bool, BoolObjForListBox> FloatType = new Dictionary<bool, BoolObjForListBox>();
            FloatType.Add(false, new BoolObjForListBox(false, Lang.GetAttributeText(aLang.ListBoxFloatTypeDisable)));
            FloatType.Add(true, new BoolObjForListBox(true, Lang.GetAttributeText(aLang.ListBoxFloatTypeEnable)));
            ListBoxProperty.FloatTypeList = FloatType;

            Dictionary<byte, ByteObjForListBox> Enable = new Dictionary<byte, ByteObjForListBox>();
            Enable.Add(0x00, new ByteObjForListBox(0x00, "00: " + Lang.GetAttributeText(aLang.ListBoxDisable)));
            Enable.Add(0x01, new ByteObjForListBox(0x01, "01: " + Lang.GetAttributeText(aLang.ListBoxEnable)));
            ListBoxProperty.EnemyEnableList = Enable;

            Dictionary<byte, ByteObjForListBox> SpecialZoneCategory = new Dictionary<byte, ByteObjForListBox>();
            SpecialZoneCategory.Add(0x00, new ByteObjForListBox(0x00, "00: " + Lang.GetAttributeText(aLang.ListBoxSpecialZoneCategory00)));
            SpecialZoneCategory.Add(0x01, new ByteObjForListBox(0x01, "01: " + Lang.GetAttributeText(aLang.ListBoxSpecialZoneCategory01)));
            SpecialZoneCategory.Add(0x02, new ByteObjForListBox(0x02, "02: " + Lang.GetAttributeText(aLang.ListBoxSpecialZoneCategory02)));
            ListBoxProperty.SpecialZoneCategoryList = SpecialZoneCategory;

            Dictionary<byte, ByteObjForListBox> QuadCustomPointStatusList = new Dictionary<byte, ByteObjForListBox>();
            QuadCustomPointStatusList.Add(0x00, new ByteObjForListBox(0x00, "00: " + Lang.GetAttributeText(aLang.ListBoxQuadCustomPointStatus00)));
            QuadCustomPointStatusList.Add(0x01, new ByteObjForListBox(0x01, "01: " + Lang.GetAttributeText(aLang.ListBoxQuadCustomPointStatus01)));
            QuadCustomPointStatusList.Add(0x02, new ByteObjForListBox(0x02, "02: " + Lang.GetAttributeText(aLang.ListBoxQuadCustomPointStatus02)));
            ListBoxProperty.QuadCustomPointStatusList = QuadCustomPointStatusList;

            Dictionary<byte, ByteObjForListBox> RefInteractionTypeList = new Dictionary<byte, ByteObjForListBox>();
            RefInteractionTypeList.Add(0x00, new ByteObjForListBox(0x00, "00: " + Lang.GetAttributeText(aLang.ListBoxRefInteractionType00)));
            RefInteractionTypeList.Add(0x01, new ByteObjForListBox(0x01, "01: " + Lang.GetAttributeText(aLang.ListBoxRefInteractionType01Enemy)));
            RefInteractionTypeList.Add(0x02, new ByteObjForListBox(0x02, "02: " + Lang.GetAttributeText(aLang.ListBoxRefInteractionType02EtcModel)));
            ListBoxProperty.RefInteractionTypeList = RefInteractionTypeList;

            Dictionary<ushort, UshortObjForListBox> ItemAuraType = new Dictionary<ushort, UshortObjForListBox>();
            ItemAuraType.Add(0x00, new UshortObjForListBox(0x00, "00: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType00)));
            ItemAuraType.Add(0x01, new UshortObjForListBox(0x01, "01: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType01)));
            ItemAuraType.Add(0x02, new UshortObjForListBox(0x02, "02: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType02)));
            ItemAuraType.Add(0x03, new UshortObjForListBox(0x03, "03: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType03)));
            ItemAuraType.Add(0x04, new UshortObjForListBox(0x04, "04: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType04)));
            ItemAuraType.Add(0x05, new UshortObjForListBox(0x05, "05: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType05)));
            ItemAuraType.Add(0x06, new UshortObjForListBox(0x06, "06: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType06)));
            ItemAuraType.Add(0x07, new UshortObjForListBox(0x07, "07: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType07)));
            ItemAuraType.Add(0x08, new UshortObjForListBox(0x08, "08: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType08)));
            ItemAuraType.Add(0x09, new UshortObjForListBox(0x09, "09: " + Lang.GetAttributeText(aLang.ListBoxItemAuraType09)));
            ListBoxProperty.ItemAuraTypeList = ItemAuraType;


            //ListBoxProperty.SpecialTypeList
            Dictionary<SpecialType, ByteObjForListBox> SpecialTypeList = new Dictionary<SpecialType, ByteObjForListBox>();
            SpecialTypeList.Add(SpecialType.T00_GeneralPurpose, new ByteObjForListBox(0x00, Lang.GetAttributeText(aLang.SpecialType00_GeneralPurpose)));
            SpecialTypeList.Add(SpecialType.T01_WarpDoor, new ByteObjForListBox(0x01, Lang.GetAttributeText(aLang.SpecialType01_WarpDoor)));
            SpecialTypeList.Add(SpecialType.T02_CutSceneEvents, new ByteObjForListBox(0x02, Lang.GetAttributeText(aLang.SpecialType02_CutSceneEvents)));
            SpecialTypeList.Add(SpecialType.T03_Items, new ByteObjForListBox(0x03, Lang.GetAttributeText(aLang.SpecialType03_Items)));
            SpecialTypeList.Add(SpecialType.T04_GroupedEnemyTrigger, new ByteObjForListBox(0x04, Lang.GetAttributeText(aLang.SpecialType04_GroupedEnemyTrigger)));
            SpecialTypeList.Add(SpecialType.T05_Message, new ByteObjForListBox(0x05, Lang.GetAttributeText(aLang.SpecialType05_Message)));
            //SpecialTypeList.Add(SpecialType.T06_Unused, new ByteObjForListBox(0x06, Lang.GetAttributeText(aLang.SpecialType06_Unused)));
            //SpecialTypeList.Add(SpecialType.T07_Unused, new ByteObjForListBox(0x07, Lang.GetAttributeText(aLang.SpecialType07_Unused)));
            SpecialTypeList.Add(SpecialType.T08_TypeWriter, new ByteObjForListBox(0x08, Lang.GetAttributeText(aLang.SpecialType08_TypeWriter)));
            //SpecialTypeList.Add(SpecialType.T09_Unused, new ByteObjForListBox(0x09, Lang.GetAttributeText(aLang.SpecialType09_Unused)));
            SpecialTypeList.Add(SpecialType.T0A_DamagesThePlayer, new ByteObjForListBox(0x0A, Lang.GetAttributeText(aLang.SpecialType0A_DamagesThePlayer)));
            SpecialTypeList.Add(SpecialType.T0B_FalseCollision, new ByteObjForListBox(0x0B, Lang.GetAttributeText(aLang.SpecialType0B_FalseCollision)));
            //SpecialTypeList.Add(SpecialType.T0C_Unused, new ByteObjForListBox(0x0C, Lang.GetAttributeText(aLang.SpecialType0C_Unused)));
            SpecialTypeList.Add(SpecialType.T0D_FieldInfo, new ByteObjForListBox(0x0D, Lang.GetAttributeText(aLang.SpecialType0D_FieldInfo)));
            SpecialTypeList.Add(SpecialType.T0E_Crouch, new ByteObjForListBox(0x0E, Lang.GetAttributeText(aLang.SpecialType0E_Crouch)));
            //SpecialTypeList.Add(SpecialType.T0F_Unused, new ByteObjForListBox(0x0F, Lang.GetAttributeText(aLang.SpecialType0F_Unused)));
            SpecialTypeList.Add(SpecialType.T10_FixedLadderClimbUp, new ByteObjForListBox(0x10, Lang.GetAttributeText(aLang.SpecialType10_FixedLadderClimbUp)));
            SpecialTypeList.Add(SpecialType.T11_ItemDependentEvents, new ByteObjForListBox(0x11, Lang.GetAttributeText(aLang.SpecialType11_ItemDependentEvents)));
            SpecialTypeList.Add(SpecialType.T12_AshleyHideCommand, new ByteObjForListBox(0x12, Lang.GetAttributeText(aLang.SpecialType12_AshleyHideCommand)));
            SpecialTypeList.Add(SpecialType.T13_LocalTeleportation, new ByteObjForListBox(0x13, Lang.GetAttributeText(aLang.SpecialType13_LocalTeleportation)));
            SpecialTypeList.Add(SpecialType.T14_UsedForElevators, new ByteObjForListBox(0x14, Lang.GetAttributeText(aLang.SpecialType14_UsedForElevators)));
            SpecialTypeList.Add(SpecialType.T15_AdaGrappleGun, new ByteObjForListBox(0x15, Lang.GetAttributeText(aLang.SpecialType15_AdaGrappleGun)));
            ListBoxProperty.SpecialTypeList = SpecialTypeList;

        }

        /// <summary>
        /// prenche EnemiesList, EtcmodelsList, ItemsList em ListBoxProperty class
        /// </summary>
        public static void StartSetListBoxsPropertybjsInfoLists()
        {

            //ListBoxProperty.EnemiesList
            Dictionary<ushort, UshortObjForListBox> Enemies = new Dictionary<ushort, UshortObjForListBox>();
            foreach (var item in DataBase.EnemiesIDs.List)
            {
                string ID = item.Value.HexID.ToString("X4");
                if (ID[2] == 'F' && ID[3] == 'F'){ continue; }
                if (ID == "FFFF") { continue; }
                Enemies.Add(item.Value.HexID, new UshortObjForListBox(item.Value.HexID, item.Value.HexID.ToString("X4") + ": " + item.Value.Description));
            }
            Enemies = Enemies.OrderBy(o => o.Key).ToDictionary(p => p.Key, p => p.Value);
            ListBoxProperty.EnemiesList = Enemies;



            //ListBoxProperty.EtcmodelsList
            Dictionary<ushort, UshortObjForListBox> EtcModels = new Dictionary<ushort, UshortObjForListBox>();
            foreach (var item in DataBase.EtcModelIDs.List)
            {
                if (item.Value.HexID == ushort.MaxValue) { continue; }
                EtcModels.Add(item.Value.HexID, new UshortObjForListBox(item.Value.HexID, item.Value.HexID.ToString("X4") + ": " + item.Value.Description));
            }
            EtcModels = EtcModels.OrderBy(o => o.Key).ToDictionary(p => p.Key, p => p.Value);
            ListBoxProperty.EtcmodelsList = EtcModels;



            //ListBoxProperty.ItemsList
            Dictionary<ushort, UshortObjForListBox> Itens = new Dictionary<ushort, UshortObjForListBox>();
            foreach (var item in DataBase.ItemsIDs.List)
            {
                if (item.Value.HexID == ushort.MaxValue) { continue; }
                Itens.Add(item.Value.HexID, new UshortObjForListBox(item.Value.HexID, item.Value.HexID.ToString("X4") + ": " + item.Value.Description));
            }
            Itens = Itens.OrderBy(o => o.Key).ToDictionary(p => p.Key, p => p.Value);
            ListBoxProperty.ItemsList = Itens;


            //ListBoxProperty.QuadCustomModelIDList
            Dictionary<uint, UintObjForListBox> QuadCustom = new Dictionary<uint, UintObjForListBox>();
            foreach (var item in DataBase.QuadCustomIDs.List)
            {
                if (item.Value.ID == uint.MaxValue) { continue; }
                QuadCustom.Add(item.Value.ID, new UintObjForListBox(item.Value.ID, item.Value.ID.ToString() + ": " + item.Value.Name));
            }
            QuadCustom = QuadCustom.OrderBy(o => o.Key).ToDictionary(p => p.Key, p => p.Value);
            ListBoxProperty.QuadCustomModelIDList = QuadCustom;
        }

        /// <summary>
        /// metodo destinado a instanciar os Nodes de Grupos;
        /// </summary>
        public static void StartCreateNodes() 
        {
            DataBase.NodeESL = new EnemyNodeGroup
            {
                Group = GroupType.ESL,
                Text = Lang.GetText(eLang.NodeESL),
                Name = Consts.NodeESL,
                ForeColor = Globals.NodeColorESL,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeETS = new EtcModelNodeGroup
            {
                Group = GroupType.ETS,
                Text = Lang.GetText(eLang.NodeETS),
                Name = Consts.NodeETS,
                ForeColor = Globals.NodeColorETS,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeITA = new SpecialNodeGroup
            {
                Group = GroupType.ITA,
                Text = Lang.GetText(eLang.NodeITA),
                Name = Consts.NodeITA,
                ForeColor = Globals.NodeColorITA,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeAEV = new SpecialNodeGroup
            {
                Group = GroupType.AEV,
                Text = Lang.GetText(eLang.NodeAEV),
                Name = Consts.NodeAEV,
                ForeColor = Globals.NodeColorAEV,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEXTRAS = new ExtraNodeGroup
            {
                Group = GroupType.EXTRAS,
                Text = Lang.GetText(eLang.NodeEXTRAS),
                Name = Consts.NodeEXTRAS,
                ForeColor = Globals.NodeColorEXTRAS,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeDSE = new NewAge_DSE_NodeGroup
            {
                Group = GroupType.DSE,
                Text = Lang.GetText(eLang.NodeDSE),
                Name = Consts.NodeDSE,
                ForeColor = Globals.NodeColorDSE,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEMI = new NewAge_EMI_NodeGroup
            {
                Group = GroupType.EMI,
                Text = Lang.GetText(eLang.NodeEMI),
                Name = Consts.NodeEMI,
                ForeColor = Globals.NodeColorEMI,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeSAR = new NewAge_ESAR_NodeGroup
            {
                Group = GroupType.SAR,
                Text = Lang.GetText(eLang.NodeSAR),
                Name = Consts.NodeSAR,
                ForeColor = Globals.NodeColorSAR,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEAR = new NewAge_ESAR_NodeGroup
            {
                Group = GroupType.EAR,
                Text = Lang.GetText(eLang.NodeEAR),
                Name = Consts.NodeEAR,
                ForeColor = Globals.NodeColorEAR,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeESE = new NewAge_ESE_NodeGroup
            {
                Group = GroupType.ESE,
                Text = Lang.GetText(eLang.NodeESE),
                Name = Consts.NodeESE,
                ForeColor = Globals.NodeColorESE,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeFSE = new NewAge_FSE_NodeGroup
            {
                Group = GroupType.FSE,
                Text = Lang.GetText(eLang.NodeFSE),
                Name = Consts.NodeFSE,
                ForeColor = Globals.NodeColorFSE,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeQuadCustom = new QuadCustomNodeGroup
            {
                Group = GroupType.QUAD_CUSTOM,
                Text = Lang.GetText(eLang.NodeQuadCustom),
                Name = Consts.NodeQuadCustom,
                ForeColor = Globals.NodeColorQuadCustom,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeLIT_Groups = new NewAge_LIT_Groups_NodeGroup
            {
                Group = GroupType.LIT_GROUPS,
                Text = Lang.GetText(eLang.NodeLIT_GROUPS),
                Name = Consts.NodeLIT_GROUPS,
                ForeColor = Globals.NodeColorLIT_GROUPS,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeLIT_Entrys = new NewAge_LIT_Entrys_NodeGroup
            {
                Group = GroupType.LIT_ENTRYS,
                Text = Lang.GetText(eLang.NodeLIT_ENTRYS),
                Name = Consts.NodeLIT_ENTRYS,
                ForeColor = Globals.NodeColorLIT_ENTRYS,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table0 = new NewAge_EFF_NodeGroup
            {
                Group = GroupType.EFF_Table0,
                Text = Lang.GetText(eLang.NodeEFF_Table0),
                Name = Consts.NodeEFF_Table0,
                ForeColor = Globals.NodeColorEFF_Table0,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table1 = new NewAge_EFF_NodeGroup
            {
                Group = GroupType.EFF_Table1,
                Text = Lang.GetText(eLang.NodeEFF_Table1),
                Name = Consts.NodeEFF_Table1,
                ForeColor = Globals.NodeColorEFF_Table1,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table2 = new NewAge_EFF_NodeGroup
            {
                Group = GroupType.EFF_Table2,
                Text = Lang.GetText(eLang.NodeEFF_Table2),
                Name = Consts.NodeEFF_Table2,
                ForeColor = Globals.NodeColorEFF_Table2,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table3 = new NewAge_EFF_NodeGroup
            {
                Group = GroupType.EFF_Table3,
                Text = Lang.GetText(eLang.NodeEFF_Table3),
                Name = Consts.NodeEFF_Table3,
                ForeColor = Globals.NodeColorEFF_Table3,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table4 = new NewAge_EFF_NodeGroup
            {
                Group = GroupType.EFF_Table4,
                Text = Lang.GetText(eLang.NodeEFF_Table4),
                Name = Consts.NodeEFF_Table4,
                ForeColor = Globals.NodeColorEFF_Table4,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table6 = new NewAge_EFF_NodeGroup
            {
                Group = GroupType.EFF_Table6,
                Text = Lang.GetText(eLang.NodeEFF_Table6),
                Name = Consts.NodeEFF_Table6,
                ForeColor = Globals.NodeColorEFF_Table6,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table7_Effect_0 =  new NewAge_EFF_EffectGroup_NodeGroup
            {
                Group = GroupType.EFF_Table7_Effect_0,
                Text = Lang.GetText(eLang.NodeEFF_Table7_Effect_0),
                Name = Consts.NodeEFF_Table7_Effect_0,
                ForeColor = Globals.NodeColorEFF_Table7_Effect_0,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table8_Effect_1 = new NewAge_EFF_EffectGroup_NodeGroup
            {
                Group = GroupType.EFF_Table8_Effect_1,
                Text = Lang.GetText(eLang.NodeEFF_Table8_Effect_1),
                Name = Consts.NodeEFF_Table8_Effect_1,
                ForeColor = Globals.NodeColorEFF_Table8_Effect_1,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_EffectEntry = new NewAge_EFF_EffectEntry_NodeGroup
            {
                Group = GroupType.EFF_EffectEntry,
                Text = Lang.GetText(eLang.NodeEFF_EffectEntry),
                Name = Consts.NodeEFF_EffectEntry,
                ForeColor = Globals.NodeColorEFF_EffectEntry,
                NodeFont = Globals.TreeNodeFontText
            };

            DataBase.NodeEFF_Table9 = new NewAge_EFF_Table9Entry_NodeGroup
            {
                Group = GroupType.EFF_Table9,
                Text = Lang.GetText(eLang.NodeEFF_Table9),
                Name = Consts.NodeEFF_Table9,
                ForeColor = Globals.NodeColorEFF_Table9,
                NodeFont = Globals.TreeNodeFontText
            };

        }

        /// <summary>
        /// metodo destinado a instanciar o node do ExtraGroup;
        /// </summary>
        public static void StartExtraGroup() 
        {
            DataBase.Extras = new Class.Files.ExtraGroup();
            DataBase.NodeEXTRAS.DisplayMethods = DataBase.Extras.DisplayMethods;
            DataBase.NodeEXTRAS.MoveMethods = DataBase.Extras.MoveMethods;
        }

        public static List<ushort> AllUshots() 
        {
            return Enumerable.Range(0, ushort.MaxValue).Cast<ushort>().ToList();
        }

        public static OpenTK.Vector4 ColorToVector4(Color color) 
        {
            return new OpenTK.Vector4((float)color.R / byte.MaxValue, (float)color.G / byte.MaxValue, (float)color.B / byte.MaxValue, (float)color.A / byte.MaxValue);
        }


        public static Color Vector4ToColor(OpenTK.Vector4 color)
        {
            return Color.FromArgb((int)Math.Round(color.W * byte.MaxValue), (int)Math.Round(color.X * byte.MaxValue), (int)Math.Round(color.Y * byte.MaxValue), (int)Math.Round(color.Z * byte.MaxValue));
        }

        public static float EnemyAngleToRad(short EnemyAngle)
        {
            //return ((2 * (float)Math.PI) * EnemyAngle) / 32768;
            return (MathHelper.TwoPi * EnemyAngle) / 32768;
        }

        public static short RadToEnemyAngle(float RadAngle)
        {
            //float temp = (32768 * RadAngle) / (2 * (float)Math.PI);
            float temp = (32768 * RadAngle) / MathHelper.TwoPi;
            if (temp > short.MaxValue) { temp = short.MaxValue; }
            if (temp < short.MinValue) { temp = short.MinValue; }
            return (short)temp;
        }

        public static float RadAngle1Scale(float RadAngle) 
        {
            float rad = RadAngle;
            while (rad > MathHelper.TwoPi)
            {
                rad -= MathHelper.TwoPi;
            }
            while (rad < -MathHelper.TwoPi)
            {
                rad += MathHelper.TwoPi;
            }
            return rad;
        }

        public static SpecialFileFormat GroupTypeToSpecialFileFormat(GroupType group) 
        {
            switch (group)
            {
                case GroupType.ITA:
                    return SpecialFileFormat.ITA;
                case GroupType.AEV:
                    return SpecialFileFormat.AEV;
            }
            return SpecialFileFormat.NULL;
        }

        public static SpecialType ToSpecialType(byte specialType)
        {
            if (specialType < 0x16)
            {
                return (SpecialType)specialType;
            }
            return SpecialType.UnspecifiedType;
        }

        public static void ToMoveCheckLimits(ref Vector3[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Vector3 vec = value[i];
                if (float.IsNaN(vec.X) || float.IsInfinity(vec.X)) { vec.X = 0; }
                if (float.IsNaN(vec.Y) || float.IsInfinity(vec.Y)) { vec.Y = 0; }
                if (float.IsNaN(vec.Z) || float.IsInfinity(vec.Z)) { vec.Z = 0; }
                if (vec.X > Consts.MyFloatMax) { vec.X = Consts.MyFloatMax; }
                if (vec.X < -Consts.MyFloatMax) { vec.X = -Consts.MyFloatMax; }
                if (vec.Y > Consts.MyFloatMax) { vec.Y = Consts.MyFloatMax; }
                if (vec.Y < -Consts.MyFloatMax) { vec.Y = -Consts.MyFloatMax; }
                if (vec.Z > Consts.MyFloatMax) { vec.Z = Consts.MyFloatMax; }
                if (vec.Z < -Consts.MyFloatMax) { vec.Z = -Consts.MyFloatMax; }
                value[i] = vec;
            }
        }

        public static void ToCameraCheckValue(ref Vector3 position) 
        {
            if (float.IsNaN(position.X) || float.IsInfinity(position.X)) { position.X = 0; }
            if (float.IsNaN(position.Y) || float.IsInfinity(position.Y)) { position.Y = 0; }
            if (float.IsNaN(position.Z) || float.IsInfinity(position.Z)) { position.Z = 0; }
        }


        /// <summary>
        /// Recarrega os arquivos Json e seus textos;
        /// </summary>
        public static void ReloadJsonFiles() 
        {
            StartLoadObjsInfoLists();
            StartEnemyExtraSegmentList();
            StartSetListBoxsPropertybjsInfoLists();
            StartLoadPromptMessageList();
        }

        public static void StartReloadDirectoryDic()
        {
            DataBase.DirectoryDic = new Dictionary<string, string>();
            DataBase.DirectoryDic.Add("", "");
            DataBase.DirectoryDic.Add("app", AppContext.BaseDirectory + "\\");
            DataBase.DirectoryDic.Add("xfile", Globals.DirectoryXFILE);
            DataBase.DirectoryDic.Add("2007re4", Globals.Directory2007RE4);
            DataBase.DirectoryDic.Add("ps2re4", Globals.DirectoryPS2RE4);
            DataBase.DirectoryDic.Add("uhdre4", Globals.DirectoryUHDRE4);
            DataBase.DirectoryDic.Add("ps4nsre4", Globals.DirectoryPS4NSRE4);
            DataBase.DirectoryDic.Add("custom1", Globals.DirectoryCustom1);
            DataBase.DirectoryDic.Add("custom2", Globals.DirectoryCustom2);
            DataBase.DirectoryDic.Add("custom3", Globals.DirectoryCustom3);
        }


        /// <summary>
        /// Recarrega os modelos 3d;
        /// </summary>
        public static void ReloadModels()
        {
            DataBase.InternalModels?.ClearGL();
            DataBase.ItemsModels?.ClearGL();
            DataBase.EtcModels?.ClearGL();
            DataBase.EnemiesModels?.ClearGL();
            DataBase.QuadCustomModels?.ClearGL();
            StartLoadObjsModels();
            GC.Collect();
        }


        public static void ChangeTextureTypeFromModels() 
        {
            DataBase.InternalModels?.ChangeTextureType();
            DataBase.ItemsModels?.ChangeTextureType();
            DataBase.EtcModels?.ChangeTextureType();
            DataBase.EnemiesModels?.ChangeTextureType();
            DataBase.QuadCustomModels?.ChangeTextureType();
        }

        public static UshortObjForListBox[] ItemRotationOrderForListBox() 
        {
            UshortObjForListBox[] list = new UshortObjForListBox[15];
            list[0] = new UshortObjForListBox(0, Lang.GetText(eLang.RotationXYZ));
            list[1] = new UshortObjForListBox(1, Lang.GetText(eLang.RotationXZY));
            list[2] = new UshortObjForListBox(2, Lang.GetText(eLang.RotationYXZ));
            list[3] = new UshortObjForListBox(3, Lang.GetText(eLang.RotationYZX));
            list[4] = new UshortObjForListBox(4, Lang.GetText(eLang.RotationZYX));
            list[5] = new UshortObjForListBox(5, Lang.GetText(eLang.RotationZXY));
            list[6] = new UshortObjForListBox(6, Lang.GetText(eLang.RotationXY));
            list[7] = new UshortObjForListBox(7, Lang.GetText(eLang.RotationXZ));
            list[8] = new UshortObjForListBox(8, Lang.GetText(eLang.RotationYX));
            list[9] = new UshortObjForListBox(9, Lang.GetText(eLang.RotationYZ));
            list[10] = new UshortObjForListBox(10, Lang.GetText(eLang.RotationZX));
            list[11] = new UshortObjForListBox(11, Lang.GetText(eLang.RotationZY));
            list[12] = new UshortObjForListBox(12, Lang.GetText(eLang.RotationX));
            list[13] = new UshortObjForListBox(13, Lang.GetText(eLang.RotationY));
            list[14] = new UshortObjForListBox(14, Lang.GetText(eLang.RotationZ));

            return list;
        }

        /// <summary>
        /// carrega o conteudo da tradução selecionada.
        /// </summary>
        public static void StartLoadLangFile() 
        {
            if (Globals.BackupConfigs.LoadLangTranslation)
            {
                string path = Path.Combine(AppContext.BaseDirectory, Consts.LangDirectory, Globals.BackupConfigs?.LangJsonFile ?? "");
                if (File.Exists(path))
                {
                    Lang.LoadedTranslation = true;
                    Lang.StartOthersTexts();
                    LangFile.ParseFromFileLang(path);
                }
            }
       
        }

   
        public static Vector3 GetObjPostion_ToCamera_Null(ushort ID)
        {
            return Vector3.Zero;
        }
        public static float GetObjAngleY_ToCamera_Null(ushort ID)
        {
            return 0;
        }

        public static Vector3[] GetObjPostion_ToMove_General_Null(ushort ID) { return null; }
        public static void SetObjPostion_ToMove_General_Null(ushort ID, Vector3[] value) { }
        public static Vector3[] GetObjRotationAngles_ToMove_Null(ushort ID) { return null; }
        public static void SetObjRotationAngles_ToMove_Null(ushort ID, Vector3[] value) { }
        public static Vector3[] GetObjScale_ToMove_Null(ushort ID) { return null; }
        public static void SetObjScale_ToMove_Null(ushort ID, Vector3[] value) { }
        public static TriggerZoneCategory GetTriggerZoneCategory_Null(ushort ID) { return TriggerZoneCategory.Disable; }

        public static Class.ObjMethods.NodeMoveMethods GetMoveMethodNull() 
        {
            return new Class.ObjMethods.NodeMoveMethods
            {
                GetObjPostion_ToCamera = GetObjPostion_ToCamera_Null,
                GetObjAngleY_ToCamera = GetObjAngleY_ToCamera_Null,
                GetObjPostion_ToMove_General = GetObjPostion_ToMove_General_Null,
                SetObjPostion_ToMove_General = SetObjPostion_ToMove_General_Null,
                GetObjRotationAngles_ToMove = GetObjRotationAngles_ToMove_Null,
                SetObjRotationAngles_ToMove = SetObjRotationAngles_ToMove_Null,
                GetObjScale_ToMove = GetObjScale_ToMove_Null,
                SetObjScale_ToMove = SetObjScale_ToMove_Null,
                GetTriggerZoneCategory = GetTriggerZoneCategory_Null
            };
        }

    }


}
