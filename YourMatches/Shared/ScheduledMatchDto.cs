using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class ScheduledMatchDto
    {
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public ScoreResult ScoreResult { get; set; }
        public int MatchDay { get; set; }
        public LeagueDto League { get; set; }

        public ScheduledMatchDto()
        {

        }

        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date, Status status, ScoreResult scoreResult, int matchDay, LeagueDto league)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Date = date;
            Status = status;
            ScoreResult = scoreResult;
            MatchDay = matchDay;
            League = league;
        }
    }
}
