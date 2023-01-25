using ControlzEx.Standard;
using RestSharp;
using RestSharp.Authenticators;
using SprintPlanningTool.DataObjects;
using System;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace SprintPlanningTool.Connector
{
    internal class JiraConnector
    {
        private RestClient _client;

        public JiraConnector()
        {
            _client = new RestClient(Config.Get.JiraBaseUrl);

            _client.Authenticator = new HttpBasicAuthenticator(Config.Get.AtlassianUsername, Config.Get.AtlassianPassword);
        }

        public async Task<string> GetDoneBugsCount(string username)
        {
            return await ExecSearch($"assignee = {username} and type = bug and (status = done or status = closed)");
        }

        public async Task<string> GetDoneTickets(string username)
        {
            return await ExecSearch($"assignee = {username} and type != bug and type != Sub-task and type != task and (status = done or status = closed)");
        }

        public async Task<string> GetStoryPointsDone(string username)
        {
            var req = new RestRequest("rest/api/2/search");
            req.AddQueryParameter("jql", $"assignee = {username} and(status = done or status = closed) and \"Story Points\" != null");
            var res = await _client.GetAsync<StoryPointSearchResponse>(req);

            return res.Issues.Sum(s => s.Fields.Customfield_10008).ToString();
        }

        private async Task<string> ExecSearch(string jql)
        {
            var req = new RestRequest("rest/api/2/search");

            req.AddQueryParameter("jql", jql);

            var res = await _client.GetAsync<SearchResponse>(req);

            return res.Total.ToString();
        }
    }
}
