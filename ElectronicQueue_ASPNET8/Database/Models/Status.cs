using System.ComponentModel.DataAnnotations;
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

        [Key]
        public short Number { get; set; }
        public string? Name { get; set; }
    }
}
