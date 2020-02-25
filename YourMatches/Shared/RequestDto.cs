using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class RequestDto
    {
        public List<Status> StatusChecked { get; set; }
        public List<League> LeaguesChecked { get; set; } = new List<League>();
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public RequestDto()
        {

        }
    }
}
