using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using YourMatches.Shared;

namespace YourMatches.Server.Services
{
    public class ApiHelper
    {
        private List<Timer> callCounter;
        private int maxCallsPerInterval;
        private int interval;

        public static readonly Dictionary<League, string> LeaguesCodes = new Dictionary<League, string>()
        {
            { League.ENGLAND_1, "PL" },
            { League.SPAIN_1, "PD" },
            { League.FRANCE_1, "FL1" },
            { League.GERMANY_1, "BL1" },
            { League.ITALY_1, "SA" }
        };
        public ApiHelper()
        {
            callCounter = new List<Timer>();
        }

        public ApiHelper(int maxCallsPerInterval, int interval)
        {
            this.callCounter = new List<Timer>();
            this.maxCallsPerInterval = maxCallsPerInterval;
            this.interval = interval;
        }

        public bool CheckCallAvaibilty()
        {
            if (callCounter.Count < maxCallsPerInterval)
            {
                var timer = new Timer();
                timer.Interval = interval * 1000;
                timer.Elapsed += new ElapsedEventHandler(DeleteCall);
                timer.Start();
                callCounter.Add(timer);
                return true;
            }
            return false;
        }
        private void DeleteCall(object sender, EventArgs e)
        {
            if (callCounter.Count > 0)
                callCounter.RemoveAt(0);
        }
    }
}
