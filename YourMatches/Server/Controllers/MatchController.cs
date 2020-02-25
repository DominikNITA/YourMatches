using YourMatches.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using FootballDataApi;
using Newtonsoft.Json;
using YourMatches.Server.Services;

namespace YourMatches.Server.Controllers
{
    [ApiController]
    [Route("/match")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> logger;
        private readonly HttpClient _http;
        private readonly ApiHelper _apiHelper;
        private readonly string API_TOKEN = "e95d43bc75a44b5b9ff0d7b2749ff52f" /*"816c6d64f7d84e3c82eb5af45172fa86"*/;

        public MatchController(ILogger<MatchController> logger, ApiHelper apiHelper)
        {
            this.logger = logger;
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("X-Auth-Token", API_TOKEN);
            _apiHelper = apiHelper;
        }

        [HttpPost]
        public List<ScheduledMatchDto> Get([FromBody]RequestDto request)
        {
            List<ScheduledMatchDto> result = new List<ScheduledMatchDto>();
            if (_apiHelper.CheckCallAvaibilty())
            {
                string codes = GetCompetitionsProperty(request.LeaguesChecked);

                var matchController = MatchProvider.Create().With(_http).Build();
                var matches = matchController.GetAllMatches("competitions", codes , "dateFrom", "2020-02-22", "dateTo", "2020-03-03");

                foreach (var match in matches.Result)
                {
                    Enum.TryParse(match.Status.ToUpper(), out Status state);
                    Result winner = match.Score.Winner != null ? Enum.Parse<Result>(match.Score.Winner.ToUpper()) : Result.NONE;
                    result.Add(new ScheduledMatchDto(
                        new TeamDto(match.HomeTeam.Name, match.HomeTeam.Name),
                        new TeamDto(match.AwayTeam.Name, match.AwayTeam.Name),
                        match.UtcDate,
                        state,
                        winner,
                        (int)match.Matchday,
                        new LeagueDto(match.Competition.Name, match.Competition.Area.Name)));
                }
                return result;
            }
            else
            {
                //TODO: Send error of too many requests
                //result.Add(new ScheduledMatchDto(new TeamDto("aa", "aa", null), new TeamDto("aa", "aa", null), DateTime.Now));
                return null/*result.ToArray()*/;
            }
        }

        private string GetCompetitionsProperty(List<League> leagues)
        {
            //Refactor with LINQ
            List<string> leaguesCodes = new List<string>();
            foreach (var league in leagues)
            {
                string code = ApiHelper.LeaguesCodes.First(x => x.Key == league).Value;
                if(code != null)
                {
                    leaguesCodes.Add(code);
                }
            }
            return string.Join(",",leaguesCodes);
        }
        //[HttpGet]
        //public string GetText()
        //{
        //    var competitionController = CompetitionProvider.Create().With(_http).Build();

        //    var competitions = competitionController.GetAvailableCompetition();
        //    return competitions.Result.Select(s => (s.Id, s.Name)).Aggregate("", (current, next) => current + ", " + next);
        //}
    }
}
