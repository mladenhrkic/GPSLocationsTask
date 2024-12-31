using Domain.Services;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.SignalR;

public class NotificationService(IHubContext<ChatHub> hubContext) : INotificationService
{
    public async Task NotifyConsoleAsync(string controller, string methodName, string actionMethod)
    {
        var message = $"Requested http controller: {controller}, method: {methodName}, actionMethod: {actionMethod}";
        await hubContext.Clients.Group("ConsoleAppGroup").SendAsync("ReceiveMessage", message);
    }

    public async Task NotifyUserAsync(string message)
    {
        await hubContext.Clients.Group("TestGroup").SendAsync("ReceiveMessage", $"{message}");
    }
}
