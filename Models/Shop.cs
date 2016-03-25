using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class Shop
    {
        public int ShopId { get; set; }

        public string ShopName { get; set; }

        public int TotalCompletedVisits { get; set; }
        // assumed that there is always the same amount of barbers working.
        public double CutTimeAverage { get; set; }
    }
}
