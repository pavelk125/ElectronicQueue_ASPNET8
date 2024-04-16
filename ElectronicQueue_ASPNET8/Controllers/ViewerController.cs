using ElectronicQueue.Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Models;

namespace ElectronicQueue.Controllers
{
    public class ViewerController : Controller
    {
        DatabaseContext db;
        public ViewerController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var filteredList = db.QueueItems
                .Where(qItem => qItem.Status.Number != (short)QueueElementStatus.Processed && qItem.Status.Number != (short)QueueElementStatus.Processing)
                .Include(t => t.Theme)
                .ToList();
            return View(filteredList);
        }
    }
}
