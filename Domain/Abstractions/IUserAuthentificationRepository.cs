using Domain.Models;

namespace Domain.Abstractions;

public interface IUserAuthentificationRepository
{
    Task<Result<ApiKey>> Authenticate(User user);
    Task<Response> ValidateApiKey(string apiKey);
}