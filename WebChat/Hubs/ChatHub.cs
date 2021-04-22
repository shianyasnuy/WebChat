using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebChat.Models;

namespace WebChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, int messageId, string userName, 
            string time, string textToReply, string userToReply)
        {
            await this.Clients.All.SendAsync("Send", message, messageId, userName,
                time, textToReply, userToReply);
        }
    }
}
