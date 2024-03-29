﻿using Infrastructure.Services.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<PostHub>("/post-hub");
            webApplication.MapHub<ChatHub>("/chat-hub");
        }
    }
}
