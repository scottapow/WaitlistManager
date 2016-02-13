using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class Barber
    {
        public int BarberId { get; set; }

        public string FullName { get; set; }

        public bool CutsF { get; set; }

        public bool CutsM { get; set; }

        public string Bio { get; set; }

        public double AvgCutTime { get; set; }

        public string ProfilePicPath { get; set;}

        public int Password { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
