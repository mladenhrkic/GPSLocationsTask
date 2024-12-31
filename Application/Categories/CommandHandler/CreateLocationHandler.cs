using Application.Categories.Command;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.CommandHandler;

public class CreateLocationHandler(ILocationRepository locationRepository) : 
    IRequestHandler<CreateLocation, Result<List<Location>>>
{
    public async Task<Result<List<Location>>> Handle(CreateLocation request, CancellationToken cancellationToken)
    {
        return await locationRepository.CreateLocations(request.Locations, request.ApiKey);
    }
}