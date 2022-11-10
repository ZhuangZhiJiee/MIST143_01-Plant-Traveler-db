using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
        public async Task AclientMessage(string message)
        {
            string Uid = GetConnectionId();
            await Clients.All.SendAsync("AclientMessageFunction", message, Uid);
        }
    }
}
