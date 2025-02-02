using Application.Enum;
using Domain.Services;
using GooglePlacesService;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Factory
{
    public static class PlacesServiceCollection
    {
        public static IServiceCollection PlacesFactory(this IServiceCollection services, Places places)
        {
            return places switch
            {
                Places.GooglePlaces => services.AddScoped<ILocationService, GetLocation>(),
                _ => services
            };
        }
    }
}