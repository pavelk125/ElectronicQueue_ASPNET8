using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ElectronicQueue.Database.Models;
using System.Collections.Generic;
using ElectronicQueue.Database;
using ElectronicQueue.Database.Models.Enums;
using MvcApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ElectronicQueue.Hubs;

namespace ElectronicQueue.Controllers
{
    //[Authorize(Roles = "a,b,c")]
    public class OperatorController : Controller
    {
        private readonly DatabaseContext db;
        private readonly IHubContext<QueueHub> hubContext;
        public OperatorController(DatabaseContext context, IHubContext<QueueHub> hubContext) {
            db = context;
            this.hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var filteredList = db.QueueItems
                .Include(q => q.Status)
                .Include(q => q.Theme)
                .Where(qItem => qItem.Status.Number != (int)QueueElementStatus.Processed).ToList();
            return View(filteredList);
        }


        [HttpPost]
        public async Task<IActionResult> Call(string qItemId, int queueNumber)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.QueueNumber = 0;
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (int)QueueElementStatus.Called);
                qItem.CallTime = DateTime.Now;
                await db.SaveChangesAsync();

                var q = db.QueueItems
                    .Include(q => q.Status)
                    .Include(q => q.Theme)
                    .FirstOrDefault(q => q.Id == qItem.Id);

                await hubContext.Clients.All.SendAsync("TicketUpdateHandler", q);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Ошибка. QueueItem с таким ID не найден");
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Process(string qItemId)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (short)QueueElementStatus.Processing);
                qItem.StartProcessTime = DateTime.Now;
                await db.SaveChangesAsync();

                var q = db.QueueItems
                    .Include(q => q.Status)
                    .Include(q => q.Theme)
                    .FirstOrDefault(q => q.Id == qItem.Id);

                await hubContext.Clients.All.SendAsync("TicketUpdateHandler", q);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Ошибка. QueueItem с таким ID не найден");
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> End(string qItemId)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (short)QueueElementStatus.Processed);
                qItem.EndProcessTime = DateTime.Now;
                await db.SaveChangesAsync();

                var q = db.QueueItems
                    .Include(q => q.Status)
                    .Include(q => q.Theme)
                    .FirstOrDefault(q => q.Id == qItem.Id);

                await hubContext.Clients.All.SendAsync("TicketUpdateHandler", q);
                await db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Ошибка. QueueItem с таким ID не найден");
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
