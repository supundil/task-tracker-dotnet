using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Interfaces;

namespace TaskTracker.Infrastructure.Security
{
    public class PasswordHasherService: IPasswordHasher
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password) == PasswordVerificationResult.Success;
        }
    }
}
