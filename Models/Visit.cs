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

        [Required]
        [MaxLength(20, ErrorMessage = "Your first name is not actually that long... right?")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ScaffoldColumn(false)]
        public bool isMissing { get; set; }

        [ScaffoldColumn(false)]
        public bool isCheckedOff { get; set; }

        [ScaffoldColumn(false)]
        public DateTime SignInTime { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CheckOffTime { get; set; }

        [ScaffoldColumn(false)]
        public int BarberId { get; set; }

        public virtual Barber Barber { get; set; }
    }
}
