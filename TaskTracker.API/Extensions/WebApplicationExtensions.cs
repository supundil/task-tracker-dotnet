using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Enums;
using TaskTracker.Application.Interfaces;
using TaskTracker.Infrastructure.Persistence;

namespace TaskTracker.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedAdminUserAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();

            var passwordHasher =
                services.GetRequiredService<IPasswordHasher>();

            var exists = await context.Users.AnyAsync(x =>
                x.Email == "admin@tasktracker.com");

            if (exists)
                return;

            var admin = new User
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                Email = "admin@tasktracker.com",
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            admin.PasswordHash =
                passwordHasher.HashPassword(admin, "Admin@123");

            context.Users.Add(admin);

            await context.SaveChangesAsync();
        }
    }
}