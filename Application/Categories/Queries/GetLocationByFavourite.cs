using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Queries;
public class GetLocationByFavourite : IRequest<Result<List<Location>>>
{
    public required string ApiKey { get; init; }
}