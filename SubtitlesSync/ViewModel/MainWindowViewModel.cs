using Microsoft.Win32;
using SubtitlesSync.Lib;
using SubtitlesSync.Model;
using SubtitlesSync.MVVM;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Json;
using System.Windows;

namespace SubtitlesSync.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string folderPath = "d:\\Torrent\\House MD Season 1, 2, 3, 4, 5, 6, 7 & 8 + Extras DVDRip TSV\\Season 6\\"; // ## odstranit default value, dat tam loadovani z json filu misto toho

        public string FolderPath
        {
            get { return folderPath; }
            set { 
                folderPath = value;
                OnPropertyChanged();
            }
        }
        //private Settings persistentSettings = JSON.ImportFromFile();
        ////private List<Settings> persistentSettings;

        //public Settings PersistentSettings
        //{
        //    get { return persistentSettings; }
        //    set { 
        //        persistentSettings = value;
        //        //JSON.ExportToFile(persistentSettings);
        //        OnPropertyChanged();
        //    }
        //}

        public string[] SubtitleSuffixes { get; set; } = { "*.srt", "*.sub" };


        public ObservableCollection<Item> Items { get; set; }

        //public RelayCommand AddCommand => new RelayCommand(execute => AddItem(), canExecute => { return true; });
        public RelayCommand BrowseCommand => new RelayCommand(execute => BrowseNLoadFolder());
        public RelayCommand ReloadFolder => new RelayCommand(execute => LoadFolder(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand RenameCommand => new RelayCommand(execute => RenameSubtitles(), canExecute => { return Items.Count > 0; });
        
        //public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
        //public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
        //public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Item>();
            LoadFolder();
        }

        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(); // SelectedItem is automatically passed as parameter diky tomu [CallerMemberName]
            }
        }

        private void BrowseNLoadFolder()
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            folderDialog.Title = "Select folder containing subtitles...";

            bool? success = folderDialog.ShowDialog();
            if (success == true)
            {
                string path = folderDialog.FolderName;
                FolderPath = path;

                LoadFolder();
            }
        }
        private void LoadFolder()
        {
            if (Directory.Exists(FolderPath))
            {
                //string[] fileEntries = Directory.GetFiles(FolderPath, "*.srt"); // ## doplnit jeste *.sub. Mozna proste poslat tenhle prikaz jeste jednou a vysledky secist
                List<string> fileEntries = new List<string>();
                foreach (string suffix in SubtitleSuffixes)
                {
                    fileEntries.AddRange(Directory.GetFiles(FolderPath, suffix));
                }

                Items.Clear();
                foreach (string fileEntry in fileEntries)
                {
                    Items.Add(new Item { FileName = Path.GetFileName(fileEntry) });
                }
            }
        }
        private void RenameSubtitles()
        {
            // ## testovaci!

            //List<Settings> jsonContent = JSON.ImportFromFile();
            //string message = "Message: " + jsonContent[0].FolderPath;
            //MessageBox.Show(message);

            //JSON.ExportToFile(PersistentSettings);
        }
    }
}
