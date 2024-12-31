using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Command;

public class RemoveFavourite : IRequest<Result<Unit>>
{
    public required List<LocationId> LocationId { get; init; }
    public required string ApiKey { get; init; }
}