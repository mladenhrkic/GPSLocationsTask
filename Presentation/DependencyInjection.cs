using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Presentation.Filters;
using Presentation.Middleware;
using Presentation.SignalR;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthorizationWithMiddleware API", Version = "v1" });
            c.OperationFilter<AddRequiredHeaderParameter>();
        });

        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddTransient<ApiKeyMiddleware>();
        services.AddScoped<SignalRNotificationMiddleware>();
        services.AddSingleton<SynchronousRequestMiddleware>();
        services.AddSignalR();
        
        services.AddSingleton<INotificationService, NotificationService>();
        
        return services;
    }
}