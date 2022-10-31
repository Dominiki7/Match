using System;
using System.Collections.Generic;

namespace MatchManagement.Models
{
    public partial class Match
    {
        public Match()
        {
            MatchOdds = new HashSet<MatchOdds>();
        }

        public int MatchId { get; set; }
        public string MatchDescription { get; set; }
        public DateTime MatchDate { get; set; }
        public TimeSpan MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public string Sport { get; set; }

        public virtual ICollection<MatchOdds> MatchOdds { get; set; }
    }
}
