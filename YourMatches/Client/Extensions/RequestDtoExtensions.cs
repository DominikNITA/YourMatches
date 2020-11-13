using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Client.Extensions
{
    public static class RequestDtoExtensions
    {
        public static Dictionary<string, string> GetQueryDictionary(this RequestDto requestDto)
        {
            var query = new Dictionary<string, string>();

            string statusString = String.Join(",", requestDto.StatusChecked.Where(status => !RequestDto.AlwaysSelectedStatus.Contains(status)).Select(status => status.ToString()));
            query.Add("status", statusString);

            string leaguesString = String.Join(",", requestDto.LeaguesChecked.Select(league => league.ToString()));
            query.Add("league", leaguesString);

            string startingDateString = requestDto.StartingDate.ToString("MMddyyyy");
            query.Add("start", startingDateString);

            if (requestDto.IsEndingDateSelected)
            {
                string endingDateString = requestDto.EndingDate.ToString("MMddyyyy");
                query.Add("end", endingDateString);
            }

            return query;
        }

        public static bool TryGetStateFromQuery(this RequestDto requestDto, NavigationManager navigationManager)
        {
            if(navigationManager.TryGetQueryString<List<Status>>("status", out var statusList))
            {
                requestDto.StatusChecked = statusList;
            }
            else
            {
                Console.WriteLine("Failed on parsing status");
            }

            if (navigationManager.TryGetQueryString<List<League>>("league", out var leaguesList))
            {
                requestDto.LeaguesChecked = leaguesList;
            }
            else
            {
                Console.WriteLine("Failed on parsing leagues");
            }

            if (navigationManager.TryGetQueryString<DateTime>("start", out var startingDate))
            {
                requestDto.StartingDate = startingDate;
            }
            else
            {
                Console.WriteLine("Failed on parsing starting date");
            }

            if (navigationManager.TryGetQueryString<DateTime>("end", out var endingDate))
            {
                requestDto.EndingDate = endingDate;
                requestDto.IsEndingDateSelected = true;
            }
            else
            {
                Console.WriteLine("Failed on parsing ending date");
            }
            return true;
        }
    }
}
