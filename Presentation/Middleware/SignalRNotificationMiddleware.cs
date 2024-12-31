using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Presentation.Helper;

namespace Presentation.Middleware;

public class SignalRNotificationMiddleware(
    INotificationService notificationService,
    ILogger<SignalRNotificationMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var descriptor = ControllerDescriptor.Get(context);
            var controller = descriptor.ControllerName;
            var actionMethod = descriptor.ActionName;
            var method = context.Request.Method;
            
            await notificationService.NotifyConsoleAsync(controller, method, actionMethod);
            
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError($"An error occurred in the SignalRNotificationMiddleware: {ex.Message}");
            await next(context);
            
        }
    }
}