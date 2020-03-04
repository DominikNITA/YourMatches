using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using YourMatches.Shared;

namespace YourMatches.Server.Services
{
    public class ApiCallLimiter
    {
        private List<Timer> callCounter;
        private int _maxCallsPerInterval;
        private int _interval;

        public ApiCallLimiter()
        {
            callCounter = new List<Timer>();
            _maxCallsPerInterval = 1;
            _interval = 1;
        }

        /// <param name="maxCallsPerInterval"></param>
        /// <param name="interval">Interval in seconds</param>
        public ApiCallLimiter(int maxCallsPerInterval, int interval)
        {
            callCounter = new List<Timer>();
            _maxCallsPerInterval = maxCallsPerInterval;
            _interval = interval;
        }

        public bool CheckCallAvaibilty()
        {
            if (callCounter.Count < _maxCallsPerInterval)
            {
                var timer = new Timer();
                timer.Interval = _interval * 1000;
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
