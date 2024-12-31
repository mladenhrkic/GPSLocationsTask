using Domain.Models;

namespace Infrastructure.Helper;

public static class GenerateApiKey
{
    public static async Task<ApiKey> Run(DatabaseContext context, User user, ApiSettings apiSettings )
    {
        var apiKey = new ApiKey
        {
            Key = Guid.NewGuid().ToString(),
            Expiration = DateTime.Now.AddMinutes(apiSettings.ApiKeyLifetimeMinutes),
            UserId = user.Id
        };
        await context.ApiKeys.AddAsync(apiKey);
        await context.SaveChangesAsync();
        
        return apiKey;
    }
}