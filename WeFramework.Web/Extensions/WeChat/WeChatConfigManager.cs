using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Extensions.WeChat
{
    public static class WeChatConfigManager
    {
        static Lazy<NameValueCollection> weChatProperties = new Lazy<NameValueCollection>(WeChatConfigInitialize);

        private static NameValueCollection WeChatConfigInitialize()
        {
            string configurationFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WeChat.config");

            var configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilepath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            System.Xml.XmlDocument sectionXmlDocument = new System.Xml.XmlDocument();
            sectionXmlDocument.Load(new StringReader(configuration.GetSection("wechat").SectionInformation.GetRawXml()));
            NameValueSectionHandler handler = new NameValueSectionHandler();

            return (NameValueCollection)handler.Create(null, null, sectionXmlDocument.DocumentElement);
        }

        public static NameValueCollection Settings => weChatProperties.Value;
    }
}