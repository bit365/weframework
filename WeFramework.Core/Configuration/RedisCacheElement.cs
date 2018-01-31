using System.Configuration;

namespace WeFramework.Core.Configuration
{
    public class RedisCacheElement : ConfigurationElement
    {
        private const string EnabledPropertyName = "enabled";

        private const string ConnectionStringPropertyName = "connectionString";

        [ConfigurationProperty(EnabledPropertyName, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)base[EnabledPropertyName]; }
            set { base[EnabledPropertyName] = value; }
        }

        [ConfigurationProperty(ConnectionStringPropertyName, IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base[ConnectionStringPropertyName]; }
            set { base[ConnectionStringPropertyName] = value; }
        }
    }
}
