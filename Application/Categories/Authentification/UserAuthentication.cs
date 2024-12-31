using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Authentification;

public class UserAuthentication : IRequest<Result<ApiKey>>
{
    public required User User { get; init; }
}