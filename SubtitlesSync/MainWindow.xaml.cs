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
        }
    }
}