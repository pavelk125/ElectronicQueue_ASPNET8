using System;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicQueue.Database;
using ElectronicQueue.Database.Models.Enums;

namespace ElectronicQueue.Database.Models
{
   
    public class QueueItem 
    {
        public QueueItem()
        {
            Id = Guid.NewGuid().ToString("D");
            AddTime = DateTime.Now;
        }

        public QueueItem(string number, Theme theme, int statusId) : this()
        {
            ThemeId = theme.Id;
            Theme = theme;
            Number = number;
            StatusId = statusId;
        }
        public string Id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? CallTime { get; set; }
        public DateTime? StartProcessTime { get; set; }
        public DateTime? EndProcessTime { get; set; }
        public string ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public string Number { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public int? QueueNumber { get; set; }
    }
}