using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.Authentification;

public class RegistrationAuthentication : IRequest<Result<Unit>>
{
    public required User User { get; init; }
}