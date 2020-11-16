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
        private readonly ApiCallLimiter _apiCallLimiter;
        private readonly IMatchRetriever _matchRetriever;

        public MatchController(ILogger<MatchController> logger, ApiCallLimiter apiHelper, IMatchRetriever matchRetriever)
        {
            this.logger = logger;
            _apiCallLimiter = apiHelper;
            _matchRetriever = matchRetriever;
        }

        [HttpPost]
        public async Task<ActionResult<List<ScheduledMatchDto>>> Get([FromBody]RequestDto request)
        {
            Console.WriteLine(request.StatusChecked.Count);
            Console.WriteLine(request.StartingDate.ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_apiCallLimiter.CheckCallAvailability())
            {
                return await _matchRetriever.GetMatches(request);
            }
            else
            {
                logger.LogWarning("Limit of requests reached.");
                throw new Exception("Too many requests!");
            }
        }
    }
}
