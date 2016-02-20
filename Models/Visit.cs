using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class Visit
    {
        public int VisitId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool isMissing { get; set; }

        public bool isCheckedOff { get; set; }

        public DateTime SignInTime { get; set; }

        public DateTime CheckOffTime { get; set; }

        public int BarberId { get; set; }

        public virtual Barber Barber { get; set; }
    }
}
