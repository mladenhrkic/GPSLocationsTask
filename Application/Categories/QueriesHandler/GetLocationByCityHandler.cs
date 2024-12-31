using Application.Categories.Queries;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.QueriesHandler;

public class GetLocationByCityHandler(ILocationRepository locationRepository)
    : IRequestHandler<GetLocationByCity, Result<List<Location>>>
{
    public async Task<Result<List<Location>>> Handle(GetLocationByCity request, CancellationToken cancellationToken)
    {
        return await locationRepository.GetLocationsByCity(request.City);
    }
}