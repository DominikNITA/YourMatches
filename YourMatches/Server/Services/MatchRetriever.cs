using FootballDataApi;
using FootballDataApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Server.Services
{
    public class MatchRetriever
    {
        private HttpClient _httpClient;
        private readonly string API_TOKEN = "e95d43bc75a44b5b9ff0d7b2749ff52f" /*"816c6d64f7d84e3c82eb5af45172fa86"*/;
        public MatchRetriever(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", API_TOKEN);
        }

        public async Task<List<ScheduledMatchDto>> GetMatchesFromApi(RequestDto request)
        {
            List<ScheduledMatchDto> result = new List<ScheduledMatchDto>();
            //Convert the list of selected list to the string of filter
            string codes = GetCompetitionsCodes(request.LeaguesChecked);

            var matchController = MatchProvider.Create().With(_httpClient).Build();

            IEnumerable<Match> matches;
            if (!request.IsEndingDateSelected)
            {
                request.EndingDate = request.StartingDate;
            }

            matches = await matchController.GetAllMatches("competitions", codes, "dateFrom", request.StartingDate.ToString("yyyy-MM-dd"), "dateTo", request.EndingDate.ToString("yyyy-MM-dd"));

            foreach (var match in matches)
            {
                Enum.TryParse(match.Status.ToUpper(), out Status state);
                ScoreResult scoreResult = new ScoreResult(match.Score.FullTime.HomeTeam, match.Score.FullTime.AwayTeam);
                //Result winner = match.Score.Winner != null ? Enum.Parse<Result>(match.Score.Winner.ToUpper()) : Result.NONE;
                result.Add(new ScheduledMatchDto(
                    new TeamDto(match.HomeTeam.Name, match.HomeTeam.Name),
                    new TeamDto(match.AwayTeam.Name, match.AwayTeam.Name),
                    match.UtcDate,
                    state,
                    scoreResult,
                    (int)match.Matchday,
                    new LeagueDto(match.Competition.Name, match.Competition.Area.Name)));
            }
            //Filter matches with selected status
            result = result.Where(m => request.StatusChecked.Contains(m.Status)).ToList();
            return result;
        }

        private string GetCompetitionsCodes(List<League> leagues)
        {
            //TODO: Refactor with LINQ
            List<string> leaguesCodes = new List<string>();
            foreach (var league in leagues)
            {
                string code = ApiHelper.LeaguesCodes.First(x => x.Key == league).Value;
                if (code != null)
                {
                    leaguesCodes.Add(code);
                }
            }
            return string.Join(",", leaguesCodes);
        }
    }
}
