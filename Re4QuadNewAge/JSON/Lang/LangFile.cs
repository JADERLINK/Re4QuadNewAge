using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.JSON
{
    public class LangFile
    {
        public static void WriteToLangFile(string filename)
        {
            JObject eLang = new JObject();
            foreach (var item in Lang.Text)
            {
                eLang[Enum.GetName(typeof(eLang), item.Key)] = item.Value;
            }

            JObject aLang = new JObject();
            foreach (var item in Lang.AttributeText)
            {
                aLang[Enum.GetName(typeof(aLang), item.Key)] = item.Value;
            }

            JObject entry = new JObject();
            entry["LangName"] = "English (From Json File)";
            entry["eLang"] = eLang;
            entry["aLang"] = aLang;

            JObject o = new JObject();
            o["LangV2"] = entry;
            try { File.WriteAllText(filename, o.ToString()); } catch (Exception) { }
        }

        public static LangObjForList ParseFromFileForList(string filename)
        {
            LangObjForList obj = null;

            if (File.Exists(filename))
            {
                string json = null;
                JObject o = null;
                try { json = File.ReadAllText(filename); } catch (Exception) { }
                try { o = JObject.Parse(json); } catch (Exception) { }

                if (o != null && o["LangV2"] != null)
                {
                    try
                    {
                        FileInfo info = new FileInfo(filename);
                        string fileName = info.Name.ToLowerInvariant();

                        string langName = "Null";
                        JObject oLang = (JObject)o["LangV2"];

                        if (oLang["LangName"] != null)
                        {
                            langName = oLang["LangName"].ToString();
                        }

                        obj = new LangObjForList(langName, fileName);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            return obj;
        }


        public static void ParseFromFileLang(string filename)
        {
            if (File.Exists(filename))
            {
                string json = null;
                JObject o = null;
                try { json = File.ReadAllText(filename); } catch (Exception) { }
                try { o = JObject.Parse(json); } catch (Exception) { }

                if (o != null && o["LangV2"] != null)
                {
                    JObject oLang = (JObject)o["LangV2"];
                    
                    if (oLang["eLang"] != null)
                    {
                        JObject eLangJSON = (JObject)oLang["eLang"];
                        foreach (var item in eLangJSON)
                        {
                            eLang e = eLang.Null;
                            try { e = (eLang)Enum.Parse(typeof(eLang), item.Key); } catch (Exception) { }
                            if (e != eLang.Null)
                            {
                                Lang.SetText(e, item.Value.ToString());
                            }
                        }
                    }

                    if (oLang["aLang"] != null)
                    {
                        JObject aLangJSON = (JObject)oLang["aLang"];
                        foreach (var item in aLangJSON)
                        {
                            aLang a = aLang.Null;
                            try { a = (aLang)Enum.Parse(typeof(aLang), item.Key); } catch (Exception) { }
                            if (a != aLang.Null)
                            {
                                Lang.SetAttributeText(a, item.Value.ToString());
                            }
                        }
                    }

                }

            }
        }

    }
}
