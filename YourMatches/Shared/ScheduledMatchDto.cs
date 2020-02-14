using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class ScheduledMatchDto
    {
        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Date = date;
        }
        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date, Status status, Result result) : this(homeTeam, awayTeam, date)
        {
            Status = status;
            Result = result;
        }
        public ScheduledMatchDto()
        {

        }
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public Result Result { get; set; }
    }
}
