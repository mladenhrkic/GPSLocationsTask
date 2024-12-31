using Application.Categories.Command;
using Domain.Abstractions;
using MediatR;

namespace Application.Categories.CommandHandler;

public class SetFavouriteHandler(ILocationRepository locationRepository) : IRequestHandler<SetFavourite, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SetFavourite request, CancellationToken cancellationToken)
    {
        return await locationRepository.SetLocationsFavourite(request.LocationId, request.ApiKey);
    }
}