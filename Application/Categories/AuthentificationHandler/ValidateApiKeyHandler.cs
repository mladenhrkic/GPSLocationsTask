using Application.Categories.Authentification;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Categories.AuthentificationHandler;

public class ValidateApiKeyHandler(IUserAuthentificationRepository userAuthentificationRepository)
    : IRequestHandler<ValidateApiKey, Response>
{
    public async Task<Response> Handle(ValidateApiKey request, CancellationToken cancellationToken)
    {
        return await userAuthentificationRepository.ValidateApiKey(request.Key);
    }
}