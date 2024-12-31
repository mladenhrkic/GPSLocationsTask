using Domain.Models;
using MediatR;

namespace Application.Categories.Authentification;

public class ValidateApiKey : IRequest<Response>
{
    public required string? Key { get; init; }
}