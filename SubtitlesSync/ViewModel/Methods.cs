using Microsoft.Win32;
using SubtitlesSync.Model;
using SubtitlesSync.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace SubtitlesSync.ViewModel
{
    internal partial class MainWindowViewModel
    {

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
        private void GetFolderContent()
        {
            if (FolderBackupContent.Count() > 0)
            {
                FolderBackupContent.Clear();
            }

            if (Directory.Exists(FolderPath))
            {
                FolderBackupContent = new List<string>(Directory.GetFiles(FolderPath));
            }
        }
        //private void CheckAndUpdateFolderContentVar() // mozna by to melo byt spis neco jako IfChangedThenStop
        //{
        //    if (Directory.Exists(FolderPath))
        //    {
        //        List<string> folderContentNew = new List<string>(Directory.GetFiles(FolderPath));
        //        if (FolderBackupContent.Count() > 0)
        //        {
        //            if (folderContentNew.Count() > 0)
        //            {
        //                if (FolderBackupContent.SequenceEqual(folderContentNew) == false)
        //                {
        //                    FolderBackupContent = folderContentNew;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            FolderBackupContent = folderContentNew;
        //        }
        //    }
        //    else
        //    {
        //        FolderBackupContent.Clear();
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
            GetFolderContent();
            //CheckAndUpdateFolderContentVar(); // ## protoze je to funkce PopulateDataGrid da se predpokladat ze promenna FolderContent bude prazdna
            DetermineEpisode();
            MatchVideoAndSubtitles();


            // tohle predelat na funkci:
            //foreach (var item in FolderContent)
            //{
            //    Items.Add(new Item {
            //        //FileName = item.ShortName
            //        FileName = $"{item.ShortName} {item.Season} {item.Episode}"
            //    });
            //}


            //LoadFolderVideo();
        }
        private void DetermineEpisode()
        {
            foreach (FilesExtended fileItem in FolderContent)
            {
                bool successSeason = false;
                bool successEpisode = false;
                foreach (RGXPatterns currentPattern in RegexPatterns)
                {
                    successSeason = false;
                    successEpisode = false;

                    Regex seasonAndEpisodeRegex = new Regex(currentPattern.WholeTitle);
                    Match matchSeasonAndEpisode = seasonAndEpisodeRegex.Match(fileItem.ShortName);

                    Match matchSeason = (new Regex(currentPattern.SeasonLong)).Match(matchSeasonAndEpisode.Value);
                    string tempSeasonNumber = Regex.Replace(matchSeason.Value, currentPattern.SeasonShort, "", RegexOptions.IgnoreCase);
                    int seasonNumberInt;
                    if (Int32.TryParse(tempSeasonNumber, out seasonNumberInt))
                    {
                        fileItem.Season = seasonNumberInt;
                        successSeason = true;
                    }

                    Match matchEpisode = (new Regex(currentPattern.EpisodeLong)).Match(matchSeasonAndEpisode.Value);
                    string tempEpisodeNumber = Regex.Replace(matchEpisode.Value, currentPattern.EpisodeShort, "", RegexOptions.IgnoreCase);
                    int episodeNumberInt;
                    if (Int32.TryParse(tempEpisodeNumber, out episodeNumberInt))
                    {
                        fileItem.Episode = episodeNumberInt;
                        successEpisode = true;
                    }

                    if (successSeason == true && successEpisode == true)
                    {
                        fileItem.SuccessfullyFoundSeasonAndEpisode = true;
                        break;
                    }
                }
                if (successSeason == false || successEpisode == false)
                {
                    fileItem.SuccessfullyFoundSeasonAndEpisode = false;
                    //throw new Exception("spatnej regex match");
                }

            }
        }
        //private void RegexMatch()
        //{

        //}
        private void MatchVideoAndSubtitles()
        {
            //var videoFiles = Array.Exists(FolderContent, (FilesExtended item) => VideoSuffixes.Contains(item.Extension));
            List<FilesExtended> videoFiles = new List<FilesExtended>();
            List<FilesExtended> subtitleFiles = new List<FilesExtended>();
            foreach (FilesExtended fileItem in FolderContent)
            {
                if (fileItem.SuccessfullyFoundSeasonAndEpisode == true && VideoSuffixes.Contains(fileItem.Extension)) videoFiles.Add(fileItem);
                if (fileItem.SuccessfullyFoundSeasonAndEpisode == true && SubtitleSuffixes.Contains(fileItem.Extension)) subtitleFiles.Add(fileItem);
            }

            if (Items.Count > 0) Items.Clear();

            //foreach( var item in Environment.GetCommandLineArgs())
            //{
            //    Items.Add(new Item { VideoDisplayName = item });
            //}

            foreach (var videoItem in videoFiles)
            {
                //Item currentItem = new Item { VideoFileName = videoItem.ShortName };
                //Item currentItem = new Item {
                //    VideoFullFileName = videoItem.FileName,
                //    VideoFileName = videoItem.ShortName,
                //    VideoDisplayName = $"[S{videoItem.Season}E{videoItem.Episode}] {videoItem.ShortName}",
                //    VideoSuffix = videoItem.Extension
                //};
                Item currentItem = new Item { };
                currentItem.VideoFullFileName = videoItem.FileName;
                currentItem.VideoFileName = videoItem.ShortName;
                currentItem.VideoDisplayName = $"[S{videoItem.Season}E{videoItem.Episode}] {videoItem.ShortName}";
                currentItem.VideoSuffix = videoItem.Extension;
                
                foreach (var subItem in subtitleFiles)
                {
                    if (videoItem.Season == subItem.Season && videoItem.Episode == subItem.Episode)
                    {
                        currentItem.SubtitlesFileName = subItem.ShortName;
                        currentItem.SubtitlesFullFileName = subItem.FileName;
                        currentItem.SubtitlesDisplayName = $"[S{subItem.Season}E{subItem.Episode}] {subItem.ShortName}";
                        currentItem.SubtitlesSuffix = subItem.Extension;
                        currentItem.IsChecked = false;
                        currentItem.ReadyToRename = true;
                        currentItem.Status = "<ready>";
                    }
                }
                Items.Add(currentItem);
            }
        }
        private bool CheckRenamePrepared()
        {
            if (CheckWhetherFolderIsUnchanged() == false) return false;

            foreach(var item in Items)
            {
                if (item.ReadyToRename == true) return true;
            }

            return false;
        }

        private bool CheckWhetherFolderIsUnchanged()
        {
            if (Directory.Exists(FolderPath) == false) { return false; }

            if (Items.Count > 0)
            {
                if (FolderBackupContent.SequenceEqual(Directory.GetFiles(FolderPath)))
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
        private void StartRenaming()
        {
            //List<string> folderContentNew = new List<string>(Directory.GetFiles(FolderPath));
            if (CheckWhetherFolderIsUnchanged() == false)
            {
                MessageBox.Show("Content of the folder changed. Refresh the list before you continue!");
            }

            string backupFolder = "SubtitlesBackup";
            if (Directory.Exists(FolderPath + "\\SubtitlesBackup") == false) { Directory.CreateDirectory(FolderPath + "\\SubtitlesBackup"); }

            foreach(var item in Items)
            {
                if (String.IsNullOrEmpty(item.SubtitlesFileName) == false || String.IsNullOrEmpty(item.SubtitlesSuffix) == false)
                {
                    //File.Copy(Path.Combine(FolderPath, item.SubtitlesFileName), Path.Combine(FolderPath, backupFolder, item.SubtitlesFileName), true);
                    //MessageBox.Show(Path.Combine(FolderPath, backupFolder, item.SubtitlesFileName));
                    File.Copy(item.SubtitlesFullFileName, Path.Combine(FolderPath, backupFolder, item.SubtitlesFileName), true);

                    string newSubName = Regex.Replace(item.VideoFileName, item.VideoSuffix, "", RegexOptions.IgnoreCase);
                    string newSuffix = item.SubtitlesSuffix;
                    string newSubNamePlusPath = Path.Combine(FolderPath, newSubName + newSuffix);
                    try
                    {
                        File.Move(item.SubtitlesFullFileName, newSubNamePlusPath);
                        item.Status = "Backed-up & Renamed";
                    }
                    catch
                    {
                        MessageBox.Show("Error, file not renamed!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error );
                        item.Status = "Error";
                    }
                    //item.Status = newSubNamePlusPath;
                    //MessageBox.Show(newSubNamePlusPath);
                }
            }

            //MessageBox.Show("TEST");
        }
        
        private void CloseApplication()
        {
            // ## predelat na MVVM model: https://www.youtube.com/watch?v=U7Qclpe2joo
            System.Windows.Application.Current.Shutdown();
        }

        //private void WriteRegistryEntry()
        //{
        //    RegistryKey key = Registry.LocalMachine.OpenSubKey("Software", true);
        //    //RegistryKey key = Registry.ClassesRoot

        //    key.CreateSubKey("AppName");
        //    key = key.OpenSubKey("AppName", true);


        //    key.CreateSubKey("AppVersion");
        //    key = key.OpenSubKey("AppVersion", true);

        //    key.SetValue("yourkey", "yourvalue");
        //}

        private void AssociateWithVideoFilesRegistry()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to create context menu for video files to allow quick search for subtitles?", "Context menu association", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            string registryFolderPath = "SOFTWARE\\Classes";

            foreach (string suffix in VideoSuffixes)
            {
                string suffixKey = Registry.CurrentUser.OpenSubKey(Path.Combine(registryFolderPath, suffix)).GetValue(String.Empty).ToString();

                if (suffixKey == null) { break; }

                RegistryKey regClassesSubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes", true);
                RegistryKey regFileSuffixSubKey = regClassesSubKey.CreateSubKey(suffixKey);

                string command = "\"C:\\Users\\svihe\\Dropbox\\Coding\\C#\\SubtitlesSync\\SubtitlesSync\\bin\\Debug\\net8.0-windows\\SubtitlesSync.exe\" \"null\" \"%1\"";
                CreateSubtitlesSyncRegistry(regFileSuffixSubKey, "Search for subtitles", command, "%SystemRoot%\\System32\\shell32.dll,315");
            }
        }
        private void AssociateWithFolderRegistry()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to create context menu for folder to allow quick start of SubtitlesSync app?", "Context menu association", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            RegistryKey regDirectorySubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\Directory", true);

            string command = "\"C:\\Users\\svihe\\Dropbox\\Coding\\C#\\SubtitlesSync\\SubtitlesSync\\bin\\Debug\\net8.0-windows\\SubtitlesSync.exe\" \"%L\"";
            CreateSubtitlesSyncRegistry(regDirectorySubKey, "Sync subtitles with video...", command, "%SystemRoot%\\System32\\shell32.dll,115");
        }
        private void CreateSubtitlesSyncRegistry(RegistryKey directory, string displayName, string command, string icon)
        {
            RegistryKey regShellSubKey = directory.CreateSubKey("shell");
            RegistryKey regSubtitlesSyncSubKey = regShellSubKey.CreateSubKey("SubtitlesSync");
            regSubtitlesSyncSubKey.SetValue(String.Empty, displayName);
            regSubtitlesSyncSubKey.SetValue("icon", icon);

            RegistryKey regCommandSubKey = regSubtitlesSyncSubKey.CreateSubKey("Command");
            regCommandSubKey.SetValue(String.Empty, command);
        }
        private void RemoveContextMenus()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to remove all context menues associated with this app?", "Context menu deassociation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            // ## dodelat!
        }
        private void ParseFileNameAndFolder(string subtitlesString, out string folderPath, out string searchPattern)
        {
            string[] folderPathSplit = subtitlesString.Split("\\");
            folderPath = String.Empty;
            string fileNameWithSuffix = String.Empty;
            for (int i = 0; i < folderPathSplit.Length; i++)
            //foreach (string item in subtitlesString.Split("\\"))
            {
                if (i < folderPathSplit.Length - 1)
                {
                    folderPath = Path.Combine(folderPath, folderPathSplit[i]);
                }
                else
                {
                    fileNameWithSuffix = folderPathSplit[i];
                }
            }

            string fileNameWithoutSuffix = String.Empty;
            string[] fileNameSplit = fileNameWithSuffix.Split(".");
            for (int i = 0; i < fileNameSplit.Length - 1; i++)
            {
                fileNameWithoutSuffix += fileNameSplit[i];
            }
            searchPattern = fileNameWithoutSuffix.Replace(" ", "+");
        }
        private string ParseFileNameAndFolder(string subtitlesString)
        {
            string fileNameWithoutSuffix;
            ParseFileNameAndFolder(subtitlesString, out _, out fileNameWithoutSuffix);

            return fileNameWithoutSuffix;
        }
        private void OpenWebSearchForSubtitles(string fileName)
        {
            //MessageBox.Show(searchPattern);
            // ## predelat na defaultni browser - pouzit "Navigating event" https://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp
            string searchPattern = fileName.Replace(" ", "+");
            System.Diagnostics.Process.Start("C:\\Program Files\\Mozilla Firefox\\firefox.exe", $"https://www.opensubtitles.org/en/search2/sublanguageid-eng/moviename-{searchPattern}");
        }
        private void DownloadSelected()
        {
            foreach(var item in Items)
            {
                if (item.IsChecked)
                {
                    string searchPattern = ParseFileNameAndFolder(item.VideoFileName);
                    OpenWebSearchForSubtitles(searchPattern);
                }
            }
        }
    }
}
