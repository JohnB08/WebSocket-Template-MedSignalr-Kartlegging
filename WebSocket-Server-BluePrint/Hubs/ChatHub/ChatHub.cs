using Microsoft.AspNetCore.SignalR;

namespace WebSocket_Server_BluePrint.Hubs.ChatHub;

public class ChatHub : Hub
{
    public async Task SendMessageToChannel(string channel, string user, string message)
    {
        await Clients.OthersInGroup(channel).SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinChannel(string channel)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, channel);
    }

    public async Task LeaveChannel(string channel)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);
    }
    
}