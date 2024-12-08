using Microsoft.Win32;
using SubtitlesSync.Model;
using SubtitlesSync.MVVM;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using SubtitlesSync.Properties;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;
using SubtitlesSync.Services;
using System.Windows.Input;

namespace SubtitlesSync.ViewModel
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        //private string folderPath = Settings.Default.folderPath;
        private string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                folderPath = value;
                Settings.Default.folderPath = value;
                Settings.Default.Save();
                OnPropertyChanged();

                //if (string.IsNullOrEmpty(value))
                //{
                //    MessageBox.Show("prazdny! null");
                //    folderPath = Settings.Default.folderPath;
                //    return;
                //}
                //else if (value == "null")
                //{
                //    MessageBox.Show("prazdny!");
                //    folderPath = Settings.Default.folderPath;
                //    return;
                //}
                //else
                //{
                //    folderPath = value;
                //    Settings.Default.folderPath = value;
                //    Settings.Default.Save();
                //    OnPropertyChanged();
                //}
            }
        }
        private string subtitlesToSearchFor;

        public string SubtitlesToSearchFor
        {
            get { return subtitlesToSearchFor; }
            set { subtitlesToSearchFor = value; }
        }

        public string[] VideoSuffixes { get; set; } = { ".avi", ".mkv", ".mp4", ".mpg" };
        public string[] SubtitleSuffixes { get; set; } = { ".srt", ".sub" };

        private string subtitlesDownloadSource = Settings.Default.subtitlesDownloadSource;
        public string SubtitlesDownloadSource
        {
            get { return subtitlesDownloadSource; }
            set
            {
                if (value.IndexOf("%s") != -1)
                {
                    subtitlesDownloadSource = value;
                }
            }
        }


        //public List<FilesExtended> VideoFiles { get; set; }
        //public List<FilesExtended> SubtitleFiles { get; set; }

        private List<FilesExtended> folderContent = new List<FilesExtended>();
        public List<FilesExtended> FolderContent
        {
            get { return folderContent; }
            private set { folderContent = value; }
        }

        private List<string> folderBackupContent = new List<string>();
        public List<string> FolderBackupContent
        {
            get { return folderBackupContent; }
            private set { folderBackupContent = value; }
        }

        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>(){};
        //public ObservableCollection<Item> Items { get; set; }

        //public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>(){ // tohle je jen pro design. bude prepsany jakmile pustim appku // ## smazat? Nejak to stejne nefunguje
        //    new Item { VideoDisplayName = "qwer" },
        //    new Item { VideoDisplayName = "asdf" },
        //    new Item { VideoDisplayName = "zxcv" },
        //    new Item { VideoDisplayName = "tyui" }
        //};

        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private int windowHeight = Settings.Default.windowHeight;

        public int WindowHeight
        {
            get { return windowHeight; }
            set {
                windowHeight = value;
                Settings.Default.windowHeight = value;
                Settings.Default.Save();
                OnPropertyChanged();
            }
        }
        private int windowWidth = Settings.Default.windowWidth;

        public int WindowWidth
        {
            get { return windowWidth; }
            set {
                windowWidth = value;
                Settings.Default.windowWidth = value;
                Settings.Default.Save();
                OnPropertyChanged();
            }
        }

        public List<RGXPatterns> RegexPatterns { get; set; } = RegexPatternsClass.GetValues();

        
        //ObservableCollection<Item> customSampleData = new ObservableCollection<Item>() // ## smazat?
        //{
        //    new Item { VideoDisplayName = "qwer" },
        //    new Item { VideoDisplayName = "asdf" },
        //    new Item { VideoDisplayName = "zxcv" },
        //    new Item { VideoDisplayName = "tyui" }
        //};

        //public ObservableCollection<Item> CustomSampleData
        //{
        //    get { return customSampleData; }
        //    set { customSampleData = value; }
        //}



        //public RelayCommand AddCommand => new RelayCommand(execute => AddItem(), canExecute => { return true; });
        public RelayCommand BrowseCommand => new RelayCommand(execute => BrowseNLoadFolder());
        //public RelayCommand ReloadFolder => new RelayCommand(execute => LoadFolderVideo(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand ReloadFolder => new RelayCommand(execute => PopulateDataGrid(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand RenameCommand => new RelayCommand(execute => StartRenaming(), canExecute => { return CheckRenamePrepared(); });
        public RelayCommand EscKeyCommand => new RelayCommand(execute => CloseApplication());
        public RelayCommand SearchContextMenuCommand => new RelayCommand(execute => AssociateWithVideoFilesRegistry());
        public RelayCommand SubtitlesSyncContextMenuCommand => new RelayCommand(execute => AssociateWithFolderRegistry());
        public RelayCommand RemoveContextMenuCommand => new RelayCommand(execute => RemoveContextMenus());
        public RelayCommand DownloadSelectedCommand => new RelayCommand(execute => DownloadSelected(), canExecute => { return DownloadCheckIfAvailable(); });
        //public RelayCommand OpenOptionsCommand => new RelayCommand(execute => OpenOptionsWindow());

        private IWindowService _windowService;
        //public ICommand OpenWindowCommand { get; set; }
        //public ICommand CloseWindowCommand { get; set; }
        public RelayCommand OpenOptionsWindowCommand => new RelayCommand(execute => OnOpenWindow());
        public RelayCommand CloseOptionsWindowCommand => new RelayCommand(execute => OnCloseWindow());

        private void OnOpenWindow()
        {
            _windowService.OpenWindow();
        }
        private void OnCloseWindow()
        {
            _windowService?.CloseWindow();
        }


        public MainWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            //OpenWindowCommand = new RelayCommand(param => OnOpenWindow());
            //CloseWindowCommand = new RelayCommand(param => OnCloseWindow());

            // https://stackoverflow.com/questions/11769113/how-to-start-wpf-based-on-arguments
            FolderPath = (Environment.GetCommandLineArgs().Length > 1) ? Environment.GetCommandLineArgs()[1] : Settings.Default.folderPath;
            SubtitlesToSearchFor = (Environment.GetCommandLineArgs().Length > 2) ? Environment.GetCommandLineArgs()[2] : String.Empty;
            //string SubtitlesToSearchFor = "D:\\Torrent\\House MD Season 1, 2, 3, 4, 5, 6, 7 & 8 + Extras DVDRip TSV\\Season 7\\House MD Season 7 Episode 20 - Changes.avi";

            //Items = new ObservableCollection<Item>();
            PopulateDataGrid();

            if (SubtitlesToSearchFor != String.Empty)
            {
                string folderOut;
                string fileOut;
                ParseFileNameAndFolder(SubtitlesToSearchFor, out folderOut, out fileOut);
                //MessageBox.Show($"Folder: #{folderOut}#\n\nFile: {fileOut}" );

                FolderPath = folderOut;
                OpenWebSearchForSubtitles(fileOut);
            }
        }
    }
}
