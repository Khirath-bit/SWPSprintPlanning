using Utility.MVVM;

namespace SprintPlanningTool.ViewModel
{
    internal class UserStatisticsViewModel : ObservableObject
    {
        private string _username;
        private string _linesOfCodeWrittenText = "Lines of Code (Added+Deleted): ";
        private string _storyPointsDoneText = "StoryPoints done: ";
        private string _bugsDoneText = "Bugs done: ";
        private string _ticketsDoneText = "Tickets (without bugs) done: ";

        public UserStatisticsViewModel(string username, string linesOfCodeWritten, string storyPointsDone, string bugsDone, string ticketsDone,
            string jqlTicketsDone, string jqlBugsDone, string linkBitbucketStatistics)
        {
            Username += username;
            LinesOfCodeWrittenText += linesOfCodeWritten;
            StoryPointsDoneText += storyPointsDone;
            BugsDoneText += bugsDone;
            TicketsDoneText += ticketsDone;
        }

        public string Username
        {
            get => _username;
            set => SetField(ref _username, value);
        }

        public string LinesOfCodeWrittenText
        {
            get => _linesOfCodeWrittenText;
            set => SetField(ref _linesOfCodeWrittenText, value);
        }

        public string StoryPointsDoneText
        {
            get => _storyPointsDoneText;
            set => SetField(ref _storyPointsDoneText, value);
        }

        public string BugsDoneText
        {
            get => _bugsDoneText;
            set => SetField(ref _bugsDoneText, value);
        }

        public string TicketsDoneText
        {
            get => _ticketsDoneText;
            set => SetField(ref _ticketsDoneText, value);
        }
    }
}
