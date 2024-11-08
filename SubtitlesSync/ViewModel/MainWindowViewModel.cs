using Microsoft.Win32;
using SubtitlesSync.Model;
using SubtitlesSync.MVVM;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using SubtitlesSync.Properties;

namespace SubtitlesSync.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string folderPath = Settings.Default.folderPath;

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

        public string[] SubtitleSuffixes { get; set; } = { "*.srt", "*.sub" };


        public ObservableCollection<Item> Items { get; set; }

        //public RelayCommand AddCommand => new RelayCommand(execute => AddItem(), canExecute => { return true; });
        public RelayCommand BrowseCommand => new RelayCommand(execute => BrowseNLoadFolder());
        public RelayCommand ReloadFolder => new RelayCommand(execute => LoadFolder(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand RenameCommand => new RelayCommand(execute => RenameSubtitles(), canExecute => { return Items.Count > 0; });
        public RelayCommand EscKeyCommand => new RelayCommand(execute => CloseApplication());

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
        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
