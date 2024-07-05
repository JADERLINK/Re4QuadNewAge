using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Re4QuadExtremeEditor.src.Class.Enums;
using System.IO;

namespace Re4QuadExtremeEditor.src.JSON
{
    /// <summary>
    /// Representa o arquivo de configurações json, nas quais são replicadas na classe Globals;
    /// </summary>
    public class Configs
    {
        public string DirectoryXFILE { get; set; }
        public string Directory2007RE4 { get; set; }
        public string DirectoryPS2RE4 { get; set; }
        public string DirectoryUHDRE4 { get; set; }
        public string DirectoryCustom1 { get; set; }
        public string DirectoryCustom2 { get; set; }
        public string DirectoryCustom3 { get; set; }

        //listagens json
        public string FileDiretoryItemsList { get; set; }
        public string FileDiretoryEtcModelsList { get; set; }
        public string FileDiretoryEnemiesList { get; set; }

        public Color SkyColor { get; set; }

        // floats
        public ConfigFrationalSymbol FrationalSymbol { get; set; }
        public int FrationalAmount { get; set; }

        //items rotations
        public bool ItemDisableRotationAll { get; set; }
        public bool ItemDisableRotationIfXorYorZequalZero { get; set; }
        public bool ItemDisableRotationIfZisNotGreaterThanZero { get; set; }
        public ObjRotationOrder ItemRotationOrder { get; set; }
        public float ItemRotationCalculationMultiplier { get; set; }
        public float ItemRotationCalculationDivider { get; set; }

        //theme
        public bool UseDarkerGrayTheme { get; set; }

        // lang
        public bool LoadLangTranslation { get; set; }
        public string LangJsonFile { get; set; }


        /// <summary>
        /// define as configs padrões 
        /// </summary>
        /// <returns></returns>
        public static Configs GetDefaultConfigs()
        {
            Configs configs = new Configs();
            configs.DirectoryXFILE = @"";
            configs.Directory2007RE4 = @"";
            configs.DirectoryPS2RE4 = @"";
            configs.DirectoryUHDRE4 = @"";
            configs.DirectoryCustom1 = @"";
            configs.DirectoryCustom2 = @"";
            configs.DirectoryCustom3 = @"";

            configs.FileDiretoryEnemiesList = Consts.DefaultEnemiesListFileDirectory;
            configs.FileDiretoryEtcModelsList = Consts.DefaultEtcModelsListFileDirectory;
            configs.FileDiretoryItemsList = Consts.DefaultItemsListFileDirectory;

            configs.SkyColor = Color.FromArgb(0xFF, 0x94, 0xD2, 0xFF);
            // colocar novas configurões aqui;
            configs.FrationalAmount = 9;
            configs.FrationalSymbol = ConfigFrationalSymbol.AcceptsCommaAndPeriod_OutputPeriod;

            configs.ItemDisableRotationAll = false;
            configs.ItemDisableRotationIfXorYorZequalZero = false;
            configs.ItemDisableRotationIfZisNotGreaterThanZero = true;
            configs.ItemRotationOrder = ObjRotationOrder.RotationXY;
            configs.ItemRotationCalculationMultiplier = 1;
            configs.ItemRotationCalculationDivider = 1;

            configs.UseDarkerGrayTheme = false;
            configs.LoadLangTranslation = false;
            configs.LangJsonFile = "";
            return configs;
        }

        /// <summary>
        /// metodo que tem como função carregar as cofigurações ao carregar;
        /// </summary>
        public static void StartLoadConfigs()
        {
            if (File.Exists(Consts.ConfigsFileDirectory))
            {
                Configs configs = GetDefaultConfigs();
                // para caso o arquivo não consiga ser lido
                try { configs = ConfigsFile.parseConfigs(Consts.ConfigsFileDirectory); } catch (Exception) { }

                Globals.BackupConfigs = configs;
                Globals.DirectoryXFILE = configs.DirectoryXFILE;
                Globals.Directory2007RE4 = configs.Directory2007RE4;
                Globals.DirectoryPS2RE4 = configs.DirectoryPS2RE4;
                Globals.DirectoryUHDRE4 = configs.DirectoryUHDRE4;
                Globals.DirectoryCustom1 = configs.DirectoryCustom1;
                Globals.DirectoryCustom2 = configs.DirectoryCustom2;
                Globals.DirectoryCustom3 = configs.DirectoryCustom3;

                Globals.FileDiretoryEnemiesList = configs.FileDiretoryEnemiesList;
                Globals.FileDiretoryEtcModelsList = configs.FileDiretoryEtcModelsList;
                Globals.FileDiretoryItemsList = configs.FileDiretoryItemsList;

                Globals.SkyColor = configs.SkyColor;

                // colocar novas configurões aqui;
                Globals.FrationalAmount = configs.FrationalAmount;
                Globals.FrationalSymbol = configs.FrationalSymbol;

                Globals.ItemDisableRotationAll = configs.ItemDisableRotationAll;
                Globals.ItemDisableRotationIfXorYorZequalZero = configs.ItemDisableRotationIfXorYorZequalZero;
                Globals.ItemDisableRotationIfZisNotGreaterThanZero = configs.ItemDisableRotationIfZisNotGreaterThanZero;
                Globals.ItemRotationOrder = configs.ItemRotationOrder;
                Globals.ItemRotationCalculationMultiplier = configs.ItemRotationCalculationMultiplier;
                Globals.ItemRotationCalculationDivider = configs.ItemRotationCalculationDivider;
            }
            else
            {
                // para caso o arquivo não consiga ser gravado
                try { ConfigsFile.writeConfigsFile(Consts.ConfigsFileDirectory, GetDefaultConfigs()); } catch (Exception) { }

                Globals.BackupConfigs = GetDefaultConfigs();
            }

        }
    }
}
