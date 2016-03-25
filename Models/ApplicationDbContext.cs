using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace WaitlistManager.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Shop> Shops { get; set; }
        
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new ApplicationDbConfigurations(builder.Entity<Visit>(), builder.Entity<Barber>());
        }
    }
}
