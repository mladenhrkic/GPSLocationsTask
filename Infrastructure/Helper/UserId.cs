using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helper;

public static class UserId
{
    public static async Task<int> Get(DatabaseContext context, string ApiKey)
    {
        var result = await context.ApiKeys.FirstOrDefaultAsync(x => x.Key == ApiKey);
        return result!.UserId;
    }
}