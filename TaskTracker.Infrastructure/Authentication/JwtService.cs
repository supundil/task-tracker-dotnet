using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskTracker.Application.Entities;
using TaskTracker.Application.Interfaces;

namespace TaskTracker.Infrastructure.Authentication
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateToken(User user)
{
    Console.WriteLine($"Jwt:Key = {_configuration["Jwt:Key"]}");
    Console.WriteLine($"Jwt:Issuer = {_configuration["Jwt:Issuer"]}");
    Console.WriteLine($"Jwt:Audience = {_configuration["Jwt:Audience"]}");
    Console.WriteLine($"Jwt:ExpiryMinutes = {_configuration["Jwt:ExpiryMinutes"]}");

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256);


    var expiryMinutes = int.TryParse(
    _configuration["Jwt:ExpiryMinutes"],
    out var minutes)
        ? minutes
        : 60;

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
    }
}
