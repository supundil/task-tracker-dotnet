using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Interfaces;

namespace TaskTracker.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
