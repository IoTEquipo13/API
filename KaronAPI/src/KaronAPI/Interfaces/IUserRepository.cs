using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KaronAPI.Models;

namespace KaronAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<string> Create(User user);
    }
}
