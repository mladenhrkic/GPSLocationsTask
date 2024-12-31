using Domain.Abstractions;
using Domain.Models;
using Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories;

public class UserAuthentificationRepository(DatabaseContext context, IOptions<ApiSettings> apiSettings)
    : IUserAuthentificationRepository
{
    private readonly ApiSettings _apiSettings = apiSettings.Value;

    public async Task<Result<ApiKey>> Authenticate(User user)
    {
        var authenticatedUser = await context.Users.SingleOrDefaultAsync(u => 
            u.Username == user.Username && u.Password == user.Password);

        if (authenticatedUser == null)
        {
            return Result<ApiKey>.Failure(Error.Unauthorized("User.Unauthorized", 
                "Invalid username or password"));
        }
        
        var apiKey = await GenerateApiKey.Run(context, authenticatedUser, _apiSettings);
        return Result<ApiKey>.Success(apiKey);
    }
    
    public async Task<Response> ValidateApiKey(string apiKey)
    {
        var response = new Response();
        var key = await context.ApiKeys.SingleOrDefaultAsync(k => k.Key == apiKey);
        response.Ok = key != null && key.Expiration > DateTime.UtcNow;
        return response;
    }
}