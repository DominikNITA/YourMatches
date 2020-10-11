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
        private List<Timer> _callCounter;
        private int _maxCallsPerInterval;
        private int _interval;

        public ApiCallLimiter()
        {
            _callCounter = new List<Timer>();
            _maxCallsPerInterval = 1;
            _interval = 1;
        }

        /// <param name="maxCallsPerInterval"></param>
        /// <param name="interval">Interval in seconds</param>
        public ApiCallLimiter(int maxCallsPerInterval, int interval)
        {
            _callCounter = new List<Timer>();
            _maxCallsPerInterval = maxCallsPerInterval;
            _interval = interval;
        }

        public bool CheckCallAvailability()
        {
            if (_callCounter.Count < _maxCallsPerInterval)
            {
                var timer = new Timer();
                timer.Interval = _interval * 1000;
                timer.Elapsed += new ElapsedEventHandler(DeleteCall);
                timer.Start();
                _callCounter.Add(timer);
                return true;
            }
            return false;
        }
        private void DeleteCall(object sender, EventArgs e)
        {
            if (_callCounter.Count > 0)
                _callCounter.RemoveAt(0);
        }
    }
}
