using Application.Categories.Authentification;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.AuthentificationHandler;

public class UserAuthenticationHandler(IUserAuthentificationRepository userAuthentificationRepository)
    : IRequestHandler<UserAuthentication, Result<ApiKey>>
{
    public async Task<Result<ApiKey>> Handle(UserAuthentication request, CancellationToken cancellationToken)
    {
        return await userAuthentificationRepository.Authenticate(request.User);
    }
}