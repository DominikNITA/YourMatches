using System.Collections.Generic;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Server.Services
{
    public interface IMatchRetriever
    {
        Task<List<ScheduledMatchDto>> GetMatches(RequestDto request);
    }
}