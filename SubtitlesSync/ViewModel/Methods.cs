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
        private void CheckAndUpdateFolderContentVar() // mozna by to melo byt spis neco jako IfChangedThenStop
        {
            if (Directory.Exists(FolderPath))
            {
                List<string> folderContentNew = new List<string>(Directory.GetFiles(FolderPath));
                if (FolderBackupContent.Count() > 0)
                {
                    if (folderContentNew.Count() > 0)
                    {
                        if (FolderBackupContent.SequenceEqual(folderContentNew) == false)
                        {
                            FolderBackupContent = folderContentNew;
                        }
                    }
                }
                else
                {
                    FolderBackupContent = folderContentNew;
                }
            }
            else
            {
                FolderBackupContent.Clear();
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
                        break;
                    }
                }
                if (successSeason == false || successEpisode == false)
                {
                    throw new Exception("spatnej regex match");
                }

            }
        }
        private void RegexMatch()
        {

        }
        //private bool RegexMatch(FilesExtended fileItem)
        //{
        //    var currentPattern = RegexPatterns[0];
        //    bool success = true;
        //    // ($item.Name -match "Season \d{1,2} Episode \d{1,2}")
        //    //var asdf = Regex.Replace();
        //    //FilesExtended currentFile = new FilesExtended();

        //    Regex seasonAndEpisodeRegex = new Regex(currentPattern.WholeTitle);
        //    Match matchSeasonAndEpisode = seasonAndEpisodeRegex.Match(fileItem.ShortName);

        //    Match matchSeason = (new Regex(currentPattern.SeasonLong)).Match(matchSeasonAndEpisode.Value);
        //    string tempSeasonNumber = Regex.Replace(matchSeason.Value, currentPattern.SeasonShort, "", RegexOptions.IgnoreCase);
        //    int seasonNumberInt;
        //    if (Int32.TryParse(tempSeasonNumber, out seasonNumberInt))
        //    {
        //        fileItem.Season = seasonNumberInt;
        //    }
        //    else
        //    {
        //        //throw new Exception("spatnej regex match");
        //        success = false;
        //    }

        //    Match matchEpisode = (new Regex(currentPattern.EpisodeLong)).Match(matchSeasonAndEpisode.Value);
        //    string tempEpisodeNumber = Regex.Replace(matchEpisode.Value, currentPattern.EpisodeShort, "", RegexOptions.IgnoreCase);
        //    int episodeNumberInt;
        //    if (Int32.TryParse(tempEpisodeNumber, out episodeNumberInt))
        //    {
        //        fileItem.Episode = episodeNumberInt;
        //    }
        //    else
        //    {
        //        //throw new Exception("spatnej regex match");
        //        success = false;
        //    }
        //    return success;
        //}
        //private FilesExtended RegexMatch(FilesExtended fileItem)
        //{
        //    var currentPattern = RegexPatterns[0];

        //    // ($item.Name -match "Season \d{1,2} Episode \d{1,2}")
        //    //var asdf = Regex.Replace();
        //    FilesExtended currentFile = new FilesExtended();

        //    Regex seasonAndEpisodeRegex = new Regex(currentPattern.WholeTitle);
        //    Match matchSeasonAndEpisode = seasonAndEpisodeRegex.Match(fileItem.ShortName);

        //    Match matchSeason = (new Regex(currentPattern.SeasonLong)).Match(matchSeasonAndEpisode.Value);
        //    string tempSeasonNumber = Regex.Replace(matchSeason.Value, currentPattern.SeasonShort, "", RegexOptions.IgnoreCase);
        //    int seasonNumberInt;
        //    if (Int32.TryParse(tempSeasonNumber, out seasonNumberInt))
        //    {
        //        currentFile.Season = seasonNumberInt;
        //    }
        //    else
        //    {
        //        throw new Exception("spatnej regex match");
        //    }

        //    Match matchEpisode = (new Regex(currentPattern.EpisodeLong)).Match(matchSeasonAndEpisode.Value);
        //    string tempEpisodeNumber = Regex.Replace(matchEpisode.Value, currentPattern.EpisodeShort, "", RegexOptions.IgnoreCase);
        //    int episodeNumberInt;
        //    if (Int32.TryParse(tempEpisodeNumber, out episodeNumberInt))
        //    {
        //        currentFile.Episode = episodeNumberInt;
        //    }
        //    else
        //    {
        //        throw new Exception("spatnej regex match");
        //    }


        //    return currentFile;
        //}
        private void MatchVideoAndSubtitles()
        {
            //var videoFiles = Array.Exists(FolderContent, (FilesExtended item) => VideoSuffixes.Contains(item.Extension));
            List<FilesExtended> videoFiles = new List<FilesExtended>();
            List<FilesExtended> subtitleFiles = new List<FilesExtended>();
            foreach (FilesExtended fileItem in FolderContent)
            {
                if (VideoSuffixes.Contains("*" + fileItem.Extension)) videoFiles.Add(fileItem);
                if (SubtitleSuffixes.Contains("*" + fileItem.Extension)) subtitleFiles.Add(fileItem);
            }

            if (Items.Count > 0) Items.Clear();

            foreach (var videoItem in videoFiles)
            {
                //Item currentItem = new Item { VideoFileName = videoItem.ShortName };
                Item currentItem = new Item { VideoFileName = $"{videoItem.ShortName} [S{videoItem.Season}E{videoItem.Episode}]" };
                foreach (var subItem in subtitleFiles)
                {
                    if (videoItem.Season == subItem.Season && videoItem.Episode == subItem.Episode)
                    {
                        //currentItem.SubtitlesFileName = subItem.ShortName;
                        currentItem.SubtitlesFileName = $"{videoItem.ShortName} [S{videoItem.Season}E{videoItem.Episode}]";
                        currentItem.Status = "ready";
                    }
                }
                Items.Add(currentItem);
            }
        }

        private void StartRenaming()
        {
            //List<string> folderContentNew = new List<string>(Directory.GetFiles(FolderPath));
            CheckWhetherFolderChanged();
            MessageBox.Show("TEST");
        }
        private bool CheckWhetherFolderChanged()
        {
            // ## tohle jeste zkontrolovat, udelal jsem to trochu narychlo
            if (Items.Count > 0)
            {
                if (folderBackupContent.SequenceEqual(Directory.GetFiles(FolderPath)))
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
        //internal bool CompareLists(List<string> tempList)
        //{
        //    // ## dodelat

        //    //foreach(Item item in Items)
        //    //{
        //    //    foreach (Item tempItem in tempList)
        //    //    {

        //    //    }
        //    //}

        //    // ## dodelat porovnavani Listu

            

        //    return folderBackupContent.SequenceEqual(Directory.GetFiles(FolderPath));
        //}
        private void CloseApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
