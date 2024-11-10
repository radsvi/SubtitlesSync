namespace SubtitlesSync.Model
{
    internal class Item
    {
        [ObsoleteAttribute]public string FileName { get; set; }
        public string VideoFileName { get; set; }
        public string SubtitlesFileName { get; set; }
        [ObsoleteAttribute] public string NewFileName { get; set; }
        public string Status { get; set; }
    }
}
