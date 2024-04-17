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
        public QueueItem(string number, string themeId, int statusNumber) : this()
        {
            ThemeId = themeId;
            Number = number;
            StatusId = statusNumber;
        }
        public string Id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? CallTime { get; set; }
        public DateTime? StartProcessTime { get; set; }
        public DateTime? EndProcessTime { get; set; }
        [ForeignKey("Theme")]
        public string ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public string Number { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public int? QueueNumber { get; set; }
    }
}