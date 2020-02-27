using System;
using System.Collections.Generic;
using System.Text;

namespace YourMatches.Shared
{
    public class ScoreResult
    {
        public Result Result { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public ScoreResult(int homeGoals, int awayGoals)
        {
            HomeGoals = homeGoals;
            AwayGoals = awayGoals;
            if (HomeGoals > AwayGoals)
            {
                Result = Result.HOME_TEAM;
            }
            else if (AwayGoals > HomeGoals)
            {
                Result = Result.AWAY_TEAM;
            }
            else
            {
                Result = Result.DRAW;
            }
        }
        public ScoreResult()
        {
            Result = Result.NONE;
            HomeGoals = 0;
            AwayGoals = 0;
        }
    }
}
