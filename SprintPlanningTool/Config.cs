using Newtonsoft.Json;
using SprintPlanningTool.DataObjects;
using System.IO;

namespace SprintPlanningTool
{
    internal static class Config
    {
        private static Configuration _config;

        public static Configuration Get => _config;

        public static void Load()
        {
            _config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("Configuration.json"));
        }
    }
}
