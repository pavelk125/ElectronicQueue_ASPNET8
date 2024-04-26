using ElectronicQueue.Database.Models;
using ElectronicQueue.Database.Models.Enums;
using ElectronicQueue.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MvcApp.Models;
using Newtonsoft.Json;

namespace ElectronicQueue.Controllers
{

    public class TerminalController : Controller
    {
        private readonly DatabaseContext db;
        private readonly IHubContext<QueueHub> hubContext;
        public TerminalController(DatabaseContext context, IHubContext<QueueHub> hubContext)
        {
            db = context;
            this.hubContext = hubContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            db.Statuses.ToList();
            var theme = db.Themes.FirstOrDefault(t => t.Id!=null);
            return View(db.Themes.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Ticket(string themeId)
        {   
            Theme? theme = db.Themes.FirstOrDefault(theme => theme.Id == themeId);

            if (theme != null)
            {
                int number = db.QueueItems.Count();
                number = 1 + number % 99;
                string t = "";
                if (number < 10) {
                    t = "0" + number.ToString();
                }
                else t = number.ToString();

                QueueItem queueItem = new QueueItem(t, themeId, (int)QueueElementStatus.None);

                db.QueueItems.Add(queueItem);
                await db.SaveChangesAsync();

                var q = db.QueueItems
                    .Include(q => q.Status)
                    .Include(q => q.Theme)
                    .FirstOrDefault(q => q.Id == queueItem.Id);

                await hubContext.Clients.All.SendAsync("TicketUpdateHandler", q);
                return View(queueItem);
            }
            else
            {
                Console.WriteLine("Ошибка. Theme с таким ID не найден");
                return NotFound();
            }
        }
    }
}
