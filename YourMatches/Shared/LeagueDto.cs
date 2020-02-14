namespace YourMatches.Shared
{
    public class LeagueDto
    {
        public LeagueDto(string fullName, string shortcutName, string country)
        {
            FullName = fullName;
            ShortcutName = shortcutName;
            Country = country;
        }
        public LeagueDto()
        {

        }
        public string FullName { get; set; }
        public string ShortcutName { get; set; }
        public string Country { get; set; }
    }
}