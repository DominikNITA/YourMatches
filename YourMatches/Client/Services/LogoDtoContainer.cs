using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Client.Services
{
    public class LogoDtoContainer
    {
        private List<LogoDto> Logos { get; set; }

        public LogoDtoContainer()
        {
            Logos = new List<LogoDto>();
        }

        public void AddLogo(LogoDto logoDto)
        {
            Logos.Add(logoDto);
        }

        public string GetLogoSourceByClubName(string clubName)
        {
            return Logos.Where(l => l.ClubName == clubName).Select(l => l.ImageSource).FirstOrDefault();
        }
    }
}
