using backend.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Models;

namespace backend.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }

        public DbSet<Event> Events { get; set; }
        /*
        public static List<Event> Events = new List<Event>
        {
            new Event{Id = 1, Location = "Event 1", Date = new DateTime()},
            new Event{Id = 2, Location = "Event 2", Date = new DateTime()},
            new Event{Id = 3, Location = "Event 3", Date = new DateTime()}

        };        
        
        public static List<User> Users = new List<User>
        {
            new User{Id = 1, Password = "1111", userName = "odeya"},
            new User{Id = 2, Password = "2222", userName = "david"}
        };        
        
        public static List<Report> Reports = new List<Report>
        {

        };
        */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = localhost;Database=events;Trusted_Connection=True;");
        
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
