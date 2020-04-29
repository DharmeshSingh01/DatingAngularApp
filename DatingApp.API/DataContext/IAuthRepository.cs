using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DataContext
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string Password);
        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}
