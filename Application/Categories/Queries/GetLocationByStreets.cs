using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Queries;

public class GetLocationByStreets : IRequest<Result<List<Location>>>
{
    public required string Street { get; init; } 
}