﻿using YourMatches.Shared;
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
        public async Task<ActionResult<List<ScheduledMatchDto>>> Get([FromBody]RequestDto request)
        {
            if (_apiHelper.CheckCallAvaibilty())
            {
                return await _matchRetriever.GetMatchesFromApi(request);
            }
            else
            {
                logger.LogWarning("Limit of requests reached.");
                throw new Exception("Too many requests!");
            }
        }
    }
}
