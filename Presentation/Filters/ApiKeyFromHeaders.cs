using Microsoft.AspNetCore.Http;

namespace Presentation.Filters;

public static class ApiKeyFromHeaders
{
    public static string Get(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey))
        {
            return apiKey;
        }
        return string.Empty;
    }
}