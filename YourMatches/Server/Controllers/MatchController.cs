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
        private readonly string API_TOKEN = "e95d43bc75a44b5b9ff0d7b2749ff52f";
        string[] codes = { "SA", "FL1", "BL1", "PL", "PD" };

        public MatchController(ILogger<MatchController> logger, ApiHelper apiHelper)
        {
            this.logger = logger;
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("X-Auth-Token", API_TOKEN);
            _apiHelper = apiHelper;
        }

        [HttpGet]
        public ScheduledMatchDto[] Get()
        {
            List<ScheduledMatchDto> result = new List<ScheduledMatchDto>();
            if (_apiHelper.CheckCallAvaibilty())
            {
                var matchController = MatchProvider.Create().With(_http).Build();
                var matches = matchController.GetAllMatches("competitions", "PL,BL1,PD,SA,FL1", "status", "SCHEDULED", "dateFrom", "2020-02-15", "dateTo", "2020-02-16");

                foreach (var match in matches.Result)
                {
                    Enum.TryParse(match.Status.ToUpper(), out Status status);
                    Result winner = match.Score.Winner != null ? Enum.Parse<Result>(match.Score.Winner.ToUpper()) : Result.NONE;
                    result.Add(new ScheduledMatchDto(
                        new TeamDto(match.HomeTeam.Name, match.HomeTeam.Name, new LeagueDto(match.Competition.Name, match.Competition.Name, match.Competition.Area.Name)),
                        new TeamDto(match.AwayTeam.Name, match.AwayTeam.Name, new LeagueDto(match.Competition.Name, match.Competition.Name, match.Competition.Area.Name)),
                        match.UtcDate,
                        status,
                        winner));
                }
                return result.ToArray();
            }
            else
            {
                //TODO: Send error of too many requests
                //result.Add(new ScheduledMatchDto(new TeamDto("aa", "aa", null), new TeamDto("aa", "aa", null), DateTime.Now));
                return null/*result.ToArray()*/;
            }
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
