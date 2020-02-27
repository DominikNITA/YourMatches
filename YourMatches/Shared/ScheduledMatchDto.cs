using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class ScheduledMatchDto
    {
        //TODO: Delete unused ctors
        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Date = date;
        }
        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date, Status status, ScoreResult result) : this(homeTeam, awayTeam, date)
        {
            Status = status;
            ScoreResult = result;
        }
        public ScheduledMatchDto()
        {

        }

        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date, Status status, ScoreResult result, int matchDay) : this(homeTeam, awayTeam, date, status, result)
        {
            MatchDay = matchDay;
        }

        public ScheduledMatchDto(TeamDto homeTeam, TeamDto awayTeam, DateTime date, Status status, ScoreResult result, int matchDay, LeagueDto league) : this(homeTeam, awayTeam, date, status, result, matchDay)
        {
            League = league;
        }

        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public ScoreResult ScoreResult { get; set; }
        public int MatchDay { get; set; }
        public LeagueDto League { get; set; }
    }
}
