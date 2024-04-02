using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicQueue.Database.Models
{
    public class Status 
    {
        public Status() { 

        }
        public Status(short number, string name)
        {
            Number = number;
            Name = name;
        }
        public short Number { get; set; }
        public string? Name { get; set; }
    }
}
