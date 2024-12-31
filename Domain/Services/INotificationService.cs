namespace Domain.Services;

public interface INotificationService
{
    Task NotifyConsoleAsync(string controller, string methodName, string actionMethod);
    Task NotifyUserAsync(string message);
}