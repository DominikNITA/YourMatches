using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Client.Services
{
    public class LogoDtoContainer
    {
        public List<LogoDto> Logos { get; set; }

        public LogoDtoContainer()
        {
            Logos = new List<LogoDto>();
        }
    }
}
