using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaitlistManager.Services;

namespace WaitlistManager.Services
{
    public class AverageService : IAverageCalculator
    {
        public double CalculateAverage(double currentAve, int completedVisits, double cutLength)
        {
            double newAverage = 0.0;
            // if there is no average, because this is the first cut, simple add the cutlength
            if (currentAve <= 0 || Double.IsNaN(currentAve))
            {
                newAverage = cutLength;
            }
            // if this is the 2nd || > cut a new average must calculate
            else {
                newAverage = (((currentAve * completedVisits) + cutLength) / (completedVisits + 1));
            }
            return newAverage;
        }
    }
}
