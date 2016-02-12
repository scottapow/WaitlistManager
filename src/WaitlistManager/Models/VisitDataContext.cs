using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class VisitDataContext : DbContext
    {
        public DbSet<Visit> Visits { get; set; }

        public VisitDataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Database=WaitlistManager";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Visit>()
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            mb.Entity<Visit>()
                .Property(x => x.isCheckedOff)
                .HasDefaultValue(false);

            mb.Entity<Visit>()
                .Property(x => x.FirstName)
                .IsRequired();

            mb.Entity<Visit>()
                .Property(x => x.BarberPreference)
                .HasDefaultValue("none");
        }
    }
}
