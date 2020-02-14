using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YourMatches.Server.Services;
using YourMatches.Shared;

namespace YourMatches.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoController : ControllerBase
    {
        private readonly ILogger<LogoController> logger;
        private LogoContainer _logoContainer;
        private WebScraper _scraper;

        public LogoController(ILogger<LogoController> logger, WebScraper scraper, LogoContainer logos)
        {
            this.logger = logger;
            _scraper = scraper;
            _logoContainer = logos;
        }

        [HttpGet("{clubName}")]
        public async Task<LogoDto> Get(string clubName)
        {
            //TODO: Refactor whole thing
            if (_logoContainer.Logos.Where(logo => logo.ClubName == clubName).ToList().Count() == 0)
            {
                _logoContainer.Logos.Add(new Models.Logo(clubName, null));
                var imageSource = await _scraper.GetAddress(clubName);
                //TODO: Catch errors
                _logoContainer.Logos.Where(logo => logo.ClubName == clubName).First().UpdateImageSource(imageSource);
            }
            return _logoContainer.Logos.Where(logo => logo.ClubName == clubName).First().ToLogoDto();
        }
        //[HttpGet("[action]/dodo")]
        //public async Task<string> TestWebScraper()
        //{
        //    return await _scraper.TestMe();
        //}
    }
}
