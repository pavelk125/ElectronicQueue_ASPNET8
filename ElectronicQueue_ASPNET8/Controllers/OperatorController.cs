using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ElectronicQueue.Database.Models;
using System.Collections.Generic;
using ElectronicQueue.Database;
using ElectronicQueue.Database.Models.Enums;
using MvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicQueue.Controllers
{
    public class OperatorController : Controller
    {

        DatabaseContext db;
        public OperatorController(DatabaseContext context) {
            db = context;
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

        [HttpGet]
        public IActionResult Call(string qItemId)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.QueueNumber = 0;
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (int)QueueElementStatus.Called);
                qItem.CallTime = DateTime.Now;
                db.SaveChangesAsync();
            }
            else { 
                Console.WriteLine("Ошибка. QueueItem с таким ID не найден"); 
                return NotFound();
            }
            return RedirectToAction("Index", "Operator");
        }
        [HttpPost]
        public IActionResult Call(string qItemId, int queueNumber)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.QueueNumber = 0;
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (int)QueueElementStatus.Called);
                qItem.CallTime = DateTime.Now;
                db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Ошибка. QueueItem с таким ID не найден");
                return NotFound();
            }
            return RedirectToAction("Index", "Operator");
        }

        [HttpPost]
        public IActionResult Process(string qItemId)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (short)QueueElementStatus.Processing);
                qItem.StartProcessTime = DateTime.Now;
                db.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Ошибка. QueueItem с таким ID не найден");
                return NotFound();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult End(string qItemId)
        {
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.Status = db.Statuses.FirstOrDefault(s => s.Number == (short)QueueElementStatus.Processed);
                qItem.EndProcessTime = DateTime.Now;
                db.SaveChangesAsync();
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
