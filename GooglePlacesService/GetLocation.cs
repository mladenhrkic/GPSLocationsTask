using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.Configuration;

namespace GooglePlacesService;

public class GetLocation : ILocationService
{
    public async Task<ICollection<Location>> GetLocationsAsync(IHttpClientFactory factory, 
        IConfiguration configuration, GeoCoordinate coordinates)
    {
        var request = new HttpRequest(factory, configuration);
        var root = await request.Send(coordinates);

        return ParsingResponse.Action(root);
    }
}