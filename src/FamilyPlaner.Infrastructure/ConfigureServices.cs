using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Infrastructure.Identity;
using FamilyPlaner.Infrastructure.Persistence;
using FamilyPlaner.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FamilyPlaner.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services
          .ConfigureApplicationDatabase(configuration)
          .ConfigureAuthentication(configuration)
          .AddScoped<ApplicationDatabasesInitializer>()
          .AddScoped<EntitySaveChangesInterceptor>(); ;

        return services;
    }

    public static async Task InitDatabases(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDatabasesInitializer>();
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
    }

    private static IServiceCollection ConfigureApplicationDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDatabase<ApplicationDbContext>(configuration, "ApplicationDb");

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services
          .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret")))
                };
            });

        services
          .ConfigureDatabase<IdentityDbContext>(configuration, "IdentityDb")
          .AddAuthorization();

        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }

    private static IServiceCollection ConfigureDatabase<TContext>(this IServiceCollection services, IConfiguration configuration, string connectionName) where TContext : DbContext
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<TContext>(options =>
                options.UseInMemoryDatabase(connectionName));
        }
        else
        {
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(connectionName),
                    builder => builder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
        }

        return services;
    }
}
