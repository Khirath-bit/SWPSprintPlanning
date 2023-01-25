using SprintPlanningTool.Connector;
using SprintPlanningTool.DataObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Utility.MVVM;

namespace SprintPlanningTool.ViewModel
{
    internal class SprintPlanningViewModel : ObservableObject
    {
        private JiraConnector _jiraConnector;
        private BitbucketConnector _bitbucketConnector;

        private ObservableCollection<object> _userStatistics = new ObservableCollection<object>();

        public ObservableCollection<object> UserStatistics
        {
            get => _userStatistics;
            set => SetField(ref _userStatistics, value);
        }

        private ObservableCollection<string> _sortedUser = new ObservableCollection<string>();

        public ObservableCollection<string> SortedUser
        {
            get => _sortedUser;
            set => SetField(ref _sortedUser, value);
        }

        public SprintPlanningViewModel()
        {
            _jiraConnector = new JiraConnector();
            _bitbucketConnector =  new BitbucketConnector();
            LoadForAllUsers();
        }

        private async void LoadForAllUsers()
        {
            await _bitbucketConnector.LoadCodeInfos();

            var users = new List<UserStatistics>();

            foreach (var user in Config.Get.ParticipatingJiraUsers)
            {
                var bugsDone = await _jiraConnector.GetDoneBugsCount(user.Username);
                var ticketsDone = await _jiraConnector.GetDoneTickets(user.Username);
                var storyPointsDone = await _jiraConnector.GetStoryPointsDone(user.Username);
                var linesOfCode = _bitbucketConnector.GetCodeInfos(user.Username);

                users.Add(new UserStatistics
                {
                    Name = user.Username,
                    LinesOfCode = linesOfCode.Added + linesOfCode.Deleted,
                    StoryPoints = int.Parse(storyPointsDone)
                }); ;

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    UserStatistics.Add(new UserStatisticsViewModel(user.Name, (linesOfCode.Added + linesOfCode.Deleted).ToString(), storyPointsDone, bugsDone, ticketsDone, "", "", ""));
                });
            }

            var byStoryPoints = users.OrderBy(o => o.StoryPoints).ToList();
            var byLinesOfCode = users.OrderBy(o => o.LinesOfCode).ToList();

            var finished = users.OrderBy(o => 2 * byLinesOfCode.IndexOf(o) + byStoryPoints.IndexOf(o)).ToList();

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                SortedUser = new ObservableCollection<string>(finished.Select(s => (finished.IndexOf(s) + 1) + ". " + s.Name));
            });
            
        }
    }
}
