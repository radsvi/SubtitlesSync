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
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
        }

        //private void btnBrowse_Click(object sender, RoutedEventArgs e)
        //{
        //    //WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
        //    OpenFolderDialog folderDialog = new OpenFolderDialog();
        //    //fileDialog.Title = "Please pick a PNG picture file...";
        //    folderDialog.Title = "Select folder containing subtitles...";

        //    bool? success = folderDialog.ShowDialog();
        //    if (success == true)
        //    {
        //        //string path = fileDialog.FileName;
        //        //string fileName = fileDialog.SafeFileName;
        //        // for multiples:
        //        string path = folderDialog.FolderName;

        //        //tbInfo.Text = path;
        //        //tbInfo.Text = fileName;
        //        //for multiples we have to process this into single string, obviously.

        //    }
        //    else
        //    {
        //        // didnt pick anything
        //    }
        //}

        //private void btnRename_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}