using ApplicationLayer.Interfaces;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<RegisteredUsers> registeredUsers { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        public DbSet<AssignTasks> assignTasks { get; set; }
        public DbSet<UserSuccess> userSuccesses { get; set; }

      
    }
}
