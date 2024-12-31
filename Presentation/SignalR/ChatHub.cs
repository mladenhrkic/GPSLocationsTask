using Microsoft.AspNetCore.SignalR;

namespace Presentation.SignalR;

public class ChatHub : Hub
{
    public async Task JoinGroup(string groupName) => 
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
}