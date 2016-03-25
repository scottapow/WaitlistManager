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

        public double CalculateWaitPerVisit(int v, double sca, double bca, int bcv)
        {                   // ((4      -      3)         *         15)     +  (  3   *       18.25)
            double time = (((v - bcv) * sca) + ((bcv * 0.8) * bca));
            return time;
        }
    }
}
