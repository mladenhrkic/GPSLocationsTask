using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Categories.Service;

public class LocationServiceMediator : IRequest<ICollection<Location>>
{
    public required IHttpClientFactory HttpClientFactory { get; init; }
    public required IConfiguration Configuration { get; init; } 
    public required GeoCoordinate GeoCoordinate { get; init; }
}