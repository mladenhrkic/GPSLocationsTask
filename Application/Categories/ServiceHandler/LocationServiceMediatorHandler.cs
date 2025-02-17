using Application.Categories.Service;
using Domain.Models;
using Domain.Services;
using MediatR;

namespace Application.Categories.ServiceHandler;

public class LocationServiceMediatorHandler(ILocationService locationService)
    : IRequestHandler<LocationServiceMediator, ICollection<Location>>
{
    public async Task<ICollection<Location>> Handle(LocationServiceMediator request,
        CancellationToken cancellationToken)
    {
        return await locationService.GetLocationsAsync(
            request.HttpClientFactory, request.Configuration, request.GeoCoordinate);
    }
}