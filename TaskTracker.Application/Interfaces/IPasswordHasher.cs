using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Entities;

namespace TaskTracker.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(User user, string password);

        bool VerifyPassword(User user, string password);
    }
}
