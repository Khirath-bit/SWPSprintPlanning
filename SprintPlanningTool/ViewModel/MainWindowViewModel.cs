using Utility.MVVM;

namespace SprintPlanningTool.ViewModel
{
    internal class MainWindowViewModel : ObservableObject
    {
        private object _control;

        public object Control
        {
            get => _control;
            set => SetField(ref _control, value);
        }

        public void Load()
        {
            Config.Load();
            Control = new SprintPlanningViewModel();
        }
    }
}
