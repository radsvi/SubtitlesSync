using SubtitlesSync.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesSync.Model
{
    internal class DownloadFolderFiles : ViewModelBase
    {
        public string FileName { get; set; } // full name with path and suffix
        public string BaseName { get; set; } // name without suffix and without path
        bool EnabledToTransfer { get; set; }
    }
}
