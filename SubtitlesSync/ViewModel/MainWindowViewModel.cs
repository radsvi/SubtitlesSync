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
using System.Windows.Shapes;
//using SubtitlesSync.View.UserControls;

namespace SubtitlesSync.ViewModel
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private string folderPath = Settings.Default.folderPath;
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
                    OnPropertyChanged();
                    ToggleVideoFilesRegistry();
                    Settings.Default.subtitlesDownloadSource = value;
                    Settings.Default.Save();
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

        private bool searchContextMenuChecked = Settings.Default.searchContextMenuChecked;
        public bool SearchContextMenuChecked
        {
            get { return searchContextMenuChecked; }
            set {
                searchContextMenuChecked = value;
                Settings.Default.searchContextMenuChecked = value;
                Settings.Default.Save();
            }
        }

        private bool syncContextMenuChecked = Settings.Default.syncContextMenuChecked;
        public bool SyncContextMenuChecked
        {
            get { return syncContextMenuChecked; }
            set {
                syncContextMenuChecked = value;
                Settings.Default.syncContextMenuChecked = value;
                Settings.Default.Save();
            }
        }

        private bool showWindowEvenFromContextMenu = Settings.Default.showWindowEvenFromContextMenu;

        public bool ShowWindowEvenFromContextMenu
        {
            get { return showWindowEvenFromContextMenu; }
            set {
                showWindowEvenFromContextMenu = value;
                Settings.Default.showWindowEvenFromContextMenu = value;
                Settings.Default.Save();
            }
        }

        //public bool AlreadyOpenedURL { get; set; } = false;





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

        #region OptionsWindow properties
        private string downloadPath = Settings.Default.downloadPath;
        public string DownloadPath
        {
            get { return downloadPath; }
            set
            {
                downloadPath = value;
                OnPropertyChanged();
                Settings.Default.downloadPath = value;
                Settings.Default.Save();
            }
        }

        private int fileNewerThanHours = Settings.Default.fileNewerThanHours;
        public int FileNewerThanHours
        {
            get { return fileNewerThanHours; }
            set
            {
                fileNewerThanHours = value;
                OnPropertyChanged();
                Settings.Default.fileNewerThanHours = value;
                Settings.Default.Save();
            }
        }

        private int optionsWindowHeight = Settings.Default.optionsWindowHeight;
        public int OptionsWindowHeight
        {
            get { return optionsWindowHeight; }
            set
            {
                optionsWindowHeight = value;
                Settings.Default.optionsWindowHeight = value;
                Settings.Default.Save();
                OnPropertyChanged();
            }
        }
        private int optionsWindowWidth = Settings.Default.optionsWindowWidth;
        public int OptionsWindowWidth
        {
            get { return optionsWindowWidth; }
            set
            {
                optionsWindowWidth = value;
                Settings.Default.optionsWindowWidth = value;
                Settings.Default.Save();
                OnPropertyChanged();
            }
        }


        private ObservableCollection<DownloadFolderFiles> downloadedFiles = new ObservableCollection<DownloadFolderFiles>();
        public ObservableCollection<DownloadFolderFiles> DownloadedFiles
        {
            get
            {
                return downloadedFiles;
            }
            set
            {
                downloadedFiles = value;
                OnPropertyChanged();
            }
        }

        //public string[] packageTypes { get; set; } = { ".zip", ".rar", ".7z" }; // ## otestovat ostatni pripony
        public string[] packageTypes { get; set; } = { ".zip" };
        #endregion

        public MainWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            string firstArg = (Environment.GetCommandLineArgs().Length > 1) ? Environment.GetCommandLineArgs()[1] : String.Empty;
            if (String.IsNullOrEmpty(firstArg) == false && firstArg.Equals("null") == false)
            {
                FolderPath = firstArg;
            }
            SubtitlesToSearchFor = (Environment.GetCommandLineArgs().Length > 2) ? Environment.GetCommandLineArgs()[2] : String.Empty;

            PopulateDataGrid();

            if (SubtitlesToSearchFor != String.Empty && String.IsNullOrEmpty(SubtitlesToSearchFor) == false)
            {
                string folderOut;
                string fileOut;
                ParseFileNameAndFolder(SubtitlesToSearchFor, out folderOut, out fileOut);

                FolderPath = folderOut;
                OpenWebSearchForSubtitles(fileOut);

                if (ShowWindowEvenFromContextMenu == false)
                {
                    CloseApplication();
                }
            }
        }
    }
}
