using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;

namespace ParkyAPI.Repository.IRepository
{
    interface IUserRepository
    {
        bool IsUniqueUser(string UserName);
        Users Authenticate(string Username, string Password);
        Users Register(string Username, string Password);
    }
}
