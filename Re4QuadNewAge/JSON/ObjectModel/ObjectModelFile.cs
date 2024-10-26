using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using OpenTK;
using ViewerBase;

namespace Re4QuadExtremeEditor.src.JSON
{
    public static class ObjectModelFile
    {
        private static string parseFromVector3(Vector3 v)
        {
            List<float> xyz = new List<float>() { v.X, v.Y, v.Z };
            return JsonConvert.SerializeObject(xyz);
        }

        private static Vector3 parseToVector3(string json)
        {
            List<float> xyz = JsonConvert.DeserializeObject<List<float>>(json);
            return new Vector3(xyz[0], xyz[1], xyz[2]);
        }

        private static void SetPreFix(ref JObject obj, PreFix prefix) 
        {
            if (prefix.Position.X != 0
                || prefix.Position.Y != 0
                || prefix.Position.Z != 0
                || prefix.Angle.X != 0
                || prefix.Angle.Y != 0
                || prefix.Angle.Z != 0
                || prefix.Scale.X != 1
                || prefix.Scale.Y != 1
                || prefix.Scale.Z != 1)
            {
                JObject jprefix = new JObject();
                jprefix["Angle"] = parseFromVector3(prefix.Angle);
                jprefix["Position"] = parseFromVector3(prefix.Position);
                jprefix["Scale"] = parseFromVector3(prefix.Scale);
                obj["PreFix"] = jprefix;
            }
        }

        public static void WriteToFile(string jsonFilePath, ObjectModel model)
        {
            JObject entry = new JObject();
            entry["PathKey"] = model.PathKey.ToLowerInvariant();
            entry["Type"] = Enum.GetName(typeof(ObjectModel.EType), model.Type);

            if (model is ObjectModel2007 model2007)
            {
                JArray array = new JArray();
                foreach (var item in model2007.List)
                {
                    JObject obj = new JObject();
                    obj["PmdFile"] = item.PmdFile;
                    SetPreFix(ref obj, item.PreFix);
                    array.Add(obj);
                }
                entry["PmdList"] = array;
            }
            else if (model is ObjectModelPs2 modelPs2) 
            {
                JArray array = new JArray();
                foreach (var item in modelPs2.List)
                {
                    JObject obj = new JObject();
                    obj["Ps2BinFile"] = item.Ps2BinFile;
                    obj["Ps2TplFile"] = item.Ps2TplFile;
                    SetPreFix(ref obj, item.PreFix);
                    array.Add(obj);
                }
                entry["Ps2ModelList"] = array;
            }
            else if (model is ObjectModelUhd modelUhd)
            {
                if (modelUhd.PackPathKey != modelUhd.PathKey)
                {
                    entry["PackPathKey"] = modelUhd.PackPathKey;
                }

                entry["PackFolder"] = modelUhd.PackFolder;
                JArray array = new JArray();
                foreach (var item in modelUhd.List)
                {
                    JObject obj = new JObject();
                    obj["UhdBinFile"] = item.UhdBinFile;
                    obj["UhdTplFile"] = item.UhdTplFile;
                    SetPreFix(ref obj, item.PreFix);
                    array.Add(obj);
                }
                entry["UhdModelList"] = array;
            }

            JObject o = new JObject();
            o["ObjectModel"] = entry;

            File.WriteAllText(jsonFilePath, o.ToString());
        }
                
        private static bool checkValidEntryV2007(JObject entry)
        {
            return (entry["PmdFile"] != null);
        }

        private static bool checkValidEntryPs2(JObject entry)
        {
            return (entry["Ps2BinFile"] != null && entry["Ps2TplFile"] != null);
        }

        private static bool checkValidEntryUhd(JObject entry)
        {
            return (entry["UhdBinFile"] != null && entry["UhdTplFile"] != null);
        }

        private static bool checkValidEntryPreFix(JObject entry)
        {
            return (entry["Angle"] != null && entry["Position"] != null && entry["Scale"] != null);
        }

        private static PreFix GetPreFix(JObject entry) 
        {
            PreFix prefix = new PreFix();
            prefix.Angle = new Vector3(0, 0, 0);
            prefix.Position = new Vector3(0, 0, 0);
            prefix.Scale = new Vector3(1, 1, 1);

            if (entry["PreFix"] != null)
            {
                JObject jprefix = (JObject)entry["PreFix"];
                if (checkValidEntryPreFix(jprefix))
                {
                    prefix.Angle = parseToVector3(jprefix["Angle"].ToString());
                    prefix.Position = parseToVector3(jprefix["Position"].ToString());
                    prefix.Scale = parseToVector3(jprefix["Scale"].ToString());
                }
            }
            return prefix;
        }

