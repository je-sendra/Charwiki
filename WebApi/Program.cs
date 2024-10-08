using System.Text;
using Charwiki.WebApi.Configuration;
using Charwiki.WebApi.Services;
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

        // Configure security settings
        builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection("SecuritySettings"));

        // Add JWT Authentication
        var securitySettings = builder.Configuration.GetSection("SecuritySettings").Get<SecuritySettings>()!;
        builder.Services.AddJwtAuth(securitySettings);

        // Register custom services
        builder.Services.AddScoped<IAuthService, AuthService>();

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

        app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// Adds JWT Authentication to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="securitySettings"></param>
    private static void AddJwtAuth(this IServiceCollection services, SecuritySettings securitySettings)
    {
        var secret = securitySettings.JwtSettings.Secret;
        var secretKey = Encoding.ASCII.GetBytes(secret);

        // Configure Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = securitySettings.JwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = securitySettings.JwtSettings.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                ValidateLifetime = true
            };
        });
    }
}
