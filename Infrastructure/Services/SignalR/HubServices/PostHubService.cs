using Application.Abstracts.Hubs;
using Infrastructure.Services.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.SignalR.HubServices
{
    public class PostHubService : IPostHubService
    {
        private readonly IHubContext<PostHub> _hubContext;

        public PostHubService(IHubContext<PostHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PostAddedMessageAsync(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.PostAddedMessage,message);
        }
    }
}
