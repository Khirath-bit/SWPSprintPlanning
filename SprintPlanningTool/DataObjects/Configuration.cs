using System.Collections.Generic;
using System.Windows.Documents;

namespace SprintPlanningTool.DataObjects
{
    internal class Configuration
    {
        public string AtlassianUsername { get; set; }
        public string AtlassianPassword { get; set; }
        public string JiraBaseUrl { get; set; }
        public string BitbucketBaseUrl { get; set; }
        public string BitbucketProject { get; set; }
        public string BitbucketRepo { get; set; }

        public string CalculateCodeMetricsSince { get; set; }

        public List<ParticipatingJiraUsersConfig> ParticipatingJiraUsers { get; set; }
    }

    public class ParticipatingJiraUsersConfig
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public List<string> Email { get; set; }
    }
}
