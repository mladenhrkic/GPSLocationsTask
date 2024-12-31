using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Command;

public class CreateLocation : IRequest<Result<List<Location>>>
{
    public required ICollection<Location> Locations { get; init; }
    public required string ApiKey { get; init; }
}