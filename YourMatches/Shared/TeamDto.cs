using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class TeamDto
    {
        public TeamDto(string teamName, string shortcutName, LeagueDto league)
        {
            TeamName = teamName;
            ShortcutName = shortcutName;
            League = league;
        }

        public TeamDto()
        {

        }
        public string TeamName { get; set; }
        public string ShortcutName { get; set; }
        public LeagueDto League { get; set; }
    }
}
