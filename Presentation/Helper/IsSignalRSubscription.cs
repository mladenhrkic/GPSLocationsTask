using Microsoft.AspNetCore.Http;

namespace Presentation.Helper;

public static class IsSignalRSubscription
{
    public static bool Check(HttpContext httpContext)
    {
        var endPoint = httpContext.GetEndpoint()?.DisplayName;
        return endPoint != null && endPoint.Contains("/chat-hub");
    }
}