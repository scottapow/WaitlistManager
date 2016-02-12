using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class Barber
    {

        public string FullName { get; set; }

        public List<string> Styles { get; set; }

        public bool CutsF { get; set; }

        public bool CutsM { get; set; }

        public string Bio { get; set; }

        public double AvgCutTime { get; set; }
    }
}
