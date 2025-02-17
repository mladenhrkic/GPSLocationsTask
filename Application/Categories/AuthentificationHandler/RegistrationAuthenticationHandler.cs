using Application.Categories.Authentification;
using Domain.Abstractions;
using MediatR;

namespace Application.Categories.AuthentificationHandler;

public class RegistrationAuthenticationHandler(IUserRegistrationRepository userRegistrationRepository)
    : IRequestHandler<RegistrationAuthentication, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RegistrationAuthentication request, CancellationToken cancellationToken)
    {
        return await userRegistrationRepository.RegistrationAsync(request.User);
    }
}