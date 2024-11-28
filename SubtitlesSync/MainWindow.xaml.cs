using SubtitlesSync.Services;
using SubtitlesSync.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace SubtitlesSync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MainWindowViewModel vm = new MainWindowViewModel(); // binding pres XAML
            //DataContext = vm;

            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);

            // Create an instance of the WindowService
            var windowService = new WindowService();

            // Set the data context for the main window to a new instance of the ViewModel,
            // passing in the WindowService as a dependency
            DataContext = new MainWindowViewModel(windowService);
        }
    }
}