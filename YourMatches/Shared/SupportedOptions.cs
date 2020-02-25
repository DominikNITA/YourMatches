using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public static class SupportedOptions
    {
        public static readonly Dictionary<League, string> LeaguesNames = new Dictionary<League, string>()
        {
            { League.ENGLAND_1, "Premier League" },
            { League.SPAIN_1, "Primera Division" },
            { League.FRANCE_1, "Ligue 1" },
            { League.GERMANY_1, "Bundesliga" },
            { League.ITALY_1, "Serie A" }
        };

        public static Dictionary<Status, string> StatusNames = new Dictionary<Status, string>()
        {
            { Status.SCHEDULED, "Scheduled" },
            { Status.FINISHED, "Finished" },
            { Status.IN_PLAY, "Live"}
        };
    }
}
