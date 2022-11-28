using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EFCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EFCore.data
{
    public class Context : DbContext
    {
        public DbSet<Facility> Facilities { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<BusinessUser> BusinessUsers { get; set; } = default!;
        public DbSet<MaintenanceIntervention> MaintenanceInterventions { get; set; } = default!;
        public DbSet<Participant> Participants { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator<string>("Discriminator")
                .HasValue<BusinessUser>("BusinessUser")
                .HasValue<User>("User");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.js", false);
            IConfiguration config = builder.Build();
            string connString = config.GetConnectionString("MyConnString");
            optionsBuilder.UseSqlServer(connString);
            
            //optionsBuilder.UseInMemoryDatabase("FacilityDatabase");
        }

        
    }
    
}
