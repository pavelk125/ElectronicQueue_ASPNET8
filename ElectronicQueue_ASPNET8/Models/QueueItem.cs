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

        public QueueItem(string number, Theme theme) : this()
        {
            ThemeId = theme.Id;
            Theme = theme;
            Number = number;
            Status? status = Database.statusList.Find(s => s.Number == (short)QueueElementStatus.None);
            if(status == null)
            {
                throw new Exception();
            }
            Status = status;
        }
        public string Id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? CallTime { get; set; }
        public DateTime? StartProcessTime { get; set; }
        public DateTime? EndProcessTime { get; set; }
        public string ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public string Number { get; set; }
        public Status? Status { get; set; }
        public int? QueueNumber { get; set; }
    }
}