using Infrastructure.Services.SignalR.HubDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Operation
{
    public static class ClientSource
    {
        public static List<Client> Clients { get; } = new List<Client>();
    }
}
