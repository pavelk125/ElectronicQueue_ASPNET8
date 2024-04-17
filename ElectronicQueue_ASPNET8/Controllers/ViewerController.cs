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
                .Include(q => q.Status)
                .Include(q => q.Theme)
                .Where(qItem => qItem.Status.Number != (int)QueueElementStatus.Processed && qItem.Status.Number != (int)QueueElementStatus.Processing)
                .ToList();
            return View(filteredList);
        }
    }
}
