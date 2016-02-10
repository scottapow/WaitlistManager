using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class BarberDataContext : DbContext
    {
        public DbSet<Barber> Barbers { get; set; }

        public BarberDataContext()
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

            mb.Entity<Barber>()
                .Property(x => x.CutsF)
                .HasDefaultValue(false);	
        }
    }
}
