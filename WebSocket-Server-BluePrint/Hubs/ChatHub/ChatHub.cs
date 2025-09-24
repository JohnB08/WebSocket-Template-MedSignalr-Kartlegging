using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace WebSocket_Server_BluePrint.Hubs.ChatHub;

/* Denne ChatHub classen er en utvidet class, som arver fra "inheriter" mange properties og metoder fra en grunnlegende Hub, som SignalR har gitt oss.
 
 Legg merke til at jeg ikke har definert hva Clients er, eller hva metodene OthersInGroup er, men det er metoder gjort tilgjengelig for oss
 fra SignalR pakken vi lastet ned. */
public class ChatHub : Hub
{
    /* SendMessageToChannel er en metode som representerer det som heter abstraksjon. Det vil si vi wrapper
     Clients.OthersInGroup metoden i vår egen metode, slik at det er lett for oss å Invoke den fra front-end siden. 
        Det er ganske vanlig å abstraktere vekk litt avansert logikk, samt ting som method chains, som nedenfor, bak enkle metoder som
        representerer det samme. */
    public async Task SendMessageToChannel(string channel, string user, string message)
    {
        await Clients.OthersInGroup(channel).SendAsync("ReceiveMessage", user, message);
    }
    
    /* Her er vår metode som heter JoinChannel, som igjen representerer en abstraksjon, hvor vi kaller Groups.AddToGroupAsync via denne metoden.*/
    public async Task JoinChannel(string channel)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, channel);
    }

    /* Vi har også en leavechannel metode */
    public async Task LeaveChannel(string channel)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);
    }
    
}