using ElectronicQueue.Database.Models;
using ElectronicQueue.Database.Models.Enums;
using Microsoft.VisualBasic;
using MvcApp.Models;

namespace ElectronicQueue.Database.Initializers
{
    public static class StatusInitializer
    {
        public static async Task InitializeAsync(DatabaseContext dbContext)
        {
            if (dbContext.Statuses.Any()) return;

            dbContext.Statuses.Add(new Status { Number = (short)QueueElementStatus.None, Name = "В очереди" });
            dbContext.Statuses.Add(new Status { Number = (short)QueueElementStatus.Called, Name = "Подойдите к окну" });
            dbContext.Statuses.Add(new Status { Number = (short)QueueElementStatus.Processing, Name = "Обслуживается" });
            dbContext.Statuses.Add(new Status { Number = (short)QueueElementStatus.Processed, Name = "Завершен" });
            dbContext.Statuses.Add(new Status { Number = (short)QueueElementStatus.Canceled, Name = "Отменен" });

            await dbContext.SaveChangesAsync();
        }
    }
}
