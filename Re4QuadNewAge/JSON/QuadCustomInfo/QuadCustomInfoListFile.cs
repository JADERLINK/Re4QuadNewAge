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
    public static class QuadCustomInfoListFile
    {
        public static void WriteToFile(string jsonFilePath, QuadCustomInfoList info)
        {
            JArray array = new JArray();
            foreach (var item in info.List.Values)
            {
                JObject entry = new JObject();
                entry["ID"] = item.ID.ToString();
                entry["ObjectModel"] = item.ObjectModel.ToLowerInvariant();
                entry["Name"] = item.Name;
                array.Add(entry);
            }

            JObject oInfo = new JObject();
            oInfo["ListName"] = info.ListName;
            oInfo["Folder"] = info.Folder;
            oInfo["List"] = array;

            JObject o = new JObject();
            o["QuadCustom"] = oInfo;

            File.WriteAllText(jsonFilePath, o.ToString());
        }

        private static bool checkValidEntry(JObject entry)
        {
            return (entry["ID"] != null && entry["ObjectModel"] != null && entry["Name"] != null);
        }

        public static QuadCustomInfoList ParseFromFile(string jsonFilePath)
        {
            QuadCustomInfoList objectInfoList = new QuadCustomInfoList(Consts.NameNull, Consts.NameNull, Consts.NameNull, new Dictionary<uint, QuadCustomInfo>());

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);
                if (o["QuadCustom"] != null)
                {
                    Dictionary<uint, QuadCustomInfo> List = new Dictionary<uint, QuadCustomInfo>();

                    JObject oInfo = (JObject)o["QuadCustom"];
                    string ListName = oInfo["ListName"].ToString();
                    string Folder = oInfo["Folder"].ToString();
                    JArray array = (JArray)oInfo["List"];
                    foreach (JToken token in array.Children())
                    {
                        JObject entry = (JObject)token;
                        if (checkValidEntry(entry))
                        {
                            uint ID = uint.Parse(entry["ID"].ToString(), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
                            string ObjectModel = entry["ObjectModel"].ToString().ToLowerInvariant();
                            string Name = entry["Name"].ToString();
                            QuadCustomInfo obj = new QuadCustomInfo(ID, ObjectModel, Name);
                            if (!List.ContainsKey(ID))
                            {
                                List.Add(ID, obj);
                            }
                        }

                    }
                    objectInfoList = new QuadCustomInfoList(fileInfo.Name.ToLowerInvariant(), ListName, Folder, List);
                }
            }

            return objectInfoList;
        }

        public static QuadCustomInfoList ParseFromFileForOptions(string jsonFilePath)
        {
            QuadCustomInfoList objectInfoList = null;

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);
                if (o["QuadCustom"] != null)
                {
                    Dictionary<uint, QuadCustomInfo> List = new Dictionary<uint, QuadCustomInfo>();
                    JObject oInfo = (JObject)o["QuadCustom"];
                    string ListName = oInfo["ListName"].ToString();
                    string Folder = oInfo["Folder"].ToString();
                    objectInfoList = new QuadCustomInfoList(fileInfo.Name.ToLowerInvariant(), ListName, Folder, List);
                }
            }

            return objectInfoList;
        }
    }
}