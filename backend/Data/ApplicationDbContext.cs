using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
            ) : base(options)
        { }

        public DbSet<Event> Events { get; set; } // reference to the Database - list that represents a table of events in the database
        public DbSet<Report> Reports { get; set; } // reference to the Database - list that represents a table of reports in the database
        public DbSet<EventType> EventTypes { get; set; } // reference to the Database - list that represents a table of EventTypes in the database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.Entity<Event>().HasKey(e => e.Id); // The id is key
            builder.Entity<EventType>()
                .HasData(new List<EventType>
                {
                    new EventType
                    {
                        Id = 1,
                        Type = "Fire"
                    },
                    new EventType
                    {
                        Id = 2,
                        Type = "Collapsing Building"
                    }
                });

            builder.Entity<Report>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey("UserId")
                .IsRequired();
        }
    }
}
