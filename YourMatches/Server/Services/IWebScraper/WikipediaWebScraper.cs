using AngleSharp.Html.Parser;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace YourMatches.Server.Services.IWebScraper
{
    public class WikipediaWebScraper : IWebScraper
    {
        private HttpClient _http;
        private string startingUrl = "https://en.wikipedia.org/wiki/";

        public WikipediaWebScraper(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetAddress(string clubName)
        {
            try
            {
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                string sanitizedClubName = clubName.Replace(' ', '_');
                string url = startingUrl + sanitizedClubName;
                var document = await context.OpenAsync(url);
                IElement image = document.QuerySelector(".infobox.vcard").QuerySelector("tbody").QuerySelector("tr").QuerySelector("td").QuerySelector("a").QuerySelector("img");
                return image?.GetAttribute("src");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
