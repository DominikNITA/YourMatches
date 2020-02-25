using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class TeamDto
    {
        public TeamDto(string teamName, string shortcutName)
        {
            TeamName = teamName;
            ShortcutName = shortcutName;
        }

        public TeamDto()
        {

        }
        public string TeamName { get; set; }
        public string ShortcutName { get; set; }
    }
}
