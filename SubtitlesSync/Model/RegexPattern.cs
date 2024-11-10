using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitlesSync.Model
{
    internal class RGXPatterns
    {
        //public RGXPatterns() { }
        //public RGXPatterns(string wholeTitle, string seasonLong, string seasonShort, string episodeLong, string episodeShort)
        //{
        //    this.WholeTitle = wholeTitle;
        //    this.SeasonLong = seasonLong;
        //    this.SeasonShort = seasonShort;
        //    this.EpisodeLong = episodeLong;
        //    this.EpisodeShort = episodeShort;
        //}
        public string WholeTitle { get; set; }
        public string SeasonLong { get; set; }
        public string SeasonShort { get; set; }
        public string EpisodeLong { get; set; }
        public string EpisodeShort { get; set; }

    }
}
