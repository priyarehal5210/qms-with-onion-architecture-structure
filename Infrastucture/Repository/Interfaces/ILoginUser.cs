using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Interfaces
{
    public interface ILoginUser
    {
        RegisteredUsers Login(string email, string password);
    }
}
