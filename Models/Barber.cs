using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WaitlistManager.Models
{
    public class Barber
    {
        public int BarberId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "We're almost positive you have a name")]
        [Display(Name = "Barber Preference")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Well, do you or don't you?")]
        [Display(Name = "Cuts Female Styles")]
        public bool CutsF { get; set; }

        [Required(ErrorMessage = "Well, do you or don't you?")]
        [Display(Name = "Cuts Male Styles")]
        public bool CutsM { get; set; }

        [MaxLength(400, ErrorMessage = "That's too long. Have you no brevity?")]
        public string Bio { get; set; }

        [ScaffoldColumn(false)]
        public double AvgCutTime { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Profile Image")]
        public string ProfilePicPath { get; set;}

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("\\d{5}", ErrorMessage = "Password must be a 5 digits, no letters or special characters.")]
        public int Password { get; set; }

        [ScaffoldColumn(false)]
        public bool IsAdmin { get; set; }

        public List<Visit> Visits  { get; set; }

        public int VisitAmount { get; set; }
        
    }
}
