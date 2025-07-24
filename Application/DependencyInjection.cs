using System.Net.NetworkInformation;
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

        int test32 = 35;

        string tt = "test";

        decimal tt = 44m;

        return services;
    }


    private static string VisualStudio()
    {
        return "";
    }

    private static void GitHub(){
        var num = 23;
    }
}
