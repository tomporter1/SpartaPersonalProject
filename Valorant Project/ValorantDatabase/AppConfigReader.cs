
using System.Configuration;
using System.Text.Json.Serialization;

namespace ValorantDatabase
{
    public static class AppConfigReader
    {
        public static readonly string ConnectionString = ConfigurationManager.AppSettings["connection_string"];
    }
}