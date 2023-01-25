using MahApps.Metro.Controls;
using SprintPlanningTool.ViewModel;

namespace SprintPlanningTool.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(DataContext is MainWindowViewModel vm)
                vm.Load();
        }
    }
}
