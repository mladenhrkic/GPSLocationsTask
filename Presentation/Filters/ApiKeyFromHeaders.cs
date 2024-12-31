using System.Net;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Presentation.Helper;

namespace Presentation.Filters;

public static class ApiKeyFromHeaders
{
    public static string Get(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey))
        {
            return apiKey;
        }
        throw new UnauthorizedAccessException("ApiKey not found in headers.");
        
    }
        
}