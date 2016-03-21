using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WaitlistManager.Models;

namespace WaitlistManager.ViewModels.Visits
{
    public class CutViewModel
    {
        [Required]
        public int Password { get; set; }

        public bool isValid { get; set; }

        public Visit Visitor { get; set; }
    }
}
