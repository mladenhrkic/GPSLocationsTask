using Application.Enum;
using Application.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        services.PlacesFactory(Places.GooglePlaces);

        return services;
    }


    private static int VisualStudio23()
    {
        return 0;
    }

    private static decimal test()
    {
        return 0;
    }

    private static void GitHub(){
        var num = 23;
    }
}
