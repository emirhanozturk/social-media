using Domain.Entities.Identity;
using Infrastructure.Operation;
using Infrastructure.Services.SignalR.HubDatas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task GetUserName(string userName)
        
        
        {
            Client clients = new Client
            {
                ConnectionId = Context.ConnectionId,
                Username= userName,
            };

            ClientSource.Clients.Add(clients);
            await Clients.Others.SendAsync("clientJoined",userName);
        }
    }

    
}
