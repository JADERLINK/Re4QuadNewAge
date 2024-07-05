using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Re4QuadExtremeEditor.src.Class;

namespace Re4QuadExtremeEditor.src.JSON
{
    static class PromptMessageListFile
    {
        public static void writePromptMessageFile(string filename, Dictionary<byte, ByteObjForListBox> list)
        {
            JObject jList = new JObject();
            foreach (var item in list)
            {
                jList[item.Key.ToString("X2")] = item.Value.Description.Replace(item.Key.ToString("X2") + ": ", "");
            }
            JObject o = new JObject();
            o["PromptMessageList"] = jList;
            File.WriteAllText(filename, o.ToString());
        }

        public static Dictionary<byte, ByteObjForListBox> parsePromptMessageList(string filename)
        {
            Dictionary<byte, ByteObjForListBox> r = new Dictionary<byte, ByteObjForListBox>();

            if (File.Exists(filename))
            {
                string json = null;
                JObject o = null;
                try { json = File.ReadAllText(filename); } catch (Exception) { }
                try { o = JObject.Parse(json); } catch (Exception) { }

                if (o != null && o["PromptMessageList"] != null)
                {
                    JObject jList = (JObject)o["PromptMessageList"];
                    for (int i = 0; i <= byte.MaxValue; i++)
                    {
                        if (jList[i.ToString("X2")] != null)
                        {
                            string Value = jList[i.ToString("X2")].ToString();
                            r.Add((byte)i, new ByteObjForListBox((byte)i, i.ToString("X2") + ": " + Value));
                        }
                    }
                }
            }

            return r;
        }
    }
}
