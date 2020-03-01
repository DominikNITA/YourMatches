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
using FootballDataApi.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace YourMatches.Server.Controllers
{
    [ApiController]
    [Route("/match")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> logger;
        private readonly ApiHelper _apiHelper;
        private readonly MatchRetriever _matchRetriever;

        public MatchController(ILogger<MatchController> logger, ApiHelper apiHelper, MatchRetriever matchRetriever)
        {
            this.logger = logger;
            _apiHelper = apiHelper;
            _matchRetriever = matchRetriever;
        }

        [HttpPost]
        public List<ScheduledMatchDto> Get([FromBody]RequestDto request)
        {
            List<ScheduledMatchDto> result = new List<ScheduledMatchDto>();
            if (_apiHelper.CheckCallAvaibilty())
            {
                return _matchRetriever.GetMatchesFromApi(request);
            }
            else
            {
                //TODO: Send error of too many requests
                //result.Add(new ScheduledMatchDto(new TeamDto("aa", "aa", null), new TeamDto("aa", "aa", null), DateTime.Now));
                return null/*result.ToArray()*/;
            }
        }



        //private string ConvertDateTimeToFilterFormat(DateTime dateToConvert)
        //{
        //    return dateToConvert.ToShortDateString
        //}
        //[HttpGet]
        //public string GetText()
        //{
        //    var competitionController = CompetitionProvider.Create().With(_http).Build();

        //    var competitions = competitionController.GetAvailableCompetition();
        //    return competitions.Result.Select(s => (s.Id, s.Name)).Aggregate("", (current, next) => current + ", " + next);
        //}
    }
}
