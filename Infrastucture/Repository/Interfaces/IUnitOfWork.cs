using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IRegisterUsers registerUsers { get; }
        ILoginUser loginUser { get; }
        ITasks tasks { get; }
        IAssignTasks assignTasks { get; }
        IUserSuccess userSuccess { get; }
    }
}
