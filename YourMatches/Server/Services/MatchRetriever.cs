using FootballDataApi;
using FootballDataApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Server.Services
{
    public class MatchRetriever
    {
        private HttpClient _httpClient;
        private readonly string API_TOKEN = "e95d43bc75a44b5b9ff0d7b2749ff52f" /*"816c6d64f7d84e3c82eb5af45172fa86"*/;

        public static readonly Dictionary<League, string> LeaguesCodesDict = new Dictionary<League, string>()
        {
            { League.ENGLAND_1, "PL" },
            { League.SPAIN_1, "PD" },
            { League.FRANCE_1, "FL1" },
            { League.GERMANY_1, "BL1" },
            { League.ITALY_1, "SA" }
        };
        public MatchRetriever(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", API_TOKEN);
        }

        public async Task<List<ScheduledMatchDto>> GetMatchesFromApi(RequestDto request)
        {
            //TODO: Refactor

            //Convert the list of selected list to the string of filter
            var codes = GetCompetitionsCodes(request.LeaguesChecked);

            var matchController = MatchProvider.Create().With(_httpClient).Build();

            var matches = await matchController.GetAllMatches("competitions", codes, "dateFrom", request.StartingDate.ToString("yyyy-MM-dd"), "dateTo", request.EndingDate.ToString("yyyy-MM-dd"));

            var result = new List<ScheduledMatchDto>();
            foreach (var match in matches)
            {
                Enum.TryParse(match.Status.ToUpper(), out Status state);
                ScoreResult scoreResult = new ScoreResult(match.Score.FullTime.HomeTeam, match.Score.FullTime.AwayTeam);

                result.Add(new ScheduledMatchDto(
                    match.HomeTeam.Name,
                    match.AwayTeam.Name,
                    match.UtcDate,
                    state,
                    scoreResult,
                    (int)match.Matchday,
                    new LeagueDto(match.Competition.Name, match.Competition.Area.Name)
                    ));
            }
            //Filter matches with selected status
            result = result.Where(m => request.StatusChecked.Contains(m.Status)).ToList();
            return result;
        }

        private string GetCompetitionsCodes(List<League> leagues)
        {
            List<string> leaguesCodes = LeaguesCodesDict.Where(x => leagues.Contains(x.Key)).Select(x => x.Value).ToList(); ;
            return string.Join(",", leaguesCodes);
        }
    }
}
