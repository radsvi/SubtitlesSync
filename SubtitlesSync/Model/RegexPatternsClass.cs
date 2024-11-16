namespace SubtitlesSync.Model
{
    internal class RegexPatternsClass
    {
		//private List<RGXPatterns> regexPatternsVariable;

		//public List<RGXPatterns> RegexPatternsVariable
		//      {
		//	get { return regexPatternsVariable; }
		//	set { regexPatternsVariable = value; }
		//}

		public List<RGXPatterns> RegexPatternsVariable { get; set; } = new List<RGXPatterns>()
		{
            new RGXPatterns { // example: S01E01
                WholeTitle = @"S\s?\d{1,2}\s?E\s?\d{1,2}",
                SeasonLong = @"S\s?\d{1,2}",
                SeasonShort = @"S\s?",
                EpisodeLong = @"E\s?\d{1,2}",
                EpisodeShort = @"E\s?"
            },
            new RGXPatterns { // example: Season 6 Episode 01
                WholeTitle = @"Season\s?\d{1,2} Episode \d{1,2}",
                SeasonLong = @"Season\s?\d{1,2}",
                SeasonShort = @"Season\s?",
                EpisodeLong = @"Episode\s?\d{1,2}",
                EpisodeShort = @"Episode\s?"
            },
            new RGXPatterns { // example: 01x01
                WholeTitle = @"\d{1,2}\s?x\s?\d{1,2}",
                SeasonLong = @"\d{1,2}",
                SeasonShort = @"",
                EpisodeLong = @"x\d{1,2}",
                EpisodeShort = @"x"
            },
            new RGXPatterns { // example: .0101., or .101.
                WholeTitle = @"\.\d{3,4}\.",
                SeasonLong = @"S\s?\d{1,2}",
                SeasonShort = @"S\s?",
                EpisodeLong = @"E\s?\d{1,2}",
                EpisodeShort = @"E\s?"
            }
        };
    }
}