        public static ObjectModel ParseFromFile(string jsonFilePath)
        {
            ObjectModel m = null;

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string jsonFileName = fileInfo.Name.ToLowerInvariant();

                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);

                if (o["ObjectModel"] != null)
                {
                    JObject oObjectModel = (JObject)o["ObjectModel"];
                    string PathKey = oObjectModel["PathKey"].ToString().ToLowerInvariant();
                    ObjectModel.EType type = ObjectModel.EType.NULL;

                    if (oObjectModel["Type"] != null)
                    {
                        try
                        {
                            type = (ObjectModel.EType)Enum.Parse(typeof(ObjectModel.EType), oObjectModel["Type"].ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }

                    switch (type)
                    {
                        case ObjectModel.EType.V2007:
                            {
                                List<PmdSub> list = new List<PmdSub>();
                                if (oObjectModel["PmdList"] != null)
                                {
                                    JArray array = (JArray)oObjectModel["PmdList"];
                                    foreach (JToken token in array.Children())
                                    {
                                        JObject entry = (JObject)token;
                                        if (checkValidEntryV2007(entry))
                                        {
                                            string PmdFile = entry["PmdFile"].ToString();
                                            PreFix prefix = GetPreFix(entry);
                                            PmdSub sub = new PmdSub(PmdFile, prefix);
                                            list.Add(sub);
                                        }
                                    }
                                }
                                ObjectModel2007 model2007 = new ObjectModel2007(jsonFileName, type, PathKey, list.ToArray());
                                m = model2007;
                            }
                            break;
                        case ObjectModel.EType.PS2:
                            {
                                List<Ps2Sub> list = new List<Ps2Sub>();
                                if (oObjectModel["Ps2ModelList"] != null)
                                {
                                    JArray array = (JArray)oObjectModel["Ps2ModelList"];
                                    foreach (JToken token in array.Children())
                                    {
                                        JObject entry = (JObject)token;
                                        if (checkValidEntryPs2(entry))
                                        {
                                            string Ps2BinFile = entry["Ps2BinFile"].ToString();
                                            string Ps2TplFile = entry["Ps2TplFile"].ToString();
                                            PreFix prefix = GetPreFix(entry);
                                            Ps2Sub sub = new Ps2Sub(Ps2BinFile, Ps2TplFile, prefix);
                                            list.Add(sub);
                                        }
                                    }
                                }
                                ObjectModelPs2 modelPs2 = new ObjectModelPs2(jsonFileName, type, PathKey, list.ToArray());
                                m = modelPs2;
                            }
                            break;
                        case ObjectModel.EType.PS4NS:
                        case ObjectModel.EType.UHD:
                            {
                                string PackPathKey = PathKey;
                                if (oObjectModel["PackPathKey"] != null)
                                {
                                    PackPathKey = oObjectModel["PackPathKey"].ToString();
                                }

                                string PackFolder = "";
                                if (oObjectModel["PackFolder"] != null)
                                {
                                    PackFolder = oObjectModel["PackFolder"].ToString();
                                }

                                List<UhdSub> list = new List<UhdSub>();
                                if (oObjectModel["UhdModelList"] != null)
                                {
                                    JArray array = (JArray)oObjectModel["UhdModelList"];
                                    foreach (JToken token in array.Children())
                                    {
                                        JObject entry = (JObject)token;
                                        if (checkValidEntryUhd(entry))
                                        {
                                            string UhdBinFile = entry["UhdBinFile"].ToString();
                                            string UhdTplFile = entry["UhdTplFile"].ToString();
                                            PreFix prefix = GetPreFix(entry);
                                            UhdSub sub = new UhdSub(UhdBinFile, UhdTplFile, prefix);
                                            list.Add(sub);
                                        }
                                    }
                                }
                                ObjectModelUhd modelUhd = new ObjectModelUhd(jsonFileName, type, PathKey, PackPathKey, PackFolder, list.ToArray());
                                m = modelUhd;
                            }
                            break;
                        case ObjectModel.EType.NULL:
                        default:
                            break;
                    }

                }
            }

            return m;
        }

    }
}
