using DomainLayer.Entities;
using Infrastucture.Persistence.Context;
using Infrastucture.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Implementations
{
    public class RegisteredUsersRepository : GenericRepository<RegisteredUsers>, IRegisterUsers
    {
        public RegisteredUsersRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}
