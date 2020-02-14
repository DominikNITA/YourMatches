using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class LogoDto
    {
        public string ClubName { get; set; }
        public string ImageSource { get; set; }
        public LogoDto()
        {
                
        }

        public LogoDto(string clubName, string imageSource)
        {
            ClubName = clubName;
            ImageSource = imageSource;
        }
    }
}
