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
    public static class ObjectInfoListFile
    {
        public static void WriteToFile(string jsonFilePath, string mainTag, ObjectInfoList info)
        {
            JArray array = new JArray();
            foreach (var item in info.List.Values)
            {
                JObject entry = new JObject();
                entry["HexID"] = item.HexID.ToString("X4");
                entry["ObjectModel"] = item.ObjectModel.ToLowerInvariant();
                entry["Name"] = item.Name;
                entry["Description"] = item.Description;
                array.Add(entry);
            }

            JObject oInfo = new JObject();
            oInfo["ListName"] = info.ListName;
            oInfo["Folder"] = info.Folder;
            oInfo["List"] = array;

            JObject o = new JObject();
            o[mainTag] = oInfo;

            File.WriteAllText(jsonFilePath, o.ToString());
        }

        private static bool checkValidEntry(JObject entry)
        {
            return (entry["HexID"] != null && entry["ObjectModel"] != null
                 && entry["Name"] != null && entry["Description"] != null);
        }

        public static ObjectInfoList ParseFromFile(string jsonFilePath, string mainTag)
        {
            ObjectInfoList objectInfoList = new ObjectInfoList(Consts.NameNull, Consts.NameNull, Consts.NameNull, new Dictionary<ushort, ObjectInfo>());

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);
                if (o[mainTag] != null)
                {
                    Dictionary<ushort, ObjectInfo> List = new Dictionary<ushort, ObjectInfo>();

                    JObject oInfo = (JObject)o[mainTag];
                    string ListName = oInfo["ListName"].ToString();
                    string Folder = oInfo["Folder"].ToString();
                    JArray array = (JArray)oInfo["List"];
                    foreach (JToken token in array.Children())
                    {
                        JObject entry = (JObject)token;
                        if (checkValidEntry(entry))
                        {
                            ushort HexID = ushort.Parse(entry["HexID"].ToString(), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
                            string ObjectModel = entry["ObjectModel"].ToString().ToLowerInvariant();
                            string Name = entry["Name"].ToString();
                            string Description = entry["Description"].ToString();
                            ObjectInfo obj = new ObjectInfo(HexID, ObjectModel, Name, Description);
                            if (!List.ContainsKey(HexID))
                            {
                                List.Add(HexID, obj);
                            }
                        }

                    }
                    objectInfoList = new ObjectInfoList(fileInfo.Name.ToLowerInvariant(), ListName, Folder, List);
                }
            }

            return objectInfoList;
        }

        public static ObjectInfoList ParseFromFileForOptions(string jsonFilePath, string mainTag) 
        {
            ObjectInfoList objectInfoList = null;

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);
                if (o[mainTag] != null)
                {
                    Dictionary<ushort, ObjectInfo> List = new Dictionary<ushort, ObjectInfo>();
                    JObject oInfo = (JObject)o[mainTag];
                    string ListName = oInfo["ListName"].ToString();
                    string Folder = oInfo["Folder"].ToString();
                    objectInfoList = new ObjectInfoList(fileInfo.Name.ToLowerInvariant(), ListName, Folder, List);
                }
            }

            return objectInfoList;
        }
    }
}
