using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<RegisteredUsers> registeredUsers { get; set; }
        DbSet<Tasks> tasks { get; set; }
        DbSet<AssignTasks> assignTasks { get; set; }
        DbSet<UserSuccess> userSuccesses { get; set; }
    }
}
