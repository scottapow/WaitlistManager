using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WaitlistManager.Models
{
    public class Barber
    {
        public int BarberId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "We're almost positive you have a name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Well, do you or don't you?")]
        public bool CutsF { get; set; }

        [Required(ErrorMessage = "Well, do you or don't you?")]
        public bool CutsM { get; set; }

        public string Bio { get; set; }

        [ScaffoldColumn(false)]
        public double AvgCutTime { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ProfilePicPath { get; set;}

        [DataType(DataType.Password)]
        [RegularExpression("/^\\d{5}$/", ErrorMessage = "Password must be a 5 digits, no letters or special characters.")]
        public int Password { get; set; }

        [ScaffoldColumn(false)]
        public bool IsAdmin { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
