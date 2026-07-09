using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskTracker.API.Extensions
{
    public static class AuthenticationExtensions
    {

        public static IServiceCollection AddJwtAuthentication(
    this IServiceCollection services,
    IConfiguration configuration)
{
    Console.WriteLine("===== JWT CONFIGURATION =====");
    Console.WriteLine($"Key: {configuration["Jwt:Key"]}");
    Console.WriteLine($"Issuer: {configuration["Jwt:Issuer"]}");
    Console.WriteLine($"Audience: {configuration["Jwt:Audience"]}");
    Console.WriteLine($"Expiry: {configuration["Jwt:ExpiryMinutes"]}");
    Console.WriteLine("=============================");

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],

                    ValidAudience = configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
        });

    services.AddAuthorization();

    return services;
}
    }
}
