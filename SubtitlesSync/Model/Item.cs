using SubtitlesSync.MVVM;

namespace SubtitlesSync.Model
{
    internal class Item : ViewModelBase
    {
        [ObsoleteAttribute]public string FileName { get; set; }
        public string VideoFullFileName { get; set; }
        public string VideoFileName { get; set; }
        public string VideoBaseName { get; set; }
        public string VideoDisplayName { get; set; }
        public string VideoSuffix { get; set; }
        public string SubtitlesFullFileName { get; set; }
        public string SubtitlesFileName { get; set; }
        public string SubtitlesBaseName { get; set; }
        public string SubtitlesDisplayName { get; set; }
        public string SubtitlesSuffix { get; set; }
        //public bool Checked { get; set; } = true;
        private bool isChecked = true;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }
        public bool ReadyToRename { get; set; } = false;

        [ObsoleteAttribute] public string NewFileName { get; set; }
        //public string Status { get; set; }
        [ObsoleteAttribute] private string status;
        [ObsoleteAttribute] public string Status
        {
            get { return status; }
            set { 
                status = value;
                OnPropertyChanged();
            }
        }
        public enum _InternalStatus { empty, notready, ready, renamed, matches, error };
        private _InternalStatus internalStatus = _InternalStatus.empty;
        public _InternalStatus InternalStatus
        {
            get { return internalStatus; }
            set { 
                internalStatus = value;
                switch (value)
                {
                    case _InternalStatus.notready:
                        DisplayStatus = "Not ready";
                        break;
                    case _InternalStatus.ready:
                        DisplayStatus = "<Ready>";
                        break;
                    case _InternalStatus.renamed:
                        DisplayStatus = "Backed-up & Renamed";
                        break;
                    case _InternalStatus.matches:
                        DisplayStatus = "Already matching";
                        break;
                    case _InternalStatus.error:
                        DisplayStatus = "Error";
                        break;
                    default:
                        DisplayStatus = "N/A";
                        break;
                }
            }
        }


        private string displayStatus;
        public string DisplayStatus
        {
            get { return displayStatus; }
            private set {
                displayStatus = value;
                OnPropertyChanged();
            }
        }

    }
}
