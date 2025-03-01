using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Queries;

public class GetLocationByCity : IRequest<Result<List<Location>>>
{
    public required string City { get; init; }
}