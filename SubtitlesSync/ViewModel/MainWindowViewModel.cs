using Microsoft.Win32;
using SubtitlesSync.Model;
using SubtitlesSync.MVVM;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using SubtitlesSync.Properties;
using SubtitlesSync.Lib;
using System.Linq;

namespace SubtitlesSync.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string folderPath = Settings.Default.folderPath;
        public string[] SubtitleSuffixes { get; set; } = { "*.srt", "*.sub" };
        public string[] VideoSuffixes { get; set; } = { "*.avi", "*.mkv", "*.mp4", "*.mpg" };
        private List<string> folderContent;

        public List<string> FolderContent
        {
            get {
                
                
                
                return folderContent;
            }
            set {
                folderContent = value;
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

        //public RelayCommand AddCommand => new RelayCommand(execute => AddItem(), canExecute => { return true; });
        public RelayCommand BrowseCommand => new RelayCommand(execute => BrowseNLoadFolder());
        //public RelayCommand ReloadFolder => new RelayCommand(execute => LoadFolderVideo(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand ReloadFolder => new RelayCommand(execute => PopulateDataGrid(), canExecute => { return Directory.Exists(FolderPath); });
        public RelayCommand RenameCommand => new RelayCommand(execute => StartRenaming(), canExecute => { return Items.Count > 0; });
        public RelayCommand EscKeyCommand => new RelayCommand(execute => CloseApplication());

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Item>();
            LoadFolderVideo();
        }

        //private Item selectedItem;
        //public Item SelectedItem
        //{
        //    get { return selectedItem; }
        //    set
        //    {
        //        selectedItem = value;
        //        OnPropertyChanged(); // SelectedItem is automatically passed as parameter diky tomu [CallerMemberName]
        //    }
        //}

        private void BrowseNLoadFolder()
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            folderDialog.Title = "Select folder containing subtitles...";

            bool? success = folderDialog.ShowDialog();
            if (success == true)
            {
                string path = folderDialog.FolderName;
                FolderPath = path;

                PopulateDataGrid();
            }
        }
        private void PopulateDataGrid()
        {
            
            
            
            LoadFolderVideo();
        }
        private void LoadFolderVideo()
        {
            if (Directory.Exists(FolderPath))
            {
                List<string> fileEntries = new List<string>();
                foreach (string suffix in VideoSuffixes)
                {
                    fileEntries.AddRange(Directory.GetFiles(FolderPath, suffix));
                }

                Items.Clear();
                //ObservableCollection<Item> itemsTemp = new ObservableCollection<Item>();
                foreach (string fileEntry in fileEntries)
                {
                    //itemsTemp.Add(new Item { FileName = Path.GetFileName(fileEntry) });
                    Items.Add(new Item { FileName = Path.GetFileName(fileEntry) });
                    //itemsTemp.Add(Path.GetFileName(fileEntry));
                    //FolderContent.Add(Path.GetFileName(fileEntry));
                }

                //if (CheckWhetherFolderChanged(itemsTemp))
                //{ // folder changed
                //    MessageBox.Show("Folder content changed. Refreshing...");

                //}
                //else
                //{

                //}
            }
        }
        private void LoadFolderSubs()
        {
            if (Directory.Exists(FolderPath))
            {
                List<string> fileEntries = new List<string>();
                foreach (string suffix in SubtitleSuffixes)
                {
                    fileEntries.AddRange(Directory.GetFiles(FolderPath, suffix));
                }

                Items.Clear();
                //ObservableCollection<Item> itemsTemp = new ObservableCollection<Item>();
                foreach (string fileEntry in fileEntries)
                {
                    //itemsTemp.Add(new Item { FileName = Path.GetFileName(fileEntry) });
                    Items.Add(new Item { FileName = Path.GetFileName(fileEntry) });
                    //itemsTemp.Add(Path.GetFileName(fileEntry));
                    //FolderContent.Add(Path.GetFileName(fileEntry));
                }

                //if (CheckWhetherFolderChanged(itemsTemp))
                //{ // folder changed
                //    MessageBox.Show("Folder content changed. Refreshing...");

                //}
                //else
                //{

                //}
            }
        }
        private void StartRenaming()
        {
            // ## pridat nejakou kontrolu jestli se nezmenil obsah slozky. Chci to delat pro pripad kdy... je to vlastne dulezity? kdyz to neudelam co se zmeni?
            // no muze to teoreticky znova stahovat titulky, ale to se zas tak moc nestane.
            // nebo...?

            //if (CheckWhetherFolderChanged(itemsTemp))
            //{ // folder changed
            //    MessageBox.Show("Folder content changed. Refreshing...");

            //}
            //else
            //{

            //}
            MatchVideoAndSub.RenamingProcess();
        }
        private bool CheckWhetherFolderChanged(ObservableCollection<Item> itemsTemp)
        {
            // ## tohle jeste zkontrolovat, udelal jsem to trochu narychlo
            if (Items.Count > 0)
            {
                if (CompareLists(itemsTemp))
                {
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        internal bool CompareLists(ObservableCollection<Item> tempList)
        {
            // ## dodelat
            
            //foreach(Item item in Items)
            //{
            //    foreach (Item tempItem in tempList)
            //    {

            //    }
            //}

            // ## dodelat porovnavani Listu
            
            return true;
        }
        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

    }
}
