using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;

namespace Re4QuadExtremeEditor.src.JSON
{
    static class ConfigsFile
    {

        public static void writeConfigsFile(string filename, Configs config)
        {
            JObject entry = new JObject();
            entry["DirectoryXFILE"] = config.DirectoryXFILE;
            entry["Directory2007RE4"] = config.Directory2007RE4;
            entry["DirectoryPS2RE4"] = config.DirectoryPS2RE4;
            entry["DirectoryUHDRE4"] = config.DirectoryUHDRE4;
            entry["DirectoryCustom1"] = config.DirectoryCustom1;
            entry["DirectoryCustom2"] = config.DirectoryCustom2;
            entry["DirectoryCustom3"] = config.DirectoryCustom3;

            entry["FileDiretoryEnemiesList"] = config.FileDiretoryEnemiesList;
            entry["FileDiretoryEtcModelsList"] = config.FileDiretoryEtcModelsList;
            entry["FileDiretoryItemsList"] = config.FileDiretoryItemsList;


            entry["SkyColor"] = config.SkyColor.ToArgb().ToString("X8");
            entry["FrationalSymbol"] = (int)config.FrationalSymbol;
            entry["FrationalAmount"] = config.FrationalAmount;
            entry["ItemDisableRotationAll"] = config.ItemDisableRotationAll;
            entry["ItemDisableRotationIfXorYorZequalZero"] = config.ItemDisableRotationIfXorYorZequalZero;
            entry["ItemDisableRotationIfZisNotGreaterThanZero"] = config.ItemDisableRotationIfZisNotGreaterThanZero;
            entry["ItemRotationCalculationDivider"] = config.ItemRotationCalculationDivider;
            entry["ItemRotationCalculationMultiplier"] = config.ItemRotationCalculationMultiplier;
            entry["ItemRotationOrder"] = (int)config.ItemRotationOrder;
            entry["UseDarkerGrayTheme"] = config.UseDarkerGrayTheme;
            entry["LoadLangTranslation"] = config.LoadLangTranslation;
            entry["LangJsonFile"] = config.LangJsonFile;

            JObject o = new JObject();
            o["Configs"] = entry;
            try
            {
                File.WriteAllText(filename, o.ToString());
            }
            catch (Exception)
            {
            }
            
        }

        private static string FixDirectory(string dir)
        {
            return dir != null && dir.Length > 0 ? (dir + (dir.Last() != '\\' ? "\\" : "")) : "";
        }

