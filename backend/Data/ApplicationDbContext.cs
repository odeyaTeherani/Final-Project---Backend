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
using IdentityModel;

namespace backend.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }

        public DbSet<Event> Events { get; set; } // reference to the Database - list that represents a table of events in the database
        public DbSet<User> Users { get; set; } // reference to the Database - list that represents a table of users in the database
        public DbSet<Report> Reports { get; set; } // reference to the Database - list that represents a table of reports in the database
    }
}
