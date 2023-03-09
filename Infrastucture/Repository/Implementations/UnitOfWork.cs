using Infrastucture.Jwt;
using Infrastucture.Persistence.Context;
using Infrastucture.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Appsetting _jwt;
        public UnitOfWork(ApplicationDbContext context,IOptions<Appsetting>jwt)
        {
            _jwt = jwt.Value;
            _context = context;
            registerUsers = new RegisteredUsersRepository(_context);
            loginUser = new LoginVmRepository(_context,jwt);
            tasks =new TasksRepository(_context);    
            assignTasks = new AssignTasksRepository(_context);
            userSuccess= new UserSuccessRepository(_context);
        }

        public IRegisterUsers registerUsers { get; private set; }

        public ILoginUser loginUser { get; private set; }

        public ITasks tasks { get; private set; }

        public IAssignTasks assignTasks { get; private set; }

        public IUserSuccess userSuccess { get; private set; }
    }
}
