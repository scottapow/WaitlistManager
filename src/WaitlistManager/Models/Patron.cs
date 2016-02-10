using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class Patron
    {
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Barber BarberPreference { get; set; }

        public bool isCheckedOff { get; set; }

        public DateTime SignInTime { get; set; }
    }
}
