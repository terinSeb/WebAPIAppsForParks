﻿using ParkyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.IRepository
{
    public interface IAccountRepository : IRepository<User>
    {
        Task<User> LoginAsync(string url, User ObjToCreate);
        Task<bool> RegisterAsync(string url, User ObjToCreate);
    }
}
