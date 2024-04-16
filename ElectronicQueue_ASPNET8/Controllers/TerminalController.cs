using ElectronicQueue.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Models;

namespace ElectronicQueue.Controllers
{

    public class TerminalController : Controller
    {
        DatabaseContext db;
        public TerminalController(DatabaseContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            db.Statuses.ToList();
            var theme = db.Themes.FirstOrDefault(t => t.Id!=null);
            return View(db.Themes.Where(c => true).ToList());
        }
        [HttpGet]
        public IActionResult Ticket(string themeId)
        {   
            Theme? theme = db.Themes.FirstOrDefault(x => x.Id == themeId);

            if (theme != null)
            {
                int number = db.QueueItems.Count();
                number = 1 + number % 99;
                string t = "";
                if (number < 10) {
                    t = "0" + number.ToString();
                }
                else t = number.ToString();
                QueueItem queueItem = new QueueItem(t, theme, 0);
                db.QueueItems.Add(queueItem);
                db.SaveChangesAsync();
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
