using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Server.Models
{
    public class Logo
    {
        public string ClubName { get; set; }
        public string ImageSource { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public Logo()
        {

        }

        public Logo(string clubName, string imageSource)
        {
            ClubName = clubName;
            UpdateImageSource(imageSource);
        }

        public LogoDto ToLogoDto()
        {
            return new LogoDto(ClubName, ImageSource);
        }

        public void UpdateImageSource(string newImageSource)
        {
            ImageSource = newImageSource;
            LastUpdated = DateTime.Now;
        }
    }
}
