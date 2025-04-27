using Microsoft.AspNetCore.SignalR;

namespace PNTest.SignalR
{
  
    public class RequestHub : Hub
    {
        public async Task SendNewRequestNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNewRequest", message);
        }
    }
}
