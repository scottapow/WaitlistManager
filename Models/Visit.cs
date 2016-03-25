using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class Visit
    {
        public int VisitId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MaxLength(20, ErrorMessage = "Your first name is not actually that long... right?")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        

        [ScaffoldColumn(false)]
        public bool isCheckedOff { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Sign In Time")]
        public DateTime SignInTime { get; set; }
        
        public double WaitTime { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CheckOffTime { get; set; }

        [Display(Name = "Barber Preference")]
        public int? BarberId { get; set; }

        [ForeignKey("BarberId")]
        public virtual Barber Barber { get; set; }
    }
}
