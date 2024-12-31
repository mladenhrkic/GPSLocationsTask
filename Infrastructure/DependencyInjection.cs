using Domain.Abstractions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options => 
            options.UseSqlite(connectionString));
        
        services.AddScoped<IUserAuthentificationRepository, UserAuthentificationRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
        
       return services;
    }
}