using Application.Categories.Queries;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.QueriesHandler;

public class GetLocationByFavouriteHandler(ILocationRepository locationRepository) : 
    IRequestHandler<GetLocationByFavourite, Result<List<Location>>>
{
    public async Task<Result<List<Location>>> Handle(GetLocationByFavourite request, CancellationToken cancellationToken)
    {
        return await locationRepository.GetLocationsByFavourite(request.ApiKey);
    }
}