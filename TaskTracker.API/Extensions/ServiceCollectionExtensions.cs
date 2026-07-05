using FluentValidation;
using FluentValidation.AspNetCore;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Application.Validators;
using TaskTracker.Infrastructure.SignalR;
using TaskTracker.Infrastructure.Security;

namespace TaskTracker.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IPasswordHasher, PasswordHasherService>();

            services.AddScoped<INotificationService, NotificationService>();

            services.AddSignalR();

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<CreateTaskRequestValidator>();

            return services;
        }
    }
}
