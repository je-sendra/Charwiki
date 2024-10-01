using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Charwiki.WebApi;

/// <summary>
/// Represents the program entry point.
/// </summary>
public static class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Register the database context.
        builder.Services.AddDbContext<CharwikiDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("CharwikiDbContext"));
        });

        // Configure JWT Settings
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"]  ?? throw new NoNullAllowedException("Secret not found in configuration");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var key = Encoding.ASCII.GetBytes(secretKey);

        // Configure Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ValidateLifetime = true
            };
        });

        // Add Authorization
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Add Authentication & Authorization Middleware
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
