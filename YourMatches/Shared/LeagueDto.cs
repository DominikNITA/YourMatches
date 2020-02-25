namespace YourMatches.Shared
{
    public class LeagueDto
    {
        public LeagueDto(string fullName, string country)
        {
            FullName = fullName;
            Country = country;
        }
        public LeagueDto()
        {

        }
        public string FullName { get; set; }
        public string Country { get; set; }
    }
}