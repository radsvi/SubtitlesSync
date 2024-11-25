namespace SubtitlesSync.Model
{
    internal class FilesExtended
    {
        public string FileName { get; set; } // full name with path and suffix
        public string BaseName { get; set; } // name without suffix and without path
        public string ShortName { get; set; } // name with suffix, but without path
        public string Extension { get; set; } // suffix in format ".srt"
        public int Season { get; set; }
        public int Episode { get; set; }
        public bool SuccessfullyFoundSeasonAndEpisode { get; set; }

    }
}
