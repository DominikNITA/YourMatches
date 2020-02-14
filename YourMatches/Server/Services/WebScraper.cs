using AngleSharp.Html.Parser;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;

namespace YourMatches.Server.Services
{
    public class WebScraper
    {
        private HttpClient _http;
        private string startingUrl = "https://en.wikipedia.org/wiki/";
        /* "https://seeklogo.net/?s=";*/
        /*"https://www.google.com/search?tbm=isch&q=logo+";*/

        public WebScraper(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetAddress(string clubName)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            string sanitizedClubName = clubName.Replace(' ', '_');
            string url = startingUrl + sanitizedClubName;
            var document = await context.OpenAsync(url);
            //var list = document.QuerySelector(".infobox");
            //var str = list.GetAttribute("Href");
            //document = await context.OpenAsync(str);
            IElement image;
            try
            {
                image = document.QuerySelector(".infobox.vcard").QuerySelector("tbody").QuerySelector("tr").QuerySelector("td").QuerySelector("a").QuerySelector("img");
            }
            catch (Exception)
            {
                return null;
            }
            return image?.GetAttribute("src");
            //var source = image.GetAttribute("src");
            //return source;
        }
    }
}
