using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace YourMatches.Server.Services
{
    public class ApiHelper
    {
        private List<Timer> callCounter;
        private int maxCallsPerInterval;
        private int interval;

        public ApiHelper()
        {
            callCounter = new List<Timer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCallsPerInterval"></param>
        /// <param name="interval">Time in seconds</param>
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
