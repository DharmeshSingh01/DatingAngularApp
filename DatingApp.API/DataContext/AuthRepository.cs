using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.DataContext
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContexts _context;
        public AuthRepository(DataContexts dataContext)
        {
            _context = dataContext;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username.ToLower());
            if(user == null)
            {
                return null;
            }
             if(!VerifyPassword(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedpass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedpass.Length;i++)
                {
                    if(passwordHash[i]!=computedpass[i])
                    {
                        return false;
                    }
                    
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string Password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHas(Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = user.Username.ToLower();
           await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        private void CreatePasswordHas(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username.ToLower()))
                return true;
            return false;
        }
    }
}
