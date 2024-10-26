using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Re4QuadExtremeEditor.src.JSON
{
    public static class RoomModelFile
    {
        public static void WriteToFile(string jsonFilePath, RoomModel roomModel)
        {
            JObject entry = new JObject();
            entry["Type"] = Enum.GetName(typeof(RoomModel.EType), roomModel.Type);
            entry["PathKey"] = roomModel.PathKey.ToLowerInvariant();
            entry["HexID"] = roomModel.HexID.ToString("X4");
            entry["Description"] = roomModel.Description;
            entry["SmdFile"] = roomModel.SmdFile;
            entry["SmxFile"] = roomModel.SmxFile;

            if (roomModel is RoomModel2007 model2007)
            {
                entry["PmdFolder"] = model2007.PmdFolder;
                entry["PmdBaseName"] = model2007.PmdBaseName;
            }
            else if (roomModel is RoomModelR100Uhd modelR100Uhd) 
            {
                entry["PackFolder"] = modelR100Uhd.PackFolder;
                entry["SharedSmd"] = modelR100Uhd.SharedSmd;
                entry["DatSmd"] = JArray.Parse(JsonConvert.SerializeObject(modelR100Uhd.DatSmd));
            }
            else if (roomModel is RoomModelUhd modelUhd)
            {
                entry["PackFolder"] = modelUhd.PackFolder;
            }

            JObject o = new JObject();
            o["RoomModel"] = entry;

            File.WriteAllText(jsonFilePath, o.ToString());
        }

        private static bool checkValidEntry(JObject entry)
        {
            return (entry["HexID"] != null && entry["Description"] != null && entry["Type"] != null
                 && entry["PathKey"] != null && entry["SmdFile"] != null);
        }

        public static RoomModel ParseFromFile(string jsonFilePath)
        {
            RoomModel roomModel = null;

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string jsonFileName = fileInfo.Name.ToLowerInvariant();

                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);
                if (o["RoomModel"] != null)
                {
                    JObject entry = (JObject)o["RoomModel"];
                    if (checkValidEntry(entry))
                    {
                        ushort HexID = ushort.Parse(entry["HexID"].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                        string Description = entry["Description"].ToString();
                        string PathKey = entry["PathKey"].ToString().ToLowerInvariant();
                        RoomModel.EType type = RoomModel.EType.NULL;
                        if (entry["Type"] != null)
                        {
                            try
                            {
                                type = (RoomModel.EType)Enum.Parse(typeof(RoomModel.EType), entry["Type"].ToString());
                            }
                            catch (Exception)
                            {
                            }
                        }
                        string SmdFile = entry["SmdFile"].ToString();
                        string SmxFile = "";
                        if (entry["SmxFile"] != null)
                        {
                            SmxFile = entry["SmxFile"].ToString();
                        }

                        switch (type)
                        {
                            case RoomModel.EType.PS2:
                                {
                                    roomModel = new RoomModelPs2(jsonFileName, HexID, Description, PathKey, type, SmdFile, SmxFile);
                                }
                                break;
                            case RoomModel.EType.V2007:
                                {
                                    string PmdFolder = "";
                                    string PmdBaseName = "";
                                    if (entry["PmdFolder"] != null)
                                    {
                                        PmdFolder = entry["PmdFolder"].ToString();
                                    }
                                    if (entry["PmdBaseName"] != null)
                                    {
                                        PmdBaseName = entry["PmdBaseName"].ToString();
                                    }
                                    roomModel = new RoomModel2007(jsonFileName, HexID, Description, PathKey, type, SmdFile, SmxFile, PmdFolder, PmdBaseName);
                                }
                                break;
                            case RoomModel.EType.PS4NS:
                            case RoomModel.EType.UHD:
                                {
                                    string PackFolder = "";
                                    if (entry["PackFolder"] != null)
                                    {
                                        PackFolder = entry["PackFolder"].ToString();
                                    }
                                    roomModel = new RoomModelUhd(jsonFileName, HexID, Description, PathKey, type, SmdFile, SmxFile, PackFolder);
                                }
                                break;
                            case RoomModel.EType.R100PS4NS:
                            case RoomModel.EType.R100UHD:
                                {
                                    string PackFolder = "";
                                    if (entry["PackFolder"] != null)
                                    {
                                        PackFolder = entry["PackFolder"].ToString();
                                    }

                                    string SharedSmd = "";
                                    if (entry["SharedSmd"] != null)
                                    {
                                        SharedSmd = entry["SharedSmd"].ToString();
                                    }

                                    string[] DatSmd = new string[0];

                                    if (entry["DatSmd"] != null)
                                    {
                                        DatSmd = JsonConvert.DeserializeObject<string[]>(entry["DatSmd"].ToString());
                                    }

                                    roomModel = new RoomModelR100Uhd(jsonFileName, HexID, Description, PathKey, type, SmdFile, SmxFile, PackFolder, SharedSmd, DatSmd);
                                }
                                break;
                            case RoomModel.EType.NULL:
                            default:
                                break;
                        }
                    }

                }

            }

            return roomModel;
        }

    }
}
