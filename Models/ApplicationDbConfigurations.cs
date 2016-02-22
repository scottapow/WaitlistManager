using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class ApplicationDbConfigurations
    {
        public ApplicationDbConfigurations(EntityTypeBuilder<Visit> visit, EntityTypeBuilder<Barber> barber)
        {

            // Visit Data Context Configuration

            visit.Property(v => v.isCheckedOff)
                .HasDefaultValue(false);
            visit.Property(v => v.isMissing)
                .HasDefaultValue(false);

            

            // Barber Data Context Configuration  
            


      
        }
    }
}
