using ElectronicQueue.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicQueue.Controllers
{

    public class TerminalController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(Database.Database.themes);
        }
        [HttpGet]
        public IActionResult Ticket(string themeId)
        {   
            Theme? theme = Database.Database.themes.Find(theme => theme.Id == themeId);
            if (theme != null)
            {
                int number = Database.Database.queueItems.Count;
                number = 1 + number % 99;
                string t = "";
                if (number < 10) {
                    t = "0" + number.ToString();
                }
                else t = number.ToString();
                QueueItem queueItem = new QueueItem(t, theme);
                Database.Database.queueItems.Add(queueItem);
                return View(queueItem);
            }
            else
            {
                Console.WriteLine("Ошибка. Theme с таким ID не найден");
                return Content("Ошибка");
            }
        }
    }
}
