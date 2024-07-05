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
    public static class RoomListObjFile
    {
        public static void WriteToFile(string jsonFilePath, RoomListObj roomlist)
        {
            JObject entry = new JObject();
            entry["Description"] = roomlist.Description;
            entry["Folder"] = roomlist.Folder;
            JObject o = new JObject();
            o["RoomList"] = entry;
            File.WriteAllText(jsonFilePath, o.ToString());
        }

        private static bool checkValidEntry(JObject entry)
        {
            return (entry["Folder"] != null && entry["Description"] != null);
        }

        public static RoomListObj ParseFromFile(string jsonFilePath)
        {
            RoomListObj roomlist = null;

            FileInfo fileInfo = new FileInfo(jsonFilePath);

            if (fileInfo.Exists)
            {
                string jsonFileName = fileInfo.Name.ToLowerInvariant();

                string json = File.ReadAllText(jsonFilePath);
                JObject o = JObject.Parse(json);
                if (o["RoomList"] != null)
                {
                    JObject entry = (JObject)o["RoomList"];
                    if (checkValidEntry(entry))
                    {
                        string Description = entry["Description"].ToString();
                        string Folder = entry["Folder"].ToString();

                        roomlist = new RoomListObj(jsonFileName, Folder, Description);
                    }

                }
            }

            return roomlist;
        }

    }
}
