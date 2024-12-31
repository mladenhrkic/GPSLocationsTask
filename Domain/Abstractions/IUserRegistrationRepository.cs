using Domain.Models;
using MediatR;

namespace Domain.Abstractions;

public interface IUserRegistrationRepository
{
    Task<Result<Unit>> RegistrationAsync(User user);
}