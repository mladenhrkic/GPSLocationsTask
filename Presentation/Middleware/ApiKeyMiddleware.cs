using Application.Categories.Authentification;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Presentation.Filters;
using Presentation.Helper;
using ApiKey = Domain.Models.ApiKey;

namespace Presentation.Middleware;

public class ApiKeyMiddleware(IMediator mediator) : IMiddleware
{
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
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is required.");
            return;
        }

        var result = await IsApiKeyValid(apiKey);
        if (!result)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await next(context);
    }

    private async Task<bool> IsApiKeyValid(string extractedApiKey)
    {
        var model = new ValidateApiKey{ Key = extractedApiKey };
        var result = await mediator.Send(model);
        return result.Ok;
       
    }
}