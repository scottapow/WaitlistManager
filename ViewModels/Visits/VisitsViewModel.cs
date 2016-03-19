using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaitlistManager.Models;

namespace WaitlistManager.ViewModels.Visits
{
    public class VisitsViewModel
    {
        public IEnumerable<Visit> Visits { get; set; }
        public Visit Visit { get; set; }
    }
}
