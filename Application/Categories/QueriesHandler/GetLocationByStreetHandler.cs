using Application.Categories.Queries;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.QueriesHandler;

public class GetLocationByStreetHandler(ILocationRepository locationRepository)
    : IRequestHandler<GetLocationByStreets, Result<List<Location>>>
{
    public async Task<Result<List<Location>>> Handle(GetLocationByStreets request, CancellationToken cancellationToken)
    {
        return await locationRepository.GetLocationsByStreet(request.Street);
    }
}