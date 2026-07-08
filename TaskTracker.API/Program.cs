//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models;
//using System.Text;
//using TaskTracker.Application.Entities;
//using TaskTracker.Application.Enums;
//using TaskTracker.Application.Interfaces;
//using TaskTracker.Application.Services;
using TaskTracker.Infrastructure.Extensions;
//using TaskTracker.Infrastructure.Persistence;
//using TaskTracker.Infrastructure.Security;
using TaskTracker.API.Extensions;
using TaskTracker.Infrastructure.SignalR;
//using FluentValidation;
//using FluentValidation.AspNetCore;
//using TaskTracker.Application.Validators;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],

//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
//        };
//    });

//builder.Services.AddAuthorization();
//builder.Services.AddControllers();
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskRequestValidator>();
//builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<ITaskService, TaskService>();
//builder.Services.AddSignalR();
//builder.Services.AddScoped<INotificationService, NotificationService>();
//builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Task Tracker API",
//        Version = "v1"
//    });

//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization using Bearer scheme.",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//});

builder.Services.AddControllers();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowLocalhost5173", policy =>
//     {
//         policy.WithOrigins("http://localhost:5173")
//               .AllowAnyHeader()
//               .AllowAnyMethod()
//               .AllowCredentials();
//     });
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://6a4ebc033b967c0008a785f8--tasktrackerdotnetreact.netlify.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    if (!context.Users.Any(x => x.Email == "admin@tasktracker.com"))
//    {
//        var hasher = new PasswordHasherService();

//        var admin = new User
//        {
//            Id = Guid.NewGuid(),
//            Name = "Administrator",
//            Email = "admin@tasktracker.com",
//            Role = UserRole.Admin,
//            CreatedAt = DateTime.UtcNow,
//            UpdatedAt = DateTime.UtcNow
//        };

//        admin.PasswordHash =
//            hasher.HashPassword(admin, "Admin@123");

//        context.Users.Add(admin);
//        context.SaveChanges();
//    }
//}

await app.SeedAdminUserAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

//app.UseCors("AllowLocalhost5173");
app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<TaskHub>("/hubs/tasks");

app.Run();
