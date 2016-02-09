using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaitlistManager.Models
{
    public class SignInDataContext : DbContext
    {
        public DbSet<Patron> Patrons { get; set; }

        public SignInDataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Database=AspNetBlog";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Patron>()
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn();

            mb.Entity<Patron>()
                .Property(x => x.isCheckedOff)
                .HasDefaultValue(false);
        }
    }
}