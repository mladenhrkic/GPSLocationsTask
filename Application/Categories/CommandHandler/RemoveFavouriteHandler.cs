using Application.Categories.Command;
using Domain.Abstractions;
using MediatR;

namespace Application.Categories.CommandHandler;

public class RemoveFavouriteHandler(ILocationRepository locationRepository)
    : IRequestHandler<RemoveFavourite, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveFavourite request, CancellationToken cancellationToken)
    {
        return await locationRepository.RemoveFavourite(request.LocationId, request.ApiKey);
    }
}