using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YourMatches.Server.Services;
using YourMatches.Server.Models;
using YourMatches.Shared;
using YourMatches.Server.Services.IWebScraper;

namespace YourMatches.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoController : ControllerBase
    {
        private readonly ILogger<LogoController> logger;
        private LogoContainer _logoContainer;
        private WikipediaWebScraper _scraper;

        public LogoController(ILogger<LogoController> logger, WikipediaWebScraper scraper, LogoContainer logos)
        {
            this.logger = logger;
            _scraper = scraper;
            _logoContainer = logos;
        }

        [HttpGet("{clubName}")]
        public async Task<LogoDto> Get(string clubName)
        {
            if (!_logoContainer.ContainsLogoForClubName(clubName))
            {
                var imageSource = await _scraper.GetAddress(clubName);
                if(imageSource == null)
                {
                    //return null when scraper didn't found logo or crashed
                    logger.LogWarning("Unable to get logo for club: " + clubName);
                    return null;
                }

                _logoContainer.AddLogo(clubName, imageSource);
                return new LogoDto(clubName, imageSource);
            }
            return _logoContainer.GetLogoByClubName(clubName).ToLogoDto();
        }
    }
}
