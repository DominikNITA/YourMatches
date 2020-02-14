using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Server.Models;

namespace YourMatches.Server.Services
{
    public class LogoContainer
    {
        public List<Logo> Logos { get; set; }

        public LogoContainer()
        {
            Logos = new List<Logo>();
        }
    }
}
