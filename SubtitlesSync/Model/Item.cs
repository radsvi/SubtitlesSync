using SubtitlesSync.MVVM;

namespace SubtitlesSync.Model
{
    internal class Item : ViewModelBase
    {
        [ObsoleteAttribute]public string FileName { get; set; }
        public string VideoFullFileName { get; set; }
        public string VideoFileName { get; set; }
        public string VideoDisplayName { get; set; }
        public string VideoSuffix { get; set; }
        public string SubtitlesFullFileName { get; set; }
        public string SubtitlesFileName { get; set; }
        public string SubtitlesDisplayName { get; set; }
        public string SubtitlesSuffix { get; set; }
        [ObsoleteAttribute] public string NewFileName { get; set; }
        //public string Status { get; set; }
        private string status;

        public string Status
        {
            get { return status; }
            set { 
                status = value;
                OnPropertyChanged();
            }
        }

    }
}
