using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;
using Presentation.Filters;
using Presentation.Helper;

namespace Presentation.Middleware;

public class SynchronousRequestMiddleware : IMiddleware
{
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _userQueues = new();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (IsLoginAction.Check(context))
        {
            await next(context);
            return;
        }

        var apiKey = ApiKeyFromHeaders.Get(context);
        if (string.IsNullOrEmpty(apiKey))
        {
            await next(context);
            return;
        }
        
        var semaphore = _userQueues.GetOrAdd(apiKey, _ => new SemaphoreSlim(1, 1));

        await semaphore.WaitAsync(context.RequestAborted);

        try
        {
            await next(context);
        }
        finally
        {
            semaphore.Release();
            if (semaphore.CurrentCount == 1 && _userQueues.TryRemove(apiKey, out var removedSemaphore))
            {
                removedSemaphore.Dispose();
            }
        }
    }
}