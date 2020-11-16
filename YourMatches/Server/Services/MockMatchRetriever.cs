using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Server.Services
{
    public class MockMatchRetriever : IMatchRetriever
    {
        public async Task<List<ScheduledMatchDto>> GetMatches(RequestDto request)
        {
            var result = new List<ScheduledMatchDto>();
            result.Add(new ScheduledMatchDto("Liverpool FC", "Manchester United", DateTime.Now.Subtract(TimeSpan.FromHours(3)), Status.FINISHED, new ScoreResult(2, 1), 13, new LeagueDto("Premiere League", "UK")));
            result.Add(new ScheduledMatchDto("FC Barcelona", "Manchester City", DateTime.Now, Status.IN_PLAY, new ScoreResult(0, 3), 4, new LeagueDto("Champions League", "Europe")));
            result.Add(new ScheduledMatchDto("Bayern Munchen", "Ukala Munchen", DateTime.Now.AddHours(1), Status.SCHEDULED, new ScoreResult(null, null), 12, new LeagueDto("Bundesliga", "Germany")));
            result.Add(new ScheduledMatchDto("Juventus", "Napoli FC", DateTime.Now.AddHours(5), Status.POSTPONED, new ScoreResult(null, null), 7, new LeagueDto("Serie a", "Italy")));

            return result;  
        }
    }
}
