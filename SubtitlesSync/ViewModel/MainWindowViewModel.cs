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

namespace SubtitlesSync.ViewModel
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private string folderPath = Settings.Default.folderPath;
        public string[] VideoSuffixes { get; set; } = { "*.avi", "*.mkv", "*.mp4", "*.mpg" };
        public string[] SubtitleSuffixes { get; set; } = { "*.srt", "*.sub" };

        //public List<FilesExtended> VideoFiles { get; set; }
        //public List<FilesExtended> SubtitleFiles { get; set; }

        private List<FilesExtended> folderContent = new List<FilesExtended>();
        public List<FilesExtended> FolderContent
        {
            get { return folderContent; }
            set { folderContent = value; }
        }

        private List<string> folderBackupContent = new List<string>();
        public List<string> FolderBackupContent
        {
            get { return folderBackupContent; }
            set
            {
                folderBackupContent = value;

                if (FolderContent.Count() > 0) FolderContent.Clear();
                foreach (string fileName in value)
                {
                    FolderContent.Add(new FilesExtended { FileName = fileName, ShortName = Path.GetFileName(fileName), Extension = Path.GetExtension(fileName) });
                }
            }
        }

        public ObservableCollection<Item> Items { get; set; }

        public string FolderPath
        {
            get { return folderPath; }
            set {
                folderPath = value;
                Settings.Default.folderPath = value;
                Settings.Default.Save();
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

        public List<RGXPatterns> RegexPatterns { get; set; }



        //public RelayCommand AddCommand => new RelayCommand(execute => AddItem(), canExecute => { return true; });
        public RelayCommand BrowseCommand => new RelayCommand(execute => BrowseNLoadFolder());
        //public RelayCommand ReloadFolder => new RelayCommand(execute => LoadFolderVideo(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand ReloadFolder => new RelayCommand(execute => PopulateDataGrid(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand RenameCommand => new RelayCommand(execute => StartRenaming(), canExecute => { return CheckWhetherFolderChanged(); });
        public RelayCommand EscKeyCommand => new RelayCommand(execute => CloseApplication());

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Item>();
            RegexPatterns = new List<RGXPatterns>();
            RegexPatterns.Add(new RGXPatterns
            { // example: S01E01
                WholeTitle = @"S\s?\d{1,2}\s?E\s?\d{1,2}",
                SeasonLong = @"S\s?\d{1,2}",
                SeasonShort = @"S\s?",
                EpisodeLong = @"E\s?\d{1,2}",
                EpisodeShort = @"E\s?"
            });
            RegexPatterns.Add(new RGXPatterns
            { // example: Season 6 Episode 01
                WholeTitle = @"Season\s?\d{1,2} Episode \d{1,2}",
                SeasonLong = @"Season\s?\d{1,2}",
                SeasonShort = @"Season\s?",
                EpisodeLong = @"Episode\s?\d{1,2}",
                EpisodeShort = @"Episode\s?"
            });
            RegexPatterns.Add(new RGXPatterns
            { // example: 01x01
                WholeTitle = @"\d{1,2}\s?x\s?\d{1,2}",
                SeasonLong = @"\d{1,2}",
                SeasonShort = @"",
                EpisodeLong = @"x\d{1,2}",
                EpisodeShort = @"x"
            });
            RegexPatterns.Add(new RGXPatterns
            { // example: .0101., or .101.
                WholeTitle = @"\.\d{3,4}\.",
                SeasonLong = @"S\s?\d{1,2}",
                SeasonShort = @"S\s?",
                EpisodeLong = @"E\s?\d{1,2}",
                EpisodeShort = @"E\s?"
            });
            PopulateDataGrid();
        }
        

    }
}
