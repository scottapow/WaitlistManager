using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Services
{
    public interface IAverageCalculator
    {
        double CalculateAverage(double currentAve, int completedVisits, double cutLength);
    }
}
