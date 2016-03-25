using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Services
{
    public interface IWaitCalculator
    {
        DateTime CalculateWait(int visitors, double waitPerVisit);

        double CalculateWaitPerVisit(int visitors, double shopCutAve, double barberCutAve, int barberCurrentVisitors);
    }
}

