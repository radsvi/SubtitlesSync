using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SubtitlesSync.Services;
using SubtitlesSync.ViewModel;

namespace SubtitlesSync.View.Windows
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            //Owner = parentWindow;
            InitializeComponent();

            var windowService = new WindowService();
            var dc = new MainWindowViewModel(windowService);
            DataContext = dc;

            Top = (SystemParameters.WorkArea.Height - Height) / 2 + 50;
            Left = (SystemParameters.WorkArea.Width - Width) / 2;

            //CheckDownloadFolder();
        }
    }
}
