using Microsoft.EntityFrameworkCore;
using ElectronicQueue.Database.Models;

namespace MvcApp.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<QueueItem> QueueItems { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Theme> Themes { get; set; } = null!;
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}