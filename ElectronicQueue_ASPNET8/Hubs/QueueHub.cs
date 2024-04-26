using ElectronicQueue.Database.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ElectronicQueue.Hubs
{
    public class QueueHub : Hub
    {
        public Task TicketUpdate(QueueItem queueItem)
        {
            var json = JsonConvert.SerializeObject(queueItem);
            return Clients.Others.SendAsync("TicketUpdateHandler", json);
        }
    }
}
