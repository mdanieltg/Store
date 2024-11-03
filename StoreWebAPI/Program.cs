using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Store.WebAPI.DataAccess;
using StoreWebAPI.DataAccess;
using StoreWebAPI.Services;
using StoreWebAPI.Util;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ApplicationEnvironment>();
builder.Services.AddControllers();

// JWT
byte[] signingKeyString = RandomNumberGenerator.GetBytes(32);
SymmetricSecurityKey signingKey = new(signingKeyString);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddSingleton(new SecurityKeyProvider { Key = signingKey });
builder.Services.AddSingleton<JwtService>();

// Data
string connectionString = builder.Configuration.GetConnectionString("Default")
                          ?? throw new NullReferenceException("Default connection string not found");

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion("8.0.39")));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerSetup();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
