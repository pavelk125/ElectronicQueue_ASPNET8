using ElectronicQueue.Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicQueue.Controllers
{
    public class ViewerController : Controller
    {
        public IActionResult Index()
        {
            var filteredList = Database.Database.queueItems.Where(qItem => qItem.Status.Number != (short)QueueElementStatus.Processed && qItem.Status.Number != (short)QueueElementStatus.Processing).ToList();
            return View(filteredList);
        }
    }
}
