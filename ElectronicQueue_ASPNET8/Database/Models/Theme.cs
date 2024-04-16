using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ElectronicQueue.Database.Models
{

    public class Theme
    {
        public Theme()
        {
            Id = Guid.NewGuid().ToString("D");
        }
        public string Id { get; set; }
        public string ThemeName { get; set; }

        public string Description { get; set; }

    }
}