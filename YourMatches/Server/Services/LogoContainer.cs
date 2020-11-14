using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Server.Models;

namespace YourMatches.Server.Services
{
    public class LogoContainer
    {
        private List<Logo> _logos { get; set; }

        public LogoContainer()
        {
            _logos = new List<Logo>();
        }

        public bool ContainsLogoForClubName(string clubName)
        {
            return _logos.Where(logo => logo.ClubName == clubName).ToList().Count() > 0;
        }

        public void AddLogo(Logo logo)
        {
            _logos.Add(logo);
        }

        public void AddLogo(string clubName, string imageSource)
        {
            _logos.Add(new Logo(clubName, imageSource));
        }

        public Logo GetLogoByClubName(string clubName)
        {
            return _logos.Where(l => l.ClubName == clubName).First();
        }
    }
}
