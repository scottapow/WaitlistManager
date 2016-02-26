using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaitlistManager.Services;

namespace WaitlistManager.Services
{
    public class WaittimeService : IWaitCalculator
    {
        // Should return the longest waittime based on the amount of visitors
        public DateTime CalculateWait(int visitors, double waitPerVisit)
        {
            DateTime _now = DateTime.Now;
            for (int i = 0; i < visitors; i++)
            {
                _now = _now.AddMinutes(waitPerVisit);
            }
            return _now;
        }
    }
}
