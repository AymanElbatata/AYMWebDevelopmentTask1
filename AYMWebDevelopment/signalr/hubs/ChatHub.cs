using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AYMWebDevelopment.signalr.hubs
{
    public class ChatHub : Hub
    {
        public void Send(string Type, string AutoCode, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(Type, AutoCode, message);
        }
    }
}