        public static Configs parseConfigs(string filename)
        {
            Configs config = Configs.GetDefaultConfigs();
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                JObject o = JObject.Parse(json);
                if (o["Configs"] != null)
                {
                    JObject oConfigs = (JObject)o["Configs"];

                    config.DirectoryXFILE = FixDirectory(oConfigs?["DirectoryXFILE"]?.ToString());
                    config.Directory2007RE4 = FixDirectory(oConfigs?["Directory2007RE4"]?.ToString());
                    config.DirectoryPS2RE4 = FixDirectory(oConfigs?["DirectoryPS2RE4"]?.ToString());
                    config.DirectoryUHDRE4 = FixDirectory(oConfigs?["DirectoryUHDRE4"]?.ToString());
                    config.DirectoryCustom1 = FixDirectory(oConfigs?["DirectoryCustom1"]?.ToString());
                    config.DirectoryCustom2 = FixDirectory(oConfigs?["DirectoryCustom2"]?.ToString());
                    config.DirectoryCustom3 = FixDirectory(oConfigs?["DirectoryCustom3"]?.ToString());

                    config.FileDiretoryEnemiesList = oConfigs?["FileDiretoryEnemiesList"]?.ToString() ?? Consts.DefaultEnemiesListFileDirectory;
                    config.FileDiretoryEtcModelsList = oConfigs?["FileDiretoryEtcModelsList"]?.ToString() ?? Consts.DefaultEtcModelsListFileDirectory;
                    config.FileDiretoryItemsList = oConfigs?["FileDiretoryItemsList"]?.ToString() ?? Consts.DefaultItemsListFileDirectory;

                    if (oConfigs["SkyColor"] != null)
                    {
                        try
                        {
                            config.SkyColor = Color.FromArgb(int.Parse(oConfigs["SkyColor"].ToString(), System.Globalization.NumberStyles.HexNumber));
                        }
                        catch (Exception)
                        {
                        }
                      
                    }

                    // colocar novas configurações aqui;

                    if (oConfigs["FrationalSymbol"] != null)
                    {
                        int value = 0;
                        try
                        {
                            value = int.Parse(oConfigs["FrationalSymbol"].ToString(), System.Globalization.NumberStyles.Number);
                        }
                        catch (Exception)
                        {
                        }
                        if (value > 3)
                        {
                            value = 0;
                        }
                        else if (value < 0)
                        {
                            value = 0;
                        }

                        config.FrationalSymbol = (Class.Enums.ConfigFrationalSymbol)value;
                    }

                    if (oConfigs["FrationalAmount"] != null)
                    {
                        int value = 9;
                        try
                        {
                            value = int.Parse(oConfigs["FrationalAmount"].ToString(), System.Globalization.NumberStyles.Number);
                        }
                        catch (Exception)
                        {
                        }
                        if (value > 9)
                        {
                            value = 9;
                        }
                        else if (value < 4)
                        {
                            value = 4;
                        }
                        config.FrationalAmount = value;
                    }

                    if (oConfigs["ItemDisableRotationAll"] != null)
                    {
                        try
                        {
                            config.ItemDisableRotationAll = bool.Parse(oConfigs["ItemDisableRotationAll"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                       
                    }

                    if (oConfigs["ItemDisableRotationIfXorYorZequalZero"] != null)
                    {
                        try
                        {
                            config.ItemDisableRotationIfXorYorZequalZero = bool.Parse(oConfigs["ItemDisableRotationIfXorYorZequalZero"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                    }

                    if (oConfigs["ItemDisableRotationIfZisNotGreaterThanZero"] != null)
                    {
                        try
                        {
                            config.ItemDisableRotationIfZisNotGreaterThanZero = bool.Parse(oConfigs["ItemDisableRotationIfZisNotGreaterThanZero"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                    }


                    if (oConfigs["ItemRotationCalculationDivider"] != null)
                    {
                        try
                        {
                            config.ItemRotationCalculationDivider = float.Parse(oConfigs["ItemRotationCalculationDivider"].ToString(), System.Globalization.NumberStyles.Float);
                        }
                        catch (Exception)
                        {
                        }

                    }

                    if (oConfigs["ItemRotationCalculationMultiplier"] != null)
                    {
                        try
                        {
                            config.ItemRotationCalculationMultiplier = float.Parse(oConfigs["ItemRotationCalculationMultiplier"].ToString(), System.Globalization.NumberStyles.Float);
                        }
                        catch (Exception)
                        {
                        }

                    }

                    if (oConfigs["ItemRotationOrder"] != null)
                    {
                        int value = 0;
                        try
                        {
                            value = int.Parse(oConfigs["ItemRotationOrder"].ToString(), System.Globalization.NumberStyles.Number);
                        }
                        catch (Exception)
                        {
                        }
                        if (value > 14)
                        {
                            value = 0;
                        }
                        else if (value < 0)
                        {
                            value = 0;
                        }
                        config.ItemRotationOrder = (Class.Enums.ObjRotationOrder)value;
                    }

                    if (oConfigs["UseDarkerGrayTheme"] != null)
                    {
                        try
                        {
                            config.UseDarkerGrayTheme = bool.Parse(oConfigs["UseDarkerGrayTheme"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (oConfigs["LoadLangTranslation"] != null)
                    {
                        try
                        {
                            config.LoadLangTranslation = bool.Parse(oConfigs["LoadLangTranslation"].ToString());
                        }
                        catch (Exception)
                        {
                        }

                    }

                    if (oConfigs["LangJsonFile"] != null)
                    {
                        config.LangJsonFile = oConfigs["LangJsonFile"].ToString();
                    }

                }

            }
            return config;
        }



    }
}
