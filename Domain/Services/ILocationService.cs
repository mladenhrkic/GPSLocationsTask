using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Domain.Services;

public interface ILocationService
{
    Task<ICollection<Location>> GetLocationsAsync(
        IHttpClientFactory factory, 
        IConfiguration configuration, 
        GeoCoordinate coordinates);
}