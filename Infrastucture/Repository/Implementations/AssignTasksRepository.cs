using DomainLayer.Entities;
using Infrastucture.Persistence.Context;
using Infrastucture.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Implementations
{
    public class AssignTasksRepository:GenericRepository<AssignTasks>,IAssignTasks
    {
        private readonly ApplicationDbContext _context;
        public AssignTasksRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
