using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using SubtitlesSync.MVVM;
using SubtitlesSync.Services;
using SubtitlesSync.ViewModel;

namespace SubtitlesSync.View.Windows
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow(object dataContext)
        {
            //Owner = parentWindow;
            InitializeComponent();

            var windowService = new WindowService();
            //var dc = new MainWindowViewModel(windowService);
            //var dc = dataContext;
            //DataContext = dc;
            DataContext = dataContext;

            //CheckDownloadFolder()
            //windowService.
            //CheckDownloadFolderCommand;

            //dataContext.OnMethodCall();
            //dataContext.MethodCall += CheckDownloadFolder;

            //dataContext.MethodCall?.Invoke(this, new PropertyChangedEventArgs("OptionsWindow"));

            MainWindowViewModel dtc = dataContext as MainWindowViewModel;
            Top = (SystemParameters.WorkArea.Height - dtc.OptionsWindowHeight) / 2 + 50;
            Left = (SystemParameters.WorkArea.Width - dtc.OptionsWindowWidth) / 2;
        }


    }
}
