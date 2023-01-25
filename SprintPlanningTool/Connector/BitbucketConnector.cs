using RestSharp;
using RestSharp.Authenticators;
using SprintPlanningTool.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprintPlanningTool.Connector
{
    internal class BitbucketConnector
    {
        private RestClient _client;

        private Dictionary<string, CodeLineInfoLinesOfCode> _data = new();

        public BitbucketConnector()
        {
            _client = new RestClient(Config.Get.BitbucketBaseUrl);

            _client.Authenticator = new HttpBasicAuthenticator(Config.Get.AtlassianUsername, Config.Get.AtlassianPassword);
        }

        public async Task LoadCodeInfos()
        {
            var req = new RestRequest($"/rest/awesome-graphs-api/latest/projects/{Config.Get.BitbucketProject}/repos/{Config.Get.BitbucketRepo}/commits");

            req.AddQueryParameter("sinceDate", Config.Get.CalculateCodeMetricsSince, false);
            req.AddQueryParameter("merges", "exclude");
            req.AddQueryParameter("limit", "100");
            req.AddQueryParameter("start", 0);

            var res = await _client.GetAsync<CodeLineInfoResponse>(req);

            foreach (var item in res.Values)
            {
                var name = item.User?.Name ?? Config.Get.ParticipatingJiraUsers.First(f => f.Email.Contains(item.Author.EmailAddress)).Username;

                if (_data.ContainsKey(name))
                {
                    _data[name].Added += item.LinesOfCode.Added;
                    _data[name].Deleted += item.LinesOfCode.Deleted;
                    continue;
                }

                _data.Add(name, item.LinesOfCode);
            }

            var counter = 0;

            while (!res.IsLastPage)
            {
                counter += 100;

                Thread.Sleep(100);

                req = new RestRequest($"/rest/awesome-graphs-api/latest/projects/{Config.Get.BitbucketProject}/repos/{Config.Get.BitbucketRepo}/commits");

                req.AddQueryParameter("sinceDate", Config.Get.CalculateCodeMetricsSince, false);
                req.AddQueryParameter("merges", "exclude");
                req.AddQueryParameter("limit", "100");
                req.AddQueryParameter("start", counter);

                res = await _client.GetAsync<CodeLineInfoResponse>(req);

                foreach (var item in res.Values)
                {
                    var name = item.User?.Name ?? Config.Get.ParticipatingJiraUsers.First(f => f.Email.Contains(item.Author.EmailAddress)).Username;

                    if (_data.ContainsKey(name))
                    {
                        _data[name].Added += item.LinesOfCode.Added;
                        _data[name].Deleted += item.LinesOfCode.Deleted;
                        continue;
                    }

                    _data.Add(name, item.LinesOfCode);
                }
            }
        }

        public CodeLineInfoLinesOfCode GetCodeInfos(string username)
        {
            return _data.ContainsKey(username) ? _data[username] : new CodeLineInfoLinesOfCode { Added = 0, Deleted = 0 };
        }
    }
}
