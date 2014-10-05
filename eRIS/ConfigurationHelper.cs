using System.Xml;

namespace eRIS
{
    public static class ConfigurationHelper
    {
        public static string GetAppSetting(string settingName)
        {
            string strValue = string.Empty;
            var settings = new XmlReaderSettings();           
            settings.XmlResolver = new XmlXapResolver();


            XmlReader reader = XmlReader.Create("ServiceReferences.ClientConfig");
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "add")
                {
                    if (reader.HasAttributes)
                    {
                        strValue = reader.GetAttribute("key");
                        if (!string.IsNullOrEmpty(strValue) && strValue == settingName)
                        {
                            strValue = reader.GetAttribute("value");
                            return strValue;
                        }
                    }
                }
            }
            return strValue;
        }
    }
}