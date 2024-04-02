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
            var filteredList = db.QueueItems.Where(qItem => qItem.Status.Number != (short)QueueElementStatus.Processed);
            //var filteredList = Database.Database.queueItems.Where(qItem => qItem.Status.Number != (short)QueueElementStatus.Processed).ToList();
            return View(filteredList);
        }

        [HttpGet]
        public IActionResult Call(string qItemId)
        {
            //QueueItem? qItem = Database.Database.queueItems.Find(qItem => qItem.Id == qItemId);
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.QueueNumber = 0;
                qItem.Status = Database.Database.statusList.Find(s => s.Number == (short)QueueElementStatus.Called);
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
            //QueueItem? qItem = Database.Database.queueItems.Find(qItem => qItem.Id == qItemId);
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.QueueNumber = queueNumber;
                qItem.Status = Database.Database.statusList.Find(s => s.Number == (short)QueueElementStatus.Called);
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
        public IActionResult Process(string qItemId)
        {
            //QueueItem? qItem = Database.Database.queueItems.Find(qItem => qItem.Id == qItemId);
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.Status = Database.Database.statusList.Find(s => s.Number == (short)QueueElementStatus.Processing);
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
            //QueueItem? qItem = Database.Database.queueItems.Find(qItem => qItem.Id == qItemId);
            var qItem = db.QueueItems.FirstOrDefault(qItem => qItem.Id == qItemId);
            if (qItem != null)
            {
                qItem.Status = Database.Database.statusList.Find(s => s.Number == (short)QueueElementStatus.Processed);
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
