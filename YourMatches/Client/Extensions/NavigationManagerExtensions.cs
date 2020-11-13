using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using YourMatches.Shared;

namespace YourMatches.Client.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T value)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }

                if(typeof(T) == typeof(List<Status>))
                {
                    List<Status> valueAsStatusList = new List<Status>();
                    var temp = valueFromQueryString.ToString().Split(',');
                    foreach (var str in temp)
                    {
                        if (Enum.TryParse<Status>(str, out Status status))
                            valueAsStatusList.Add(status);
                    }
                    value = (T)(object)valueAsStatusList;
                    return true;
                }

                if (typeof(T) == typeof(List<League>))
                {
                    List<League> valueAsLeaguesList = new List<League>();
                    var temp = valueFromQueryString.ToString().Split(',');
                    foreach (var str in temp)
                    {
                        if (Enum.TryParse<League>(str, out League league))
                            valueAsLeaguesList.Add(league);
                    }
                    value = (T)(object)valueAsLeaguesList;
                    return true;
                }

                if(typeof(T) == typeof(DateTime) && DateTime.TryParseExact(valueFromQueryString,"MMddyyyy",CultureInfo.InvariantCulture,DateTimeStyles.None, out var valueAsDateTime))
                {
                    value = (T)(object)valueAsDateTime;
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}
