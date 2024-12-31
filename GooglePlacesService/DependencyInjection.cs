using Microsoft.Extensions.DependencyInjection;

namespace GooglePlacesService;

public static class DependencyInjection
{
    public static IServiceCollection AddGooglePlacesServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        return services;
    }
}